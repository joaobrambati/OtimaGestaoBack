using Application.Common;
using Domain.Entities;
using Infrastructure.Context;
using MediatR;

namespace Application.Produtos.Commands;

public class ProdutoCriarCommand : IRequest<Response<bool>>
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
}

public class ProdutoCriarCommandHandler : IRequestHandler<ProdutoCriarCommand, Response<bool>>
{
    private readonly DataContext _context;

    public ProdutoCriarCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(ProdutoCriarCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Cria um novo produto a partir dos dados enviados
            var produto = new Produto
            {
                Nome = request.Nome,
                Descricao = request.Descricao,
                Preco = request.Preco,
                Estoque = request.Estoque,
                DataCadastro = DateTime.Now
            };

            _context.Produtos.Add(produto);
            var criou = await _context.SaveChangesAsync(cancellationToken);

            if (criou > 0)
                return new Response<bool> { Data = true, Status = true, Message = "Produto criado com sucesso" };

            return new Response<bool> { Data = false, Status = false, Message = "Erro ao criar o produto" };

        }
        catch (Exception ex)
        {
            return new Response<bool>
            {
                Status = false,
                Message = ex.Message
            };
        }
    }
}
