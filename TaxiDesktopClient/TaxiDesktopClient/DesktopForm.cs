using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Client;
using YandexAPI.Maps;
using System.Globalization;

namespace TaxiDesktopClient
{
    public partial class DesktopForm : Form
    {
        Client DesktopClient= new Client();
        List<string> Extra = new List<string>();
        int Phase = 0;
        //int Port = 10083;
        string IP = "127.0.0.1";
        int Port = 4040;        
        //string IP = "95.79.210.235";
        bool TextChange = false;
        Tuple<double, double> MapCenter = new Tuple<double,double>(56.299520, 43.982913);
        Tuple<double, double> MapMoveStart;
        bool MapMove = false;
        double kx = 0.0057;
        double ky = 0.0017;
        YandexAPI.Maps.GeoCode geoCode = new GeoCode();
        int zPos = 17;
        int MapMode = -1;
        Tuple<double, double> Start;
        Tuple<double, double> Finish;
        string sStart;
        string sFinish;
        string sDriver = "0,0";
        string SampleStart="Нижний Новгород, Гагарина, 23к2";
        string SampleFinish = "Нижний Новгород, ул. Бекетова, 69";

        public DesktopForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PhoneBox.Text = "+70000000000";
            if (!DesktopClient.Connect(IP, Port))
            {
                MessageBox.Show("Не удается подключиться к серверу");
                Close();
            }          
            MapCenter = new Tuple<double, double>(43.982913,56.299520);
            Start = new Tuple<double, double>(0, 0);
            Finish = new Tuple<double, double>(0, 0);
            string ImageUrl = GetUrl(MapCenter, Start, Finish, zPos, pictureBoxYandexMap.Width, pictureBoxYandexMap.Height);
            pictureBoxYandexMap.Image = geoCode.DownloadMapImage(ImageUrl);
            pictureBoxYandexMap.Refresh();
        }

        private void buttonFromAddress_Click(object sender, EventArgs e)
        {
            buttonFromAddress.Visible = false;
            buttonFromCoord.Visible = false;
            FromAddressBox.Visible = true;
            FromAddressLabel.Visible = true;
            FromAddressBox.Visible = true;
        }

        private void buttonFromCoord_Click(object sender, EventArgs e)
        {
            buttonFromAddress.Visible = false;
            buttonFromCoord.Visible = false;
            labelFromX.Visible = true;
            labelFromY.Visible = true;
            textBoxFromX.Visible = true;
            textBoxFromY.Visible = true;
            buttonMapFrom.Visible = true;
        }

        private void buttonToAddress_Click(object sender, EventArgs e)
        {
            buttonToAddress.Visible = false;
            buttonToCoord.Visible = false;
            ToAddressBox.Visible = true;
            ToAddressLabel.Visible = true;
            ToAddressBox.Visible = true;
        }

        private void buttonToCoord_Click(object sender, EventArgs e)
        {
            buttonToAddress.Visible = false;
            buttonToCoord.Visible = false;
            labelToX.Visible = true;
            labelToY.Visible = true;
            textBoxToX.Visible = true;
            textBoxToY.Visible = true;
            buttonMapTo.Visible = true;
        }

