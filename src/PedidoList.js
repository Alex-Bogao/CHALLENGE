import React from "react";

const PedidoList = ({ pedidos }) => {
  return (
    <div>
      {pedidos.length === 0 ? (
        <p>No hay pedidos guardados.</p>
      ) : (
        <ul>
          {pedidos.map((pedido, index) => (
            <li key={index}>
              <strong>Vendedor:</strong> {pedido.vendedor}
              <ul>
                {pedido.articulos.map((articulo) => (
                  <li key={articulo.codigo}>
                    {articulo.descripcion} - Precio: ${articulo.precio}
                  </li>
                ))}
              </ul>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default PedidoList;
