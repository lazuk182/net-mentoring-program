using CartingService.BLL.Services;
using CartingService.DAL;
using CartingService.DAL.Database;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using System.Text;
using System.Runtime;
using System.Threading.Channels;
using System;

namespace CartingService.API
{
    public class MessageConsumer :BackgroundService
    {
        private ICartService? cartService;
        private IConnection _connection;
        private IModel _channel;
        public MessageConsumer(LiteDB.LiteDatabase Database) 
        {
            ICartData cartData = new CartLiteDb(Database);
            cartService = new CartService(cartData);
            InitRabbitMQ();
        }

        private void InitRabbitMQ()
        {
            var factory = new ConnectionFactory 
            { 
                HostName = "localhost",
                UserName = "laz",
                Password = "adrian"
            };

            // create connection  
            _connection = factory.CreateConnection();

            // create channel  
            _channel = _connection.CreateModel();
            _channel.QueueDeclare("product", exclusive: false);

            //_channel.ExchangeDeclare("demo.exchange", ExchangeType.Topic);
            //_channel.QueueDeclare("demo.queue.log", false, false, false, null);
            //_channel.QueueBind("demo.queue.log", "demo.exchange", "demo.queue.*", null);
            //_channel.BasicQos(0, 1, false);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, eventArgs) =>
            {
                // received message  
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var jsonSpecified = Encoding.UTF8.GetString(eventArgs.Body.Span);

                // handle the received message  
                var item = JsonConvert.DeserializeObject<DAL.Models.Item>(jsonSpecified, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                cartService?.UpdateItemInformation(item);
                Console.WriteLine($"Product message received: {message}");
            };

            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume(queue: "product", autoAck: true, consumer: consumer);
            return Task.CompletedTask;
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }
        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
