import { Link } from "react-router-dom";
import Header from "../Components/Header-Component/Header";
import Navbar from "../Components/Header-Component/Navbar";
import Slideshow from "../Components/Body-Components/Slideshow";
import "../Components/Header-Component/Navbar.css";
import "../Components/Header-Component/Header.css";

function Home() {
  return (
    <div className="Home">
      <Header />
      <Navbar />
      <Slideshow />
      <h1>Welcome to the Home Page!</h1>
      <p>This is the main landing page of our application.</p>
    </div>
  );
}

export default Home;