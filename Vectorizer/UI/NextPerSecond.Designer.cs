namespace Vectorizer.UI
{
    partial class NextPerSecond
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.NextPerSecondLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // NextPerSecondLabel
            // 
            this.NextPerSecondLabel.AutoSize = true;
            this.NextPerSecondLabel.Location = new System.Drawing.Point(-3, 5);
            this.NextPerSecondLabel.Name = "NextPerSecondLabel";
            this.NextPerSecondLabel.Size = new System.Drawing.Size(0, 13);
            this.NextPerSecondLabel.TabIndex = 0;
            // 
            // NextPerSecond
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.NextPerSecondLabel);
            this.Name = "NextPerSecond";
            this.Size = new System.Drawing.Size(83, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NextPerSecondLabel;
    }
}
