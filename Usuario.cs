namespace APIBancoDeDado;
using Dapper;

public class Usuario
{
    public DateOnly DataDeNascimento { get; set; }

    public string? Nome { get; set; }
    public string? CPF { get; set; }
}