        private void OrderButton_Click(object sender, EventArgs e)
        {
            Extra.Clear();
            foreach (string s in ExtracheckedListBox.CheckedItems)
            {
                Extra.Add(s);
            }
            if (!DesktopClient.CreateOrder(NameBox.Text, PhoneBox.Text, FromAddressBox.Text, textBoxFromX.Text, textBoxFromY.Text, ToAddressBox.Text, textBoxToX.Text, textBoxToY.Text, Extra))
            {
                MessageBox.Show("Введены некорректные данные");
            }          
            else
            {
                OrderButton.Visible = false;
                labelPrice.Visible = true;
                labelProcessing.Visible = true;
                NameBox.Enabled = false;
                PhoneBox.Enabled = false;
                textBoxFromX.Enabled = false;
                textBoxFromY.Enabled = false;
                textBoxToX.Enabled = false;
                textBoxToY.Enabled = false;
                FromAddressBox.Enabled = false;
                ToAddressBox.Enabled = false;
                ExtracheckedListBox.Enabled = false;
                labelOperatorPhone.Visible = true;
                Phase = 1;
                buttonAbort.Visible = true;
                buttonMapFrom.Enabled = false;
                buttonMapTo.Enabled = false;
                labelOperatorPhone.Text = "Связь с оператором: " + DesktopClient.GetOperatorPhone();
                sStart = DesktopClient.GetStartPosition();
                sFinish = DesktopClient.GetFinishPosition();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Phase == 1)
            {
                if (DesktopClient.IsTaken())
                {
                    Phase = 2;
                    DesktopClient.SetOrderDriverInfo();
                    labelProcessing.Text = "Ваш заказ принят";
                    labelDriverName.Text = "Имя водителя: " + DesktopClient.GetDriverName();
                    labelDriverPhone.Text = "Связь с водителем: " + DesktopClient.GetDriverPhone();
                    labelDriverName.Visible = true;
                    labelDriverPhone.Visible = true;
                }
            }
            if (Phase==2)
            {
                sDriver=DesktopClient.GetDriverPosition();
                string ImageUrl = GetUrl(MapCenter, sStart, sFinish, sDriver, zPos, pictureBoxYandexMap.Width, pictureBoxYandexMap.Height);
                pictureBoxYandexMap.Image = geoCode.DownloadMapImage(ImageUrl);
                pictureBoxYandexMap.Refresh();
            }
            if (TextChange)
            {
                if ((FromAddressBox.Text != "" || (textBoxFromX.Text != "" && textBoxFromY.Text != "")) && (ToAddressBox.Text != "" || (textBoxToX.Text != "" && textBoxToY.Text != "")))
                {
                    decimal Tmp = DesktopClient.GetPrice(FromAddressBox.Text, textBoxFromX.Text, textBoxFromY.Text, ToAddressBox.Text, textBoxToX.Text, textBoxToY.Text);
                    if (Tmp != (-1))
                    {
                        labelPrice.Text = "Цена: " + DesktopClient.GetPrice(FromAddressBox.Text, textBoxFromX.Text, textBoxFromY.Text, ToAddressBox.Text, textBoxToX.Text, textBoxToY.Text) + " руб.";
                    }
                    else
                    {
                        labelPrice.Text = "Цена: ";
                    }
                    TextChange = false;
                }
            }
        }

        private void buttonAbort_Click(object sender, EventArgs e)
        {
            DesktopClient.AbortOrder();
            OrderButton.Visible = true;
            labelProcessing.Visible = false;
            NameBox.Enabled = true;
            PhoneBox.Enabled = true;
            textBoxFromX.Enabled = true;
            textBoxFromY.Enabled = true;
            textBoxToX.Enabled = true;
            textBoxToY.Enabled = true;
            FromAddressBox.Enabled = true;
            ToAddressBox.Enabled = true;
            ExtracheckedListBox.Enabled = true;
            labelOperatorPhone.Visible = false;
            Phase = 0;
            buttonAbort.Visible = false;
            buttonMapFrom.Enabled = true;
            buttonMapTo.Enabled = true;
            labelDriverName.Visible = false;
            labelDriverPhone.Visible = false;
            Start = new Tuple<double, double>(0, 0);
            Finish = new Tuple<double, double>(0, 0);
            sDriver = "0,0";
            MapMode = -1;
        }

        private void buttonMapFrom_Click(object sender, EventArgs e)
        {
            MapMode = 0;
            
        }

        private void buttonMapTo_Click(object sender, EventArgs e)
        {  
            MapMode = 1;
        }

        private void StartFinishTextChanged(object sender, EventArgs e)
        {
            TextChange = true;
        }

        private void MapMoveOn(object sender, MouseEventArgs e)
        {
            MapMove = true;
            MapMoveStart = new Tuple<double, double>(e.X, e.Y);
        }

        private void MapMoveOff(object sender, MouseEventArgs e)
        {
            MapMove = false;
        }

        private void MapMoveEvent(object sender, MouseEventArgs e)
        {
            if (MapMove)
            {
                double dx = e.X - MapMoveStart.Item1;
                double dy = e.Y - MapMoveStart.Item2;
                MapCenter = new Tuple<double, double>(MapCenter.Item1 - dx * 0.5*kx / pictureBoxYandexMap.Width, MapCenter.Item2 + dy * 0.5*ky / pictureBoxYandexMap.Height);
                string ImageUrl;
                if (Phase == 2)
                {
                    ImageUrl = GetUrl(MapCenter, sStart, sFinish, sDriver, zPos, pictureBoxYandexMap.Width, pictureBoxYandexMap.Height);
                }
                else
                {
                    ImageUrl = GetUrl(MapCenter, Start, Finish, zPos, pictureBoxYandexMap.Width, pictureBoxYandexMap.Height);
                }
                pictureBoxYandexMap.Image = geoCode.DownloadMapImage(ImageUrl);
                pictureBoxYandexMap.Refresh();
            }
        }

