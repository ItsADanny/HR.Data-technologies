import { ChangeEvent, useEffect, useState } from "react";
import { useAuthContext } from "../context/AuthContext";
import { Link } from "react-router-dom";

type Address = {
    addressId: number;
    country: string;
    city: string;
    street: string;
    houseNumber: number;
    houseNumberAddition: string;
    postCode: string;
};

type User = {
    first_Name: string;
    last_Name: string;
    email: string;
    phone: string;
    country: string;
    shipping_Address?: number | null;
    billing_Address?: number | null;
};

type UserFieldKey = "first_Name" | "last_Name" | "email" | "phone" | "country";

const userFields: { name: UserFieldKey; placeholder: string }[] = [
    { name: "first_Name", placeholder: "First Name" },
    { name: "last_Name", placeholder: "Last Name" },
    { name: "email", placeholder: "Email" },
    { name: "phone", placeholder: "Phone" },
    { name: "country", placeholder: "Country" }
];

const emptyAddressBody = {
    country: "",
    city: "",
    street: "",
    houseNumber: 0,
    houseNumberAddition: "",
    postCode: ""
};

function addressToBody(address: Address | undefined) {
    if (!address) return emptyAddressBody;
    return {
        country: address.country,
        city: address.city,
        street: address.street,
        houseNumber: address.houseNumber,
        houseNumberAddition: address.houseNumberAddition,
        postCode: address.postCode
    };
}

export default function UserAccountInfo() {
    const { userID: userId } = useAuthContext();

    const [user, setUser] = useState<User>({
        first_Name: "",
        last_Name: "",
        email: "",
        phone: "",
        country: ""
    });
    const [addresses, setAddresses] = useState<Address[]>([]);
    const [saving, setSaving] = useState(false);
    const [message, setMessage] = useState("");

    useEffect(() => {
        if (!userId) return;

        async function fetchUserData() {
            try {
                const response = await fetch(`/api/User/userid/${userId}`);
                if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`);
                const userData = await response.json();
                setUser(userData);
            } catch (error) {
                console.error('Error fetching user data:', error);
            }
        }

        async function fetchAddresses() {
            try {
                const response = await fetch(`/api/Address/user/${userId}`);
                if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`);
                const data = await response.json();
                setAddresses(data);
            } catch (error) {
                console.error('Error fetching addresses:', error);
            }
        }

        fetchUserData();
        fetchAddresses();
    }, [userId]);

    function handleChange(e: ChangeEvent<HTMLInputElement>) {
        setUser(prev => ({
            ...prev,
            [e.target.name]: e.target.value
        }));
    }

    async function handleDeleteAddress(addressId: number) {
        try {
            const response = await fetch(`/api/Address/${addressId}`, { method: "DELETE" });
            if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`);
            setAddresses(prev => prev.filter(address => address.addressId !== addressId));
        } catch (error) {
            console.error('Error deleting address:', error);
            setMessage("Failed to delete address.");
        }
    }

    async function handleSave() {
        if (!userId) return;

        setSaving(true);
        try {
            // The backend's UpdateAddressDTO fields are non-nullable, so shippingAddress/
            // billingAddress must always be present. Round-trip the currently linked
            // address (if any) so the update is a no-op for it.
            const linkedShipping = addresses.find(address => address.addressId === user.shipping_Address);
            const linkedBilling = addresses.find(address => address.addressId === user.billing_Address);

            const body = {
                shipping_Address: user.shipping_Address ?? null,
                billing_Address: user.billing_Address ?? null,
                first_Name: user.first_Name,
                last_Name: user.last_Name,
                email: user.email,
                phone: user.phone,
                country: user.country,
                shippingAddress: addressToBody(linkedShipping),
                billingAddress: addressToBody(linkedBilling)
            };

            const res = await fetch(`/api/User/userid/${userId}`, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(body)
            });
            setMessage(res.ok ? "Profile updated successfully!" : "Failed to update profile.");

        } catch (error) {
            console.error('Error updating user data:', error);
            setMessage("Failed to update profile.");
        }
        setSaving(false);
    }

    return (
        <div>
            <Link to="/">Back to Home</Link>
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

            <button onClick={handleSave} disabled={saving}>
                {saving ? "Saving..." : "Update Profile"}
            </button>

            <h2>My Addresses</h2>
            {addresses.length === 0 && <p>No addresses saved.</p>}
            <ul>
                {addresses.map(address => (
                    <li key={address.addressId}>
                        {address.street} {address.houseNumber}{address.houseNumberAddition}, {address.postCode} {address.city}, {address.country}
                        {" "}
                        <button onClick={() => handleDeleteAddress(address.addressId)}>Delete</button>
                    </li>
                ))}
            </ul>
        </div>
    );
}
