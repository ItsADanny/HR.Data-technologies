import { useEffect, useState } from "react";
import "./Componenten-category.css";

const components = [
    {
        id: 1,
        name: "CPU",

    },
    {
        id: 2,
        name: "GPU",
        
    },
    {
        id: 3,
        name: "RAM",
    },
    {
        id: 4,
        name: "Storage",
    },
    {
        id: 5,
        name: "Motherboard",
    },
    {
        id: 6,
        name: "Power Supply",
    },
    {
        id: 7,
        name: "Cooling Solutions",
    },
    {
        id: 8,
        name: "Cases",
    }
];

export default function Components() {
    return (
        <section className="components-container" aria-label="Component categories">
            <div className="components-grid">
                {components.map((component) => (
                    <article key={component.id} className="component-card">
                        <div className="component-content">
                            <h2>{component.name}</h2>
                            <a className="component-link" href="/">
                                View Details
                            </a>
                        </div>
                    </article>  
                ))}
            </div>
        </section>
    );
}