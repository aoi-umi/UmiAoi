using System.Windows;
using System.Windows.Media;

namespace UmiAoi.Adorners
{
    public class SimpleDecorator : System.Windows.Controls.Decorator
    {
        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawEllipse(Brushes.Red, new Pen(Brushes.Green, .5), new Point(0, 0), 5, 5);
            drawingContext.DrawEllipse(Brushes.Red, new Pen(Brushes.Green, .5), new Point(Child.RenderSize.Width, 0), 5, 5);
            drawingContext.DrawEllipse(Brushes.Red, new Pen(Brushes.Green, .5), new Point(0, Child.RenderSize.Height), 5, 5);
            drawingContext.DrawEllipse(Brushes.Red, new Pen(Brushes.Green, .5), new Point(Child.RenderSize.Width, Child.RenderSize.Height), 5, 5);
        }
    }
}
