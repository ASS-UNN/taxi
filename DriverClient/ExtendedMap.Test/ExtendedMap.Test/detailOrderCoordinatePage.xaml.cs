using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using System.Globalization;

namespace ExtendedMap.Test
{
    public partial class detailOrderCoordinatePage : ContentPage
    {
        public bool isTakeOrder = false;
        public Pin currentPositionDriver;
        public DriverPerson driver;
        public ExtendedMap map;
        public bool isClose = false;
        public detailOrderCoordinatePage(Tuple<int, string, string, string, string> local, DriverPerson driver)
        {
            
            this.driver = driver;
            this.driver.finishPointOfOrder = new Tuple<double, double>(System.Convert.ToDouble(local.Item4, CultureInfo.InvariantCulture), System.Convert.ToDouble(local.Item5, CultureInfo.InvariantCulture));
            map = new ExtendedMap(
               MapSpan.FromCenterAndRadius(
                   new Position(56.300351, 43.947364), Distance.FromMiles(0.3)))
            {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
 
            Pin startAdress = new Pin
            {
                Position = new Position(System.Convert.ToDouble(local.Item2, CultureInfo.InvariantCulture), System.Convert.ToDouble(local.Item3, CultureInfo.InvariantCulture)),
                Label = "Забрать клиента отсбда"
            };
            Pin finishAdress = new Pin
            {
                Position = new Position(System.Convert.ToDouble(local.Item4, CultureInfo.InvariantCulture), System.Convert.ToDouble(local.Item5, CultureInfo.InvariantCulture)),
                Label = "Отвезти клиента сюда"
            };

           
            currentPositionDriver = new Pin
            {
                Type = PinType.SearchResult,
                Position = new Position(0, 0),
                Label = "Ваше местоположение"
            };
            

            map.Pins.Add(startAdress);
            map.Pins.Add(finishAdress);
            map.Pins.Add(currentPositionDriver);
            
            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);

            Button takeButton = new Button {
                Text = "Взять заказ",
            };
            takeButton.Clicked += async (o, e) =>
            {
                bool result = await DisplayAlert("Взять заказ", "Вы уверенны что хотите выбрать этот заказ?", "Да", "Нет");
                if (result == true)
                {
                    if (driver.takeNewOrder(local.Item1))
                    {
                        isTakeOrder = true;
                        //driver.finishPointOfOrder = new Tuple<double, double>(System.Convert.ToDouble(local.Item4, CultureInfo.InvariantCulture), System.Convert.ToDouble(local.Item4, CultureInfo.InvariantCulture));
                        await DisplayAlert("Отлично", "Заказ ваш. Выполняйте!", "Так точно товарищ генерал!");
                    }
                    else
                    {
                        await DisplayAlert("Упс", "Этот заказ уже взят=(", "Значит не успел.");
                        await Navigation.PopAsync();
                    }

                }
            };

            Button endButton = new Button { Text = "Завершить" };
            endButton.Clicked += async(o, e) =>
            {
                driver.isEnd = true;
                driver.endOrderAboutButton();
                isTakeOrder = false;
                isClose = false;
                await DisplayAlert("Внимание!", "Заказ выполнен. Нажмите окей для продолжения работы.", "ОК");
                await Navigation.PopAsync();
            };


            Button goToBack = new Button{ Text = "Вернуться" };
            goToBack.Clicked += async (o, e) =>
            {
                await Navigation.PopAsync();
            };

            StackLayout buttonStack = new StackLayout();
            buttonStack.Orientation = StackOrientation.Horizontal;

            buttonStack.Children.Add(takeButton);
            buttonStack.Children.Add(endButton);
            buttonStack.Children.Add(goToBack);
            

            stack.Children.Add(buttonStack);
            
            Content = stack;
            Device.StartTimer(TimeSpan.FromSeconds(20), onTimerTick);
            
        }

        public bool onTimerTick()
        {
            getListAdressOfOrder();
            if (isTakeOrder == true)
            {
                driver.updateDriverLocation();
                if (driver.haveFinishOrder())
                {
                    isClose = true;
                }
            }
            return true;
        }

        async void getListAdressOfOrder()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var positionDriverGeo = await locator.GetPositionAsync(timeoutMilliseconds: 20000);
            driver.locationX = positionDriverGeo.Latitude;
            driver.locationY = positionDriverGeo.Longitude;
            if (isClose)
            {
                isClose = false;
                isTakeOrder = false;
                await DisplayAlert("Внимание!", "Заказ выполнен. Нажмите окей для продолжения работы.", "ОК");
                await Navigation.PopAsync();
            }
            currentPositionDriver.Position = new Position(positionDriverGeo.Latitude, positionDriverGeo.Longitude);
            map.Pins.RemoveAt(2);
            map.Pins.Add(currentPositionDriver);
        }

    }
}
