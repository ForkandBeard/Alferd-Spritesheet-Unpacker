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
        private int Flood;
        private int Counter = 0;
        private int NewDrops;
        private Random Random = new Random();

        private List<PointF> Drops = new List<PointF>();

        public AboutForm()
        {
            Paint += frmAbout_Paint;
            Load += frmAbout_Load;
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

                Graphics objGraphics = default(Graphics);

                objGraphics = Graphics.FromImage(this.CoolBackGroundImage);
                objGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                if (this.Random.Next(0, 20) == 1)
                {
                    Bitmap objHead = default(Bitmap);
                    objHead = global::ASU.Properties.Resources.AlferdPackerHead;

                    if (this.Random.Next(0, 2) == 1)
                    {
                        objHead.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    }

                    if (this.Random.Next(0, 2) == 1)
                    {
                        objHead.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    }

                    objHead.MakeTransparent(Color.White);
                    objHead = BO.Rotate.RotateImage(objHead, this.Random.Next(0, 360));

                    RectangleF objRandomRectangle = default(RectangleF);

                    objRandomRectangle = new RectangleF(ForkandBeard.Util.Geometry.GeometryHelper.RandomPointInRectangle(new Rectangle(0, 0, this.CoolBackGroundImage.Width, this.CoolBackGroundImage.Height)), objHead.Size);

                    objGraphics.DrawImage(objHead, ForkandBeard.Util.Geometry.GeometryHelper.RandomPointInRectangle(new Rectangle(0, 0, this.CoolBackGroundImage.Width, this.CoolBackGroundImage.Height)));
                    objGraphics.DrawLine(Pens.Black, ForkandBeard.Util.Geometry.GeometryHelper.RandomPointInRectangle(objRandomRectangle), ForkandBeard.Util.Geometry.GeometryHelper.RandomPointInRectangle(new Rectangle(0, 0, this.CoolBackGroundImage.Width, this.CoolBackGroundImage.Height)));
                    objHead.Dispose();
                    objGraphics.Dispose();
                    objGraphics = Graphics.FromImage(this.CoolBackGroundImage);
                    objGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                }

                if (this.Random.Next(0, 20) == 1)
                {
                    int intRand = 0;
                    intRand = this.Random.Next(0, 5);
                    switch (intRand)
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
                    int intSize = 0;
                    intSize = this.Random.Next(1, 25);
                    objGraphics.FillEllipse(new SolidBrush(Color.FromArgb(100, 200, 0, 0)), new RectangleF(ForkandBeard.Util.Geometry.GeometryHelper.RandomPointInRectangle(new Rectangle(0, 0, this.CoolBackGroundImage.Width, this.CoolBackGroundImage.Height)), new Size(intSize, intSize)));
                }

                if (this.Random.Next(0, 2) == 1)
                {
                    objGraphics.DrawLine(new Pen(Color.FromArgb(100, 50, 50, 50), 3), ForkandBeard.Util.Geometry.GeometryHelper.RandomPointInRectangle(new Rectangle(0, 0, this.CoolBackGroundImage.Width, this.CoolBackGroundImage.Height)), ForkandBeard.Util.Geometry.GeometryHelper.RandomPointInRectangle(new Rectangle(0, 0, this.CoolBackGroundImage.Width, this.CoolBackGroundImage.Height)));
                }
                else
                {
                    objGraphics.DrawLine(Pens.White, ForkandBeard.Util.Geometry.GeometryHelper.RandomPointInRectangle(new Rectangle(0, 0, this.CoolBackGroundImage.Width, this.CoolBackGroundImage.Height)), ForkandBeard.Util.Geometry.GeometryHelper.RandomPointInRectangle(new Rectangle(0, 0, this.CoolBackGroundImage.Width, this.CoolBackGroundImage.Height)));
                }

                objGraphics.Dispose();
            }
            catch (Exception sorry)
            {
            }
        }

        private void RotateBackground()
        {
            Graphics objGraphics = default(Graphics);
            Bitmap objOldImage = default(Bitmap);

            objOldImage = (Bitmap)this.CoolBackGroundImage.Clone();
            this.CoolBackGroundImage.Dispose();

            this.CoolBackGroundImage = new Bitmap(this.Width * 2, this.Height * 2);

            objGraphics = Graphics.FromImage(this.CoolBackGroundImage);
            objGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            objGraphics.RotateTransform(0.05f);

            objGraphics.DrawImage(objOldImage, this.Movement);
            objGraphics.Dispose();
            objOldImage.Dispose();
        }

        private void frmAbout_Load(object sender, System.EventArgs e)
        {
            MessageBox.Show("Any bugs, suggestions, feedback to cat@forkandbeard.co.uk");
            this.CoolBackGroundImage = new Bitmap(this.Width * 2, this.Height * 2);
            Graphics.FromImage(this.CoolBackGroundImage).FillRectangle(Brushes.Silver, new Rectangle(0, 0, this.Width * 2, this.Height * 2));
        }

        private void frmAbout_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
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
