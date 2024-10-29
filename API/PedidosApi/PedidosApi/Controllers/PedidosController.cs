using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using pedidosApi.Models;

namespace pedidosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly string _jsonFilePath;

        public PedidosController()
        {
            // Ruta al archivo JSON que contiene los artículos
            _jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "articulos_challenge.json");
        }

        // Endpoint para obtener los artículos
        [HttpGet("articulos")]
        public IActionResult GetArticulos()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "articulos_challenge.json");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("El archivo JSON no existe.");
            }

            var json = System.IO.File.ReadAllText(filePath);

            try
            {
                var articulosRoot = JsonSerializer.Deserialize<List<Articulo>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                // Filtrar los artículos que pertenecen al Depósito 1
                var articulosFiltrados = articulosRoot.Where(a => a.Deposito == 1).ToList();

                return Ok(articulosFiltrados);
            }
            catch (JsonException ex)
            {
                return BadRequest($"Error deserializando el archivo JSON: {ex.Message}");
            }
        }

       



        // Endpoint para recibir un pedido
        [HttpPost("guardar-pedido")]
        public IActionResult GuardarPedido([FromBody] Pedido pedido)
        {
            // Validar que se haya seleccionado al menos un artículo
            if (pedido.Articulos == null || !pedido.Articulos.Any())
            {
                return BadRequest("Debe seleccionar al menos un artículo.");
            }

            // Validar cada artículo
            foreach (var articulo in pedido.Articulos)
            {
                if (articulo.Precio <= 0)
                {
                    return BadRequest($"El artículo {articulo.Descripcion} tiene un precio inválido.");
                }

                if (HasSpecialCharacters(articulo.Descripcion))
                {
                    return BadRequest($"La descripción del artículo {articulo.Descripcion} contiene caracteres especiales no permitidos.");
                }
            }

            return Ok("Pedido guardado exitosamente.");
        }

        // Método auxiliar para validar si una descripción contiene caracteres especiales
        private bool HasSpecialCharacters(string input)
        {
            return input.Any(ch => !char.IsLetterOrDigit(ch) && !char.IsWhiteSpace(ch));
        }
    }
}
