using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;
using Xamarin.Forms;

namespace ExtendedMap.Test
{
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            var map = new ExtendedMap(
                MapSpan.FromCenterAndRadius(
                    new Position(37, -122), Distance.FromMiles(0.3)))
            {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            map.Tap += async (o, e) =>
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var positionDriverGeo = await locator.GetPositionAsync(timeoutMilliseconds: 60000);
                bool result = await DisplayAlert("Взять заказ", "Ваша позиция: " + positionDriverGeo.Latitude.ToString() + positionDriverGeo.Longitude.ToString(), "Да", "Нет");
            };


            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            // The root page of your application
            Content = stack;
        }
    }
}
