 using CustomUIControls.Graphing;
    using LBSoft.IndustrialCtrls.Leds;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.IO.Ports;
    using System.Windows.Forms;
using System.Diagnostics;
using PrintControl;
using System.Drawing.Printing;
using System.IO;

namespace SESSCOPE
{
    public partial class MainForm : Form
    {
        string str = String.Empty;
        public MainForm()
        {
            InitializeComponent();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var _Settings = new SettingsForm();
            _Settings.ShowDialog();
        }
        int  FW  = 10;
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
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

                this.str = this.str + this.serialPort1.ReadExisting();
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
                        Debug.WriteLine(String.Format("number : {0}", str2));

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

                this.c2DPushGraph1.UpdateGraph();
                this.lbLed1.State = LBLed.LedState.Off;
            }
            catch (FormatException Ew)
            {
                Debug.WriteLine(String.Format("Error : {0}",Ew.Message));
                //timer1.Enabled = false;
            }

            catch (Exception Ew)
            {
                Debug.WriteLine(String.Format("Error : {0}", Ew.Message));
                //timer1.Enabled = false;
            }

            }

        private void lbButton1_Load(object sender, EventArgs e)
        {
           
        }

        private void lbButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.serialPort1.IsOpen)
                {
                    this.serialPort1.Close();
                }
                this.serialPort1.PortName = Properties.Settings.Default.PortName;
                this.serialPort1.BaudRate = Convert.ToInt32(Properties.Settings.Default.BaudRate);


                this.serialPort1.Open();
                this.timer1.Enabled = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Exception caught." + exception.ToString());

            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            this.c2DPushGraph1.AddLine(0, Color.White);
        
            //this.c2DPushGraph1.MinLabel =   Properties.Settings.Default.Min ;
            //this.c2DPushGraph1.MaxLabel = Properties.Settings.Default.Max; 
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            this.lbLed1.State = LBLed.LedState.On;
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var _ControlPrint = new ControlPrint(this);
            printPreviewDialog1.ShowIcon = false;
            printPreviewDialog1.Document = (PrintDocument)_ControlPrint;
            printPreviewDialog1.ShowDialog();
           
        }

        private void lbButton2_Load(object sender, EventArgs e)
        {
           
        }

        private void lbButton2_Click(object sender, EventArgs e)
        {
            try
            {
                this.textBox1.Clear();
                this.str = "";
                if (this.serialPort1.IsOpen)
                {
                    if (this.serialPort1.BytesToRead > 0)
                    {
                        this.serialPort1.ReadExisting();
                    }
                }
                this.c2DPushGraph1.RemoveLine(0);
                this.c2DPushGraph1.AddLine(0, Color.White);
                this.c2DPushGraph1.UpdateGraph();

            }
            finally
            {
            }
        }

        private void lbButton3_Click(object sender, EventArgs e)
        {
            try
            {
                var _File = new StreamWriter(DateTime.Now.ToString() + ".txt");
                _File.WriteLine(textBox1.Text);
            }
            finally
            {
            }
        }
           
            
        }
    }

