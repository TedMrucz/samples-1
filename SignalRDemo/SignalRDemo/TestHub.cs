using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SignalRCommon;

namespace SignalRHub
{
    [HubName("TestHub")]
    public class TestHub : Hub
    {
        public void DetermineLength(string message)
        {
            Console.WriteLine(message);

            string newMessage = string.Format(@"{0} has a length of: {1}", message, message.Length);
            Clients.All.ReceiveLength(newMessage);
        }

        public void PassingDataObject(IDataObject dataObject)
        {
            Console.WriteLine(dataObject.Name);

            dataObject.Name += "- xxxxxx";

            Clients.All.ReceiveData(dataObject);
        }

        public void PassingMessage(string message)
        {
            Console.WriteLine("Hub");
            Console.WriteLine(message);
            Clients.All.ReceiveData(message);
        }
    }
}
