import React from 'react';
import { Link } from 'react-router-dom';
import { useState } from 'react';

export default function Login() {
    const [email, setEmail] = React.useState('');
    const [password, setPassword] = React.useState('');

    // Here If statement for if user is already logged in, redirect to home page or dashboard
    //
    //
    // -------------------------------------------------------------

    const handleSubmit = (e) => {
        e.preventDefault();
        // Here handle logic for backend 
    };

    return (
    <div className="Login">
        <h1>Login Page</h1>
        <p>Please enter your credentials to log in.</p>
        {/* Add your login form here */}
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