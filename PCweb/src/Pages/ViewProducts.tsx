import { useState, useEffect } from 'react';
import { Link, useSearchParams, useNavigate } from 'react-router-dom';
import Header from '../Components/Header-Component/Header';
import Navbar from '../Components/Header-Component/Navbar';
import hero from '../assets/hero.png';
import { useProductsData, GetBrandsInSameCategory, type ProductItem } from '../hooks/useProductsData';
import { useCartContext } from '../context/CartContext';
import './ViewProducts.css';

const DEFAULT_CATEGORY_ID = '15';
const PRICE_SLIDER_MAX = 5000;
const DEFAULT_MAX_PRICE = 10000;

export default function ViewProducts() {
	const [searchParams, setSearchParams] = useSearchParams();
	const navigate = useNavigate();
	const { addItem } = useCartContext();

	const categoryId = searchParams.get('categoryId') || DEFAULT_CATEGORY_ID;
	const currentSort = searchParams.get('sort') || 'A-Z';
	const selectedBrand = searchParams.get('brand');
	const searchQuery = searchParams.get('search') || undefined;
	const isSearchMode = Boolean(searchQuery);
	const isPickingPart = Boolean(localStorage.getItem('selectingFor'));

	const [page, setPage] = useState(1);
	const [minPrice, setMinPrice] = useState(searchParams.get('minPrice') ? Number(searchParams.get('minPrice')) : 0);
	const [maxPrice, setMaxPrice] = useState(searchParams.get('maxPrice') ? Number(searchParams.get('maxPrice')) : DEFAULT_MAX_PRICE);
	const [brands, setBrands] = useState<string[]>([]);

	const { products, loading, categoryName } = useProductsData(categoryId, selectedBrand || undefined, page, searchQuery);

	// Fetch brands when categoryId changes (not relevant when searching across categories)
	useEffect(() => {
		const fetchBrands = async () => {
			if (categoryId && !isSearchMode) {
				try {
					const brandList = await GetBrandsInSameCategory(categoryId);
					if (brandList && Array.isArray(brandList)) {
						setBrands(brandList);
					}
				} catch (err) {
					console.error('Error fetching brands:', err);
				}
			}
		};
		fetchBrands();
	}, [categoryId, isSearchMode]);

	const handleFilterChange = (brand: string, checked: boolean) => {
		const nextParams = new URLSearchParams(searchParams);

		if (checked) {
			nextParams.set('brand', brand);
		} else {
			nextParams.delete('brand');
		}

		setSearchParams(nextParams);
	};

	const handleSortChange = (sortValue: string) => {
		const nextParams = new URLSearchParams(searchParams);

		nextParams.set('sort', sortValue);
		setSearchParams(nextParams);
	};

	const handlePriceChange = (type: 'min' | 'max', value: string) => {
		const nextParams = new URLSearchParams(searchParams);
		const numValue = value === '' ? 0 : Number(value);

		if (type === 'min') {
			setMinPrice(numValue);
			if (numValue > 0) {
				nextParams.set('minPrice', String(numValue));
			} else {
				nextParams.delete('minPrice');
			}
		} else {
			setMaxPrice(numValue);
			if (numValue < PRICE_SLIDER_MAX) {
				nextParams.set('maxPrice', String(numValue));
			} else {
				nextParams.delete('maxPrice');
			}
		}

		setSearchParams(nextParams);
	};

	const handleNextPage = () => setPage(page + 1);
	const handlePrevPage = () => setPage(page > 1 ? page - 1 : 1);

	const handleAddToCart = (product: ProductItem) => {
		addItem({
			id: product.id,
			name: product.name,
			price: product.price,
			quantity: 1,
		});
		navigate('/cart');
	};

	const handleSelectForPartPicker = (product: ProductItem) => {
		try {
			const componentName = localStorage.getItem('selectingFor');
			if (!componentName) {
				console.error('No component selected for part picker.');
				return;
			}

			const selectedParts = JSON.parse(localStorage.getItem('selectedParts') || '{}');
			selectedParts[componentName] = {
				id: product.id,
				name: product.name,
				price: product.price,
				stock: 10,
			};

			localStorage.setItem('selectedParts', JSON.stringify(selectedParts));
			localStorage.removeItem('selectingFor');
			localStorage.removeItem('selectingCategoryId');

			navigate('/partpicker');
		} catch (err) {
			console.error('Error selecting product:', err);
		}
	};

	const effectiveMinPrice = minPrice || 0;
	const effectiveMaxPrice = maxPrice || DEFAULT_MAX_PRICE;

	const visibleProducts = products
		.filter(product => product.price >= effectiveMinPrice && product.price <= effectiveMaxPrice)
		.sort((a, b) => {
			switch (currentSort) {
				case 'A-Z':
					return a.name.localeCompare(b.name);
				case 'price-low-high':
					return a.price - b.price;
				case 'price-high-low':
					return b.price - a.price;
				default:
					return 0;
			}
		});

	if (loading) {
		return <div className='view-products-loading'>Loading products...</div>;
	}

	return (
		<>
			<Header />
			<Navbar />

			<main className='view-products-page'>
				<aside className='filters-panel'>
					<PriceFilter minPrice={minPrice} maxPrice={maxPrice} onPriceChange={handlePriceChange} />

					{!isSearchMode && (
						<BrandFilterList brands={brands} selectedBrand={selectedBrand} onFilterChange={handleFilterChange} />
					)}
				</aside>

				<section className='results-panel'>
					<ResultsHeader isSearchMode={isSearchMode} searchQuery={searchQuery} categoryName={categoryName} />

					{!isSearchMode && (
						<BrandChips brands={brands} selectedBrand={selectedBrand} onFilterChange={handleFilterChange} />
					)}

					<Toolbar resultCount={visibleProducts.length} currentSort={currentSort} onSortChange={handleSortChange} />

					<div className='product-grid'>
						{visibleProducts.map((product) => (
							<ProductCard
								key={product.id}
								product={product}
								isPickingPart={isPickingPart}
								onAddToCart={handleAddToCart}
								onSelectForPartPicker={handleSelectForPartPicker}
							/>
						))}
					</div>

					<Pagination page={page} onPrevPage={handlePrevPage} onNextPage={handleNextPage} />
				</section>
			</main>
		</>
	);
}

