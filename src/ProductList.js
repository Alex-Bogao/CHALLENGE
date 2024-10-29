import React from "react";

const ProductList = ({ productos, setPedido, pedido }) => {
  const handleCheckboxChange = (producto) => {
    // Eliminar caracteres especiales de la descripción
    const descripcionLimpia = producto.descripcion.replace(/[^a-zA-Z0-9\s]/g, "");

    // Validar si el precio es 0
    if (producto.precio === 0) {
      alert(`El artículo ${descripcionLimpia} no es válido porque su precio es 0.`);
      return; // Impedir que se añada el artículo con precio 0
    }

    const isInPedido = pedido.some((item) => item.codigo === producto.codigo);

    if (isInPedido) {
      setPedido(pedido.filter((item) => item.codigo !== producto.codigo));
    } else {
      const articuloConDescripcionLimpia = {
        ...producto,
        descripcion: descripcionLimpia, 
      };
      setPedido([...pedido, articuloConDescripcionLimpia]);
    }
  };

  return (
    <div>
      <h2>Lista de Productos</h2>
      <ul>
        {productos.map((producto) => {
          // Eliminar caracteres especiales de la descripción para mostrarlos en la lista
          const descripcionLimpia = producto.descripcion.replace(/[^a-zA-Z0-9\s]/g, "");

          return (
            <li key={producto.codigo}>
              <input
                type="checkbox"
                checked={pedido.some((item) => item.codigo === producto.codigo)}
                onChange={() => handleCheckboxChange(producto)}
              />
              {descripcionLimpia} - ${producto.precio}
            </li>
          );
        })}
      </ul>
    </div>
  );
};

export default ProductList;

