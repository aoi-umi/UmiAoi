using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace UmiAoi.Behaviors
{
    public class DragBehavior: Behavior<FrameworkElement>  
    {
        private FrameworkElement element { get; set; }
        private bool isDraging = false;
        protected override void OnAttached()
        {
            base.OnAttached();
            element = AssociatedObject as FrameworkElement;
            AssociatedObject.MouseMove += AssociatedObject_MouseMove;
            AssociatedObject.PreviewMouseDown += AssociatedObject_MouseLeftButtonDown;
            AssociatedObject.PreviewMouseUp += AssociatedObject_MouseLeftButtonUp;
        }

        private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.GetPosition((FrameworkElement)sender);
            if (isDraging)
            {
                pos.X -= element.ActualWidth / 2;
                pos.Y -= element.ActualHeight / 2;
                var left = (double)element.GetValue(Canvas.LeftProperty);
                var top = (double)element.GetValue(Canvas.TopProperty);
                if (double.IsNaN(left)) left = 0;
                if (double.IsNaN(top)) top = 0;
                element.SetValue(Canvas.LeftProperty, left + pos.X);
                element.SetValue(Canvas.TopProperty, top + pos.Y);                
            }
        }

        private void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDraging = false;
            e.Handled = true;
        }

        private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDraging = true;
            e.Handled = true;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
            AssociatedObject.MouseLeftButtonUp -= AssociatedObject_MouseLeftButtonUp;
            AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
        }
    }
}
