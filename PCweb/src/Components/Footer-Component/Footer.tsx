import { Link } from "react-router-dom";
import "./Footer.css";

function Footer() {
  return (
    <footer className="footer">
        <h2 className="footer-title">Ontdek alles bij BuildHub</h2>
        <div className="footer-container">
            <div className="footer-grid">
                {/* Shop */}
                <div>
                    <h3 className="footer-heading">Shop</h3>
                    <ul className="footer-links">
                        <li><Link to="/partpicker">Part Picker</Link></li>
                        <li><Link to="/viewproducts">All Products</Link></li>
                        <li><Link to="/cart">Cart</Link></li>
                    </ul>
                </div>

                {/* Support */}
                <div>
                    <h3 className="footer-heading">Support</h3>
                    <ul className="footer-links">
                        <li><Link to="/contact">Contact Us</Link></li>
                        <li><Link to="/about">About Us</Link></li>
                        <li><Link to="/account">My Account</Link></li>
                        <li><Link to="/orders">Order History</Link></li>
                    </ul>
                </div>
            </div>
        </div>
    </footer>
  );
}

export default Footer;