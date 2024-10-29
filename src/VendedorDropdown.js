import React, { useEffect, useState } from "react";
import axios from "axios";

const VendedorDropdown = ({ setVendedorSeleccionado, vendedorSeleccionado }) => {
  const [vendedores, setVendedores] = useState([]);

  useEffect(() => {
    const fetchVendedores = async () => {
      try {
        const response = await axios.get("https://localhost:7225/api/Vendedores");
        setVendedores(response.data);
      } catch (error) {
        console.error("Error al obtener los vendedores:", error);
      }
    };

    fetchVendedores();
  }, []);

  const handleChange = (event) => {
    setVendedorSeleccionado(event.target.value); 
  };

  return (
    <div>
      <label htmlFor="vendedor">Selecciona un Vendedor:</label>
      <select id="vendedor" value={vendedorSeleccionado || ""} onChange={handleChange}>
        <option value="">Seleccionar...</option>
        {vendedores.map((vendedor) => (
          <option key={vendedor.id} value={vendedor.id}>
            {vendedor.descripcion}
          </option>
        ))}
      </select>
    </div>
  );
};

export default VendedorDropdown;
