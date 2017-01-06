using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Client;
using TestingSample;

namespace DriverPlaceholder
{
    class Program
    {
        class Menu
        {
            protected string[] entries;
            public void Print()
            {
                for (int i = 0; i < entries.Length; i++)
                    Console.WriteLine((i+1).ToString() + ". " + entries[i]);
                Console.Write("> ");
            }
        }
        class MainMenu : Menu
        {
            public MainMenu()
            {
                entries = new string[]{ "Register", "Log in" };
            }
        }
        class DriverMenu : Menu
        {
            public DriverMenu()
            {
                entries = new string[]{ "Get Available Orders", "Take Order", "Update Position", "Complete Order" };
            }
        }

        static void Main(string[] args)
        {
            var clientService = ScsServiceClientBuilder.CreateClient<ITaxiService>(new ScsTcpEndPoint("127.0.0.1", 4040));
            clientService.Connect();

            var service = clientService.ServiceProxy;

            Console.WriteLine("Welcome to Driver Placeholder Application!\n" +
                "Please, use an appropriate mobile application whenever possible to avoid errors.\n");

            string input = "";
            Menu menu = new MainMenu();
            bool success = false;

            while (success == false)
            {
                Console.Clear();
                menu.Print();
                input = Console.ReadLine();

                if (input == "1") //register
                {
                    Console.Write("Login: ");
                    string login = Console.ReadLine();
                    Console.Write("Password: ");
                    string password = Console.ReadLine();
                    Console.Write("Phone: ");
                    string phone = Console.ReadLine();
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    int id = service.RegisterNewDriver(name, phone, login, password);
                    if (id < 0)
                    {
                        Console.WriteLine("Error occured, please try again.");
                    }
                    else
                        success = true;
                }
                if (input == "2") //login
                {
                    Console.Write("Login: ");
                    string login = Console.ReadLine();
                    Console.Write("Password: ");
                    string password = Console.ReadLine();
                    int id = service.LogDriverIn(login, password);
                    if (id < 0)
                    {
                        Console.WriteLine("Error occured, please try again.");
                    }
                    else
                        success = true;
                }
            }
            //We are successfully registered or logged in
            menu = new DriverMenu();
            double posx = 56.299520;
            double posy = 43.982913;
            service.UpdateDriverPosition(Convert.ToString(posx, CultureInfo.InvariantCulture), Convert.ToString(posy, CultureInfo.InvariantCulture));

            while (true)
            {//menu cycle
                Console.Clear();
                menu.Print();

                input = Console.ReadLine();

                if (input == "1") //list all orders
                {
                    var orders = service.GetAvailableOrders();
                    int ordCount = orders.Count;
                    for (int i = 0; i < ordCount; i++)
                    {
                        Console.WriteLine("Order #" + orders[i].Item1 + "\n\tFrom (" + orders[i].Item2 + ", " + orders[i].Item3 + ")");
                        Console.WriteLine("\n\tTo (" + orders[i].Item4 + ", " + orders[i].Item5 + ")");
                    }
                    Console.ReadLine();
                }
                else if (input == "2") //take order
                {
                    Console.Write("Take Order # ");
                    input = Console.ReadLine();
                    int orderNum = Convert.ToInt32(input);
                    if (service.TakeOrder(orderNum))
                    {                        
                        Console.WriteLine("You are working with order #" + orderNum);
                        Console.WriteLine("Press Return to continue...");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Error occured. Please try again...");
                        Console.ReadLine();
                    }
                }
                else if (input == "3") //update position
                {
                    Console.WriteLine("Update position with dx and dy.");
                    Console.Write("> ");
                    input = Console.ReadLine();
                    posx += Convert.ToDouble(input);
                    Console.Write("> ");
                    input = Console.ReadLine();
                    posy += Convert.ToDouble(input);
                    service.UpdateDriverPosition(Convert.ToString(posx, CultureInfo.InvariantCulture), Convert.ToString(posy, CultureInfo.InvariantCulture));
                    Console.WriteLine("Position updated to " + posx + " and " + posy);
                    Console.WriteLine("Press Return to continue...");
                    Console.ReadLine();
                }
                else if (input == "4")//complete order
                {
                    Console.WriteLine("Press Return to Complete Order");
                    Console.ReadLine();
                    service.MarkOrderComplete();
                }
            }//menu cycle

        }
    }
}
