using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace UmiAoi.Controls
{
    public class MyTextBox : TextBox
    {
        static MyTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyTextBox), new FrameworkPropertyMetadata(typeof(MyTextBox)));
        }

        public MyTextBox() : base()
        {
            Placeholder = "请输入";
            PreviewDrop += MyTextBox_PreviewDrop;
            PreviewDragOver += MyTextBox_PreviewDragOver;
        }

        private const string PART_PlaceholderName = "PART_Placeholder";
        private TextBlock PART_Placeholder;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_Placeholder = GetTemplateChild(PART_PlaceholderName) as TextBlock;

            var binding = new MultiBinding
            {
                Converter = new PlaceholderVisibilityConverter()
            };
            binding.Bindings.Add(new Binding()
            {
                Source = this,
                Path = new PropertyPath("Text"),
            });
            binding.Bindings.Add(new Binding()
            {
                Source = this,
                Path = new PropertyPath("IsFocused"),
            });
            PART_Placeholder.SetBinding(TextBlock.VisibilityProperty, binding);
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

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Placeholder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(MyTextBox), new PropertyMetadata(""));


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

    class PlaceholderVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = Visibility.Visible;
            var isEmpty = String.IsNullOrEmpty(values[0].ToString());
            Boolean.TryParse(values[1].ToString(), out bool isFocused);
            if (!isEmpty || isFocused)
                visibility = Visibility.Collapsed;

            return visibility;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

