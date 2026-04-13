import React from 'react';
import Slideshow from '../Components/Body-Components/Slideshow';
import Header from '../Components/Header-Component/Header';
import Navbar from '../Components/Header-Component/Navbar';
import hero from '../assets/hero.png';
import './Products.css';

interface ProductItem {
    id: number;
    category: string;
    name: string;
    manufacturer: string;
    description: string;
    price: number;
}

export default function Products() {
    const item: ProductItem = {
        id: 1,
        category: 'Electronics',
        name: 'MSI MPG 341CQR QD-OLED X36 34" 360Hz Wide Quad HD Gaming Monitor',
        manufacturer: 'MSI',
        description: 'The MSI MPG 341CQR offers an ultrawide 34-inch QD-OLED panel with a sharp 3440 x 1440 resolution and smooth 360Hz refresh rate. This monitor combines fast response times with rich HDR performance and an immersive curved design.',
        price: 1299
    };

    const specs = [
        { label: 'Brand', value: item.manufacturer },
        { label: 'Resolution Class', value: 'WQHD' },
        { label: 'Screen Resolution', value: '3440 x 1440 pixels' },
        { label: 'Screen Size', value: '34.0 inch' },
        { label: 'Refresh Rate', value: '360 Hz' },
        { label: 'Aspect Ratio', value: '21:9' },
        { label: 'Panel Type', value: 'QD-OLED' },
        { label: 'HDR Type', value: 'HDR True Black 500' },
        { label: 'Response Time', value: '0.03 ms' },
        { label: 'Curved', value: 'Yes' }
    ];

    const productImages = [
        { src: hero, alt: `${item.name} view 1` },
        { src: hero, alt: `${item.name} view 2` },
        { src: hero, alt: `${item.name} view 3` },
        { src: hero, alt: `${item.name} view 4` }
    ];

    return (
        <>
            <Header />
            <Navbar />
            <main className="products-page">
                <h1 className="products-title">{item.name}</h1>
                <section className="product-layout">
                    <div className="product-media">
                        <Slideshow compact imageSlides={productImages} />
                    </div>

                    <div className="product-info">
                        <p className="product-description">{item.description}</p>

                        <h2 className="spec-title">Key Specifications</h2>
                        <table className="spec-table">
                            <tbody>
                                {specs.map((spec) => (
                                    <tr key={spec.label}>
                                        <th scope="row">{spec.label}</th>
                                        <td>{spec.value}</td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>

                    <aside className="purchase-box">
                        <p className="purchase-price">${item.price.toFixed(0)},-</p>
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