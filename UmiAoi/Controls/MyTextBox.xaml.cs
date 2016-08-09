using System;
using System.Windows;
using System.Windows.Controls;

namespace UmiAoi.Controls
{
    /// <summary>
    /// Interaction logic for MyTextBox.xaml
    /// </summary>
    public partial class MyTextBox : TextBox
    {
        public MyTextBox()
        {
            InitializeComponent();
        }

        public string Filename
        {
            get
            {
                string filename = string.Empty;
                if (!string.IsNullOrEmpty(Text))
                {
                    int index = Text.LastIndexOf("\\");
                    if (index >= 0) filename = Text.Substring(index + 1);
                }
                return filename;
            }
        }

        public string Ext
        {
            get
            {
                string ext = string.Empty;
                if (!string.IsNullOrEmpty(Filename))
                {
                    int index = Filename.LastIndexOf(".");
                    if (index >= 0) ext = Filename.Substring(index + 1);
                }
                return ext;
            }
        }


        

        private void MyTextBox_PreviewDrop(object sender, DragEventArgs e)
        {
            Text = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
        }

        private void MyTextBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
            e.Handled = true;
        }
    }
}
