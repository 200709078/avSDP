namespace sdp_02
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.color_picker = new System.Windows.Forms.PictureBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_line = new System.Windows.Forms.Button();
            this.btn_rectangle = new System.Windows.Forms.Button();
            this.btn_elips = new System.Windows.Forms.Button();
            this.btn_eraser = new System.Windows.Forms.Button();
            this.pencil = new System.Windows.Forms.Button();
            this.btn_fill = new System.Windows.Forms.Button();
            this.pic_color = new System.Windows.Forms.Button();
            this.btn_color = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pic = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.color_picker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.color_picker);
            this.panel1.Controls.Add(this.btn_save);
            this.panel1.Controls.Add(this.btn_clear);
            this.panel1.Controls.Add(this.btn_line);
            this.panel1.Controls.Add(this.btn_rectangle);
            this.panel1.Controls.Add(this.btn_elips);
            this.panel1.Controls.Add(this.btn_eraser);
            this.panel1.Controls.Add(this.pencil);
            this.panel1.Controls.Add(this.btn_fill);
            this.panel1.Controls.Add(this.pic_color);
            this.panel1.Controls.Add(this.btn_color);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(944, 100);
            this.panel1.TabIndex = 0;
            // 
            // color_picker
            // 
            this.color_picker.Image = global::sdp_02.Properties.Resources.color_picker;
            this.color_picker.Location = new System.Drawing.Point(12, 0);
            this.color_picker.Name = "color_picker";
            this.color_picker.Size = new System.Drawing.Size(95, 97);
            this.color_picker.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.color_picker.TabIndex = 6;
            this.color_picker.TabStop = false;
            this.color_picker.MouseClick += new System.Windows.Forms.MouseEventHandler(this.color_picker_MouseClick);
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_save.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_save.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_save.Location = new System.Drawing.Point(853, 14);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(70, 30);
            this.btn_save.TabIndex = 5;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_clear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_clear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btn_clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_clear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_clear.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_clear.Location = new System.Drawing.Point(853, 50);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(70, 30);
            this.btn_clear.TabIndex = 5;
            this.btn_clear.Text = "Clear";
            this.btn_clear.UseVisualStyleBackColor = false;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_line
            // 
            this.btn_line.BackColor = System.Drawing.Color.Black;
            this.btn_line.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_line.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btn_line.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_line.Image = global::sdp_02.Properties.Resources.line;
            this.btn_line.Location = new System.Drawing.Point(770, 14);
            this.btn_line.Name = "btn_line";
            this.btn_line.Size = new System.Drawing.Size(60, 60);
            this.btn_line.TabIndex = 5;
            this.btn_line.UseVisualStyleBackColor = false;
            this.btn_line.Click += new System.EventHandler(this.btn_line_Click);
            // 
            // btn_rectangle
            // 
            this.btn_rectangle.BackColor = System.Drawing.Color.Black;
            this.btn_rectangle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_rectangle.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btn_rectangle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_rectangle.Image = global::sdp_02.Properties.Resources.rectangle;
            this.btn_rectangle.Location = new System.Drawing.Point(678, 14);
            this.btn_rectangle.Name = "btn_rectangle";
            this.btn_rectangle.Size = new System.Drawing.Size(60, 60);
            this.btn_rectangle.TabIndex = 5;
            this.btn_rectangle.UseVisualStyleBackColor = false;
            this.btn_rectangle.Click += new System.EventHandler(this.btn_rectangle_Click);
            // 
            // btn_elips
            // 
            this.btn_elips.BackColor = System.Drawing.Color.Black;
            this.btn_elips.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_elips.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btn_elips.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_elips.Image = global::sdp_02.Properties.Resources.elips;
            this.btn_elips.Location = new System.Drawing.Point(586, 14);
            this.btn_elips.Name = "btn_elips";
            this.btn_elips.Size = new System.Drawing.Size(60, 60);
            this.btn_elips.TabIndex = 5;
            this.btn_elips.UseVisualStyleBackColor = false;
            this.btn_elips.Click += new System.EventHandler(this.btn_elips_Click);
            // 
            // btn_eraser
            // 
            this.btn_eraser.BackColor = System.Drawing.Color.Black;
            this.btn_eraser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_eraser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btn_eraser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_eraser.Image = global::sdp_02.Properties.Resources.eraser;
            this.btn_eraser.Location = new System.Drawing.Point(494, 14);
            this.btn_eraser.Name = "btn_eraser";
            this.btn_eraser.Size = new System.Drawing.Size(60, 60);
            this.btn_eraser.TabIndex = 4;
            this.btn_eraser.UseVisualStyleBackColor = false;
            this.btn_eraser.Click += new System.EventHandler(this.btn_eraser_Click);
            // 
            // pencil
            // 
            this.pencil.BackColor = System.Drawing.Color.Black;
            this.pencil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pencil.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.pencil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pencil.Image = global::sdp_02.Properties.Resources.pencil;
            this.pencil.Location = new System.Drawing.Point(402, 14);
            this.pencil.Name = "pencil";
            this.pencil.Size = new System.Drawing.Size(60, 60);
            this.pencil.TabIndex = 3;
            this.pencil.UseVisualStyleBackColor = false;
            this.pencil.Click += new System.EventHandler(this.pencil_Click);
            // 
            // btn_fill
            // 
            this.btn_fill.BackColor = System.Drawing.Color.Black;
            this.btn_fill.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_fill.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btn_fill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_fill.Image = global::sdp_02.Properties.Resources.fill;
            this.btn_fill.Location = new System.Drawing.Point(310, 14);
            this.btn_fill.Name = "btn_fill";
            this.btn_fill.Size = new System.Drawing.Size(60, 60);
            this.btn_fill.TabIndex = 2;
            this.btn_fill.UseVisualStyleBackColor = false;
            this.btn_fill.Click += new System.EventHandler(this.btn_fill_Click);
            // 
            // pic_color
            // 
            this.pic_color.BackColor = System.Drawing.Color.White;
            this.pic_color.Enabled = false;
            this.pic_color.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.pic_color.Location = new System.Drawing.Point(126, 14);
            this.pic_color.Name = "pic_color";
            this.pic_color.Size = new System.Drawing.Size(60, 60);
            this.pic_color.TabIndex = 1;
            this.pic_color.UseVisualStyleBackColor = false;
            // 
            // btn_color
            // 
            this.btn_color.BackColor = System.Drawing.Color.Black;
            this.btn_color.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_color.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btn_color.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_color.Image = global::sdp_02.Properties.Resources.palette;
            this.btn_color.Location = new System.Drawing.Point(218, 14);
            this.btn_color.Name = "btn_color";
            this.btn_color.Size = new System.Drawing.Size(60, 60);
            this.btn_color.TabIndex = 0;
            this.btn_color.UseVisualStyleBackColor = false;
            this.btn_color.Click += new System.EventHandler(this.btn_color_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 476);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(944, 27);
            this.panel2.TabIndex = 1;
            // 
            // pic
            // 
            this.pic.BackColor = System.Drawing.Color.White;
            this.pic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic.Location = new System.Drawing.Point(0, 0);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(944, 503);
            this.pic.TabIndex = 2;
            this.pic.TabStop = false;
            this.pic.Paint += new System.Windows.Forms.PaintEventHandler(this.pic_Paint);
            this.pic.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pic_MouseClick);
            this.pic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic_MouseDown);
            this.pic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic_MouseMove);
            this.pic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pic_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 503);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SDP_02";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.color_picker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Button btn_color;
        private Panel panel2;
        private PictureBox pic;
        private Button pencil;
        private Button btn_fill;
        private Button pic_color;
        private Button btn_line;
        private Button btn_rectangle;
        private Button btn_elips;
        private Button btn_eraser;
        private Button btn_save;
        private Button btn_clear;
        private PictureBox color_picker;
    }
}