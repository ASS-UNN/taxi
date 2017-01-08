using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Client;
using Xamarin.Forms;

namespace CustomerGraphicsInterface
{
    public class App : Application
    {
        public App()
        {
            var client = ScsServiceClientBuilder.CreateClient<ITaxiService>(new ScsTcpEndPoint("95.79.210.235", 4040));
            client.Connect();
            MainPage = new NavigationPage(new MainPage(client));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
