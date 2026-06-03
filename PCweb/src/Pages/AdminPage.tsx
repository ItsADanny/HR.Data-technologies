import { useEffect, useState } from "react";

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



    return (
        <div>
            <h1>Admin Page</h1>
            <p>Welcome to the admin page. Here you can manage users, view reports, and configure settings.</p>

            <h2>User Management</h2>
            <table>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Email</th>
                        <th>Role</th>
                        <th>Phone</th>
                        <th>Country</th>
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
                        </tr>
                    ))}
                </tbody>
            </table>


            <h2>User Roles</h2>
            <ul>
                {userRoles.map((role) => (
                    <li key={role.id}>{role.id}: {role.name} - {role.description}</li>
                ))}
            </ul>

        </div>
    );
}