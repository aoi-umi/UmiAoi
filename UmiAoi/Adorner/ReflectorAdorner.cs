using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace UmiAoi.Adorner
{
    public class ReflectorAdorner : System.Windows.Documents.Adorner
    {
        //public Dock DockType
        //{
        //    get { return (Dock)GetValue(DockTypeProperty); }
        //    set { SetValue(DockTypeProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty DockTypeProperty =
        //    DependencyProperty.Register(nameof(DockType), typeof(Dock), typeof(ReflectorAdorner), new PropertyMetadata(Dock.Bottom));

        private Rectangle rec;
        private VisualCollection VisualChildren;
        private UIElement element;
        public ReflectorAdorner(UIElement adornedElement, Dock DockType) : base(adornedElement)
        {
            element = adornedElement;
            rec = new Rectangle();
            VisualChildren = new VisualCollection(this);
            AddReflection(adornedElement, DockType);
        }

        private void AddReflection(UIElement element, Dock DockType)
        {
            VisualBrush brush = new VisualBrush(element) { Stretch = Stretch.Fill};
            LinearGradientBrush linerBrush = new LinearGradientBrush();
            switch (DockType)
            {
                case Dock.Left:
                    linerBrush.StartPoint = new Point(0, 0.5);
                    linerBrush.EndPoint = new Point(1, 0.5);
                    rec.Margin = new Thickness(0, 1, 0, 0);                    
                    rec.RenderTransform = new ScaleTransform() { ScaleX = -1, ScaleY = 1 };
                    break;
                case Dock.Right:
                    linerBrush.StartPoint = new Point(1, 0.5);
                    linerBrush.EndPoint = new Point(0, 0.5);
                    rec.Margin = new Thickness(0, 1, 0, 0);
                    rec.RenderTransform = new ScaleTransform() { ScaleX = -1, ScaleY = 1 };
                    break;
                case Dock.Top:
                    linerBrush.StartPoint = new Point(0.5, 0);
                    linerBrush.EndPoint = new Point(0.5, 1);
                    rec.Margin = new Thickness(0, 0, 0, 1);
                    rec.RenderTransform = new ScaleTransform() { ScaleX = 1, ScaleY = -1 };
                    break;
                case Dock.Bottom:
                    linerBrush.StartPoint = new Point(0.5, 1);
                    linerBrush.EndPoint = new Point(0.5, 0);
                    rec.Margin = new Thickness(0, 1, 0, 0);
                    rec.RenderTransform = new ScaleTransform() { ScaleX = 1, ScaleY = -1 };
                    break;
            }
            rec.Fill = brush;
            linerBrush.GradientStops.Add(new GradientStop() { Color = Colors.Black, Offset = 0.124 });
            linerBrush.GradientStops.Add(new GradientStop() { Color = Colors.Transparent, Offset = 1 });
            rec.OpacityMask = linerBrush;
            rec.RenderTransformOrigin = new Point(.5, .5);
            VisualChildren.Add(rec);
        }

        public void HideReflector()
        {
            rec.Visibility = Visibility.Hidden;
        }

        public void ShowReflector()
        {
            rec.Visibility = Visibility.Visible;
        }

        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        protected override Visual GetVisualChild(int index)
        {
            return rec;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            rec.Width = DesiredSize.Width;
            rec.Height = DesiredSize.Height;
            rec.Arrange(new Rect(0, DesiredSize.Height, DesiredSize.Width, DesiredSize.Height));
            return base.ArrangeOverride(finalSize);
        }

        //protected override void OnRender(DrawingContext drawingContext)
        //{
        //    //base.OnRender(drawingContext);
        //    drawingContext.DrawEllipse(Brushes.Red, new Pen(Brushes.Green, .5), new Point(element.RenderSize.Width, element.RenderSize.Height), 5, 5);
        //}
    }    
}
