import { Link } from 'react-router-dom';
import Header from '../Components/Header-Component/Header';
import Footer from '../Components/Footer-Component/Footer';
import '../Components/Header-Component/Header.css';

function About() {
  return (
    <>
      <Header />
      <div className="About" style={{ maxWidth: 800, margin: '60px auto', padding: '0 24px' }}>
        <h1>About Us</h1>
        <p>This page provides information about our company and our mission.</p>
        <Link to="/">Go back to Home</Link>
        <br />
        <Link to="/contact">Contact us</Link>
      </div>
      <Footer />
    </>
  );
}

export default About;
