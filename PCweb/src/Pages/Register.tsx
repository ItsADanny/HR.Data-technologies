import React from "react";
import { Link } from "react-router-dom";
import { useState } from "react";

export default function Register() {
    const [email, setEmail] = React.useState('');
    const [password, setPassword] = React.useState('');

    // Here If statement for if user is already logged in, redirect to home page or dashboard
    //
    //
    // -------------------------------------------------------------

    const handleSubmit = (e: { preventDefault: () => void; }) => {
        e.preventDefault();
        // Here handle logic for backend 
    };

    return (
    <div className="Register">
        <h1>Register Page</h1>
        <p>Please fill in the form to create an account.</p>
        {/* registration form  */}
        <form onSubmit={handleSubmit}>
            <label> First Name </label>
            <br />
            <input 
                id="firstName"
                type="text"
                placeholder="John"
                required 
            />
            <br />
            <label> Last Name </label>
            <br />
            <input
                id="lastName"
                type="text"
                placeholder="Doe"
                required
            />
            <br />
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
        <Link to="/login">Have an account? Login here!</Link>
    </div>
    );
}