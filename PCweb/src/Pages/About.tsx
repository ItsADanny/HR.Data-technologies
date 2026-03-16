import { Link } from 'react-router-dom';

function About() {
  return (
    <div className="About">
      <h1>About Us</h1>
      <p>This page provides information about our company and our mission.</p>

      <Link to="/">Go back to Home</Link>
      <br />
        <Link to="/contact">Contact us</Link>
    </div>
  );
}

export default About;