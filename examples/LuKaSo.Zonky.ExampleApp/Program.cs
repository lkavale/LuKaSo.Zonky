﻿using log4net;
using LuKaSo.Zonky.Client;
using System;

namespace LuKaSo.Zonky.ExampleApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            var reader = new SecretsJsonReader();
            var client = new ZonkyClient(reader.Read(), true);

            var timer1 = new System.Timers.Timer(30000);
            timer1.Elapsed += (s, e) =>
            {
                client.GetInvestmentsAsync(0, 1).GetAwaiter().GetResult();
            };
            timer1.Start();

            var timer2 = new System.Timers.Timer(10000);
            timer2.Elapsed += (s, e) =>
            {
                client.GetAllUncoveredPrimaryMarketPlaceAsync().GetAwaiter().GetResult();
            };
            timer2.Start();

            var timer3 = new System.Timers.Timer(20000);
            timer3.Elapsed += (s, e) =>
            {
                client.GetAllSecondaryMarketplaceAsync().GetAwaiter().GetResult();
            };
            timer3.Start();

            Console.WriteLine("Press ENTER to exit...");
            Console.ReadLine();

            timer1.Stop();
            timer1.Dispose();

            timer2.Stop();
            timer2.Dispose();

            timer3.Stop();
            timer3.Dispose();

            client.Dispose();
        }
    }
}
