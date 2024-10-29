using Microsoft.AspNetCore.Mvc;
using pedidosApi.Models;
using Newtonsoft.Json;
using System.Text.Json;

[ApiController]
[Route("api/[controller]")]
public class ArticulosController : ControllerBase
{
    [HttpGet]
    public IActionResult GetArticulos()
    {
        // Ruta del archivo JSON
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "articulos_challenge.json");

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound("El archivo JSON no existe.");
        }
        var json = System.IO.File.ReadAllText(filePath);

        try
        {

            var articulosNew = JsonConvert.DeserializeObject<List<Articulo>>(json);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        var articulos = System.Text.Json.JsonSerializer.Deserialize<List<Articulo>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return Ok(articulos);
    }
}


