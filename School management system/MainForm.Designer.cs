namespace School_management_system
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            panel1 = new Panel();
            btn_AddRecord = new Button();
            btn_Home = new Button();
            lbl_Username = new Label();
            panel2 = new Panel();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            pic_UserImage = new SchoolSystem.Controls.CircularPictureBox();
            btn_PeopleList = new Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_UserImage).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.OldLace;
            panel1.Controls.Add(btn_AddRecord);
            panel1.Controls.Add(btn_Home);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(257, 878);
            panel1.TabIndex = 0;
            // 
            // btn_AddRecord
            // 
            btn_AddRecord.Cursor = Cursors.Hand;
            btn_AddRecord.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_AddRecord.Image = (Image)resources.GetObject("btn_AddRecord.Image");
            btn_AddRecord.ImageAlign = ContentAlignment.MiddleLeft;
            btn_AddRecord.Location = new Point(0, 152);
            btn_AddRecord.Name = "btn_AddRecord";
            btn_AddRecord.Size = new Size(255, 60);
            btn_AddRecord.TabIndex = 2;
            btn_AddRecord.Text = "Add record";
            btn_AddRecord.UseVisualStyleBackColor = true;
            btn_AddRecord.Click += btn_AddRecord_Click;
            // 
            // btn_Home
            // 
            btn_Home.Cursor = Cursors.Hand;
            btn_Home.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_Home.Image = Properties.Resources.home;
            btn_Home.ImageAlign = ContentAlignment.MiddleLeft;
            btn_Home.Location = new Point(0, 91);
            btn_Home.Name = "btn_Home";
            btn_Home.Size = new Size(255, 60);
            btn_Home.TabIndex = 1;
            btn_Home.Text = "Home";
            btn_Home.UseVisualStyleBackColor = true;
            btn_Home.Click += btn_Home_Click;
            // 
            // lbl_Username
            // 
            lbl_Username.AutoSize = true;
            lbl_Username.Location = new Point(1366, 31);
            lbl_Username.Name = "lbl_Username";
            lbl_Username.Size = new Size(75, 20);
            lbl_Username.TabIndex = 1;
            lbl_Username.Text = "Username";
            // 
            // panel2
            // 
            panel2.BackColor = Color.Ivory;
            panel2.Controls.Add(pictureBox1);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(pic_UserImage);
            panel2.Controls.Add(lbl_Username);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1525, 85);
            panel2.TabIndex = 2;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.school_building_in_flat_style_modern_school_college_building_illustration_vector;
            pictureBox1.Location = new Point(3, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(114, 79);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(123, 27);
            label2.Name = "label2";
            label2.Size = new Size(149, 31);
            label2.TabIndex = 3;
            label2.Text = "School Name";
            // 
            // pic_UserImage
            // 
            pic_UserImage.BorderColor = Color.Black;
            pic_UserImage.BorderSize = 1F;
            pic_UserImage.Image = Properties.Resources.photo_2026_03_04_14_37_39;
            pic_UserImage.Location = new Point(1447, 10);
            pic_UserImage.Name = "pic_UserImage";
            pic_UserImage.Size = new Size(66, 66);
            pic_UserImage.SizeMode = PictureBoxSizeMode.StretchImage;
            pic_UserImage.TabIndex = 0;
            pic_UserImage.TabStop = false;
            // 
            // btn_PeopleList
            // 
            btn_PeopleList.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_PeopleList.Location = new Point(290, 106);
            btn_PeopleList.Name = "btn_PeopleList";
            btn_PeopleList.Size = new Size(170, 123);
            btn_PeopleList.TabIndex = 3;
            btn_PeopleList.Text = "People";
            btn_PeopleList.UseVisualStyleBackColor = true;
            btn_PeopleList.Click += btn_PeopleList_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1525, 878);
            Controls.Add(btn_PeopleList);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_UserImage).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label lbl_Username;
        private Button btn_AddRecord;
        private Button btn_Home;
        private Panel panel2;
        private SchoolSystem.Controls.CircularPictureBox pic_UserImage;
        private PictureBox pictureBox1;
        private Label label2;
        private Button btn_PeopleList;
    }
}
