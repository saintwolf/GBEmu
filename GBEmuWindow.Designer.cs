namespace GBEmu
{
    partial class GBEmuWindow
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
            this.display = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStep = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnNothing = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtStepN = new System.Windows.Forms.TextBox();
            this.btnStepN = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtStepN2 = new System.Windows.Forms.TextBox();
            this.btnStepN2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.display)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // display
            // 
            this.display.Location = new System.Drawing.Point(12, 12);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(160, 144);
            this.display.TabIndex = 0;
            this.display.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnNothing);
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.btnStep);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Location = new System.Drawing.Point(301, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(87, 144);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(6, 19);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStep
            // 
            this.btnStep.Location = new System.Drawing.Point(6, 48);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(75, 23);
            this.btnStep.TabIndex = 1;
            this.btnStep.Text = "Step";
            this.btnStep.UseVisualStyleBackColor = true;
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(6, 77);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnNothing
            // 
            this.btnNothing.Location = new System.Drawing.Point(6, 106);
            this.btnNothing.Name = "btnNothing";
            this.btnNothing.Size = new System.Drawing.Size(75, 23);
            this.btnNothing.TabIndex = 3;
            this.btnNothing.Text = "Nothing";
            this.btnNothing.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 163);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(375, 258);
            this.textBox1.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnStepN);
            this.groupBox2.Controls.Add(this.txtStepN);
            this.groupBox2.Location = new System.Drawing.Point(179, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(116, 70);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Step Until Address";
            // 
            // txtStepN
            // 
            this.txtStepN.Location = new System.Drawing.Point(7, 18);
            this.txtStepN.Name = "txtStepN";
            this.txtStepN.Size = new System.Drawing.Size(100, 20);
            this.txtStepN.TabIndex = 0;
            // 
            // btnStepN
            // 
            this.btnStepN.Location = new System.Drawing.Point(19, 43);
            this.btnStepN.Name = "btnStepN";
            this.btnStepN.Size = new System.Drawing.Size(75, 23);
            this.btnStepN.TabIndex = 1;
            this.btnStepN.Text = "Step";
            this.btnStepN.UseVisualStyleBackColor = true;
            this.btnStepN.Click += new System.EventHandler(this.btnStepN_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnStepN2);
            this.groupBox3.Controls.Add(this.txtStepN2);
            this.groupBox3.Location = new System.Drawing.Point(179, 85);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(116, 71);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Step N Times";
            // 
            // txtStepN2
            // 
            this.txtStepN2.Location = new System.Drawing.Point(6, 17);
            this.txtStepN2.Name = "txtStepN2";
            this.txtStepN2.Size = new System.Drawing.Size(100, 20);
            this.txtStepN2.TabIndex = 0;
            // 
            // btnStepN2
            // 
            this.btnStepN2.Location = new System.Drawing.Point(19, 43);
            this.btnStepN2.Name = "btnStepN2";
            this.btnStepN2.Size = new System.Drawing.Size(75, 23);
            this.btnStepN2.TabIndex = 1;
            this.btnStepN2.Text = "Step";
            this.btnStepN2.UseVisualStyleBackColor = true;
            this.btnStepN2.Click += new System.EventHandler(this.button1_Click);
            // 
            // GBEmuWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 433);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.display);
            this.Name = "GBEmuWindow";
            this.Text = "GBEmuWindow";
            this.Load += new System.EventHandler(this.GBEmuWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.display)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox display;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnNothing;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnStep;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnStepN;
        private System.Windows.Forms.TextBox txtStepN;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnStepN2;
        private System.Windows.Forms.TextBox txtStepN2;
    }
}