using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Config;
using NServiceBus.Config.ConfigurationSource;
//using NServiceBus.Configuration.AdvancedExtensibility;
using NServiceBus.Logging;

namespace ClientUI
{
    class asdasd :
        IProvideConfiguration<MessageForwardingInCaseOfFaultConfig>
    {
        public MessageForwardingInCaseOfFaultConfig GetConfiguration()
        {
            var a = new MessageForwardingInCaseOfFaultConfig();
            a.ErrorQueue = "error";
            return a;
        }
    }

    class Program
    {
        static async Task Main()
        {
            Console.Title = "ClientUI";

            var endpointConfiguration = new BusConfiguration();
            endpointConfiguration.EndpointName("ClientUI");
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.UseSerialization<XmlSerializer>().DontWrapRawXml();
            var transport = endpointConfiguration.UseTransport<MsmqTransport>();

            var endpointInstance = Bus.Create(endpointConfiguration);
            endpointInstance.Start();

            await RunLoop(endpointInstance)
                .ConfigureAwait(false);
        }

        #region RunLoop

        static ILog log = LogManager.GetLogger<Program>();

        static async Task RunLoop(IStartableBus endpointInstance)
        {
            while (true)
            {
                log.Info("Press 'P' to place an order, or 'Q' to quit.");
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.P:
                        // Instantiate the command
                        var command = new PlaceOrder
                        {
                            OrderId = Guid.NewGuid().ToString(),
                            OrderText = "First line\r\nSecond line"
                        };

                        // Send the command to the local endpoint
                        log.Info($"Sending PlaceOrder command, OrderId = {command.OrderId}");
                        endpointInstance.SendLocal(command);

                        break;

                    case ConsoleKey.Q:
                        return;

                    default:
                        log.Info("Unknown input. Please try again.");
                        break;
                }
            }
        }

        #endregion
    }
}