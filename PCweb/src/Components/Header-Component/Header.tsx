import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import cartIcon from '../../assets/cart-icon-white.png';

export default function Header() {
    const [search, setSearch] = useState('');
    const navigate = useNavigate();

    const handleSearchSubmit = (e: React.SyntheticEvent<HTMLFormElement, SubmitEvent>) => {
        e.preventDefault();

        const trimmedSearch = search.trim();
        if (!trimmedSearch) return;

        navigate(`/viewproducts?search=${encodeURIComponent(trimmedSearch)}`);
    };

    return (
        <header>
            <div className='header-container'>
                <Link className="logo" to="/">My PC Builder</Link>
                <form className="search-container" onSubmit={handleSearchSubmit}>
                    <input className='searchfield'
                        type="text"
                        placeholder="Search..."
                        value={search}
                        onChange={(e) => setSearch(e.target.value)}
                    />
                </form>
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