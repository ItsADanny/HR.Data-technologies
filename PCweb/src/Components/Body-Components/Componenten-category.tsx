import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "./Componenten-category.css";

const components = [
    {
        id: 1,
        name: "CPU",
        categoryId: 1,
    },
    {
        id: 2,
        name: "GPU",
        categoryId: 15,
    },
    {
        id: 3,
        name: "RAM",
        categoryId: 3,
    },
    {
        id: 4,
        name: "Storage",
        categoryId: 4,
    },
    {
        id: 5,
        name: "Motherboard",
        categoryId: 5,
    },
    {
        id: 6,
        name: "Power Supply",
        categoryId: 6,
    },
    {
        id: 7,
        name: "Cooling Solutions",
        categoryId: 7,
    },
    {
        id: 8,
        name: "Cases",
        categoryId: 8,
    }
];

export default function Components() {
    const navigate = useNavigate();

    const handleCategoryClick = (categoryId: number) => {
        navigate(`/viewproducts?categoryId=${categoryId}`);
    };

    return (
        <section className="components-container" aria-label="Component categories">
            <div className="components-grid">
                {components.map((component) => (
                    <article key={component.id} className="component-card">
                        <div className="component-content">
                            <h2>{component.name}</h2>
                            <button 
                                className="component-link"
                                onClick={() => handleCategoryClick(component.categoryId)}
                                style={{ background: 'none', border: 'none', cursor: 'pointer', color: 'inherit', textDecoration: 'underline' }}
                            >
                                View Details
                            </button>
                        </div>
                    </article>  
                ))}
            </div>
        </section>
    );
}