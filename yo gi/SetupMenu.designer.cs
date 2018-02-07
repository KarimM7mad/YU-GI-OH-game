namespace yo_gi
{
    partial class SetupMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupMenu));
            this.StartBtn = new System.Windows.Forms.Button();
            this.PortBar = new System.Windows.Forms.TextBox();
            this.HostBtn = new System.Windows.Forms.RadioButton();
            this.GBtn = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // StartBtn
            // 
            this.StartBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("StartBtn.BackgroundImage")));
            this.StartBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StartBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartBtn.Location = new System.Drawing.Point(182, 125);
            this.StartBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(101, 37);
            this.StartBtn.TabIndex = 2;
            this.StartBtn.Text = "Start";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // PortBar
            // 
            this.PortBar.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.PortBar.Location = new System.Drawing.Point(182, 76);
            this.PortBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PortBar.Name = "PortBar";
            this.PortBar.Size = new System.Drawing.Size(90, 20);
            this.PortBar.TabIndex = 4;
            this.PortBar.TextChanged += new System.EventHandler(this.PortBar_TextChanged);
            this.PortBar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PortNumber);
            // 
            // HostBtn
            // 
            this.HostBtn.AutoSize = true;
            this.HostBtn.BackColor = System.Drawing.Color.Transparent;
            this.HostBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HostBtn.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.HostBtn.Location = new System.Drawing.Point(25, 140);
            this.HostBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.HostBtn.Name = "HostBtn";
            this.HostBtn.Size = new System.Drawing.Size(51, 17);
            this.HostBtn.TabIndex = 6;
            this.HostBtn.TabStop = true;
            this.HostBtn.Text = "Host";
            this.HostBtn.UseVisualStyleBackColor = false;
            this.HostBtn.CheckedChanged += new System.EventHandler(this.HostBtn_CheckedChanged);
            // 
            // GBtn
            // 
            this.GBtn.AutoSize = true;
            this.GBtn.BackColor = System.Drawing.Color.Transparent;
            this.GBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBtn.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.GBtn.Location = new System.Drawing.Point(90, 140);
            this.GBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GBtn.Name = "GBtn";
            this.GBtn.Size = new System.Drawing.Size(58, 17);
            this.GBtn.TabIndex = 6;
            this.GBtn.TabStop = true;
            this.GBtn.Text = "Guest";
            this.GBtn.UseVisualStyleBackColor = false;
            this.GBtn.CheckedChanged += new System.EventHandler(this.GBtn_CheckedChanged);
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(17, 20);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 37);
            this.button1.TabIndex = 8;
            this.button1.Text = "IP Address";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(17, 67);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(127, 37);
            this.button2.TabIndex = 9;
            this.button2.Text = "Port Number";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(182, 37);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(101, 20);
            this.textBox1.TabIndex = 10;
            // 
            // SetupMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(331, 197);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.GBtn);
            this.Controls.Add(this.HostBtn);
            this.Controls.Add(this.PortBar);
            this.Controls.Add(this.StartBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.TextBox PortBar;
        private System.Windows.Forms.RadioButton HostBtn;
        private System.Windows.Forms.RadioButton GuestBtn;
        private System.Windows.Forms.RadioButton GBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
    }
}