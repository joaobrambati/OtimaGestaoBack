namespace Application.Produtos.DTO;
public class ProdutoDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
    public DateTime DataCadastro { get; set; }
}
