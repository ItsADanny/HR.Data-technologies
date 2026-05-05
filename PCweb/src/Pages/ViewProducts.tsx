import React from 'react';
import { Link, useSearchParams } from 'react-router-dom';
import Header from '../Components/Header-Component/Header';
import Navbar from '../Components/Header-Component/Navbar';
import hero from '../assets/hero.png';
import './ViewProducts.css';

interface FilterOption {
	name: string;
}

interface FilterSection {
	name: string;
	options: FilterOption[];
}

interface ProductCard {
	id: number;
	brand: string;
	name: string;
	subname: string;
	price: string;
	oldPrice?: string;
	discount?: string;
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

const productCards: ProductCard[] = [
	{
		id: 1,
		brand: 'JSKOL',
		name: 'Action Camera HD - Pocket Vintage Camera - Gratis SD kaart',
		subname: 'HD Ready video • WiFi • 270° draaibare lens',
		price: '99,99',
		image: hero,
	},
    {
        id: 2,
        brand: 'GoPro',
        name: 'GoPro HERO11 Black',
        subname: '5.3K video • HyperSmooth 5.0 • 27MP foto’s',
        price: '499,99',
        oldPrice: '549,99',
        discount: '9%',
        image: hero,
    },
    {
        id: 3,
        brand: 'Strex',
        name: 'Strex Action Camera 5K 50MP - Inclusief Accessoires',
        subname: '4K video • 50 Megapixel • 170° beeldhoek',
        price: '72,15',
        oldPrice: '79,95',
        discount: '10%',
        image: hero,
    },
    {
        id: 4,
        brand: 'DJI',
        name: 'DJI Osmo Action 3',
        subname: '4K video • RockSteady 3.0 • 16MP foto’s',
        price: '329,99',
        oldPrice: '379,99',
        discount: '13%',
        image: hero,
    },
    {
        id: 5,
        brand: 'GoPro',
        name: 'GoPro HERO11 Black',
        subname: '5.3K video • HyperSmooth 5.0 • 27MP foto’s',
        price: '499,99',
        oldPrice: '549,99',
        discount: '9%',
        image: hero,
    },
    {
        id: 6,
        brand: 'Insta360',
        name: 'Insta360 ONE X2',
        subname: '4K video • 20MP foto’s • 360° beeld',
        price: '499,99',
        oldPrice: '549,99',
        discount: '9%',
        image: hero,
    }
];



const productsPath = '/viewproducts';

const filterQueryKeys: Record<string, string> = {
	Brand: 'brand',
	Resistentie: 'resistance',
	Serie: 'series',
	Videoresolutie: 'resolution',
};

export default function ViewProducts() {
	const [searchParams, setSearchParams] = useSearchParams();
	const currentSort = searchParams.get('sort') ?? 'popular';

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

	return (
		<>
			<Header />
			<Navbar />

			<main className='view-products-page'>
				<aside className='filters-panel'>
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
						<h1>Action camera&apos;s</h1>
						<p>
							Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla sit amet ex eget lectus laoreet mollis. 
                            Cras molestie tincidunt nibh, ullamcorper interdum diam molestie quis. 
                            Integer et urna nisl. Curabitur bibendum luctus ligula eu rhoncus.
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
						<p>680 results</p>
						<div className='toolbar-actions'>
							<label htmlFor='sort'>Sortering</label>
							<select id='sort' value={currentSort} onChange={(event) => handleSortChange(event.target.value)}>
								<option value='popular'>Popular</option>
								<option value='price-low-high'>Price low-high</option>
								<option value='price-high-low'>Price high-low</option>
								<option value='best-reviewed'>Best reviewed</option>
							</select>
							<button type='button' className='layout-btn' aria-label='Wijzig weergave'>
								☰
							</button>
						</div>
					</div>

					<div className='product-grid'>
						{productCards.map((product) => (
							<article key={product.id} className='product-card'>
                                <Link to="/products">
                                	<img src={product.image} alt={product.name} />
								</Link>

								<p className='brand'>{product.brand}</p>
								<h3>{product.name}</h3>
								<p className='subname'>{product.subname}</p>


								<div className='price-row'>
									<p className='price'>{product.price}</p>
									{product.discount && <span className='discount'>discount {product.discount}</span>}
								</div>

								{product.oldPrice && <p className='old-price'>Most {product.oldPrice}</p>}
								<p className='delivery'>Order by 16:00, delivered tomorrow</p>
                                <Link to="/cart" type='button' className='add-to-cart-btn' >
									Add to cart
								</Link>

							</article>
						))}
					</div>
				</section>
			</main>
		</>
	);
}
