using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CustomerGraphicsInterface
{
    public partial class CheckoutPage : ContentPage
    {
        Label listOfExtra;
        public string name = "";
        public string phone = "";
        public CheckoutPage(string where, string toWhere, decimal orderCost, string extraProperties)
        {
            this.Title = "Superb taxi service";
            //this.BackgroundImage = Device.OnPlatform("Resources/TaxiLogo.png", "Drawable/TaxiLogo.png", "Assets/TaxiLogo.png");
            Grid grid = new Grid
            {
                RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
            },
                ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
            }
            };
            var logo = new Image { Source = "logo.png" };
            Label nameCustomer = new Label { Text = "Имя", Font = Font.SystemFontOfSize(15) };
            Entry nameCustomerEntry = new Entry { Placeholder = "Введите своё имя" };
            nameCustomerEntry.Unfocused += delegate { name = nameCustomerEntry.Text; };
            Label phoneCustomer = new Label { Text = "Телефон", Font = Font.SystemFontOfSize(15) };
            Entry phoneCustomerEntry = new Entry { Placeholder = "Введите свой номер телефона" };
            phoneCustomerEntry.Unfocused += delegate { phone = phoneCustomerEntry.Text; };
            Label planeGaveL = new Label { Text = where };
            Label planeToL = new Label { Text = toWhere };
            Label planeGave = new Label { Text = "Откуда:", Font = Font.SystemFontOfSize(15) };
            Label planeTo = new Label { Text = "Куда:", Font = Font.SystemFontOfSize(15) };
            Label orientalCost = new Label { Text = "Ориентировачная стоимость", Font = Font.SystemFontOfSize(15) };
            Label resultOrientalCost = new Label { Text = "---", Font = Font.SystemFontOfSize(15) };
            Label orderCostLabel = new Label { Text = orderCost.ToString(), Font = Font.SystemFontOfSize(15) };
            listOfExtra = new Label { Text = extraProperties, Font = Font.SystemFontOfSize(15) };
            Button orderTaxiAndToTrack = new Button { Text = "Заказать!" };
            orderTaxiAndToTrack.Clicked += toTrackPage;
            grid.Children.Add(logo, 0, 0);
            grid.Children.Add(nameCustomer, 0, 3);
            grid.Children.Add(nameCustomerEntry, 0, 4);
            grid.Children.Add(phoneCustomer, 0, 5);
            grid.Children.Add(phoneCustomerEntry, 0, 6);
            grid.Children.Add(planeGave, 0, 7);
            grid.Children.Add(planeGaveL, 0, 8);
            grid.Children.Add(planeTo, 0, 9);
            grid.Children.Add(planeToL, 0, 10);
            grid.Children.Add(orientalCost, 0, 11);
            grid.Children.Add(orderCostLabel, 5, 11);
            grid.Children.Add(listOfExtra, 0, 12);
            grid.Children.Add(orderTaxiAndToTrack, 0, 14);
            Grid.SetColumnSpan(logo, 6);
            Grid.SetRowSpan(logo, 3);
            Grid.SetColumnSpan(nameCustomer, 6);
            Grid.SetColumnSpan(nameCustomerEntry, 6);
            Grid.SetColumnSpan(phoneCustomer, 6);
            Grid.SetColumnSpan(phoneCustomerEntry, 6);
            Grid.SetColumnSpan(planeGave, 6);
            Grid.SetColumnSpan(planeGaveL, 6);
            Grid.SetColumnSpan(planeTo, 6);
            Grid.SetColumnSpan(planeToL, 6);
            Grid.SetColumnSpan(orientalCost, 5);
            Grid.SetColumnSpan(orderCostLabel, 1);
            Grid.SetColumnSpan(listOfExtra, 6);
            Grid.SetRowSpan(listOfExtra, 2);
            Grid.SetColumnSpan(orderTaxiAndToTrack, 6);
            this.Content = grid;
        }
        async private void toTrackPage(object sender, EventArgs e)
        {
            if ((name != "") && (phone != ""))
            {
                await Navigation.PushAsync(new TrackPage());
            }
            else
            {
                await DisplayAlert("Ошибка", "Введите своё имя и свой телефон", "ОK");
            }
        }
    }
}
