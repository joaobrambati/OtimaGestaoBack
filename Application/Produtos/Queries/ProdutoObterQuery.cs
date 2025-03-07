using Application.Common;
using Domain.Entities;
using Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Produtos.Queries;

public class ProdutoObterQuery : IRequest<Response<List<Produto>>> 
{
}

public class ProdutoObterQueryHandler : IRequestHandler<ProdutoObterQuery, Response<List<Produto>>>
{
    private readonly DataContext _context;

    public ProdutoObterQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Response<List<Produto>>> Handle(ProdutoObterQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var produtos = await _context.Produtos.ToListAsync();

            if (!produtos.Any())
                return new Response<List<Produto>> { Status = false, Message = "Lista de Produtos vazia" };

            return new Response<List<Produto>> { Data = produtos, Status = true, Message = "Produtos listados com sucesso" };

        }
        catch (Exception ex)
        {
            return new Response<List<Produto>> { Status = false, Message = ex.Message };
        }
    }
}



