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
        public bool IsKeptMovePointCenter
        {
            get { return (bool)GetValue(IsKeptMovePointCenterProperty); }
            set { SetValue(IsKeptMovePointCenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsKeptMovePointCenter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsKeptMovePointCenterProperty =
            DependencyProperty.Register(nameof(IsKeptMovePointCenter), typeof(bool), typeof(DragBehavior), new PropertyMetadata(false));

        private FrameworkElement element { get; set; }
        protected override void OnAttached()
        {
            base.OnAttached();
            element = AssociatedObject;
            element.PreviewMouseMove += AssociatedObject_MouseMove;   
        }

        Point PressedPoint { get; set; }
        private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.GetPosition((FrameworkElement)sender);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (PressedPoint == new Point(0, 0)) PressedPoint = e.GetPosition((FrameworkElement)sender);
                if (IsKeptMovePointCenter)
                {
                    pos.X -= element.ActualWidth / 2;
                    pos.Y -= element.ActualHeight / 2;
                }
                else
                {
                    pos.X -= PressedPoint.X;
                    pos.Y -= PressedPoint.Y;
                }
                var left = (double)element.GetValue(Canvas.LeftProperty);
                var top = (double)element.GetValue(Canvas.TopProperty);
                if (double.IsNaN(left)) left = 0;
                if (double.IsNaN(top)) top = 0;
                element.SetValue(Canvas.LeftProperty, left + pos.X);
                element.SetValue(Canvas.TopProperty, top + pos.Y);
            }
            else if (PressedPoint != new Point(0, 0))
            {
                PressedPoint = new Point(0, 0);
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
        }
    }
}
