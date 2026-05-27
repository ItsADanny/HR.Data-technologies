import React, { useState } from 'react';
import { Link, useSearchParams } from 'react-router-dom';
import Header from '../Components/Header-Component/Header';
import Navbar from '../Components/Header-Component/Navbar';
import hero from '../assets/hero.png';
import { useProductsData } from '../hooks/useProductsData';
import './ViewProducts.css';

interface ProductField {
	categoryName: string;
	productID: number;
	name: string;
	price: number;
	fieldName: string;
	fieldValue: string;
}

interface FilterOption {
	name: string;
}

interface FilterSection {
	name: string;
	options: FilterOption[];
}

interface ProductCard {
	id: number;
	categoryName: string;
	name: string;
	price: number;
	fields: Record<string, string>;
	image: string;
}

const filterSections: FilterSection[] = [
	{
		name: 'Brand',
		options: [
			{ name: 'GoPro'},
			{ name: 'DJI'},
			{ name: 'Insta360'}
		]
	},
	{
		name: 'Resistentie',
		options: [
            { name: 'waterproof'},
			{ name: 'Water resistant'},
			{ name: 'Schok resistent'},
        ]
	},
	{
		name: 'Serie',
		options: [
			{ name: 'GoPro HERO'},
			{ name: 'Insta360 GO'},
			{ name: 'DJI Osmo'}
		]
	},
	{
		name: 'Videoresolutie',
		options: [
			{ name: 'Full HD'},
			{ name: '4K'},
			{ name: 'HD Ready'}
		]
	}
];


const brands = ['GoPro', 'DJI', 'Insta360', 'Denver', 'WOLFANG', 'Akaso', 'Strex', 'Salora', 'Vynox'];

const productsPath = '/viewproducts';

const filterQueryKeys: Record<string, string> = {
	Brand: 'brand',
	Resistentie: 'resistance',
	Serie: 'series',
	Videoresolutie: 'resolution',
};

export default function ViewProducts() {
	const [searchParams, setSearchParams] = useSearchParams();
	const categoryId = searchParams.get('categoryId') || '15';
	const currentSort = searchParams.get('sort') || 'A-Z';
	const [page, setPage] = useState(1);
	const [minPrice, setMinPrice] = useState(searchParams.get('minPrice') ? Number(searchParams.get('minPrice')) : 0);
	const [maxPrice, setMaxPrice] = useState(searchParams.get('maxPrice') ? Number(searchParams.get('maxPrice')) : 10000);
	const { products, loading, categoryName, error } = useProductsData(categoryId, page);

	const handleFilterChange = (sectionName: string, optionName: string, checked: boolean) => {
		const filterKey = filterQueryKeys[sectionName];

		if (!filterKey) {
			return;
		}

		const nextParams = new URLSearchParams(searchParams);

		if (checked) {
			nextParams.set(filterKey, optionName);
		} else {
			nextParams.delete(filterKey);
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
			if (numValue < 5000) {
				nextParams.set('maxPrice', String(numValue));
			} else {
				nextParams.delete('maxPrice');
			}
		}

		setSearchParams(nextParams);
	};

	const handleNextPage = () => setPage(page + 1);
	const handlePrevPage = () => setPage(page > 1 ? page - 1 : 1);

	// Sort products based on currentSort
	const min = minPrice || 0;
	const max = maxPrice || 10000;
	const filteredByPrice = products.filter(product => product.price >= min && product.price <= max);

	const sortedProducts = [...filteredByPrice].sort((a, b) => {
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
		return <div style={{ padding: '20px' }}>Loading products...</div>;
	}

	return (
		<>
			<Header />
			<Navbar />

			<main className='view-products-page'>
				<aside className='filters-panel'>
					<section className='filter-block'>
						<div className='filter-name-row'>
							<h3>Price</h3>
						</div>
						<div style={{ padding: '10px 0' }}>
							<div style={{ marginBottom: '8px' }}>
								<input
									type='range'
									min='0'
									max='5000'
									step='10'
									value={minPrice}
									onChange={(e) => handlePriceChange('min', e.target.value)}
									style={{ width: '100%' }}
								/>
								<div style={{ fontSize: '0.85rem', marginTop: '4px' }}>Min: ${minPrice}</div>
							</div>
							<div>
								<input
									type='range'
									min='0'
									max='5000'
									step='10'
									value={maxPrice}
									onChange={(e) => handlePriceChange('max', e.target.value)}
									style={{ width: '100%' }}
								/>
								<div style={{ fontSize: '0.85rem', marginTop: '4px' }}>Max: ${maxPrice}</div>
							</div>
						</div>
					</section>

					{filterSections.map((section) => (
						<section key={section.name} className='filter-block'>
							<div className='filter-name-row'>
								<h3>{section.name}</h3>
							</div>

							<ul>
								{section.options.map((option) => (
									<li key={option.name}>
										<div className='filter-option-row'>
											<input
												type='checkbox'
												checked={searchParams.get(filterQueryKeys[section.name]) === option.name}
												onChange={(event) => handleFilterChange(section.name, option.name, event.target.checked)}
											/>
											<span>{option.name}</span>
										</div>
									</li>
								))}
							</ul>
						</section>
					))}
				</aside>

				<section className='results-panel'>
					<header className='results-header'>
						<h1>{categoryName}</h1>
						<p>
							Browse our collection of {categoryName.toLowerCase()} components. Find the perfect hardware for your needs.
						</p>
					</header>

					<div className='brand-row'>
						<h2>Brand</h2>
						<div className='brand-chips'>
							{brands.map((brand) => (
								<button key={brand} type='button' onClick={() => handleFilterChange('Brand', brand, true)} className='chip chip-link'>
									{brand}
								</button>
							))}
						</div>
					</div>

					<div className='toolbar'>
				<p>{sortedProducts.length} results</p>
						<div className='toolbar-actions'>
							<label htmlFor='sort'>Sortering</label>
							<select id='sort' value={currentSort} onChange={(event) => handleSortChange(event.target.value)}>
								<option value='A-Z'>A-Z</option>
								<option value='price-low-high'>Price low-high</option>
								<option value='price-high-low'>Price high-low</option>
							</select>
							<button type='button' className='layout-btn' aria-label='Wijzig weergave'>
								☰
							</button>
						</div>
					</div>

					<div className='product-grid'>
					{sortedProducts.map((product) => (
						<article key={product.id} className='product-card'>
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
                                <Link to="/cart" type='button' className='add-to-cart-btn' >
									Add to cart
								</Link>

							</article>
						))}
					</div>

					<div className='pagination'>
						<button onClick={handlePrevPage} disabled={page === 1}>Previous</button>
						<span>Page {page}</span>
						<button onClick={handleNextPage}>Next</button>
					</div>
				</section>
			</main>
		</>
	);
}
