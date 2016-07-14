namespace NET_Project
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grabImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.liveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewModel = new System.Windows.Forms.ToolStripMenuItem();
            this.findModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calibrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executeCalibrationCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.measureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analyzeDefectBlobToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edgeDetectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openImg = new System.Windows.Forms.OpenFileDialog();
            this.MousePosRow = new System.Windows.Forms.Label();
            this.MousePosCol = new System.Windows.Forms.Label();
            this.LogTextBox = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SelectRegionButton = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hWindowControl = new HalconDotNet.HWindowControl();
            this.saveShapeModel = new System.Windows.Forms.SaveFileDialog();
            this.openShapeModel = new System.Windows.Forms.OpenFileDialog();
            this.Execute = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imageToolStripMenuItem,
            this.modelToolStripMenuItem,
            this.calibrationToolStripMenuItem,
            this.measureToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(808, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.grabImageToolStripMenuItem});
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.imageToolStripMenuItem.Text = "Image";
            // 
            // grabImageToolStripMenuItem
            // 
            this.grabImageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fToolStripMenuItem,
            this.liveImageToolStripMenuItem});
            this.grabImageToolStripMenuItem.Name = "grabImageToolStripMenuItem";
            this.grabImageToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.grabImageToolStripMenuItem.Text = "Grab Image...";
            // 
            // fToolStripMenuItem
            // 
            this.fToolStripMenuItem.Name = "fToolStripMenuItem";
            this.fToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.fToolStripMenuItem.Text = "From File";
            this.fToolStripMenuItem.Click += new System.EventHandler(this.fToolStripMenuItem_Click);
            // 
            // liveImageToolStripMenuItem
            // 
            this.liveImageToolStripMenuItem.Name = "liveImageToolStripMenuItem";
            this.liveImageToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.liveImageToolStripMenuItem.Text = "Live Image";
            this.liveImageToolStripMenuItem.Click += new System.EventHandler(this.liveImageToolStripMenuItem_Click);
            // 
            // modelToolStripMenuItem
            // 
            this.modelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createModelToolStripMenuItem,
            this.viewModel,
            this.findModelToolStripMenuItem});
            this.modelToolStripMenuItem.Name = "modelToolStripMenuItem";
            this.modelToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.modelToolStripMenuItem.Text = "Model";
            // 
            // createModelToolStripMenuItem
            // 
            this.createModelToolStripMenuItem.Name = "createModelToolStripMenuItem";
            this.createModelToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.createModelToolStripMenuItem.Text = "Create Model";
            this.createModelToolStripMenuItem.Click += new System.EventHandler(this.createModelToolStripMenuItem_Click);
            // 
            // viewModel
            // 
            this.viewModel.Name = "viewModel";
            this.viewModel.Size = new System.Drawing.Size(133, 22);
            this.viewModel.Text = "View Model";
            this.viewModel.Click += new System.EventHandler(this.viewModel_Click);
            // 
            // findModelToolStripMenuItem
            // 
            this.findModelToolStripMenuItem.Name = "findModelToolStripMenuItem";
            this.findModelToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.findModelToolStripMenuItem.Text = "Find Model";
            this.findModelToolStripMenuItem.Click += new System.EventHandler(this.findModelToolStripMenuItem_Click);
            // 
            // calibrationToolStripMenuItem
            // 
            this.calibrationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.executeCalibrationCodeToolStripMenuItem});
            this.calibrationToolStripMenuItem.Name = "calibrationToolStripMenuItem";
            this.calibrationToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.calibrationToolStripMenuItem.Text = "Calibration";
            // 
            // executeCalibrationCodeToolStripMenuItem
            // 
            this.executeCalibrationCodeToolStripMenuItem.Name = "executeCalibrationCodeToolStripMenuItem";
            this.executeCalibrationCodeToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.executeCalibrationCodeToolStripMenuItem.Text = "Pose Estimation";
            this.executeCalibrationCodeToolStripMenuItem.Click += new System.EventHandler(this.executeCalibrationCodeToolStripMenuItem_Click);
            // 
            // measureToolStripMenuItem
            // 
            this.measureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.analyzeDefectBlobToolStripMenuItem,
            this.edgeDetectionToolStripMenuItem});
            this.measureToolStripMenuItem.Name = "measureToolStripMenuItem";
            this.measureToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.measureToolStripMenuItem.Text = "Measure";
            // 
            // analyzeDefectBlobToolStripMenuItem
            // 
            this.analyzeDefectBlobToolStripMenuItem.Name = "analyzeDefectBlobToolStripMenuItem";
            this.analyzeDefectBlobToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.analyzeDefectBlobToolStripMenuItem.Text = "Analyze Defect Blob";
            this.analyzeDefectBlobToolStripMenuItem.Click += new System.EventHandler(this.analyzeDefectBlobToolStripMenuItem_Click);
            // 
            // edgeDetectionToolStripMenuItem
            // 
            this.edgeDetectionToolStripMenuItem.Name = "edgeDetectionToolStripMenuItem";
            this.edgeDetectionToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.edgeDetectionToolStripMenuItem.Text = "Edge Detection";
            // 
            // openImg
            // 
            this.openImg.FileName = "openImg";
            this.openImg.Filter = "All Image Files|*.PNG;*.GIF;*.BMP;*.JPG|GIF Files|*.GIF|Windows Bitmaps|*.BMP|JPE" +
    "G Files|*.JPG";
            // 
            // MousePosRow
            // 
            this.MousePosRow.AutoSize = true;
            this.MousePosRow.Location = new System.Drawing.Point(712, 476);
            this.MousePosRow.Name = "MousePosRow";
            this.MousePosRow.Size = new System.Drawing.Size(30, 12);
            this.MousePosRow.TabIndex = 2;
            this.MousePosRow.Text = "Row:";
            // 
            // MousePosCol
            // 
            this.MousePosCol.AutoSize = true;
            this.MousePosCol.Location = new System.Drawing.Point(717, 488);
            this.MousePosCol.Name = "MousePosCol";
            this.MousePosCol.Size = new System.Drawing.Size(25, 12);
            this.MousePosCol.TabIndex = 3;
            this.MousePosCol.Text = "Col:";
            // 
            // LogTextBox
            // 
            this.LogTextBox.Location = new System.Drawing.Point(12, 505);
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.Size = new System.Drawing.Size(695, 85);
            this.LogTextBox.TabIndex = 4;
            this.LogTextBox.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SelectRegionButton,
            this.saveImageToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(135, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // SelectRegionButton
            // 
            this.SelectRegionButton.Name = "SelectRegionButton";
            this.SelectRegionButton.Size = new System.Drawing.Size(134, 22);
            this.SelectRegionButton.Text = "Select Region";
            this.SelectRegionButton.Click += new System.EventHandler(this.SelectRegionButton_Click);
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.saveImageToolStripMenuItem.Text = "Save Image";
            this.saveImageToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            // 
            // hWindowControl
            // 
            this.hWindowControl.BackColor = System.Drawing.Color.Black;
            this.hWindowControl.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl.Location = new System.Drawing.Point(11, 40);
            this.hWindowControl.Margin = new System.Windows.Forms.Padding(2);
            this.hWindowControl.Name = "hWindowControl";
            this.hWindowControl.Size = new System.Drawing.Size(696, 460);
            this.hWindowControl.TabIndex = 0;
            this.hWindowControl.WindowSize = new System.Drawing.Size(696, 460);
            this.hWindowControl.HMouseMove += new HalconDotNet.HMouseEventHandler(this.hWindowControl_HMouseMove);
            this.hWindowControl.HMouseDown += new HalconDotNet.HMouseEventHandler(this.hWindowControl_HMouseDown);
            // 
            // saveShapeModel
            // 
            this.saveShapeModel.Filter = "mod files|*.mod";
            // 
            // openShapeModel
            // 
            this.openShapeModel.FileName = "*.mod";
            this.openShapeModel.Filter = "MOD files|*.mod";
            // 
            // Execute
            // 
            this.Execute.Location = new System.Drawing.Point(714, 85);
            this.Execute.Name = "Execute";
            this.Execute.Size = new System.Drawing.Size(75, 23);
            this.Execute.TabIndex = 5;
            this.Execute.Text = "Execute";
            this.Execute.UseVisualStyleBackColor = true;
            this.Execute.Click += new System.EventHandler(this.Execute_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 596);
            this.Controls.Add(this.Execute);
            this.Controls.Add(this.LogTextBox);
            this.Controls.Add(this.MousePosCol);
            this.Controls.Add(this.MousePosRow);
            this.Controls.Add(this.hWindowControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "NET PROJECT";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grabImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openImg;
        private System.Windows.Forms.Label MousePosRow;
        private System.Windows.Forms.Label MousePosCol;
        private System.Windows.Forms.RichTextBox LogTextBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem SelectRegionButton;
        private System.Windows.Forms.ToolStripMenuItem modelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findModelToolStripMenuItem;
        public HalconDotNet.HWindowControl hWindowControl;
        private System.Windows.Forms.SaveFileDialog saveShapeModel;
        private System.Windows.Forms.ToolStripMenuItem viewModel;
        private System.Windows.Forms.OpenFileDialog openShapeModel;
        private System.Windows.Forms.ToolStripMenuItem calibrationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem measureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem liveImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem executeCalibrationCodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analyzeDefectBlobToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem edgeDetectionToolStripMenuItem;
        private System.Windows.Forms.Button Execute;
    }
}

