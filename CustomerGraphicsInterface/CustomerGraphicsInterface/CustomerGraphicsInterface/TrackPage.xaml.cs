using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Messaging;
using Xamarin.Forms;

namespace CustomerGraphicsInterface
{
    public partial class TrackPage : ContentPage
    {
        public string phoneNumberManager = "+79524480324";
        public string phoneNumberDriver = "+79108724299";
        public TrackPage()
        {
            this.Title = "Superb taxi service";
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
            Label orderStart = new Label { Text = "Заказ оформлен", Font = Font.SystemFontOfSize(15) };
            Label linkManagerL = new Label { Text = "Связаться с менеджером:", Font = Font.SystemFontOfSize(15) };
            Label phoneManager = new Label { Text = phoneNumberManager, Font = Font.SystemFontOfSize(25) };
            Label linlDriverL = new Label { Text = "Связаться с водителем", Font = Font.SystemFontOfSize(15) };
            Label phoneDriver = new Label { Text = phoneNumberDriver, Font = Font.SystemFontOfSize(25) };
            Button callManager = new Button { Text = "Вызвать" };
            Button callDriver = new Button { Text = "Вызвать" };
            callDriver.Clicked += delegate
            {
                var phoneCallTask = CrossMessaging.Current.PhoneDialer;
                if (phoneCallTask.CanMakePhoneCall)
                {
                    phoneCallTask.MakePhoneCall(phoneNumberDriver, "Водятел");
                }
            };
            callManager.Clicked += delegate
            {
                var phoneCallTask = CrossMessaging.Current.PhoneDialer;
                if (phoneCallTask.CanMakePhoneCall)
                {
                    phoneCallTask.MakePhoneCall(phoneNumberManager, "Мэнеджер");
                }
            };
            Button coordinateOfDriver = new Button { Text = "Отследить положение водителя" };
            Button deleteOrder = new Button { Text = "Отменить заказ" };
            Button questionAboutDelete = new Button { Text = "?" };
            deleteOrder.Clicked += async delegate {
                bool result = await DisplayAlert("Отказ от заказа", "Вы уверены что хотите отказаться от заказа?", "Да", "Нет");
                if (result == true)
                {
                    await Navigation.PopToRootAsync();
                }
            };
            questionAboutDelete.Clicked += delegate { DisplayAlert("Справка", "В случае отказа от такчи, мы вынуждены занести вас в чёрный список компании", "ОK"); };

            grid.Children.Add(logo, 0, 0);
            grid.Children.Add(orderStart, 0, 3);
            grid.Children.Add(linkManagerL, 0, 4);
            grid.Children.Add(phoneManager, 0, 5);
            grid.Children.Add(callManager, 3, 5);
            grid.Children.Add(linlDriverL, 0, 6);
            grid.Children.Add(phoneDriver, 0, 7);
            grid.Children.Add(callDriver, 3, 7);
            grid.Children.Add(coordinateOfDriver, 0, 8);
            grid.Children.Add(deleteOrder, 0, 10);
            grid.Children.Add(questionAboutDelete, 5, 10);
            Grid.SetColumnSpan(logo, 6);
            Grid.SetRowSpan(logo, 3);
            Grid.SetColumnSpan(orderStart, 6);
            Grid.SetColumnSpan(linkManagerL, 6);
            Grid.SetColumnSpan(phoneManager, 3);
            Grid.SetColumnSpan(callManager, 3);
            Grid.SetColumnSpan(linlDriverL, 6);
            Grid.SetColumnSpan(phoneDriver, 3);
            Grid.SetColumnSpan(callDriver, 3);
            Grid.SetColumnSpan(coordinateOfDriver, 6);
            Grid.SetRowSpan(coordinateOfDriver, 2);
            Grid.SetColumnSpan(deleteOrder, 5);
            Grid.SetColumnSpan(questionAboutDelete, 1);

            this.Content = grid;
        }
    }
}
