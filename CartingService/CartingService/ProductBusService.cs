using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace CartingService.API
{
    internal class ProductBusService : IDisposable
    {
        private const string QueueName = "Products"; 
        private readonly IModel _channel; 
        //private readonly IApplicationDbContext _context; 
        public ProductBusService(IModel channel/*, IApplicationDbContext context*/) 
        { 
            ArgumentNullException.ThrowIfNull(channel, nameof(channel)); 
            //ArgumentNullException.ThrowIfNull(context, nameof(context)); 
            _channel = channel; 
            //_context = context; 
        }
        public void Dispose() 
        { 
            _channel.Dispose(); 
        }
        public async Task GetProductMessagesAsync(CancellationToken cancellationToken) 
        { 
            _channel.QueueDeclare(QueueName, true, false, false); 
            var consumer = new AsyncEventingBasicConsumer(_channel); 
            consumer.Received += async (s, e) => 
            { 
                var jsonSpecified = Encoding.UTF8.GetString(e.Body.Span); 
                var item = JsonConvert.DeserializeObject<DAL.Models.Item>(jsonSpecified, new JsonSerializerSettings() 
                { 
                    NullValueHandling = NullValueHandling.Ignore, 
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore 
                }); 
                //await UpdateItemAsync(item, cancellationToken); 
                await Task.Yield(); 
            }; 
            _channel.BasicConsume(QueueName, true, consumer); 
            await Task.Yield(); 
        }
        public async Task SendProductMessageAsync(DAL.Models.Item message) 
        { 
            await Task.Run(() => 
            { 
                _channel.QueueDeclare(QueueName, true, false, false); 
                var properties = _channel.CreateBasicProperties(); 
                properties.Persistent = false; 
                var output = JsonConvert.SerializeObject(message, new JsonSerializerSettings() 
                { 
                    NullValueHandling = NullValueHandling.Ignore, 
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore 
                }); 
                _channel.BasicPublish(string.Empty, QueueName, null, Encoding.UTF8.GetBytes(output)); 
            }); 
        }
        //private async Task UpdateItemAsync(DAL.Models.Item product, CancellationToken cancellationToken) 
        //{ 
        //    var items = await _context.Item.Where(i => i.ExternalId == product.Id).ToListAsync(cancellationToken); 
        //    if (items.Any()) 
        //    { 
        //        items.ForEach(i => { i.Name = product.Name; i.Price = product.Price; }); 
        //    } 
        //    await _context.SaveChangesAsync(cancellationToken); 
        //}
    }
}
