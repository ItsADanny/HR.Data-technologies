import { useNavigate } from 'react-router-dom';
import { useEffect, useState } from 'react';
import Header from '../Components/Header-Component/Header';
import Footer from '../Components/Footer-Component/Footer';
import './PartPicker.css';

interface SelectedPart {
    id: number;
    name: string;
    price: number;
    stock: number;
}

interface CompatibilityWarning {
    checkType: string;
    component1: string;
    component2: string;
    warnings: string[];
    isCompatible: boolean;
}

export default function PartPicker() {
    const navigate = useNavigate();
    const [selectedParts, setSelectedParts] = useState<Record<string, SelectedPart | null>>({});
    const [compatibilityWarnings, setCompatibilityWarnings] = useState<CompatibilityWarning[]>([]);
    const [isCheckingCompatibility, setIsCheckingCompatibility] = useState(false);

    const components = [
        { name: 'CPU', label: 'Choose a CPU', categoryId: 4 },
        { name: 'CPU Cooler', label: 'Choose a CPU Cooler', categoryId: 5 },
        { name: 'Motherboard', label: 'Choose a Motherboard', categoryId: 9 },
        { name: 'Memory', label: 'Choose Memory', categoryId: 8 },
        { name: 'Storage', label: 'Choose Storage', categoryId: 7 },
        { name: 'GPU', label: 'Choose a GPU', categoryId: 15 },
        { name: 'Case', label: 'Choose a Case', categoryId: 1 },
        { name: 'Power Supply', label: 'Choose a Power Supply', categoryId: 12 },
    ];

    // Load selected parts from localStorage on mount
    useEffect(() => {
        const saved = localStorage.getItem('selectedParts');
        console.log('Loading from localStorage:', saved);
        if (saved) {
            try {
                setSelectedParts(JSON.parse(saved));
            } catch (error) {
                console.error('Error parsing selectedParts:', error);
            }
        }

        // Check when returning from another page
        const handleVisibilityChange = () => {
            if (document.visibilityState === 'visible') {
                const currentData = localStorage.getItem('selectedParts');
                console.log('Page became visible, reloading:', currentData);
                if (currentData) {
                    try {
                        setSelectedParts(JSON.parse(currentData));
                    } catch (error) {
                        console.error('Error parsing selectedParts:', error);
                    }
                }
            }
        };

        document.addEventListener('visibilitychange', handleVisibilityChange);
        return () => document.removeEventListener('visibilitychange', handleVisibilityChange);
    }, []);


    // Check compatibility whenever parts change
    useEffect(() => {
        checkCompatibility();
    }, [selectedParts]);

    const checkCompatibility = async () => {
        const selectedCount = Object.values(selectedParts).filter(p => p !== null).length;
        
        // Only check if 2 or more components are selected
        if (selectedCount < 2) {
        setCompatibilityWarnings([]);
        return;
        }

        setIsCheckingCompatibility(true);
        const warnings: CompatibilityWarning[] = [];

        try {
        const apiBase = 'http://localhost:5221/api/compatibility';

        // CPU - Motherboard
        if (selectedParts['CPU'] && selectedParts['Motherboard']) {
            const res = await fetch(`${apiBase}/cpu-motherboard`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                productId1: selectedParts['CPU'].id,
                productId2: selectedParts['Motherboard'].id,
            }),
            });
            if (res.ok) {
            const data = await res.json();
            if (!data.isCompatible) warnings.push(data);
            }
        }

        // Memory - Motherboard
        if (selectedParts['Memory'] && selectedParts['Motherboard']) {
            const res = await fetch(`${apiBase}/memory-motherboard`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                productId1: selectedParts['Memory'].id,
                productId2: selectedParts['Motherboard'].id,
            }),
            });
            if (res.ok) {
            const data = await res.json();
            if (!data.isCompatible) warnings.push(data);
            }
        }

        // CPU Cooler - CPU
        if (selectedParts['CPU Cooler'] && selectedParts['CPU']) {
            const res = await fetch(`${apiBase}/cooler-cpu`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                productId1: selectedParts['CPU Cooler'].id,
                productId2: selectedParts['CPU'].id,
            }),
            });
            if (res.ok) {
            const data = await res.json();
            if (!data.isCompatible) warnings.push(data);
            }
        }

        // GPU - Case
        if (selectedParts['GPU'] && selectedParts['Case']) {
            const res = await fetch(`${apiBase}/gpu-case`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                productId1: selectedParts['GPU'].id,
                productId2: selectedParts['Case'].id,
            }),
            });
            if (res.ok) {
            const data = await res.json();
            if (!data.isCompatible) warnings.push(data);
            }
        }

        // System - PSU (all components)
        if (selectedParts['Power Supply']) {
            const res = await fetch(`${apiBase}/system-psu`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                selectedParts: {
                CPU: selectedParts['CPU']?.id || 0,
                GPU: selectedParts['GPU']?.id || 0,
                'Power Supply': selectedParts['Power Supply'].id,
                },
            }),
            });
            if (res.ok) {
            const data = await res.json();
            if (!data.isCompatible) warnings.push(data);
            }
        }

        // GPU - PSU
        if (selectedParts['GPU'] && selectedParts['Power Supply']) {
            const res = await fetch(`${apiBase}/gpu-psu`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                productId1: selectedParts['GPU'].id,
                productId2: selectedParts['Power Supply'].id,
            }),
            });
            if (res.ok) {
            const data = await res.json();
            if (!data.isCompatible) warnings.push(data);
            }
        }

        // Motherboard - Case
        if (selectedParts['Motherboard'] && selectedParts['Case']) {
            const res = await fetch(`${apiBase}/motherboard-case`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                productId1: selectedParts['Motherboard'].id,
                productId2: selectedParts['Case'].id,
            }),
            });
            if (res.ok) {
            const data = await res.json();
            if (!data.isCompatible) warnings.push(data);
            }
        }

        setCompatibilityWarnings(warnings);
        } catch (error) {
        console.error('Error checking compatibility:', error);
        } finally {
        setIsCheckingCompatibility(false);
        }
    };


    const handleChoose = (categoryId: number, componentName: string) => {
        console.log(`Selecting for: ${componentName}`);
        localStorage.setItem('selectingFor', componentName);
        localStorage.setItem('selectingCategoryId', String(categoryId));
        navigate(`/viewproducts?categoryId=${categoryId}`);
    };

    const handleRemove = (componentName: string) => {
        console.log(`Removing: ${componentName}`);
        const updated = { ...selectedParts, [componentName]: null };
        setSelectedParts(updated);
        // Save immediately when user removes
        localStorage.setItem('selectedParts', JSON.stringify(updated));
    };

    const totalPrice = Object.values(selectedParts)
        .reduce((sum, part) => sum + (part ? part.price : 0), 0)
        .toFixed(2);

    return (
        <div>
            <Header />
            <div className="partpicker-container">
                <h1>Choose Your Parts</h1>

                <div className={`compatibility-banner ${compatibilityWarnings.length === 0 ? 'ok' : 'warn'}`}>
                    <h2>
                        {compatibilityWarnings.length === 0 ? '✓ No compatibility issues' : `⚠ ${compatibilityWarnings.length} compatibility warning${compatibilityWarnings.length !== 1 ? 's' : ''}`}
                    </h2>
                    {compatibilityWarnings.map((warning, index) => (
                        <div key={index} className="compat-warning-card">
                            <h3>{warning.component1} ↔ {warning.component2}</h3>
                            <ul>
                                {warning.warnings.map((msg, i) => (
                                    <li key={i}>{msg}</li>
                                ))}
                            </ul>
                        </div>
                    ))}
                    {isCheckingCompatibility && (
                        <p className="compat-checking">Checking compatibility...</p>
                    )}
                </div>

                <div className="partpicker-table-wrapper">
                    <table className="partpicker-table">
                        <thead>
                            <tr>
                                <th>Component</th>
                                <th>Selection</th>
                                <th>Price</th>
                                <th>Availability</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            {components.map((component) => {
                                const selected = selectedParts[component.name];
                                return (
                                    <tr key={component.name}>
                                        <td className="component-label">{component.name}</td>
                                        <td className={selected ? 'part-name' : 'part-empty'}>
                                            {selected ? selected.name : 'No part selected'}
                                        </td>
                                        <td className="part-price">
                                            {selected ? `$${selected.price.toFixed(2)}` : '—'}
                                        </td>
                                        <td>
                                            {selected ? (
                                                <span className={`stock-badge ${selected.stock > 0 ? 'in-stock' : 'out-of-stock'}`}>
                                                    {selected.stock > 0 ? 'In Stock' : 'Out of Stock'}
                                                </span>
                                            ) : '—'}
                                        </td>
                                        <td>
                                            <div className="action-cell">
                                                <button className="btn-choose" onClick={() => handleChoose(component.categoryId, component.name)}>
                                                    {selected ? 'Change' : 'Choose'}
                                                </button>
                                                {selected && (
                                                    <button className="btn-remove" onClick={() => handleRemove(component.name)}>
                                                        Remove
                                                    </button>
                                                )}
                                            </div>
                                        </td>
                                    </tr>
                                );
                            })}
                        </tbody>
                    </table>
                </div>

                <div className="partpicker-total">
                    <span>Estimated Total</span>
                    <strong>${totalPrice}</strong>
                </div>
            </div>
            <Footer />
        </div>
    );
}
