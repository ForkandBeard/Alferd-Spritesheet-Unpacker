using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ASU.UI
{
    public partial class OptionsForm : Form
    {
        private const string STR_FORM_TITLE = "Alferd Spritesheet Unpacker ver.{0} Options";
        private const string STR_ADVANCED_EXPORT_FILE_FORMAT = "<Advanced>";

        public MainForm Main;
        private void cmdSelectedColour_Click(System.Object sender, System.EventArgs e)
        {
            this.ColorDialog1.Color = this.pnlSelectedColour.BackColor;
            this.ColorDialog1.ShowDialog();
            this.pnlSelectedColour.BackColor = Color.FromArgb(200, this.ColorDialog1.Color);
        }

        private void cmdTileBorderColour_Click(System.Object sender, System.EventArgs e)
        {
            this.ColorDialog1.Color = this.pnlTileBorderColour.BackColor;
            this.ColorDialog1.ShowDialog();
            this.pnlTileBorderColour.BackColor = this.ColorDialog1.Color;
        }

        private void cmdHoverColour_Click(System.Object sender, System.EventArgs e)
        {
            this.ColorDialog1.Color = this.pnlHoverColour.BackColor;
            this.ColorDialog1.ShowDialog();
            this.pnlHoverColour.BackColor = Color.FromArgb(150, this.ColorDialog1.Color);
        }

        private void cmdClose_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (this.Main != null)
                {
                    bool blnReloadNeeded = false;

                    MainForm.SelectedFill = new SolidBrush(Color.FromArgb(MainForm.SelectedFill.Color.A, this.pnlSelectedColour.BackColor));
                    MainForm.ZoomPen = new Pen(Color.FromArgb(MainForm.ZoomPen.Color.A, this.pnlSelectedColour.BackColor), MainForm.ZoomPen.Width);
                    MainForm.HoverFill = new SolidBrush(Color.FromArgb(MainForm.HoverFill.Color.A, this.pnlHoverColour.BackColor));
                    MainForm.Outline = new Pen(Color.FromArgb(MainForm.Outline.Color.A, this.pnlTileBorderColour.BackColor), (float)this.ctlOutlineWidth.Value);
                    MainForm.PromptForDestinationFolder = this.chkPromptDestinationFolder.Checked;
                    MainForm.AutoOpenDestinationFolder = this.chkOpenExportedDestination.Checked;
                    MainForm.MakeBackgroundTransparent = this.chkExportBGTransparent.Checked;

                    if (this.cboExportFormat.Text != STR_ADVANCED_EXPORT_FILE_FORMAT)
                    {
                        MainForm.ExportNConvertArgs = string.Empty;
                        MainForm.ExportFormat = (System.Drawing.Imaging.ImageFormat)this.cboExportFormat.SelectedItem;
                    }
                    else
                    {
                        MainForm.ExportFormat = null;
                        MainForm.ExportNConvertArgs = this.CreateCommandLineArgs();
                    }

                    if (MainForm.DistanceBetweenTiles != this.ctlDistanceBetweenTiles.Value)
                    {
                        MainForm.DistanceBetweenTiles = Convert.ToInt32(this.ctlDistanceBetweenTiles.Value);
                        blnReloadNeeded = true;
                    }

                    MainForm.SheetWithBoxes = null;
                    if (blnReloadNeeded)
                    {
                        this.Main.ReloadOriginal();
                    }
                    else
                    {
                        this.Main.pnlMain.Refresh();
                    }
                }
                this.Hide();
            }
            catch (Exception ex)
            {
                ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk");
            }
        }

        private void frmOptions_Load(object sender, System.EventArgs e)
        {
            try
            {
                this.Text = String.Format(STR_FORM_TITLE, ForkandBeard.Logic.Names.GetApplicationMajorVersion());
                this.lnkCommandLineHelp.Text = String.Format("{0} command line help file", System.IO.Path.GetFileName(System.Configuration.ConfigurationManager.AppSettings["ThirdPartyImageConverter"]));

                if (this.cboSelectAllOrder.SelectedIndex == -1)
                {
                    this.cboSelectAllOrder.SelectedIndex = 0;
                }

                this.cboExportFormat.Items.Clear();
                this.cboExportFormat.Items.Add(System.Drawing.Imaging.ImageFormat.Png);
                this.cboExportFormat.Items.Add(System.Drawing.Imaging.ImageFormat.Bmp);
                this.cboExportFormat.Items.Add(System.Drawing.Imaging.ImageFormat.Gif);
                this.cboExportFormat.Items.Add(System.Drawing.Imaging.ImageFormat.Tiff);
                this.cboExportFormat.Items.Add(System.Drawing.Imaging.ImageFormat.Jpeg);
                this.cboExportFormat.Items.Add(STR_ADVANCED_EXPORT_FILE_FORMAT);
                this.lblCommandLine.Text = this.CreateCommandLineArgs();

                if (this.Main != null)
                {
                    this.chkExportBGTransparent.Checked = MainForm.MakeBackgroundTransparent;
                    this.pnlSelectedColour.BackColor = MainForm.SelectedFill.Color;
                    this.pnlTileBorderColour.BackColor = MainForm.Outline.Color;
                    this.pnlHoverColour.BackColor = MainForm.HoverFill.Color;
                    this.ctlDistanceBetweenTiles.Value = MainForm.DistanceBetweenTiles;
                    this.ctlOutlineWidth.Value = Convert.ToDecimal(MainForm.Outline.Width);
                    if (MainForm.ExportFormat != null)
                    {
                        this.cboExportFormat.SelectedItem = MainForm.ExportFormat;
                    }
                    else
                    {
                        this.cboExportFormat.Text = STR_ADVANCED_EXPORT_FILE_FORMAT;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error during option load");
            }
        }

        private void cmdAbout_Click(System.Object sender, System.EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void cboExportFormat_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.txtNConvertArgs.Enabled = this.cboExportFormat.Text == STR_ADVANCED_EXPORT_FILE_FORMAT;
            this.chkShowCommandLineArgs.Enabled = this.txtNConvertArgs.Enabled;
            this.lnkCommandLineHelp.Visible = this.txtNConvertArgs.Enabled && this.chkShowCommandLineArgs.Checked;
            this.lblCommandLine.Visible = this.txtNConvertArgs.Enabled && this.chkShowCommandLineArgs.Checked;

            if (this.cboExportFormat.Text == STR_ADVANCED_EXPORT_FILE_FORMAT)
            {
                this.chkExportBGTransparent.Checked = false;
            }
            else
            {
                this.chkShowCommandLineArgs.Checked = false;
            }
        }

        private void txtNConvertArgs_TextChanged(System.Object sender, System.EventArgs e)
        {
            this.lblCommandLine.Text = this.CreateCommandLineArgs();
        }

        private string CreateCommandLineArgs()
        {
            string strReturn = null;

            strReturn = System.Configuration.ConfigurationManager.AppSettings["ThirdPartyImageConverterCommandArgsExportFormat"].Replace("{text}", this.txtNConvertArgs.Text);

            return strReturn;
        }

        private void chkShowCommandLineArgs_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            this.lblCommandLine.Visible = this.chkShowCommandLineArgs.Checked;
            this.lnkCommandLineHelp.Visible = this.chkShowCommandLineArgs.Checked;

            if (this.chkShowCommandLineArgs.Checked)
            {
                this.pnlExport.Height += 91;
                this.Height += 89;
            }
            else
            {
                this.pnlExport.Height -= 91;
                this.Height -= 89;
            }
        }

        private void lnkCommandLineHelp_LinkClicked(System.Object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            string strThirdPartyImageConverterHelpFile = null;

            strThirdPartyImageConverterHelpFile = System.Configuration.ConfigurationManager.AppSettings["ThirdPartyImageConverterHelpFile"];
            if (strThirdPartyImageConverterHelpFile.StartsWith("\\"))
            {
                strThirdPartyImageConverterHelpFile = AppDomain.CurrentDomain.BaseDirectory + strThirdPartyImageConverterHelpFile;
            }
            System.Diagnostics.Process.Start(strThirdPartyImageConverterHelpFile);
        }

        private void lnkDownload_LinkClicked(System.Object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ForkandBeard/Alferd-Spritesheet-Unpacker/releases");
        }

        private void lnkHelp_LinkClicked(System.Object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.alferdspritesheetunpacker.forkandbeard.co.uk/ForkandBeard/apps/AlferdSpritesheetUnpacker/FAQ.aspx?app=Alferd");
        }

        private void chkPreservePallette_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            if (this.chkPreservePallette.Checked)
            {
                this.chkExportBGTransparent.Checked = false;
                this.chkExportBGTransparent.Enabled = false;
            }
            else
            {
                this.chkExportBGTransparent.Enabled = true;
            }
        }
    }
}
