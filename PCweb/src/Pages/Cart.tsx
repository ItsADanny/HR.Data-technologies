import React from 'react';
import Header from '../Components/Header-Component/Header';
import Navbar from '../Components/Header-Component/Navbar';
import { useCartContext } from '../context/CartContext';
import './Cart.css';
import hero from '../assets/hero.png';

export default function Cart() {
    const { items, removeItem, updateQuantity, getTotalPrice } = useCartContext();

    return (
        <>
            <Header />
            <Navbar />
            <div className='cart-container'>
                <div className='cart-components-grid'>
                    {items.length === 0 ? (
                        <p>Your cart is empty.</p>
                    ) : (
                        <>
                            <div className='cart-items'>
                                {items.map(item => (
                                    <div key={item.id} className='cart-item'>
                                        <div className='cart-item-image-container'>
                                            <img src={hero} alt={item.name} className='cart-item-image' />
                                        </div>
                                        <div className='cart-item-details'>
                                            <p className='cart-item-name'><strong>{item.name}</strong></p>
                                            <p className='cart-item-price'>Price: ${item.price.toFixed(2)}</p>
                                            <div className='quantity-control'>
                                                <select value={item.quantity} onChange={(e) => updateQuantity(item.id, parseInt(e.target.value))}>
                                                    {[...Array(10)].map((_, i) => (
                                                        <option key={i + 1} value={i + 1}>
                                                            {i + 1}
                                                        </option>
                                                    ))}
                                                </select>
                                            </div>
                                            <button className='remove-btn' onClick={() => removeItem(item.id)}>
                                                Remove
                                            </button>
                                        </div>
                                    </div>
                                ))}
                            </div>
                            <div className='cart-total'>
                                <h1>Total Price</h1>

                                <h3>Total: ${getTotalPrice().toFixed(2)}</h3>
                                <button className='checkout-btn'>Checkout</button>
                            </div>
                        </>
                    )}
                </div>
            </div>
        </>
    );
}