import React, { useState, useEffect } from "react";
import axios from "axios";
import ProductList from "./ProductList";
import VendedorDropdown from "./VendedorDropdown";
import PedidoList from "./PedidoList";

function App() {
  const [productos, setProductos] = useState([]);
  const [vendedorSeleccionado, setVendedorSeleccionado] = useState(null);
  const [pedido, setPedido] = useState([]);
  const [pedidosGuardados, setPedidosGuardados] = useState([]);

  useEffect(() => {
    // Obtener productos desde el backend
    const fetchProductos = async () => {
      try {
        const response = await axios.get("https://localhost:7225/api/Pedidos/articulos");
        const productosFiltrados = response.data.filter(prod => prod.deposito === 1);
        setProductos(productosFiltrados);
      } catch (error) {
        console.error("Error al obtener los productos:", error);
      }
    };    
    
    fetchProductos();
  }, []);

  const handlePedido = () => {
    if (!vendedorSeleccionado) {
      alert("Debes seleccionar un vendedor");
      return;
    }

    if (pedido.length === 0) {
      alert("Debes seleccionar al menos un artículo.");
      return;
    }

    setPedidosGuardados([...pedidosGuardados, { vendedor: vendedorSeleccionado, articulos: pedido }]);
    setPedido([]);
    setVendedorSeleccionado(null);
  };

  return (
    <div>
      <h1>Generar Pedido</h1>
      <VendedorDropdown 
        setVendedorSeleccionado={setVendedorSeleccionado} 
        vendedorSeleccionado={vendedorSeleccionado}
      />
      <ProductList 
        productos={productos} 
        setPedido={setPedido} 
        pedido={pedido} 
      />
      <button onClick={handlePedido}>Guardar Pedido</button>

      <h2>Pedidos Guardados</h2>
      <PedidoList pedidos={pedidosGuardados} />
    </div>
  );
}

export default App;
