using Application.Common;
using Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Produtos.Commands;

public class ProdutoEditarCommand : IRequest<Response<bool>>
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public decimal? Preco { get; set; }
    public int? Estoque { get; set; }
}

public class ProdutoEditarCommandHandler : IRequestHandler<ProdutoEditarCommand, Response<bool>>
{
    private readonly DataContext _context;

    public ProdutoEditarCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(ProdutoEditarCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var produto = await _context.Produtos
                .FirstOrDefaultAsync(p => p.Id == request.Id);

            if (produto is null)
                return new Response<bool> { Data = false , Status = false, Message = "Produto não encontrado" };

            if (!string.IsNullOrWhiteSpace(request.Nome))
                produto.Nome = request.Nome;

            if (!string.IsNullOrWhiteSpace(request.Descricao))
                produto.Descricao = request.Descricao;

            if (request.Preco.HasValue)
                produto.Preco = request.Preco.Value;

            if (request.Estoque.HasValue)
                produto.Estoque = request.Estoque.Value;


            _context.Produtos.Update(produto);
            var editou = await _context.SaveChangesAsync(cancellationToken);

            if (editou > 0)
                return new Response<bool> { Data = true, Status = true, Message = "Produto atualizado com sucesso" };

            return new Response<bool> { Data = false, Status = false, Message = "Erro ao atualizar o produto" };

        }
        catch (Exception ex)
        {
            return new Response<bool> { Status = false, Message = ex.Message };
        }
    }

}
