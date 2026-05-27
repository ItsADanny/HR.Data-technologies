
import { Link } from 'react-router-dom';
import Header from '../Components/Header-Component/Header';
import Footer from '../Components/Footer-Component/Footer';

export default function PartPicker() {
    const components = [
        { name: 'CPU', label: 'Choose a CPU', categoryId: 4 },
        { name: 'CPU Cooler', label: 'Choose a CPU Cooler', categoryId: 5 },
        { name: 'Motherboard', label: 'Choose a Motherboard', categoryId: 9 },
        { name: 'Memory', label: 'Choose Memory', categoryId: 8 },
        { name: 'Storage', label: 'Choose Storage', categoryId: 7 },
        { name: 'GPU', label: 'Choose a GPU', categoryId: 15 },
        { name: 'Case', label: 'Choose a Case', categoryId: 1 },
        { name: 'Power Supply', label: 'Choose a Power Supply', categoryId: 12 },
    ];

    return (
        <div>
            <Header />
            <h1>Choose your parts!</h1>
            <table border={1}>
                <thead>
                    <tr>
                        <th>Component</th>
                        <th>Selection</th>
                        <th>Price</th>
                        <th>Availability</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    {components.map((component) => (
                        <tr key={component.name}>
                            <td>{component.name}</td>
                            <td>
                                {/* http://localhost:5173/viewproducts?categoryId=1 */}
                                <Link to={`/viewproducts?categoryId=${component.categoryId}`}>
                                    {component.label}
                                </Link>
                            </td>
                            <td>-</td>
                            <td>-</td>
                            <td>-</td>
                        </tr>
                    ))}
                </tbody>
            </table>

            <Footer />
        </div>
    );
}
