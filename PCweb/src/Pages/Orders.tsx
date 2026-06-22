import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Header from "../Components/Header-Component/Header";
import Footer from "../Components/Footer-Component/Footer";
import "../Components/Header-Component/Header.css";
import "./Orders.css";

type Order = {
    orderID: number;
    userID: number;
    userName: string;
    orderStatus: string;
    totalAmount: number;
    shippingAddress: string;
    billingAddress: string;
    createDateTime: string;
};
export default function Orders() {
    const [orders, setOrders] = useState<Order[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");

    const fetchOrders = async () => {
        try {
            setLoading(true);
            setError("");

            const response = await fetch("http://localhost:5221/api/order/admin/orders");

            if (!response.ok) {
                throw new Error("Failed to fetch orders");
            }

            const data = await response.json();
            setOrders(data);

        } catch (err: any) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchOrders();
    }, []);

    return (
        <>
            <Header />
            <div className="orders-page">
                <Link className="orders-back-link" to="/admin">← Back to Admin</Link>
                <h1>Admin Orders</h1>

                {loading && <p className="orders-status">Loading orders...</p>}
                {error && <p className="orders-error">{error}</p>}

                <div className="orders-section">
                    {orders.length === 0 && !loading
                        ? <p className="orders-empty">No orders found.</p>
                        : (
                            <table className="orders-table">
                                <thead>
                                    <tr>
                                        <th>Order ID</th>
                                        <th>User</th>
                                        <th>Status</th>
                                        <th>Total</th>
                                        <th>Shipping</th>
                                        <th>Billing</th>
                                        <th>Date</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {orders.map((order) => (
                                        <tr key={order.orderID}>
                                            <td>#{order.orderID}</td>
                                            <td>{order.userName}</td>
                                            <td>
                                                <span className="orders-badge">{order.orderStatus}</span>
                                            </td>
                                            <td className="orders-total">€{order.totalAmount}</td>
                                            <td>{order.shippingAddress}</td>
                                            <td>{order.billingAddress}</td>
                                            <td>{new Date(order.createDateTime).toLocaleString()}</td>
                                        </tr>
                                    ))}
                                </tbody>
                            </table>
                        )
                    }
                </div>
            </div>
            <Footer />
        </>
    );
}