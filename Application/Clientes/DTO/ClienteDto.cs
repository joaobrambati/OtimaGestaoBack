namespace Application.Clientes.DTO;
public class ClienteDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Endereco { get; set; }
    public DateTime DataCadastro { get; set; }
}
