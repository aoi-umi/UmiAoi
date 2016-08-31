using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace UmiAoi.Adorners
{
    public class ReflectorAdorner : Adorner
    {
        private Dock dockType;
        private Rectangle rec;
        private VisualCollection VisualChildren;
        private UIElement element;

        public ReflectorAdorner(UIElement adornedElement) : base(adornedElement)
        {
            Init(adornedElement, Dock.Bottom);
        }

        public ReflectorAdorner(UIElement adornedElement, Dock DockType) : base(adornedElement)
        {
            Init(adornedElement, DockType);
        }

        private void Init(UIElement adornedElement, Dock DockType)
        {
            element = adornedElement;
            dockType = DockType;
            rec = new Rectangle();
            VisualChildren = new VisualCollection(this);
            AddReflection(adornedElement);
        }

        private void AddReflection(UIElement element)
        {
            VisualBrush brush = new VisualBrush(element) { Stretch = Stretch.Fill};
            LinearGradientBrush linerBrush = new LinearGradientBrush();
            switch (dockType)
            {
                case Dock.Left:
                    linerBrush.StartPoint = new Point(0, 0.5);
                    linerBrush.EndPoint = new Point(1, 0.5);               
                    rec.RenderTransform = new ScaleTransform() { ScaleX = -1, ScaleY = 1 };
                    break;
                case Dock.Right:
                    linerBrush.StartPoint = new Point(1, 0.5);
                    linerBrush.EndPoint = new Point(0, 0.5);
                    rec.RenderTransform = new ScaleTransform() { ScaleX = -1, ScaleY = 1 };
                    break;
                case Dock.Top:
                    linerBrush.StartPoint = new Point(0.5, 0);
                    linerBrush.EndPoint = new Point(0.5, 1);
                    rec.RenderTransform = new ScaleTransform() { ScaleX = 1, ScaleY = -1 };
                    break;
                case Dock.Bottom:
                    linerBrush.StartPoint = new Point(0.5, 1);
                    linerBrush.EndPoint = new Point(0.5, 0);
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

        public void ShowOrHideReflector()
        {
            rec.Visibility = rec.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }

        protected override int VisualChildrenCount
        {
            get { return VisualChildren.Count; }
        }

        protected override Visual GetVisualChild(int index)
        {
            return VisualChildren[index];
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            SetReflector();
        }

        private void SetReflector()
        {
            rec.Width = DesiredSize.Width;
            rec.Height = DesiredSize.Height;
            double margin = 1;
            switch (dockType)
            {
                case Dock.Top:
                    rec.Arrange(new Rect(0, -DesiredSize.Height - margin, DesiredSize.Width, DesiredSize.Height));
                    break;
                case Dock.Bottom:
                    rec.Arrange(new Rect(0, DesiredSize.Height + margin, DesiredSize.Width, DesiredSize.Height));
                    break;
                case Dock.Left:
                    rec.Arrange(new Rect(-DesiredSize.Width - margin, 0, DesiredSize.Width, DesiredSize.Height));
                    break;
                case Dock.Right:
                    rec.Arrange(new Rect(DesiredSize.Width + margin, 0, DesiredSize.Width, DesiredSize.Height));
                    break;
            }
        }
    }    
}
