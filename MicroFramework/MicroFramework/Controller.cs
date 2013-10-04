using System;
using Microsoft.SPOT;

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
            string response = "HTTP/1.1 200 OK\r\nContent-Type: text/html; charset=utf-8\r\n\r\n<html><head><title>WebServer</title></head><body>" + driver.GetData() + "</body></html>";
            return response;
        }
    }
}
