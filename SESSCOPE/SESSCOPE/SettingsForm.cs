using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace SESSCOPE
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            string[] portNames = SerialPort.GetPortNames();
            this.comboBox2.Items.AddRange(portNames);
            this.comboBox1.Items.AddRange(new object[] { "300", "600", "1200", "2400", "4800", "9600", "14400", "19200", "28800", "38400", "57600", "115200", "230400" });

            this.textBox3.Text = Properties.Settings.Default.Max;
            this.textBox2.Text = Properties.Settings.Default.Min;
            int j = 0;
            foreach (var item in comboBox2.Items)
            {
                if ((string)item == (string)Properties.Settings.Default.PortName)
                {
                    comboBox2.SelectedIndex = j;

                }
                j++;
            }

           
            int i=0;
            foreach (string item in comboBox1.Items)
            {
               
                if ((string)item ==  (string)Properties.Settings.Default.BaudRate)
                {
                    comboBox1.SelectedIndex = i;

                }
                i++;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.Max = this.textBox3.Text;
                Properties.Settings.Default.Min = this.textBox2.Text;

                Properties.Settings.Default.PortName = comboBox2.SelectedItem as string;
                Properties.Settings.Default.BaudRate = comboBox1.SelectedItem as string;
                Properties.Settings.Default.Save();
                MessageBox.Show("Settings Saved");
            }
            catch
            {
            }
        }
    }
}
