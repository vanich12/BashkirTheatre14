using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Clipboard = System.Windows.Clipboard;
using RichTextBox = System.Windows.Controls.RichTextBox;

namespace BashkirTheatre14.Helpers
{
    public class RichTextBoxHelper
    {
        public static readonly DependencyProperty HtmlTextProperty = DependencyProperty.RegisterAttached(
            "HtmlText", typeof(string), typeof(RichTextBoxHelper),
            new PropertyMetadata(default(string?), HtmlTextPropertyChangedCallback));

        public static void SetHtmlText(DependencyObject element, string? value)
        {
            element.SetValue(HtmlTextProperty, value);
        }

        public static string? GetHtmlText(DependencyObject element)
        {
            return (string?) element.GetValue(HtmlTextProperty);
        }

        private static void HtmlTextPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is not string htmlText)
                return;

            switch (d)
            {
                case RichTextBox richTextBox:
                    SetRichTextBoxText(richTextBox, htmlText);
                    break;

                case TextBlock textBlock:
                    SetTextBlockText(textBlock, htmlText);
                    break;
            }
        }

        private static void SetTextBlockText(TextBlock textBlock, string htmlText)
        {
            CopyTextInClipboard(htmlText);

            try
            {
                textBlock.Text = Clipboard.GetText();
            }
            catch (Exception ex)
            {
                
            }
        }
        
        private static void SetRichTextBoxText(RichTextBox richTextBox, string htmlText)
        {
            CopyTextInClipboard(htmlText);

            richTextBox.SelectAll();
            richTextBox.Paste();

            var range = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            range.ApplyPropertyValue(TextElement.FontFamilyProperty, richTextBox.FontFamily);

            var defaultFontSize = 16;
            var fontSizeRatio = richTextBox.FontSize / defaultFontSize;

            foreach (var block in richTextBox.Document.Blocks)
            {
                
                block.FontSize *= fontSizeRatio;
            }
        }

        private static void CopyTextInClipboard(string htmlText)
        {
            var webBrowser = new System.Windows.Forms.WebBrowser();
            webBrowser.Navigate("about:blank");
            webBrowser.Document?.Write(htmlText);
            webBrowser.Document?.ExecCommand("SelectAll", false, null);
            webBrowser.Document?.ExecCommand("Copy", false, null);
        }
    }
}