        private void MoveKey(object sender, KeyEventArgs e)
        {
        }

        private void FocusForm(object sender, EventArgs e)
        {
            
        }

        private string GetUrl(Tuple<double, double> Center, Tuple<double, double> Start, Tuple<double, double> Finish, int zPos, int Width, int Height)
        {
            
            string pCenter = Center.Item1.ToString("G", CultureInfo.InvariantCulture) + "," + Center.Item2.ToString("G", CultureInfo.InvariantCulture);
            string pStart =Start.Item1.ToString("G", CultureInfo.InvariantCulture) + "," +Start.Item2.ToString("G", CultureInfo.InvariantCulture);
            string pFinish =Finish.Item1.ToString("G", CultureInfo.InvariantCulture) + "," + Finish.Item2.ToString("G", CultureInfo.InvariantCulture);
            return String.Format("http://static-maps.yandex.ru/1.x/?ll={0}&size={3},{4}&z={5}&l=map&pt={1},pm2al~{2},pm2bl&lang=ru-RU", pCenter,pStart,pFinish, Width, Height, zPos);
        }
        private string GetUrl(Tuple<double, double> Center, string Start, string Finish, string Driver, int zPos, int Width, int Height)
        {

            string pCenter = Center.Item1.ToString("G", CultureInfo.InvariantCulture) + "," + Center.Item2.ToString("G", CultureInfo.InvariantCulture);
            return String.Format("http://static-maps.yandex.ru/1.x/?ll={0}&size={4},{5}&z={6}&l=map&pt={1},pm2al~{2},pm2bl~{3},flag&lang=ru-RU", pCenter, Start, Finish, Driver, Width, Height, zPos);
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            if (zPos < 19)
            {
                zPos += 1;
                kx /= 2;
                ky /= 2;
                string ImageUrl = GetUrl(MapCenter, Start, Finish, zPos, pictureBoxYandexMap.Width, pictureBoxYandexMap.Height);
                pictureBoxYandexMap.Image = geoCode.DownloadMapImage(ImageUrl);
                pictureBoxYandexMap.Refresh();
            }
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            if (zPos > 7)
            {
                zPos -= 1;
                kx *= 2;
                ky *= 2;
                string ImageUrl = GetUrl(MapCenter, Start, Finish, zPos, pictureBoxYandexMap.Width, pictureBoxYandexMap.Height);
                pictureBoxYandexMap.Image = geoCode.DownloadMapImage(ImageUrl);
                pictureBoxYandexMap.Refresh();
            }
        }

        private void pictureBoxYandexMap_Click(object sender, EventArgs e)
        {
            MouseEventArgs M = (MouseEventArgs)e;
            double dx = M.X - pictureBoxYandexMap.Width/2;
            double dy = M.Y - pictureBoxYandexMap.Height / 2;
            if (MapMode == 0)
            {
                Start = new Tuple<double, double>(MapCenter.Item1 + dx * kx / pictureBoxYandexMap.Width, MapCenter.Item2 - dy * ky / pictureBoxYandexMap.Height);
                MapMode = -1;
                textBoxFromX.Text = Start.Item1.ToString("G", CultureInfo.InvariantCulture);
                textBoxFromY.Text = Start.Item2.ToString("G", CultureInfo.InvariantCulture);
                string ImageUrl = GetUrl(MapCenter, Start, Finish, zPos, pictureBoxYandexMap.Width, pictureBoxYandexMap.Height);
                pictureBoxYandexMap.Image = geoCode.DownloadMapImage(ImageUrl);
                pictureBoxYandexMap.Refresh();
            }
            if (MapMode == 1)
            {
                Finish = new Tuple<double, double>(MapCenter.Item1 + dx * kx / pictureBoxYandexMap.Width, MapCenter.Item2 - dy * ky / pictureBoxYandexMap.Height);
                MapMode = -1;
                textBoxToX.Text = Finish.Item1.ToString("G", CultureInfo.InvariantCulture);
                textBoxToY.Text = Finish.Item2.ToString("G", CultureInfo.InvariantCulture);
                string ImageUrl = GetUrl(MapCenter, Start, Finish, zPos, pictureBoxYandexMap.Width, pictureBoxYandexMap.Height);
                pictureBoxYandexMap.Image = geoCode.DownloadMapImage(ImageUrl);
                pictureBoxYandexMap.Refresh();
            }
        }
    }
}
