using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ASU.UI
{
    public partial class MainForm : Form
    {
        #region " Class Data "
        private const string STR_FORM_TITLE = "Alferd Spritesheet Unpacker ver.{0} {1}";

        private const int INT_REGION_WIDTHS = 200;
        private bool LoadingImage = false;

        private bool IsMouseDown;
        private string FormTitle;

        private Bitmap ZoomImage;
        private Bitmap OriginalImage = null;
        private Bitmap PaintedImage = null;

        private string OverlayText;
        private Point MouseLocation;
        private Point MouseDownLocation;
        private Point Offset = new Point(0, 0);
        private List<Rectangle> _Boxes = new List<Rectangle>();
        private List<Rectangle> Boxes
        {
            get { return this._Boxes; }
            set { this._Boxes = value; }
        }
        private List<Rectangle> Selected = new List<Rectangle>();
        private List<Rectangle> Splits = new List<Rectangle>();
        private Rectangle SplitTopLeft;
        private Rectangle SplitBottomRight;
        private Rectangle BoxSplitting;
        private Rectangle Hover = Rectangle.Empty;
        private OptionsForm Options;
        private Rectangle HighlightRect = Rectangle.Empty;

        private bool SuppressThirdPartyWarningMessage = false;
        public static int DistanceBetweenTiles = 3;
        public static Bitmap SheetWithBoxes;
        private static Bitmap SheetWithBoxesEnlarged;
        public static SolidBrush HoverFill = new SolidBrush(Color.FromArgb(150, 224, 224, 224));
        public static SolidBrush SelectedFill = new SolidBrush(Color.FromArgb(200, 100, 100, 255));
        public static Pen ZoomPen = new Pen(Color.FromArgb(100, 100, 100, 255), 4);

        public static Pen Outline = new Pen(Color.FromArgb(225, 50, 50, 255), 2);
        public static System.Drawing.Imaging.ImageFormat ExportFormat = System.Drawing.Imaging.ImageFormat.Png;
        public static string ExportNConvertArgs = string.Empty;
        private static string ThirdPartyImageConverterPath;
        public static bool PromptForDestinationFolder = true;
        public static bool AutoOpenDestinationFolder = true;
        public static bool MakeBackgroundTransparent = true;
        public static bool PreservePallette = false;

        private System.Threading.Timer multipleUnpackerTimer;
        #endregion
        private Random Random = new Random();

        public MainForm()
        {
            this.InitializeComponent();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            try
            {
                this.SuppressThirdPartyWarningMessage = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["SuppressThirdPartyImageConverterWarningMessage"]);
                this.ExportLocationTextBox.Text = AppDomain.CurrentDomain.BaseDirectory;
                this.KeyPreview = true;
                ThirdPartyImageConverterPath = System.Configuration.ConfigurationManager.AppSettings["ThirdPartyImageConverter"];

                if (ThirdPartyImageConverterPath.StartsWith("\\"))
                {
                    ThirdPartyImageConverterPath = AppDomain.CurrentDomain.BaseDirectory + ThirdPartyImageConverterPath;
                }
                AutoOpenDestinationFolder = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["AutoOpenDestinationFolder"]);
                PromptForDestinationFolder = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["PromptForDestinationFolder"]);
                Outline.Width = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TileOutlineWidth"]);
                DistanceBetweenTiles = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DistanceBetweenFrames"]);
                MakeBackgroundTransparent = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["ExportedOptionsMakeBackgroundTransparent"]);
                PreservePallette = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["PreservePallette"]);

                Dictionary<string, System.Drawing.Imaging.ImageFormat> formats = new Dictionary<string, System.Drawing.Imaging.ImageFormat>();
                formats.Add("png", System.Drawing.Imaging.ImageFormat.Png);
                formats.Add("bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                formats.Add("gif", System.Drawing.Imaging.ImageFormat.Gif);
                formats.Add("tiff", System.Drawing.Imaging.ImageFormat.Tiff);
                formats.Add("jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                formats.Add("jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                ExportFormat = formats[System.Configuration.ConfigurationManager.AppSettings["ExportedOptionsFileFormat"].Replace(".", "").ToLower()];
            }
            catch (Exception ex)
            {
                ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk");
            }
        }

        private List<BO.ImageUnpacker> unpackers = new List<BO.ImageUnpacker>();
        private void CreateUnpacker(Bitmap image, string fileName)
        {
            BO.ImageUnpacker unpacker;

            this.OptionsPanel.Enabled = false;
            this.Boxes.Clear();
            this.Selected.Clear();
            this.Hover = Rectangle.Empty;

            if (this.OriginalImage != null)
            {
                this.OriginalImage.Dispose();
                this.OriginalImage = null;
            }

            if (this.PaintedImage != null)
            {
                this.PaintedImage.Dispose();
                this.PaintedImage = null;
            }  

            BO.RegionUnpacker.Counter = 0;
            SheetWithBoxes = null;

            this.UpdateTitlePc(0);

            this.ZoomPanel.Visible = false;

            unpacker = new BO.ImageUnpacker(image, fileName, MakeBackgroundTransparent && !PreservePallette);
            this.unpackers.Add(unpacker);
        }

        private void StartUnpackers()
        {
            if (this.unpackers.Count == 1)
            {
                this.HandleOneOrMoreUnpackers(null);
            }
            else
            {
                if (MessageBox.Show(String.Format("You are about to Unpack and automatically export {0} spritesheets to [{1}]. Are you sure you want to continue?", this.unpackers.Count, this.ExportLocationTextBox.Text), "Confirm multiple Unpack", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                {
                    this.ResetFormPostUnpack(null);
                    return;
                }
                this.multipleUnpackerTimer = new System.Threading.Timer(new System.Threading.TimerCallback(this.HandleOneOrMoreUnpackers), null, 0, 1000);
            }

            this.LoadingImage = true;
            this.ImageClipperAndAnimatorTimer.Start();
            this.CheckForUnpackFinishTimer.Start();
        }

        private void HandleOneOrMoreUnpackers(object state)
        {
            try
            {
                if (this.unpackers.Count == 1)
                {
                    this.unpackers[0].StartUnpacking();
                    return;
                }

                int countUnpacking = 0;
                int countUnpacked = 0;

                foreach (BO.ImageUnpacker unpacker in this.unpackers)
                {
                    if (unpacker.IsUnpacking() && !unpacker.IsUnpacked())
                    {
                        countUnpacking += 1;
                    }

                    if (unpacker.IsUnpacked())
                    {
                        countUnpacked += 1;
                    }
                }

                if (countUnpacked == this.unpackers.Count)
                {
                    bool oldPromptForDestinationFolder = PromptForDestinationFolder;
                    PromptForDestinationFolder = false;
                    this.multipleUnpackerTimer.Dispose();

                    this.ExportUnpackers(this.unpackers);
                    this.unpackers.Clear();
                    PromptForDestinationFolder = oldPromptForDestinationFolder;

                    if (AutoOpenDestinationFolder)
                    {
                        System.Diagnostics.Process.Start(this.ExportLocationTextBox.Text);
                    }
                }
                else
                {   // Ensure 2 are unpacking at one time (where multiple processors are available).
                    if (
                            (
                            (Environment.ProcessorCount > 1)
                            && (countUnpacking < 2)
                            )
                        ||  (countUnpacking < 1)
                        )
                    {
                        foreach (BO.ImageUnpacker unpacker in this.unpackers)
                        {
                            if (!unpacker.IsUnpacking() && !unpacker.IsUnpacked())
                            {
                                unpacker.StartUnpacking();
                                this.HandleOneOrMoreUnpackers(state);
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk", this);
            }
        }

        private void UpdateTitlePc(int pintPc)
        {
            string title = null;
            string pcCompleteText = "[";

            for (int k = 0; k <= 100; k += 10)
            {
                if (pintPc + 5 >= k)
                {
                    pcCompleteText += ">";
                }
                else
                {
                    pcCompleteText += "=";
                }
            }

            pcCompleteText += String.Format("] Unpacking {0}%", pintPc);
            title = String.Format(STR_FORM_TITLE, ForkandBeard.Logic.Names.GetApplicationMajorVersion(), pcCompleteText);
            this.FormTitle = title;
        }


        private void SetHoverOverlayText()
        {
	        this.SetOverlayText(
                                new List<string> {"height", "width"}
                                , new List<string>{ this.Hover.Height.ToString(), this.Hover.Width.ToString() }
                                );
        }

        private void SetFullImageOverlayText()
        {
            string colours;

            if (this.unpackers[0].ColoursCount > 999)
            {
                colours = "999+";
            }
            else
            {
                colours = this.unpackers[0].ColoursCount.ToString();
            }

            this.SetOverlayText(
                                new List<string>{
		                                            "height"
		                                            , "width"
		                                            , "colours"
		                                            , "frames"
		                                            , "selected"
	                                            }
                                , new List<string>{
		                                            this.unpackers[0].GetSize().Height.ToString()
                                                    , this.unpackers[0].GetSize().Width.ToString()
                                                    , colours
                                                    , this.Boxes.Count.ToString()
                                                    , this.Selected.Count.ToString()
	                                                }
                                );
        }

        private void SetOverlayText(List<string> labels, List<string> data)
        {
            this.OverlayText = string.Empty;
            for (int i = 0; i <= labels.Count - 1; i++)
            {
                this.OverlayText += labels[i] + ":" + new string(' ', 9 - labels[i].Length) + data[i];
                this.OverlayText += Environment.NewLine;
            }
        }

        private void SetColoursBasedOnBackground(Color colour)
        {
            float brightness = 0;

            brightness = colour.GetBrightness();

            if (brightness >= 0.66)
            {
                // Very light.
                Outline.Color = Color.FromArgb(Outline.Color.A, 0, 0, 0);
                SelectedFill.Color = Color.FromArgb(SelectedFill.Color.A, 0, 0, 75);
                HoverFill.Color = Color.FromArgb(HoverFill.Color.A, 75, 0, 0);
            }
            else if (brightness <= 0.33)
            {
                // Very dark.
                Outline.Color = Color.FromArgb(Outline.Color.A, 255, 255, 255);
                SelectedFill.Color = Color.FromArgb(SelectedFill.Color.A, 200, 200, 255);
                HoverFill.Color = Color.FromArgb(HoverFill.Color.A, 255, 200, 200);
            }
            else
            {
                int r = 0;
                int g = 0;
                int b = 0;

                if (colour.R >= colour.G)
                {
                    if (colour.B >= colour.R)
                    {
                        // Blue is dominant.
                        g = 200;
                        r = 200;
                        b = Convert.ToInt32(colour.R * 0.5f);
                        Outline.Color = Color.FromArgb(Outline.Color.A, r, g, b);
                        HoverFill.Color = Color.FromArgb(HoverFill.Color.A, Convert.ToInt32(r * 0.5f), Convert.ToInt32(g * 0.5f), Convert.ToInt32(b * 0.5f));
                        r = Convert.ToInt32(colour.G * 0.5f);
                    }
                    else
                    {
                        // Red is dominant.
                        g = 200;
                        b = 200;
                        r = Convert.ToInt32(colour.B * 0.5f);
                        Outline.Color = Color.FromArgb(Outline.Color.A, r, g, b);
                        HoverFill.Color = Color.FromArgb(HoverFill.Color.A, Convert.ToInt32(r * 0.5f), Convert.ToInt32(g * 0.5f), Convert.ToInt32(b * 0.5f));
                        b = Convert.ToInt32(colour.G * 0.5f);
                    }
                }
                else
                {
                    if (colour.B >= colour.G)
                    {
                        // Blue is dominant.
                        g = 200;
                        b = 200;
                        r = Convert.ToInt32(colour.R * 0.5f);
                        Outline.Color = Color.FromArgb(Outline.Color.A, r, g, b);
                        HoverFill.Color = Color.FromArgb(HoverFill.Color.A, Convert.ToInt32(r * 0.5f), Convert.ToInt32(g * 0.5f), Convert.ToInt32(b * 0.5f));
                        b = Convert.ToInt32(colour.G * 0.5f);
                    }
                    else
                    {
                        // Green is dominant.
                        b = 200;
                        r = 200;
                        g = Convert.ToInt32(colour.R * 0.5f);
                        Outline.Color = Color.FromArgb(Outline.Color.A, r, g, b);
                        HoverFill.Color = Color.FromArgb(HoverFill.Color.A, Convert.ToInt32(r * 0.5f), Convert.ToInt32(g * 0.5f), Convert.ToInt32(b * 0.5f));
                        r = Convert.ToInt32(colour.B * 0.5f);
                    }
                }

                SelectedFill.Color = Color.FromArgb(SelectedFill.Color.A, Convert.ToInt32(r * 0.8f), Convert.ToInt32(g * 0.8f), Convert.ToInt32(b * 0.8f));
            }
        }

        private void SplitBoxAtLocation(Rectangle box, Point location)
        {
            this.Splits.Clear();

            if (box.Width * box.Height > 1)
            {
                this.SplitTopLeft = new Rectangle(box.X, box.Y, location.X - box.X, location.Y - box.Y);
                this.Splits.Add(this.SplitTopLeft);
                this.Splits.Add(new Rectangle(location.X, box.Y, box.Right - location.X, location.Y - box.Y));
                this.Splits.Add(new Rectangle(box.X, location.Y, location.X - box.X, box.Bottom - location.Y));
                this.SplitBottomRight = new Rectangle(location.X, location.Y, box.Right - location.X, box.Bottom - location.Y);
                this.Splits.Add(this.SplitBottomRight);
                this.BoxSplitting = box;
            }
            else
            {
                this.BoxSplitting = Rectangle.Empty;
            }
        }

        public void ReloadOriginal()
        {
            if (this.unpackers.Count != 1)
            {
                return;
            }

            this.OptionsPanel.Enabled = false;
            this.Boxes.Clear();
            this.Selected.Clear();
            this.Hover = Rectangle.Empty;

            BO.RegionUnpacker.Counter = 0;
            SheetWithBoxes = null;

            this.LoadingImage = true;
            this.UpdateTitlePc(0);

            this.ZoomPanel.Visible = false;

            this.StartUnpackers();
        }

        #region " Events "

        private void frmMain_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            //TODO: Dispose all unpacker images.

            if (SheetWithBoxes != null)
            {
                SheetWithBoxes.Dispose();
            }

            if (SheetWithBoxesEnlarged != null)
            {
                SheetWithBoxesEnlarged.Dispose();
            }
        }

        private void MainPanel_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            object dropped = null;
            string[] droppedFileNames;

            if (this.LoadingImage)
            {
                return;
            }

            try
            {
                this.unpackers.Clear();
                foreach (string objFormat in e.Data.GetFormats())
                {
                    dropped = e.Data.GetData(objFormat);

                    if(
                        (dropped != null)
                        && (object.ReferenceEquals(dropped.GetType(), typeof(string[])))
                        )
                    {   // Dropped object is an array of string, so assume they are file names.
                        Bitmap image;
                        List<string> fileNames = null;
                        bool hasUserBeenPromptedToConvertFiles = false;
                        bool userOkToConvertFiles = true;

                        droppedFileNames = (string[])dropped;

                        fileNames = new List<string>(droppedFileNames);

                        foreach (string fileName in fileNames)
                        {
                            try
                            {
                                image = new Bitmap(fileName);
                                this.CreateUnpacker(image, System.IO.Path.GetFileNameWithoutExtension(fileName));
                            }
                            catch (ArgumentException)
                            {
                                string args;
                                string location;
                                Process convertProcess;
                                string tempFileName;
                                string tempFileNameAndExtension;

                                if (!hasUserBeenPromptedToConvertFiles)
                                {
                                    if (!this.SuppressThirdPartyWarningMessage)
                                    {
                                        userOkToConvertFiles = MessageBox.Show(
                                                                                String.Format(
                                                                                            @"You are trying to load a sprite sheet in a non-standard image file format. " 
                                                                                            + @"This file will be converted to a common image format first using the third party command line tool '{0}'." 
                                                                                            + @"{1}{1}Your operating system may request confirmation to execute {0}." 
                                                                                            + @"{1}{1}If you'd like to use a different conversion utility then please do so by editing the 'app.config' file found here:{1}{2}" 
                                                                                            + @"{1}{1}Do you want to continue and use '{0}'?"
                                                                                            , BO.ThirdPartyPaths.GetThirdPartyConversionToolExecutableName()
                                                                                            , Environment.NewLine
                                                                                            , AppDomain.CurrentDomain.BaseDirectory + "\\app.config"
                                                                                            )
                                                                                , "Third Party Converter Warning" 
                                                                                , MessageBoxButtons.YesNoCancel
                                                                                , MessageBoxIcon.Warning
                                                                                ) == System.Windows.Forms.DialogResult.Yes;

                                        hasUserBeenPromptedToConvertFiles = true;
                                        if (userOkToConvertFiles)
                                        {
                                            this.SuppressThirdPartyWarningMessage = true;
                                        }
                                    }
                                }

                                if (userOkToConvertFiles)
                                {
                                    BO.RegionUnpacker.DeleteAllTempFiles();

                                    tempFileName = String.Format("asu_temp_spritesheet_{0}", Guid.NewGuid().ToString().Replace("-", ""));
                                    tempFileNameAndExtension = tempFileName + System.IO.Path.GetExtension(fileName);

                                    System.IO.File.Copy(fileName, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + tempFileNameAndExtension, true);

                                    location = String.Format("\"{0}\\{1}", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), tempFileNameAndExtension);
                                    args = System.Configuration.ConfigurationManager.AppSettings["ThirdPartyImageConverterCommandArgsConvertImportToBitmap"];
                                    args = args.Replace("{temp}", location);

                                    convertProcess = System.Diagnostics.Process.Start(ThirdPartyImageConverterPath, args);
                                    convertProcess.WaitForExit();

                                    location = String.Format("{0}\\{1}.bmp", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), tempFileName);
                                    image = new Bitmap(location);
                                    this.CreateUnpacker(image, System.IO.Path.GetFileNameWithoutExtension(fileName));
                                }
                            }
                            this.DragAndDropLabel.Visible = false;
                            this.ControlsHelpLabel.Visible = false;
                        }
                        this.StartUnpackers();
                        return;
                    }
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("File is not an image file format supported by ASU. " + ex.Message, "Unable to import file");
            }
            catch (Exception ex)
            {
                ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk");
            }
        }

        private void MainPanel_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void MainPanel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    this.ToggleSelectSplitMode();
                }

                if (this.LoadingImage)
                {
                    return;
                }

                if (ModifierKeys == Keys.Control)
                {
                    this.HighlightRect = new Rectangle(e.Location.X - this.Offset.X, e.Location.Y - this.Offset.Y, 1, 1);
                }
                else if (!this.SplitFrameCheckBoxButton.Checked)
                {
                    if (this.Hover != Rectangle.Empty)
                    {
                        if (this.Selected.Contains(this.Hover))
                        {
                            this.Selected.Remove(this.Hover);
                        }
                        else
                        {
                            this.Selected.Add(this.Hover);
                        }

                        return;
                    }
                }
                else
                {
                    if (!this.BoxSplitting.IsEmpty && this.Splits.Count > 0)
                    {
                        if (this.Selected.Contains(this.BoxSplitting))
                        {
                            this.Selected.Remove(this.BoxSplitting);
                            this.Selected.AddRange(this.Splits);
                        }
                        this.Boxes.Remove(this.BoxSplitting);
                        this.Boxes.AddRange(this.Splits);
                        this.Splits.Clear();
                        SheetWithBoxes = null;
                        this.SetClickMode(false);
                        return;
                    }
                }

                this.IsMouseDown = true;
                this.MouseDownLocation = new Point(e.Location.X - this.Offset.X, e.Location.Y - this.Offset.Y);
            }
            catch (Exception ex)
            {
                ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk");
            }
        }

        private void MainPanel_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                if (this.LoadingImage)
                {
                    return;
                }

                Point location;
                this.MainPanel.Cursor = Cursors.Default;

                location = new Point(e.Location.X - this.Offset.X, e.Location.Y - this.Offset.Y);

                if (this.IsMouseDown)
                {
                    if (ModifierKeys == Keys.Control)
                    {
                        this.HighlightRect = new Rectangle(MouseDownLocation.X, MouseDownLocation.Y, location.X - MouseDownLocation.X, location.Y - MouseDownLocation.Y);

                        // Make sure the width and height are not negative.
                        if (this.HighlightRect.Width < 0)
                        {
                            this.HighlightRect.Width = Math.Abs(this.HighlightRect.Width);
                            this.HighlightRect.X = MouseDownLocation.X - HighlightRect.Width;
                        }
                        if (this.HighlightRect.Height < 0)
                        {
                            this.HighlightRect.Height = Math.Abs(this.HighlightRect.Height);
                            this.HighlightRect.Y = MouseDownLocation.Y - HighlightRect.Height;
                        }
                    }
                    else
                        this.Offset = new Point(e.Location.X - this.MouseDownLocation.X, e.Location.Y - this.MouseDownLocation.Y);
                }
                else
                {
                    this.Hover = Rectangle.Empty;

                    foreach (Rectangle box in this.Boxes)
                    {
                        if (box.Contains(location))
                        {
                            if (this.SplitFrameCheckBoxButton.Checked)
                            {
                                this.MainPanel.Cursor = Cursors.Cross;
                                this.SplitBoxAtLocation(box, location);
                            }
                            else
                            {
                                this.Hover = box;
                            }
                            break; // TODO: might not be correct. Was : Exit For
                        }
                        else
                        {
                            this.Splits.Clear();
                        }
                    }

                    if (this.unpackers.Count > 0 && this.AreAllUnpacked())
                    {
                        if (this.Hover.IsEmpty)
                        {
                            this.SetFullImageOverlayText();
                        }
                        else
                        {
                            this.SetHoverOverlayText();
                        }
                    }

                    this.MouseLocation = e.Location;
                }
                this.Refresh();
                this.ZoomPanel.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error during mouse over");
            }
        }

        private void MainPanel_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.IsMouseDown = false;

            if (this.HighlightRect != Rectangle.Empty)
            {
                this.Selected.Clear();

                foreach (Rectangle box in this.Boxes)
                {
                    if (box.IntersectsWith(this.HighlightRect))
                    {
                        this.Selected.Add(box);
                    }
                }
            }

            this.HighlightRect = Rectangle.Empty;
        }

        private void MainPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics graphics = null;
            Graphics zoomGraphics = null;
            bool refresh = false;

            try
            {
                if (this.LoadingImage)
                {
                    return;
                }

                if (
                    (this.unpackers.Count == 1)
                    && (OriginalImage == null)
                    )
                {
                    OriginalImage = this.unpackers[0].GetOriginalClone();
                }

                if (OriginalImage != null)
                {
                    if (SheetWithBoxes == null)
                    {
                        Graphics boxGraphics = null;
                        try
                        {
                            SheetWithBoxes = new Bitmap(OriginalImage);
                            boxGraphics = Graphics.FromImage(SheetWithBoxes);
                            boxGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

                            Pen zoomOutline;
                            zoomOutline = new Pen(Outline.Color, 1);
                            SheetWithBoxesEnlarged = new Bitmap(OriginalImage);
                            zoomGraphics = Graphics.FromImage(SheetWithBoxesEnlarged);

                            foreach (Rectangle box in this.Boxes)
                            {
                                boxGraphics.DrawRectangle(Outline, box);
                                zoomGraphics.DrawRectangle(zoomOutline, box);
                            }
                        }
                        finally
                        {
                            if (boxGraphics != null)
                            {
                                boxGraphics.Dispose();
                            }
                        }
                    }

                    if (PaintedImage == null)
                    {
                        refresh = true;
                    }
                    else if (
                                (PaintedImage != null)
                                && (
                                    this.MainPanel.ClientRectangle.Width != PaintedImage.Width 
                                    || this.MainPanel.ClientRectangle.Height != PaintedImage.Height
                                    )
                            )
                    {   
                        refresh = true;
                    }

                    if (refresh)
                    {
                        if (PaintedImage != null)
                        {
                            PaintedImage.Dispose();
                        }
                        PaintedImage = new Bitmap(this.MainPanel.ClientRectangle.Width, this.MainPanel.ClientRectangle.Height);
                    }

                    graphics = Graphics.FromImage(PaintedImage);
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    graphics.Clear(this.unpackers[0].CreateOuterBackgroundColour());
                    graphics.DrawImage(SheetWithBoxes, this.Offset);

                    Rectangle boxOffset;

                    if (this.Splits.Count > 0)
                    {
                        boxOffset = this.SplitTopLeft;
                        boxOffset.Offset(this.Offset);
                        graphics.FillRectangle(HoverFill, boxOffset);

                        boxOffset = this.SplitBottomRight;
                        boxOffset.Offset(this.Offset);
                        graphics.FillRectangle(HoverFill, boxOffset);
                    }

                    if (this.Hover != Rectangle.Empty)
                    {
                        boxOffset = this.Hover;
                        boxOffset.Offset(this.Offset);
                        graphics.FillRectangle(HoverFill, boxOffset);
                    }

                    foreach (Rectangle selectedBox in this.Selected)
                    {
                        boxOffset = selectedBox;
                        boxOffset.Offset(this.Offset);
                        graphics.FillRectangle(SelectedFill, boxOffset);
                    }

                    if (this.HighlightRect != Rectangle.Empty)
                    {
                        Rectangle highlightFixed = this.HighlightRect;
                        highlightFixed.X += this.Offset.X;
                        highlightFixed.Y += this.Offset.Y;

                        graphics.DrawRectangle(Outline, highlightFixed);
                    }

                    if (!this.IsMouseDown)
                    {
                        this.ZoomImage = new Bitmap(20, 20);

                        using (Graphics g = Graphics.FromImage(this.ZoomImage))
                        {
                            g.DrawImage(PaintedImage, 0, 0, new Rectangle(this.MouseLocation.X - 10, this.MouseLocation.Y - 10, 20, 20), GraphicsUnit.Pixel);
                        }

                        if (this.ZoomPanel.BackgroundImage != null)
                        {
                            this.ZoomPanel.BackgroundImage.Dispose();
                        }
                        this.ZoomPanel.BackgroundImage = BO.ImageScaler.IncreaseScale(this.ZoomImage, 4);
                        this.ZoomImage.Dispose();
                    }

                    e.Graphics.DrawImage(PaintedImage, new Point(0, 0));

                    if (Outline.Color.GetBrightness() >= 0.5)
                    {
                        using (SolidBrush overlayBrushShadow = new SolidBrush(Color.FromArgb(Convert.ToInt32(Outline.Color.G / 2), Convert.ToInt32(Outline.Color.B / 2), Convert.ToInt32(Outline.Color.R / 2))))
                        {
                            e.Graphics.DrawString(this.OverlayText, this.OverlayFontLabel.Font, overlayBrushShadow, new Point(this.Width - 109, 6));
                        }
                    }
                    else
                    {
                        using (SolidBrush overlayBrushShadow = new SolidBrush(Color.FromArgb(Math.Min(255, Convert.ToInt32(Outline.Color.G * 1.5)), Math.Min(255, Convert.ToInt32(Outline.Color.B * 1.5)), Math.Min(255, Convert.ToInt32(Outline.Color.R * 1.5)))))
                        {
                            e.Graphics.DrawString(this.OverlayText, this.OverlayFontLabel.Font, overlayBrushShadow, new Point(this.Width - 109, 6));
                        }
                    }

                    using (SolidBrush overlayBrush = new SolidBrush(Outline.Color))
                    {
                        e.Graphics.DrawString(this.OverlayText, this.OverlayFontLabel.Font, overlayBrush, new Point(this.Width - 110, 5));
                    }
                }
            }
            catch (Exception ex)
            {
                ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk");
            }
            finally
            {
                if (graphics != null)
                {
                    graphics.Dispose();
                }
                if (zoomGraphics != null)
                {
                    zoomGraphics.Dispose();
                }
            }
        }

        private int GetNearestWhole(int pintValue, int pintIncrement)
        {
            return Convert.ToInt32(pintValue / pintIncrement) * pintIncrement;
        }

        private void MainForm_Resize(object sender, System.EventArgs e)
        {
            this.MainPanel.Refresh();
        }

        private void CombineButton_Click(System.Object sender, System.EventArgs e)
        {
            Rectangle newBox = Rectangle.Empty;
            try
            {
                if (this.unpackers.Count != 1)
                {
                    MessageBox.Show("No spritesheet loaded. Please drop a spritesheet onto this form and select some frames before trying to combine frames.");
                    return;
                }
                else
                {
                    if (this.Selected.Count == 0)
                    {
                        MessageBox.Show("No frames selected. Please select some frames before trying to combine.");
                        return;
                    }
                }

                foreach (Rectangle box in this.Selected)
                {
                    if (newBox == Rectangle.Empty)
                    {
                        newBox = box;
                    }
                    else
                    {
                        if (box.Right > newBox.Right)
                        {
                            newBox.Width = box.Right - newBox.Left;
                        }

                        if (box.Left < newBox.Left)
                        {
                            newBox.Width += newBox.Left - box.Left;
                        }

                        if (box.Bottom > newBox.Bottom)
                        {
                            newBox.Height = box.Bottom - newBox.Top;
                        }

                        if (box.Top < newBox.Top)
                        {
                            newBox.Height += newBox.Top - box.Top;
                        }

                        newBox.X = Math.Min(newBox.X, box.X);
                        newBox.Y = Math.Min(box.Y, newBox.Y);
                    }

                    this.Boxes.Remove(box);
                }

                List<Rectangle> toRemove = new List<Rectangle>();

                foreach (Rectangle box in this.Boxes)
                {
                    if (box.IntersectsWith(newBox))
                    {
                        toRemove.Add(box);
                    }
                }

                foreach (Rectangle remove in toRemove)
                {
                    this.Boxes.Remove(remove);
                }

                this.Selected.Clear();
                this.Selected.Add(newBox);
                this.Boxes.Add(newBox);
                SheetWithBoxes = null;
                this.Refresh();
            }
            catch (Exception ex)
            {
                ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk");
            }
        }

        private void ExportUnpackers(List<BO.ImageUnpacker> unpackers)
        {
            string args = null;
            List<string> tempFiles = new List<string>();
            string outpath = null;
            bool hasUserBeenPromptedToConvertFiles = false;
            bool userOkToConvertFiles = true;
            List<Rectangle> boxes = null;
            DateTime lapse = System.DateTime.MinValue;
            System.Windows.Forms.DialogResult folderResponse = System.Windows.Forms.DialogResult.Cancel;

            try
            {
                if (PromptForDestinationFolder)
                {
                    this.FolderBrowserDialog1.SelectedPath = this.ExportLocationTextBox.Text;
                }

                if (this.Selected.Count > 0 || unpackers.Count > 1)
                {
                    if (PromptForDestinationFolder)
                    {
                        folderResponse = this.FolderBrowserDialog1.ShowDialog();
                    }

                    foreach (BO.ImageUnpacker unpacker in unpackers)
                    {
                        if (
                            (folderResponse == System.Windows.Forms.DialogResult.OK) 
                            || (!PromptForDestinationFolder)
                            )
                        {
                            if (PromptForDestinationFolder)
                            {
                                this.ExportLocationTextBox.Text = this.FolderBrowserDialog1.SelectedPath;
                                outpath = this.FolderBrowserDialog1.SelectedPath;
                            }

                            outpath = this.ExportLocationTextBox.Text;

                            Bitmap original;

                            original = unpacker.GetOriginalClone();

                            if (!string.IsNullOrEmpty(ExportNConvertArgs))
                            {
                                if (!this.SuppressThirdPartyWarningMessage)
                                {
                                    if (!hasUserBeenPromptedToConvertFiles)
                                    {
                                        userOkToConvertFiles = MessageBox.Show(
                                                                                String.Format("You are about to export an advanced image file format." 
                                                                                                + "This file will be converted to a to common file format first and then converted to the advanced image file format by the third party command line tool '{0}'." 
                                                                                                + "{1}{1}Your operating system may request confirmation to execute {0}." 
                                                                                                + "{1}{1}If you'd like to use a different conversion utility then please do so by editing the 'app.config' file found here:{1}{2}" 
                                                                                                + "{1}{1}Do you want to continue and use'{0}'?"
                                                                                                , BO.ThirdPartyPaths.GetThirdPartyConversionToolExecutableName()
                                                                                                , Environment.NewLine
                                                                                                , AppDomain.CurrentDomain.BaseDirectory + "\\app.config")
                                                                                , "Third Party Converter Warning"
                                                                                , MessageBoxButtons.YesNoCancel
                                                                                , MessageBoxIcon.Warning
                                                                                ) == System.Windows.Forms.DialogResult.Yes;

                                        hasUserBeenPromptedToConvertFiles = true;
                                    }

                                    if (!userOkToConvertFiles)
                                    {
                                        return;
                                    }
                                }
                                else
                                {
                                    this.SuppressThirdPartyWarningMessage = true;
                                }
                            }

                            if (unpackers.Count > 1)
                            {
                                outpath = System.IO.Path.Combine(this.ExportLocationTextBox.Text, unpacker.FileName);
                                System.IO.Directory.CreateDirectory(outpath);
                            }

                            int preFileCount = 0;

                            preFileCount = System.IO.Directory.GetFiles(outpath).Length;

                            if (unpackers.Count > 1)
                            {
                                boxes = unpacker.GetBoxes();
                                if (this.Options == null)
                                {
                                    boxes = BO.ImageUnpacker.OrderBoxes(boxes, Enums.SelectAllOrder.TopLeft, unpacker.GetSize());
                                }
                                else
                                {
                                    boxes = BO.ImageUnpacker.OrderBoxes(boxes, (Enums.SelectAllOrder)this.Options.SelectAllOrderComboBox.SelectedIndex, unpacker.GetSize());
                                }
                            }
                            else
                            {
                                boxes = this.Selected;
                            }

                            if (lapse != System.DateTime.MaxValue)
                            {
                                lapse = System.DateTime.Now;
                            }

                            for (int k = 0; k <= boxes.Count - 1; k++)
                            {

                                if (!boxes[k].IsEmpty)
                                {
                                    Bitmap bitmap = new Bitmap(boxes[k].Width, boxes[k].Height, original.PixelFormat);

                                    using (Graphics objGraphics = Graphics.FromImage(bitmap))
                                    {
                                        objGraphics.DrawImage(original, new Rectangle(0, 0, bitmap.Width, bitmap.Height), boxes[k], GraphicsUnit.Pixel);
                                        objGraphics.Dispose();
                                    }

                                    if (PreservePallette)
                                    {
                                        if (unpacker.GetPallette() != null)
                                        {
                                            ImageQuantizers.PaletteQuantizer quantizer = default(ImageQuantizers.PaletteQuantizer);
                                            Bitmap quantized = default(Bitmap);
                                            quantizer = new ImageQuantizers.PaletteQuantizer(new System.Collections.ArrayList(unpacker.GetPallette().Entries));
                                            quantized = quantizer.Quantize(bitmap);
                                            bitmap.Dispose();
                                            bitmap = quantized;
                                        }
                                    }

                                    if (MakeBackgroundTransparent)
                                    {
                                        bitmap.MakeTransparent(unpacker.GetBackgroundColour());
                                    }

                                    if (string.IsNullOrEmpty(ExportNConvertArgs))
                                    {
                                        bitmap.Save(String.Format("{0}\\{1}.{2}", outpath, k.ToString(), ExportFormat.ToString().ToLower()), ExportFormat);
                                    }
                                    else
                                    {
                                        string tempBitmapPath = null;
                                        System.Diagnostics.ProcessStartInfo startInfo = null;
                                        tempBitmapPath = String.Format("{0}\\{1}.png", outpath, k.ToString());
                                        tempFiles.Add(tempBitmapPath);
                                        bitmap.Save(tempBitmapPath, System.Drawing.Imaging.ImageFormat.Png);
                                        args = ExportNConvertArgs.Replace("{file_name}", String.Format("\"{0}\\{1}.png\"", outpath, k.ToString()));

                                        startInfo = new System.Diagnostics.ProcessStartInfo(ThirdPartyImageConverterPath, args);
                                        startInfo.CreateNoWindow = true;
                                        startInfo.UseShellExecute = true;
                                        using (System.Diagnostics.Process objProcess = System.Diagnostics.Process.Start(startInfo))
                                        {
                                            objProcess.WaitForExit();
                                            if (System.DateTime.Now.Subtract(lapse).TotalSeconds > 10)
                                            {
                                                if (MessageBox.Show("Export of frames is taking a while. Do you want to abort?", "Execessive Export Time", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                                                {
                                                    if (original != null)
                                                    {
                                                        original.Dispose();
                                                    }
                                                    return;
                                                }
                                                lapse = System.DateTime.MaxValue;
                                            }
                                        }
                                    }

                                    bitmap.Dispose();
                                }
                            }

                            if (original != null)
                            {
                                original.Dispose();
                            }

                            if (tempFiles.Count > 0)
                            {
                                List<string> notConverted = new List<string>();

                                foreach (string tempFile in tempFiles)
                                {
                                    if (System.IO.File.Exists(tempFile))
                                    {
                                        notConverted.Add(tempFile);
                                    }
                                    System.IO.File.Delete(tempFile);
                                }

                                if (notConverted.Count > 0 && System.IO.Directory.GetFiles(outpath).Length < (preFileCount + (tempFiles.Count * 2)))
                                {
                                    MessageBox.Show(String.Format("Exported files failed to be converted.{3}{3}Arguments used:{3}{0}{3}{3}Please see '{1}' documentation at {3}[{2}].", ExportNConvertArgs, BO.ThirdPartyPaths.GetThirdPartyConversionToolExecutableName(), BO.ThirdPartyPaths.GetThirdPartyConversionToolDirectory(), Environment.NewLine));
                                    return;
                                }
                            }

                            if (AutoOpenDestinationFolder && unpackers.Count == 1)
                            {
                                System.Diagnostics.Process.Start(outpath);
                            }
                        }
                    }
                }
                else
                {
                    if (this.unpackers.Count != 1)
                    {
                        MessageBox.Show("No spritesheet loaded. Please drop a spritesheet onto this form and select some frames before exporting.");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("No frames selected. Please select some frames before exporting.");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured whilst exporting frames.", ex);
            }
        }

        private void ExportButton_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                this.ExportUnpackers(this.unpackers);
            }
            catch (Exception ex)
            {
                ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk");
            }
        }

        private void ZoomPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            try
            {
                if (!this.SplitFrameCheckBoxButton.Checked)
                {
                    e.Graphics.DrawLine(ZoomPen, 0, (this.ZoomPanel.ClientRectangle.Height / 2f) + 2, this.ZoomPanel.ClientRectangle.Width, (this.ZoomPanel.ClientRectangle.Height / 2f) + 2);
                    e.Graphics.DrawLine(ZoomPen, (this.ZoomPanel.ClientRectangle.Width / 2f) + 2, 0, (this.ZoomPanel.ClientRectangle.Width / 2f) + 2, this.ZoomPanel.ClientRectangle.Height);
                }
            }
            catch (Exception sorry)
            {
            }
        }

        private void UndoButton_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (this.unpackers.Count == 0)
                {
                    MessageBox.Show("No image loaded. First drag an image onto the form above, or use the paste 'From Clipboard' button.");
                    return;
                }

                this.ReloadOriginal();
            }
            catch (Exception ex)
            {
                ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk");
            }
        }

        private void SelectAllButton_Click(System.Object sender, System.EventArgs e)
        {
            Enums.SelectAllOrder selectOrder;

            try
            {
                if (this.unpackers.Count == 0)
                {
                    MessageBox.Show("No image loaded. First drag an image onto the form above, or use the paste 'From Clipboard' button.");
                    return;
                }

                this.Selected.Clear();
                if (this.Options == null)
                {
                    selectOrder = Enums.SelectAllOrder.TopLeft;                    
                }
                else
                {
                    selectOrder = (Enums.SelectAllOrder)this.Options.SelectAllOrderComboBox.SelectedIndex;
                }

                if (this.unpackers.Count == 1)
                {
                    this.Selected = BO.ImageUnpacker.OrderBoxes(this.Boxes, selectOrder, this.unpackers[0].GetSize());
                }

                this.SetFullImageOverlayText();
                this.Refresh();
            }
            catch (Exception ex)
            {
                ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk");
            }
        }

        private void DeSelectAllButton_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (this.unpackers.Count == 0)
                {
                    MessageBox.Show("No image loaded. First drag an image onto the form above, or use the paste 'From Clipboard' button.");
                    return;
                }

                this.Selected.Clear();
                this.SetFullImageOverlayText();
                this.Refresh();
            }
            catch (Exception ex)
            {
                ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk");
            }
        }

        private void PasteButton_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (Clipboard.ContainsImage())
                {
                    this.unpackers.Clear();
                    this.DragAndDropLabel.Visible = false;
                    this.ControlsHelpLabel.Visible = false;
                    this.CreateUnpacker(new Bitmap(Clipboard.GetImage()), "clipboard");
                    this.StartUnpackers();
                }
                else
                {
                    MessageBox.Show("No image found in Clipboard");
                }
            }
            catch (Exception ex)
            {
                ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk");
            }
        }

        private void OptionsButton_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (this.Options == null || this.Options.IsDisposed)
                {
                    this.Options = new OptionsForm();
                    this.Options.Main = this;
                    this.Options.PromptDestinationFolderCheckBox.Checked = PromptForDestinationFolder;
                    this.Options.OpenExportedDestinationCheckBox.Checked = AutoOpenDestinationFolder;
                }

                this.Options.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading options");
            }
        }

        private bool AreAllUnpacked()
        {
            int unpackedCount = 0;

            foreach (BO.ImageUnpacker unpacker in this.unpackers)
            {
                if (unpacker.IsUnpacked())
                {
                    unpackedCount += 1;
                }
            }

            return unpackedCount == this.unpackers.Count;
        }

        private void ResetFormPostUnpack(BO.ImageUnpacker unpacker)
        {
            if (unpacker != null)
            {
                this.Boxes = unpacker.GetBoxes();
            }
            else
            {
                this.Boxes = new List<Rectangle>();
            }

            this.MainPanel.Refresh();
            this.CheckForUnpackFinishTimer.Stop();
            this.ImageClipperAndAnimatorTimer.Stop();
            this.MainPanel.BackgroundImage = null;
            this.OptionsPanel.Enabled = true;
            this.ZoomPanel.Visible = this.unpackers.Count == 1;
            this.HyperModeUnpackingLabel.Visible = false;
            this.HyperModeUnpacking0Label.Visible = false;
            this.HyperModeUnpacking1Label.Visible = false;

            this.Offset = new Point(0, 0);
            this.Text = String.Format(STR_FORM_TITLE, ForkandBeard.Logic.Names.GetApplicationMajorVersion(), "");
            this.LoadingImage = false;

            if (this.unpackers.Count > 1 || this.unpackers.Count == 0)
            {
                this.DragAndDropLabel.Visible = true;
                this.ControlsHelpLabel.Visible = true;
                this.MainPanel.BackColor = Color.FromArgb(224, 224, 224);
                this.SetOverlayText(new List<string>(), new List<string>());
            }
            else
            {
                this.SetFullImageOverlayText();
            }

            this.MainPanel.Refresh();
        }

        private void CheckForUnpackFinishTimer_Tick(object sender, System.EventArgs e)
        {
            BO.ImageUnpacker unpacker;

            try
            {
                if (this.unpackers.Count == 1)
                {
                    unpacker = this.unpackers[0];
                }
                else
                {
                    int counter = 0;
                    do
                    {
                        unpacker = this.unpackers[this.Random.Next(0, this.unpackers.Count)];
                        counter += 1;
                    } while ((unpacker.IsUnpacked() | !unpacker.IsUnpacking()) & counter < 100);
                }

                if (unpacker.IsBackgroundColourSet())
                {
                    this.SetColoursBasedOnBackground(unpacker.GetBackgroundColour());
                    this.MainPanel.BackColor = unpacker.GetBackgroundColour();
                }

                if (this.AreAllUnpacked())
                {
                    this.ResetFormPostUnpack(unpacker);
                }
            }
            catch (Exception ex)
            {
                CheckForUnpackFinishTimer.Stop();
                MessageBox.Show(ex.Message + ". CheckForUnpackFinishTimer has been stopped, you may need to restart application", "Error during timer");
            }
        }

        private void ImageClipperAndAnimatorTimer_BalancedTock()
        {
            BO.ImageUnpacker unpacker;
            int pcComplete = 0;
            Bitmap original = null;
            Bitmap backgroundImage = null;
            Graphics graphics = null;
            try
            {
                if (this.unpackers.Count == 1)
                {
                    unpacker = this.unpackers[0];
                    pcComplete = unpacker.GetPcComplete();
                }
                else
                {
                    int counter = 0;
                    do
                    {
                        unpacker = this.unpackers[this.Random.Next(0, this.unpackers.Count)];
                        counter += 1;
                    } while ((unpacker.IsUnpacked() | !unpacker.IsUnpacking()) & counter < 100);

                    foreach (BO.ImageUnpacker item in this.unpackers)
                    {
                        pcComplete += item.GetPcComplete();
                    }

                    pcComplete = Convert.ToInt32(pcComplete / this.unpackers.Count);
                }

                if (!unpacker.IsUnpacked())
                {
                    this.UpdateTitlePc(pcComplete);
                    this.HyperModeUnpackingLabel.Visible = false;
                    this.HyperModeUnpacking0Label.Visible = false;
                    this.HyperModeUnpacking1Label.Visible = false;

                    if (unpacker.IsLarge)
                    {
                        this.MainPanel.Refresh();
                        this.MainPanel.BackgroundImage = null;
                        this.HyperModeUnpackingLabel.Visible = true;
                        this.HyperModeUnpacking1Label.Visible = true;
                        this.HyperModeUnpacking0Label.Visible = true;
                        this.HyperModeUnpacking0Label.ForeColor = Color.FromArgb(this.Random.Next(50, 150), this.Random.Next(0, 10), this.Random.Next(0, 100));
                        this.HyperModeUnpackingLabel.ForeColor = Color.FromArgb(this.Random.Next(150, 160), this.Random.Next(0, 100), this.Random.Next(100, 200));
                        this.HyperModeUnpacking1Label.ForeColor = Color.FromArgb(this.Random.Next(200, 256), this.Random.Next(200, 256), this.Random.Next(200, 210));
                    }
                    else
                    {
                        Rectangle randomRectangle;
                        
                        original = unpacker.GetOriginalClone();                        
                        randomRectangle = new Rectangle(0, 0, this.Random.Next(2, Convert.ToInt32(original.Width * 0.4f)), this.Random.Next(2, Convert.ToInt32(original.Height * 0.4f)));
                        backgroundImage = new Bitmap(randomRectangle.Width, randomRectangle.Height);
                        randomRectangle.X = this.Random.Next(0, Convert.ToInt32(original.Width - randomRectangle.Width));
                        randomRectangle.Y = this.Random.Next(0, Convert.ToInt32(original.Height - randomRectangle.Height));

                        graphics = Graphics.FromImage(backgroundImage);
                        graphics.DrawImage(original, 0, 0, randomRectangle, GraphicsUnit.Pixel);

                        this.MainPanel.BackgroundImage = backgroundImage;                        
                    }

                    this.Text = this.FormTitle;

                    lock ((BO.RegionUnpacker.Wait))
                    {
                        System.Threading.Monitor.PulseAll(BO.RegionUnpacker.Wait);
                    }
                }
            }
            catch (Exception ex)
            {
                ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk", this);
            }
            finally
            {
                if (original != null)
                {
                    original.Dispose();
                }

                if (graphics != null)
                {
                    graphics.Dispose();
                }
            }
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {

            if (keyData == Keys.Escape)
            {
                if (this.unpackers.Count == 1 && this.AreAllUnpacked())
                {
                    this.unpackers.Clear();
                    this.ResetFormPostUnpack(null);
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }

            Rectangle panelRectangle;
            Point cursor;

            cursor = Cursor.Position;
            panelRectangle = this.MainPanel.ClientRectangle;

            panelRectangle.X = this.Left;
            panelRectangle.Y = this.Top + (this.Height - this.MainPanel.Height - this.OptionsPanel.Height);

            if (panelRectangle.Contains(cursor))
            {
                switch (keyData)
                {
                    case Keys.Left:
                        Cursor.Position = new Point(Cursor.Position.X - 1, Cursor.Position.Y);
                        return true;
                    case Keys.Right:
                        Cursor.Position = new Point(Cursor.Position.X + 1, Cursor.Position.Y);
                        return true;
                    case Keys.Up:
                        Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y - 1);
                        return true;
                    case Keys.Down:
                        Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y + 1);
                        return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        private void SetClickMode(bool isSplit)
        {
            if (this.SplitFrameCheckBoxButton.Checked != isSplit)
            {
                this.ToggleSelectSplitMode();
            }
        }

        private void ToggleSelectSplitMode()
        {
            this.SplitFrameCheckBoxButton.Checked = !this.SplitFrameCheckBoxButton.Checked;
            this.SetCurrentSelectSplitModeTextAndIcon();
        }

        private void SetCurrentSelectSplitModeTextAndIcon()
        {
            if (this.SplitFrameCheckBoxButton.Checked)
            {
                this.SplitFrameCheckBoxButton.Image = global::ASU.Properties.Resources.cursor;
                this.SplitFrameCheckBoxButton.Text = "Select";
            }
            else
            {
                this.SplitFrameCheckBoxButton.Image = global::ASU.Properties.Resources.cut;
                this.SplitFrameCheckBoxButton.Text = "Split";
            }
        }

        private void ClickModeCheckBoxButton_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            this.SetCurrentSelectSplitModeTextAndIcon();
        }
    }
}
