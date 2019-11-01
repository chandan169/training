using System;
using System.Linq;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace TopicSubscriberr
{
    class Program
    {
        static void Main(string[] args)
        {
            
            if(args.Length<2)
            {
                Console.WriteLine("Invalid number of arguments");
                return;
            }
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "topic-exch",
                type: ExchangeType.Topic, durable: false, autoDelete: false, arguments: null);
            channel.QueueDeclare(args[0], durable: false, exclusive: false, autoDelete: false);

            var keys = args.Skip(1).Take(args.Length - 1);
            foreach(var key in keys)
            {
                channel.QueueBind(args[0], "topic-exch", key, null);
            }

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
              {
                  var message = Encoding.UTF8.GetString(ea.Body);
                  Console.WriteLine($"Message received:{message}");
              };
            channel.BasicConsume(args[0], true, consumer);
            Console.WriteLine("waiting for message.... press enter to exit");
            Console.ReadLine();

            connection.Dispose();
            channel.Dispose();
        }
    }
}
