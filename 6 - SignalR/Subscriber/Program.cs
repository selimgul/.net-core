using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Subscriber");

            HubConnection conn = new HubConnectionBuilder()
                 .WithUrl("http://localhost:5000/QuoteHub")                 
                 .Build();

            conn.StartAsync().ContinueWith(t => {
                if (t.IsFaulted)
                    Console.WriteLine(t.Exception.GetBaseException());
                else
                    Console.WriteLine("Connected to Hub");

            }).Wait();

            conn.On<string, string>("ReceiveMessage", (user, message) => {
                Console.WriteLine(user +  " => " + message);
            });

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
            conn.DisposeAsync().ContinueWith(t => {
                if (t.IsFaulted)
                    Console.WriteLine(t.Exception.GetBaseException());
                else
                    Console.WriteLine("Disconnected");
            });
        }
    }
}
