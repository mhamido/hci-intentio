namespace Intentio
{
    partial class ParentDashBoard
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimesDistracted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeToComplete = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LettersMistaken = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumbersMistaken = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalMistakes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Number,
            this.Name,
            this.TimesDistracted,
            this.TimeToComplete,
            this.LettersMistaken,
            this.NumbersMistaken,
            this.TotalMistakes});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(1005, 542);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Number
            // 
            this.Number.HeaderText = "Session Number";
            this.Number.Name = "Number";
            this.Number.ReadOnly = true;
            // 
            // Name
            // 
            this.Name.HeaderText = "ParentDashBoard";
            this.Name.Name = "Name";
            this.Name.ReadOnly = true;
            // 
            // TimesDistracted
            // 
            this.TimesDistracted.HeaderText = "TimesDistracted";
            this.TimesDistracted.Name = "TimesDistracted";
            this.TimesDistracted.ReadOnly = true;
            // 
            // TimeToComplete
            // 
            this.TimeToComplete.HeaderText = "TimeToComplete";
            this.TimeToComplete.Name = "TimeToComplete";
            this.TimeToComplete.ReadOnly = true;
            // 
            // LettersMistaken
            // 
            this.LettersMistaken.HeaderText = "LettersMistaken";
            this.LettersMistaken.Name = "LettersMistaken";
            this.LettersMistaken.ReadOnly = true;
            // 
            // NumbersMistaken
            // 
            this.NumbersMistaken.HeaderText = "NumbersMistaken";
            this.NumbersMistaken.Name = "NumbersMistaken";
            this.NumbersMistaken.ReadOnly = true;
            // 
            // TotalMistakes
            // 
            this.TotalMistakes.HeaderText = "TotalMistakes";
            this.TotalMistakes.Name = "TotalMistakes";
            this.TotalMistakes.ReadOnly = true;
            // 
            // ParentDashBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 584);
            this.Controls.Add(this.dataGridView1);
            this.Name.Name = "ParentDashBoard";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimesDistracted;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeToComplete;
        private System.Windows.Forms.DataGridViewTextBoxColumn LettersMistaken;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumbersMistaken;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalMistakes;
    }
}