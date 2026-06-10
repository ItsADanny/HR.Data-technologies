import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "./Componenten-category.css";

// const components = [
//     {
//         id: 1,
//         name: "CPU",
//         categoryId: 4,
//     },
//     {
//         id: 2,
//         name: "GPU",
//         categoryId: 15,
//     },
//     {
//         id: 3,
//         name: "RAM",
//         categoryId: 8,
//     },
//     {
//         id: 4,
//         name: "Storage",
//         categoryId: 7,
//     },
//     {
//         id: 5,
//         name: "Motherboard",
//         categoryId: 9,
//     },
//     {
//         id: 6,
//         name: "Power Supply",
//         categoryId: 12,
//     },
//     {
//         id: 7,
//         name: "Cooling Solutions",
//         categoryId: 5,
//     },
//     {
//         id: 8,
//         name: "Cases",
//         categoryId: 1,
//     }
// ];
interface Component {
    id: number;
    categoryName: string;
    description: string;
}


export default function Components() {
    const navigate = useNavigate();
    const [categories, setCategories] = useState<Component[]>([]);

    useEffect(() => {
        fetch('http://localhost:5221/api/category/all')
            .then(response => response.json())
            .then(data => setCategories(data));
    }, []);

    const handleCategoryClick = (categoryId: number) => {
        navigate(`/viewproducts?categoryId=${categoryId}`);
    };

    return (
        <section className="components-container" aria-label="Component categories">
            <div className="components-grid">
                {categories.map((category) => (
                    <article key={category.id} className="component-card">
                        <div className="component-content">
                            <h2>{category.categoryName}</h2>
                            <button 
                                className="component-link"
                                onClick={() => handleCategoryClick(category.id)}
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