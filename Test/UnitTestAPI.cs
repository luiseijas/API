using API.Models;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using API.Services;
using System.Net.Http.Json;
using API.Controllers;
using API.Utils;
using Org.BouncyCastle.Utilities;

namespace Test
{
    [TestClass]
    public class UnitTestAPI
    {
        public async Task Test() {
            
        }
        
        class subcriptionAPI : WebApplicationFactory<Program> {
            protected override IHost CreateHost(IHostBuilder builder)
            {
                builder.ConfigureServices(x => x.AddScoped<IPedidosServices>(_ => new TestIPedidosServices()));

                return base.CreateHost(builder);
            }

            public class TestIPedidosServices : IPedidosServices
            {
                public Task<Pedido> CrearPedido(Pedido pedido, bool sandbox)
                {
                    return Task.FromResult(pedido);
                }

                public List<Pedido> ObtenerPedidos()
                {
                    throw new NotImplementedException();
                }
            }
        }


        public async Task RegistrarPedido_ReturnsPedido()
        {
            //Arrange
            var mockUtiles = new Mock<IUtiles>();
            var mockPedidosServices = new Mock<IPedidosServices>();
            var pedido = new Pedido { Direccion = "test address" };
            var climaPedido = new Clima { Temperatura = 20, Humedad = 50 };
            mockUtiles.Setup(x => x.ObtenerClimalocalidad(It.IsAny<string>())).ReturnsAsync(climaPedido);
            mockPedidosServices.Setup(x => x.CrearPedido(It.IsAny<Pedido>(), It.IsAny<bool>())).Returns(Task.CompletedTask);
            var controller = new PedidosController(mockPedidosServices.Object, mockUtiles.Object);
            //Act
            var result = await controller.RegistrarPedido(pedido);
            //Assert
            var actionResult = Assert.IsType<ActionResult<Pedido>>(result);
            var returnValue = Assert.IsType<Pedido>(actionResult.Value);
            Assert.Equal(pedido.Direccion, returnValue.Direccion);
            Assert.Equal(climaPedido.Temperatura, returnValue.Clima.Temperatura);
            Assert.Equal(climaPedido.Humedad, returnValue.Clima.Humedad);
            mockUtiles.Verify(x => x.ObtenerClimalocalidad(It.IsAny<string>()), Times.Once);
            mockPedidosServices.Verify(x => x.CrearPedido(It.IsAny<Pedido>(), It.IsAny<bool>()), Times.Once);
        }
    }
}