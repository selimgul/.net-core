using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;

namespace RabbitMQProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                int messageID = 0;
                string message;
                byte[] body;

                while (true)
                {
                    messageID++;

                    message = String.Format("Hello World!-{0}", messageID);

                    body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);

                    Console.WriteLine(" [x] Sent {0}", message);

                    Thread.Sleep(500);
                }

            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
