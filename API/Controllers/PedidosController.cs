using API.Models;
using API.Services;
using API.Utils;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidosServices pedidosServices;
        public PedidosController(IPedidosServices pedidosServices)
        {
            this.pedidosServices = pedidosServices;
        }

        [Route("ObtenerPedido")]
        [HttpGet]
        public ActionResult<List<Pedido>> ObtenerPedido()
        {

            var pedido = pedidosServices.ObtenerPedidos();
            if (pedido == null)
            {
                return NotFound($"No existen pedidos");
            }
            return pedido;
        }

        [Route("RegistrarPedido")]
        [HttpPost]
        public async Task<ActionResult<Pedido>> RegistrarPedido([FromBody] Pedido pedido)
        {
            var climaPedido = await Utiles.ObtenerClimalocalidad(pedido.Direccion);
            if (climaPedido != null) {
                pedido.Clima = climaPedido;
            }
            await pedidosServices.CrearPedido(pedido);
            return pedido;
        }

        [Route("ObtenerClima/{localidad}")]
        [HttpGet]
        public async Task<ActionResult<Clima>> GetClima(string localidad)
        {
            var clima = await Utiles.ObtenerClimalocalidad(localidad);
            if (clima == null)
            {
                return BadRequest($"No se pido obtener en clima para la localidad de {localidad}");
            }
            else {
                return Ok(clima);
            }
        }

        
    }
}
