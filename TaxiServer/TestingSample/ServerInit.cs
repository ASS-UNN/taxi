using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Service;

namespace TestingSample
{
    class ServerInit
    {
        static void Main(string[] args)
        {
            var serviceApplication = ScsServiceBuilder.CreateService(new ScsTcpEndPoint(4040));

            serviceApplication.AddService<ITaxiService, TaxiService>(new TaxiService());

            serviceApplication.Start();

            Console.WriteLine("Server started... Push enter for stop.");
            Console.ReadLine();

            serviceApplication.Stop();
        }
    }
}