// ====================================================================================
// Sidebar filters
// ====================================================================================

interface PriceFilterProps {
	minPrice: number;
	maxPrice: number;
	onPriceChange: (type: 'min' | 'max', value: string) => void;
}

function PriceFilter({ minPrice, maxPrice, onPriceChange }: PriceFilterProps) {
	return (
		<section className='filter-block'>
			<div className='filter-name-row'>
				<h3>Price</h3>
			</div>
			<div className='price-filter'>
				<div className='price-filter-row'>
					<input
						type='range'
						min='0'
						max={PRICE_SLIDER_MAX}
						step='10'
						value={minPrice}
						onChange={(e) => onPriceChange('min', e.target.value)}
					/>
					<div className='price-filter-label'>Min: ${minPrice}</div>
				</div>
				<div className='price-filter-row'>
					<input
						type='range'
						min='0'
						max={PRICE_SLIDER_MAX}
						step='10'
						value={maxPrice}
						onChange={(e) => onPriceChange('max', e.target.value)}
					/>
					<div className='price-filter-label'>Max: ${maxPrice}</div>
				</div>
			</div>
		</section>
	);
}

interface BrandFilterProps {
	brands: string[];
	selectedBrand: string | null;
	onFilterChange: (brand: string, checked: boolean) => void;
}

