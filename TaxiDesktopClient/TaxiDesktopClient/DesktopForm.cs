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
        MapForm Map = new MapForm();
        IScsServiceClient<ITaxiService> client;
        int ID=-1;
        int Status = -1;
        List<string> Extra = new List<string>();
        bool FromMode = true;
        bool ToMode = true;
        int Phase = 0;
        Coord DriverPos;
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
            client = ScsServiceClientBuilder.CreateClient<ITaxiService>(new ScsTcpEndPoint(IP, Port));
            try
            {
                client.Connect();
            }
            catch(Exception E)
            {
                MessageBox.Show("Не удается подключиться к серверу");
                client.Dispose();
                Close();
            }
            //client.Dispose();
        }

        private void buttonFromAddress_Click(object sender, EventArgs e)
        {
            buttonFromAddress.Visible = false;
            buttonFromCoord.Visible = false;
            FromAddressBox.Visible = true;
            FromAddressLabel.Visible = true;
            FromAddressBox.Visible = true;
            FromMode = false;
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
            FromMode = true;
        }

        private void buttonToAddress_Click(object sender, EventArgs e)
        {
            buttonToAddress.Visible = false;
            buttonToCoord.Visible = false;
            ToAddressBox.Visible = true;
            ToAddressLabel.Visible = true;
            ToAddressBox.Visible = true;
            ToMode = false;
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
            ToMode = true;
        }

        private void OrderButton_Click(object sender, EventArgs e)
        {
            Map.ShowDialog();
            float x;
            float y;
            Extra.Clear();
            if (FromMode)
            {
                try
                {
                    x = System.Convert.ToSingle(textBoxFromX.Text);
                    y = System.Convert.ToSingle(textBoxFromY.Text);

                }
                catch (Exception E)
                {
                    MessageBox.Show("Введены некорректные координаты");
                    return;
                }
            }
            if (ToMode)
            {
                try
                {
                    x = System.Convert.ToSingle(textBoxToX.Text);
                    y = System.Convert.ToSingle(textBoxToY.Text);

                }
                catch (Exception E)
                {
                    MessageBox.Show("Введены некорректные координаты");
                    return;
                }
            }
            foreach (string s in ExtracheckedListBox.CheckedItems)
            {
                Extra.Add(s);
            }
            if (!FromMode && !ToMode)
            {
                ID = client.ServiceProxy.CreateOrder(NameBox.Text, PhoneBox.Text, FromAddressBox.Text, ToAddressBox.Text, Extra);
            }
            if (FromMode && !ToMode)
            {
                ID = client.ServiceProxy.CreateOrder(NameBox.Text, PhoneBox.Text, System.Convert.ToSingle(textBoxFromX.Text), System.Convert.ToSingle(textBoxFromY.Text), ToAddressBox.Text, Extra);
            }
            if (!FromMode && ToMode)
            {
                ID = client.ServiceProxy.CreateOrder(NameBox.Text, PhoneBox.Text,FromAddressBox.Text, System.Convert.ToSingle(textBoxToX.Text), System.Convert.ToSingle(textBoxToY.Text), Extra);
            }
            if (FromMode && ToMode)
            {
                ID = client.ServiceProxy.CreateOrder(NameBox.Text, PhoneBox.Text, System.Convert.ToSingle(textBoxFromX.Text), System.Convert.ToSingle(textBoxFromY.Text), System.Convert.ToSingle(textBoxToX.Text), System.Convert.ToSingle(textBoxToY.Text), Extra);
            }
            if (ID == -1)
            {
                MessageBox.Show("Введены некорректные данные");
            }            
            else
            {
                OrderButton.Visible = false;
                labelPrice.Text = "Цена:" + System.Convert.ToString(client.ServiceProxy.GetPrice(ID));
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
                buttonMapFrom.Visible = false;
                buttonMapTo.Visible = false;
                labelOperatorPhone.Text = "Связь с оператором: " + client.ServiceProxy.GetOperatorPhone();
                buttonDriverPos.Visible = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Phase == 1)
            {
                Status = client.ServiceProxy.GetOrderStatus(ID);
                if (Status!=-1)
                {
                    Phase = 2;
                    labelProcessing.Text = "Ваш заказ принят";
                    labelDriverName.Text = "Имя водителя: "+client.ServiceProxy.GetDriverName(ID);
                    labelDriverPhone.Text = "Связь с водителем: " + client.ServiceProxy.GetDriverPhone(ID);
                }
            }
            if (Phase==2)
            {
                DriverPos = client.ServiceProxy.GetDriverPosition(ID);
                Map.DrawPos(DriverPos);
            }
        }

        private void buttonAbort_Click(object sender, EventArgs e)
        {
            client.ServiceProxy.AbortOrder(ID);
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
            Phase = 1;
            buttonAbort.Visible = false;
            if (FromMode)
            {
                buttonMapFrom.Visible = true;
            }
            if (ToMode)
            {
                buttonMapTo.Visible = true;
            }
            ID = -1;
            Status = -1;
            buttonDriverPos.Visible = false;
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
