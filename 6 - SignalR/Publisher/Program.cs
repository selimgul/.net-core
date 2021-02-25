using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Publisher");

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
            });

            for (int i = 0; i < 1000; i++)
            {
                Random random = new Random();

                string strMessage = Guid.NewGuid().ToString();
                conn.InvokeAsync("SendMessage", "Publisher", strMessage)
                .ContinueWith(t => {
                    Console.WriteLine("Published message => " + strMessage);
                    if (t.IsFaulted)
                        Console.WriteLine(t.Exception.GetBaseException());                    
                });

                Thread.Sleep(5000);
            }

            conn.DisposeAsync().ContinueWith(t => {
                if (t.IsFaulted)
                    Console.WriteLine(t.Exception.GetBaseException());
                else
                    Console.WriteLine("Disconnected");
            });
        }
    }
}
