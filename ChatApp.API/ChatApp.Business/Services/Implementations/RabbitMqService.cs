using ChatApp.Business.Services.Interfaces;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

namespace ChatApp.Business.Services.Implementations
{
    public class RabbitMqService : IRabbitMqService
    {
        public void SendMessage(object obj)
        {
            var message = JsonSerializer.Serialize(obj);
            SendMessage(message);
        }

        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672"),
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "message",
                           durable: true,
                           exclusive: false,
                           autoDelete: false,
                           arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                           routingKey: "message",
                           basicProperties: null,
                           body: body);
        }
    }
}
