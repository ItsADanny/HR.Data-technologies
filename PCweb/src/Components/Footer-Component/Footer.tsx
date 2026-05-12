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
                        <Link to="/products">Products</Link><br />
                        <Link to="/categories">CPU</Link><br />
                        <Link to="/categories">GPU</Link><br />
                        <Link to="/categories">RAM</Link><br />
                        <Link to="/categories">Storage</Link><br />
                        <Link to="/categories">Motherboard</Link><br />
                        <Link to="/categories">Power Supply</Link><br />
                        <Link to="/categories">Cases</Link><br />
                    </ul>
                </div>

                {/* Support */}
                <div>
                    <h3 className="footer-heading">Support</h3>
                    <ul className="footer-links">
                        <Link to="/contact">Contact Us</Link><br />
                        <Link to="/about">About Us</Link><br />
                        <Link to="/faq">FAQ</Link><br />
                        <Link to="/my-account">My Account</Link><br />
                        <Link to="/order-history">Order History</Link><br />
                        <Link to="/terms">Terms of Service</Link><br />
                        <Link to="/privacy">Privacy Policy</Link><br />
                    </ul>
                </div>
            </div>
        </div>
    </footer>
  );
}

export default Footer;