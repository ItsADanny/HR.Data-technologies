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
<Footer />
    </div>
  );
}

export default Home;