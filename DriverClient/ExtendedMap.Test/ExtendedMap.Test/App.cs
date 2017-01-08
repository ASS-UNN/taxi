using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ExtendedMap.Test
{
    public class App : Application
    {
        public App()
        {
            //DriverController controller = new DriverController();
            DriverPerson driver = new DriverPerson();
            MainPage = new NavigationPage(new WelcomePage(driver));
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
