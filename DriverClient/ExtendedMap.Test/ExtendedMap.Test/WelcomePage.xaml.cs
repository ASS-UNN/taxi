using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ExtendedMap.Test
{
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage(DriverPerson driver)
        {
            this.Title = "Superb taxi service";
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
            Label logiLabel = new Label { Text = "Логин", Font = Font.BoldSystemFontOfSize(30) };
            Entry loginEnter = new Entry { Placeholder = "Введите логин здесь" };
            Label passLabel = new Label { Text = "Пароль", Font = Font.BoldSystemFontOfSize(30) };
            Entry passEnter = new Entry { Placeholder = "Введите пароль здесь", IsPassword = true };
            Button logTo = new Button { Text = "Войти" };
            logTo.Clicked += async delegate {
                if (driver.loginingToServer(loginEnter.Text, passEnter.Text) != -1)//(driver.canLog(loginEnter.Text, passEnter.Text))
                {
                    await Navigation.PushAsync(new SearchOrderPage(driver));
                }
                else
                {
                    loginEnter.Text = "";
                    passEnter.Text = "";
                    await DisplayAlert("Ошибка", "В базе данных нет подходящего сочетания логина и пароля", "ОК");
                }
            };
            Button inormation = new Button { Text = "?", Font = Font.BoldSystemFontOfSize(20) };
            inormation.Clicked += async delegate {
                await DisplayAlert("Внимание", "Если вы не регистрировались в штате компании, обратитесь к системному администратору для авторизации в системе!", "ОК");
            };

            Button registration = new Button { Text = "Регистрация" };
            registration.Clicked += async delegate {
                await Navigation.PushAsync(new RegistrationPage(driver));
            };

            //Zone of add
            grid.Children.Add(logo, 0, 0);
            grid.Children.Add(logiLabel, 2, 5);
            grid.Children.Add(loginEnter, 0, 6);
            grid.Children.Add(passLabel, 2, 7);
            grid.Children.Add(passEnter, 0, 8);
            grid.Children.Add(logTo, 0, 9);
            grid.Children.Add(inormation, 5, 9);
            grid.Children.Add(registration, 0, 10);

            //Zone of location screen
            Grid.SetColumnSpan(logo, 6);
            Grid.SetRowSpan(logo, 5);
            Grid.SetColumnSpan(logiLabel, 2);
            Grid.SetColumnSpan(loginEnter, 6);
            Grid.SetColumnSpan(passLabel, 2);
            Grid.SetColumnSpan(passEnter, 6);
            Grid.SetColumnSpan(logTo, 5);
            Grid.SetColumnSpan(inormation, 1);
            Grid.SetColumnSpan(registration, 6);
            this.Content = grid;
        }
    }
}
