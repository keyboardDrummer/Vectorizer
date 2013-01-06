using System.Drawing;

namespace Vectorizer.UI
{
    partial class VectorizerForm
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
            this.consoleTextBox = new System.Windows.Forms.RichTextBox();
            this.clearConsoleButton = new System.Windows.Forms.Button();
            this.redrawButton = new System.Windows.Forms.Button();
            this.updateLastSwitch = new Generic.Switch();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Difference = new Vectorizer.UI.CanvasBox();
            this.Target = new Vectorizer.UI.CanvasBox();
            this.HighResBest = new Vectorizer.UI.CanvasBox();
            this.LowResBest = new Vectorizer.UI.CanvasBox();
            this.LowResLast = new Vectorizer.UI.CanvasBox();
            this.nextPerSecond = new Vectorizer.UI.NextPerSecond();
            this.myController = new Vectorizer.UI.Controller();
            this.HighResLast = new Vectorizer.UI.CanvasBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // consoleTextBox
            // 
            this.consoleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.consoleTextBox.Location = new System.Drawing.Point(12, 41);
            this.consoleTextBox.Name = "consoleTextBox";
            this.consoleTextBox.Size = new System.Drawing.Size(230, 654);
            this.consoleTextBox.TabIndex = 6;
            this.consoleTextBox.Text = "";
            // 
            // clearConsoleButton
            // 
            this.clearConsoleButton.Location = new System.Drawing.Point(12, 12);
            this.clearConsoleButton.Name = "clearConsoleButton";
            this.clearConsoleButton.Size = new System.Drawing.Size(109, 23);
            this.clearConsoleButton.TabIndex = 11;
            this.clearConsoleButton.Text = "Clear console";
            this.clearConsoleButton.UseVisualStyleBackColor = true;
            this.clearConsoleButton.Click += new System.EventHandler(this.ClearConsoleButtonClick);
            // 
            // redrawButton
            // 
            this.redrawButton.Location = new System.Drawing.Point(1031, 12);
            this.redrawButton.Name = "redrawButton";
            this.redrawButton.Size = new System.Drawing.Size(75, 23);
            this.redrawButton.TabIndex = 20;
            this.redrawButton.Text = "Redraw";
            this.redrawButton.UseVisualStyleBackColor = true;
            this.redrawButton.Click += new System.EventHandler(this.RedrawButtonClick);
            // 
            // updateLastSwitch
            // 
            this.updateLastSwitch.IsOn = false;
            this.updateLastSwitch.Location = new System.Drawing.Point(1186, 12);
            this.updateLastSwitch.Name = "updateLastSwitch";
            this.updateLastSwitch.OffLabel = "No";
            this.updateLastSwitch.OnLabel = "Yes";
            this.updateLastSwitch.Size = new System.Drawing.Size(75, 24);
            this.updateLastSwitch.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1112, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Update Last:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.Difference, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Target, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.HighResBest, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.LowResBest, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.HighResLast, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.LowResLast, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(248, 42);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1163, 653);
            this.tableLayoutPanel1.TabIndex = 29;
            // 
            // Difference
            // 
            this.Difference.AutoSize = true;
            this.Difference.BackColor = System.Drawing.Color.Silver;
            this.Difference.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Difference.LabelText = "Difference";
            this.Difference.Location = new System.Drawing.Point(3, 329);
            this.Difference.Name = "Difference";
            this.Difference.PaintMethod = null;
            this.Difference.Size = new System.Drawing.Size(381, 321);
            this.Difference.TabIndex = 26;
            this.Difference.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // Target
            // 
            this.Target.AutoSize = true;
            this.Target.BackColor = System.Drawing.Color.Silver;
            this.Target.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Target.LabelText = "Target";
            this.Target.Location = new System.Drawing.Point(3, 3);
            this.Target.Name = "Target";
            this.Target.PaintMethod = null;
            this.Target.Size = new System.Drawing.Size(381, 320);
            this.Target.TabIndex = 21;
            this.Target.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // HighResBest
            // 
            this.HighResBest.BackColor = System.Drawing.Color.Silver;
            this.HighResBest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HighResBest.LabelText = "High Res Best Solution";
            this.HighResBest.Location = new System.Drawing.Point(390, 3);
            this.HighResBest.Name = "HighResBest";
            this.HighResBest.PaintMethod = null;
            this.HighResBest.Size = new System.Drawing.Size(381, 320);
            this.HighResBest.TabIndex = 22;
            this.HighResBest.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // LowResBest
            // 
            this.LowResBest.BackColor = System.Drawing.Color.Silver;
            this.LowResBest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LowResBest.LabelText = "Low Res Best Solution";
            this.LowResBest.Location = new System.Drawing.Point(777, 3);
            this.LowResBest.Name = "LowResBest";
            this.LowResBest.PaintMethod = null;
            this.LowResBest.Size = new System.Drawing.Size(383, 320);
            this.LowResBest.TabIndex = 23;
            this.LowResBest.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // LowResLast
            // 
            this.LowResLast.BackColor = System.Drawing.Color.Silver;
            this.LowResLast.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LowResLast.LabelText = "Low Res Last";
            this.LowResLast.Location = new System.Drawing.Point(777, 329);
            this.LowResLast.Name = "LowResLast";
            this.LowResLast.PaintMethod = null;
            this.LowResLast.Size = new System.Drawing.Size(383, 321);
            this.LowResLast.TabIndex = 25;
            this.LowResLast.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // nextPerSecond
            // 
            this.nextPerSecond.BackColor = System.Drawing.Color.Silver;
            this.nextPerSecond.Location = new System.Drawing.Point(1267, 12);
            this.nextPerSecond.MyController = this.myController;
            this.nextPerSecond.Name = "nextPerSecond";
            this.nextPerSecond.Size = new System.Drawing.Size(83, 22);
            this.nextPerSecond.TabIndex = 28;
            // 
            // myController
            // 
            this.myController.Location = new System.Drawing.Point(127, 12);
            this.myController.Name = "myController";
            this.myController.Size = new System.Drawing.Size(854, 24);
            this.myController.TabIndex = 16;
            this.myController.TargetImage = global::Vectorizer.Properties.Resources.Polygon;
            // 
            // HighResLast
            // 
            this.HighResLast.BackColor = System.Drawing.Color.Silver;
            this.HighResLast.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HighResLast.LabelText = "High Res Last";
            this.HighResLast.Location = new System.Drawing.Point(390, 329);
            this.HighResLast.Name = "HighResLast";
            this.HighResLast.PaintMethod = null;
            this.HighResLast.Size = new System.Drawing.Size(381, 321);
            this.HighResLast.TabIndex = 24;
            this.HighResLast.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // VectorizerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1423, 699);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.nextPerSecond);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.updateLastSwitch);
            this.Controls.Add(this.redrawButton);
            this.Controls.Add(this.myController);
            this.Controls.Add(this.clearConsoleButton);
            this.Controls.Add(this.consoleTextBox);
            this.Name = "VectorizerForm";
            this.Text = "Vectorizer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox consoleTextBox;
        private System.Windows.Forms.Button clearConsoleButton;
        private Controller myController;
        private System.Windows.Forms.Button redrawButton;
        private CanvasBox Target;
        private CanvasBox HighResBest;
        private CanvasBox LowResBest;
        private CanvasBox LowResLast;
        private Generic.Switch updateLastSwitch;
        private System.Windows.Forms.Label label1;
        private NextPerSecond nextPerSecond;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CanvasBox Difference;
        private CanvasBox HighResLast;
    }
}

