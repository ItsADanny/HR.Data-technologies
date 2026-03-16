import { Link } from "react-router-dom";

function Home() {
  return (
    <div className="Home">
      <h1>Welcome to the Home Page!</h1>
      <p>This is the main landing page of our application.</p>
      <Link to="/about">Learn more about us</Link>
      <br />
      <Link to="/contact">Contact us</Link>
      <br />
      <Link to="/login">Login</Link>
    </div>
  );
}

export default Home;