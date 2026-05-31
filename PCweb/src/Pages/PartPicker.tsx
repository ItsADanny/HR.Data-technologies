import { Link, useNavigate } from 'react-router-dom';
import { useEffect, useState } from 'react';
import Header from '../Components/Header-Component/Header';
import Footer from '../Components/Footer-Component/Footer';

interface SelectedPart {
    id: number;
    name: string;
    price: number;
    stock: number;
}

export default function PartPicker() {
    const navigate = useNavigate();
    const [selectedParts, setSelectedParts] = useState<Record<string, SelectedPart | null>>({});

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

    return (
        <div>
            <Header />
            <h1>Choose your parts!</h1>
            <table border={1}>
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
                                <td>{component.name}</td>
                                <td>{selected ? selected.name : '-'}</td>
                                <td>{selected ? `$${selected.price.toFixed(2)}` : '-'}</td>
                                <td>{selected ? (selected.stock > 0 ? 'In Stock' : 'Out of Stock') : '-'}</td>
                                <td>
                                    <button onClick={() => handleChoose(component.categoryId, component.name)}>
                                        Choose
                                    </button>
                                    {selected && (
                                        <button onClick={() => handleRemove(component.name)} style={{ marginLeft: '5px' }}>
                                            Remove
                                        </button>
                                    )}
                                </td>
                            </tr>
                        );
                    })}
                </tbody>
            </table>

            <Footer />
        </div>
    );
}
