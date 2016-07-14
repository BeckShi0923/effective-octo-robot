namespace NET_Project
{
    partial class MatchingParameters
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MatchingParameters));
            this.contrastBar = new System.Windows.Forms.HScrollBar();
            this.label1 = new System.Windows.Forms.Label();
            this.ManualRadio = new System.Windows.Forms.RadioButton();
            this.CentroidRadio = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.OptimizationComboBox = new System.Windows.Forms.ComboBox();
            this.AngleExtentText = new System.Windows.Forms.TextBox();
            this.AngleExtentLabel = new System.Windows.Forms.Label();
            this.AngleStartLabel = new System.Windows.Forms.Label();
            this.AngleStartText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.minScoreScroll = new System.Windows.Forms.HScrollBar();
            this.ErrorMessage = new System.Windows.Forms.Label();
            this.CreateModel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.OverlapLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SubPixLabel = new System.Windows.Forms.Label();
            this.GreedinessLabel = new System.Windows.Forms.Label();
            this.NumLevelsLabel = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.FindButton = new System.Windows.Forms.Button();
            this.NumLevelsTextBox = new System.Windows.Forms.TextBox();
            this.GreedinessValue = new System.Windows.Forms.Label();
            this.GreedinessScroll = new System.Windows.Forms.HScrollBar();
            this.SubPixComboBox = new System.Windows.Forms.ComboBox();
            this.NumMatchTextBox = new System.Windows.Forms.TextBox();
            this.OverlapScroll = new System.Windows.Forms.HScrollBar();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // contrastBar
            // 
            this.contrastBar.LargeChange = 1;
            resources.ApplyResources(this.contrastBar, "contrastBar");
            this.contrastBar.Maximum = 255;
            this.contrastBar.Name = "contrastBar";
            this.contrastBar.Value = 30;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // ManualRadio
            // 
            resources.ApplyResources(this.ManualRadio, "ManualRadio");
            this.ManualRadio.Name = "ManualRadio";
            this.ManualRadio.TabStop = true;
            this.ManualRadio.UseVisualStyleBackColor = true;
            // 
            // CentroidRadio
            // 
            resources.ApplyResources(this.CentroidRadio, "CentroidRadio");
            this.CentroidRadio.Name = "CentroidRadio";
            this.CentroidRadio.TabStop = true;
            this.CentroidRadio.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.hWindowControl1);
            this.groupBox1.Controls.Add(this.ManualRadio);
            this.groupBox1.Controls.Add(this.CentroidRadio);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            resources.ApplyResources(this.hWindowControl1, "hWindowControl1");
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.WindowSize = new System.Drawing.Size(240, 204);
            // 
            // OptimizationComboBox
            // 
            this.OptimizationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OptimizationComboBox.FormattingEnabled = true;
            this.OptimizationComboBox.Items.AddRange(new object[] {
            resources.GetString("OptimizationComboBox.Items"),
            resources.GetString("OptimizationComboBox.Items1"),
            resources.GetString("OptimizationComboBox.Items2"),
            resources.GetString("OptimizationComboBox.Items3"),
            resources.GetString("OptimizationComboBox.Items4"),
            resources.GetString("OptimizationComboBox.Items5"),
            resources.GetString("OptimizationComboBox.Items6")});
            resources.ApplyResources(this.OptimizationComboBox, "OptimizationComboBox");
            this.OptimizationComboBox.Name = "OptimizationComboBox";
            // 
            // AngleExtentText
            // 
            resources.ApplyResources(this.AngleExtentText, "AngleExtentText");
            this.AngleExtentText.Name = "AngleExtentText";
            this.AngleExtentText.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // AngleExtentLabel
            // 
            resources.ApplyResources(this.AngleExtentLabel, "AngleExtentLabel");
            this.AngleExtentLabel.Name = "AngleExtentLabel";
            this.toolTip1.SetToolTip(this.AngleExtentLabel, resources.GetString("AngleExtentLabel.ToolTip"));
            // 
            // AngleStartLabel
            // 
            resources.ApplyResources(this.AngleStartLabel, "AngleStartLabel");
            this.AngleStartLabel.Name = "AngleStartLabel";
            this.toolTip1.SetToolTip(this.AngleStartLabel, resources.GetString("AngleStartLabel.ToolTip"));
            // 
            // AngleStartText
            // 
            resources.ApplyResources(this.AngleStartText, "AngleStartText");
            this.AngleStartText.Name = "AngleStartText";
            this.AngleStartText.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.toolTip1.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            this.toolTip1.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
            // 
            // minScoreScroll
            // 
            this.minScoreScroll.LargeChange = 1;
            resources.ApplyResources(this.minScoreScroll, "minScoreScroll");
            this.minScoreScroll.Name = "minScoreScroll";
            this.minScoreScroll.Value = 50;
            // 
            // ErrorMessage
            // 
            resources.ApplyResources(this.ErrorMessage, "ErrorMessage");
            this.ErrorMessage.Name = "ErrorMessage";
            // 
            // CreateModel
            // 
            resources.ApplyResources(this.CreateModel, "CreateModel");
            this.CreateModel.Name = "CreateModel";
            this.CreateModel.UseVisualStyleBackColor = true;
            // 
            // OverlapLabel
            // 
            resources.ApplyResources(this.OverlapLabel, "OverlapLabel");
            this.OverlapLabel.Name = "OverlapLabel";
            this.toolTip1.SetToolTip(this.OverlapLabel, resources.GetString("OverlapLabel.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // SubPixLabel
            // 
            resources.ApplyResources(this.SubPixLabel, "SubPixLabel");
            this.SubPixLabel.Name = "SubPixLabel";
            this.toolTip1.SetToolTip(this.SubPixLabel, resources.GetString("SubPixLabel.ToolTip"));
            // 
            // GreedinessLabel
            // 
            resources.ApplyResources(this.GreedinessLabel, "GreedinessLabel");
            this.GreedinessLabel.Name = "GreedinessLabel";
            this.toolTip1.SetToolTip(this.GreedinessLabel, resources.GetString("GreedinessLabel.ToolTip"));
            // 
            // NumLevelsLabel
            // 
            resources.ApplyResources(this.NumLevelsLabel, "NumLevelsLabel");
            this.NumLevelsLabel.Name = "NumLevelsLabel";
            this.toolTip1.SetToolTip(this.NumLevelsLabel, resources.GetString("NumLevelsLabel.ToolTip"));
            // 
            // Cancel
            // 
            resources.ApplyResources(this.Cancel, "Cancel");
            this.Cancel.Name = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.FindButton);
            this.groupBox2.Controls.Add(this.NumLevelsTextBox);
            this.groupBox2.Controls.Add(this.NumLevelsLabel);
            this.groupBox2.Controls.Add(this.GreedinessValue);
            this.groupBox2.Controls.Add(this.GreedinessScroll);
            this.groupBox2.Controls.Add(this.GreedinessLabel);
            this.groupBox2.Controls.Add(this.SubPixComboBox);
            this.groupBox2.Controls.Add(this.SubPixLabel);
            this.groupBox2.Controls.Add(this.NumMatchTextBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.OverlapScroll);
            this.groupBox2.Controls.Add(this.OverlapLabel);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.minScoreScroll);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // FindButton
            // 
            resources.ApplyResources(this.FindButton, "FindButton");
            this.FindButton.Name = "FindButton";
            this.FindButton.UseVisualStyleBackColor = true;
            // 
            // NumLevelsTextBox
            // 
            resources.ApplyResources(this.NumLevelsTextBox, "NumLevelsTextBox");
            this.NumLevelsTextBox.Name = "NumLevelsTextBox";
            this.NumLevelsTextBox.TextChanged += new System.EventHandler(this.NumLevelsTextBox_TextChanged);
            // 
            // GreedinessValue
            // 
            resources.ApplyResources(this.GreedinessValue, "GreedinessValue");
            this.GreedinessValue.Name = "GreedinessValue";
            // 
            // GreedinessScroll
            // 
            resources.ApplyResources(this.GreedinessScroll, "GreedinessScroll");
            this.GreedinessScroll.LargeChange = 1;
            this.GreedinessScroll.Name = "GreedinessScroll";
            this.GreedinessScroll.Value = 90;
            this.GreedinessScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.GreedinessScroll_Scroll);
            // 
            // SubPixComboBox
            // 
            this.SubPixComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SubPixComboBox.FormattingEnabled = true;
            this.SubPixComboBox.Items.AddRange(new object[] {
            resources.GetString("SubPixComboBox.Items"),
            resources.GetString("SubPixComboBox.Items1"),
            resources.GetString("SubPixComboBox.Items2"),
            resources.GetString("SubPixComboBox.Items3"),
            resources.GetString("SubPixComboBox.Items4"),
            resources.GetString("SubPixComboBox.Items5"),
            resources.GetString("SubPixComboBox.Items6"),
            resources.GetString("SubPixComboBox.Items7"),
            resources.GetString("SubPixComboBox.Items8"),
            resources.GetString("SubPixComboBox.Items9"),
            resources.GetString("SubPixComboBox.Items10")});
            resources.ApplyResources(this.SubPixComboBox, "SubPixComboBox");
            this.SubPixComboBox.Name = "SubPixComboBox";
            // 
            // NumMatchTextBox
            // 
            resources.ApplyResources(this.NumMatchTextBox, "NumMatchTextBox");
            this.NumMatchTextBox.Name = "NumMatchTextBox";
            this.NumMatchTextBox.TextChanged += new System.EventHandler(this.NumMatchTextBox_TextChanged);
            // 
            // OverlapScroll
            // 
            this.OverlapScroll.LargeChange = 1;
            resources.ApplyResources(this.OverlapScroll, "OverlapScroll");
            this.OverlapScroll.Name = "OverlapScroll";
            this.OverlapScroll.Value = 50;
            // 
            // MatchingParameters
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.CreateModel);
            this.Controls.Add(this.ErrorMessage);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.AngleStartLabel);
            this.Controls.Add(this.AngleStartText);
            this.Controls.Add(this.AngleExtentLabel);
            this.Controls.Add(this.AngleExtentText);
            this.Controls.Add(this.OptimizationComboBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.contrastBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "MatchingParameters";
            this.ShowInTaskbar = false;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.HScrollBar contrastBar;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.RadioButton ManualRadio;
        public System.Windows.Forms.RadioButton CentroidRadio;
        private System.Windows.Forms.GroupBox groupBox1;
        public HalconDotNet.HWindowControl hWindowControl1;
        public System.Windows.Forms.ComboBox OptimizationComboBox;
        private System.Windows.Forms.TextBox AngleExtentText;
        private System.Windows.Forms.Label AngleExtentLabel;
        private System.Windows.Forms.Label AngleStartLabel;
        private System.Windows.Forms.TextBox AngleStartText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.HScrollBar minScoreScroll;
        public System.Windows.Forms.Label ErrorMessage;
        public System.Windows.Forms.Button CreateModel;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.HScrollBar OverlapScroll;
        private System.Windows.Forms.Label OverlapLabel;
        public System.Windows.Forms.ComboBox SubPixComboBox;
        private System.Windows.Forms.Label SubPixLabel;
        private System.Windows.Forms.TextBox NumMatchTextBox;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.HScrollBar GreedinessScroll;
        private System.Windows.Forms.Label GreedinessLabel;
        private System.Windows.Forms.Label GreedinessValue;
        private System.Windows.Forms.TextBox NumLevelsTextBox;
        private System.Windows.Forms.Label NumLevelsLabel;
        public System.Windows.Forms.Button FindButton;
    }
}