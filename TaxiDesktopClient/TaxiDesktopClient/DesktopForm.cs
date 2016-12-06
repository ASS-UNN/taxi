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

namespace TaxiDesktopClient
{
    public partial class DesktopForm : Form
    {
        Client DesktopClient= new Client();
        MapForm Map = new MapForm();
        List<string> Extra = new List<string>();
        int Phase = 0;
        //int Port = 10083;
        //string IP = "127.0.0.1";
        int Port = 4040;        
        string IP = "95.79.210.235";

        public DesktopForm()
        {
            InitializeComponent();
            Map.Owner = this;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!DesktopClient.Connect(IP, Port))
            {
                MessageBox.Show("Не удается подключиться к серверу");
                Close();
            }
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
                labelPrice.Text = "Цена:" + System.Convert.ToString(DesktopClient.GetPrice());
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
                    buttonDriverPos.Visible = true;
                }
            }
            if (Phase==2)
            {
                Map.DrawPos(DesktopClient.GetDriverPossition());
            }
        }

        private void buttonAbort_Click(object sender, EventArgs e)
        {
            DesktopClient.AbortOrder();
            OrderButton.Visible = true;
            labelPrice.Visible = false;
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
            buttonDriverPos.Visible = false;
            labelDriverName.Visible = false;
            labelDriverPhone.Visible = false;
        }

        private void buttonDriverPos_Click(object sender, EventArgs e)
        {
            Map.MapMode = -1;
            Map.ShowDialog();
        }

        private void buttonMapFrom_Click(object sender, EventArgs e)
        {
            Map.MapMode = 0;
            Map.SetInit();
            Map.ShowDialog();
            
        }

        private void buttonMapTo_Click(object sender, EventArgs e)
        {
            Map.MapMode = 1;
            Map.SetInit();
            Map.ShowDialog();           
        }
    }
}
