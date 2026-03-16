import { Link } from "react-router-dom";

function Contact() {
  return (
    <div className="Contact">
      <h1>Contact Us</h1>
      <p>If you have any questions, please feel free to reach out to us!</p>
      <Link to="/">Go back to Home</Link>
      <br />
      <Link to="/about">Learn more about us</Link>
      
    </div>
    );
}

export default Contact;