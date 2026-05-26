import React, { useState, useEffect } from 'react';
import Slideshow from '../Components/Body-Components/Slideshow';
import Header from '../Components/Header-Component/Header';
import Navbar from '../Components/Header-Component/Navbar';
import hero from '../assets/hero.png';
import './Products.css';
import { useParams, useLocation } from 'react-router-dom';

interface ProductItem {
	id: number;
	categoryName: string;
	name: string;
	price: number;
	fields: Record<string, string>;
}

export default function Products() {
	const { productId } = useParams<{ productId: string }>();
	const location = useLocation();
	const [product, setProduct] = useState<ProductItem | null>(location.state?.product || null);
	const [loading, setLoading] = useState(!product && !productId);
	const [error, setError] = useState<string | null>(null);

	useEffect(() => {
		// If product not in state, fetch it from API
		if (!product && productId) {
			const fetchProduct = async () => {
				try {
					setLoading(true);
					// Note: You may need to create an API endpoint to fetch a single product
					// For now, this is a placeholder - adjust based on your API
					const response = await fetch(`/api/product/${productId}`);
					if (!response.ok) {
						throw new Error('Product not found');
					}
					const data = await response.json();
					setProduct(data);
					setError(null);
				} catch (err) {
					console.error('Error fetching product:', err);
					setError(err instanceof Error ? err.message : 'Failed to fetch product');
				} finally {
					setLoading(false);
				}
			};

			fetchProduct();
		} else {
			setLoading(false);
		}
	}, [productId, product]);

	if (loading) {
		return (
			<>
				<Header />
				<Navbar />
				<main className="products-page">
					<p>Loading product...</p>
				</main>
			</>
		);
	}

	if (!product || error) {
		return (
			<>
				<Header />
				<Navbar />
				<main className="products-page">
					<p>{error || 'Product not found'}</p>
				</main>
			</>
		);
	}

	const productImages = [
		{ src: hero, alt: `${product.name} view 1` },
		{ src: hero, alt: `${product.name} view 2` },
		{ src: hero, alt: `${product.name} view 3` },
		{ src: hero, alt: `${product.name} view 4` }
	];

	return (
		<>
			<Header />
			<Navbar />
			<main className="products-page">
				<h1 className="products-title">{product.name}</h1>
				<section className="product-layout">
					<div className="product-media">
						<Slideshow compact imageSlides={productImages} />
					</div>

					<div className="product-info">
						<p className="product-description">{product.fields.description || 'No description available'}</p>

						<h2 className="spec-title">Key Specifications</h2>
						<table className="spec-table">
							<tbody>
								{Object.entries(product.fields).map(([key, value]) => (
									<tr key={key}>
										<th scope="row">{key}</th>
										<td>{value}</td>
									</tr>
								))}
							</tbody>
						</table>
					</div>

					<aside className="purchase-box">
						<p className="purchase-price">${product.price.toFixed(0)},-</p>
						<button className="add-to-cart-btn">Add to Cart</button>
						<ul className="purchase-notes">
							<li>In stock</li>
							<li>Order today, shipped tomorrow</li>
							<li>30-day return policy</li>
						</ul>
					</aside>
				</section>
			</main>
		</>
	);
}