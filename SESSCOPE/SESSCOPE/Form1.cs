namespace SerialInterface
{
    using CustomUIControls.Graphing;
    using LBSoft.IndustrialCtrls.Leds;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.IO.Ports;
    using System.Windows.Forms;

    public class Form1 : Form
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private Button buttonClr;
        private C2DPushGraph c2DPushGraph1;
        private ComboBox comboBoxBaudRates;
        private ComboBox comboBoxPortNames;
        private IContainer components = null;
        private Label label1;
        private Label labelBaudrate;
        private LBLed lbLed1;
        private LBLed lbLed2;
        private int[] mydata = new int[0x200];
        private SerialPort myserialPort;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private double[] sensordata = new double[0x60];
        private string str;
        private TextBox textBox1;
        private Timer timer1;

        public Form1()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.InitSample();
            this.textBox1.Text = "START\r\n";
            for (int i = 0; i < 0x60; i++)
            {
                this.c2DPushGraph1.Push(((int) this.sensordata[i]) * 10, 0);
                this.textBox1.Text = this.textBox1.Text + string.Format("0x{0:x4}", (int) (this.sensordata[i] * 10.0)) + "\r\n";
            }
            this.textBox1.Text = this.textBox1.Text + "END\r\n";
            this.c2DPushGraph1.UpdateGraph();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.myserialPort.IsOpen)
            {
                this.myserialPort.Close();
            }
            this.myserialPort.PortName = this.comboBoxPortNames.SelectedItem.ToString();
            this.myserialPort.BaudRate = int.Parse(this.comboBoxBaudRates.SelectedItem.ToString());
            try
            {
                this.myserialPort.Open();
                this.lbLed1.State = LBLed.LedState.On;
                this.timer1.Enabled = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Exception caught." + exception.ToString());
                this.lbLed1.State = LBLed.LedState.Off;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.InitSample();
            if (this.myserialPort.IsOpen)
            {
                this.myserialPort.WriteLine("START\r");
                for (int i = 0; i < 0x60; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        this.myserialPort.WriteLine(string.Format("0x{0:x4}", (int) (this.sensordata[i] * 10.0)) + "\r");
                    }
                }
                this.myserialPort.WriteLine("END");
            }
            else
            {
                MessageBox.Show("Port is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void buttonClr_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
            this.str = "";
            if (this.myserialPort.IsOpen && (this.myserialPort.BytesToRead > 0))
            {
                this.myserialPort.ReadExisting();
            }
            this.c2DPushGraph1.RemoveLine(0);
            this.c2DPushGraph1.AddLine(0, Color.White);
            this.c2DPushGraph1.UpdateGraph();
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            this.comboBoxPortNames.Items.Clear();
            string[] portNames = SerialPort.GetPortNames();
            this.comboBoxPortNames.Items.AddRange(portNames);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.myserialPort.IsOpen)
            {
                this.myserialPort.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.comboBoxPortNames.Items.Clear();
            string[] portNames = SerialPort.GetPortNames();
            this.comboBoxPortNames.Items.AddRange(portNames);
            this.comboBoxPortNames.Text = portNames[portNames.Length - 1];
            this.c2DPushGraph1.AddLine(0, Color.White);
            this.InitMySampleData();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.c2DPushGraph1 = new C2DPushGraph();
            this.textBox1 = new TextBox();
            this.comboBoxPortNames = new ComboBox();
            this.myserialPort = new SerialPort(this.components);
            this.button2 = new Button();
            this.lbLed1 = new LBLed();
            this.lbLed2 = new LBLed();
            this.label1 = new Label();
            this.comboBoxBaudRates = new ComboBox();
            this.labelBaudrate = new Label();
            this.button1 = new Button();
            this.button3 = new Button();
            this.timer1 = new Timer(this.components);
            this.buttonClr = new Button();
            this.radioButton1 = new RadioButton();
            this.radioButton2 = new RadioButton();
            base.SuspendLayout();
            this.c2DPushGraph1.AccessibleRole = AccessibleRole.ScrollBar;
            this.c2DPushGraph1.AutoAdjustPeek = false;
            this.c2DPushGraph1.BackColor = Color.Black;
            this.c2DPushGraph1.GridColor = Color.Green;
            this.c2DPushGraph1.GridSize = 10;
            this.c2DPushGraph1.HighQuality = true;
            this.c2DPushGraph1.LineInterval = 1;
            this.c2DPushGraph1.Location = new Point(12, 0x24);
            this.c2DPushGraph1.MaxLabel = "5v";
            this.c2DPushGraph1.MaxPeekMagnitude = 0x13ec;
            this.c2DPushGraph1.MinLabel = "0v";
            this.c2DPushGraph1.MinPeekMagnitude = -100;
            this.c2DPushGraph1.Name = "c2DPushGraph1";
            this.c2DPushGraph1.ShowGrid = true;
            this.c2DPushGraph1.ShowLabels = true;
            this.c2DPushGraph1.Size = new Size(0x3a9, 0xeb);
            this.c2DPushGraph1.TabIndex = 0;
            this.c2DPushGraph1.Text = "c2DPushGraph1";
            this.c2DPushGraph1.TextColor = Color.Yellow;
            this.textBox1.AcceptsReturn = true;
            this.textBox1.AcceptsTab = true;
            this.textBox1.Location = new Point(0x3bb, 0x1f);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = ScrollBars.Both;
            this.textBox1.Size = new Size(0x83, 240);
            this.textBox1.TabIndex = 2;
            this.comboBoxPortNames.FormattingEnabled = true;
            this.comboBoxPortNames.Location = new Point(12, 290);
            this.comboBoxPortNames.Name = "comboBoxPortNames";
            this.comboBoxPortNames.Size = new Size(0x8b, 0x15);
            this.comboBoxPortNames.TabIndex = 3;
            this.comboBoxPortNames.MouseClick += new MouseEventHandler(this.comboBox1_MouseClick);
            this.myserialPort.ReadBufferSize = 0x10000;
            this.myserialPort.DataReceived += new SerialDataReceivedEventHandler(this.myserialPort_DataReceived);
            this.button2.Location = new Point(0x130, 290);
            this.button2.Name = "button2";
            this.button2.Size = new Size(50, 0x15);
            this.button2.TabIndex = 4;
            this.button2.Text = "Open";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.lbLed1.BackColor = Color.Transparent;
            this.lbLed1.BlinkInterval = 500;
            this.lbLed1.Label = "RDY";
            this.lbLed1.LabelPosition = LBLed.LedLabelPosition.Top;
            this.lbLed1.LedColor = Color.Red;
            this.lbLed1.LedSize = new SizeF(20f, 20f);
            this.lbLed1.Location = new Point(0xe9, 0x110);
            this.lbLed1.Name = "lbLed1";
            this.lbLed1.Renderer = null;
            this.lbLed1.Size = new Size(0x1d, 0x27);
            this.lbLed1.State = LBLed.LedState.Off;
            this.lbLed1.Style = LBLed.LedStyle.Circular;
            this.lbLed1.TabIndex = 5;
            this.lbLed2.BackColor = Color.Transparent;
            this.lbLed2.BlinkInterval = 500;
            this.lbLed2.Label = "RX";
            this.lbLed2.LabelPosition = LBLed.LedLabelPosition.Top;
            this.lbLed2.LedColor = Color.Red;
            this.lbLed2.LedSize = new SizeF(20f, 20f);
            this.lbLed2.Location = new Point(0x10c, 0x110);
            this.lbLed2.Name = "lbLed2";
            this.lbLed2.Renderer = null;
            this.lbLed2.Size = new Size(0x1d, 0x27);
            this.lbLed2.State = LBLed.LedState.Off;
            this.lbLed2.Style = LBLed.LedStyle.Circular;
            this.lbLed2.TabIndex = 6;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(9, 0x112);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x5b, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Select Serial Port:";
            this.comboBoxBaudRates.FormattingEnabled = true;
            this.comboBoxBaudRates.Items.AddRange(new object[] { "300", "600", "1200", "2400", "4800", "9600", "14400", "19200", "28800", "38400", "57600", "115200", "230400" });
            this.comboBoxBaudRates.Location = new Point(0x9f, 290);
            this.comboBoxBaudRates.Name = "comboBoxBaudRates";
            this.comboBoxBaudRates.Size = new Size(0x44, 0x15);
            this.comboBoxBaudRates.TabIndex = 8;
            this.comboBoxBaudRates.Text = "115200";
            this.labelBaudrate.AutoSize = true;
            this.labelBaudrate.Location = new Point(0x9c, 0x112);
            this.labelBaudrate.Name = "labelBaudrate";
            this.labelBaudrate.Size = new Size(0x49, 13);
            this.labelBaudrate.TabIndex = 9;
            this.labelBaudrate.Text = "Baudrate(bps)";
            this.button1.Location = new Point(0x20f, 290);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 10;
            this.button1.Text = "Sample";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button3.Location = new Point(0x1be, 290);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x4b, 0x17);
            this.button3.TabIndex = 12;
            this.button3.Text = "Serial Test";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new EventHandler(this.button3_Click);
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
            this.buttonClr.Location = new Point(0x3bb, 0x120);
            this.buttonClr.Name = "buttonClr";
            this.buttonClr.Size = new Size(0x71, 0x17);
            this.buttonClr.TabIndex = 13;
            this.buttonClr.Text = "Clear";
            this.buttonClr.UseVisualStyleBackColor = true;
            this.buttonClr.Click += new EventHandler(this.buttonClr_Click);
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new Point(12, 12);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new Size(0x34, 0x11);
            this.radioButton1.TabIndex = 14;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Lab 1";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new Point(0x59, 12);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new Size(0x34, 0x11);
            this.radioButton2.TabIndex = 15;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Lab 2";
            this.radioButton2.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x44a, 0x141);
            base.Controls.Add(this.radioButton2);
            base.Controls.Add(this.radioButton1);
            base.Controls.Add(this.buttonClr);
            base.Controls.Add(this.button3);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.labelBaudrate);
            base.Controls.Add(this.comboBoxBaudRates);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.lbLed2);
            base.Controls.Add(this.lbLed1);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.comboBoxPortNames);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.c2DPushGraph1);
            base.Name = "Form1";
            this.Text = "Serial Monitor v2.0";
            base.Load += new EventHandler(this.Form1_Load);
            base.FormClosing += new FormClosingEventHandler(this.Form1_FormClosing);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void InitMySampleData()
        {
            this.mydata[0] = 0x3f;
            this.mydata[1] = 0x3f;
            this.mydata[2] = 0x3f;
            this.mydata[3] = 0x44;
            this.mydata[4] = 0x44;
            this.mydata[5] = 0x3f;
            this.mydata[6] = 0x3f;
            this.mydata[7] = 0x44;
            this.mydata[8] = 0x3f;
            this.mydata[9] = 0x3f;
            this.mydata[10] = 0x3a;
            this.mydata[11] = 0x35;
            this.mydata[12] = 0x3a;
            this.mydata[13] = 0x35;
            this.mydata[14] = 0x35;
            this.mydata[15] = 0x30;
            this.mydata[0x10] = 0x30;
            this.mydata[0x11] = 0x2b;
            this.mydata[0x12] = 0x2b;
            this.mydata[0x13] = 0x35;
            this.mydata[20] = 0x3a;
            this.mydata[0x15] = 0x3a;
            this.mydata[0x16] = 0x3a;
            this.mydata[0x17] = 0x3a;
            this.mydata[0x18] = 0x7a3;
            this.mydata[0x19] = 0x115a;
            this.mydata[0x1a] = 0x11a4;
            this.mydata[0x1b] = 0x11b2;
            this.mydata[0x1c] = 0x11b7;
            this.mydata[0x1d] = 0x11cb;
            this.mydata[30] = 0x11ad;
            this.mydata[0x1f] = 0x11b7;
            this.mydata[0x20] = 0x11cb;
            this.mydata[0x21] = 0xf85;
            this.mydata[0x22] = 0xd7f;
            this.mydata[0x23] = 0xd6b;
            this.mydata[0x24] = 0xd70;
            this.mydata[0x25] = 0xd6b;
            this.mydata[0x26] = 0xb8c;
            this.mydata[0x27] = 0x9ed;
            this.mydata[40] = 0x9d9;
            this.mydata[0x29] = 0x9cb;
            this.mydata[0x2a] = 0x9cb;
            this.mydata[0x2b] = 0x8fe;
            this.mydata[0x2c] = 0x830;
            this.mydata[0x2d] = 0x82b;
            this.mydata[0x2e] = 0x826;
            this.mydata[0x2f] = 0x818;
            this.mydata[0x30] = 0x7a3;
            this.mydata[0x31] = 0x76d;
            this.mydata[50] = 0x763;
            this.mydata[0x33] = 0x754;
            this.mydata[0x34] = 0x74f;
            this.mydata[0x35] = 0x710;
            this.mydata[0x36] = 0x6da;
            this.mydata[0x37] = 0x6d5;
            this.mydata[0x38] = 0x6d0;
            this.mydata[0x39] = 0x6c7;
            this.mydata[0x3a] = 0x6ae;
            this.mydata[0x3b] = 0x691;
            this.mydata[60] = 0x696;
            this.mydata[0x3d] = 0x687;
            this.mydata[0x3e] = 0x682;
            this.mydata[0x3f] = 0x634;
            this.mydata[0x40] = 0x60d;
            this.mydata[0x41] = 0x603;
            this.mydata[0x42] = 0x603;
            this.mydata[0x43] = 0x5c8;
            this.mydata[0x44] = 0x5ab;
            this.mydata[0x45] = 0x5b5;
            this.mydata[70] = 0x59c;
            this.mydata[0x47] = 0x59c;
            this.mydata[0x48] = 0x57f;
            this.mydata[0x49] = 0x575;
            this.mydata[0x4a] = 0x56c;
            this.mydata[0x4b] = 0x55d;
            this.mydata[0x4c] = 0x558;
            this.mydata[0x4d] = 0x4a8;
            this.mydata[0x4e] = 0x48b;
            this.mydata[0x4f] = 0x481;
            this.mydata[80] = 0x477;
            this.mydata[0x51] = 0x472;
            this.mydata[0x52] = 0x3fd;
            this.mydata[0x53] = 0x3e5;
            this.mydata[0x54] = 0x3e0;
            this.mydata[0x55] = 0x3d1;
            this.mydata[0x56] = 0x3c7;
            this.mydata[0x57] = 0x402;
            this.mydata[0x58] = 0x402;
            this.mydata[0x59] = 0x3f8;
            this.mydata[90] = 0x3e9;
            this.mydata[0x5b] = 0x3e9;
            this.mydata[0x5c] = 0x411;
            this.mydata[0x5d] = 0x411;
            this.mydata[0x5e] = 0x40c;
            this.mydata[0x5f] = 0x3fd;
            this.mydata[0x60] = 0x402;
            this.mydata[0x61] = 0x3fd;
            this.mydata[0x62] = 0x3f8;
            this.mydata[0x63] = 0x3fd;
            this.mydata[100] = 0x3f8;
            this.mydata[0x65] = 0x481;
            this.mydata[0x66] = 0x486;
            this.mydata[0x67] = 0x490;
            this.mydata[0x68] = 0x490;
            this.mydata[0x69] = 0x495;
            this.mydata[0x6a] = 0x4d9;
            this.mydata[0x6b] = 0x4e3;
            this.mydata[0x6c] = 0x4e8;
            this.mydata[0x6d] = 0x4e3;
            this.mydata[110] = 0x4ec;
            this.mydata[0x6f] = 0x50a;
            this.mydata[0x70] = 0x514;
            this.mydata[0x71] = 0x518;
            this.mydata[0x72] = 0x514;
            this.mydata[0x73] = 0x51d;
            this.mydata[0x74] = 0x514;
            this.mydata[0x75] = 0x50f;
            this.mydata[0x76] = 0x522;
            this.mydata[0x77] = 0x522;
            this.mydata[120] = 0x522;
            this.mydata[0x79] = 0x52c;
            this.mydata[0x7a] = 0x531;
            this.mydata[0x7b] = 0x540;
            this.mydata[0x7c] = 0x53b;
            this.mydata[0x7d] = 0x540;
            this.mydata[0x7e] = 0x4d9;
            this.mydata[0x7f] = 0x4d4;
            this.mydata[0x80] = 0x4d9;
            this.mydata[0x81] = 0x4e3;
            this.mydata[130] = 0x43d;
            this.mydata[0x83] = 0x433;
            this.mydata[0x84] = 0x43d;
            this.mydata[0x85] = 0x438;
            this.mydata[0x86] = 0x446;
            this.mydata[0x87] = 0x553;
            this.mydata[0x88] = 0x567;
            this.mydata[0x89] = 0x570;
            this.mydata[0x8a] = 0x567;
            this.mydata[0x8b] = 0x575;
            this.mydata[140] = 0x665;
            this.mydata[0x8d] = 0x66a;
            this.mydata[0x8e] = 0x674;
            this.mydata[0x8f] = 0x674;
            this.mydata[0x90] = 0x682;
            this.mydata[0x91] = 0x732;
            this.mydata[0x92] = 0x732;
            this.mydata[0x93] = 0x741;
            this.mydata[0x94] = 0x746;
            this.mydata[0x95] = 0x74b;
            this.mydata[150] = 0x88d;
            this.mydata[0x97] = 0x897;
            this.mydata[0x98] = 0x89c;
            this.mydata[0x99] = 0x8a6;
            this.mydata[0x9a] = 0x8ea;
            this.mydata[0x9b] = 0x97d;
            this.mydata[0x9c] = 0x982;
            this.mydata[0x9d] = 0x98b;
            this.mydata[0x9e] = 0x98b;
            this.mydata[0x9f] = 0x9a9;
            this.mydata[160] = 0x9c6;
            this.mydata[0xa1] = 0x9d5;
            this.mydata[0xa2] = 0x9e3;
            this.mydata[0xa3] = 0x9f2;
            this.mydata[0xa4] = 0xa01;
            this.mydata[0xa5] = 0xa14;
            this.mydata[0xa6] = 0xa0f;
            this.mydata[0xa7] = 0xa14;
            this.mydata[0xa8] = 0xa19;
            this.mydata[0xa9] = 0xa0f;
            this.mydata[170] = 0xa14;
            this.mydata[0xab] = 0xa0f;
            this.mydata[0xac] = 0xa01;
            this.mydata[0xad] = 0x9f2;
            this.mydata[0xae] = 0x9e3;
            this.mydata[0xaf] = 0x9e3;
            this.mydata[0xb0] = 0x9d0;
            this.mydata[0xb1] = 0x9d9;
            this.mydata[0xb2] = 0x9e8;
            this.mydata[0xb3] = 0x9fc;
            this.mydata[180] = 0xa01;
            this.mydata[0xb5] = 0x9fc;
            this.mydata[0xb6] = 0x9e8;
            this.mydata[0xb7] = 0xa0f;
            this.mydata[0xb8] = 0xa14;
            this.mydata[0xb9] = 0xa14;
            this.mydata[0xba] = 0xa14;
            this.mydata[0xbb] = 0xa0a;
            this.mydata[0xbc] = 0xa23;
            this.mydata[0xbd] = 0xa23;
            this.mydata[190] = 0xa2d;
            this.mydata[0xbf] = 0xa1e;
            this.mydata[0xc0] = 0xa23;
            this.mydata[0xc1] = 0xa3b;
            this.mydata[0xc2] = 0xa31;
            this.mydata[0xc3] = 0xa2d;
            this.mydata[0xc4] = 0xa28;
            this.mydata[0xc5] = 0xa4f;
            this.mydata[0xc6] = 0xa4f;
            this.mydata[0xc7] = 0xa59;
            this.mydata[200] = 0xa45;
            this.mydata[0xc9] = 0xa4f;
            this.mydata[0xca] = 0xa40;
            this.mydata[0xcb] = 0xa31;
            this.mydata[0xcc] = 0xa36;
            this.mydata[0xcd] = 0xa19;
            this.mydata[0xce] = 0xa28;
            this.mydata[0xcf] = 0xa0a;
            this.mydata[0xd0] = 0x9e8;
            this.mydata[0xd1] = 0x9fc;
            this.mydata[210] = 0x9de;
            this.mydata[0xd3] = 0x9e8;
            this.mydata[0xd4] = 0x9c6;
            this.mydata[0xd5] = 0x9a4;
            this.mydata[0xd6] = 0x9a4;
            this.mydata[0xd7] = 0x995;
            this.mydata[0xd8] = 0x99a;
            this.mydata[0xd9] = 0x978;
            this.mydata[0xda] = 0x956;
            this.mydata[0xdb] = 0x95f;
            this.mydata[220] = 0x94c;
            this.mydata[0xdd] = 0x956;
            this.mydata[0xde] = 0x8ef;
            this.mydata[0xdf] = 0x8d6;
            this.mydata[0xe0] = 0x8d2;
            this.mydata[0xe1] = 0x8c8;
            this.mydata[0xe2] = 0x88d;
            this.mydata[0xe3] = 0x87e;
            this.mydata[0xe4] = 0x87a;
            this.mydata[0xe5] = 0x875;
            this.mydata[230] = 0x861;
            this.mydata[0xe7] = 0x83a;
            this.mydata[0xe8] = 0x83a;
            this.mydata[0xe9] = 0x830;
            this.mydata[0xea] = 0x82b;
            this.mydata[0xeb] = 0x818;
            this.mydata[0xec] = 0x7dd;
            this.mydata[0xed] = 0x7ca;
            this.mydata[0xee] = 0x7c0;
            this.mydata[0xef] = 0x7c5;
            this.mydata[240] = 0x7bb;
            this.mydata[0xf1] = 0x76d;
            this.mydata[0xf2] = 0x754;
            this.mydata[0xf3] = 0x75e;
            this.mydata[0xf4] = 0x768;
            this.mydata[0xf5] = 0x763;
            this.mydata[0xf6] = 0x70b;
            this.mydata[0xf7] = 0x706;
            this.mydata[0xf8] = 0x70b;
            this.mydata[0xf9] = 0x710;
            this.mydata[250] = 0x710;
            this.mydata[0xfb] = 0x687;
            this.mydata[0xfc] = 0x687;
            this.mydata[0xfd] = 0x682;
            this.mydata[0xfe] = 0x687;
            this.mydata[0xff] = 0x691;
            this.mydata[0x100] = 0x5ba;
            this.mydata[0x101] = 0x5bf;
            this.mydata[0x102] = 0x5bf;
            this.mydata[0x103] = 0x5ba;
            this.mydata[260] = 0x536;
            this.mydata[0x105] = 0x531;
            this.mydata[0x106] = 0x52c;
            this.mydata[0x107] = 0x536;
            this.mydata[0x108] = 0x536;
            this.mydata[0x109] = 0x4f6;
            this.mydata[0x10a] = 0x4e8;
            this.mydata[0x10b] = 0x4f1;
            this.mydata[0x10c] = 0x4fb;
            this.mydata[0x10d] = 0x500;
            this.mydata[270] = 0x4e3;
            this.mydata[0x10f] = 0x4f1;
            this.mydata[0x110] = 0x4f6;
            this.mydata[0x111] = 0x4f1;
            this.mydata[0x112] = 0x4f6;
            this.mydata[0x113] = 0x4c5;
            this.mydata[0x114] = 0x4bc;
            this.mydata[0x115] = 0x4ca;
            this.mydata[0x116] = 0x4ca;
            this.mydata[0x117] = 0x4cf;
            this.mydata[280] = 0x7a;
            this.mydata[0x119] = 0x4e;
            this.mydata[0x11a] = 0x4e;
            this.mydata[0x11b] = 0x49;
            this.mydata[0x11c] = 0x4e;
            this.mydata[0x11d] = 0x3f;
            this.mydata[0x11e] = 0x44;
            this.mydata[0x11f] = 0x44;
            this.mydata[0x120] = 0x3f;
            this.mydata[0x121] = 0x30;
            this.mydata[290] = 0x2b;
            this.mydata[0x123] = 0x30;
            this.mydata[0x124] = 0x2b;
            this.mydata[0x125] = 0x30;
            this.mydata[0x126] = 0x35;
            this.mydata[0x127] = 0x3a;
            this.mydata[0x128] = 0x3a;
            this.mydata[0x129] = 0x3a;
            this.mydata[0x12a] = 0x3a;
            this.mydata[0x12b] = 0x4e;
            this.mydata[300] = 0x4e;
            this.mydata[0x12d] = 0x4e;
            this.mydata[0x12e] = 0x4e;
            this.mydata[0x12f] = 0x44;
            this.mydata[0x130] = 0x22;
            this.mydata[0x131] = 0x27;
            this.mydata[0x132] = 0x22;
            this.mydata[0x133] = 0x22;
            this.mydata[0x134] = 0x30;
            this.mydata[0x135] = 0x44;
            this.mydata[310] = 0x44;
            this.mydata[0x137] = 0x44;
            this.mydata[0x138] = 0x44;
            this.mydata[0x139] = 0x44;
            this.mydata[0x13a] = 0x44;
            this.mydata[0x13b] = 0x44;
            this.mydata[0x13c] = 0x49;
            this.mydata[0x13d] = 0x44;
            this.mydata[0x13e] = 0x3a;
            this.mydata[0x13f] = 0x30;
            this.mydata[320] = 0x30;
            this.mydata[0x141] = 0x35;
            this.mydata[0x142] = 0x3a;
            this.mydata[0x143] = 0x3a;
            this.mydata[0x144] = 0x3a;
            this.mydata[0x145] = 0x3f;
            this.mydata[0x146] = 0x3a;
            this.mydata[0x147] = 0x4e;
            this.mydata[0x148] = 0x61;
            this.mydata[0x149] = 0x5c;
            this.mydata[330] = 0x61;
            this.mydata[0x14b] = 0x61;
            this.mydata[0x14c] = 0x4e;
            this.mydata[0x14d] = 0x3f;
            this.mydata[0x14e] = 0x3a;
            this.mydata[0x14f] = 0x3f;
            this.mydata[0x150] = 0x3a;
            this.mydata[0x151] = 0x44;
            this.mydata[0x152] = 0x49;
            this.mydata[0x153] = 0x4e;
            this.mydata[340] = 0x4e;
            this.mydata[0x155] = 0x49;
            this.mydata[0x156] = 0x4e;
            this.mydata[0x157] = 0x4e;
            this.mydata[0x158] = 0x49;
            this.mydata[0x159] = 0x49;
            this.mydata[0x15a] = 0x49;
            this.mydata[0x15b] = 0x2b;
            this.mydata[0x15c] = 0x1d;
            this.mydata[0x15d] = 0x1d;
            this.mydata[350] = 0x1d;
            this.mydata[0x15f] = 0x1d;
            this.mydata[0x160] = 0x3f;
            this.mydata[0x161] = 0x44;
            this.mydata[0x162] = 0x44;
            this.mydata[0x163] = 0x3f;
            this.mydata[0x164] = 0x49;
            this.mydata[0x165] = 0x53;
            this.mydata[0x166] = 0x4e;
            this.mydata[0x167] = 0x4e;
            this.mydata[360] = 0x4e;
            this.mydata[0x169] = 0x4e;
            this.mydata[0x16a] = 0x4e;
            this.mydata[0x16b] = 0x4e;
            this.mydata[0x16c] = 0x4e;
            this.mydata[0x16d] = 0x4e;
            this.mydata[0x16e] = 0x53;
            this.mydata[0x16f] = 0x53;
            this.mydata[0x170] = 0x53;
            this.mydata[0x171] = 0x4e;
            this.mydata[370] = 0x57;
            this.mydata[0x173] = 0x49;
            this.mydata[0x174] = 0x44;
            this.mydata[0x175] = 0x44;
            this.mydata[0x176] = 0x44;
            this.mydata[0x177] = 0x44;
            this.mydata[0x178] = 0x30;
            this.mydata[0x179] = 0x2b;
            this.mydata[0x17a] = 0x2b;
            this.mydata[0x17b] = 0x2b;
            this.mydata[380] = 0x2b;
            this.mydata[0x17d] = 0x2b;
            this.mydata[0x17e] = 0x2b;
            this.mydata[0x17f] = 0x2b;
            this.mydata[0x180] = 0x30;
            this.mydata[0x181] = 0x3f;
            this.mydata[0x182] = 0x35;
            this.mydata[0x183] = 0x30;
            this.mydata[0x184] = 0x35;
            this.mydata[0x185] = 0x30;
            this.mydata[390] = 0x3a;
            this.mydata[0x187] = 0x35;
            this.mydata[0x188] = 0x3a;
            this.mydata[0x189] = 0x3a;
            this.mydata[0x18a] = 0x3a;
            this.mydata[0x18b] = 0x35;
            this.mydata[0x18c] = 0x30;
            this.mydata[0x18d] = 0x30;
            this.mydata[0x18e] = 0x35;
            this.mydata[0x18f] = 0x30;
            this.mydata[400] = 0x5c;
            this.mydata[0x191] = 0x61;
            this.mydata[0x192] = 0x5c;
            this.mydata[0x193] = 0x61;
            this.mydata[0x194] = 0x61;
            this.mydata[0x195] = 0x3a;
            this.mydata[0x196] = 0x35;
            this.mydata[0x197] = 0x35;
            this.mydata[0x198] = 0x30;
            this.mydata[0x199] = 0x35;
            this.mydata[410] = 0x3f;
            this.mydata[0x19b] = 0x3a;
            this.mydata[0x19c] = 0x3f;
            this.mydata[0x19d] = 0x3a;
            this.mydata[0x19e] = 0x3a;
            this.mydata[0x19f] = 0x49;
            this.mydata[0x1a0] = 0x44;
            this.mydata[0x1a1] = 0x44;
            this.mydata[0x1a2] = 0x49;
            this.mydata[0x1a3] = 0x49;
            this.mydata[420] = 0x44;
            this.mydata[0x1a5] = 0x49;
            this.mydata[0x1a6] = 0x49;
            this.mydata[0x1a7] = 0x49;
            this.mydata[0x1a8] = 0x5c;
            this.mydata[0x1a9] = 0x61;
            this.mydata[0x1aa] = 0x5c;
            this.mydata[0x1ab] = 0x61;
            this.mydata[0x1ac] = 0x5c;
            this.mydata[0x1ad] = 0x3f;
            this.mydata[430] = 0x44;
            this.mydata[0x1af] = 0x3f;
            this.mydata[0x1b0] = 0x3f;
            this.mydata[0x1b1] = 0x44;
            this.mydata[0x1b2] = 0x44;
            this.mydata[0x1b3] = 0x44;
            this.mydata[0x1b4] = 0x3f;
            this.mydata[0x1b5] = 0x44;
            this.mydata[0x1b6] = 0x3f;
            this.mydata[0x1b7] = 0x3a;
            this.mydata[440] = 0x35;
            this.mydata[0x1b9] = 0x35;
            this.mydata[0x1ba] = 0x35;
            this.mydata[0x1bb] = 0x3a;
            this.mydata[0x1bc] = 0x35;
            this.mydata[0x1bd] = 0x3a;
            this.mydata[0x1be] = 0x35;
            this.mydata[0x1bf] = 0x3a;
            this.mydata[0x1c0] = 0x4e;
            this.mydata[0x1c1] = 0x4e;
            this.mydata[450] = 0x4e;
            this.mydata[0x1c3] = 0x4e;
            this.mydata[0x1c4] = 0x49;
            this.mydata[0x1c5] = 0x3a;
            this.mydata[0x1c6] = 0x35;
            this.mydata[0x1c7] = 0x3a;
            this.mydata[0x1c8] = 0x3a;
            this.mydata[0x1c9] = 0x49;
            this.mydata[0x1ca] = 0x57;
            this.mydata[0x1cb] = 0x57;
            this.mydata[460] = 0x5c;
            this.mydata[0x1cd] = 0x5c;
            this.mydata[0x1ce] = 410;
            this.mydata[0x1cf] = 0x44b;
            this.mydata[0x1d0] = 0x45a;
            this.mydata[0x1d1] = 0x45a;
            this.mydata[0x1d2] = 0x464;
            this.mydata[0x1d3] = 0x469;
            this.mydata[0x1d4] = 0x469;
            this.mydata[0x1d5] = 0x464;
            this.mydata[470] = 0x46d;
            this.mydata[0x1d7] = 0x46d;
            this.mydata[0x1d8] = 0x46d;
            this.mydata[0x1d9] = 0x46d;
            this.mydata[0x1da] = 0x46d;
            this.mydata[0x1db] = 0x477;
            this.mydata[0x1dc] = 0x481;
            this.mydata[0x1dd] = 0x477;
            this.mydata[0x1de] = 0x47c;
            this.mydata[0x1df] = 0x481;
            this.mydata[480] = 0x48b;
            this.mydata[0x1e1] = 0x486;
            this.mydata[0x1e2] = 0x486;
            this.mydata[0x1e3] = 0x48b;
            this.mydata[0x1e4] = 0x495;
            this.mydata[0x1e5] = 0x499;
            this.mydata[0x1e6] = 0x49e;
            this.mydata[0x1e7] = 0x49e;
            this.mydata[0x1e8] = 0x4a3;
            this.mydata[0x1e9] = 0x49e;
            this.mydata[490] = 0x49e;
            this.mydata[0x1eb] = 0x49e;
            this.mydata[0x1ec] = 0x4a8;
            this.mydata[0x1ed] = 0x4a8;
            this.mydata[0x1ee] = 0x4ad;
            this.mydata[0x1ef] = 0x4b7;
            this.mydata[0x1f0] = 0x4b2;
            this.mydata[0x1f1] = 0x4b2;
            this.mydata[0x1f2] = 0x4b2;
            this.mydata[0x1f3] = 0x4b2;
            this.mydata[500] = 0x4bc;
            this.mydata[0x1f5] = 0x4c1;
            this.mydata[0x1f6] = 0x4c1;
            this.mydata[0x1f7] = 0x4ca;
            this.mydata[0x1f8] = 0x4ca;
            this.mydata[0x1f9] = 0x4c5;
            this.mydata[0x1fa] = 0x4cf;
            this.mydata[0x1fb] = 0x4ca;
            this.mydata[0x1fc] = 0x4d9;
            this.mydata[0x1fd] = 0x4d9;
            this.mydata[510] = 0x4cf;
            this.mydata[0x1ff] = 0x4d4;
        }

        private void InitSample()
        {
            this.sensordata[0] = 6.0;
            this.sensordata[1] = 6.0;
            this.sensordata[2] = 5.0;
            this.sensordata[3] = 4.0;
            this.sensordata[4] = 5.0;
            this.sensordata[5] = 828.0;
            this.sensordata[6] = 464.0;
            this.sensordata[7] = 305.0;
            this.sensordata[8] = 216.0;
            this.sensordata[9] = 174.0;
            this.sensordata[10] = 156.0;
            this.sensordata[11] = 145.0;
            this.sensordata[12] = 141.0;
            this.sensordata[13] = 132.0;
            this.sensordata[14] = 125.0;
            this.sensordata[15] = 122.0;
            this.sensordata[0x10] = 102.0;
            this.sensordata[0x11] = 89.0;
            this.sensordata[0x12] = 96.0;
            this.sensordata[0x13] = 102.0;
            this.sensordata[20] = 106.0;
            this.sensordata[0x15] = 119.0;
            this.sensordata[0x16] = 126.0;
            this.sensordata[0x17] = 129.0;
            this.sensordata[0x18] = 128.0;
            this.sensordata[0x19] = 129.0;
            this.sensordata[0x1a] = 119.0;
            this.sensordata[0x1b] = 103.0;
            this.sensordata[0x1c] = 129.0;
            this.sensordata[0x1d] = 150.0;
            this.sensordata[30] = 165.0;
            this.sensordata[0x1f] = 191.0;
            this.sensordata[0x20] = 207.0;
            this.sensordata[0x21] = 210.0;
            this.sensordata[0x22] = 212.0;
            this.sensordata[0x23] = 213.0;
            this.sensordata[0x24] = 210.0;
            this.sensordata[0x25] = 216.0;
            this.sensordata[0x26] = 221.0;
            this.sensordata[0x27] = 225.0;
            this.sensordata[40] = 229.0;
            this.sensordata[0x29] = 234.0;
            this.sensordata[0x2a] = 233.0;
            this.sensordata[0x2b] = 229.0;
            this.sensordata[0x2c] = 224.0;
            this.sensordata[0x2d] = 220.0;
            this.sensordata[0x2e] = 210.0;
            this.sensordata[0x2f] = 205.0;
            this.sensordata[0x30] = 201.0;
            this.sensordata[0x31] = 195.0;
            this.sensordata[50] = 188.0;
            this.sensordata[0x33] = 178.0;
            this.sensordata[0x34] = 164.0;
            this.sensordata[0x35] = 143.0;
            this.sensordata[0x36] = 129.0;
            this.sensordata[0x37] = 122.0;
            this.sensordata[0x38] = 120.0;
            this.sensordata[0x39] = 115.0;
            this.sensordata[0x3a] = 7.0;
            this.sensordata[0x3b] = 6.0;
            this.sensordata[60] = 4.0;
            this.sensordata[0x3d] = 5.0;
            this.sensordata[0x3e] = 7.0;
            this.sensordata[0x3f] = 3.0;
            this.sensordata[0x40] = 6.0;
            this.sensordata[0x41] = 6.0;
            this.sensordata[0x42] = 4.0;
            this.sensordata[0x43] = 5.0;
            this.sensordata[0x44] = 8.0;
            this.sensordata[0x45] = 5.0;
            this.sensordata[70] = 6.0;
            this.sensordata[0x47] = 6.0;
            this.sensordata[0x48] = 2.0;
            this.sensordata[0x49] = 5.0;
            this.sensordata[0x4a] = 6.0;
            this.sensordata[0x4b] = 6.0;
            this.sensordata[0x4c] = 6.0;
            this.sensordata[0x4d] = 5.0;
            this.sensordata[0x4e] = 3.0;
            this.sensordata[0x4f] = 3.0;
            this.sensordata[80] = 5.0;
            this.sensordata[0x51] = 6.0;
            this.sensordata[0x52] = 5.0;
            this.sensordata[0x53] = 10.0;
            this.sensordata[0x54] = 5.0;
            this.sensordata[0x55] = 6.0;
            this.sensordata[0x56] = 7.0;
            this.sensordata[0x57] = 7.0;
            this.sensordata[0x58] = 9.0;
            this.sensordata[0x59] = 6.0;
            this.sensordata[90] = 6.0;
            this.sensordata[0x5b] = 5.0;
            this.sensordata[0x5c] = 5.0;
            this.sensordata[0x5d] = 7.0;
            this.sensordata[0x5e] = 5.0;
            this.sensordata[0x5f] = 8.0;
        }

        private void myserialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            this.lbLed2.State = LBLed.LedState.On;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int index;
            int length;
            string str;
            int num4;
            List<double> list;
            char[] chArray;
            string str2;
            int num6;
            double[] numArray;
            if (this.radioButton2.Checked)
            {
                if (this.myserialPort.BytesToRead > 0)
                {
                    this.str = this.str + this.myserialPort.ReadExisting();
                    index = this.str.IndexOf("0x");
                    int num2 = this.str.IndexOf("END");
                    if (num2 == -1)
                    {
                        return;
                    }
                    this.textBox1.Text = this.textBox1.Text + this.str;
                    length = this.str.Length;
                    str = this.str.Substring(index, num2 - index);
                    if (str != null)
                    {
                        num4 = 0;
                        list = new List<double>();
                        while (str.Length > 10)
                        {
                            num4 = str.IndexOf("0x");
                            chArray = new char[4];
                            str.CopyTo(num4 + 2, chArray, 0, 4);
                            str2 = new string(chArray);
                            num6 = (int) (((int.Parse(str2, NumberStyles.AllowHexSpecifier) * 5.0) / 1023.0) * 1000.0);
                            this.c2DPushGraph1.Push(num6, 0);
                            list.Add((double) num6);
                            str = str.Substring(num4 + 6);
                        }
                        numArray = list.ToArray();
                        this.lbLed2.State = LBLed.LedState.Off;
                        str = null;
                        this.str = null;
                    }
                }
            }
            else if (this.radioButton1.Checked && (this.myserialPort.BytesToRead > 0))
            {
                try
                {
                    this.str = this.str + this.myserialPort.ReadExisting();
                    this.textBox1.Text = this.textBox1.Text + this.str;
                    index = this.str.IndexOf("0x");
                    length = this.str.Length;
                    str = this.str;
                    if (str != null)
                    {
                        num4 = 0;
                        list = new List<double>();
                        while (str.Length > 10)
                        {
                            num4 = str.IndexOf("0x");
                            chArray = new char[4];
                            str.CopyTo(num4 + 2, chArray, 0, 4);
                            str2 = new string(chArray);
                            num6 = (int)(((int.Parse(str2, NumberStyles.AllowHexSpecifier) * 5.0) / 1023.0) * 1000.0);
                            this.c2DPushGraph1.Push(num6, 0);
                            list.Add((double)num6);
                            if (str.Length > 6)
                            {
                                str = str.Substring(num4 + 6);
                            }
                            else
                            {
                                str = "";
                            }
                        }
                        numArray = list.ToArray();
                        str = null;
                        this.str = null;
                    }
                }
                finally
                {
                    this.c2DPushGraph1.UpdateGraph();
                    this.lbLed2.State = LBLed.LedState.Off;
                }
            }
            
        }
    }
}

