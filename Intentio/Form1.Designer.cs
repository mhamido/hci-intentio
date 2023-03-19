namespace Intentio
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
            DisplayDevicesBox = new System.Windows.Forms.ListBox();
            ScanButton = new System.Windows.Forms.Button();
            Selected = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // DisplayDevicesBox
            // 
            DisplayDevicesBox.FormattingEnabled = true;
            DisplayDevicesBox.ItemHeight = 32;
            DisplayDevicesBox.Location = new System.Drawing.Point(673, 722);
            DisplayDevicesBox.Name = "DisplayDevicesBox";
            DisplayDevicesBox.Size = new System.Drawing.Size(477, 228);
            DisplayDevicesBox.TabIndex = 1;
            // 
            // ScanButton
            // 
            ScanButton.Location = new System.Drawing.Point(749, 956);
            ScanButton.Name = "ScanButton";
            ScanButton.Size = new System.Drawing.Size(150, 46);
            ScanButton.TabIndex = 2;
            ScanButton.Text = "Scan";
            ScanButton.UseVisualStyleBackColor = true;
            ScanButton.Click += ScanButton_Click;
            // 
            // Selected
            // 
            Selected.Location = new System.Drawing.Point(905, 956);
            Selected.Name = "Selected";
            Selected.Size = new System.Drawing.Size(150, 46);
            Selected.TabIndex = 3;
            Selected.Text = "Select";
            Selected.UseVisualStyleBackColor = true;
            Selected.Click += Selected_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.AppCode;
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            ClientSize = new System.Drawing.Size(1733, 1108);
            Controls.Add(Selected);
            Controls.Add(ScanButton);
            Controls.Add(DisplayDevicesBox);
            DoubleBuffered = true;
            Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListBox DisplayDevicesBox;
        private System.Windows.Forms.Button ScanButton;
        private System.Windows.Forms.Button Selected;
    }
}

