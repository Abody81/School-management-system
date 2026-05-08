using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace SchoolSystem.Controls
{
    public partial class CircularPictureBox : PictureBox
    {
        // إضافة الأوسمة لحل مشكلة التسلسل (Serialization)
        [Category("Appearance")] // تظهر تحت قسم Appearance في شاشة الخصائص
        [Description("سمك الإطار الخارجي للصورة")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float BorderSize { get; set; } = 2f;

        [Category("Appearance")]
        [Description("لون الإطار الخارجي للصورة")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BorderColor { get; set; } = Color.RoyalBlue;

        public CircularPictureBox()
        {
            this.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            var graphics = pe.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias; // لتنعيم الحواف

            using (var path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, this.Width - 1, this.Height - 1);
                this.Region = new Region(path);

                if (BorderSize > 0)
                {
                    using (var pen = new Pen(BorderColor, BorderSize))
                    {
                        pen.Alignment = PenAlignment.Inset;
                        graphics.DrawEllipse(pen, 0, 0, this.Width - 1, this.Height - 1);
                    }
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Size = new Size(this.Width, this.Width);
        }
    }
}