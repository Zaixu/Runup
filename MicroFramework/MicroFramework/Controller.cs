using System;
using Microsoft.SPOT;
using System.Collections;

namespace MicroFramework
{
    /// <summary>
    /// Controller to handle webserver response/requests
    /// </summary>
    public class Controller : IController
    {
        private IDriver driver;

        /// <summary>
        /// Constructor - Set driver being used
        /// </summary>
        /// <param name="dataDriver"></param>
        public Controller(IDriver dataDriver)
        {
            driver = dataDriver;
        }

        /// <summary>
        /// Handler for webserver requests
        /// </summary>
        /// <param name="request">String of request data from webserver</param>
        /// <returns>Returns response data to webserver</returns>
        public string Handler(string request)
        {
            //Get location of Get and HTTP in request
            int getIndex = request.IndexOf("GET");
            int httpIndex = request.IndexOf("HTTP");

            string completeRequest = "";
            //If its an browser request, get the parameters being called on domain (/List or /Example/example)
            if (getIndex != -1 && httpIndex != -1)
                completeRequest = request.Substring(getIndex + 4, httpIndex - (getIndex + 5));

            string response = "";
            //If the request is a list, get list in drivers queue and output it else just output the most recent one
            if (completeRequest == "/List")
            {
                //Get data queue from driver
                Queue queueClone = (Queue)((ManagedQueue)driver.GetData()).GetQueue();
                //Output each data point in queue to response
                foreach (LM75Driver.QueueClass value in queueClone)
                {
                    response = response + "Date: " + value.datetime.ToString();
                    response = response + " - Temperature: " + value.temperature.ToString() + "\n\r";
                }
            }
            else
            {
                //Get the latest data point and output it to response
                LM75Driver.QueueClass temp = ((LM75Driver.QueueClass)((ManagedQueue)driver.GetData()).Newest());
                response = "Date: " + temp.datetime.ToString() + "\n\r" + "Temperature: " + temp.temperature.ToString();
            }

            //Embed the response in html
            string fullResponse = "<html><head><title>WebServer</title></head><body>" + response +"</body></html>";
            
            return response;
        }
    }
}
