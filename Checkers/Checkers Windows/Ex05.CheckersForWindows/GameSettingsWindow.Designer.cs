namespace Ex05.CheckersForWindows
{
    public partial class GameSettingsWindow
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
            this.labelBoardSize = new System.Windows.Forms.Label();
            this.radioSixBySix = new System.Windows.Forms.RadioButton();
            this.radioEightByEight = new System.Windows.Forms.RadioButton();
            this.radioTenByTen = new System.Windows.Forms.RadioButton();
            this.labelPlayers = new System.Windows.Forms.Label();
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.checkBoxSecondPlayer = new System.Windows.Forms.CheckBox();
            this.textBoxPlayer1 = new System.Windows.Forms.TextBox();
            this.textBoxPlayer2 = new System.Windows.Forms.TextBox();
            this.MyButtonDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelBoardSize
            // 
            this.labelBoardSize.AutoSize = true;
            this.labelBoardSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelBoardSize.Location = new System.Drawing.Point(15, 15);
            this.labelBoardSize.Name = "labelBoardSize";
            this.labelBoardSize.Size = new System.Drawing.Size(77, 16);
            this.labelBoardSize.TabIndex = 0;
            this.labelBoardSize.Text = "Board Size:";
            // 
            // radioSixBySix
            // 
            this.radioSixBySix.AutoSize = true;
            this.radioSixBySix.Checked = true;
            this.radioSixBySix.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.radioSixBySix.Location = new System.Drawing.Point(37, 37);
            this.radioSixBySix.Name = "radioSixBySix";
            this.radioSixBySix.Size = new System.Drawing.Size(52, 20);
            this.radioSixBySix.TabIndex = 1;
            this.radioSixBySix.TabStop = true;
            this.radioSixBySix.Text = "6 x 6";
            this.radioSixBySix.UseVisualStyleBackColor = true;
            // 
            // radioEightByEight
            // 
            this.radioEightByEight.AutoSize = true;
            this.radioEightByEight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.radioEightByEight.Location = new System.Drawing.Point(97, 37);
            this.radioEightByEight.Name = "radioEightByEight";
            this.radioEightByEight.Size = new System.Drawing.Size(52, 20);
            this.radioEightByEight.TabIndex = 2;
            this.radioEightByEight.Text = "8 x 8";
            this.radioEightByEight.UseVisualStyleBackColor = true;
            this.radioEightByEight.CheckedChanged += new System.EventHandler(this.radioEightByEight_CheckedChanged);
            // 
            // radioTenByTen
            // 
            this.radioTenByTen.AutoSize = true;
            this.radioTenByTen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.radioTenByTen.Location = new System.Drawing.Point(155, 37);
            this.radioTenByTen.Name = "radioTenByTen";
            this.radioTenByTen.Size = new System.Drawing.Size(66, 20);
            this.radioTenByTen.TabIndex = 3;
            this.radioTenByTen.Text = "10 x 10";
            this.radioTenByTen.UseVisualStyleBackColor = true;
            this.radioTenByTen.CheckedChanged += new System.EventHandler(this.radioTenByTen_CheckedChanged);
            // 
            // labelPlayers
            // 
            this.labelPlayers.AutoSize = true;
            this.labelPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelPlayers.Location = new System.Drawing.Point(15, 60);
            this.labelPlayers.Name = "labelPlayers";
            this.labelPlayers.Size = new System.Drawing.Size(57, 16);
            this.labelPlayers.TabIndex = 4;
            this.labelPlayers.Text = "Players:";
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.AutoSize = true;
            this.labelPlayer1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelPlayer1.Location = new System.Drawing.Point(33, 94);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(67, 16);
            this.labelPlayer1.TabIndex = 5;
            this.labelPlayer1.Text = "Players 1:";
            // 
            // checkBoxSecondPlayer
            // 
            this.checkBoxSecondPlayer.AutoSize = true;
            this.checkBoxSecondPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.checkBoxSecondPlayer.Location = new System.Drawing.Point(36, 131);
            this.checkBoxSecondPlayer.Name = "checkBoxSecondPlayer";
            this.checkBoxSecondPlayer.Size = new System.Drawing.Size(79, 20);
            this.checkBoxSecondPlayer.TabIndex = 6;
            this.checkBoxSecondPlayer.Text = "Player 2:";
            this.checkBoxSecondPlayer.UseVisualStyleBackColor = true;
            this.checkBoxSecondPlayer.CheckStateChanged += new System.EventHandler(this.checkBoxSecondPlayer_CheckStateChanged);
            // 
            // textBoxPlayer1
            // 
            this.textBoxPlayer1.Location = new System.Drawing.Point(121, 93);
            this.textBoxPlayer1.Name = "textBoxPlayer1";
            this.textBoxPlayer1.Size = new System.Drawing.Size(100, 20);
            this.textBoxPlayer1.TabIndex = 7;
            this.textBoxPlayer1.TextChanged += new System.EventHandler(this.textBoxPlayer1_TextChanged);
            // 
            // textBoxPlayer2
            // 
            this.textBoxPlayer2.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxPlayer2.Enabled = false;
            this.textBoxPlayer2.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.textBoxPlayer2.Location = new System.Drawing.Point(121, 131);
            this.textBoxPlayer2.Name = "textBoxPlayer2";
            this.textBoxPlayer2.Size = new System.Drawing.Size(100, 20);
            this.textBoxPlayer2.TabIndex = 8;
            this.textBoxPlayer2.Text = "[Computer]";
            this.textBoxPlayer2.TextChanged += new System.EventHandler(this.textBoxPlayer2_TextChanged);
            // 
            // MyButtonDone
            // 
            this.MyButtonDone.Location = new System.Drawing.Point(146, 167);
            this.MyButtonDone.Name = "MyButtonDone";
            this.MyButtonDone.Size = new System.Drawing.Size(75, 23);
            this.MyButtonDone.TabIndex = 9;
            this.MyButtonDone.Text = "Done";
            this.MyButtonDone.UseVisualStyleBackColor = true;
            this.MyButtonDone.Click += new System.EventHandler(this.MyButtonDone_Click);
            // 
            // GameSettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(236, 209);
            this.Controls.Add(this.MyButtonDone);
            this.Controls.Add(this.textBoxPlayer2);
            this.Controls.Add(this.textBoxPlayer1);
            this.Controls.Add(this.checkBoxSecondPlayer);
            this.Controls.Add(this.labelPlayer1);
            this.Controls.Add(this.labelPlayers);
            this.Controls.Add(this.radioTenByTen);
            this.Controls.Add(this.radioEightByEight);
            this.Controls.Add(this.radioSixBySix);
            this.Controls.Add(this.labelBoardSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "GameSettingsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelBoardSize;
        private System.Windows.Forms.RadioButton radioSixBySix;
        private System.Windows.Forms.RadioButton radioEightByEight;
        private System.Windows.Forms.RadioButton radioTenByTen;
        private System.Windows.Forms.Label labelPlayers;
        private System.Windows.Forms.Label labelPlayer1;
        private System.Windows.Forms.CheckBox checkBoxSecondPlayer;
        private System.Windows.Forms.TextBox textBoxPlayer1;
        private System.Windows.Forms.TextBox textBoxPlayer2;
        private System.Windows.Forms.Button MyButtonDone;
    }
}