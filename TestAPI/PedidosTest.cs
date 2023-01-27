using API.Controllers;
using API.Data;
using API.Entities;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Moq;
using MySqlX.XDevAPI;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace TestAPI
{
    public class PedidosTest
    {
        class TestApi : WebApplicationFactory<Program>
        {
            protected override IHost CreateHost(IHostBuilder builder)
            {
                builder.ConfigureServices(serviceCollection =>
                {
                    serviceCollection
                        .AddScoped<IPedidosServices, PedidosServices>();
                });
                return base.CreateHost(builder);
            }
        }

        [Fact]
        public async Task RegistrarPedido_ReturnsPedido()
        {
            TestApi application = new();

            Pedido pedido = new Pedido
            {
                Fecha = DateTime.Now,
                Total = 15,
                Estado = "Pagado",
                Usuario = "Luis",
                Direccion = "A Coruna, Spain",
                Detalles = new List<DetallePedido>
                {
                    new DetallePedido { Id = 1,Producto="Menu 6",Precio=15,Cantidad=1}
                }
            };
            string serializedPedido = JsonSerializer.Serialize(pedido);
            using (HttpClient client = application.CreateClient())
            {
                var res = await client.PostAsync("/Pedidos/RegistrarPedido", new StringContent(serializedPedido, Encoding.UTF8, "application/json"));
                res.EnsureSuccessStatusCode();
                Assert.True(res.IsSuccessStatusCode);
            }
        }

        [Fact]
        public async Task ObtenerClimalocalidad_returnClima()
        {
            TestApi application = new();
            using (HttpClient client = application.CreateClient())
            {
                string localidad = "A Coruna, Spain";
                var res = await client.GetAsync($"/Pedidos/ObtenerClima/{localidad}");
                res.EnsureSuccessStatusCode();    
                Assert.True(res.IsSuccessStatusCode);
            }
        }
    }
}