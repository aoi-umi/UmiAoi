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

namespace CustomControlDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            miniWindow.CloseClicked += miniWindow_CloseClicked;
        }

        private void miniWindow_CloseClicked(object sender, RoutedEventArgs e)
        {
            var element = sender as UIElement;
            uniform.Children.Remove(element);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var element = sender as ContentControl;
            string message = "content is ";
            if (element != null)
                message += element.Content;
            MessageBox.Show(message);
        }
    }
}
