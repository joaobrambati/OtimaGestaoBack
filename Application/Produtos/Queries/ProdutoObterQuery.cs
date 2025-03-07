using Application.Common;
using Application.Produtos.DTO;
using Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Produtos.Queries;

public class ProdutoObterQuery : IRequest<Response<List<ProdutoDto>>>
{
}

public class ProdutoObterQueryHandler : IRequestHandler<ProdutoObterQuery, Response<List<ProdutoDto>>>
{
    private readonly DataContext _context;

    public ProdutoObterQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Response<List<ProdutoDto>>> Handle(ProdutoObterQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var produtos = await _context.Produtos
                .Select(p => new ProdutoDto
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Descricao = p.Descricao,
                    Preco = p.Preco,
                    Estoque = p.Estoque,
                    DataCadastro = p.DataCadastro
                })
            .ToListAsync();

            if (!produtos.Any())
                return new Response<List<ProdutoDto>> { Status = false, Message = "Lista de Produtos vazia" };

            return new Response<List<ProdutoDto>> { Data = produtos, Status = true, Message = "Produtos listados com sucesso" };

        }
        catch (Exception ex)
        {
            return new Response<List<ProdutoDto>> { Status = false, Message = ex.Message };
        }
    }
}



