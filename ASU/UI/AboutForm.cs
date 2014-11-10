using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ASU.UI
{
    public partial class AboutForm
    {
        private PointF Movement = new PointF(0, 0);
        private Bitmap CoolBackGroundImage = null;
        private Random Random = new Random();

        private List<PointF> Drops = new List<PointF>();

        public AboutForm()
        {
            this.InitializeComponent();
            Paint += AboutForm_Paint;
            Load += AboutForm_Load;
        }

        private void AboutForm_Load(object sender, System.EventArgs e)
        {
            MessageBox.Show("Any bugs, suggestions, feedback can be left @ https://github.com/ForkandBeard/Alferd-Spritesheet-Unpacker/issues");
            this.CoolBackGroundImage = new Bitmap(this.Width * 2, this.Height * 2);
            Graphics.FromImage(this.CoolBackGroundImage).FillRectangle(Brushes.Silver, new Rectangle(0, 0, this.Width * 2, this.Height * 2));
        }

        private void Timer1_Tick(System.Object sender, System.EventArgs e)
        {
            try
            {
                this.Refresh();

                if (this.CoolBackGroundImage == null)
                {
                    return;
                }

                this.RotateBackground();

                Graphics graphics = default(Graphics);

                graphics = Graphics.FromImage(this.CoolBackGroundImage);
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                if (this.Random.Next(0, 20) == 1)
                {
                    Bitmap head;
                    head = global::ASU.Properties.Resources.AlferdPackerHead;

                    if (this.Random.Next(0, 2) == 1)
                    {
                        head.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    }

                    if (this.Random.Next(0, 2) == 1)
                    {
                        head.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    }

                    head.MakeTransparent(Color.White);
                    head = BO.Rotate.RotateImage(head, this.Random.Next(0, 360));

                    RectangleF randomRectangle = default(RectangleF);

                    randomRectangle = new RectangleF(ForkandBeard.Util.Geometry.GeometryHelper.RandomPointInRectangle(new Rectangle(0, 0, this.CoolBackGroundImage.Width, this.CoolBackGroundImage.Height)), head.Size);

                    graphics.DrawImage(head, ForkandBeard.Util.Geometry.GeometryHelper.RandomPointInRectangle(new Rectangle(0, 0, this.CoolBackGroundImage.Width, this.CoolBackGroundImage.Height)));
                    graphics.DrawLine(Pens.Black, ForkandBeard.Util.Geometry.GeometryHelper.RandomPointInRectangle(randomRectangle), ForkandBeard.Util.Geometry.GeometryHelper.RandomPointInRectangle(new Rectangle(0, 0, this.CoolBackGroundImage.Width, this.CoolBackGroundImage.Height)));
                    head.Dispose();
                    graphics.Dispose();
                    graphics = Graphics.FromImage(this.CoolBackGroundImage);
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                }

                if (this.Random.Next(0, 20) == 1)
                {
                    int randomInteger = 0;
                    randomInteger = this.Random.Next(0, 5);
                    switch (randomInteger)
                    {
                        case 0:
                            this.Movement = new PointF(0f, 0.1f);
                            break;
                        case 1:
                            this.Movement = new PointF(0f, -0.1f);
                            break;
                        case 2:
                            this.Movement = new PointF(0.1f, 0f);
                            break;
                        case 3:
                            this.Movement = new PointF(-0.1f, 0f);
                            break;
                        case 4:
                            this.Movement = new PointF(0, 0);
                            break;
                    }
                }

                if (this.Random.Next(0, 10) == 1)
                {
                    int size = 0;
                    size = this.Random.Next(1, 25);
                    graphics.FillEllipse(new SolidBrush(Color.FromArgb(100, 200, 0, 0)), new RectangleF(ForkandBeard.Util.Geometry.GeometryHelper.RandomPointInRectangle(new Rectangle(0, 0, this.CoolBackGroundImage.Width, this.CoolBackGroundImage.Height)), new Size(size, size)));
                }

                if (this.Random.Next(0, 2) == 1)
                {
                    graphics.DrawLine(new Pen(Color.FromArgb(100, 50, 50, 50), 3), ForkandBeard.Util.Geometry.GeometryHelper.RandomPointInRectangle(new Rectangle(0, 0, this.CoolBackGroundImage.Width, this.CoolBackGroundImage.Height)), ForkandBeard.Util.Geometry.GeometryHelper.RandomPointInRectangle(new Rectangle(0, 0, this.CoolBackGroundImage.Width, this.CoolBackGroundImage.Height)));
                }
                else
                {
                    graphics.DrawLine(Pens.White, ForkandBeard.Util.Geometry.GeometryHelper.RandomPointInRectangle(new Rectangle(0, 0, this.CoolBackGroundImage.Width, this.CoolBackGroundImage.Height)), ForkandBeard.Util.Geometry.GeometryHelper.RandomPointInRectangle(new Rectangle(0, 0, this.CoolBackGroundImage.Width, this.CoolBackGroundImage.Height)));
                }

                graphics.Dispose();
            }
            catch (Exception sorry)
            {
            }
        }

        private void RotateBackground()
        {
            Graphics graphics = default(Graphics);
            Bitmap oldImage = default(Bitmap);

            oldImage = (Bitmap)this.CoolBackGroundImage.Clone();
            this.CoolBackGroundImage.Dispose();

            this.CoolBackGroundImage = new Bitmap(this.Width * 2, this.Height * 2);

            graphics = Graphics.FromImage(this.CoolBackGroundImage);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            graphics.RotateTransform(0.05f);

            graphics.DrawImage(oldImage, this.Movement);
            graphics.Dispose();
            oldImage.Dispose();
        }

        private void AboutForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.DrawImage(this.CoolBackGroundImage, (this.CoolBackGroundImage.Width * 0.3f) * -1, (this.CoolBackGroundImage.Height * 0.3f) * -1);
        }

        private void LinkLabel1_LinkClicked(System.Object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.forkandbeard.co.uk?app=Alferd");
            this.LinkLabel1.Visible = false;
        }

        private void Label3_Click(System.Object sender, System.EventArgs e)
        {
            this.Label3.Visible = false;
        }

        private void Label2_Click(System.Object sender, System.EventArgs e)
        {
            this.Label2.Visible = false;
        }

        private void Label1_Click(System.Object sender, System.EventArgs e)
        {
            this.Label1.Visible = false;
        }

        private void BuffablePanel1_Click(object sender, System.EventArgs e)
        {
            this.BuffablePanel1.Visible = false;
        }
    }
}
