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
                        <li><Link to="/products">Products</Link></li>
                        <li><Link to="/categories">CPU</Link></li>
                        <li><Link to="/categories">GPU</Link></li>
                        <li><Link to="/categories">RAM</Link></li>
                        <li><Link to="/categories">Storage</Link></li>
                        <li><Link to="/categories">Motherboard</Link></li>
                        <li><Link to="/categories">Power Supply</Link></li>
                        <li><Link to="/categories">Cases</Link></li>
                    </ul>
                </div>

                {/* Support */}
                <div>
                    <h3 className="footer-heading">Support</h3>
                    <ul className="footer-links">
                        <li><Link to="/contact">Contact Us</Link></li>
                        <li><Link to="/about">About Us</Link></li>
                        <li><Link to="/faq">FAQ</Link></li>
                        <li><Link to="/my-account">My Account</Link></li>
                        <li><Link to="/order-history">Order History</Link></li>
                        <li><Link to="/terms">Terms of Service</Link></li>
                        <li><Link to="/privacy">Privacy Policy</Link></li>
                    </ul>
                </div>
            </div>
        </div>
    </footer>
  );
}

export default Footer;