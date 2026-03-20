import React, { useState } from 'react';
import cartIcon from '../../assets/cart-icon-white.png';

export default function Header() {
    const [search, setSearch] = useState('');

    return (
        <header>
            <div className='header-container'>
                <h1 className="logo">My PC Builder</h1>
                <div className="search-container">
                    <input className='searchfield'
                        type="text"
                        placeholder="Search..."
                        value={search}
                        onChange={(e) => setSearch(e.target.value)}
                    />
                </div>
                <div className='user-container'>
                    <a className='login-link' href="...">Login</a>
                    <img className='cart-icon' src={cartIcon} alt="Cart" />
                </div>
            </div>
        </header>
    );
}