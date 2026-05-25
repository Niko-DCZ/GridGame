namespace GridGame
{
    partial class Game
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
            components = new System.ComponentModel.Container();
            mapPanel = new Panel();
            whichPlayerWins = new Label();
            saveFileDialog1 = new SaveFileDialog();
            timer1 = new System.Windows.Forms.Timer(components);
            InfoBox = new ListBox();
            SelectedPic = new PictureBox();
            DebugBox = new ListBox();
            lstCharacters = new ListBox();
            lstCharactersConfirmed = new ListBox();
            txtCoord = new TextBox();
            confirmLocationButton = new Button();
            setupGameButton = new Button();
            panelForWinning = new Panel();
            ((System.ComponentModel.ISupportInitialize)SelectedPic).BeginInit();
            panelForWinning.SuspendLayout();
            SuspendLayout();
            // 
            // mapPanel
            // 
            mapPanel.Location = new Point(9, 3);
            mapPanel.Name = "mapPanel";
            mapPanel.Size = new Size(600, 600);
            mapPanel.TabIndex = 0;
            // 
            // whichPlayerWins
            // 
            whichPlayerWins.AutoSize = true;
            whichPlayerWins.Font = new Font("Segoe UI", 50F);
            whichPlayerWins.Location = new Point(54, 167);
            whichPlayerWins.Name = "whichPlayerWins";
            whichPlayerWins.Size = new Size(568, 89);
            whichPlayerWins.TabIndex = 0;
            whichPlayerWins.Text = "which Player Wins";
            whichPlayerWins.Visible = false;
            // 
            // InfoBox
            // 
            InfoBox.FormattingEnabled = true;
            InfoBox.ItemHeight = 15;
            InfoBox.Location = new Point(618, 204);
            InfoBox.Name = "InfoBox";
            InfoBox.Size = new Size(223, 199);
            InfoBox.TabIndex = 1;
            // 
            // SelectedPic
            // 
            SelectedPic.Location = new Point(618, 3);
            SelectedPic.Name = "SelectedPic";
            SelectedPic.Size = new Size(223, 195);
            SelectedPic.SizeMode = PictureBoxSizeMode.Zoom;
            SelectedPic.TabIndex = 2;
            SelectedPic.TabStop = false;
            // 
            // DebugBox
            // 
            DebugBox.FormattingEnabled = true;
            DebugBox.ItemHeight = 15;
            DebugBox.Location = new Point(618, 409);
            DebugBox.Name = "DebugBox";
            DebugBox.Size = new Size(223, 214);
            DebugBox.TabIndex = 3;
            // 
            // lstCharacters
            // 
            lstCharacters.FormattingEnabled = true;
            lstCharacters.ItemHeight = 15;
            lstCharacters.Location = new Point(847, 12);
            lstCharacters.Name = "lstCharacters";
            lstCharacters.Size = new Size(174, 199);
            lstCharacters.TabIndex = 4;
            // 
            // lstCharactersConfirmed
            // 
            lstCharactersConfirmed.FormattingEnabled = true;
            lstCharactersConfirmed.ItemHeight = 15;
            lstCharactersConfirmed.Location = new Point(1027, 12);
            lstCharactersConfirmed.Name = "lstCharactersConfirmed";
            lstCharactersConfirmed.Size = new Size(192, 199);
            lstCharactersConfirmed.TabIndex = 5;
            // 
            // txtCoord
            // 
            txtCoord.Location = new Point(847, 218);
            txtCoord.Name = "txtCoord";
            txtCoord.Size = new Size(174, 23);
            txtCoord.TabIndex = 6;
            // 
            // confirmLocationButton
            // 
            confirmLocationButton.Location = new Point(1027, 217);
            confirmLocationButton.Name = "confirmLocationButton";
            confirmLocationButton.Size = new Size(83, 23);
            confirmLocationButton.TabIndex = 7;
            confirmLocationButton.Text = "Select position";
            confirmLocationButton.UseVisualStyleBackColor = true;
            confirmLocationButton.Click += ConfirmSelection;
            // 
            // setupGameButton
            // 
            setupGameButton.Location = new Point(968, 250);
            setupGameButton.Name = "setupGameButton";
            setupGameButton.Size = new Size(126, 92);
            setupGameButton.TabIndex = 8;
            setupGameButton.Text = "Setup Game";
            setupGameButton.UseVisualStyleBackColor = true;
            setupGameButton.Click += setupGameButton_Click;
            // 
            // panelForWinning
            // 
            panelForWinning.Controls.Add(whichPlayerWins);
            panelForWinning.Location = new Point(5, 3);
            panelForWinning.Margin = new Padding(2);
            panelForWinning.Name = "panelForWinning";
            panelForWinning.Size = new Size(602, 609);
            panelForWinning.TabIndex = 1;
            panelForWinning.Visible = false;
            // 
            // Game
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1231, 623);
            Controls.Add(panelForWinning);
            Controls.Add(setupGameButton);
            Controls.Add(confirmLocationButton);
            Controls.Add(txtCoord);
            Controls.Add(lstCharactersConfirmed);
            Controls.Add(lstCharacters);
            Controls.Add(DebugBox);
            Controls.Add(SelectedPic);
            Controls.Add(InfoBox);
            Controls.Add(mapPanel);
            Name = "Game";
            Text = "Game";
            ((System.ComponentModel.ISupportInitialize)SelectedPic).EndInit();
            panelForWinning.ResumeLayout(false);
            panelForWinning.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel mapPanel;
        private SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Timer timer1;
        private ListBox InfoBox;
        private PictureBox SelectedPic;
        private ListBox DebugBox;
        private ListBox lstCharacters;
        private ListBox lstCharactersConfirmed;
        private TextBox txtCoord;
        private Button confirmLocationButton;
        private Button setupGameButton;
        private Label whichPlayerWins;
        private Panel panelForWinning;
    }
}