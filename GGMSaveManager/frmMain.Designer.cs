namespace GGMSaveManager
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnOpenFile1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnNewFile1 = new System.Windows.Forms.Button();
            this.btnSaveFile1 = new System.Windows.Forms.Button();
            this.btnCopySlotFile1 = new System.Windows.Forms.Button();
            this.btnPasteSlotFile1 = new System.Windows.Forms.Button();
            this.btnClearSlotFile1 = new System.Windows.Forms.Button();
            this.btnExportSRAMSlotFile1 = new System.Windows.Forms.Button();
            this.txtFile1 = new System.Windows.Forms.TextBox();
            this.btnLoadGameNames = new System.Windows.Forms.Button();
            this.listBoxFile1 = new System.Windows.Forms.ListBox();
            this.openFileDialog3 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.lblSlot1 = new System.Windows.Forms.Label();
            this.btnImportSRAMSlotFile1 = new System.Windows.Forms.Button();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog4 = new System.Windows.Forms.OpenFileDialog();
            this.btnChangeGameIDSlotFile1 = new System.Windows.Forms.Button();
            this.btnChangeSaveStateIDSlotFile1 = new System.Windows.Forms.Button();
            this.btnChangeVersionSlotFile1 = new System.Windows.Forms.Button();
            this.chkAdvancedFeatures = new System.Windows.Forms.CheckBox();
            this.btnChangeVersionSlotFile2 = new System.Windows.Forms.Button();
            this.btnChangeSaveStateIDSlotFile2 = new System.Windows.Forms.Button();
            this.btnChangeGameIDSlotFile2 = new System.Windows.Forms.Button();
            this.btnImportSRAMSlotFile2 = new System.Windows.Forms.Button();
            this.lblSlot2 = new System.Windows.Forms.Label();
            this.listBoxFile2 = new System.Windows.Forms.ListBox();
            this.txtFile2 = new System.Windows.Forms.TextBox();
            this.btnExportSRAMSlotFile2 = new System.Windows.Forms.Button();
            this.btnClearSlotFile2 = new System.Windows.Forms.Button();
            this.btnPasteSlotFile2 = new System.Windows.Forms.Button();
            this.btnCopySlotFile2 = new System.Windows.Forms.Button();
            this.btnSaveFile2 = new System.Windows.Forms.Button();
            this.btnNewFile2 = new System.Windows.Forms.Button();
            this.btnOpenFile2 = new System.Windows.Forms.Button();
            this.btnChangeSRAMSizeSlotFile1 = new System.Windows.Forms.Button();
            this.btnRecalculateHashSlotFile1 = new System.Windows.Forms.Button();
            this.lblSet1 = new System.Windows.Forms.Label();
            this.lblSet2 = new System.Windows.Forms.Label();
            this.btnRecalculateHashSlotFile2 = new System.Windows.Forms.Button();
            this.btnChangeSRAMSizeSlotFile2 = new System.Windows.Forms.Button();
            this.btnSaveAsFile1 = new System.Windows.Forms.Button();
            this.btnSaveAsFile2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOpenFile1
            // 
            this.btnOpenFile1.Location = new System.Drawing.Point(12, 12);
            this.btnOpenFile1.Name = "btnOpenFile1";
            this.btnOpenFile1.Size = new System.Drawing.Size(93, 27);
            this.btnOpenFile1.TabIndex = 0;
            this.btnOpenFile1.Text = "Open 1st File";
            this.btnOpenFile1.UseVisualStyleBackColor = true;
            this.btnOpenFile1.Click += new System.EventHandler(this.btnOpenFile1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "ggmsav";
            this.openFileDialog1.Filter = "GGM save data (*.ggmsav)|*.GGMSAV;save.bin|All files (*.*)|*.*";
            this.openFileDialog1.InitialDirectory = "@\"C:\\\"";
            this.openFileDialog1.RestoreDirectory = true;
            // 
            // btnNewFile1
            // 
            this.btnNewFile1.Location = new System.Drawing.Point(232, 12);
            this.btnNewFile1.Name = "btnNewFile1";
            this.btnNewFile1.Size = new System.Drawing.Size(37, 27);
            this.btnNewFile1.TabIndex = 2;
            this.btnNewFile1.Text = "New";
            this.btnNewFile1.UseVisualStyleBackColor = true;
            this.btnNewFile1.Click += new System.EventHandler(this.btnNewFile1_Click);
            // 
            // btnSaveFile1
            // 
            this.btnSaveFile1.Enabled = false;
            this.btnSaveFile1.Location = new System.Drawing.Point(111, 12);
            this.btnSaveFile1.Name = "btnSaveFile1";
            this.btnSaveFile1.Size = new System.Drawing.Size(51, 27);
            this.btnSaveFile1.TabIndex = 3;
            this.btnSaveFile1.Text = "Save";
            this.btnSaveFile1.UseVisualStyleBackColor = true;
            this.btnSaveFile1.Click += new System.EventHandler(this.btnSaveFile1_Click);
            // 
            // btnCopySlotFile1
            // 
            this.btnCopySlotFile1.Enabled = false;
            this.btnCopySlotFile1.Location = new System.Drawing.Point(58, 76);
            this.btnCopySlotFile1.Name = "btnCopySlotFile1";
            this.btnCopySlotFile1.Size = new System.Drawing.Size(47, 27);
            this.btnCopySlotFile1.TabIndex = 4;
            this.btnCopySlotFile1.Text = "Copy";
            this.btnCopySlotFile1.UseVisualStyleBackColor = true;
            this.btnCopySlotFile1.Click += new System.EventHandler(this.btnCopySlotFile1_Click);
            // 
            // btnPasteSlotFile1
            // 
            this.btnPasteSlotFile1.Enabled = false;
            this.btnPasteSlotFile1.Location = new System.Drawing.Point(111, 76);
            this.btnPasteSlotFile1.Name = "btnPasteSlotFile1";
            this.btnPasteSlotFile1.Size = new System.Drawing.Size(47, 27);
            this.btnPasteSlotFile1.TabIndex = 5;
            this.btnPasteSlotFile1.Text = "Paste";
            this.btnPasteSlotFile1.UseVisualStyleBackColor = true;
            this.btnPasteSlotFile1.Click += new System.EventHandler(this.btnPasteSlotFile1_Click);
            // 
            // btnClearSlotFile1
            // 
            this.btnClearSlotFile1.Enabled = false;
            this.btnClearSlotFile1.Location = new System.Drawing.Point(164, 76);
            this.btnClearSlotFile1.Name = "btnClearSlotFile1";
            this.btnClearSlotFile1.Size = new System.Drawing.Size(47, 27);
            this.btnClearSlotFile1.TabIndex = 6;
            this.btnClearSlotFile1.Text = "Clear";
            this.btnClearSlotFile1.UseVisualStyleBackColor = true;
            this.btnClearSlotFile1.Click += new System.EventHandler(this.btnClearSlotFile1_Click);
            // 
            // btnExportSRAMSlotFile1
            // 
            this.btnExportSRAMSlotFile1.Enabled = false;
            this.btnExportSRAMSlotFile1.Location = new System.Drawing.Point(301, 76);
            this.btnExportSRAMSlotFile1.Name = "btnExportSRAMSlotFile1";
            this.btnExportSRAMSlotFile1.Size = new System.Drawing.Size(87, 27);
            this.btnExportSRAMSlotFile1.TabIndex = 7;
            this.btnExportSRAMSlotFile1.Text = "Export SRAM";
            this.btnExportSRAMSlotFile1.UseVisualStyleBackColor = true;
            this.btnExportSRAMSlotFile1.Click += new System.EventHandler(this.btnExportSRAMSlotFile1_Click);
            // 
            // txtFile1
            // 
            this.txtFile1.Location = new System.Drawing.Point(12, 45);
            this.txtFile1.Name = "txtFile1";
            this.txtFile1.ReadOnly = true;
            this.txtFile1.Size = new System.Drawing.Size(376, 20);
            this.txtFile1.TabIndex = 8;
            // 
            // btnLoadGameNames
            // 
            this.btnLoadGameNames.Location = new System.Drawing.Point(275, 12);
            this.btnLoadGameNames.Name = "btnLoadGameNames";
            this.btnLoadGameNames.Size = new System.Drawing.Size(113, 27);
            this.btnLoadGameNames.TabIndex = 9;
            this.btnLoadGameNames.Text = "Load Game Names";
            this.btnLoadGameNames.UseVisualStyleBackColor = true;
            this.btnLoadGameNames.Click += new System.EventHandler(this.btnLoadGameNames_Click);
            // 
            // listBoxFile1
            // 
            this.listBoxFile1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listBoxFile1.FormattingEnabled = true;
            this.listBoxFile1.Location = new System.Drawing.Point(12, 148);
            this.listBoxFile1.Name = "listBoxFile1";
            this.listBoxFile1.Size = new System.Drawing.Size(376, 290);
            this.listBoxFile1.TabIndex = 10;
            this.listBoxFile1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBoxFile1_DrawItem);
            this.listBoxFile1.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.listBox_MeasureItem);
            this.listBoxFile1.SelectedIndexChanged += new System.EventHandler(this.listBoxFile1_SelectedIndexChanged);
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
            // lblSlot1
            // 
            this.lblSlot1.AutoSize = true;
            this.lblSlot1.Location = new System.Drawing.Point(12, 83);
            this.lblSlot1.Name = "lblSlot1";
            this.lblSlot1.Size = new System.Drawing.Size(25, 13);
            this.lblSlot1.TabIndex = 11;
            this.lblSlot1.Text = "Slot";
            // 
            // btnImportSRAMSlotFile1
            // 
            this.btnImportSRAMSlotFile1.Enabled = false;
            this.btnImportSRAMSlotFile1.Location = new System.Drawing.Point(217, 76);
            this.btnImportSRAMSlotFile1.Name = "btnImportSRAMSlotFile1";
            this.btnImportSRAMSlotFile1.Size = new System.Drawing.Size(78, 27);
            this.btnImportSRAMSlotFile1.TabIndex = 12;
            this.btnImportSRAMSlotFile1.Text = "Import SRAM";
            this.btnImportSRAMSlotFile1.UseVisualStyleBackColor = true;
            this.btnImportSRAMSlotFile1.Click += new System.EventHandler(this.btnImportSRAMSlotFile1_Click);
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
            // btnChangeGameIDSlotFile1
            // 
            this.btnChangeGameIDSlotFile1.Enabled = false;
            this.btnChangeGameIDSlotFile1.Location = new System.Drawing.Point(189, 109);
            this.btnChangeGameIDSlotFile1.Name = "btnChangeGameIDSlotFile1";
            this.btnChangeGameIDSlotFile1.Size = new System.Drawing.Size(71, 27);
            this.btnChangeGameIDSlotFile1.TabIndex = 13;
            this.btnChangeGameIDSlotFile1.Text = "Game No.";
            this.btnChangeGameIDSlotFile1.UseVisualStyleBackColor = true;
            this.btnChangeGameIDSlotFile1.Click += new System.EventHandler(this.btnChangeGameIDSlotFile1_Click);
            // 
            // btnChangeSaveStateIDSlotFile1
            // 
            this.btnChangeSaveStateIDSlotFile1.Enabled = false;
            this.btnChangeSaveStateIDSlotFile1.Location = new System.Drawing.Point(118, 109);
            this.btnChangeSaveStateIDSlotFile1.Name = "btnChangeSaveStateIDSlotFile1";
            this.btnChangeSaveStateIDSlotFile1.Size = new System.Drawing.Size(63, 27);
            this.btnChangeSaveStateIDSlotFile1.TabIndex = 14;
            this.btnChangeSaveStateIDSlotFile1.Text = "Save Slot";
            this.btnChangeSaveStateIDSlotFile1.UseVisualStyleBackColor = true;
            this.btnChangeSaveStateIDSlotFile1.Click += new System.EventHandler(this.btnChangeSaveStateIDSlotFile1_Click);
            // 
            // btnChangeVersionSlotFile1
            // 
            this.btnChangeVersionSlotFile1.Enabled = false;
            this.btnChangeVersionSlotFile1.Location = new System.Drawing.Point(58, 109);
            this.btnChangeVersionSlotFile1.Name = "btnChangeVersionSlotFile1";
            this.btnChangeVersionSlotFile1.Size = new System.Drawing.Size(54, 27);
            this.btnChangeVersionSlotFile1.TabIndex = 15;
            this.btnChangeVersionSlotFile1.Text = "Version";
            this.btnChangeVersionSlotFile1.UseVisualStyleBackColor = true;
            this.btnChangeVersionSlotFile1.Click += new System.EventHandler(this.btnChangeVersionSlotFile1_Click);
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
            // btnChangeVersionSlotFile2
            // 
            this.btnChangeVersionSlotFile2.Enabled = false;
            this.btnChangeVersionSlotFile2.Location = new System.Drawing.Point(460, 109);
            this.btnChangeVersionSlotFile2.Name = "btnChangeVersionSlotFile2";
            this.btnChangeVersionSlotFile2.Size = new System.Drawing.Size(54, 27);
            this.btnChangeVersionSlotFile2.TabIndex = 31;
            this.btnChangeVersionSlotFile2.Text = "Version";
            this.btnChangeVersionSlotFile2.UseVisualStyleBackColor = true;
            this.btnChangeVersionSlotFile2.Click += new System.EventHandler(this.btnChangeVersionSlotFile2_Click);
            // 
            // btnChangeSaveStateIDSlotFile2
            // 
            this.btnChangeSaveStateIDSlotFile2.Enabled = false;
            this.btnChangeSaveStateIDSlotFile2.Location = new System.Drawing.Point(520, 109);
            this.btnChangeSaveStateIDSlotFile2.Name = "btnChangeSaveStateIDSlotFile2";
            this.btnChangeSaveStateIDSlotFile2.Size = new System.Drawing.Size(63, 27);
            this.btnChangeSaveStateIDSlotFile2.TabIndex = 30;
            this.btnChangeSaveStateIDSlotFile2.Text = "Save Slot";
            this.btnChangeSaveStateIDSlotFile2.UseVisualStyleBackColor = true;
            this.btnChangeSaveStateIDSlotFile2.Click += new System.EventHandler(this.btnChangeSaveStateIDSlotFile2_Click);
            // 
            // btnChangeGameIDSlotFile2
            // 
            this.btnChangeGameIDSlotFile2.Enabled = false;
            this.btnChangeGameIDSlotFile2.Location = new System.Drawing.Point(589, 109);
            this.btnChangeGameIDSlotFile2.Name = "btnChangeGameIDSlotFile2";
            this.btnChangeGameIDSlotFile2.Size = new System.Drawing.Size(71, 27);
            this.btnChangeGameIDSlotFile2.TabIndex = 29;
            this.btnChangeGameIDSlotFile2.Text = "Game No.";
            this.btnChangeGameIDSlotFile2.UseVisualStyleBackColor = true;
            this.btnChangeGameIDSlotFile2.Click += new System.EventHandler(this.btnChangeGameIDSlotFile2_Click);
            // 
            // btnImportSRAMSlotFile2
            // 
            this.btnImportSRAMSlotFile2.Enabled = false;
            this.btnImportSRAMSlotFile2.Location = new System.Drawing.Point(617, 76);
            this.btnImportSRAMSlotFile2.Name = "btnImportSRAMSlotFile2";
            this.btnImportSRAMSlotFile2.Size = new System.Drawing.Size(78, 27);
            this.btnImportSRAMSlotFile2.TabIndex = 28;
            this.btnImportSRAMSlotFile2.Text = "Import SRAM";
            this.btnImportSRAMSlotFile2.UseVisualStyleBackColor = true;
            this.btnImportSRAMSlotFile2.Click += new System.EventHandler(this.btnImportSRAMSlotFile2_Click);
            // 
            // lblSlot2
            // 
            this.lblSlot2.AutoSize = true;
            this.lblSlot2.Location = new System.Drawing.Point(411, 83);
            this.lblSlot2.Name = "lblSlot2";
            this.lblSlot2.Size = new System.Drawing.Size(25, 13);
            this.lblSlot2.TabIndex = 27;
            this.lblSlot2.Text = "Slot";
            // 
            // listBoxFile2
            // 
            this.listBoxFile2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listBoxFile2.FormattingEnabled = true;
            this.listBoxFile2.Location = new System.Drawing.Point(412, 148);
            this.listBoxFile2.Name = "listBoxFile2";
            this.listBoxFile2.Size = new System.Drawing.Size(376, 290);
            this.listBoxFile2.TabIndex = 26;
            this.listBoxFile2.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBoxFile2_DrawItem);
            this.listBoxFile2.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.listBox_MeasureItem);
            this.listBoxFile2.SelectedIndexChanged += new System.EventHandler(this.listBoxFile2_SelectedIndexChanged);
            // 
            // txtFile2
            // 
            this.txtFile2.Location = new System.Drawing.Point(412, 45);
            this.txtFile2.Name = "txtFile2";
            this.txtFile2.ReadOnly = true;
            this.txtFile2.Size = new System.Drawing.Size(376, 20);
            this.txtFile2.TabIndex = 24;
            // 
            // btnExportSRAMSlotFile2
            // 
            this.btnExportSRAMSlotFile2.Enabled = false;
            this.btnExportSRAMSlotFile2.Location = new System.Drawing.Point(701, 76);
            this.btnExportSRAMSlotFile2.Name = "btnExportSRAMSlotFile2";
            this.btnExportSRAMSlotFile2.Size = new System.Drawing.Size(87, 27);
            this.btnExportSRAMSlotFile2.TabIndex = 23;
            this.btnExportSRAMSlotFile2.Text = "Export SRAM";
            this.btnExportSRAMSlotFile2.UseVisualStyleBackColor = true;
            this.btnExportSRAMSlotFile2.Click += new System.EventHandler(this.btnExportSRAMSlotFile2_Click);
            // 
            // btnClearSlotFile2
            // 
            this.btnClearSlotFile2.Enabled = false;
            this.btnClearSlotFile2.Location = new System.Drawing.Point(564, 76);
            this.btnClearSlotFile2.Name = "btnClearSlotFile2";
            this.btnClearSlotFile2.Size = new System.Drawing.Size(47, 27);
            this.btnClearSlotFile2.TabIndex = 22;
            this.btnClearSlotFile2.Text = "Clear";
            this.btnClearSlotFile2.UseVisualStyleBackColor = true;
            this.btnClearSlotFile2.Click += new System.EventHandler(this.btnClearSlotFile2_Click);
            // 
            // btnPasteSlotFile2
            // 
            this.btnPasteSlotFile2.Enabled = false;
            this.btnPasteSlotFile2.Location = new System.Drawing.Point(511, 76);
            this.btnPasteSlotFile2.Name = "btnPasteSlotFile2";
            this.btnPasteSlotFile2.Size = new System.Drawing.Size(47, 27);
            this.btnPasteSlotFile2.TabIndex = 21;
            this.btnPasteSlotFile2.Text = "Paste";
            this.btnPasteSlotFile2.UseVisualStyleBackColor = true;
            this.btnPasteSlotFile2.Click += new System.EventHandler(this.btnPasteSlotFile2_Click);
            // 
            // btnCopySlotFile2
            // 
            this.btnCopySlotFile2.Enabled = false;
            this.btnCopySlotFile2.Location = new System.Drawing.Point(458, 76);
            this.btnCopySlotFile2.Name = "btnCopySlotFile2";
            this.btnCopySlotFile2.Size = new System.Drawing.Size(47, 27);
            this.btnCopySlotFile2.TabIndex = 20;
            this.btnCopySlotFile2.Text = "Copy";
            this.btnCopySlotFile2.UseVisualStyleBackColor = true;
            this.btnCopySlotFile2.Click += new System.EventHandler(this.btnCopySlotFile2_Click);
            // 
            // btnSaveFile2
            // 
            this.btnSaveFile2.Enabled = false;
            this.btnSaveFile2.Location = new System.Drawing.Point(511, 12);
            this.btnSaveFile2.Name = "btnSaveFile2";
            this.btnSaveFile2.Size = new System.Drawing.Size(51, 27);
            this.btnSaveFile2.TabIndex = 19;
            this.btnSaveFile2.Text = "Save";
            this.btnSaveFile2.UseVisualStyleBackColor = true;
            this.btnSaveFile2.Click += new System.EventHandler(this.btnSaveFile2_Click);
            // 
            // btnNewFile2
            // 
            this.btnNewFile2.Location = new System.Drawing.Point(632, 12);
            this.btnNewFile2.Name = "btnNewFile2";
            this.btnNewFile2.Size = new System.Drawing.Size(51, 27);
            this.btnNewFile2.TabIndex = 18;
            this.btnNewFile2.Text = "New";
            this.btnNewFile2.UseVisualStyleBackColor = true;
            this.btnNewFile2.Click += new System.EventHandler(this.btnNewFile2_Click);
            // 
            // btnOpenFile2
            // 
            this.btnOpenFile2.Location = new System.Drawing.Point(412, 12);
            this.btnOpenFile2.Name = "btnOpenFile2";
            this.btnOpenFile2.Size = new System.Drawing.Size(93, 27);
            this.btnOpenFile2.TabIndex = 17;
            this.btnOpenFile2.Text = "Open 2nd File";
            this.btnOpenFile2.UseVisualStyleBackColor = true;
            this.btnOpenFile2.Click += new System.EventHandler(this.btnOpenFile2_Click);
            // 
            // btnChangeSRAMSizeSlotFile1
            // 
            this.btnChangeSRAMSizeSlotFile1.Enabled = false;
            this.btnChangeSRAMSizeSlotFile1.Location = new System.Drawing.Point(266, 109);
            this.btnChangeSRAMSizeSlotFile1.Name = "btnChangeSRAMSizeSlotFile1";
            this.btnChangeSRAMSizeSlotFile1.Size = new System.Drawing.Size(71, 27);
            this.btnChangeSRAMSizeSlotFile1.TabIndex = 33;
            this.btnChangeSRAMSizeSlotFile1.Text = "SRAM size";
            this.btnChangeSRAMSizeSlotFile1.UseVisualStyleBackColor = true;
            this.btnChangeSRAMSizeSlotFile1.Click += new System.EventHandler(this.btnChangeSRAMSizeSlotFile1_Click);
            // 
            // btnRecalculateHashSlotFile1
            // 
            this.btnRecalculateHashSlotFile1.Enabled = false;
            this.btnRecalculateHashSlotFile1.Location = new System.Drawing.Point(343, 109);
            this.btnRecalculateHashSlotFile1.Name = "btnRecalculateHashSlotFile1";
            this.btnRecalculateHashSlotFile1.Size = new System.Drawing.Size(45, 27);
            this.btnRecalculateHashSlotFile1.TabIndex = 34;
            this.btnRecalculateHashSlotFile1.Text = "Hash";
            this.btnRecalculateHashSlotFile1.UseVisualStyleBackColor = true;
            this.btnRecalculateHashSlotFile1.Click += new System.EventHandler(this.btnRecalculateHashSlotFile1_Click);
            // 
            // lblSet1
            // 
            this.lblSet1.AutoSize = true;
            this.lblSet1.Enabled = false;
            this.lblSet1.Location = new System.Drawing.Point(12, 116);
            this.lblSet1.Name = "lblSet1";
            this.lblSet1.Size = new System.Drawing.Size(23, 13);
            this.lblSet1.TabIndex = 32;
            this.lblSet1.Text = "Set";
            // 
            // lblSet2
            // 
            this.lblSet2.AutoSize = true;
            this.lblSet2.Enabled = false;
            this.lblSet2.Location = new System.Drawing.Point(411, 116);
            this.lblSet2.Name = "lblSet2";
            this.lblSet2.Size = new System.Drawing.Size(23, 13);
            this.lblSet2.TabIndex = 35;
            this.lblSet2.Text = "Set";
            // 
            // btnRecalculateHashSlotFile2
            // 
            this.btnRecalculateHashSlotFile2.Enabled = false;
            this.btnRecalculateHashSlotFile2.Location = new System.Drawing.Point(743, 109);
            this.btnRecalculateHashSlotFile2.Name = "btnRecalculateHashSlotFile2";
            this.btnRecalculateHashSlotFile2.Size = new System.Drawing.Size(45, 27);
            this.btnRecalculateHashSlotFile2.TabIndex = 37;
            this.btnRecalculateHashSlotFile2.Text = "Hash";
            this.btnRecalculateHashSlotFile2.UseVisualStyleBackColor = true;
            this.btnRecalculateHashSlotFile2.Click += new System.EventHandler(this.btnRecalculateHashSlotFile2_Click);
            // 
            // btnChangeSRAMSizeSlotFile2
            // 
            this.btnChangeSRAMSizeSlotFile2.Enabled = false;
            this.btnChangeSRAMSizeSlotFile2.Location = new System.Drawing.Point(666, 109);
            this.btnChangeSRAMSizeSlotFile2.Name = "btnChangeSRAMSizeSlotFile2";
            this.btnChangeSRAMSizeSlotFile2.Size = new System.Drawing.Size(71, 27);
            this.btnChangeSRAMSizeSlotFile2.TabIndex = 36;
            this.btnChangeSRAMSizeSlotFile2.Text = "SRAM size";
            this.btnChangeSRAMSizeSlotFile2.UseVisualStyleBackColor = true;
            this.btnChangeSRAMSizeSlotFile2.Click += new System.EventHandler(this.btnChangeSRAMSizeSlotFile2_Click);
            // 
            // btnSaveAsFile1
            // 
            this.btnSaveAsFile1.Enabled = false;
            this.btnSaveAsFile1.Location = new System.Drawing.Point(168, 12);
            this.btnSaveAsFile1.Name = "btnSaveAsFile1";
            this.btnSaveAsFile1.Size = new System.Drawing.Size(58, 27);
            this.btnSaveAsFile1.TabIndex = 38;
            this.btnSaveAsFile1.Text = "Save As";
            this.btnSaveAsFile1.UseVisualStyleBackColor = true;
            this.btnSaveAsFile1.Click += new System.EventHandler(this.btnSaveAsFile1_Click);
            // 
            // btnSaveAsFile2
            // 
            this.btnSaveAsFile2.Enabled = false;
            this.btnSaveAsFile2.Location = new System.Drawing.Point(568, 12);
            this.btnSaveAsFile2.Name = "btnSaveAsFile2";
            this.btnSaveAsFile2.Size = new System.Drawing.Size(58, 27);
            this.btnSaveAsFile2.TabIndex = 39;
            this.btnSaveAsFile2.Text = "Save As";
            this.btnSaveAsFile2.UseVisualStyleBackColor = true;
            this.btnSaveAsFile2.Click += new System.EventHandler(this.btnSaveAsFile2_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSaveAsFile2);
            this.Controls.Add(this.btnSaveAsFile1);
            this.Controls.Add(this.btnRecalculateHashSlotFile2);
            this.Controls.Add(this.btnChangeSRAMSizeSlotFile2);
            this.Controls.Add(this.lblSet2);
            this.Controls.Add(this.btnRecalculateHashSlotFile1);
            this.Controls.Add(this.btnChangeSRAMSizeSlotFile1);
            this.Controls.Add(this.lblSet1);
            this.Controls.Add(this.btnChangeVersionSlotFile2);
            this.Controls.Add(this.btnChangeSaveStateIDSlotFile2);
            this.Controls.Add(this.btnChangeGameIDSlotFile2);
            this.Controls.Add(this.btnImportSRAMSlotFile2);
            this.Controls.Add(this.lblSlot2);
            this.Controls.Add(this.listBoxFile2);
            this.Controls.Add(this.txtFile2);
            this.Controls.Add(this.btnExportSRAMSlotFile2);
            this.Controls.Add(this.btnClearSlotFile2);
            this.Controls.Add(this.btnPasteSlotFile2);
            this.Controls.Add(this.btnCopySlotFile2);
            this.Controls.Add(this.btnSaveFile2);
            this.Controls.Add(this.btnNewFile2);
            this.Controls.Add(this.btnOpenFile2);
            this.Controls.Add(this.chkAdvancedFeatures);
            this.Controls.Add(this.btnChangeVersionSlotFile1);
            this.Controls.Add(this.btnChangeSaveStateIDSlotFile1);
            this.Controls.Add(this.btnChangeGameIDSlotFile1);
            this.Controls.Add(this.btnImportSRAMSlotFile1);
            this.Controls.Add(this.lblSlot1);
            this.Controls.Add(this.listBoxFile1);
            this.Controls.Add(this.btnLoadGameNames);
            this.Controls.Add(this.txtFile1);
            this.Controls.Add(this.btnExportSRAMSlotFile1);
            this.Controls.Add(this.btnClearSlotFile1);
            this.Controls.Add(this.btnPasteSlotFile1);
            this.Controls.Add(this.btnCopySlotFile1);
            this.Controls.Add(this.btnSaveFile1);
            this.Controls.Add(this.btnNewFile1);
            this.Controls.Add(this.btnOpenFile1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "GGM Save Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenFile1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnNewFile1;
        private System.Windows.Forms.Button btnSaveFile1;
        private System.Windows.Forms.Button btnCopySlotFile1;
        private System.Windows.Forms.Button btnPasteSlotFile1;
        private System.Windows.Forms.Button btnClearSlotFile1;
        private System.Windows.Forms.Button btnExportSRAMSlotFile1;
        private System.Windows.Forms.TextBox txtFile1;
        private System.Windows.Forms.Button btnLoadGameNames;
        private System.Windows.Forms.ListBox listBoxFile1;
        private System.Windows.Forms.OpenFileDialog openFileDialog3;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
        private System.Windows.Forms.Label lblSlot1;
        private System.Windows.Forms.Button btnImportSRAMSlotFile1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.OpenFileDialog openFileDialog4;
        private System.Windows.Forms.Button btnChangeGameIDSlotFile1;
        private System.Windows.Forms.Button btnChangeSaveStateIDSlotFile1;
        private System.Windows.Forms.Button btnChangeVersionSlotFile1;
        private System.Windows.Forms.CheckBox chkAdvancedFeatures;
        private System.Windows.Forms.Button btnChangeVersionSlotFile2;
        private System.Windows.Forms.Button btnChangeSaveStateIDSlotFile2;
        private System.Windows.Forms.Button btnChangeGameIDSlotFile2;
        private System.Windows.Forms.Button btnImportSRAMSlotFile2;
        private System.Windows.Forms.Label lblSlot2;
        private System.Windows.Forms.ListBox listBoxFile2;
        private System.Windows.Forms.TextBox txtFile2;
        private System.Windows.Forms.Button btnExportSRAMSlotFile2;
        private System.Windows.Forms.Button btnClearSlotFile2;
        private System.Windows.Forms.Button btnPasteSlotFile2;
        private System.Windows.Forms.Button btnCopySlotFile2;
        private System.Windows.Forms.Button btnSaveFile2;
        private System.Windows.Forms.Button btnNewFile2;
        private System.Windows.Forms.Button btnOpenFile2;
        private System.Windows.Forms.Button btnChangeSRAMSizeSlotFile1;
        private System.Windows.Forms.Button btnRecalculateHashSlotFile1;
        private System.Windows.Forms.Label lblSet1;
        private System.Windows.Forms.Label lblSet2;
        private System.Windows.Forms.Button btnRecalculateHashSlotFile2;
        private System.Windows.Forms.Button btnChangeSRAMSizeSlotFile2;
        private System.Windows.Forms.Button btnSaveAsFile1;
        private System.Windows.Forms.Button btnSaveAsFile2;
    }
}

