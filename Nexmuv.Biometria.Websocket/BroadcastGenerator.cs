﻿using Microsoft.AspNet.SignalR.Client;
using System;

namespace Nexmuv.Biometria.Websocket
{
    public class BroadcastGenerator
    {
        public static void Run()
        {
            const string url = @"http://localhost:8585/";
            var connection = new HubConnection(url);
            var hub = connection.CreateHubProxy("BroadcastHub");
            connection.Start().Wait();

            // TimeNow
            hub.Invoke("TimeNow").Wait();

            // BrokerStatus
            var brokerData = BrokerAnalyzer();
            hub.Invoke("BrokerStatus", brokerData.Value, brokerData.Color).Wait();

        }

        private static BrokerData BrokerAnalyzer()
        {
            var brokerData = new BrokerData();
            var rnd = new Random();

            brokerData.Value = rnd.Next(-10, 10);
            brokerData.Color = brokerData.Value >= 0 ? "blue" : "red";

            return brokerData;
        }
    }

    public class BrokerData
    {
        public string Color { get; set; }
        public int Value { get; set; }
    }
}
