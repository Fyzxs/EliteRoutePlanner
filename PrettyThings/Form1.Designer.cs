using PrettyThings.Menu;

namespace PrettyThings
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
            System.Windows.Forms.ComboBox cboSystemSelector;
            System.Windows.Forms.ComboBox cboCmdLocSystem;
            System.Windows.Forms.ComboBox cboCmdLocCommodity;
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mniMainFile = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mniMainImport = new System.Windows.Forms.ToolStripMenuItem();
            this.mniMainImportEddb = new System.Windows.Forms.ToolStripMenuItem();
            this.mniMainImportEddbCommodities = new System.Windows.Forms.ToolStripMenuItem();
            this.mniMainImportEddbStations = new System.Windows.Forms.ToolStripMenuItem();
            this.mniMainImportEddbInterwebs = new System.Windows.Forms.ToolStripMenuItem();
            this.mniMainImportOcr = new System.Windows.Forms.ToolStripMenuItem();
            this.mniMainDebug = new System.Windows.Forms.ToolStripMenuItem();
            this.mniMainDebugShowSystemCount = new System.Windows.Forms.ToolStripMenuItem();
            this.mniMainDebugShowStationCount = new System.Windows.Forms.ToolStripMenuItem();
            this.mniMainDebugUseSampleData = new System.Windows.Forms.ToolStripMenuItem();
            this.mniMainDebugShowPathCount = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelLoopingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.stsMain = new System.Windows.Forms.StatusStrip();
            this.stsMainStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsMainProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.btnGenPath = new System.Windows.Forms.Button();
            this.tabControls = new System.Windows.Forms.TabControl();
            this.tabPathing = new System.Windows.Forms.TabPage();
            this.numCredits = new System.Windows.Forms.NumericUpDown();
            this.lblCredits = new System.Windows.Forms.Label();
            this.numCargo = new System.Windows.Forms.NumericUpDown();
            this.lblCargoSize = new System.Windows.Forms.Label();
            this.lblSearchDistanceLightYears = new System.Windows.Forms.Label();
            this.lblDistance = new System.Windows.Forms.Label();
            this.numDistance = new System.Windows.Forms.NumericUpDown();
            this.lblCenterSystem = new System.Windows.Forms.Label();
            this.tabCommodityLocation = new System.Windows.Forms.TabPage();
            this.cmdCmdLocLocate = new System.Windows.Forms.Button();
            this.txtCmdLocResults = new System.Windows.Forms.TextBox();
            this.lblCmdLocLightYears = new System.Windows.Forms.Label();
            this.lblCmdLocSearchDistance = new System.Windows.Forms.Label();
            this.numCmdLocDistance = new System.Windows.Forms.NumericUpDown();
            this.lblCmdLocCommodity = new System.Windows.Forms.Label();
            this.lblCmdLocStartingSystem = new System.Windows.Forms.Label();
            cboSystemSelector = new System.Windows.Forms.ComboBox();
            cboCmdLocSystem = new System.Windows.Forms.ComboBox();
            cboCmdLocCommodity = new System.Windows.Forms.ComboBox();
            this.mnuMain.SuspendLayout();
            this.stsMain.SuspendLayout();
            this.tabControls.SuspendLayout();
            this.tabPathing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCredits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCargo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDistance)).BeginInit();
            this.tabCommodityLocation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCmdLocDistance)).BeginInit();
            this.SuspendLayout();
            // 
            // cboSystemSelector
            // 
            cboSystemSelector.Location = new System.Drawing.Point(107, 6);
            cboSystemSelector.Name = "cboSystemSelector";
            cboSystemSelector.Size = new System.Drawing.Size(121, 21);
            cboSystemSelector.TabIndex = 14;
            cboSystemSelector.Text = "Teaka";
            cboSystemSelector.TextChanged += new System.EventHandler(this.cboSystemSelector_TextChanged);
            // 
            // cboCmdLocSystem
            // 
            cboCmdLocSystem.Location = new System.Drawing.Point(85, 6);
            cboCmdLocSystem.Name = "cboCmdLocSystem";
            cboCmdLocSystem.Size = new System.Drawing.Size(121, 21);
            cboCmdLocSystem.TabIndex = 16;
            cboCmdLocSystem.TextChanged += new System.EventHandler(this.cboSystemSelector_TextChanged);
            // 
            // cboCmdLocCommodity
            // 
            cboCmdLocCommodity.Location = new System.Drawing.Point(85, 33);
            cboCmdLocCommodity.Name = "cboCmdLocCommodity";
            cboCmdLocCommodity.Size = new System.Drawing.Size(121, 21);
            cboCmdLocCommodity.TabIndex = 18;
            cboCmdLocCommodity.TextChanged += new System.EventHandler(this.cboCmdLocCommodity_TextChanged);
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniMainFile,
            this.mniMainImport,
            this.mniMainDebug});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(942, 24);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "menuStrip1";
            // 
            // mniMainFile
            // 
            this.mniMainFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.mniMainFile.Name = "mniMainFile";
            this.mniMainFile.Size = new System.Drawing.Size(37, 20);
            this.mniMainFile.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // mniMainImport
            // 
            this.mniMainImport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniMainImportEddb,
            this.mniMainImportOcr});
            this.mniMainImport.Name = "mniMainImport";
            this.mniMainImport.Size = new System.Drawing.Size(55, 20);
            this.mniMainImport.Text = "Import";
            // 
            // mniMainImportEddb
            // 
            this.mniMainImportEddb.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniMainImportEddbCommodities,
            this.mniMainImportEddbStations,
            this.mniMainImportEddbInterwebs});
            this.mniMainImportEddb.Name = "mniMainImportEddb";
            this.mniMainImportEddb.Size = new System.Drawing.Size(126, 22);
            this.mniMainImportEddb.Text = "EDDB (all)";
            this.mniMainImportEddb.Click += new System.EventHandler(this.mniMainImportEddb_Click);
            // 
            // mniMainImportEddbCommodities
            // 
            this.mniMainImportEddbCommodities.Name = "mniMainImportEddbCommodities";
            this.mniMainImportEddbCommodities.Size = new System.Drawing.Size(146, 22);
            this.mniMainImportEddbCommodities.Text = "Commodities";
            this.mniMainImportEddbCommodities.Click += new System.EventHandler(this.mniMainImportEddbCommodities_Click);
            // 
            // mniMainImportEddbStations
            // 
            this.mniMainImportEddbStations.Name = "mniMainImportEddbStations";
            this.mniMainImportEddbStations.Size = new System.Drawing.Size(146, 22);
            this.mniMainImportEddbStations.Text = "Stations";
            this.mniMainImportEddbStations.Click += new System.EventHandler(this.mniMainImportEddbStations_Click);
            // 
            // mniMainImportEddbInterwebs
            // 
            this.mniMainImportEddbInterwebs.Name = "mniMainImportEddbInterwebs";
            this.mniMainImportEddbInterwebs.Size = new System.Drawing.Size(146, 22);
            this.mniMainImportEddbInterwebs.Text = "INTERWEBS!!!";
            this.mniMainImportEddbInterwebs.Click += new System.EventHandler(this.mniMainImportEddbInterwebs_Click);
            // 
            // mniMainImportOcr
            // 
            this.mniMainImportOcr.Name = "mniMainImportOcr";
            this.mniMainImportOcr.Size = new System.Drawing.Size(126, 22);
            this.mniMainImportOcr.Text = "OCR (csv)";
            this.mniMainImportOcr.Click += new System.EventHandler(this.mniMainImportOcr_Click);
            // 
            // mniMainDebug
            // 
            this.mniMainDebug.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniMainDebugShowSystemCount,
            this.mniMainDebugShowStationCount,
            this.mniMainDebugUseSampleData,
            this.mniMainDebugShowPathCount,
            this.cancelLoopingToolStripMenuItem});
            this.mniMainDebug.Name = "mniMainDebug";
            this.mniMainDebug.Size = new System.Drawing.Size(54, 20);
            this.mniMainDebug.Text = "Debug";
            // 
            // mniMainDebugShowSystemCount
            // 
            this.mniMainDebugShowSystemCount.Name = "mniMainDebugShowSystemCount";
            this.mniMainDebugShowSystemCount.Size = new System.Drawing.Size(180, 22);
            this.mniMainDebugShowSystemCount.Text = "Show System Count";
            this.mniMainDebugShowSystemCount.Click += new System.EventHandler(this.mniMainDebugShowSystemCount_Click);
            // 
            // mniMainDebugShowStationCount
            // 
            this.mniMainDebugShowStationCount.Name = "mniMainDebugShowStationCount";
            this.mniMainDebugShowStationCount.Size = new System.Drawing.Size(180, 22);
            this.mniMainDebugShowStationCount.Text = "Show Station Count";
            this.mniMainDebugShowStationCount.Click += new System.EventHandler(this.mniMainDebugShowStationCount_Click);
            // 
            // mniMainDebugUseSampleData
            // 
            this.mniMainDebugUseSampleData.CheckOnClick = true;
            this.mniMainDebugUseSampleData.Name = "mniMainDebugUseSampleData";
            this.mniMainDebugUseSampleData.Size = new System.Drawing.Size(180, 22);
            this.mniMainDebugUseSampleData.Text = "Use Sample Data";
            // 
            // mniMainDebugShowPathCount
            // 
            this.mniMainDebugShowPathCount.Name = "mniMainDebugShowPathCount";
            this.mniMainDebugShowPathCount.Size = new System.Drawing.Size(180, 22);
            this.mniMainDebugShowPathCount.Text = "Show Path Count";
            this.mniMainDebugShowPathCount.Click += new System.EventHandler(this.mniMainDebugShowPathCount_Click);
            // 
            // cancelLoopingToolStripMenuItem
            // 
            this.cancelLoopingToolStripMenuItem.Name = "cancelLoopingToolStripMenuItem";
            this.cancelLoopingToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cancelLoopingToolStripMenuItem.Text = "Cancel Looping";
            this.cancelLoopingToolStripMenuItem.Click += new System.EventHandler(this.cancelLoopingToolStripMenuItem_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(11, 57);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(908, 371);
            this.textBox1.TabIndex = 1;
            // 
            // stsMain
            // 
            this.stsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stsMainStatus,
            this.stsMainProgress});
            this.stsMain.Location = new System.Drawing.Point(0, 517);
            this.stsMain.Name = "stsMain";
            this.stsMain.Size = new System.Drawing.Size(942, 22);
            this.stsMain.TabIndex = 2;
            this.stsMain.Text = "statusStrip1";
            // 
            // stsMainStatus
            // 
            this.stsMainStatus.Name = "stsMainStatus";
            this.stsMainStatus.Size = new System.Drawing.Size(0, 17);
            this.stsMainStatus.Visible = false;
            // 
            // stsMainProgress
            // 
            this.stsMainProgress.Name = "stsMainProgress";
            this.stsMainProgress.Size = new System.Drawing.Size(100, 16);
            this.stsMainProgress.Visible = false;
            // 
            // btnGenPath
            // 
            this.btnGenPath.Location = new System.Drawing.Point(526, 31);
            this.btnGenPath.Name = "btnGenPath";
            this.btnGenPath.Size = new System.Drawing.Size(75, 23);
            this.btnGenPath.TabIndex = 12;
            this.btnGenPath.Text = "Find Path";
            this.btnGenPath.UseVisualStyleBackColor = true;
            this.btnGenPath.Click += new System.EventHandler(this.btnGenPath_Click);
            // 
            // tabControls
            // 
            this.tabControls.Controls.Add(this.tabPathing);
            this.tabControls.Controls.Add(this.tabCommodityLocation);
            this.tabControls.Location = new System.Drawing.Point(0, 27);
            this.tabControls.Name = "tabControls";
            this.tabControls.SelectedIndex = 0;
            this.tabControls.Size = new System.Drawing.Size(930, 458);
            this.tabControls.TabIndex = 10;
            // 
            // tabPathing
            // 
            this.tabPathing.Controls.Add(this.numCredits);
            this.tabPathing.Controls.Add(this.lblCredits);
            this.tabPathing.Controls.Add(this.numCargo);
            this.tabPathing.Controls.Add(this.lblCargoSize);
            this.tabPathing.Controls.Add(cboSystemSelector);
            this.tabPathing.Controls.Add(this.lblSearchDistanceLightYears);
            this.tabPathing.Controls.Add(this.lblDistance);
            this.tabPathing.Controls.Add(this.numDistance);
            this.tabPathing.Controls.Add(this.lblCenterSystem);
            this.tabPathing.Controls.Add(this.textBox1);
            this.tabPathing.Controls.Add(this.btnGenPath);
            this.tabPathing.Location = new System.Drawing.Point(4, 22);
            this.tabPathing.Name = "tabPathing";
            this.tabPathing.Padding = new System.Windows.Forms.Padding(3);
            this.tabPathing.Size = new System.Drawing.Size(922, 432);
            this.tabPathing.TabIndex = 0;
            this.tabPathing.Text = "Pathing";
            this.tabPathing.UseVisualStyleBackColor = true;
            // 
            // numCredits
            // 
            this.numCredits.Location = new System.Drawing.Point(304, 31);
            this.numCredits.Maximum = new decimal(new int[] {
            2147483640,
            0,
            0,
            0});
            this.numCredits.Name = "numCredits";
            this.numCredits.Size = new System.Drawing.Size(120, 20);
            this.numCredits.TabIndex = 19;
            this.numCredits.Value = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            // 
            // lblCredits
            // 
            this.lblCredits.AutoSize = true;
            this.lblCredits.Location = new System.Drawing.Point(240, 35);
            this.lblCredits.Name = "lblCredits";
            this.lblCredits.Size = new System.Drawing.Size(39, 13);
            this.lblCredits.TabIndex = 18;
            this.lblCredits.Text = "Credits";
            // 
            // numCargo
            // 
            this.numCargo.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.numCargo.Location = new System.Drawing.Point(304, 5);
            this.numCargo.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numCargo.Name = "numCargo";
            this.numCargo.Size = new System.Drawing.Size(120, 20);
            this.numCargo.TabIndex = 17;
            this.numCargo.Value = new decimal(new int[] {
            212,
            0,
            0,
            0});
            // 
            // lblCargoSize
            // 
            this.lblCargoSize.AutoSize = true;
            this.lblCargoSize.Location = new System.Drawing.Point(240, 9);
            this.lblCargoSize.Name = "lblCargoSize";
            this.lblCargoSize.Size = new System.Drawing.Size(58, 13);
            this.lblCargoSize.TabIndex = 15;
            this.lblCargoSize.Text = "Cargo Size";
            // 
            // lblSearchDistanceLightYears
            // 
            this.lblSearchDistanceLightYears.AutoSize = true;
            this.lblSearchDistanceLightYears.Location = new System.Drawing.Point(169, 34);
            this.lblSearchDistanceLightYears.Name = "lblSearchDistanceLightYears";
            this.lblSearchDistanceLightYears.Size = new System.Drawing.Size(19, 13);
            this.lblSearchDistanceLightYears.TabIndex = 13;
            this.lblSearchDistanceLightYears.Text = "lys";
            // 
            // lblDistance
            // 
            this.lblDistance.AutoSize = true;
            this.lblDistance.Location = new System.Drawing.Point(8, 34);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(86, 13);
            this.lblDistance.TabIndex = 12;
            this.lblDistance.Text = "Search Distance";
            // 
            // numDistance
            // 
            this.numDistance.DecimalPlaces = 2;
            this.numDistance.Location = new System.Drawing.Point(107, 32);
            this.numDistance.Name = "numDistance";
            this.numDistance.Size = new System.Drawing.Size(56, 20);
            this.numDistance.TabIndex = 11;
            this.numDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numDistance.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblCenterSystem
            // 
            this.lblCenterSystem.AutoSize = true;
            this.lblCenterSystem.Location = new System.Drawing.Point(8, 9);
            this.lblCenterSystem.Name = "lblCenterSystem";
            this.lblCenterSystem.Size = new System.Drawing.Size(80, 13);
            this.lblCenterSystem.TabIndex = 9;
            this.lblCenterSystem.Text = "Starting System";
            // 
            // tabCommodityLocation
            // 
            this.tabCommodityLocation.Controls.Add(this.cmdCmdLocLocate);
            this.tabCommodityLocation.Controls.Add(this.txtCmdLocResults);
            this.tabCommodityLocation.Controls.Add(this.lblCmdLocLightYears);
            this.tabCommodityLocation.Controls.Add(this.lblCmdLocSearchDistance);
            this.tabCommodityLocation.Controls.Add(this.numCmdLocDistance);
            this.tabCommodityLocation.Controls.Add(cboCmdLocCommodity);
            this.tabCommodityLocation.Controls.Add(this.lblCmdLocCommodity);
            this.tabCommodityLocation.Controls.Add(cboCmdLocSystem);
            this.tabCommodityLocation.Controls.Add(this.lblCmdLocStartingSystem);
            this.tabCommodityLocation.Location = new System.Drawing.Point(4, 22);
            this.tabCommodityLocation.Name = "tabCommodityLocation";
            this.tabCommodityLocation.Padding = new System.Windows.Forms.Padding(3);
            this.tabCommodityLocation.Size = new System.Drawing.Size(922, 432);
            this.tabCommodityLocation.TabIndex = 1;
            this.tabCommodityLocation.Text = "Commodity Locator";
            this.tabCommodityLocation.UseVisualStyleBackColor = true;
            // 
            // cmdCmdLocLocate
            // 
            this.cmdCmdLocLocate.Location = new System.Drawing.Point(218, 33);
            this.cmdCmdLocLocate.Name = "cmdCmdLocLocate";
            this.cmdCmdLocLocate.Size = new System.Drawing.Size(75, 23);
            this.cmdCmdLocLocate.TabIndex = 23;
            this.cmdCmdLocLocate.Text = "Locate";
            this.cmdCmdLocLocate.UseVisualStyleBackColor = true;
            this.cmdCmdLocLocate.Click += new System.EventHandler(this.cmdCmdLocLocate_Click);
            // 
            // txtCmdLocResults
            // 
            this.txtCmdLocResults.Location = new System.Drawing.Point(8, 60);
            this.txtCmdLocResults.Multiline = true;
            this.txtCmdLocResults.Name = "txtCmdLocResults";
            this.txtCmdLocResults.Size = new System.Drawing.Size(908, 366);
            this.txtCmdLocResults.TabIndex = 22;
            // 
            // lblCmdLocLightYears
            // 
            this.lblCmdLocLightYears.AutoSize = true;
            this.lblCmdLocLightYears.Location = new System.Drawing.Point(376, 11);
            this.lblCmdLocLightYears.Name = "lblCmdLocLightYears";
            this.lblCmdLocLightYears.Size = new System.Drawing.Size(19, 13);
            this.lblCmdLocLightYears.TabIndex = 21;
            this.lblCmdLocLightYears.Text = "lys";
            // 
            // lblCmdLocSearchDistance
            // 
            this.lblCmdLocSearchDistance.AutoSize = true;
            this.lblCmdLocSearchDistance.Location = new System.Drawing.Point(215, 11);
            this.lblCmdLocSearchDistance.Name = "lblCmdLocSearchDistance";
            this.lblCmdLocSearchDistance.Size = new System.Drawing.Size(86, 13);
            this.lblCmdLocSearchDistance.TabIndex = 20;
            this.lblCmdLocSearchDistance.Text = "Search Distance";
            // 
            // numCmdLocDistance
            // 
            this.numCmdLocDistance.DecimalPlaces = 2;
            this.numCmdLocDistance.Location = new System.Drawing.Point(314, 9);
            this.numCmdLocDistance.Name = "numCmdLocDistance";
            this.numCmdLocDistance.Size = new System.Drawing.Size(56, 20);
            this.numCmdLocDistance.TabIndex = 19;
            this.numCmdLocDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numCmdLocDistance.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblCmdLocCommodity
            // 
            this.lblCmdLocCommodity.AutoSize = true;
            this.lblCmdLocCommodity.Location = new System.Drawing.Point(4, 36);
            this.lblCmdLocCommodity.Name = "lblCmdLocCommodity";
            this.lblCmdLocCommodity.Size = new System.Drawing.Size(58, 13);
            this.lblCmdLocCommodity.TabIndex = 17;
            this.lblCmdLocCommodity.Text = "Commodity";
            // 
            // lblCmdLocStartingSystem
            // 
            this.lblCmdLocStartingSystem.AutoSize = true;
            this.lblCmdLocStartingSystem.Location = new System.Drawing.Point(4, 9);
            this.lblCmdLocStartingSystem.Name = "lblCmdLocStartingSystem";
            this.lblCmdLocStartingSystem.Size = new System.Drawing.Size(80, 13);
            this.lblCmdLocStartingSystem.TabIndex = 15;
            this.lblCmdLocStartingSystem.Text = "Starting System";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 539);
            this.Controls.Add(this.tabControls);
            this.Controls.Add(this.stsMain);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = this.mnuMain;
            this.Name = "Form1";
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.stsMain.ResumeLayout(false);
            this.stsMain.PerformLayout();
            this.tabControls.ResumeLayout(false);
            this.tabPathing.ResumeLayout(false);
            this.tabPathing.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCredits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCargo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDistance)).EndInit();
            this.tabCommodityLocation.ResumeLayout(false);
            this.tabCommodityLocation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCmdLocDistance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mniMainFile;
        private System.Windows.Forms.ToolStripMenuItem mniMainImport;
        private System.Windows.Forms.ToolStripMenuItem mniMainDebug;
        private System.Windows.Forms.ToolStripMenuItem mniMainDebugShowSystemCount;
        private System.Windows.Forms.ToolStripMenuItem mniMainDebugShowStationCount;
        private System.Windows.Forms.StatusStrip stsMain;
        internal System.Windows.Forms.TextBox textBox1;
        internal System.Windows.Forms.ToolStripStatusLabel stsMainStatus;
        internal System.Windows.Forms.ToolStripProgressBar stsMainProgress;
        internal System.Windows.Forms.ToolStripMenuItem mniMainDebugUseSampleData;
        private System.Windows.Forms.ToolStripMenuItem mniMainDebugShowPathCount;
        internal System.Windows.Forms.Button btnGenPath;
        private System.Windows.Forms.ToolStripMenuItem mniMainImportOcr;
        private System.Windows.Forms.ToolStripMenuItem mniMainImportEddbCommodities;
        private System.Windows.Forms.ToolStripMenuItem mniMainImportEddbStations;
        internal System.Windows.Forms.ToolStripMenuItem mniMainImportEddb;
        private System.Windows.Forms.ToolStripMenuItem mniMainImportEddbInterwebs;
        private System.Windows.Forms.ToolStripMenuItem cancelLoopingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPathing;
        private System.Windows.Forms.Label lblSearchDistanceLightYears;
        private System.Windows.Forms.Label lblDistance;
        internal System.Windows.Forms.NumericUpDown numDistance;
        private System.Windows.Forms.Label lblCenterSystem;
        private System.Windows.Forms.TabPage tabCommodityLocation;
        public System.Windows.Forms.TabControl tabControls;
        private System.Windows.Forms.NumericUpDown numCredits;
        private System.Windows.Forms.Label lblCredits;
        private System.Windows.Forms.NumericUpDown numCargo;
        private System.Windows.Forms.Label lblCargoSize;
        private System.Windows.Forms.Label lblCmdLocLightYears;
        private System.Windows.Forms.Label lblCmdLocSearchDistance;
        internal System.Windows.Forms.NumericUpDown numCmdLocDistance;
        private System.Windows.Forms.Label lblCmdLocCommodity;
        private System.Windows.Forms.Label lblCmdLocStartingSystem;
        private System.Windows.Forms.Button cmdCmdLocLocate;
        private System.Windows.Forms.TextBox txtCmdLocResults;
    }
}

