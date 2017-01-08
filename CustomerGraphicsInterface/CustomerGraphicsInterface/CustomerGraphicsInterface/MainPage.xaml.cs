using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Client;
using Xamarin.Forms;

namespace CustomerGraphicsInterface
{
    public partial class MainPage : ContentPage
    {
        public IScsServiceClient<ITaxiService> client;
        public string where = ""; // Откуда
        public string toWhere = ""; // Куда
        public List<string> extraP = new List<string>();
        public decimal orderCost = 0;
        Picker extraProperty;
        public string tmp;
        Label listOfExtra;
        public MainPage(IScsServiceClient<ITaxiService> client)
        {
            this.client = client;
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
            Label resultOrientalCost = new Label { Text = "---", Font = Font.SystemFontOfSize(15) };
            var logo = new Image { Source = "logo.png" };
            var zakaz = new Image { Source = "zakaz.png" };
            Entry planeGaveEntery = new Entry { Placeholder = "Откуда Вас забрать?" };
            planeGaveEntery.Unfocused += delegate {
                where = planeGaveEntery.Text;
                if ((where != "")&&(toWhere != ""))
                {
                    resultOrientalCost.Text = client.ServiceProxy.GetPrice(1).ToString();
                    orderCost = client.ServiceProxy.GetPrice(1);
                }
            };
            Entry planeToEntery = new Entry { Placeholder = "Куда вас отвезти?" };
            planeToEntery.Unfocused += delegate {
                toWhere = planeToEntery.Text;
                if ((where != "") && (toWhere != ""))
                {
                    resultOrientalCost.Text = client.ServiceProxy.GetPrice(1).ToString();
                    orderCost = client.ServiceProxy.GetPrice(1);
                }
            };
            Button gaveMap = new Button { Text = "Карта" };
            Button toMap = new Button { Text = "Карта" };
            Button spravka = new Button { Text = "?" };
            Button toCheckout = new Button { Text = "К оформлению заказа" };
            toCheckout.Clicked += toCheckoutPage;
            spravka.Clicked += delegate { DisplayAlert("Справка", "Приблизительная стоимость заказа появится только в случае заполненных полей 'Куда' и 'Откуда'", "ОK"); };
            Label planeGave = new Label { Text = "Откуда:", Font = Font.SystemFontOfSize(15) };
            Label planeTo = new Label { Text = "Куда:", Font = Font.SystemFontOfSize(15) };
            Label orientalCost = new Label { Text = "Ориентировачная стоимость", Font = Font.SystemFontOfSize(15) };
            
            Label magazinError = new Label { Text = "Обязательно укажите маршрут", Font = Font.SystemFontOfSize(15) };
            listOfExtra = new Label { Text = "Список дополнительных парамметров", Font = Font.SystemFontOfSize(15) };
            extraProperty = new Picker { Title = "Дополнительные парамметры заказа" };
            extraProperty.Items.Add("Детское кресло");
            extraProperty.Items.Add("Подъемник");
            extraProperty.Items.Add("Перевозка животного");
            extraProperty.Items.Add("Багаж в салоне");
            extraProperty.Items.Add("Кузов 'универсал'");
            extraProperty.Items.Add("Молчаливый водитель");
            extraProperty.Items.Add("Водитель-женщина");
            extraProperty.SelectedIndexChanged += picker_SelectedIndexChanged;
            grid.Children.Add(logo, 0, 0);
            grid.Children.Add(zakaz, 0, 3);
            grid.Children.Add(planeGave, 0, 4);
            grid.Children.Add(planeTo, 0, 6);
            grid.Children.Add(planeGaveEntery, 0, 5);
            grid.Children.Add(gaveMap, 4, 5);
            grid.Children.Add(planeToEntery, 0, 7);
            grid.Children.Add(toMap, 4, 7);
            grid.Children.Add(extraProperty, 0, 8);
            grid.Children.Add(listOfExtra, 0, 9);
            grid.Children.Add(orientalCost, 0, 10);
            grid.Children.Add(resultOrientalCost, 5, 10);
            grid.Children.Add(magazinError, 0, 11);
            grid.Children.Add(spravka, 5, 11);
            grid.Children.Add(toCheckout, 0, 12);
            Grid.SetColumnSpan(logo, 6);
            Grid.SetRowSpan(logo, 3);
            Grid.SetColumnSpan(zakaz, 6);
            Grid.SetRowSpan(zakaz, 1);
            Grid.SetColumnSpan(planeGave, 3);
            Grid.SetColumnSpan(planeTo, 3);
            Grid.SetColumnSpan(planeGaveEntery, 4);
            Grid.SetColumnSpan(gaveMap, 2);
            Grid.SetColumnSpan(planeToEntery, 4);
            Grid.SetColumnSpan(toMap, 2);
            Grid.SetColumnSpan(extraProperty, 6);
            Grid.SetColumnSpan(listOfExtra, 6);
            Grid.SetColumnSpan(orientalCost, 5);
            Grid.SetColumnSpan(resultOrientalCost, 1);
            Grid.SetColumnSpan(magazinError, 5);
            Grid.SetColumnSpan(spravka, 1);
            Grid.SetColumnSpan(toCheckout, 6);
            this.Content = grid;
        }
        void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            string extraPropertyEntered = extraProperty.Items[extraProperty.SelectedIndex];
            if (extraP.IndexOf(extraPropertyEntered) == -1)
            {
                extraP.Add(extraPropertyEntered);
            }
            else
            {
                extraP.Remove(extraPropertyEntered);
            }
            listOfExtra.Text = "";
            foreach (string s in extraP)
            {
                listOfExtra.Text += s + ", ";
            }
            tmp = listOfExtra.Text;
        }
        async private void toCheckoutPage(object sender, EventArgs e)
        {
            if ((where != "")&&(toWhere != ""))
            {
                await Navigation.PushAsync(new CheckoutPage(where, toWhere, orderCost, tmp));
            }
            else
            {
                await DisplayAlert("Ошибка", "Введите адреса загрузки и доставки клиента", "ОK");
            }
        }
    }
}
