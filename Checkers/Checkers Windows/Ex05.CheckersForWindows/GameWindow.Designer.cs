namespace Ex05.CheckersForWindows
{
    public partial class GameWindow
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
            this.labelPlayerOne = new System.Windows.Forms.Label();
            this.labelPlayerTwo = new System.Windows.Forms.Label();
            this.labelCurrentPlayerSign = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelPlayerOne
            // 
            this.labelPlayerOne.AutoSize = true;
            this.labelPlayerOne.Location = new System.Drawing.Point(0, 0);
            this.labelPlayerOne.Name = "labelPlayerOne";
            this.labelPlayerOne.Size = new System.Drawing.Size(57, 13);
            this.labelPlayerOne.TabIndex = 0;
            this.labelPlayerOne.Text = "Player 1: 0";
            // 
            // labelPlayerTwo
            // 
            this.labelPlayerTwo.AutoSize = true;
            this.labelPlayerTwo.Location = new System.Drawing.Point(0, 13);
            this.labelPlayerTwo.Name = "labelPlayerTwo";
            this.labelPlayerTwo.Size = new System.Drawing.Size(57, 13);
            this.labelPlayerTwo.TabIndex = 1;
            this.labelPlayerTwo.Text = "Player 2: 0";
            // 
            // labelCurrentPlayerSign
            // 
            this.labelCurrentPlayerSign.AutoSize = true;
            this.labelCurrentPlayerSign.Location = new System.Drawing.Point(64, 0);
            this.labelCurrentPlayerSign.Name = "labelCurrentPlayerSign";
            this.labelCurrentPlayerSign.Size = new System.Drawing.Size(0, 13);
            this.labelCurrentPlayerSign.TabIndex = 2;
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 497);
            this.Controls.Add(this.labelCurrentPlayerSign);
            this.Controls.Add(this.labelPlayerTwo);
            this.Controls.Add(this.labelPlayerOne);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPlayerOne;
        private System.Windows.Forms.Label labelPlayerTwo;
        private System.Windows.Forms.Label labelCurrentPlayerSign;
    }
}