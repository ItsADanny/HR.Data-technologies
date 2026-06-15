import { useState, useEffect } from "react";
import { useProductsData } from "../../hooks/useProductsData";
import { useAuthContext } from "../../context/AuthContext";

type Category = {
    id: number;
    categoryName: string;
};

type ProductDetails = {
    id: number;
    categoryID: number | null;
    name: string;
    manufacturer: string | null;
    description: string | null;
    price: number | null;
    stock: number;
    minimalStock: number;
    discontinued: boolean;
};

type ProductFormState = {
    categoryID: string;
    name: string;
    manufacturer: string;
    description: string;
    price: string;
    stock: string;
    minimalStock: string;
    discontinued: boolean;
};

const emptyFormState: ProductFormState = {
    categoryID: "",
    name: "",
    manufacturer: "",
    description: "",
    price: "",
    stock: "0",
    minimalStock: "0",
    discontinued: false,
};

export default function ProductManagement() {
    const { userID } = useAuthContext();

    const [categories, setCategories] = useState<Category[]>([]);
    const [categoryId, setCategoryId] = useState("");
    const [page, setPage] = useState(1);
    const { products, loading, error, refetch } = useProductsData(categoryId || undefined, undefined, page);

    const [editingProductId, setEditingProductId] = useState<number | null>(null);
    const [showForm, setShowForm] = useState(false);
    const [formState, setFormState] = useState<ProductFormState>(emptyFormState);
    const [saving, setSaving] = useState(false);

    useEffect(() => {
        const fetchCategories = async () => {
            try {
                const response = await fetch('/api/Category/all');
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const data: Category[] = await response.json();
                setCategories(data);
                setCategoryId((prev) => prev || (data.length > 0 ? String(data[0].id) : ""));
            } catch (err) {
                console.error('Error fetching categories:', err);
            }
        };
        fetchCategories();
    }, []);

    const handleCategoryChange = (value: string) => {
        setCategoryId(value);
        setPage(1);
        setShowForm(false);
    };

    const handleAddNew = () => {
        setEditingProductId(null);
        setFormState({ ...emptyFormState, categoryID: categoryId });
        setShowForm(true);
    };

    const handleEdit = async (productId: number) => {
        try {
            const response = await fetch(`/api/Product/${productId}/details`);
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            const data: ProductDetails = await response.json();

            setEditingProductId(data.id);
            setFormState({
                categoryID: data.categoryID ? String(data.categoryID) : categoryId,
                name: data.name,
                manufacturer: data.manufacturer ?? "",
                description: data.description ?? "",
                price: data.price !== null ? String(data.price) : "",
                stock: String(data.stock),
                minimalStock: String(data.minimalStock),
                discontinued: data.discontinued,
            });
            setShowForm(true);
        } catch (err) {
            console.error('Error fetching product details:', err);
            alert('Kon productgegevens niet ophalen.');
        }
    };

    const handleCancel = () => {
        setShowForm(false);
        setEditingProductId(null);
    };

    const handleDelete = async (productId: number) => {
        if (!window.confirm('Weet je zeker dat je dit product wilt verwijderen?')) return;

        try {
            const response = await fetch(`/api/Product/${productId}`, { method: 'DELETE' });
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            refetch();
        } catch (err) {
            console.error('Error deleting product:', err);
            alert('Product verwijderen is mislukt.');
        }
    };

    const handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();

        if (!formState.categoryID) {
            alert('Selecteer een categorie.');
            return;
        }

        const body = {
            categoryID: Number(formState.categoryID),
            name: formState.name,
            manufacturer: formState.manufacturer || null,
            description: formState.description || null,
            price: formState.price !== "" ? Number(formState.price) : null,
            stock: Number(formState.stock) || 0,
            minimalStock: Number(formState.minimalStock) || 0,
            discontinued: formState.discontinued,
            createUserID: userID,
            updateUserID: userID,
            fields: [],
        };

        const url = editingProductId ? `/api/Product/${editingProductId}` : `/api/Product/${formState.categoryID}`;
        const method = editingProductId ? 'PUT' : 'POST';

        setSaving(true);
        try {
            const response = await fetch(url, {
                method,
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(body),
            });

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            setShowForm(false);
            setEditingProductId(null);
            refetch();
        } catch (err) {
            console.error('Error saving product:', err);
            alert('Product opslaan is mislukt.');
        } finally {
            setSaving(false);
        }
    };

    return (
        <div>
            <h2>Product Management</h2>

            <div className="admin-toolbar">
                <label htmlFor="category-select">Category</label>
                <select id="category-select" value={categoryId} onChange={(e) => handleCategoryChange(e.target.value)}>
                    {categories.map((category) => (
                        <option key={category.id} value={category.id}>{category.categoryName}</option>
                    ))}
                </select>
                <button type="button" className="admin-btn admin-btn-primary" onClick={handleAddNew}>Add product</button>
            </div>

            {showForm && (
                <form className="admin-form" onSubmit={handleSubmit}>
                    <h3>{editingProductId ? `Edit product #${editingProductId}` : 'New product'}</h3>

                    <div className="admin-form-grid">
                        <div className="admin-form-field">
                            <label htmlFor="form-category">Category</label>
                            <select
                                id="form-category"
                                value={formState.categoryID}
                                onChange={(e) => setFormState({ ...formState, categoryID: e.target.value })}
                                required
                            >
                                <option value="" disabled>Select a category</option>
                                {categories.map((category) => (
                                    <option key={category.id} value={category.id}>{category.categoryName}</option>
                                ))}
                            </select>
                        </div>

                        <div className="admin-form-field">
                            <label htmlFor="form-name">Name</label>
                            <input
                                id="form-name"
                                type="text"
                                value={formState.name}
                                onChange={(e) => setFormState({ ...formState, name: e.target.value })}
                                required
                            />
                        </div>

                        <div className="admin-form-field">
                            <label htmlFor="form-manufacturer">Manufacturer</label>
                            <input
                                id="form-manufacturer"
                                type="text"
                                value={formState.manufacturer}
                                onChange={(e) => setFormState({ ...formState, manufacturer: e.target.value })}
                            />
                        </div>

                        <div className="admin-form-field">
                            <label htmlFor="form-price">Price</label>
                            <input
                                id="form-price"
                                type="number"
                                step="0.01"
                                min="0"
                                value={formState.price}
                                onChange={(e) => setFormState({ ...formState, price: e.target.value })}
                            />
                        </div>

                        <div className="admin-form-field">
                            <label htmlFor="form-stock">Stock</label>
                            <input
                                id="form-stock"
                                type="number"
                                min="0"
                                value={formState.stock}
                                onChange={(e) => setFormState({ ...formState, stock: e.target.value })}
                            />
                        </div>

                        <div className="admin-form-field">
                            <label htmlFor="form-minimal-stock">Minimal stock</label>
                            <input
                                id="form-minimal-stock"
                                type="number"
                                min="0"
                                value={formState.minimalStock}
                                onChange={(e) => setFormState({ ...formState, minimalStock: e.target.value })}
                            />
                        </div>

                        <div className="admin-form-field full-width">
                            <label htmlFor="form-description">Description</label>
                            <textarea
                                id="form-description"
                                value={formState.description}
                                onChange={(e) => setFormState({ ...formState, description: e.target.value })}
                            />
                        </div>

                        <div className="admin-form-field admin-form-checkbox">
                            <input
                                id="form-discontinued"
                                type="checkbox"
                                checked={formState.discontinued}
                                onChange={(e) => setFormState({ ...formState, discontinued: e.target.checked })}
                            />
                            <label htmlFor="form-discontinued">Discontinued</label>
                        </div>
                    </div>

                    <button type="submit" className="admin-btn admin-btn-primary" disabled={saving}>{saving ? 'Saving...' : 'Save'}</button>
                    <button type="button" className="admin-btn" onClick={handleCancel}>Cancel</button>
                </form>
            )}

            {loading && <p>Loading products...</p>}
            {error && <p>Error: {error}</p>}

            {!loading && !error && (
                <table className="admin-table">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Name</th>
                            <th>Price</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {products.map((product) => (
                            <tr key={product.id}>
                                <td>{product.id}</td>
                                <td>{product.name}</td>
                                <td>${product.price.toFixed(2)}</td>
                                <td>
                                    <button type="button" className="admin-btn" onClick={() => handleEdit(product.id)}>Edit</button>
                                    <button type="button" className="admin-btn admin-btn-danger" onClick={() => handleDelete(product.id)}>Delete</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            )}

            <div className="admin-pagination">
                <button type="button" className="admin-btn" onClick={() => setPage((p) => Math.max(1, p - 1))} disabled={page === 1}>Previous</button>
                <span>Page {page}</span>
                <button type="button" className="admin-btn" onClick={() => setPage((p) => p + 1)}>Next</button>
            </div>
        </div>
    );
}
