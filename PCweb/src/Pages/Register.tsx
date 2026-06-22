import React from "react";
import { Link } from "react-router-dom";
import { useState } from "react";
import Header from "../Components/Header-Component/Header";
import Footer from "../Components/Footer-Component/Footer";
import "../Components/Header-Component/Header.css";
import "./Register.css";

export default function Register() {
    const [firstName, setFirstName] = React.useState('');
    const [lastName, setLastName] = React.useState('');
    const [email, setEmail] = React.useState('');
    const [password, setPassword] = React.useState('');
    const [phone, setPhone] = React.useState('');
    const [country, setCountry] = React.useState('');
    const [success, setSuccess] = useState(false);
    const [error, setError] = useState('');

    // Here If statement for if user is already logged in, redirect to home page or dashboard
    //
    //
    // -------------------------------------------------------------

    const handleSubmit = async (e: { preventDefault: () => void; }) => {
        e.preventDefault();
        setError('');
        try {
            const response = await fetch("http://localhost:5221/api/User", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ firstName, lastName, email, password, phone, country }),
            });

            let data;
            try {
                data = await response.json();
            } catch {
                const text = await response.text();
                throw new Error(text || "Invalid server response");
            }

            if (!response.ok) {
                throw new Error(data.message || "Registration failed");
            }

            setSuccess(true);

        } catch (err) {
            setError(err instanceof Error ? err.message : "Registration failed");
        }
    };

    if (success) {
        return (
            <>
            <Header />
            <div className="form-container">
                <div className="register-success">
                    <span className="register-success-icon">✓</span>
                    <h2>Account created!</h2>
                    <p>Your account has been successfully created. You can now log in.</p>
                    <Link className="register-success-btn" to="/login">Go to Login</Link>
                </div>
            </div>
            <Footer />
            </>
        );
    }

    return (
    <>
    <Header />
    <div className="form-container">
        <h1>Register Page</h1>
        <p>Please fill in the form to create an account.</p>
        {error && <p className="form-error">{error}</p>}
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
            <label> Phone </label>
            <br />
            <input
                id="phone"
                type="tel"
                placeholder="0612345678"
                value={phone}
                onChange={(e) => setPhone(e.target.value)}
                required
            />
            <br />
            <label> Country </label>
            <br />
            <input
                id="country"
                type="text"
                placeholder="Netherlands"
                value={country}
                onChange={(e) => setCountry(e.target.value)}
                required
            />
            <br />
            <button type="submit">Register</button>
        </form>

        <Link to="/">Go back to Home</Link>
        <br />
        <Link to="/login">Have an account? Login here!</Link>
    </div>
    <Footer />
    </>
    );
}