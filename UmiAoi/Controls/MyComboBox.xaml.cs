using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Update();
        }


        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            Update();
        }

        private void Update()
        {
            if (SelectedIndex >= 0 && SelectedIndex < Items.Count)
            {
                MyComboBoxItem item = Items[SelectedIndex] as MyComboBoxItem;
                //MyComboBoxItem item = SelectedItem as MyComboBoxItem;
                if (item != null)
                {
                    SelectedItemHeader = item.Header;
                    MySelectedItem = item.Content;
                }
            }
            else
            {
                SelectedItemHeader = string.Empty;
                MySelectedItem = null;
            }
        }        

        public string SelectedItemHeader
        {
            get { return (string)GetValue(SelectedItemHeaderProperty); }
            set { SetValue(SelectedItemHeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectionBoxItemHeader.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemHeaderProperty =
            DependencyProperty.Register("SelectedItemHeader", typeof(string), typeof(MyComboBox), new PropertyMetadata(string.Empty));

        public Object MySelectedItem    
        {
            get { return (Object)GetValue(MySelectedItemProperty); }
            set { SetValue(MySelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MySelectedItemProperty =
            DependencyProperty.Register("MySelectedItem", typeof(Object), typeof(MyComboBox), new PropertyMetadata(null));



    }
}
