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
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mniMainFile = new System.Windows.Forms.ToolStripMenuItem();
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.stsMain = new System.Windows.Forms.StatusStrip();
            this.stsMainStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsMainProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.lblCenterSystem = new System.Windows.Forms.Label();
            this.txtStartingSystem = new System.Windows.Forms.TextBox();
            this.btnGenPath = new System.Windows.Forms.Button();
            this.numDistance = new System.Windows.Forms.NumericUpDown();
            this.lblDistance = new System.Windows.Forms.Label();
            this.lblSearchDistanceLightYears = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.cancelLoopingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain.SuspendLayout();
            this.stsMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDistance)).BeginInit();
            this.SuspendLayout();
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
            this.mniMainFile.Name = "mniMainFile";
            this.mniMainFile.Size = new System.Drawing.Size(37, 20);
            this.mniMainFile.Text = "File";
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
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(0, 193);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(930, 321);
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
            // lblCenterSystem
            // 
            this.lblCenterSystem.AutoSize = true;
            this.lblCenterSystem.Location = new System.Drawing.Point(-3, 27);
            this.lblCenterSystem.Name = "lblCenterSystem";
            this.lblCenterSystem.Size = new System.Drawing.Size(80, 13);
            this.lblCenterSystem.TabIndex = 3;
            this.lblCenterSystem.Text = "Starting System";
            // 
            // txtStartingSystem
            // 
            this.txtStartingSystem.Location = new System.Drawing.Point(83, 24);
            this.txtStartingSystem.Name = "txtStartingSystem";
            this.txtStartingSystem.Size = new System.Drawing.Size(100, 20);
            this.txtStartingSystem.TabIndex = 4;
            // 
            // btnGenPath
            // 
            this.btnGenPath.Location = new System.Drawing.Point(203, 47);
            this.btnGenPath.Name = "btnGenPath";
            this.btnGenPath.Size = new System.Drawing.Size(75, 23);
            this.btnGenPath.TabIndex = 5;
            this.btnGenPath.Text = "Find Path";
            this.btnGenPath.UseVisualStyleBackColor = true;
            this.btnGenPath.Click += new System.EventHandler(this.btnGenPath_Click);
            // 
            // numDistance
            // 
            this.numDistance.DecimalPlaces = 2;
            this.numDistance.Location = new System.Drawing.Point(102, 50);
            this.numDistance.Name = "numDistance";
            this.numDistance.Size = new System.Drawing.Size(56, 20);
            this.numDistance.TabIndex = 6;
            this.numDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDistance
            // 
            this.lblDistance.AutoSize = true;
            this.lblDistance.Location = new System.Drawing.Point(-3, 52);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(86, 13);
            this.lblDistance.TabIndex = 7;
            this.lblDistance.Text = "Search Distance";
            // 
            // lblSearchDistanceLightYears
            // 
            this.lblSearchDistanceLightYears.AutoSize = true;
            this.lblSearchDistanceLightYears.Location = new System.Drawing.Point(164, 52);
            this.lblSearchDistanceLightYears.Name = "lblSearchDistanceLightYears";
            this.lblSearchDistanceLightYears.Size = new System.Drawing.Size(19, 13);
            this.lblSearchDistanceLightYears.TabIndex = 8;
            this.lblSearchDistanceLightYears.Text = "lys";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(855, 96);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Find Path";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(6, 96);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox2.Size = new System.Drawing.Size(843, 91);
            this.textBox2.TabIndex = 10;
            // 
            // cancelLoopingToolStripMenuItem
            // 
            this.cancelLoopingToolStripMenuItem.Name = "cancelLoopingToolStripMenuItem";
            this.cancelLoopingToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cancelLoopingToolStripMenuItem.Text = "Cancel Looping";
            this.cancelLoopingToolStripMenuItem.Click += new System.EventHandler(this.cancelLoopingToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 539);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblSearchDistanceLightYears);
            this.Controls.Add(this.lblDistance);
            this.Controls.Add(this.numDistance);
            this.Controls.Add(this.btnGenPath);
            this.Controls.Add(this.txtStartingSystem);
            this.Controls.Add(this.lblCenterSystem);
            this.Controls.Add(this.stsMain);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = this.mnuMain;
            this.Name = "Form1";
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.stsMain.ResumeLayout(false);
            this.stsMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDistance)).EndInit();
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
        private System.Windows.Forms.Label lblCenterSystem;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.Label lblSearchDistanceLightYears;
        private System.Windows.Forms.ToolStripMenuItem mniMainDebugShowPathCount;
        internal System.Windows.Forms.TextBox txtStartingSystem;
        internal System.Windows.Forms.NumericUpDown numDistance;
        internal System.Windows.Forms.Button btnGenPath;
        private System.Windows.Forms.ToolStripMenuItem mniMainImportOcr;
        private System.Windows.Forms.ToolStripMenuItem mniMainImportEddbCommodities;
        private System.Windows.Forms.ToolStripMenuItem mniMainImportEddbStations;
        internal System.Windows.Forms.ToolStripMenuItem mniMainImportEddb;
        internal System.Windows.Forms.Button button1;
        internal System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ToolStripMenuItem mniMainImportEddbInterwebs;
        private System.Windows.Forms.ToolStripMenuItem cancelLoopingToolStripMenuItem;
    }
}

