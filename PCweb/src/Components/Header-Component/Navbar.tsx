import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import './Navbar.css';

export default function Navbar() {
    const [open, setOpen] = useState(false);

    return (
        <nav className='navbar-container' onMouseLeave={() => setOpen(false)}>
            <div
                className={`category-dropdown ${open ? 'open' : ''}`}
                onMouseEnter={() => setOpen(true)}
            >
                <button className='category-trigger'>
                    Category ▾
                </button>

                {open && (
                    <div className='megamenu'>
                        <div className='megamenu-section'>
                            <h3 className='megamenu-heading'>PC Components</h3>
                            <ul className='megamenu-list'>
                                <li><Link to='/viewproducts?categoryId=4' onClick={() => setOpen(false)}>CPUs</Link></li>
                                <li><Link to='/viewproducts?categoryId=15' onClick={() => setOpen(false)}>GPUs</Link></li>
                                <li><Link to='/viewproducts?categoryId=8' onClick={() => setOpen(false)}>RAM / Memory</Link></li>
                                <li><Link to='/viewproducts?categoryId=9' onClick={() => setOpen(false)}>Motherboards</Link></li>
                                <li><Link to='/viewproducts?categoryId=12' onClick={() => setOpen(false)}>Power Supplies</Link></li>
                                <li><Link to='/viewproducts?categoryId=7' onClick={() => setOpen(false)}>Storage</Link></li>
                                <li><Link to='/viewproducts?categoryId=5' onClick={() => setOpen(false)}>Cooling</Link></li>
                                <li><Link to='/viewproducts?categoryId=1' onClick={() => setOpen(false)}>Cases</Link></li>
                            </ul>
                        </div>

                        <div className='megamenu-divider' />

                        <div className='megamenu-section'>
                            <h3 className='megamenu-heading'>Browse</h3>
                            <ul className='megamenu-list'>
                                <li><Link to='/viewproducts' onClick={() => setOpen(false)}>All Products</Link></li>
                                <li><Link to='/partpicker' onClick={() => setOpen(false)}>PC Builder</Link></li>
                                <li><Link to='/cart' onClick={() => setOpen(false)}>Cart</Link></li>
                            </ul>
                        </div>
                    </div>
                )}
            </div>

            <Link to='/viewproducts' className='navbar-link'>Shop</Link>
            <Link to='/contact' className='navbar-link'>Contact</Link>
            <Link className='navbar-partpicker-btn' to='/partpicker'>⚙ Build Your PC</Link>
        </nav>
    );
}
