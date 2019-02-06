using NServiceBus;

namespace Messages
{
    public class PlaceOrder :
        ICommand
    {
        public string OrderId { get; set; }
        public string OrderText { get; set; }
    }
}