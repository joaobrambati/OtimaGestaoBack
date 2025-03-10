using Application.Common;
using Infrastructure.Context;
using MediatR;

namespace Application.Produtos.Commands;

public class ProdutoExcluirCommand : IRequest<Response<bool>>
{
    public int Id { get; set; }
}

public class ProdutoExcluirCommandHandler : IRequestHandler<ProdutoExcluirCommand, Response<bool>>
{
    private readonly DataContext _context;

    public ProdutoExcluirCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(ProdutoExcluirCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var produto = await _context.Produtos.FindAsync(request.Id);

            if (produto is null)
                return new Response<bool> { Data = false, Status = false, Message = "Produto não encontrado" };

            _context.Produtos.Remove(produto);
            var excluiu = await _context.SaveChangesAsync(cancellationToken);

            if (excluiu > 0)
                return new Response<bool> { Data = true, Status = true, Message = "Produto excluído com sucesso" };

            return new Response<bool> { Data = false, Status = false, Message = "Erro ao excluir o produto" };

        }
        catch (Exception ex)
        {
            return new Response<bool> { Status = false, Message = ex.Message }; 
        }
    }

}

