import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuthContext } from "../context/AuthContext";
import ProductManagement from "../Components/Admin-Components/ProductManagement";
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

    const handleResetPassword = async (userId: number) => {
        const newPassword = window.prompt("Voer een nieuw wachtwoord in voor deze gebruiker:");
        if (!newPassword) return;

        try {
            const response = await fetch(`http://localhost:5221/api/User/userid/${userId}/password`, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ newPassword }),
            });

            const data = await response.json();
            if (!response.ok) {
                throw new Error(data.message || "Password reset failed");
            }

            alert("Wachtwoord is gereset.");
        } catch (err) {
            console.error("Error resetting password:", err);
            alert("Wachtwoord resetten is mislukt.");
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
        <div className="admin-page">
            <h1>Admin Page</h1>
            <p className="admin-intro">Welcome to the admin page. Here you can manage users, view reports, and configure settings.</p>

            <section className="admin-section">
                <h2>User Management</h2>
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
                                    <button className="admin-btn" onClick={() => handleResetPassword(user.id)}>Reset password</button>
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
    );
}