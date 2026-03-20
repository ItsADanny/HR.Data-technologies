import { Link } from "react-router-dom";

export default function Header() {
    return (
        <header className="headerbox">
            <h1 className="headertext">My PC Builder</h1>

            <Link to="/products" className="headerlink">Products</Link>
        </header>
    );
}  