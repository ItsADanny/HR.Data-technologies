import { Link } from "react-router-dom";

export default function AdminProducts() {
    return (
        <div>
            <h1>Admin Product Management</h1>
            <p>Here you can manage your products.</p>

            {/* Create */}
            <form>
                <input type="number" placeholder="CategoryID" />
                <input type="text" placeholder="Product Name" />
                <button type="submit">Create Product</button>
            </form>
            {/* Update */}

            {/* Delete */}

            <Link to="/admin">Back to Admin Page</Link>
        </div>
    );
}