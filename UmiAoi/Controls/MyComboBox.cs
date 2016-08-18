using System;
using System.Windows;
using System.Windows.Controls;

namespace UmiAoi.Controls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:UmiAoi"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:UmiAoi;assembly=UmiAoi"
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
    ///     <MyNamespace:MyComboBox/>
    ///
    /// </summary>
    public class MyComboBox : ComboBox
    {
        static MyComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyComboBox), new FrameworkPropertyMetadata(typeof(MyComboBox)));
        }

        public MyComboBox() : base()
        {
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
                else
                {
                    ContentControl cc = Items[SelectedIndex] as ContentControl;
                    SelectedItemHeader = Items[SelectedIndex].ToString();
                    if (cc != null) MySelectedItem = cc.Content;

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

        public Dock ComboBoxPlacement
        {
            get { return (Dock)GetValue(ComboBoxPlacementProperty); }
            set { SetValue(ComboBoxPlacementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ComboBoxPlacementProperty =
            DependencyProperty.Register("ComboBoxPlacement", typeof(Dock), typeof(MyComboBox), new PropertyMetadata(Dock.Left));


    }
}
