import { useEffect, useState } from "react";
import { Link } from "react-router-dom";

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
        <div>
            <h1>Admin Orders</h1>
            <Link to="/admin">Back to Admin</Link>

            {loading && <p>Loading...</p>}
            {error && <p style={{ color: "red" }}>{error}</p>}

            <table>
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
                            <td>{order.orderID}</td>
                            <td>{order.userName}</td>
                            <td>{order.orderStatus}</td>
                            <td>{order.totalAmount}</td>
                            <td>{order.shippingAddress}</td>
                            <td>{order.billingAddress}</td>
                            <td>
                                {new Date(order.createDateTime).toLocaleString()}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}