function BrandFilterList({ brands, selectedBrand, onFilterChange }: BrandFilterProps) {
	return (
		<section className='filter-block'>
			<div className='filter-name-row'>
				<h3>Brand</h3>
			</div>

			<ul>
				{brands.map((brand) => (
					<li key={brand}>
						<div className='filter-option-row'>
							<input
								type='checkbox'
								checked={selectedBrand === brand}
								onChange={(event) => onFilterChange(brand, event.target.checked)}
							/>
							<span>{brand}</span>
						</div>
					</li>
				))}
			</ul>
		</section>
	);
}

function BrandChips({ brands, selectedBrand, onFilterChange }: BrandFilterProps) {
	return (
		<div className='brand-row'>
			<h2>Brand</h2>
			<div className='brand-chips'>
				{brands.map((brand) => (
					<button
						key={brand}
						type='button'
						onClick={() => onFilterChange(brand, selectedBrand !== brand)}
						className='chip chip-link'
					>
						{brand}
					</button>
				))}
			</div>
		</div>
	);
}

// ====================================================================================
// Results panel
// ====================================================================================

interface ResultsHeaderProps {
	isSearchMode: boolean;
	searchQuery?: string;
	categoryName: string;
}

function ResultsHeader({ isSearchMode, searchQuery, categoryName }: ResultsHeaderProps) {
	if (isSearchMode) {
		return (
			<header className='results-header'>
				<h1>Search results for "{searchQuery}"</h1>
				<p>Showing products matching "{searchQuery}".</p>
			</header>
		);
	}

	return (
		<header className='results-header'>
			<h1>{categoryName}</h1>
			<p>
				Browse our collection of {categoryName.toLowerCase()} components. Find the perfect hardware for your needs.
			</p>
		</header>
	);
}

interface ToolbarProps {
	resultCount: number;
	currentSort: string;
	onSortChange: (sortValue: string) => void;
}

function Toolbar({ resultCount, currentSort, onSortChange }: ToolbarProps) {
	return (
		<div className='toolbar'>
			<p>{resultCount} results</p>
			<div className='toolbar-actions'>
				<label htmlFor='sort'>Sortering</label>
				<select id='sort' value={currentSort} onChange={(event) => onSortChange(event.target.value)}>
					<option value='A-Z'>A-Z</option>
					<option value='price-low-high'>Price low-high</option>
					<option value='price-high-low'>Price high-low</option>
				</select>
				<button type='button' className='layout-btn' aria-label='Wijzig weergave'>
					☰
				</button>
			</div>
		</div>
	);
}

interface ProductCardProps {
	product: ProductItem;
	isPickingPart: boolean;
	onAddToCart: (product: ProductItem) => void;
	onSelectForPartPicker: (product: ProductItem) => void;
}

function ProductCard({ product, isPickingPart, onAddToCart, onSelectForPartPicker }: ProductCardProps) {
	return (
		<article className='product-card'>
			<Link to={`/products/${product.id}`} state={{ product }}>
				<img src={hero} alt={product.name} />
			</Link>

			<p className='brand'>{product.categoryName}</p>
			<h3>{product.name}</h3>
			<p className='subname'>{Object.values(product.fields).slice(0, 2).join(' • ')}</p>

			<div className='price-row'>
				<p className='price'>${product.price.toFixed(2)}</p>
			</div>

			<p className='delivery'>Order by 16:00, delivered tomorrow</p>

			{isPickingPart && (
				<button type='button' className='partpicker-select-btn' onClick={() => onSelectForPartPicker(product)}>
					Select for PartPicker
				</button>
			)}

			<button type='button' className='add-to-cart-btn' onClick={() => onAddToCart(product)}>
				Add to Cart
			</button>
		</article>
	);
}

interface PaginationProps {
	page: number;
	onPrevPage: () => void;
	onNextPage: () => void;
}

function Pagination({ page, onPrevPage, onNextPage }: PaginationProps) {
	return (
		<div className='pagination'>
			<button onClick={onPrevPage} disabled={page === 1}>Previous</button>
			<span>Page {page}</span>
			<button onClick={onNextPage}>Next</button>
		</div>
	);
}
