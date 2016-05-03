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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Zeitanzeige
            // 
            this.Zeitanzeige.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Zeitanzeige.Font = new System.Drawing.Font("Courier New", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Zeitanzeige.Location = new System.Drawing.Point(0, 749);
            this.Zeitanzeige.Name = "Zeitanzeige";
            this.Zeitanzeige.Size = new System.Drawing.Size(1280, 51);
            this.Zeitanzeige.TabIndex = 0;
            this.Zeitanzeige.Text = "00:00";
            this.Zeitanzeige.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MessageLabel
            // 
            this.MessageLabel.BackColor = System.Drawing.Color.LightGreen;
            this.MessageLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.MessageLabel.Font = new System.Drawing.Font("Courier New", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageLabel.Location = new System.Drawing.Point(0, 55);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(1280, 84);
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
            this.Codefeld.BackColor = System.Drawing.Color.White;
            this.Codefeld.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Codefeld.Dock = System.Windows.Forms.DockStyle.Top;
            this.Codefeld.Font = new System.Drawing.Font("Courier New", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Codefeld.Location = new System.Drawing.Point(0, 0);
            this.Codefeld.Name = "Codefeld";
            this.Codefeld.Size = new System.Drawing.Size(1280, 55);
            this.Codefeld.TabIndex = 2;
            this.Codefeld.Text = "000000";
            this.Codefeld.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Codefeld.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Codefeld_KeyDown);
            this.Codefeld.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Codefeld_KeyPress);
            // 
            // countdownbar
            // 
            this.countdownbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.countdownbar.Location = new System.Drawing.Point(0, 139);
            this.countdownbar.Name = "countdownbar";
            this.countdownbar.Size = new System.Drawing.Size(1280, 10);
            this.countdownbar.TabIndex = 3;
            // 
            // Anzeige
            // 
            this.Anzeige.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Anzeige.Font = new System.Drawing.Font("Courier New", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Anzeige.Location = new System.Drawing.Point(3, 3);
            this.Anzeige.Name = "Anzeige";
            this.Anzeige.Size = new System.Drawing.Size(1266, 595);
            this.Anzeige.TabIndex = 4;
            this.Anzeige.Text = "Anzeige";
            this.Anzeige.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Datumsanzeige
            // 
            this.Datumsanzeige.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Datumsanzeige.Font = new System.Drawing.Font("Courier New", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Datumsanzeige.Location = new System.Drawing.Point(1104, 768);
            this.Datumsanzeige.Name = "Datumsanzeige";
            this.Datumsanzeige.Size = new System.Drawing.Size(176, 32);
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
            this.Stempelliste.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Stempelliste.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Stempelliste.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Stempelliste.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5});
            this.Stempelliste.Location = new System.Drawing.Point(0, 0);
            this.Stempelliste.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.Stempelliste.Name = "Stempelliste";
            this.Stempelliste.Size = new System.Drawing.Size(806, 520);
            this.Stempelliste.SmallImageList = this.Stempelicons;
            this.Stempelliste.TabIndex = 5;
            this.Stempelliste.UseCompatibleStateImageBehavior = false;
            this.Stempelliste.View = System.Windows.Forms.View.SmallIcon;
            // 
            // Detailanzeige
            // 
            this.Detailanzeige.BackColor = System.Drawing.SystemColors.Window;
            this.Detailanzeige.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Detailanzeige.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Detailanzeige.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Detailanzeige.Location = new System.Drawing.Point(0, 0);
            this.Detailanzeige.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.Detailanzeige.Name = "Detailanzeige";
            this.Detailanzeige.Size = new System.Drawing.Size(456, 585);
            this.Detailanzeige.TabIndex = 6;
            this.Detailanzeige.Text = "Stundenkonto\r\n00.00\r\n\r\nStand der Stundenberechnung\r\n01.01.2016\r\n\r\nResturlaub bis " +
    "Jahresende\r\n00.00\r\n\r\nBereits geplante Urlaubstage\r\n00.00";
            this.Detailanzeige.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            this.tabControl1.Location = new System.Drawing.Point(0, 149);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1280, 600);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Anzeige);
            this.tabPage1.Location = new System.Drawing.Point(4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1272, 601);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 5);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1272, 591);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Detailanzeige);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(1266, 585);
            this.splitContainer1.SplitterDistance = 456;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.Stempelliste);
            this.splitContainer2.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer2.Size = new System.Drawing.Size(806, 585);
            this.splitContainer2.SplitterDistance = 61;
            this.splitContainer2.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Courier New", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(806, 61);
            this.label1.TabIndex = 0;
            this.label1.Text = "Heutige Stempelungen";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 800);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.Datumsanzeige);
            this.Controls.Add(this.Zeitanzeige);
            this.Controls.Add(this.countdownbar);
            this.Controls.Add(this.MessageLabel);
            this.Controls.Add(this.Codefeld);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
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
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label1;
    }
}

