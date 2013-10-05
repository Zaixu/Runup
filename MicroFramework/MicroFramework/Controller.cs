using System;
using Microsoft.SPOT;
using System.Collections;

namespace MicroFramework
{
    class Controller : IController
    {
        IDriver driver;

        public Controller(IDriver dataDriver)
        {
            driver = dataDriver;
        }

        public string Handler(string request)
        {
            int getIndex = request.IndexOf("GET");
            int httpIndex = request.IndexOf("HTTP");

            string completeRequest = "";
            if (getIndex != -1 && httpIndex != -1)
                completeRequest = request.Substring(getIndex + 4, httpIndex - (getIndex + 5));

            string response = "";
            if (completeRequest == "/List")
            {
                Queue queueClone = (Queue)((ManagedQueue)driver.GetData()).GetQueue();

                foreach (LM75Driver.QueueClass value in queueClone)
                {
                    response = response + "Date: " + value.datetime.ToString();
                    response = response + " - Temperature: " + value.temperature.ToString() + "\n\r";
                }
            }
            else
            {
                LM75Driver.QueueClass temp = ((LM75Driver.QueueClass)((ManagedQueue)driver.GetData()).Newest());
                response = "Date: " + temp.datetime.ToString() + "\n\r" + "Temperature: " + temp.temperature.ToString();
            }

            
            string fullResponse = "<html><head><title>WebServer</title></head><body>" + response +"</body></html>";
            
            return response;
        }
    }
}
