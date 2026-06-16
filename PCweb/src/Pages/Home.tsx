import { Link } from "react-router-dom";
import Header from "../Components/Header-Component/Header";
import Navbar from "../Components/Header-Component/Navbar";
import Slideshow from "../Components/Body-Components/Slideshow";
import Category from "../Components/Body-Components/Componenten-category";
import "../Components/Header-Component/Navbar.css";
import "../Components/Header-Component/Header.css";
import "./Home.css";
import Footer from "../Components/Footer-Component/Footer";

function Home() {
  return (
    <div className="Home">
      <Header />
      <Navbar />
      <Slideshow />
      <Category />
      <div className="home-hero-section">
        <h1>Welcome to the Home Page!</h1>
        <p>This is the main landing page of our application.</p>
        <div className="home-actions">
          <Link to="/viewproducts" className="cta-button">View Products</Link>
        </div>
      </div>

      <Footer />
    </div>
  );
}

export default Home;