using System.Drawing;
using CustomUIControls.Graphing;
using LBSoft.IndustrialCtrls.Leds;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO.Ports;
using System.Windows.Forms;
namespace SESSCOPE
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.lbLed1 = new LBSoft.IndustrialCtrls.Leds.LBLed();
            this.lbButton1 = new LBSoft.IndustrialCtrls.Buttons.LBButton();
            this.c2DPushGraph1 = new CustomUIControls.Graphing.C2DPushGraph();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.lbButton2 = new LBSoft.IndustrialCtrls.Buttons.LBButton();
            this.lbButton3 = new LBSoft.IndustrialCtrls.Buttons.LBButton();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.printToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1071, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.printToolStripMenuItem.Text = "Print";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 491);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1071, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 356);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1071, 132);
            this.textBox1.TabIndex = 4;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // lbLed1
            // 
            this.lbLed1.BackColor = System.Drawing.Color.Transparent;
            this.lbLed1.BlinkInterval = 500;
            this.lbLed1.Label = "";
            this.lbLed1.LabelPosition = LBSoft.IndustrialCtrls.Leds.LBLed.LedLabelPosition.Top;
            this.lbLed1.LedColor = System.Drawing.Color.Red;
            this.lbLed1.LedSize = new System.Drawing.SizeF(10F, 10F);
            this.lbLed1.Location = new System.Drawing.Point(0, 494);
            this.lbLed1.Name = "lbLed1";
            this.lbLed1.Renderer = null;
            this.lbLed1.Size = new System.Drawing.Size(62, 19);
            this.lbLed1.State = LBSoft.IndustrialCtrls.Leds.LBLed.LedState.Off;
            this.lbLed1.Style = LBSoft.IndustrialCtrls.Leds.LBLed.LedStyle.Circular;
            this.lbLed1.TabIndex = 6;
            // 
            // lbButton1
            // 
            this.lbButton1.BackColor = System.Drawing.Color.Transparent;
            this.lbButton1.ButtonColor = System.Drawing.Color.White;
            this.lbButton1.ForeColor = System.Drawing.Color.Blue;
            this.lbButton1.Label = "Start";
            this.lbButton1.Location = new System.Drawing.Point(987, 36);
            this.lbButton1.Name = "lbButton1";
            this.lbButton1.Renderer = null;
            this.lbButton1.RepeatInterval = 100;
            this.lbButton1.RepeatState = false;
            this.lbButton1.Size = new System.Drawing.Size(50, 50);
            this.lbButton1.StartRepeatInterval = 500;
            this.lbButton1.State = LBSoft.IndustrialCtrls.Buttons.LBButton.ButtonState.Normal;
            this.lbButton1.Style = LBSoft.IndustrialCtrls.Buttons.LBButton.ButtonStyle.Circular;
            this.lbButton1.TabIndex = 2;
            this.lbButton1.Load += new System.EventHandler(this.lbButton1_Load);
            this.lbButton1.Click += new System.EventHandler(this.lbButton1_Click);
            // 
            // c2DPushGraph1
            // 
            this.c2DPushGraph1.AutoAdjustPeek = false;
            this.c2DPushGraph1.BackColor = System.Drawing.Color.Black;
            this.c2DPushGraph1.GridColor = System.Drawing.Color.Teal;
            this.c2DPushGraph1.GridSize = ((ushort)(10));
            this.c2DPushGraph1.HighQuality = true;
            this.c2DPushGraph1.LineInterval = ((ushort)(1));
            this.c2DPushGraph1.Location = new System.Drawing.Point(12, 36);
            this.c2DPushGraph1.MaxLabel = "5v";
            this.c2DPushGraph1.MaxPeekMagnitude = 5100;
            this.c2DPushGraph1.MinLabel = "0v";
            this.c2DPushGraph1.MinPeekMagnitude = -100;
            this.c2DPushGraph1.Name = "c2DPushGraph1";
            this.c2DPushGraph1.ShowGrid = true;
            this.c2DPushGraph1.ShowLabels = true;
            this.c2DPushGraph1.Size = new System.Drawing.Size(969, 314);
            this.c2DPushGraph1.TabIndex = 0;
            this.c2DPushGraph1.Text = "c2DPushGraph1";
            this.c2DPushGraph1.TextColor = System.Drawing.Color.Yellow;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // lbButton2
            // 
            this.lbButton2.BackColor = System.Drawing.Color.Transparent;
            this.lbButton2.ButtonColor = System.Drawing.Color.Red;
            this.lbButton2.Label = "";
            this.lbButton2.Location = new System.Drawing.Point(987, 103);
            this.lbButton2.Name = "lbButton2";
            this.lbButton2.Renderer = null;
            this.lbButton2.RepeatInterval = 100;
            this.lbButton2.RepeatState = false;
            this.lbButton2.Size = new System.Drawing.Size(50, 50);
            this.lbButton2.StartRepeatInterval = 500;
            this.lbButton2.State = LBSoft.IndustrialCtrls.Buttons.LBButton.ButtonState.Normal;
            this.lbButton2.Style = LBSoft.IndustrialCtrls.Buttons.LBButton.ButtonStyle.Circular;
            this.lbButton2.TabIndex = 7;
            this.lbButton2.Load += new System.EventHandler(this.lbButton2_Load);
            this.lbButton2.Click += new System.EventHandler(this.lbButton2_Click);
            // 
            // lbButton3
            // 
            this.lbButton3.BackColor = System.Drawing.Color.Transparent;
            this.lbButton3.ButtonColor = System.Drawing.Color.Blue;
            this.lbButton3.Label = "";
            this.lbButton3.Location = new System.Drawing.Point(987, 174);
            this.lbButton3.Name = "lbButton3";
            this.lbButton3.Renderer = null;
            this.lbButton3.RepeatInterval = 100;
            this.lbButton3.RepeatState = false;
            this.lbButton3.Size = new System.Drawing.Size(50, 50);
            this.lbButton3.StartRepeatInterval = 500;
            this.lbButton3.State = LBSoft.IndustrialCtrls.Buttons.LBButton.ButtonState.Normal;
            this.lbButton3.Style = LBSoft.IndustrialCtrls.Buttons.LBButton.ButtonStyle.Circular;
            this.lbButton3.TabIndex = 8;
            this.lbButton3.Click += new System.EventHandler(this.lbButton3_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 513);
            this.Controls.Add(this.lbButton3);
            this.Controls.Add(this.lbButton2);
            this.Controls.Add(this.lbLed1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lbButton1);
            this.Controls.Add(this.c2DPushGraph1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "UoB Scope";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomUIControls.Graphing.C2DPushGraph c2DPushGraph1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private LBSoft.IndustrialCtrls.Buttons.LBButton lbButton1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer timer1;
        private System.IO.Ports.SerialPort serialPort1;
        private LBSoft.IndustrialCtrls.Leds.LBLed lbLed1;
        private ToolStripMenuItem printToolStripMenuItem;
        private PrintPreviewDialog printPreviewDialog1;
        private LBSoft.IndustrialCtrls.Buttons.LBButton lbButton2;
        private LBSoft.IndustrialCtrls.Buttons.LBButton lbButton3;
    }
}

