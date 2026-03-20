import React, { useState } from 'react';

export default function Navbar() {
    const [open, setOpen] = useState(false);

    const closeDropdown = () => {
        setOpen(false);
    };

    return (
        <header className='navbar-shell'>
            <div className='navbar-container'>
                <div
                    className={`category-dropdown ${open ? 'open' : ''}`}
                    onMouseEnter={() => setOpen(true)}
                    onMouseLeave={closeDropdown}
                >
                    <button className='category-trigger'>
                        Category
                    </button>

                    <ul className='category-menu'>
                        <li><a href='/'>PC Components</a></li>
                        <li><a href='/'>Gaming PCs</a></li>
                        <li><a href='/'>Laptops</a></li>
                        <li><a href='/'>Monitors</a></li>
                        <li><a href='/'>Storage</a></li>
                        <li><a href='/'>Accessories</a></li>
                    </ul>
                </div>
                <a href="/">Sales</a>
                <a href="/">Contact</a>
            </div>
        </header>
    );
}