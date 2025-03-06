using Domain.Enums;

namespace Domain.Entities
{
    public class Pedido
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public List<PedidoItem> Itens { get; set; }
        public PedidoStatus Status { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
