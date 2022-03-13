namespace GGMSaveManager
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button8 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.openFileDialog3 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog4 = new System.Windows.Forms.OpenFileDialog();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.chkAdvancedFeatures = new System.Windows.Forms.CheckBox();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button18 = new System.Windows.Forms.Button();
            this.button19 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.button23 = new System.Windows.Forms.Button();
            this.button24 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button25 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button26 = new System.Windows.Forms.Button();
            this.button27 = new System.Windows.Forms.Button();
            this.button28 = new System.Windows.Forms.Button();
            this.button29 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 27);
            this.button1.TabIndex = 0;
            this.button1.Text = "Open 1st File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "ggmsav";
            this.openFileDialog1.Filter = "GGM save data (*.ggmsav)|*.GGMSAV;save.bin|All files (*.*)|*.*";
            this.openFileDialog1.InitialDirectory = "@\"C:\\\"";
            this.openFileDialog1.RestoreDirectory = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(232, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(37, 27);
            this.button2.TabIndex = 2;
            this.button2.Text = "New";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(111, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(51, 27);
            this.button3.TabIndex = 3;
            this.button3.Text = "Save";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(58, 76);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(47, 27);
            this.button4.TabIndex = 4;
            this.button4.Text = "Copy";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Enabled = false;
            this.button5.Location = new System.Drawing.Point(111, 76);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(47, 27);
            this.button5.TabIndex = 5;
            this.button5.Text = "Paste";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Enabled = false;
            this.button6.Location = new System.Drawing.Point(164, 76);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(47, 27);
            this.button6.TabIndex = 6;
            this.button6.Text = "Clear";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Enabled = false;
            this.button7.Location = new System.Drawing.Point(301, 76);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(87, 27);
            this.button7.TabIndex = 7;
            this.button7.Text = "Export SRAM";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 45);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(376, 20);
            this.textBox1.TabIndex = 8;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(275, 12);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(113, 27);
            this.button8.TabIndex = 9;
            this.button8.Text = "Load Game Names";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // listBox1
            // 
            this.listBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 148);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(376, 290);
            this.listBox1.TabIndex = 10;
            this.listBox1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox1_DrawItem);
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // openFileDialog3
            // 
            this.openFileDialog3.DefaultExt = "txt";
            this.openFileDialog3.Filter = "Game List|romlist.txt|Text Files (*.txt)|*.txt|All files (*.*)|*.*";
            this.openFileDialog3.InitialDirectory = "@\"C:\\\"";
            this.openFileDialog3.RestoreDirectory = true;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "srm";
            this.saveFileDialog1.Filter = "SRAM File|*.srm|SRAM File|*.sav|All files (*.*)|*.*";
            this.saveFileDialog1.InitialDirectory = "@\"C:\\\"";
            this.saveFileDialog1.RestoreDirectory = true;
            // 
            // saveFileDialog2
            // 
            this.saveFileDialog2.DefaultExt = "ggmsav";
            this.saveFileDialog2.Filter = "GGM save data|*.ggmsav|GGM save data|save.bin|All files (*.*)|*.*";
            this.saveFileDialog2.InitialDirectory = "@\"C:\\\"";
            this.saveFileDialog2.RestoreDirectory = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Slot";
            // 
            // button9
            // 
            this.button9.Enabled = false;
            this.button9.Location = new System.Drawing.Point(217, 76);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(78, 27);
            this.button9.TabIndex = 12;
            this.button9.Text = "Import SRAM";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.DefaultExt = "ggmsav";
            this.openFileDialog2.Filter = "GGM save data (*.ggmsav)|*.GGMSAV;save.bin|All files (*.*)|*.*";
            this.openFileDialog2.InitialDirectory = "@\"C:\\\"";
            this.openFileDialog2.RestoreDirectory = true;
            // 
            // openFileDialog4
            // 
            this.openFileDialog4.DefaultExt = "srm";
            this.openFileDialog4.Filter = "SRAM File|*.srm|SRAM File|*.sav|All files (*.*)|*.*";
            this.openFileDialog4.InitialDirectory = "@\"C:\\\"";
            this.openFileDialog4.RestoreDirectory = true;
            // 
            // button10
            // 
            this.button10.Enabled = false;
            this.button10.Location = new System.Drawing.Point(189, 109);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(71, 27);
            this.button10.TabIndex = 13;
            this.button10.Text = "Game No.";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Enabled = false;
            this.button11.Location = new System.Drawing.Point(118, 109);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(63, 27);
            this.button11.TabIndex = 14;
            this.button11.Text = "Save Slot";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.Enabled = false;
            this.button12.Location = new System.Drawing.Point(58, 109);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(54, 27);
            this.button12.TabIndex = 15;
            this.button12.Text = "Version";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // chkAdvancedFeatures
            // 
            this.chkAdvancedFeatures.AutoSize = true;
            this.chkAdvancedFeatures.Location = new System.Drawing.Point(37, 116);
            this.chkAdvancedFeatures.Name = "chkAdvancedFeatures";
            this.chkAdvancedFeatures.Size = new System.Drawing.Size(15, 14);
            this.chkAdvancedFeatures.TabIndex = 16;
            this.chkAdvancedFeatures.UseVisualStyleBackColor = true;
            this.chkAdvancedFeatures.CheckedChanged += new System.EventHandler(this.chkAdvancedFeatures_CheckedChanged);
            // 
            // button13
            // 
            this.button13.Enabled = false;
            this.button13.Location = new System.Drawing.Point(460, 109);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(54, 27);
            this.button13.TabIndex = 31;
            this.button13.Text = "Version";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button14
            // 
            this.button14.Enabled = false;
            this.button14.Location = new System.Drawing.Point(520, 109);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(63, 27);
            this.button14.TabIndex = 30;
            this.button14.Text = "Save Slot";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button15
            // 
            this.button15.Enabled = false;
            this.button15.Location = new System.Drawing.Point(589, 109);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(71, 27);
            this.button15.TabIndex = 29;
            this.button15.Text = "Game No.";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // button16
            // 
            this.button16.Enabled = false;
            this.button16.Location = new System.Drawing.Point(617, 76);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(78, 27);
            this.button16.TabIndex = 28;
            this.button16.Text = "Import SRAM";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(411, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Slot";
            // 
            // listBox2
            // 
            this.listBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(412, 148);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(376, 290);
            this.listBox2.TabIndex = 26;
            this.listBox2.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox2_DrawItem);
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(412, 45);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(376, 20);
            this.textBox2.TabIndex = 24;
            // 
            // button18
            // 
            this.button18.Enabled = false;
            this.button18.Location = new System.Drawing.Point(701, 76);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(87, 27);
            this.button18.TabIndex = 23;
            this.button18.Text = "Export SRAM";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // button19
            // 
            this.button19.Enabled = false;
            this.button19.Location = new System.Drawing.Point(564, 76);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(47, 27);
            this.button19.TabIndex = 22;
            this.button19.Text = "Clear";
            this.button19.UseVisualStyleBackColor = true;
            this.button19.Click += new System.EventHandler(this.button19_Click);
            // 
            // button20
            // 
            this.button20.Enabled = false;
            this.button20.Location = new System.Drawing.Point(511, 76);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(47, 27);
            this.button20.TabIndex = 21;
            this.button20.Text = "Paste";
            this.button20.UseVisualStyleBackColor = true;
            this.button20.Click += new System.EventHandler(this.button20_Click);
            // 
            // button21
            // 
            this.button21.Enabled = false;
            this.button21.Location = new System.Drawing.Point(458, 76);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(47, 27);
            this.button21.TabIndex = 20;
            this.button21.Text = "Copy";
            this.button21.UseVisualStyleBackColor = true;
            this.button21.Click += new System.EventHandler(this.button21_Click);
            // 
            // button22
            // 
            this.button22.Enabled = false;
            this.button22.Location = new System.Drawing.Point(511, 12);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(51, 27);
            this.button22.TabIndex = 19;
            this.button22.Text = "Save";
            this.button22.UseVisualStyleBackColor = true;
            this.button22.Click += new System.EventHandler(this.button22_Click);
            // 
            // button23
            // 
            this.button23.Location = new System.Drawing.Point(632, 12);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(51, 27);
            this.button23.TabIndex = 18;
            this.button23.Text = "New";
            this.button23.UseVisualStyleBackColor = true;
            this.button23.Click += new System.EventHandler(this.button23_Click);
            // 
            // button24
            // 
            this.button24.Location = new System.Drawing.Point(412, 12);
            this.button24.Name = "button24";
            this.button24.Size = new System.Drawing.Size(93, 27);
            this.button24.TabIndex = 17;
            this.button24.Text = "Open 2nd File";
            this.button24.UseVisualStyleBackColor = true;
            this.button24.Click += new System.EventHandler(this.button24_Click);
            // 
            // button17
            // 
            this.button17.Enabled = false;
            this.button17.Location = new System.Drawing.Point(266, 109);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(71, 27);
            this.button17.TabIndex = 33;
            this.button17.Text = "SRAM size";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // button25
            // 
            this.button25.Enabled = false;
            this.button25.Location = new System.Drawing.Point(343, 109);
            this.button25.Name = "button25";
            this.button25.Size = new System.Drawing.Size(45, 27);
            this.button25.TabIndex = 34;
            this.button25.Text = "Hash";
            this.button25.UseVisualStyleBackColor = true;
            this.button25.Click += new System.EventHandler(this.button25_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(12, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Set";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Enabled = false;
            this.label4.Location = new System.Drawing.Point(411, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "Set";
            // 
            // button26
            // 
            this.button26.Enabled = false;
            this.button26.Location = new System.Drawing.Point(743, 109);
            this.button26.Name = "button26";
            this.button26.Size = new System.Drawing.Size(45, 27);
            this.button26.TabIndex = 37;
            this.button26.Text = "Hash";
            this.button26.UseVisualStyleBackColor = true;
            this.button26.Click += new System.EventHandler(this.button26_Click);
            // 
            // button27
            // 
            this.button27.Enabled = false;
            this.button27.Location = new System.Drawing.Point(666, 109);
            this.button27.Name = "button27";
            this.button27.Size = new System.Drawing.Size(71, 27);
            this.button27.TabIndex = 36;
            this.button27.Text = "SRAM size";
            this.button27.UseVisualStyleBackColor = true;
            this.button27.Click += new System.EventHandler(this.button27_Click);
            // 
            // button28
            // 
            this.button28.Enabled = false;
            this.button28.Location = new System.Drawing.Point(168, 12);
            this.button28.Name = "button28";
            this.button28.Size = new System.Drawing.Size(58, 27);
            this.button28.TabIndex = 38;
            this.button28.Text = "Save As";
            this.button28.UseVisualStyleBackColor = true;
            this.button28.Click += new System.EventHandler(this.button28_Click);
            // 
            // button29
            // 
            this.button29.Enabled = false;
            this.button29.Location = new System.Drawing.Point(568, 12);
            this.button29.Name = "button29";
            this.button29.Size = new System.Drawing.Size(58, 27);
            this.button29.TabIndex = 39;
            this.button29.Text = "Save As";
            this.button29.UseVisualStyleBackColor = true;
            this.button29.Click += new System.EventHandler(this.button29_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button29);
            this.Controls.Add(this.button28);
            this.Controls.Add(this.button26);
            this.Controls.Add(this.button27);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button25);
            this.Controls.Add(this.button17);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.button16);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button18);
            this.Controls.Add(this.button19);
            this.Controls.Add(this.button20);
            this.Controls.Add(this.button21);
            this.Controls.Add(this.button22);
            this.Controls.Add(this.button23);
            this.Controls.Add(this.button24);
            this.Controls.Add(this.chkAdvancedFeatures);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "GGM Save Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog3;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.OpenFileDialog openFileDialog4;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.CheckBox chkAdvancedFeatures;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button button19;
        private System.Windows.Forms.Button button20;
        private System.Windows.Forms.Button button21;
        private System.Windows.Forms.Button button22;
        private System.Windows.Forms.Button button23;
        private System.Windows.Forms.Button button24;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button25;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button26;
        private System.Windows.Forms.Button button27;
        private System.Windows.Forms.Button button28;
        private System.Windows.Forms.Button button29;
    }
}

