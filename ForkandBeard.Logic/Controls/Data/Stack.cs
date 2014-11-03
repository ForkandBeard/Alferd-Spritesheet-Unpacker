using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ForkandBeard.Logic.Controls.Data
{
    public partial class Stack : UserControl
    {

        private const string InnerExceptionMessageDelimeter = "--->";
        private const string EndOfInnerExceptionDelimiter = "--- End of inner exception stack trace ---";
        private const string StackLineDelimeter = "at ";
        private const string Tab = "     ";

        public Stack()
        {
            InitializeComponent();
        }

        public void FormatStack(string stack)
        {
            string localStack;
            string presentChunk;
            int indexInnerExceptionMessageDelimeter;
            int indexEndOfInnerExceptionDelimiter;
            int indexStackLineDelimeter;
            int nextIndex;
            bool headerWritten = false;
            int presentTextBoxLength;
            string stackText;
            Color stackTextColour;
            bool stackLineColourToggle = false;
            string stackLineStart = String.Empty;
            string previousStackLineStart = String.Empty;

            this.stackRichTextBox.Text = String.Empty;

            if (String.IsNullOrEmpty(stack.Trim()))
            {
                return;
            }

            try
            {
                localStack = stack.Replace(Environment.NewLine, String.Empty).Trim();

                while (localStack.Length > 0)
                {
                    indexEndOfInnerExceptionDelimiter = localStack.IndexOf(EndOfInnerExceptionDelimiter, 1);
                    indexInnerExceptionMessageDelimeter = localStack.IndexOf(InnerExceptionMessageDelimeter, 1);
                    indexStackLineDelimeter = localStack.IndexOf(StackLineDelimeter, 1);

                    while (
                            (localStack.IndexOf('(', indexStackLineDelimeter + 1) > localStack.IndexOf(StackLineDelimeter, indexStackLineDelimeter + 1))
                            && (localStack.IndexOf(StackLineDelimeter, indexStackLineDelimeter + 1) != -1)
                            )
                    {
                        indexStackLineDelimeter = localStack.IndexOf(StackLineDelimeter, indexStackLineDelimeter + 1);
                    }

                    // TODO: Also check for a [(] before next [StackLineDelimeter].

                    nextIndex = localStack.Length;

                    if (indexStackLineDelimeter <= 0)
                    {
                        indexStackLineDelimeter = Int32.MaxValue;
                    }
                    else
                    {
                        if (indexStackLineDelimeter < nextIndex)
                        {
                            nextIndex = indexStackLineDelimeter;
                        }
                    }

                    if (indexInnerExceptionMessageDelimeter <= 0)
                    {
                        indexInnerExceptionMessageDelimeter = Int32.MaxValue;
                    }
                    else
                    {
                        if (indexInnerExceptionMessageDelimeter < nextIndex)
                        {
                            nextIndex = indexInnerExceptionMessageDelimeter;
                        }
                    }

                    if (indexEndOfInnerExceptionDelimiter <= 0)
                    {
                        indexEndOfInnerExceptionDelimiter = Int32.MaxValue;
                    }
                    else
                    {
                        if (indexEndOfInnerExceptionDelimiter < nextIndex)
                        {
                            nextIndex = indexEndOfInnerExceptionDelimiter;
                        }
                    }

                    if (
                        (this.stackRichTextBox.Text.Length == 0)
                        && (nextIndex >= localStack.Length)
                        && (
                            localStack.Contains(" +")
                            || (this.GetIndexOfColonAndNumber(localStack) > 0)
                            )
                        )
                    {

                        if (
                            (localStack.StartsWith("["))
                            && localStack.Contains("]")
                            )
                        {
                            localStack = String.Format("{0} {1}{2}", localStack.Substring(0, localStack.LastIndexOf("]") + 1).Trim(), StackLineDelimeter, localStack.Substring(localStack.LastIndexOf("]") + 1).Trim());
                        }

                        int indexPlus = 0;
                        int indexColon = 0;
                        int index = 0;

                        while (
                                (localStack.IndexOf(" +", index) > 0)
                                || (this.GetIndexOfColonAndNumber(localStack.Substring(index)) > 0)
                                )
                        {
                            indexColon = this.GetIndexOfColonAndNumber(localStack.Substring(index));
                            if (indexColon > 0)
                            {
                                indexColon += index;
                            }
                            indexPlus = localStack.IndexOf(" +", index);

                            if (indexColon <= 0)
                            {
                                indexColon = Int32.MaxValue;
                            }

                            if (indexPlus <= 0)
                            {
                                indexPlus = Int32.MaxValue;
                            }
                            else
                            {
                                indexPlus += 2;
                            }

                            if (indexPlus < indexColon)
                            {
                                index = indexPlus;
                            }
                            else
                            {
                                index = indexColon;
                            }

                            while (
                                    (index < localStack.Length) 
                                    && (localStack.Substring(index, 1) != " ")
                                    )
                            {
                                index++;
                            }

                            if (
                                (localStack.IndexOf(" +", index) > 0)
                                || (this.GetIndexOfColonAndNumber(localStack.Substring(index)) > 0)
                                )
                            {
                                localStack = String.Format("{0} {1}{2}", localStack.Substring(0, index).Trim(), StackLineDelimeter, localStack.Substring(index).Trim());
                                index = localStack.LastIndexOf(StackLineDelimeter);
                            }
                        }

                        nextIndex = localStack.IndexOf(StackLineDelimeter, 1);
                    }

                    presentChunk = localStack.Substring(0, nextIndex);
                    localStack = localStack.Substring(nextIndex);

                    if (this.stackRichTextBox.Text.Length > 0)
                    {
                        this.AppendText(Environment.NewLine, Color.Black, false);
                    }
                    presentTextBoxLength = this.stackRichTextBox.Text.Length;

                    if (presentChunk.StartsWith(InnerExceptionMessageDelimeter))
                    {
                        this.AppendExceptionMessageText(presentChunk);
                    }
                    else if (presentChunk.StartsWith(EndOfInnerExceptionDelimiter))
                    {
                        this.AppendText(String.Format("{0}{1}{2}", Tab, presentChunk, Environment.NewLine), Color.FromArgb(100, 100, 100), (localStack.Length == 0));
                    }
                    else if (presentChunk.StartsWith(StackLineDelimeter))
                    {
                        if (!headerWritten)
                        {
                            this.AppendText(Environment.NewLine, Color.Black, false);
                            headerWritten = true;
                            presentTextBoxLength = this.stackRichTextBox.Text.Length;
                        }

                        this.AppendText(String.Format("{0}{1}", Tab, presentChunk.Substring(0, StackLineDelimeter.Length)), Color.Black, (localStack.Length == 0));

                        if (presentChunk.Contains(" in "))
                        {
                            stackText = presentChunk.Substring(StackLineDelimeter.Length, presentChunk.IndexOf(" in "));                                                        
                        }
                        else
                        {
                            stackText = presentChunk.Substring(StackLineDelimeter.Length);
                        }
                        
                        if (
                            stackText.StartsWith("System.")
                            || stackText.StartsWith("java.")
                            )
                        {
                            stackTextColour = Color.FromArgb(50, 50, 50);
                            stackLineColourToggle = false;
                            previousStackLineStart = String.Empty;
                        }
                        else
                        {                                                                                   
                            if (stackText.Contains("."))
                            {
                                stackLineStart = stackText.Substring(0, stackText.IndexOf("."));
                            }
                            else
                            {
                                stackLineStart = stackText.Substring(0, Math.Min(stackText.Length, 15));
                            }

                            if (
                                !String.IsNullOrEmpty(previousStackLineStart)
                                && (stackLineStart != previousStackLineStart)
                                )
                            {
                                stackLineColourToggle = !stackLineColourToggle;
                            }

                            if (stackLineColourToggle)
                            {
                                stackTextColour = Color.FromArgb(100, 0, 200);
                            }
                            else
                            {
                                stackTextColour = Color.FromArgb(0, 0, 200);
                            }

                            previousStackLineStart = stackLineStart;
                        }

                        if (stackText.Contains("("))
                        {
                            if (
                                (stackText.IndexOf(".") != -1)
                                && (stackText.IndexOf(".") < stackText.IndexOf("("))
                                )
                            {
                                this.AppendText(stackText.Substring(0, stackText.Substring(0, stackText.IndexOf("(")).LastIndexOf(".")), stackTextColour, (localStack.Length == 0));
                                stackText = stackText.Substring(stackText.Substring(0, stackText.IndexOf("(")).LastIndexOf("."));
                            }

                            this.AppendText(stackText.Substring(0, stackText.IndexOf("(")), stackTextColour, true);
                            stackText = stackText.Substring(stackText.IndexOf("("));

                            this.AppendText(stackText, stackTextColour, (localStack.Length == 0));
                        }
                        else
                        {
                            this.AppendText(stackText, stackTextColour, (localStack.Length == 0));
                        }

                        if (presentChunk.Contains(" in "))
                        {
                            this.AppendText(presentChunk.Substring(presentChunk.IndexOf(" in ") + 3), stackTextColour, (localStack.Length == 0));
                        }
                    }
                    else
                    {
                        this.AppendExceptionMessageText(presentChunk);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to format stack.", ex);
            }
        }

        private int GetIndexOfColonAndNumber(string text)
        {
            System.Text.RegularExpressions.Match match;
            match = System.Text.RegularExpressions.Regex.Match(text, ":[0-9]+");
            if (match.Success)
            {
                return match.Index;
            }
            else
            {
                return -1;
            }
        }

        private void AppendExceptionMessageText(string text)
        {
            string localText = text;
            string chunk;

            if (localText.StartsWith(InnerExceptionMessageDelimeter))
            {
                chunk = localText.Substring(0, InnerExceptionMessageDelimeter.Length);
                this.AppendText(chunk, Color.Black, false);
                localText = localText.Substring(chunk.Length);
            }

            if (localText.Contains(":"))
            {
                chunk = localText.Substring(0, localText.IndexOf(":"));
                this.AppendText(chunk, Color.FromArgb(200, 0, 0), false);
                localText = localText.Substring(chunk.Length);
            }

            this.AppendText(localText, Color.Black, true);
        }

        private void AppendText(string text, Color color, bool bold)
        {
            this.stackRichTextBox.SelectionStart = this.stackRichTextBox.TextLength;
            this.stackRichTextBox.SelectionLength = 0;

            this.stackRichTextBox.SelectionColor = color;
            if (bold)
            {
                this.stackRichTextBox.SelectionFont = new Font(this.stackRichTextBox.SelectionFont, FontStyle.Bold);
            }
            this.stackRichTextBox.AppendText(text);
            if (bold)
            {
                this.stackRichTextBox.SelectionFont = new Font(this.stackRichTextBox.SelectionFont, FontStyle.Regular);
            }
            this.stackRichTextBox.SelectionStart = this.stackRichTextBox.TextLength;
        }

        //public void FormatStack3(string stack)
        //{
        //    this.stackRichTextBox.ForeColor = Color.Black;
        //    this.stackRichTextBox.Text = String.Empty;

        //    if (String.IsNullOrEmpty(stack.Trim()))
        //    {
        //        return;
        //    }

        //    try
        //    {
        //        string localStack = stack.Replace(Environment.NewLine, String.Empty);
        //        string presentString;
        //        int index;
        //        int endOfInnerIndex;
        //        int endOfLineIndex;
        //        int start;
        //        int length;

        //        StringBuilder uiText = new StringBuilder();

        //        if (localStack.Contains(InnerExceptionMessageDelimeter))
        //        {
        //            while (localStack.IndexOf(InnerExceptionMessageDelimeter, 1) > -1)
        //            {
        //                index = localStack.IndexOf(InnerExceptionMessageDelimeter, 1);
        //                presentString = localStack.Substring(0, index);
        //                uiText.AppendLine(presentString);
        //                localStack = localStack.Substring(index);
        //            }
        //        }

        //        index = localStack.IndexOf(StackLineDelimeter);
        //        presentString = localStack.Substring(0, index);
        //        uiText.AppendLine(presentString);
        //        localStack = localStack.Substring(index);

        //        uiText.AppendLine();

        //        while (
        //                (localStack.IndexOf(StackLineDelimeter, 1) > 1)
        //                || (localStack.IndexOf(EndOfInnerExceptionDelimiter, 1) > 1)
        //                )
        //        {
        //            endOfInnerIndex = localStack.IndexOf(EndOfInnerExceptionDelimiter, 1);
        //            endOfLineIndex = localStack.IndexOf(StackLineDelimeter, 1);

        //            if (endOfInnerIndex < 0)
        //            {
        //                endOfInnerIndex = Int32.MaxValue;
        //            }

        //            if (endOfLineIndex < 0)
        //            {
        //                endOfLineIndex = Int32.MaxValue;
        //            }

        //            if (endOfLineIndex < endOfInnerIndex)
        //            {
        //                index = endOfLineIndex;
        //            }
        //            else
        //            {
        //                index = endOfInnerIndex;
        //            }

        //            presentString = localStack.Substring(0, index);
        //            if (localStack.StartsWith(StackLineDelimeter))
        //            {
        //                presentString = String.Format(@"{0}{1}", Tab, presentString);
        //            }
        //            else if (localStack.StartsWith(EndOfInnerExceptionDelimiter))
        //            {
        //                presentString = String.Format(@"{0}{1}{2}", Tab, presentString, Environment.NewLine);
        //            }

        //            uiText.AppendLine(presentString);
        //            localStack = localStack.Substring(index);
        //        }

        //        presentString = localStack;
        //        if (localStack.StartsWith(StackLineDelimeter))
        //        {
        //            presentString = String.Format(@"{0}{1}", Tab, presentString);
        //        }
        //        uiText.AppendLine(presentString);

        //        this.stackRichTextBox.Text = uiText.ToString();

        //        index = 0;
        //        foreach (string row in this.stackRichTextBox.Lines)
        //        {
        //            if (row.Trim().StartsWith(StackLineDelimeter))
        //            {
        //                if (row.Trim().StartsWith(StackLineDelimeter))
        //                {
        //                    start = index + StackLineDelimeter.Length + Tab.Length;
        //                    if (row.Contains(" in "))
        //                    {
        //                        length = row.IndexOf(" in ") - Tab.Length - StackLineDelimeter.Length;
        //                    }
        //                    else
        //                    {
        //                        length = row.Length - start;
        //                    }
        //                    this.stackRichTextBox.Select(start, length);
        //                    this.stackRichTextBox.SelectionColor = Color.FromArgb(0, 0, 200);
        //                }
        //            }
        //            index += row.Length + 1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Unable to format stack.", ex);
        //    }
        //}

        private void stackRichTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
