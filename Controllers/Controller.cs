using Microsoft.AspNetCore.Mvc; 
using Dapper;
using MySqlConnector;


namespace APIBancoDeDado.Controllers;

[ApiController]
[Route("[controller]")]
public class Controller : ControllerBase
{
    private readonly ILogger<Controller> _logger;
    private readonly IConfiguration _config;

    public Controller(ILogger<Controller> logger, IConfiguration configuration)
    {
        _logger = logger;
        _config = configuration;
    }

    [HttpGet(Name = "GET")]
    public IEnumerable<Usuario> Get()
    {
        using (var connection = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                return connection.Query<Usuario>("SELECT * FROM TB_USUARIO");
            }
    }

    [HttpPost(Name = "POST")]
    public async Task<IActionResult> Post( [FromBody] Usuario usuario){

        if (usuario == null)
            {
                return BadRequest("Invalid data");
            }

            using (var connection = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync("INSERT INTO TB_USUARIO (CPF, NOME, DATA_NASCIMENTO) VALUES (@Value1, @Value2, @Value3)", new { Value1 = usuario.CPF, Value2 = usuario.Nome, Value3 = usuario.DataDeNascimento});
            }

            return Ok("Data inserted successfully");

    }
}