import React, { useState } from 'react';
import Header from '../Components/Header-Component/Header';
import Navbar from '../Components/Header-Component/Navbar';
import './Cart.css';
import hero from '../assets/hero.png';

interface CartItem {
    id: number;
    name: string;
    price: number;
    quantity: number;
}

export default function Cart() {
    const [items, setItems] = useState<CartItem[]>([
        { id: 1, name: 'Example Item', price: 19.99, quantity: 2 },
        { id: 2, name: 'Another Item', price: 9.99, quantity: 1 },
        { id: 3, name: 'Yet Another Item', price: 29.99, quantity: 3 },
        { id: 4, name: 'Sample Item', price: 14.99, quantity: 1 },
        { id: 5, name: 'Test Item', price: 24.99, quantity: 4 }
    ]);

    const addItem = (item: CartItem) => {
        const existingItem = items.find(i => i.id === item.id);
        if (existingItem) {
            setItems(items.map(i =>
                i.id === item.id ? { ...i, quantity: i.quantity + item.quantity } : i
            ));
        } else {
            setItems([...items, item]);
        }
    };

    const removeItem = (itemId: number) => {
        setItems(items.filter(item => item.id !== itemId));
    };

    const updateQuantity = (itemId: number, quantity: number) => {
        if (quantity <= 0) {
            removeItem(itemId);
        } else {
            setItems(items.map(item =>
                item.id === itemId ? { ...item, quantity } : item
            ));
        }
    };

    const totalPrice = items.reduce((sum, item) => sum + (item.price * item.quantity), 0);

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

                                <h3>Total: ${totalPrice.toFixed(2)}</h3>
                                <button className='checkout-btn'>Checkout</button>
                            </div>
                        </>
                    )}
                </div>
            </div>
        </>
    );
}