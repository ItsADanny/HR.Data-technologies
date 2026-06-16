import React, { useState, useEffect } from 'react';
import Header from '../Components/Header-Component/Header';
import Navbar from '../Components/Header-Component/Navbar';
import { useCartContext } from '../context/CartContext';
import { addressService, Address } from '../hooks/addresshooks';
import './Cart.css';
import hero from '../assets/hero.png';

// Login is nog niet geïmplementeerd, dus er staat geen userId in localStorage. Hardcoded fallback (bestaande user in BuildHub) totdat login werkt.
const FALLBACK_USER_ID = 4;

export default function Cart() {
    const { items, removeItem, updateQuantity, getTotalPrice } = useCartContext();

    const [savedAddresses, setSavedAddresses] = useState<Address[]>([]);
    const [selectedAddressId, setSelectedAddressId] = useState<string>('');
    const [savedBillingAddresses, setSavedBillingAddresses] = useState<Address[]>([]);
    const [selectedBillingAddressId, setSelectedBillingAddressId] = useState<string>('');
    const [showCreateNew, setShowCreateNew] = useState(false);
    const [showCreateNewBilling, setShowCreateNewBilling] = useState(false);
    const [newAddress, setNewAddress] = useState({
        street: '',
        houseNumber: '',
        houseNumberAddition: '',
        city: '',
        postcode: '',
        country: ''
    });
    const [billingAddress, setBillingAddress] = useState({
        street: '',
        houseNumber: '',
        houseNumberAddition: '',
        city: '',
        postcode: '',
        country: ''
    });
    const [loading, setLoading] = useState(true);
    const userID = parseInt(localStorage.getItem('userId') ?? '') || FALLBACK_USER_ID;

    useEffect(() => {
        loadUserAddresses();
    }, []);

    useEffect(() => {
        loadBillingAddresses();
    }, []);

    const loadUserAddresses = async () => {
        const userId = parseInt(localStorage.getItem('userId') ?? '') || FALLBACK_USER_ID;
        const addresses = await addressService.getByUserId(userId);
        setSavedAddresses(addresses);
        setLoading(false);
    };

    const loadBillingAddresses = async () => {
        const userId = parseInt(localStorage.getItem('userId') ?? '') || FALLBACK_USER_ID;
        const addresses = await addressService.getBillingAddressByUserId(userId);
        setSavedBillingAddresses(addresses);
        setLoading(false);
    };

    const handleAddressChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setNewAddress(prev => ({ ...prev, [name]: value }));
    };

    const handleBillingAddressChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setBillingAddress(prev => ({ ...prev, [name]: value }));
    };

    const handleSaveAddress = async () => {
        const { street, city, country, postcode, houseNumber, houseNumberAddition } = newAddress;
        if (!street || !city || !country || !postcode) {
            alert('Vul alle verplichte velden in (straat, stad, postcode, land)');
            return;
        }

        const userId = parseInt(localStorage.getItem('userId') ?? '') || FALLBACK_USER_ID;

        try {
            await addressService.create({
                addressId: 0,
                street,
                city,
                country,
                postcode,
                houseNumber: parseInt(houseNumber) || 0,
                houseNumberAddition,
                userId,
            });
            alert('Adres opgeslagen!');
            setNewAddress({ street: '', houseNumber: '', houseNumberAddition: '', city: '', postcode: '', country: '' });
            setShowCreateNew(false);
            await loadUserAddresses(); // refresh lijst
        } catch (error) {
            alert('Opslaan mislukt. Probeer opnieuw.');
        }
    };

    const handleSaveBillingAddress = async () => {
        const { street, city, country, postcode, houseNumber, houseNumberAddition } = billingAddress;
        if (!street || !city || !country || !postcode) {
            alert('Vul alle verplichte velden in (straat, stad, postcode, land)');
            return;
        }

        const userId = parseInt(localStorage.getItem('userId') ?? '') || FALLBACK_USER_ID;

        try {
            await addressService.create({
                addressId: 0,
                street,
                city,
                country,
                postcode,
                houseNumber: parseInt(houseNumber) || 0,
                houseNumberAddition,
                userId,
            });
            alert('Adres opgeslagen!');
            setBillingAddress({ street: '', houseNumber: '', houseNumberAddition: '', city: '', postcode: '', country: '' });
            setShowCreateNewBilling(false);
            await loadBillingAddresses(); // refresh lijst
        } catch (error) {
            alert('Opslaan mislukt. Probeer opnieuw.');
        }
    };

    const handleCheckout = async () => {

        const cartItems = JSON.parse(localStorage.getItem('pcweb_cart') || '[]');;

        if (showCreateNew || savedAddresses.length === 0) {
            alert('Sla eerst een adres op voor je afrekent.');
            return;
        }
        if (!selectedAddressId) {
            alert('Selecteer een bezorgadres.');
            return;
        }
        if (!selectedBillingAddressId) {
            alert('Selecteer een factuuradres.');
            return;
        }
        if (!cartItems.length) {
            alert('Je winkelwagen is leeg.');
            return;
        }
        try {
            const response = await fetch("http://localhost:5221/api/order/create", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ 
                    userId: userID, 
                    shippingAddressId: parseInt(selectedAddressId), 
                    billingAddressId: parseInt(selectedBillingAddressId), 
                    cartItems 
                }),
            });

            let data;
            try {
                data = await response.json();
            } catch {
                const text = await response.text();
                throw new Error(text || "Invalid server response");
            }

            if (!response.ok) {
                throw new Error(data.message || "Order failed");
            }

            alert('Checkout geslaagd!');
        } catch (error) {
            console.error("Error confirming order:", error);
            alert('Er is iets misgegaan bij het plaatsen van de bestelling.');
        }
    };


    return (
        <>
            <Header />
            <Navbar />
            <div className='cart-container'>
                <div className='cart-components-grid'>
                    {items.length === 0 ? (
                        <p>Je winkelwagen is leeg.</p>
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
                                            <p className='cart-item-price'>Prijs: ${item.price.toFixed(2)}</p>
                                            <div className='quantity-control'>
                                                <select
                                                    value={item.quantity}
                                                    onChange={(e) => updateQuantity(item.id, parseInt(e.target.value))}
                                                >
                                                    {[...Array(10)].map((_, i) => (
                                                        <option key={i + 1} value={i + 1}>{i + 1}</option>
                                                    ))}
                                                </select>
                                            </div>
                                            <button className='remove-btn' onClick={() => removeItem(item.id)}>
                                                Verwijderen
                                            </button>
                                        </div>
                                    </div>
                                ))}
                            </div>

                            <div className='divider'>
                                <div className='billing-section'>
                                    <h2>Bezorgadres</h2>

                                    {loading ? (
                                        <p>Adressen laden...</p>
                                    ) : !showCreateNew && savedAddresses.length > 0 ? (
                                        // Toon opgeslagen adressen
                                        <>
                                            {savedAddresses.map((addr, index) => (
                                                <label key={index} className='address-option'>
                                                    <input
                                                        type="radio"
                                                        name="address"
                                                        value={String(addr.addressId)}
                                                        checked={selectedAddressId === String(addr.addressId)}
                                                        onChange={(e) => {
                                                            setSelectedAddressId(e.target.value);
                                                        }}
                                                    />
                                                    {addr.street} {addr.houseNumber}{addr.houseNumberAddition}, {addr.postcode} {addr.city}, {addr.country}
                                                </label>
                                            ))}
                                            <button className='back-btn' onClick={() => setShowCreateNew(true)}>
                                                + Nieuw adres toevoegen
                                            </button>
                                        </>
                                    ) : (
                                        // Nieuw adres formulier
                                        <div className='address-form'>
                                            {savedAddresses.length > 0 && (
                                                <button className='back-btn' onClick={() => setShowCreateNew(false)}>
                                                    ← Terug
                                                </button>
                                            )}
                                            <input type="text" name="street" placeholder="Straat *" value={newAddress.street} onChange={handleAddressChange} />
                                            <input type="text" name="houseNumber" placeholder="Huisnummer" value={newAddress.houseNumber} onChange={handleAddressChange} />
                                            <input type="text" name="houseNumberAddition" placeholder="Toevoeging" value={newAddress.houseNumberAddition} onChange={handleAddressChange} />
                                            <input type="text" name="city" placeholder="Stad *" value={newAddress.city} onChange={handleAddressChange} />
                                            <input type="text" name="postcode" placeholder="Postcode *" value={newAddress.postcode} onChange={handleAddressChange} />
                                            <input type="text" name="country" placeholder="Land *" value={newAddress.country} onChange={handleAddressChange} />
                                            <button className='checkout-btn' onClick={handleSaveAddress}>
                                                Adres opslaan
                                            </button>
                                        </div>
                                    )}
                                </div>
                                <div className='billing-section'>
                                    <h2>FactuurAdres</h2>

                                    {loading ? (
                                        <p>Adressen laden...</p>
                                    ) : !showCreateNewBilling && savedBillingAddresses.length > 0 ? (
                                        // Toon opgeslagen adressen
                                        <>
                                            {savedBillingAddresses.map((addr, index) => (
                                                <label key={index} className='address-option'>
                                                    <input
                                                        type="radio"
                                                        name="billingaddress"
                                                        value={String(addr.addressId)}
                                                        checked={selectedBillingAddressId === String(addr.addressId)}
                                                        onChange={(e) => {
                                                            setSelectedBillingAddressId(e.target.value);
                                                        }}
                                                    />
                                                    {addr.street} {addr.houseNumber}{addr.houseNumberAddition}, {addr.postcode} {addr.city}, {addr.country}
                                                </label>
                                            ))}
                                            <button className='back-btn' onClick={() => setShowCreateNewBilling(true)}>
                                                + Nieuw adres toevoegen
                                            </button>
                                        </>
                                    ) : (
                                        // Nieuw adres formulier
                                        <div className='address-form'>
                                            {savedBillingAddresses.length > 0 && (
                                                <button className='back-btn' onClick={() => setShowCreateNewBilling(false)}>
                                                    ← Terug
                                                </button>
                                            )}
                                            <input type="text" name="street" placeholder="Straat *" value={billingAddress.street} onChange={handleBillingAddressChange} />
                                            <input type="text" name="houseNumber" placeholder="Huisnummer" value={billingAddress.houseNumber} onChange={handleBillingAddressChange} />
                                            <input type="text" name="houseNumberAddition" placeholder="Toevoeging" value={billingAddress.houseNumberAddition} onChange={handleBillingAddressChange} />
                                            <input type="text" name="city" placeholder="Stad *" value={billingAddress.city} onChange={handleBillingAddressChange} />
                                            <input type="text" name="postcode" placeholder="Postcode *" value={billingAddress.postcode} onChange={handleBillingAddressChange} />
                                            <input type="text" name="country" placeholder="Land *" value={billingAddress.country} onChange={handleBillingAddressChange} />
                                            <button className='checkout-btn' onClick={handleSaveBillingAddress}>
                                                Adres opslaan
                                            </button>
                                        </div>
                                    )}
                                </div>
                                <div className='cart-total'>
                                    <h1>Totaalprijs</h1>
                                    <h3>Totaal: ${getTotalPrice().toFixed(2)}</h3>
                                    <button className='checkout-btn' onClick={handleCheckout}>Afrekenen</button>
                                </div>
                            </div>
                        </>
                    )}
                </div>
            </div>
        </>
    );
}