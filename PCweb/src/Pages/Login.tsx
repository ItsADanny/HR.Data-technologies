import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useState } from 'react';
import { useAuthContext } from '../context/AuthContext';
import "./Login.css";

export default function Login() {
    const [email, setEmail] = React.useState('');
    const [password, setPassword] = React.useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();
    const { login } = useAuthContext();

    // Here If statement for if user is already logged in, redirect to home page or dashboard
    //
    //
    // -------------------------------------------------------------

    const handleSubmit = async (e: { preventDefault: () => void; }) => {
        e.preventDefault();
        setError('');

        try {
            const response = await fetch("http://localhost:5221/api/User/login", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ email, password }),
            });

            let data;
            try {
                data = await response.json();
            } catch {
                const text = await response.text();
                throw new Error(text || "Invalid server response");
            }

            if (!response.ok) {
                throw new Error(data.message || "Login failed");
            }

            login(data.sessionToken);
            navigate("/");

        } catch (err) {
            console.error("Error during login:", err);
            setError(err instanceof Error ? err.message : "Login failed");
        }
    };

    return (
    <div className="form-container">
        <h1>Login Page</h1>
        <p>Please enter your credentials to log in.</p>
        {error && <p className="form-error">{error}</p>}
        {/* login form */}
        <form onSubmit={handleSubmit}>
            <label> Email </label>
            <br />
            <input 
                id="email"
                type="email" 
                placeholder="john@example.com"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                required 
            />
            <br />
            <label> Password </label>
            <br />
            <input 
                id="password"
                type="password" 
                placeholder="••••••••"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                required 
            />
            <br />
            <button type="submit">Login</button>
        </form>

        <Link to="/">Go back to Home</Link>
        <br />
        <Link to="/register">Don't have an account? Register here</Link>
    </div>
    );
}