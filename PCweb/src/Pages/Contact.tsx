import { Link } from 'react-router-dom';
import Header from '../Components/Header-Component/Header';
import Footer from '../Components/Footer-Component/Footer';
import '../Components/Header-Component/Header.css';

function Contact() {
  return (
    <>
      <Header />
      <div className="Contact" style={{ maxWidth: 800, margin: '60px auto', padding: '0 24px' }}>
        <h1>Contact Us</h1>
        <p>If you have any questions, please feel free to reach out to us!</p>
        <Link to="/">Go back to Home</Link>
        <br />
        <Link to="/about">Learn more about us</Link>
      </div>
      <Footer />
    </>
  );
}

export default Contact;
