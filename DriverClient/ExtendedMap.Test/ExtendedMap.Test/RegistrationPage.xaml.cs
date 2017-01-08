using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ExtendedMap.Test
{
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage(DriverPerson driver)
        {
            this.Title = "Регистрация";
            BackgroundColor = Color.FromRgb(0, 74, 127);
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

            Label labelRegistration = new Label { Text = "Регистрация", Font = Font.BoldSystemFontOfSize(30) };

            Entry nameEnter = new Entry { Placeholder = "Введите имя" };
            Entry phoneEnter = new Entry { Placeholder = "Введите телефон" };
            Entry loginEnter = new Entry { Placeholder = "Введите логин" };
            Entry passEnter = new Entry { Placeholder = "Введите пароль", IsPassword = true };

            Button registrationToServer = new Button { Text = "Готово", Font = Font.BoldSystemFontOfSize(20) };
            registrationToServer.Clicked += async delegate {
                if ((nameEnter.Text == "") || (phoneEnter.Text == "") || (loginEnter.Text == "") || (passEnter.Text == "")) {
                    await DisplayAlert("Ошибка", "Не все поля заполнены. Для завершения регистрации все полядолжны быть не пустыми.", "ОК");
                }
                else {
                    int resultReg = driver.registrationNewDriverOnTheServer(nameEnter.Text, phoneEnter.Text, loginEnter.Text, passEnter.Text);
                    await DisplayAlert("Ошибка", resultReg.ToString(), "Исправить");

                    if (resultReg != -1)
                    {
                        await DisplayAlert("Отлично", "Регистрация прошла успешно", "ОК");
                        await Navigation.PopAsync();
                    } else
                    {
                        await DisplayAlert("Ошибка", "Возможно водитель с таким ником уже есть, или телефон имеет неверный формат.", "Исправить");
                    }
                }
            };

            //Zone of add
            grid.Children.Add(logo, 0, 0);
            grid.Children.Add(labelRegistration, 1, 5);
            grid.Children.Add(nameEnter, 0, 6);
            grid.Children.Add(phoneEnter, 0, 7);
            grid.Children.Add(loginEnter, 0, 8);
            grid.Children.Add(passEnter, 0, 9);
            grid.Children.Add(registrationToServer, 0, 10);

            //Zone of location screen
            Grid.SetColumnSpan(logo, 6);
            Grid.SetRowSpan(logo, 5);
            Grid.SetColumnSpan(labelRegistration, 3);
            Grid.SetColumnSpan(nameEnter, 6);
            Grid.SetColumnSpan(phoneEnter, 6);
            Grid.SetColumnSpan(loginEnter, 6);
            Grid.SetColumnSpan(passEnter, 6);
            Grid.SetColumnSpan(registrationToServer, 6);


            this.Content = grid;
        }
    }
}
