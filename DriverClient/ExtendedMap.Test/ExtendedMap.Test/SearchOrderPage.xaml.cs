using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;
using Xamarin.Forms;

namespace ExtendedMap.Test
{
    public partial class SearchOrderPage : ContentPage
    {
        public string convertValue = "Empty";
        public List<Button> buttonOfOrder;
        public StackLayout stackLayout;
        public List<Tuple<int, string, string, string, string>> local;
        public ScrollView scrollView;
        public StackLayout stackOfOrder = new StackLayout();
        public List<string> listOfStringAddress = new List<string>();
        public bool isListRepair = false;
        public DriverPerson localDriver;

        public SearchOrderPage(DriverPerson driver)
        {
            this.localDriver = driver;
            var logo = new Image { Source = "logo.png" };
            BackgroundColor = Color.FromRgb(0, 74, 127);
            stackLayout = new StackLayout();
            stackLayout.Children.Add(logo);
            //driver.updateListOfOrder();
            localDriver.updateListOfOrder();
            local = localDriver.listOfOrder;
            buttonOfOrder = new List<Button>(local.Count);
            scrollView = new ScrollView();
            if (local != null)
            {
                Device.StartTimer(TimeSpan.FromSeconds(3), onTimerTick);
            }
        }
        public bool onTimerTick()
        {
            if (isListRepair)
            {
                if (buttonOfOrder != null)
                {
                    buttonOfOrder.Clear();
                }
                if (stackLayout.Children.IndexOf(stackOfOrder) != -1)
                {
                    stackLayout.Children.RemoveAt(1);
                    stackOfOrder.Children.Clear();
                }

                for (int i = 0; i < local.Count; i++)
                {
                    buttonOfOrder.Add(new Button
                    {
                        Text = listOfStringAddress[i],
                        FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button))
                    });

                }
                foreach (Button s in buttonOfOrder)
                {
                    //buttonOfOrder.FindIndex(button=>button.Text == s.Text);
                    s.Clicked += async delegate { await Navigation.PushAsync(new detailOrderCoordinatePage(local[buttonOfOrder.FindIndex(button => button.Text == s.Text)], localDriver)); };
                    stackOfOrder.Children.Add(s);
                }
                stackLayout.Children.Add(stackOfOrder);
                scrollView.Content = stackLayout;
                this.Content = scrollView;
                localDriver.updateListOfOrder();
                local = localDriver.listOfOrder;
                isListRepair = false;
                listOfStringAddress.Clear();

            }
            else
            {
                getListAdressOfOrder();
            }
            return true;
        }

        public void getListAdressOfOrder()
        {
            foreach (Tuple<int, string, string, string, string> s in local)
            {
                listOfStringAddress.Add("Забрать c " + s.Item2 + " | " + s.Item3 + " и отвезти " + s.Item4 + " | " + s.Item5 + " | ");
                //("Забрать c " + startAddres.ElementAt<string>(0) + " и отвезти " + finishAddres.ElementAt<string>(0));
                //(s.Item1 + " | " + s.Item2 + " | " + s.Item3 + " | " + s.Item4 + " | " + s.Item5 + " | "); //("Забрать c "+startAddres.ElementAt<string>(0)+ " и отвезти " + finishAddres.ElementAt<string>(0));   //(apptexta.ElementAt<string>(0));
            }
            isListRepair = true;
        }
    }
}
