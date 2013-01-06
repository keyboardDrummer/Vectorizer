namespace Vectorizer.UI
{
    partial class Controller
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
            this.saveButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.untilBetter = new System.Windows.Forms.Button();
            this.nextPicture = new System.Windows.Forms.Button();
            this.loadResults = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.loadPictureButton = new System.Windows.Forms.Button();
            this.openPictureDialog = new System.Windows.Forms.OpenFileDialog();
            this.clearResults = new System.Windows.Forms.Button();
            this.algorithmSelectMenu = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(418, 0);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 30;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButtonClick);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(337, 0);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 29;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButtonClick);
            // 
            // untilBetter
            // 
            this.untilBetter.Location = new System.Drawing.Point(256, 0);
            this.untilBetter.Name = "untilBetter";
            this.untilBetter.Size = new System.Drawing.Size(75, 23);
            this.untilBetter.TabIndex = 28;
            this.untilBetter.Text = "Improve";
            this.untilBetter.UseVisualStyleBackColor = true;
            this.untilBetter.Click += new System.EventHandler(this.UntilBetterClick);
            // 
            // nextPicture
            // 
            this.nextPicture.Location = new System.Drawing.Point(175, 0);
            this.nextPicture.Name = "nextPicture";
            this.nextPicture.Size = new System.Drawing.Size(75, 23);
            this.nextPicture.TabIndex = 27;
            this.nextPicture.Text = "Next";
            this.nextPicture.UseVisualStyleBackColor = true;
            this.nextPicture.Click += new System.EventHandler(this.NextPictureClick);
            // 
            // loadResults
            // 
            this.loadResults.Location = new System.Drawing.Point(81, 0);
            this.loadResults.Name = "loadResults";
            this.loadResults.Size = new System.Drawing.Size(88, 23);
            this.loadResults.TabIndex = 26;
            this.loadResults.Text = "Load Previous";
            this.loadResults.UseVisualStyleBackColor = true;
            this.loadResults.Click += new System.EventHandler(this.LoadResultsClick);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(580, 0);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 25;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.StopButtonClick);
            // 
            // loadPictureButton
            // 
            this.loadPictureButton.Location = new System.Drawing.Point(0, 0);
            this.loadPictureButton.Name = "loadPictureButton";
            this.loadPictureButton.Size = new System.Drawing.Size(75, 23);
            this.loadPictureButton.TabIndex = 23;
            this.loadPictureButton.Text = "Load Picture";
            this.loadPictureButton.UseVisualStyleBackColor = true;
            this.loadPictureButton.Click += new System.EventHandler(this.LoadPictureButtonClick);
            // 
            // openPictureDialog
            // 
            this.openPictureDialog.Filter = "JPEG files|*.jpg|Bmp files|*.bmp|Png files|*.png";
            this.openPictureDialog.FilterIndex = 3;
            this.openPictureDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenPictureDialogFileOk);
            // 
            // clearResults
            // 
            this.clearResults.Location = new System.Drawing.Point(499, 0);
            this.clearResults.Name = "clearResults";
            this.clearResults.Size = new System.Drawing.Size(75, 23);
            this.clearResults.TabIndex = 31;
            this.clearResults.Text = "Clear";
            this.clearResults.UseVisualStyleBackColor = true;
            this.clearResults.Click += new System.EventHandler(this.ClearResultsClick);
            // 
            // algorithmSelectMenu
            // 
            this.algorithmSelectMenu.FormattingEnabled = true;
            this.algorithmSelectMenu.Location = new System.Drawing.Point(661, 2);
            this.algorithmSelectMenu.Name = "algorithmSelectMenu";
            this.algorithmSelectMenu.Size = new System.Drawing.Size(121, 21);
            this.algorithmSelectMenu.TabIndex = 32;
            this.algorithmSelectMenu.SelectedIndexChanged += new System.EventHandler(this.AlgorithmSelectMenuSelectedIndexChanged);
            // 
            // Controller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.algorithmSelectMenu);
            this.Controls.Add(this.clearResults);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.untilBetter);
            this.Controls.Add(this.nextPicture);
            this.Controls.Add(this.loadResults);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.loadPictureButton);
            this.Name = "Controller";
            this.Size = new System.Drawing.Size(793, 24);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button untilBetter;
        private System.Windows.Forms.Button nextPicture;
        private System.Windows.Forms.Button loadResults;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button loadPictureButton;
        private System.Windows.Forms.OpenFileDialog openPictureDialog;
        private System.Windows.Forms.Button clearResults;
        private System.Windows.Forms.ComboBox algorithmSelectMenu;
    }
}
