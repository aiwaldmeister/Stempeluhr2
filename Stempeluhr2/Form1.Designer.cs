namespace Stempeluhr2
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("123456 00:00", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("123456 00:00", 1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("123456 00:00", 0);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("123456 00:00", 1);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("123456 00:00", 2);
            this.Zeitanzeige = new System.Windows.Forms.Label();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.countdowntimer = new System.Windows.Forms.Timer(this.components);
            this.uhrtimer = new System.Windows.Forms.Timer(this.components);
            this.Codefeld = new System.Windows.Forms.TextBox();
            this.countdownbar = new System.Windows.Forms.ProgressBar();
            this.Anzeige = new System.Windows.Forms.Label();
            this.Datumsanzeige = new System.Windows.Forms.Label();
            this.Stempelicons = new System.Windows.Forms.ImageList(this.components);
            this.Stempelliste = new System.Windows.Forms.ListView();
            this.Detailanzeige = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Zeitanzeige
            // 
            this.Zeitanzeige.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Zeitanzeige.Font = new System.Drawing.Font("Courier New", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Zeitanzeige.Location = new System.Drawing.Point(186, 351);
            this.Zeitanzeige.Name = "Zeitanzeige";
            this.Zeitanzeige.Size = new System.Drawing.Size(288, 51);
            this.Zeitanzeige.TabIndex = 0;
            this.Zeitanzeige.Text = "00:00";
            this.Zeitanzeige.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MessageLabel
            // 
            this.MessageLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.MessageLabel.Font = new System.Drawing.Font("Courier New", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageLabel.Location = new System.Drawing.Point(0, 40);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(653, 84);
            this.MessageLabel.TabIndex = 1;
            this.MessageLabel.Text = "MessageLabel";
            this.MessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // countdowntimer
            // 
            this.countdowntimer.Tick += new System.EventHandler(this.countdowntimer_Tick);
            // 
            // uhrtimer
            // 
            this.uhrtimer.Enabled = true;
            this.uhrtimer.Interval = 1000;
            this.uhrtimer.Tick += new System.EventHandler(this.uhrtimer_Tick);
            // 
            // Codefeld
            // 
            this.Codefeld.BackColor = System.Drawing.SystemColors.Control;
            this.Codefeld.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Codefeld.Dock = System.Windows.Forms.DockStyle.Top;
            this.Codefeld.Font = new System.Drawing.Font("Courier New", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Codefeld.Location = new System.Drawing.Point(0, 0);
            this.Codefeld.Name = "Codefeld";
            this.Codefeld.Size = new System.Drawing.Size(653, 40);
            this.Codefeld.TabIndex = 2;
            this.Codefeld.Text = "000000";
            this.Codefeld.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Codefeld.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Codefeld_KeyDown);
            this.Codefeld.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Codefeld_KeyPress);
            // 
            // countdownbar
            // 
            this.countdownbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.countdownbar.Location = new System.Drawing.Point(0, 124);
            this.countdownbar.Name = "countdownbar";
            this.countdownbar.Size = new System.Drawing.Size(653, 10);
            this.countdownbar.TabIndex = 3;
            // 
            // Anzeige
            // 
            this.Anzeige.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Anzeige.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Anzeige.Location = new System.Drawing.Point(0, 0);
            this.Anzeige.Name = "Anzeige";
            this.Anzeige.Size = new System.Drawing.Size(653, 402);
            this.Anzeige.TabIndex = 4;
            this.Anzeige.Text = "Anzeige";
            this.Anzeige.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Datumsanzeige
            // 
            this.Datumsanzeige.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Datumsanzeige.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Datumsanzeige.Location = new System.Drawing.Point(502, 378);
            this.Datumsanzeige.Name = "Datumsanzeige";
            this.Datumsanzeige.Size = new System.Drawing.Size(150, 24);
            this.Datumsanzeige.TabIndex = 0;
            this.Datumsanzeige.Text = "01.01.2000";
            this.Datumsanzeige.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Stempelicons
            // 
            this.Stempelicons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Stempelicons.ImageStream")));
            this.Stempelicons.TransparentColor = System.Drawing.Color.Transparent;
            this.Stempelicons.Images.SetKeyName(0, "einstempeln.gif");
            this.Stempelicons.Images.SetKeyName(1, "ausstempeln.gif");
            this.Stempelicons.Images.SetKeyName(2, "storniert.gif");
            // 
            // Stempelliste
            // 
            this.Stempelliste.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Stempelliste.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Stempelliste.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Stempelliste.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5});
            this.Stempelliste.Location = new System.Drawing.Point(288, 140);
            this.Stempelliste.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.Stempelliste.Name = "Stempelliste";
            this.Stempelliste.Size = new System.Drawing.Size(364, 262);
            this.Stempelliste.SmallImageList = this.Stempelicons;
            this.Stempelliste.TabIndex = 5;
            this.Stempelliste.UseCompatibleStateImageBehavior = false;
            this.Stempelliste.View = System.Windows.Forms.View.SmallIcon;
            // 
            // Detailanzeige
            // 
            this.Detailanzeige.BackColor = System.Drawing.SystemColors.Window;
            this.Detailanzeige.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Detailanzeige.Location = new System.Drawing.Point(0, 140);
            this.Detailanzeige.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.Detailanzeige.Name = "Detailanzeige";
            this.Detailanzeige.Size = new System.Drawing.Size(288, 262);
            this.Detailanzeige.TabIndex = 6;
            this.Detailanzeige.Text = "Stundenkonto\r\n00.00\r\n\r\nVerrechnete Zeit\r\n00.00\r\n\r\nRestlicher Urlaub\r\n00.00\r\n\r\nVer" +
    "planter Urlaub\r\n00.00";
            this.Detailanzeige.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 402);
            this.Controls.Add(this.Detailanzeige);
            this.Controls.Add(this.Stempelliste);
            this.Controls.Add(this.Datumsanzeige);
            this.Controls.Add(this.Zeitanzeige);
            this.Controls.Add(this.countdownbar);
            this.Controls.Add(this.MessageLabel);
            this.Controls.Add(this.Codefeld);
            this.Controls.Add(this.Anzeige);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Zeitanzeige;
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.Timer countdowntimer;
        private System.Windows.Forms.Timer uhrtimer;
        private System.Windows.Forms.TextBox Codefeld;
        private System.Windows.Forms.ProgressBar countdownbar;
        private System.Windows.Forms.Label Anzeige;
        private System.Windows.Forms.Label Datumsanzeige;
        private System.Windows.Forms.ImageList Stempelicons;
        private System.Windows.Forms.ListView Stempelliste;
        private System.Windows.Forms.Label Detailanzeige;
    }
}

