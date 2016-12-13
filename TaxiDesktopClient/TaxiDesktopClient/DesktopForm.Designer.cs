namespace TaxiDesktopClient
{
    partial class DesktopForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelPhone = new System.Windows.Forms.Label();
            this.PhoneBox = new System.Windows.Forms.TextBox();
            this.labelFrom = new System.Windows.Forms.Label();
            this.labelTo = new System.Windows.Forms.Label();
            this.buttonFromAddress = new System.Windows.Forms.Button();
            this.buttonFromCoord = new System.Windows.Forms.Button();
            this.buttonToAddress = new System.Windows.Forms.Button();
            this.buttonToCoord = new System.Windows.Forms.Button();
            this.FromAddressBox = new System.Windows.Forms.TextBox();
            this.FromAddressLabel = new System.Windows.Forms.Label();
            this.labelFromX = new System.Windows.Forms.Label();
            this.textBoxFromX = new System.Windows.Forms.TextBox();
            this.labelFromY = new System.Windows.Forms.Label();
            this.textBoxFromY = new System.Windows.Forms.TextBox();
            this.textBoxToY = new System.Windows.Forms.TextBox();
            this.labelToY = new System.Windows.Forms.Label();
            this.textBoxToX = new System.Windows.Forms.TextBox();
            this.labelToX = new System.Windows.Forms.Label();
            this.ToAddressLabel = new System.Windows.Forms.Label();
            this.ToAddressBox = new System.Windows.Forms.TextBox();
            this.ExtracheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.Extralabel = new System.Windows.Forms.Label();
            this.OrderButton = new System.Windows.Forms.Button();
            this.labelPrice = new System.Windows.Forms.Label();
            this.labelProcessing = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelDriverName = new System.Windows.Forms.Label();
            this.labelDriverPhone = new System.Windows.Forms.Label();
            this.labelOperatorPhone = new System.Windows.Forms.Label();
            this.buttonAbort = new System.Windows.Forms.Button();
            this.buttonMapFrom = new System.Windows.Forms.Button();
            this.buttonMapTo = new System.Windows.Forms.Button();
            this.pictureBoxYandexMap = new System.Windows.Forms.PictureBox();
            this.buttonPlus = new System.Windows.Forms.Button();
            this.buttonMinus = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxYandexMap)).BeginInit();
            this.SuspendLayout();
            // 
            // NameBox
            // 
            this.NameBox.Location = new System.Drawing.Point(75, 12);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(527, 20);
            this.NameBox.TabIndex = 0;
            this.NameBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MoveKey);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(30, 15);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(37, 13);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "ФИО:";
            // 
            // labelPhone
            // 
            this.labelPhone.AutoSize = true;
            this.labelPhone.Location = new System.Drawing.Point(12, 47);
            this.labelPhone.Name = "labelPhone";
            this.labelPhone.Size = new System.Drawing.Size(55, 13);
            this.labelPhone.TabIndex = 2;
            this.labelPhone.Text = "Телефон:";
            // 
            // PhoneBox
            // 
            this.PhoneBox.Location = new System.Drawing.Point(75, 44);
            this.PhoneBox.Name = "PhoneBox";
            this.PhoneBox.Size = new System.Drawing.Size(231, 20);
            this.PhoneBox.TabIndex = 3;
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(15, 79);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(46, 13);
            this.labelFrom.TabIndex = 4;
            this.labelFrom.Text = "Откуда:";
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Location = new System.Drawing.Point(15, 133);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(34, 13);
            this.labelTo.TabIndex = 5;
            this.labelTo.Text = "Куда:";
            // 
            // buttonFromAddress
            // 
            this.buttonFromAddress.Location = new System.Drawing.Point(15, 95);
            this.buttonFromAddress.Name = "buttonFromAddress";
            this.buttonFromAddress.Size = new System.Drawing.Size(82, 35);
            this.buttonFromAddress.TabIndex = 6;
            this.buttonFromAddress.Text = "Адрес";
            this.buttonFromAddress.UseVisualStyleBackColor = true;
            this.buttonFromAddress.Click += new System.EventHandler(this.buttonFromAddress_Click);
            // 
            // buttonFromCoord
            // 
            this.buttonFromCoord.Location = new System.Drawing.Point(103, 95);
            this.buttonFromCoord.Name = "buttonFromCoord";
            this.buttonFromCoord.Size = new System.Drawing.Size(82, 35);
            this.buttonFromCoord.TabIndex = 7;
            this.buttonFromCoord.Text = "Координаты";
            this.buttonFromCoord.UseVisualStyleBackColor = true;
            this.buttonFromCoord.Click += new System.EventHandler(this.buttonFromCoord_Click);
            // 
            // buttonToAddress
            // 
            this.buttonToAddress.Location = new System.Drawing.Point(15, 149);
            this.buttonToAddress.Name = "buttonToAddress";
            this.buttonToAddress.Size = new System.Drawing.Size(82, 35);
            this.buttonToAddress.TabIndex = 8;
            this.buttonToAddress.Text = "Адрес";
            this.buttonToAddress.UseVisualStyleBackColor = true;
            this.buttonToAddress.Click += new System.EventHandler(this.buttonToAddress_Click);
            // 
            // buttonToCoord
            // 
            this.buttonToCoord.Location = new System.Drawing.Point(103, 149);
            this.buttonToCoord.Name = "buttonToCoord";
            this.buttonToCoord.Size = new System.Drawing.Size(82, 35);
            this.buttonToCoord.TabIndex = 9;
            this.buttonToCoord.Text = "Координаты";
            this.buttonToCoord.UseVisualStyleBackColor = true;
            this.buttonToCoord.Click += new System.EventHandler(this.buttonToCoord_Click);
            // 
            // FromAddressBox
            // 
            this.FromAddressBox.Location = new System.Drawing.Point(62, 103);
            this.FromAddressBox.Name = "FromAddressBox";
            this.FromAddressBox.Size = new System.Drawing.Size(100, 20);
            this.FromAddressBox.TabIndex = 10;
            this.FromAddressBox.Visible = false;
            this.FromAddressBox.TextChanged += new System.EventHandler(this.StartFinishTextChanged);
            // 
            // FromAddressLabel
            // 
            this.FromAddressLabel.AutoSize = true;
            this.FromAddressLabel.Location = new System.Drawing.Point(15, 106);
            this.FromAddressLabel.Name = "FromAddressLabel";
            this.FromAddressLabel.Size = new System.Drawing.Size(41, 13);
            this.FromAddressLabel.TabIndex = 11;
            this.FromAddressLabel.Text = "Адрес:";
            this.FromAddressLabel.Visible = false;
            // 
            // labelFromX
            // 
            this.labelFromX.AutoSize = true;
            this.labelFromX.Location = new System.Drawing.Point(16, 106);
            this.labelFromX.Name = "labelFromX";
            this.labelFromX.Size = new System.Drawing.Size(15, 13);
            this.labelFromX.TabIndex = 12;
            this.labelFromX.Text = "x:";
            this.labelFromX.Visible = false;
            // 
            // textBoxFromX
            // 
            this.textBoxFromX.Location = new System.Drawing.Point(37, 103);
            this.textBoxFromX.Name = "textBoxFromX";
            this.textBoxFromX.Size = new System.Drawing.Size(54, 20);
            this.textBoxFromX.TabIndex = 13;
            this.textBoxFromX.Visible = false;
            this.textBoxFromX.TextChanged += new System.EventHandler(this.StartFinishTextChanged);
            // 
            // labelFromY
            // 
            this.labelFromY.AutoSize = true;
            this.labelFromY.Location = new System.Drawing.Point(97, 106);
            this.labelFromY.Name = "labelFromY";
            this.labelFromY.Size = new System.Drawing.Size(15, 13);
            this.labelFromY.TabIndex = 14;
            this.labelFromY.Text = "y:";
            this.labelFromY.Visible = false;
            // 
            // textBoxFromY
            // 
            this.textBoxFromY.Location = new System.Drawing.Point(118, 103);
            this.textBoxFromY.Name = "textBoxFromY";
            this.textBoxFromY.Size = new System.Drawing.Size(54, 20);
            this.textBoxFromY.TabIndex = 15;
            this.textBoxFromY.Visible = false;
            this.textBoxFromY.TextChanged += new System.EventHandler(this.StartFinishTextChanged);
            // 
            // textBoxToY
            // 
            this.textBoxToY.Location = new System.Drawing.Point(118, 157);
            this.textBoxToY.Name = "textBoxToY";
            this.textBoxToY.Size = new System.Drawing.Size(54, 20);
            this.textBoxToY.TabIndex = 19;
            this.textBoxToY.Visible = false;
            this.textBoxToY.TextChanged += new System.EventHandler(this.StartFinishTextChanged);
            // 
            // labelToY
            // 
            this.labelToY.AutoSize = true;
            this.labelToY.Location = new System.Drawing.Point(97, 160);
            this.labelToY.Name = "labelToY";
            this.labelToY.Size = new System.Drawing.Size(15, 13);
            this.labelToY.TabIndex = 18;
            this.labelToY.Text = "y:";
            this.labelToY.Visible = false;
            // 
            // textBoxToX
            // 
            this.textBoxToX.Location = new System.Drawing.Point(37, 157);
            this.textBoxToX.Name = "textBoxToX";
            this.textBoxToX.Size = new System.Drawing.Size(54, 20);
            this.textBoxToX.TabIndex = 17;
            this.textBoxToX.Visible = false;
            this.textBoxToX.TextChanged += new System.EventHandler(this.StartFinishTextChanged);
            // 
            // labelToX
            // 
            this.labelToX.AutoSize = true;
            this.labelToX.Location = new System.Drawing.Point(16, 160);
            this.labelToX.Name = "labelToX";
            this.labelToX.Size = new System.Drawing.Size(15, 13);
            this.labelToX.TabIndex = 16;
            this.labelToX.Text = "x:";
            this.labelToX.Visible = false;
            // 
            // ToAddressLabel
            // 
            this.ToAddressLabel.AutoSize = true;
            this.ToAddressLabel.Location = new System.Drawing.Point(15, 160);
            this.ToAddressLabel.Name = "ToAddressLabel";
            this.ToAddressLabel.Size = new System.Drawing.Size(41, 13);
            this.ToAddressLabel.TabIndex = 21;
            this.ToAddressLabel.Text = "Адрес:";
            this.ToAddressLabel.Visible = false;
            // 
            // ToAddressBox
            // 
            this.ToAddressBox.Location = new System.Drawing.Point(62, 157);
            this.ToAddressBox.Name = "ToAddressBox";
            this.ToAddressBox.Size = new System.Drawing.Size(100, 20);
            this.ToAddressBox.TabIndex = 20;
            this.ToAddressBox.Visible = false;
            this.ToAddressBox.TextChanged += new System.EventHandler(this.StartFinishTextChanged);
            // 
            // ExtracheckedListBox
            // 
            this.ExtracheckedListBox.FormattingEnabled = true;
            this.ExtracheckedListBox.Items.AddRange(new object[] {
            "Детское кресло",
            "Подъемник",
            "Перевозка животного",
            "Багаж в салоне",
            "Кузов \"универсал\"",
            "Молчаливый водитель",
            "Водитель-женщина"});
            this.ExtracheckedListBox.Location = new System.Drawing.Point(15, 216);
            this.ExtracheckedListBox.Name = "ExtracheckedListBox";
            this.ExtracheckedListBox.Size = new System.Drawing.Size(170, 124);
            this.ExtracheckedListBox.TabIndex = 22;
            // 
            // Extralabel
            // 
            this.Extralabel.AutoSize = true;
            this.Extralabel.Location = new System.Drawing.Point(16, 200);
            this.Extralabel.Name = "Extralabel";
            this.Extralabel.Size = new System.Drawing.Size(160, 13);
            this.Extralabel.TabIndex = 23;
            this.Extralabel.Text = "Дополнительные требования:";
            // 
            // OrderButton
            // 
            this.OrderButton.Location = new System.Drawing.Point(13, 347);
            this.OrderButton.Name = "OrderButton";
            this.OrderButton.Size = new System.Drawing.Size(99, 47);
            this.OrderButton.TabIndex = 24;
            this.OrderButton.Text = "Заказать";
            this.OrderButton.UseVisualStyleBackColor = true;
            this.OrderButton.Click += new System.EventHandler(this.OrderButton_Click);
            // 
            // labelPrice
            // 
            this.labelPrice.AutoSize = true;
            this.labelPrice.Location = new System.Drawing.Point(126, 347);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(36, 13);
            this.labelPrice.TabIndex = 25;
            this.labelPrice.Text = "Цена:";
            // 
            // labelProcessing
            // 
            this.labelProcessing.AutoSize = true;
            this.labelProcessing.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelProcessing.Location = new System.Drawing.Point(264, 86);
            this.labelProcessing.Name = "labelProcessing";
            this.labelProcessing.Size = new System.Drawing.Size(539, 44);
            this.labelProcessing.TabIndex = 26;
            this.labelProcessing.Text = "Ваш заказ обрабатывается...";
            this.labelProcessing.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelDriverName
            // 
            this.labelDriverName.AutoSize = true;
            this.labelDriverName.Location = new System.Drawing.Point(271, 171);
            this.labelDriverName.Name = "labelDriverName";
            this.labelDriverName.Size = new System.Drawing.Size(81, 13);
            this.labelDriverName.TabIndex = 27;
            this.labelDriverName.Text = "Ваш водитель:";
            this.labelDriverName.Visible = false;
            // 
            // labelDriverPhone
            // 
            this.labelDriverPhone.AutoSize = true;
            this.labelDriverPhone.Location = new System.Drawing.Point(271, 193);
            this.labelDriverPhone.Name = "labelDriverPhone";
            this.labelDriverPhone.Size = new System.Drawing.Size(108, 13);
            this.labelDriverPhone.TabIndex = 28;
            this.labelDriverPhone.Text = "Связь с водителем:";
            this.labelDriverPhone.Visible = false;
            // 
            // labelOperatorPhone
            // 
            this.labelOperatorPhone.AutoSize = true;
            this.labelOperatorPhone.Location = new System.Drawing.Point(271, 149);
            this.labelOperatorPhone.Name = "labelOperatorPhone";
            this.labelOperatorPhone.Size = new System.Drawing.Size(114, 13);
            this.labelOperatorPhone.TabIndex = 29;
            this.labelOperatorPhone.Text = "Связь с оператором:";
            this.labelOperatorPhone.Visible = false;
            // 
            // buttonAbort
            // 
            this.buttonAbort.Location = new System.Drawing.Point(12, 400);
            this.buttonAbort.Name = "buttonAbort";
            this.buttonAbort.Size = new System.Drawing.Size(100, 47);
            this.buttonAbort.TabIndex = 30;
            this.buttonAbort.Text = "Отменить заказ";
            this.buttonAbort.UseVisualStyleBackColor = true;
            this.buttonAbort.Visible = false;
            this.buttonAbort.Click += new System.EventHandler(this.buttonAbort_Click);
            // 
            // buttonMapFrom
            // 
            this.buttonMapFrom.Location = new System.Drawing.Point(192, 95);
            this.buttonMapFrom.Name = "buttonMapFrom";
            this.buttonMapFrom.Size = new System.Drawing.Size(66, 35);
            this.buttonMapFrom.TabIndex = 32;
            this.buttonMapFrom.Text = "Указать на карте";
            this.buttonMapFrom.UseVisualStyleBackColor = true;
            this.buttonMapFrom.Visible = false;
            this.buttonMapFrom.Click += new System.EventHandler(this.buttonMapFrom_Click);
            // 
            // buttonMapTo
            // 
            this.buttonMapTo.Location = new System.Drawing.Point(192, 149);
            this.buttonMapTo.Name = "buttonMapTo";
            this.buttonMapTo.Size = new System.Drawing.Size(66, 35);
            this.buttonMapTo.TabIndex = 33;
            this.buttonMapTo.Text = "Указать на карте";
            this.buttonMapTo.UseVisualStyleBackColor = true;
            this.buttonMapTo.Visible = false;
            this.buttonMapTo.Click += new System.EventHandler(this.buttonMapTo_Click);
            // 
            // pictureBoxYandexMap
            // 
            this.pictureBoxYandexMap.Location = new System.Drawing.Point(272, 347);
            this.pictureBoxYandexMap.Name = "pictureBoxYandexMap";
            this.pictureBoxYandexMap.Size = new System.Drawing.Size(506, 307);
            this.pictureBoxYandexMap.TabIndex = 34;
            this.pictureBoxYandexMap.TabStop = false;
            this.pictureBoxYandexMap.Click += new System.EventHandler(this.pictureBoxYandexMap_Click);
            this.pictureBoxYandexMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MapMoveOn);
            this.pictureBoxYandexMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MapMoveEvent);
            this.pictureBoxYandexMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MapMoveOff);
            // 
            // buttonPlus
            // 
            this.buttonPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonPlus.Location = new System.Drawing.Point(791, 347);
            this.buttonPlus.Name = "buttonPlus";
            this.buttonPlus.Size = new System.Drawing.Size(63, 55);
            this.buttonPlus.TabIndex = 35;
            this.buttonPlus.Text = "+";
            this.buttonPlus.UseVisualStyleBackColor = true;
            this.buttonPlus.Click += new System.EventHandler(this.buttonPlus_Click);
            // 
            // buttonMinus
            // 
            this.buttonMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonMinus.Location = new System.Drawing.Point(791, 408);
            this.buttonMinus.Name = "buttonMinus";
            this.buttonMinus.Size = new System.Drawing.Size(63, 55);
            this.buttonMinus.TabIndex = 36;
            this.buttonMinus.Text = "-";
            this.buttonMinus.UseVisualStyleBackColor = true;
            this.buttonMinus.Click += new System.EventHandler(this.buttonMinus_Click);
            // 
            // DesktopForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 681);
            this.Controls.Add(this.buttonMinus);
            this.Controls.Add(this.buttonPlus);
            this.Controls.Add(this.pictureBoxYandexMap);
            this.Controls.Add(this.buttonMapTo);
            this.Controls.Add(this.buttonMapFrom);
            this.Controls.Add(this.buttonAbort);
            this.Controls.Add(this.labelOperatorPhone);
            this.Controls.Add(this.labelDriverPhone);
            this.Controls.Add(this.labelDriverName);
            this.Controls.Add(this.labelProcessing);
            this.Controls.Add(this.labelPrice);
            this.Controls.Add(this.OrderButton);
            this.Controls.Add(this.Extralabel);
            this.Controls.Add(this.ExtracheckedListBox);
            this.Controls.Add(this.buttonToAddress);
            this.Controls.Add(this.buttonToCoord);
            this.Controls.Add(this.ToAddressLabel);
            this.Controls.Add(this.ToAddressBox);
            this.Controls.Add(this.textBoxToY);
            this.Controls.Add(this.labelToY);
            this.Controls.Add(this.textBoxToX);
            this.Controls.Add(this.labelToX);
            this.Controls.Add(this.textBoxFromY);
            this.Controls.Add(this.labelFromY);
            this.Controls.Add(this.textBoxFromX);
            this.Controls.Add(this.labelFromX);
            this.Controls.Add(this.FromAddressLabel);
            this.Controls.Add(this.FromAddressBox);
            this.Controls.Add(this.labelTo);
            this.Controls.Add(this.labelFrom);
            this.Controls.Add(this.PhoneBox);
            this.Controls.Add(this.labelPhone);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.NameBox);
            this.Controls.Add(this.buttonFromCoord);
            this.Controls.Add(this.buttonFromAddress);
            this.Name = "DesktopForm";
            this.Text = "DesktopClient";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.FocusForm);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MoveKey);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxYandexMap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelPhone;
        private System.Windows.Forms.TextBox PhoneBox;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.Button buttonFromAddress;
        private System.Windows.Forms.Button buttonFromCoord;
        private System.Windows.Forms.Button buttonToAddress;
        private System.Windows.Forms.Button buttonToCoord;
        private System.Windows.Forms.TextBox FromAddressBox;
        private System.Windows.Forms.Label FromAddressLabel;
        private System.Windows.Forms.Label labelFromX;
        public System.Windows.Forms.TextBox textBoxFromX;
        private System.Windows.Forms.Label labelFromY;
        public System.Windows.Forms.TextBox textBoxFromY;
        public System.Windows.Forms.TextBox textBoxToY;
        private System.Windows.Forms.Label labelToY;
        public System.Windows.Forms.TextBox textBoxToX;
        private System.Windows.Forms.Label labelToX;
        private System.Windows.Forms.Label ToAddressLabel;
        private System.Windows.Forms.TextBox ToAddressBox;
        private System.Windows.Forms.CheckedListBox ExtracheckedListBox;
        private System.Windows.Forms.Label Extralabel;
        private System.Windows.Forms.Button OrderButton;
        private System.Windows.Forms.Label labelPrice;
        private System.Windows.Forms.Label labelProcessing;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelDriverName;
        private System.Windows.Forms.Label labelDriverPhone;
        private System.Windows.Forms.Label labelOperatorPhone;
        private System.Windows.Forms.Button buttonAbort;
        private System.Windows.Forms.Button buttonMapFrom;
        private System.Windows.Forms.Button buttonMapTo;
        private System.Windows.Forms.PictureBox pictureBoxYandexMap;
        private System.Windows.Forms.Button buttonPlus;
        private System.Windows.Forms.Button buttonMinus;
    }
}

