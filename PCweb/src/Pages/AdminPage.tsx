import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useAuthContext } from "../context/AuthContext";
import ProductManagement from "../Components/Admin-Components/ProductManagement";
import Header from "../Components/Header-Component/Header";
import Footer from "../Components/Footer-Component/Footer";
import "../Components/Header-Component/Header.css";
import "./AdminPage.css";

type User = {
    id: number;
    first_Name: string;
    last_Name: string;
    email: string;
    role: string;
    phone: string;
    country: string;
};

type UserRole = {
    id: number;
    name: string;
    description: string;
    globalReadWriteUser: number;
    globalReadWriteAddress: number;
    globalReadWriteProduct: number;
    globalReadWriteCategory: number;
    globalReadWriteRole: number;
    readWriteUser: number;
    readWriteAddress: number;
    createDateTime: string;
    updateDateTime: string | null;
    createUserID: number;
    updateUserID: number | null;
};

export default function AdminPage() {
    const navigate = useNavigate();
    const { isLoggedIn, isAdmin, roleLoading } = useAuthContext();

    // Redirect non-admins away from this page
    useEffect(() => {
        if (roleLoading) return;
        if (!isLoggedIn || !isAdmin) {
            navigate("/");
        }
    }, [isLoggedIn, isAdmin, roleLoading, navigate]);

    // Get all users from controller
    const [users, setUsers] = useState<User[]>([]);
    const [userRoles, setUserRoles] = useState<UserRole[]>([]);

    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const response = await fetch('http://localhost:5221/api/User/all');
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const data = await response.json();
                console.log('Fetched users:', data);
                setUsers(data);
            } catch (err) {
                console.error('Error fetching users:', err);
            }
        };
        fetchUsers();
    }, []);

    const [resetPasswordTarget, setResetPasswordTarget] = useState<number | null>(null);
    const [resetPasswordValue, setResetPasswordValue] = useState("");
    const [resetPasswordMessage, setResetPasswordMessage] = useState("");

    const startResetPassword = (userId: number) => {
        setResetPasswordTarget(userId);
        setResetPasswordValue("");
        setResetPasswordMessage("");
    };

    const cancelResetPassword = () => {
        setResetPasswordTarget(null);
        setResetPasswordValue("");
    };

    const confirmResetPassword = async (userId: number) => {
        if (!resetPasswordValue) return;

        try {
            const response = await fetch(`http://localhost:5221/api/User/userid/${userId}/password`, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ newPassword: resetPasswordValue }),
            });

            const data = await response.json();
            if (!response.ok) {
                throw new Error(data.message || "Password reset failed");
            }

            setResetPasswordMessage("Wachtwoord is gereset.");
            setResetPasswordTarget(null);
            setResetPasswordValue("");
        } catch (err) {
            console.error("Error resetting password:", err);
            setResetPasswordMessage("Wachtwoord resetten is mislukt.");
        }
    };

    useEffect(() => {
        const fetchUserRoles = async () => {
            try {
                const response = await fetch('http://localhost:5221/api/UserRole/all');
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const data = await response.json();
                console.log('Fetched user roles:', data);
                setUserRoles(data);
            } catch (err) {
                console.error('Error fetching user roles:', err);
            } 
        };
        fetchUserRoles();
    }, []);



    if (roleLoading || !isLoggedIn || !isAdmin) {
        return null;
    }

    return (
        <>
        <Header />
        <div className="admin-page">
            <Link className="admin-back-link" to="/">← Back to Home</Link>
            <h1>Admin Page</h1>
            <p className="admin-intro">Welcome to the admin page. Here you can manage users, view reports, and configure settings.</p>
            <Link className="admin-nav-btn" to="/orders">View Orders</Link>
            <section className="admin-section">
                <h2>User Management</h2>
                {resetPasswordMessage && <p>{resetPasswordMessage}</p>}
                <table className="admin-table">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Name</th>
                            <th>Email</th>
                            <th>Role</th>
                            <th>Phone</th>
                            <th>Country</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {users.map((user) => (
                            <tr key={user.id}>
                                <td>{user.id}</td>
                                <td>{user.first_Name} {user.last_Name}</td>
                                <td>{user.email}</td>
                                <td>{user.role}</td>
                                <td>{user.phone}</td>
                                <td>{user.country}</td>
                                <td>
                                    {resetPasswordTarget === user.id ? (
                                        <div className="admin-inline-form">
                                            <input
                                                type="password"
                                                placeholder="New password"
                                                value={resetPasswordValue}
                                                onChange={(e) => setResetPasswordValue(e.target.value)}
                                            />
                                            <button className="admin-btn admin-btn-primary" onClick={() => confirmResetPassword(user.id)}>Confirm</button>
                                            <button className="admin-btn" onClick={cancelResetPassword}>Cancel</button>
                                        </div>
                                    ) : (
                                        <button className="admin-btn" onClick={() => startResetPassword(user.id)}>Reset password</button>
                                    )}
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </section>

            <section className="admin-section">
                <h2>User Roles</h2>
                <ul className="admin-role-list">
                    {userRoles.map((role) => (
                        <li key={role.id}><strong>{role.name}</strong> (ID {role.id}) - {role.description}</li>
                    ))}
                </ul>
            </section>

            <section className="admin-section">
                <ProductManagement />
            </section>
        </div>
        <Footer />
        </>
    );
}