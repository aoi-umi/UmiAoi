using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UmiAoi.Controls
{    
    public class MiniWindow : ContentControl
    {
        static MiniWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MiniWindow), new FrameworkPropertyMetadata(typeof(MiniWindow)));
        }

        public bool IsMinimized
        {
            get { return (bool)GetValue(IsMinimizedProperty); }
            set { SetValue(IsMinimizedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsMinimized.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsMinimizedProperty =
            DependencyProperty.Register(nameof(IsMinimized), typeof(bool), typeof(MiniWindow), new PropertyMetadata(false));


        public delegate void CloseClickedEvenrHandler(object sender, RoutedEventArgs e);
        public CloseClickedEvenrHandler CloseClicked;

        private string CloseButtonName = "closeButton";
        private string MinimizeButtonName = "minimizeButton";
        private string MaxmizeButtonName = "maxmizeButton";
        private Button CloseButton { get; set; }
        private Button MinimizeButton { get; set; }
        private Button MaxmizeButton { get; set; }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            CloseButton = GetTemplateChild(CloseButtonName) as Button;
            MinimizeButton = GetTemplateChild(MinimizeButtonName) as Button;
            MaxmizeButton = GetTemplateChild(MaxmizeButtonName) as Button;

            CloseButton.Click += CloseButton_Click;
            MinimizeButton.Click += MinimizeButton_Click;
            MaxmizeButton.Click += MaxmizeButton_Click;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CloseClicked?.Invoke(this, e);
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            IsMinimized = true;
        }

        private void MaxmizeButton_Click(object sender, RoutedEventArgs e)
        {
            IsMinimized = false;
        }

        ~MiniWindow()
        {
            CloseButton.Click -= CloseButton_Click;
            MinimizeButton.Click -= MinimizeButton_Click;
            MaxmizeButton.Click -= MaxmizeButton_Click;
        }
    }
}
