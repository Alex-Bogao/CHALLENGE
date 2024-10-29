using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using pedidosApi.Models;

namespace pedidosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendedoresController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetVendedores()
        {
            // Ruta del archivo JSON
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "vendedores_challenge.json");

            // Verificar si el archivo existe
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("El archivo JSON no existe.");
            }

            var json = System.IO.File.ReadAllText(filePath);

            try
            {
                var vendedoresRoot = JsonSerializer.Deserialize<List<Vendedor>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return Ok(vendedoresRoot);
            }
            catch (JsonException ex)
            {
                return BadRequest($"Error deserializando el archivo JSON: {ex.Message}");
            }
        }
    }
}
