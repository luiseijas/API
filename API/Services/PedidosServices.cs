using API.Data;
using API.Entities;
using API.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Data.Entity;
using System.Globalization;

namespace API.Services
{
    public class PedidosServices : IPedidosServices
    {
        private readonly IMongoCollection<Pedido> _mongoConfig;
        private readonly PedidosContext _mysqlConfig;
        public PedidosServices(IMongoDatabaseSettings settings, IMongoClient mongoDbClient, PedidosContext mysqlConfig)
        {
            var db = mongoDbClient.GetDatabase(settings.DatabaseName);
            _mongoConfig = db.GetCollection<Pedido>(settings.CollectionName);
            _mysqlConfig = mysqlConfig;
        }

        public async Task<Pedido> CrearPedido(Pedido pedido)
        {
            _mongoConfig.InsertOne(pedido);

            Cabecera cabecera = new Cabecera
            {
                Fecha = pedido.Fecha,
                Total = pedido.Total,
                Estado = pedido.Estado,
                Usuario = pedido.Usuario,
                Direccion = pedido.Direccion,
                Humedad = Convert.ToDouble(pedido.Clima.Humedad),
                Temperatura = pedido.Clima.Temperatura
            };

            _mysqlConfig.Cabeceras.Add(cabecera);
            _mysqlConfig.SaveChanges();
            
            List<Detalle> listaDetalles = new List<Detalle>();
            if (pedido.Detalles.Count > 0)
            {
                foreach (var detalle in pedido.Detalles)
                {
                    listaDetalles.Add(new Detalle
                    {
                        IdCabecera = cabecera.Id,
                        Producto = detalle.Producto,
                        Cantidad = detalle.Cantidad,
                        Precio = detalle.Precio
                    });
                }
            }

            _mysqlConfig.Detalles.AddRange(listaDetalles);
            await _mysqlConfig.SaveChangesAsync();

            return pedido;
        }

        public List<Pedido> ObtenerPedidos()
        {
            return _mongoConfig.Find(s => true).ToList();
        }
    }
}
