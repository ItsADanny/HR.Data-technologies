import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { CartProvider } from './context/CartContext';
import { AuthProvider } from './context/AuthContext';
import Home from './Pages/Home';
import About from './Pages/About';
import Contact from './Pages/Contact';
import Login from './Pages/Login';
import Register from './Pages/Register';
import Products from './Pages/Products';
import Cart from './Pages/Cart';
import ViewProducts from './Pages/ViewProducts';
import PartPicker from './Pages/PartPicker';
import AdminPage from './Pages/AdminPage';

function App() {
  return (
    <AuthProvider>
      <CartProvider>
        <BrowserRouter>
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/about" element={<About />} />
            <Route path="/contact" element={<Contact />} />
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
            <Route path="/products/:productId" element={<Products />} />
            <Route path="/viewproducts" element={<ViewProducts />} />
            <Route path="/cart" element={<Cart />} />
            <Route path="/partpicker" element={<PartPicker />} />
            <Route path="/admin" element={<AdminPage />} />
          </Routes>
        </BrowserRouter>
      </CartProvider>
    </AuthProvider>
  );
}

export default App;