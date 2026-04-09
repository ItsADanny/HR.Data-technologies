import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import cartIcon from '../../assets/cart-icon-white.png';

export default function Header() {
    const [search, setSearch] = useState('');

    return (
        <header>
            <div className='header-container'>
                <Link className="logo" to="/">My PC Builder</Link>
                <div className="search-container">
                    <input className='searchfield'
                        type="text"
                        placeholder="Search..."
                        value={search}
                        onChange={(e) => setSearch(e.target.value)}
                    />
                </div>
                <div className='user-container'>
                    <Link className='login-link' to="/login">Login</Link>
                    <Link className='cart-link' to="/cart">
                        <img className='cart-icon' src={cartIcon} alt="Cart" />
                    </Link>
                </div>
            </div>
        </header>
    );
}