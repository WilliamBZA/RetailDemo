using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace ClientUI
{
    #region PlaceOrderHandler

    public class PlaceOrderHandler :
        IHandleMessages<PlaceOrder>
    {
        static ILog log = LogManager.GetLogger<PlaceOrderHandler>();

        public void Handle(PlaceOrder message)
        {
            log.Info($"Received PlaceOrder, OrderId = {message.OrderId}, OrderText = {message.OrderText}");
        }
    }

    #endregion
}