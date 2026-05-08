namespace School_management_system
{
    partial class RecodesForm
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
            panel1 = new Panel();
            btn_close = new Button();
            panel2 = new Panel();
            button2 = new Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btn_close);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(738, 54);
            panel1.TabIndex = 0;
            // 
            // btn_close
            // 
            btn_close.BackColor = Color.OrangeRed;
            btn_close.FlatStyle = FlatStyle.Flat;
            btn_close.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_close.Location = new Point(690, 3);
            btn_close.Name = "btn_close";
            btn_close.Size = new Size(45, 45);
            btn_close.TabIndex = 2;
            btn_close.Text = "X";
            btn_close.UseVisualStyleBackColor = false;
            btn_close.Click += btn_close_Click;
            btn_close.MouseEnter += btn_close_MouseEnter;
            // 
            // panel2
            // 
            panel2.Controls.Add(button2);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 54);
            panel2.Name = "panel2";
            panel2.Size = new Size(738, 414);
            panel2.TabIndex = 1;
            // 
            // button2
            // 
            button2.Location = new Point(12, 6);
            button2.Name = "button2";
            button2.Size = new Size(150, 100);
            button2.TabIndex = 0;
            button2.Text = "Add new person";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // RecodesForm
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(738, 468);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "RecodesForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "RecodesForm";
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Button btn_close;
        private Button button2;
    }
}