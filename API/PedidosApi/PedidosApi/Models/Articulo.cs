namespace pedidosApi.Models
{
    public class Pedido
    {
        public int VendedorId { get; set; }
        public List<Articulo> Articulos { get; set; }
    }
    public class Articulo
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Deposito { get; set; }
    }
    public class Vendedor
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }
}
