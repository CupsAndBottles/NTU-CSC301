namespace MRTForm
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Form1(string a)
        {
            InitializeComponent();
            initStationList();
        }

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
        	this.startStation = new System.Windows.Forms.ComboBox();
        	this.endStation = new System.Windows.Forms.ComboBox();
        	this.computeBtn = new System.Windows.Forms.Button();
        	this.startLabel = new System.Windows.Forms.Label();
        	this.endLabel = new System.Windows.Forms.Label();
        	this.mrtPanel = new System.Windows.Forms.Panel();
        	this.txtResult = new System.Windows.Forms.TextBox();
        	this.btnClear = new System.Windows.Forms.Button();
        	this.SuspendLayout();
        	// 
        	// startStation
        	// 
        	this.startStation.AccessibleName = "startStation";
        	this.startStation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.startStation.FormattingEnabled = true;
        	this.startStation.Location = new System.Drawing.Point(97, 43);
        	this.startStation.Name = "startStation";
        	this.startStation.Size = new System.Drawing.Size(312, 21);
        	this.startStation.TabIndex = 0;
        	// 
        	// endStation
        	// 
        	this.endStation.AccessibleName = "endStation";
        	this.endStation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.endStation.FormattingEnabled = true;
        	this.endStation.Location = new System.Drawing.Point(97, 107);
        	this.endStation.Name = "endStation";
        	this.endStation.Size = new System.Drawing.Size(312, 21);
        	this.endStation.TabIndex = 1;
        	// 
        	// computeBtn
        	// 
        	this.computeBtn.AccessibleName = "computeBtn";
        	this.computeBtn.Location = new System.Drawing.Point(253, 173);
        	this.computeBtn.Name = "computeBtn";
        	this.computeBtn.Size = new System.Drawing.Size(75, 23);
        	this.computeBtn.TabIndex = 2;
        	this.computeBtn.Text = "Compute!";
        	this.computeBtn.UseVisualStyleBackColor = true;
        	this.computeBtn.Click += new System.EventHandler(this.computeBtn_Click);
        	// 
        	// startLabel
        	// 
        	this.startLabel.AccessibleName = "startLabel";
        	this.startLabel.AutoSize = true;
        	this.startLabel.Location = new System.Drawing.Point(26, 46);
        	this.startLabel.Name = "startLabel";
        	this.startLabel.Size = new System.Drawing.Size(68, 13);
        	this.startLabel.TabIndex = 4;
        	this.startLabel.Text = "Start Station:";
        	// 
        	// endLabel
        	// 
        	this.endLabel.AccessibleName = "endLabel";
        	this.endLabel.AutoSize = true;
        	this.endLabel.Location = new System.Drawing.Point(26, 110);
        	this.endLabel.Name = "endLabel";
        	this.endLabel.Size = new System.Drawing.Size(65, 13);
        	this.endLabel.TabIndex = 5;
        	this.endLabel.Text = "End Station:";
        	// 
        	// mrtPanel
        	// 
        	this.mrtPanel.AccessibleName = "mrtPanel";
        	this.mrtPanel.BackgroundImage = global::MRTForm.Properties.Resources.mrtmap;
        	this.mrtPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        	this.mrtPanel.Location = new System.Drawing.Point(415, 12);
        	this.mrtPanel.Name = "mrtPanel";
        	this.mrtPanel.Size = new System.Drawing.Size(752, 678);
        	this.mrtPanel.TabIndex = 7;
        	// 
        	// txtResult
        	// 
        	this.txtResult.Location = new System.Drawing.Point(26, 202);
        	this.txtResult.Multiline = true;
        	this.txtResult.Name = "txtResult";
        	this.txtResult.ReadOnly = true;
        	this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        	this.txtResult.Size = new System.Drawing.Size(383, 475);
        	this.txtResult.TabIndex = 8;
        	// 
        	// btnClear
        	// 
        	this.btnClear.Location = new System.Drawing.Point(334, 173);
        	this.btnClear.Name = "btnClear";
        	this.btnClear.Size = new System.Drawing.Size(75, 23);
        	this.btnClear.TabIndex = 9;
        	this.btnClear.Text = "Clear";
        	this.btnClear.UseVisualStyleBackColor = true;
        	this.btnClear.Click += new System.EventHandler(this.Button1Click);
        	// 
        	// Form1
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.AutoSize = true;
        	this.ClientSize = new System.Drawing.Size(1168, 702);
        	this.Controls.Add(this.btnClear);
        	this.Controls.Add(this.txtResult);
        	this.Controls.Add(this.mrtPanel);
        	this.Controls.Add(this.endLabel);
        	this.Controls.Add(this.startLabel);
        	this.Controls.Add(this.computeBtn);
        	this.Controls.Add(this.endStation);
        	this.Controls.Add(this.startStation);
        	this.Location = new System.Drawing.Point(400, 0);
        	this.Name = "Form1";
        	this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        	this.Text = "MRT Routes";
        	this.ResumeLayout(false);
        	this.PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.ComboBox startStation;
        private System.Windows.Forms.ComboBox endStation;
        private System.Windows.Forms.Button computeBtn;
        private System.Windows.Forms.Label startLabel;
        private System.Windows.Forms.Label endLabel;
        private System.Windows.Forms.Panel mrtPanel;
        
    }
}

