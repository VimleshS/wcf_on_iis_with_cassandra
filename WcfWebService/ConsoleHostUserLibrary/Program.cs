using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfWebService;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Description;

namespace ConsoleHostUserLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            WebServiceHost webhost =  new WebServiceHost(typeof(UserService));
            try
            {
                webhost.Open();
                PrintServiceInfo(webhost);
                Console.ReadLine();
                webhost.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                webhost.Abort();
            }
        }
        static void PrintServiceInfo(ServiceHost host)
        {
            Console.WriteLine("{0} is up and running with these endpoints:",
                host.Description.ServiceType);
            foreach (ServiceEndpoint se in host.Description.Endpoints)
                Console.WriteLine(se.Address);
        }
    }
}
