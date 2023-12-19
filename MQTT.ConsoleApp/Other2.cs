//using System;
//using System.Threading.Tasks;
//using MQTTnet;
//using MQTTnet.Client;
//using MQTTnet.Client.Options;
//using MQTTnet.Extensions.ManagedClient;

namespace MQTT.ConsoleApp
{
    internal class Other2
    {
    }

    //static async Task Main(string[] args)
    //{
    //    var factory = new MqttFactory();
    //    var mqttClient = factory.CreateManagedMqttClient();

    //    var options = new ManagedMqttClientOptionsBuilder()
    //        .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
    //        .WithClientOptions(new MqttClientOptionsBuilder()
    //            .WithTcpServer("mqtt.server.com", 1883) // Replace with your MQTT server address and port
    //            .WithClientId("ClientId") // Replace with a client identifier
    //            .Build())
    //        .Build();

    //    await mqttClient.StartAsync(options);

    //    string topic = "your/topic";

    //    await mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic(topic).Build());

    //    mqttClient.UseApplicationMessageReceivedHandler(e =>
    //    {
    //        Console.WriteLine($"Received message: {e.ApplicationMessage.Payload}");
    //    });

    //    string message = "Hello, MQTT!";
    //    await mqttClient.PublishAsync(new MqttApplicationMessageBuilder()
    //        .WithTopic(topic)
    //        .WithPayload(message)
    //        .Build());

    //    await Task.Delay(TimeSpan.FromSeconds(5));

    //    await mqttClient.StopAsync();
    //}

}
