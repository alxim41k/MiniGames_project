using System;
using Network;
using ServerClient;
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = int.Parse(Console.ReadLine());
            if (a == 1)
            {
                var s = new Server();
                s.Main();
            }
            else
            {
                var c = new Client();
                c.Main();
            }
        }
    }
}

