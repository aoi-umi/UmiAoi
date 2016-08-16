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
    /// Interaction logic for MyComboBox.xaml
    /// </summary>
    public partial class MyComboBox : ComboBox
    {
        public MyComboBox()
        {
            InitializeComponent();
            SelectionChanged += MyComboBox_SelectionChanged;
        }

        private void MyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyComboBoxItem item = SelectionBoxItem as MyComboBoxItem;
            if (item != null) SelectionBoxItemHeader = item.Header;
        }

        public string SelectionBoxItemHeader
        {
            get { return (string)GetValue(SelectionBoxItemHeaderProperty); }
            set { SetValue(SelectionBoxItemHeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectionBoxItemHeader.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectionBoxItemHeaderProperty =
            DependencyProperty.Register("SelectionBoxItemHeader", typeof(string), typeof(MyComboBox), new PropertyMetadata(null));

        
    }
}
