import React from "react";
import { Link } from "react-router-dom";
import { useState } from "react";
import "./Register.css";

export default function Register() {
    const [firstName, setFirstName] = React.useState('');
    const [lastName, setLastName] = React.useState('');
    const [email, setEmail] = React.useState('');
    const [password, setPassword] = React.useState('');

    // Here If statement for if user is already logged in, redirect to home page or dashboard
    //
    //
    // -------------------------------------------------------------

    const handleSubmit = async (e: { preventDefault: () => void; }) => {
        e.preventDefault();
        // Here handle logic for backend 
        try {
            const response = await fetch("http://localhost:5221/api/users/register", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ firstName, lastName, email, password }),
            });
            const data = await response.json();

            if (!response.ok) {
                throw new Error(data.message || "Registration failed");
            }

            // Handle successful registration (e.g., redirect to login page)
            console.log("Registration successful:", data);
            setEmail("");
            setPassword("");
            setFirstName("");
            setLastName("");

        } catch (error) {
            console.error("Error during registration:", error);
        }
    };

    return (
    <div className="form-container">
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
                value={firstName}
                onChange={(e) => setFirstName(e.target.value)}
                required 
            />
            <br />
            <label> Last Name </label>
            <br />
            <input
                id="lastName"
                type="text"
                placeholder="Doe"
                value={lastName}
                onChange={(e) => setLastName(e.target.value)}
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
            <button type="submit">Register</button>
        </form>

        <Link to="/">Go back to Home</Link>
        <br />
        <Link to="/login">Have an account? Login here!</Link>
    </div>
    );
}