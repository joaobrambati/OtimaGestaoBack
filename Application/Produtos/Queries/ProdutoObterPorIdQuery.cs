using Application.Common;
using Application.Produtos.DTO;
using Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Produtos.Queries;

public class ProdutoObterPorIdQuery : IRequest<Response<ProdutoDto>>
{
    public int Id { get; set; }
}

public class ProdutoObterPorIdQueryHandler : IRequestHandler<ProdutoObterPorIdQuery, Response<ProdutoDto>>
{
    private readonly DataContext _context;

    public ProdutoObterPorIdQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Response<ProdutoDto>> Handle(ProdutoObterPorIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var produto = await _context.Produtos
                .Where(p => p.Id == request.Id)
                .Select(p => new ProdutoDto
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Descricao = p.Descricao,
                    Preco = p.Preco,
                    Estoque = p.Estoque,
                    DataCadastro = p.DataCadastro
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (produto is null)
                return new Response<ProdutoDto> { Status = false, Message = "Produto não encontrado" };

            return new Response<ProdutoDto> { Data = produto, Status = true, Message = "Produto exibido com sucesso" };
        }
        catch (Exception ex)
        {
            return new Response<ProdutoDto> { Status = false, Message = ex.Message };
        }
    }

}
