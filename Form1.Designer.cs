namespace capTv
{
    partial class Form1
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
            this.videoPanel = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.bt_capture = new System.Windows.Forms.Button();
            this.bt_calculate = new System.Windows.Forms.Button();
            this.lb_x1 = new System.Windows.Forms.Label();
            this.lb_y1 = new System.Windows.Forms.Label();
            this.lb_y2 = new System.Windows.Forms.Label();
            this.lb_x2 = new System.Windows.Forms.Label();
            this.lb_ratio1 = new System.Windows.Forms.Label();
            this.lb_ratio2 = new System.Windows.Forms.Label();
            this.lb_angle2 = new System.Windows.Forms.Label();
            this.lb_angle1 = new System.Windows.Forms.Label();
            this.lb_predict2 = new System.Windows.Forms.Label();
            this.lb_predict1 = new System.Windows.Forms.Label();
            this.lb_X = new System.Windows.Forms.Label();
            this.lb_Y = new System.Windows.Forms.Label();
            this.lb_Z = new System.Windows.Forms.Label();
            this.bt_cal2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bt_robotmove = new System.Windows.Forms.Button();
            this.lb_step1 = new System.Windows.Forms.Label();
            this.lb_step2 = new System.Windows.Forms.Label();
            this.lb_step3 = new System.Windows.Forms.Label();
            this.lb_step5 = new System.Windows.Forms.Label();
            this.lb_step6 = new System.Windows.Forms.Label();
            this.lb_step7 = new System.Windows.Forms.Label();
            this.lb_step4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lb_robotx = new System.Windows.Forms.Label();
            this.lb_roboty = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // videoPanel
            // 
            this.videoPanel.Location = new System.Drawing.Point(336, 47);
            this.videoPanel.Name = "videoPanel";
            this.videoPanel.Size = new System.Drawing.Size(463, 342);
            this.videoPanel.TabIndex = 0;
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(32, 477);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(460, 319);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // bt_capture
            // 
            this.bt_capture.Location = new System.Drawing.Point(967, 121);
            this.bt_capture.Name = "bt_capture";
            this.bt_capture.Size = new System.Drawing.Size(75, 23);
            this.bt_capture.TabIndex = 2;
            this.bt_capture.Text = "CAPTURE";
            this.bt_capture.UseVisualStyleBackColor = true;
            this.bt_capture.Click += new System.EventHandler(this.bt_capture_Click);
            // 
            // bt_calculate
            // 
            this.bt_calculate.Location = new System.Drawing.Point(967, 268);
            this.bt_calculate.Name = "bt_calculate";
            this.bt_calculate.Size = new System.Drawing.Size(75, 23);
            this.bt_calculate.TabIndex = 3;
            this.bt_calculate.Text = "Shot1";
            this.bt_calculate.UseVisualStyleBackColor = true;
            this.bt_calculate.Click += new System.EventHandler(this.bt_calculate_Click);
            // 
            // lb_x1
            // 
            this.lb_x1.AutoSize = true;
            this.lb_x1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_x1.ForeColor = System.Drawing.Color.Blue;
            this.lb_x1.Location = new System.Drawing.Point(28, 16);
            this.lb_x1.Name = "lb_x1";
            this.lb_x1.Size = new System.Drawing.Size(34, 18);
            this.lb_x1.TabIndex = 4;
            this.lb_x1.Text = "Cx1";
            // 
            // lb_y1
            // 
            this.lb_y1.AutoSize = true;
            this.lb_y1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_y1.ForeColor = System.Drawing.Color.Blue;
            this.lb_y1.Location = new System.Drawing.Point(96, 16);
            this.lb_y1.Name = "lb_y1";
            this.lb_y1.Size = new System.Drawing.Size(34, 18);
            this.lb_y1.TabIndex = 5;
            this.lb_y1.Text = "Cy1";
            // 
            // lb_y2
            // 
            this.lb_y2.AutoSize = true;
            this.lb_y2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_y2.ForeColor = System.Drawing.Color.Blue;
            this.lb_y2.Location = new System.Drawing.Point(135, 34);
            this.lb_y2.Name = "lb_y2";
            this.lb_y2.Size = new System.Drawing.Size(34, 18);
            this.lb_y2.TabIndex = 7;
            this.lb_y2.Text = "Cy2";
            // 
            // lb_x2
            // 
            this.lb_x2.AutoSize = true;
            this.lb_x2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_x2.ForeColor = System.Drawing.Color.Blue;
            this.lb_x2.Location = new System.Drawing.Point(41, 34);
            this.lb_x2.Name = "lb_x2";
            this.lb_x2.Size = new System.Drawing.Size(34, 18);
            this.lb_x2.TabIndex = 6;
            this.lb_x2.Text = "Cx2";
            // 
            // lb_ratio1
            // 
            this.lb_ratio1.AutoSize = true;
            this.lb_ratio1.Location = new System.Drawing.Point(28, 64);
            this.lb_ratio1.Name = "lb_ratio1";
            this.lb_ratio1.Size = new System.Drawing.Size(38, 13);
            this.lb_ratio1.TabIndex = 8;
            this.lb_ratio1.Text = "Ratio1";
            // 
            // lb_ratio2
            // 
            this.lb_ratio2.AutoSize = true;
            this.lb_ratio2.Location = new System.Drawing.Point(37, 75);
            this.lb_ratio2.Name = "lb_ratio2";
            this.lb_ratio2.Size = new System.Drawing.Size(38, 13);
            this.lb_ratio2.TabIndex = 9;
            this.lb_ratio2.Text = "Ratio2";
            // 
            // lb_angle2
            // 
            this.lb_angle2.AutoSize = true;
            this.lb_angle2.Location = new System.Drawing.Point(130, 77);
            this.lb_angle2.Name = "lb_angle2";
            this.lb_angle2.Size = new System.Drawing.Size(39, 13);
            this.lb_angle2.TabIndex = 11;
            this.lb_angle2.Text = "angle2";
            // 
            // lb_angle1
            // 
            this.lb_angle1.AutoSize = true;
            this.lb_angle1.Location = new System.Drawing.Point(96, 64);
            this.lb_angle1.Name = "lb_angle1";
            this.lb_angle1.Size = new System.Drawing.Size(39, 13);
            this.lb_angle1.TabIndex = 10;
            this.lb_angle1.Text = "angle1";
            // 
            // lb_predict2
            // 
            this.lb_predict2.AutoSize = true;
            this.lb_predict2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_predict2.ForeColor = System.Drawing.Color.Red;
            this.lb_predict2.Location = new System.Drawing.Point(247, 77);
            this.lb_predict2.Name = "lb_predict2";
            this.lb_predict2.Size = new System.Drawing.Size(67, 20);
            this.lb_predict2.TabIndex = 13;
            this.lb_predict2.Text = "Predict2";
            // 
            // lb_predict1
            // 
            this.lb_predict1.AutoSize = true;
            this.lb_predict1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_predict1.ForeColor = System.Drawing.Color.Red;
            this.lb_predict1.Location = new System.Drawing.Point(167, 42);
            this.lb_predict1.Name = "lb_predict1";
            this.lb_predict1.Size = new System.Drawing.Size(67, 20);
            this.lb_predict1.TabIndex = 12;
            this.lb_predict1.Text = "Predict1";
            // 
            // lb_X
            // 
            this.lb_X.AutoSize = true;
            this.lb_X.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_X.ForeColor = System.Drawing.Color.Red;
            this.lb_X.Location = new System.Drawing.Point(750, 855);
            this.lb_X.Name = "lb_X";
            this.lb_X.Size = new System.Drawing.Size(27, 25);
            this.lb_X.TabIndex = 14;
            this.lb_X.Text = "X";
            // 
            // lb_Y
            // 
            this.lb_Y.AutoSize = true;
            this.lb_Y.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_Y.ForeColor = System.Drawing.Color.Red;
            this.lb_Y.Location = new System.Drawing.Point(750, 907);
            this.lb_Y.Name = "lb_Y";
            this.lb_Y.Size = new System.Drawing.Size(26, 25);
            this.lb_Y.TabIndex = 15;
            this.lb_Y.Text = "Y";
            // 
            // lb_Z
            // 
            this.lb_Z.AutoSize = true;
            this.lb_Z.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_Z.ForeColor = System.Drawing.Color.Red;
            this.lb_Z.Location = new System.Drawing.Point(750, 954);
            this.lb_Z.Name = "lb_Z";
            this.lb_Z.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lb_Z.Size = new System.Drawing.Size(25, 25);
            this.lb_Z.TabIndex = 16;
            this.lb_Z.Text = "Z";
            // 
            // bt_cal2
            // 
            this.bt_cal2.Location = new System.Drawing.Point(967, 268);
            this.bt_cal2.Name = "bt_cal2";
            this.bt_cal2.Size = new System.Drawing.Size(75, 23);
            this.bt_cal2.TabIndex = 17;
            this.bt_cal2.Text = "Shot2";
            this.bt_cal2.UseVisualStyleBackColor = true;
            this.bt_cal2.Click += new System.EventHandler(this.bt_cal2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lb_predict1);
            this.groupBox1.Controls.Add(this.lb_angle1);
            this.groupBox1.Controls.Add(this.lb_ratio1);
            this.groupBox1.Controls.Add(this.lb_y1);
            this.groupBox1.Controls.Add(this.lb_x1);
            this.groupBox1.Location = new System.Drawing.Point(32, 855);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 119);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lb_x2);
            this.groupBox2.Controls.Add(this.lb_y2);
            this.groupBox2.Controls.Add(this.lb_ratio2);
            this.groupBox2.Controls.Add(this.lb_angle2);
            this.groupBox2.Controls.Add(this.lb_predict2);
            this.groupBox2.Location = new System.Drawing.Point(350, 855);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(350, 119);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(523, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 25);
            this.label1.TabIndex = 20;
            this.label1.Text = "LIVE VIEW";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(173, 442);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 25);
            this.label2.TabIndex = 21;
            this.label2.Text = "IMAGE RESUTL 1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(799, 442);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 25);
            this.label3.TabIndex = 22;
            this.label3.Text = "IMAGE RESUTL 2";
            // 
            // bt_robotmove
            // 
            this.bt_robotmove.Location = new System.Drawing.Point(967, 366);
            this.bt_robotmove.Name = "bt_robotmove";
            this.bt_robotmove.Size = new System.Drawing.Size(75, 23);
            this.bt_robotmove.TabIndex = 23;
            this.bt_robotmove.Text = "Navigate";
            this.bt_robotmove.UseVisualStyleBackColor = true;
            this.bt_robotmove.Click += new System.EventHandler(this.bt_robotmove_Click);
            // 
            // lb_step1
            // 
            this.lb_step1.AutoSize = true;
            this.lb_step1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_step1.Location = new System.Drawing.Point(864, 71);
            this.lb_step1.Name = "lb_step1";
            this.lb_step1.Size = new System.Drawing.Size(326, 24);
            this.lb_step1.TabIndex = 24;
            this.lb_step1.Text = "Press Capture to aquire the first image";
            // 
            // lb_step2
            // 
            this.lb_step2.AutoSize = true;
            this.lb_step2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_step2.Location = new System.Drawing.Point(822, 165);
            this.lb_step2.Name = "lb_step2";
            this.lb_step2.Size = new System.Drawing.Size(403, 24);
            this.lb_step2.TabIndex = 25;
            this.lb_step2.Text = "The image of first shot is shown on Left window";
            // 
            // lb_step3
            // 
            this.lb_step3.AutoSize = true;
            this.lb_step3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_step3.Location = new System.Drawing.Point(827, 223);
            this.lb_step3.Name = "lb_step3";
            this.lb_step3.Size = new System.Drawing.Size(398, 24);
            this.lb_step3.TabIndex = 26;
            this.lb_step3.Text = "Press the bottom to calculate first shot of image";
            // 
            // lb_step5
            // 
            this.lb_step5.AutoSize = true;
            this.lb_step5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_step5.Location = new System.Drawing.Point(827, 165);
            this.lb_step5.Name = "lb_step5";
            this.lb_step5.Size = new System.Drawing.Size(410, 24);
            this.lb_step5.TabIndex = 27;
            this.lb_step5.Text = "The image of first shot is shown on right window";
            // 
            // lb_step6
            // 
            this.lb_step6.AutoSize = true;
            this.lb_step6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_step6.Location = new System.Drawing.Point(827, 223);
            this.lb_step6.Name = "lb_step6";
            this.lb_step6.Size = new System.Drawing.Size(434, 24);
            this.lb_step6.TabIndex = 28;
            this.lb_step6.Text = "Press the bottom to calculate second shot of image";
            // 
            // lb_step7
            // 
            this.lb_step7.AutoSize = true;
            this.lb_step7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_step7.Location = new System.Drawing.Point(860, 321);
            this.lb_step7.Name = "lb_step7";
            this.lb_step7.Size = new System.Drawing.Size(298, 24);
            this.lb_step7.TabIndex = 29;
            this.lb_step7.Text = "Press the bottom to start navigation";
            // 
            // lb_step4
            // 
            this.lb_step4.AutoSize = true;
            this.lb_step4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_step4.Location = new System.Drawing.Point(860, 71);
            this.lb_step4.Name = "lb_step4";
            this.lb_step4.Size = new System.Drawing.Size(362, 24);
            this.lb_step4.TabIndex = 30;
            this.lb_step4.Text = "Press Capture to aquire the second image";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(628, 477);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(460, 319);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 31;
            this.pictureBox1.TabStop = false;
            // 
            // lb_robotx
            // 
            this.lb_robotx.AutoSize = true;
            this.lb_robotx.Location = new System.Drawing.Point(82, 71);
            this.lb_robotx.Name = "lb_robotx";
            this.lb_robotx.Size = new System.Drawing.Size(127, 13);
            this.lb_robotx.TabIndex = 32;
            this.lb_robotx.Text = "Robot Move at X position";
            // 
            // lb_roboty
            // 
            this.lb_roboty.AutoSize = true;
            this.lb_roboty.Location = new System.Drawing.Point(82, 109);
            this.lb_roboty.Name = "lb_roboty";
            this.lb_roboty.Size = new System.Drawing.Size(129, 13);
            this.lb_roboty.TabIndex = 33;
            this.lb_roboty.Text = "Robot Move at Y poisition";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1264, 746);
            this.Controls.Add(this.lb_roboty);
            this.Controls.Add(this.lb_robotx);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lb_step4);
            this.Controls.Add(this.lb_step7);
            this.Controls.Add(this.lb_step6);
            this.Controls.Add(this.lb_step5);
            this.Controls.Add(this.lb_step3);
            this.Controls.Add(this.lb_step2);
            this.Controls.Add(this.lb_step1);
            this.Controls.Add(this.bt_robotmove);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bt_cal2);
            this.Controls.Add(this.lb_Z);
            this.Controls.Add(this.lb_Y);
            this.Controls.Add(this.lb_X);
            this.Controls.Add(this.bt_calculate);
            this.Controls.Add(this.bt_capture);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.videoPanel);
            this.Controls.Add(this.groupBox2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel videoPanel;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button bt_capture;
        private System.Windows.Forms.Button bt_calculate;
        private System.Windows.Forms.Label lb_x1;
        private System.Windows.Forms.Label lb_y1;
        private System.Windows.Forms.Label lb_y2;
        private System.Windows.Forms.Label lb_x2;
        private System.Windows.Forms.Label lb_ratio1;
        private System.Windows.Forms.Label lb_ratio2;
        private System.Windows.Forms.Label lb_angle2;
        private System.Windows.Forms.Label lb_angle1;
        private System.Windows.Forms.Label lb_predict2;
        private System.Windows.Forms.Label lb_predict1;
        private System.Windows.Forms.Label lb_X;
        private System.Windows.Forms.Label lb_Y;
        private System.Windows.Forms.Label lb_Z;
        private System.Windows.Forms.Button bt_cal2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bt_robotmove;
        private System.Windows.Forms.Label lb_step1;
        private System.Windows.Forms.Label lb_step2;
        private System.Windows.Forms.Label lb_step3;
        private System.Windows.Forms.Label lb_step5;
        private System.Windows.Forms.Label lb_step6;
        private System.Windows.Forms.Label lb_step7;
        private System.Windows.Forms.Label lb_step4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lb_robotx;
        private System.Windows.Forms.Label lb_roboty;

    }
}

