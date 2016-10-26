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
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:UmiAoi.Controls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:UmiAoi.Controls;assembly=UmiAoi.Controls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:MiniWindow/>
    ///
    /// </summary>
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
