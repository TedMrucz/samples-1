using System;
using System.IO;
using System.Xml.Serialization;
using Microsoft.AspNet.SignalR.Client;
using SignalRCommon;

namespace SignalRClient
{
    class Program
    {
        static void Main(string[] args)
        {
            IHubProxy _hub;
            string url = @"http://localhost:8080/";
            var connection = new HubConnection(url);
            _hub = connection.CreateHubProxy("TestHub");
            connection.Start().Wait();

            //_hub.On("ReceiveLength", x => Console.WriteLine(x));
            _hub.On("PassingMessage", x => Console.WriteLine(x));
            _hub.On("ReceiveData", d => Console.WriteLine(d.Name));

            string line = "DataObject";
            //while ((line = System.Console.ReadLine()) != null)
            //{
            //    _hub.Invoke("DetermineLength", line).Wait();
            //}

            while ((line = System.Console.ReadLine()) != null)
            {
                IDataObject dataObject = new DataObject();
                dataObject.Name = line;
                dataObject.Index = 12;
                dataObject.Amount = 123D;

                XmlSerializer xmlSerializer = new XmlSerializer(dataObject.GetType());

                using (StringWriter textWriter = new StringWriter())
                {
                    xmlSerializer.Serialize(textWriter, dataObject);
                    string message = textWriter.ToString();

                    Console.WriteLine("message");

                    _hub.Invoke("PassingMessage", message).Wait();
                }
            }

            Console.Read();
        }
    }
}
