import { ChangeEvent, useEffect, useState } from "react";

type User = {
    first_Name: string;
    last_Name: string;
    email: string;
    phone: string;
    country: string;
    shippingAddress?: any;
    billingAddress?: any;
};

const userFields: { name: keyof User; placeholder: string }[] = [
    { name: "first_Name", placeholder: "First Name" },
    { name: "last_Name", placeholder: "Last Name" },
    { name: "email", placeholder: "Email" },
    { name: "phone", placeholder: "Phone" },
    { name: "country", placeholder: "Country" }
];

const addressFields = [
    { name: "country", placeholder: "Country" },
    { name: "city", placeholder: "City" },
    { name: "street", placeholder: "Street" },
    { name: "houseNumber", placeholder: "House Number" },
    { name: "houseNumberAddition", placeholder: "Addition" },
    { name: "postCode", placeholder: "Post Code" }
];

export default function UserAccountInfo() {
    const userId = 1; // Replace with actual user ID from authentication context

    const [user, setUser] = useState<User>({
        first_Name: "",
        last_Name: "",
        email: "",
        phone: "",
        country: ""
    });
    const [saving, setSaving] = useState(false);
    const [message, setMessage] = useState("");

    
    useEffect(() => {
        async function fetchUserData() {
            try {
                const response = await fetch(`http://localhost:8067/api/account/${userId}`);
                const userData = await response.json();

                let shipping = null;
                let billing = null;

                if (userData.shipping_Address) {
                    const shippingRes = await fetch(`http://localhost:8067/api/address/${userData.shipping_Address}`);
                    shipping = await shippingRes.json();
                }

                if (userData.billing_Address) {
                    const billingRes = await fetch(`http://localhost:8067/api/address/${userData.billing_Address}`);
                    billing = await billingRes.json();
                }

                setUser({
                    ...userData,
                    shippingAddress: shipping,
                    billingAddress: billing
                });

            } catch (error) {
                console.error('Error fetching user data:', error);
            }
        }

        fetchUserData();
    }, [userId]);

    function handleChange(e: { target: { name: any; value: any; }; }) {
        if (!user) return;
        
        setUser({
            ...user,
            [e.target.name]: e.target.value
        });
    }

    function handleAddressChange(e: ChangeEvent<HTMLInputElement, HTMLInputElement>, section: string) {
        if (!user) return;
        setUser({
            ...user,
            [section]: {
                ...user[section],
                [e.target.name]: e.target.value
            }
        });
    }

    async function handleSave() {
        setSaving(true);
        try {
            const res = await fetch(`http://localhost:8067/api/account/${userId}`, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(user)
            });
            setMessage(res.ok ? "Profile updated successfully!" : "Failed to update profile.");

        } catch (error) {
            console.error('Error updating user data:', error);
        }
        setSaving(false);
    }   



    return (
        <div>
            <h1>User Account Info</h1>

            {message && <p><b>{message}</b></p>}

            <h2>Account Details</h2>

            {userFields.map(field => (
                <div key={field.name}>
                    <input
                        name={field.name}
                        value={user[field.name]}
                        onChange={handleChange}
                        placeholder={field.placeholder}
                    />
                    <br />
                </div>
            ))}

            <h2>Shipping Address</h2>
            {addressFields.map(field => (
                <div key={field.name}>
                    <input
                        name={field.name}
                        value={user.shippingAddress ? user.shippingAddress[field.name] : ""}
                        onChange={(e) => handleAddressChange(e, "shippingAddress")}
                        placeholder={field.placeholder}
                    />
                    <br />
                </div>
            ))}

            <h2>Billing Address</h2>
            {addressFields.map(field => (
                <div key={field.name}>
                    <input
                        name={field.name}
                        value={user.billingAddress ? user.billingAddress[field.name] : ""}
                        onChange={(e) => handleAddressChange(e, "billingAddress")}
                        placeholder={field.placeholder}
                    />
                    <br />
                </div>
            ))}

            <button onClick={handleSave} disabled={saving}>
                {saving ? "Saving..." : "Update Profile"}
            </button>
        </div>
    );
}