import React, { useState, useEffect } from 'react';
import Header from '../Components/Header-Component/Header';
import Navbar from '../Components/Header-Component/Navbar';
import { useCartContext } from '../context/CartContext';
import { addressService, Address } from '../services/addressService';
import './Cart.css';
import hero from '../assets/hero.png';

// Login is nog niet geïmplementeerd, dus er staat geen userId in localStorage. Hardcoded fallback (bestaande user in BuildHub) totdat login werkt.
const FALLBACK_USER_ID = 2;

export default function Cart() {
    const { items, removeItem, updateQuantity, getTotalPrice } = useCartContext();

    const [savedAddresses, setSavedAddresses] = useState<Address[]>([]);
    const [selectedAddressId, setSelectedAddressId] = useState<string>('');
    const [savedBillingAddresses, setSavedBillingAddresses] = useState<Address[]>([]);
    const [selectedBillingAddressId, setSelectedBillingAddressId] = useState<string>('');
    const [showCreateNew, setShowCreateNew] = useState(false);
    const [showCreateNewBilling, setShowCreateNewBilling] = useState(false);
    const [personalDetails, setPersonalDetails] = useState({
        firstName: '',
        lastName: '',
        email: '',
        phone: ''
    })
    const [editingPersonalDetails, setEditingPersonalDetails] = useState(true);
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

    useEffect(() => {
        loadPersonalDetails();
    }, []);

    useEffect(() => {
        loadUserAddresses();
    }, []);

    useEffect(() => {
        loadBillingAddresses();
    }, []);

    const loadPersonalDetails = async () => {
        const userId = parseInt(localStorage.getItem('userId') ?? '') || FALLBACK_USER_ID;
        //const userInfo = await addressService.getByUserId(userId);
    };

    const loadUserAddresses = async () => {
        const userId = parseInt(localStorage.getItem('userId') ?? '') || FALLBACK_USER_ID;
        const addresses = await addressService.getByUserId(userId);
        setSavedAddresses(addresses);
        setLoading(false);
    };

    const loadBillingAddresses = async () => {
        const userId = parseInt(localStorage.getItem('userId') ?? '') || FALLBACK_USER_ID;
        const addresses = await addressService.getByUserId(userId);
        setSavedBillingAddresses(addresses);
        setLoading(false);
    };

    const handlePersonalDetailsChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setNewAddress(prev => ({ ...prev, [name]: value }));
    };

    const handleAddressChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setNewAddress(prev => ({ ...prev, [name]: value }));
    };

    const handleBillingAddressChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setBillingAddress(prev => ({ ...prev, [name]: value }));
    };

    const handleSavePersonalDetails = async () => {
        const { firstName, lastName, phone, email } = personalDetails;
        if (!firstName || !lastName || !phone || !email) {
            alert('Vul alle verplichte velden in (straat, stad, postcode, land)');
            return;
        }
        else {
            setEditingPersonalDetails(false);
        }
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
        if (showCreateNew || savedAddresses.length === 0) {
            alert('Sla eerst een adres op voor je afrekent.');
            return;
        }
        if (selectedAddressId === '') {
            alert('Selecteer een adres.');
            return;
        }
        alert('Checkout geslaagd!');
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
                                <div className="personal-details">
                                        <input type="text" name="firstName" placeholder="Voornaam *" value={personalDetails.firstName} onChange={handlePersonalDetailsChange} />
                                        <input type="text" name="lastName" placeholder="Achternaam *" value={personalDetails.lastName} onChange={handlePersonalDetailsChange} />
                                        <input type="text" name="email" placeholder="E-mailadres *" value={personalDetails.email} onChange={handlePersonalDetailsChange} />
                                        <input type="text" name="phone" placeholder="Telefoonnummer *" value={personalDetails.phone} onChange={handlePersonalDetailsChange} />
                                        <button className='checkout-btn' onClick={handleSaveAddress}>
                                            Persoonlijke gegevens opslaan
                                        </button>
                                </div>
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
                                                        value={String(index)}
                                                        checked={selectedAddressId === String(index)}
                                                        onChange={(e) => setSelectedAddressId(e.target.value)}
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
                                                        value={String(index)}
                                                        checked={selectedBillingAddressId === String(index)}
                                                        onChange={(e) => setSelectedBillingAddressId(e.target.value)}
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