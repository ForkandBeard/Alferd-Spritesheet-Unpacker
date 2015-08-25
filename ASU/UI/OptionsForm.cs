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

        private void OptionsForm_Load(object sender, System.EventArgs e)
        {
            try
            {
                this.Text = String.Format(STR_FORM_TITLE, ForkandBeard.Logic.Names.GetApplicationMajorVersion());
                this.CommandLineHelpLink.Text = String.Format("{0} command line help file", System.IO.Path.GetFileName(System.Configuration.ConfigurationManager.AppSettings["ThirdPartyImageConverter"]));

                if (this.SelectAllOrderComboBox.SelectedIndex == -1)
                {
                    this.SelectAllOrderComboBox.SelectedIndex = 0;
                }

                this.ExportFormatComboBox.Items.Clear();
                this.ExportFormatComboBox.Items.Add(System.Drawing.Imaging.ImageFormat.Png);
                this.ExportFormatComboBox.Items.Add(System.Drawing.Imaging.ImageFormat.Bmp);
                this.ExportFormatComboBox.Items.Add(System.Drawing.Imaging.ImageFormat.Gif);
                this.ExportFormatComboBox.Items.Add(System.Drawing.Imaging.ImageFormat.Tiff);
                this.ExportFormatComboBox.Items.Add(System.Drawing.Imaging.ImageFormat.Jpeg);
                this.ExportFormatComboBox.Items.Add(STR_ADVANCED_EXPORT_FILE_FORMAT);
                this.CommandLineLabel.Text = this.CreateCommandLineArgs();

                if (this.Main != null)
                {
                    this.ExportBGTransparentCheckBox.Checked = MainForm.MakeBackgroundTransparent;
                    this.SelectedColourPanel.BackColor = MainForm.SelectedFill.Color;
                    this.TileBorderColourPanel.BackColor = MainForm.Outline.Color;
                    this.HoverColourPanel.BackColor = MainForm.HoverFill.Color;
                    this.DistanceBetweenTilesUpDown.Value = MainForm.DistanceBetweenTiles;
                    this.OutlineWidthUpDown.Value = Convert.ToDecimal(MainForm.Outline.Width);
                    this.PreservePalletteCheckBox.Checked = MainForm.PreservePallette;

                    if (MainForm.ExportFormat != null)
                    {
                        this.ExportFormatComboBox.SelectedItem = MainForm.ExportFormat;
                    }
                    else
                    {
                        this.ExportFormatComboBox.Text = STR_ADVANCED_EXPORT_FILE_FORMAT;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error during option load");
            }
        }

        private void SelectedColourButton_Click(System.Object sender, System.EventArgs e)
        {
            this.ColorDialog1.Color = this.SelectedColourPanel.BackColor;
            this.ColorDialog1.ShowDialog();
            this.SelectedColourPanel.BackColor = Color.FromArgb(200, this.ColorDialog1.Color);
        }

        private void TileBorderColourButton_Click(System.Object sender, System.EventArgs e)
        {
            this.ColorDialog1.Color = this.TileBorderColourPanel.BackColor;
            this.ColorDialog1.ShowDialog();
            this.TileBorderColourPanel.BackColor = this.ColorDialog1.Color;
        }

        private void HoverColourButton_Click(System.Object sender, System.EventArgs e)
        {
            this.ColorDialog1.Color = this.HoverColourPanel.BackColor;
            this.ColorDialog1.ShowDialog();
            this.HoverColourPanel.BackColor = Color.FromArgb(150, this.ColorDialog1.Color);
        }

        private void CloseButton_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (this.Main != null)
                {
                    bool reloadNeeded = false;

                    MainForm.SelectedFill = new SolidBrush(Color.FromArgb(MainForm.SelectedFill.Color.A, this.SelectedColourPanel.BackColor));
                    MainForm.ZoomPen = new Pen(Color.FromArgb(MainForm.ZoomPen.Color.A, this.SelectedColourPanel.BackColor), MainForm.ZoomPen.Width);
                    MainForm.HoverFill = new SolidBrush(Color.FromArgb(MainForm.HoverFill.Color.A, this.HoverColourPanel.BackColor));
                    MainForm.Outline = new Pen(Color.FromArgb(MainForm.Outline.Color.A, this.TileBorderColourPanel.BackColor), (float)this.OutlineWidthUpDown.Value);
                    MainForm.PromptForDestinationFolder = this.PromptDestinationFolderCheckBox.Checked;
                    MainForm.AutoOpenDestinationFolder = this.OpenExportedDestinationCheckBox.Checked;
                    MainForm.MakeBackgroundTransparent = this.ExportBGTransparentCheckBox.Checked;
                    MainForm.PreservePallette = this.PreservePalletteCheckBox.Checked;

                    if (this.ExportFormatComboBox.Text != STR_ADVANCED_EXPORT_FILE_FORMAT)
                    {
                        MainForm.ExportNConvertArgs = string.Empty;
                        MainForm.ExportFormat = (System.Drawing.Imaging.ImageFormat)this.ExportFormatComboBox.SelectedItem;
                    }
                    else
                    {
                        MainForm.ExportFormat = null;
                        MainForm.ExportNConvertArgs = this.CreateCommandLineArgs();
                    }

                    if (MainForm.DistanceBetweenTiles != this.DistanceBetweenTilesUpDown.Value)
                    {
                        MainForm.DistanceBetweenTiles = Convert.ToInt32(this.DistanceBetweenTilesUpDown.Value);
                        reloadNeeded = true;
                    }

                    MainForm.SheetWithBoxes = null;
                    if (reloadNeeded)
                    {
                        this.Main.ReloadOriginal();
                    }
                    else
                    {
                        this.Main.MainPanel.Refresh();
                    }
                }
                this.Hide();
            }
            catch (Exception ex)
            {
                ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk");
            }
        }

        private void AboutButton_Click(System.Object sender, System.EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void ExportFormatComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.NConvertArgsTextBox.Enabled = this.ExportFormatComboBox.Text == STR_ADVANCED_EXPORT_FILE_FORMAT;
            this.ShowCommandLineArgsCheckBox.Enabled = this.NConvertArgsTextBox.Enabled;
            this.CommandLineHelpLink.Visible = this.NConvertArgsTextBox.Enabled && this.ShowCommandLineArgsCheckBox.Checked;
            this.CommandLineLabel.Visible = this.NConvertArgsTextBox.Enabled && this.ShowCommandLineArgsCheckBox.Checked;

            if (this.ExportFormatComboBox.Text == STR_ADVANCED_EXPORT_FILE_FORMAT)
            {
                this.ExportBGTransparentCheckBox.Checked = false;
            }
            else
            {
                this.ShowCommandLineArgsCheckBox.Checked = false;
            }
        }

        private void NConvertArgsTextBox_TextChanged(System.Object sender, System.EventArgs e)
        {
            this.CommandLineLabel.Text = this.CreateCommandLineArgs();
        }

        private string CreateCommandLineArgs()
        {
            string returnString;

            returnString = System.Configuration.ConfigurationManager.AppSettings["ThirdPartyImageConverterCommandArgsExportFormat"].Replace("{text}", this.NConvertArgsTextBox.Text);

            return returnString;
        }

        private void ShowCommandLineArgsCheckBox_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            this.CommandLineLabel.Visible = this.ShowCommandLineArgsCheckBox.Checked;
            this.CommandLineHelpLink.Visible = this.ShowCommandLineArgsCheckBox.Checked;

            if (this.ShowCommandLineArgsCheckBox.Checked)
            {
                this.ExportPanel.Height += 91;
                this.Height += 89;
            }
            else
            {
                this.ExportPanel.Height -= 91;
                this.Height -= 89;
            }
        }

        private void CommandLineHelpLink_LinkClicked(System.Object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            string thirdPartyImageConverterHelpFile = null;

            thirdPartyImageConverterHelpFile = System.Configuration.ConfigurationManager.AppSettings["ThirdPartyImageConverterHelpFile"];
            if (thirdPartyImageConverterHelpFile.StartsWith("\\"))
            {
                thirdPartyImageConverterHelpFile = AppDomain.CurrentDomain.BaseDirectory + thirdPartyImageConverterHelpFile;
            }
            System.Diagnostics.Process.Start(thirdPartyImageConverterHelpFile);
        }

        private void DownloadLink_LinkClicked(System.Object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ForkandBeard/Alferd-Spritesheet-Unpacker/releases");
        }

        private void HelpLink_LinkClicked(System.Object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.alferdspritesheetunpacker.forkandbeard.co.uk/ForkandBeard/apps/AlferdSpritesheetUnpacker/FAQ.aspx?app=Alferd");
        }

        private void PreservePalletteCheckBox_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            if (this.PreservePalletteCheckBox.Checked)
            {
                this.ExportBGTransparentCheckBox.Checked = false;
                this.ExportBGTransparentCheckBox.Enabled = false;
            }
        }
    }
}
