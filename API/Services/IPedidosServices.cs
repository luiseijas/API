using API.Models;
using System.Globalization;

namespace API.Services
{
    public interface IPedidosServices
    {
        Task<Pedido> CrearPedido(Pedido pedido);
        List<Pedido> ObtenerPedidos();

    }
}
