using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace UmiAoi.Behaviors
{
    public sealed class FocusScaleBehavior: Behavior<UIElement>
    {
        private const double defaultZoomTime = 0.1;
        private Storyboard storyboardZoom;
        DoubleAnimation doubleAnimationScaleX;
        DoubleAnimation doubleAnimationScaleY;
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.RenderTransformOrigin = new Point(0.5, 0.5);
            ScaleTransform scale = new ScaleTransform();
            TransformGroup tg = new TransformGroup();
            tg.Children.Add(scale);
            AssociatedObject.RenderTransform = tg;

            storyboardZoom = new Storyboard() { FillBehavior = FillBehavior.HoldEnd };
            doubleAnimationScaleX = new DoubleAnimation() { Duration = TimeSpan.FromSeconds(defaultZoomTime) };
            doubleAnimationScaleY = new DoubleAnimation() { Duration = TimeSpan.FromSeconds(defaultZoomTime) };
            Storyboard.SetTarget(doubleAnimationScaleX, AssociatedObject); 
            Storyboard.SetTargetProperty(doubleAnimationScaleX, new PropertyPath("RenderTransform.Children[0].ScaleX")); 
            storyboardZoom.Children.Add(doubleAnimationScaleX);
            Storyboard.SetTarget(doubleAnimationScaleY, AssociatedObject);
            Storyboard.SetTargetProperty(doubleAnimationScaleY, new PropertyPath("RenderTransform.Children[0].ScaleY"));
            storyboardZoom.Children.Add(doubleAnimationScaleY);

            AssociatedObject.MouseEnter += AssociatedObject_MouseEnter;
            AssociatedObject.MouseLeave += AssociatedObject_MouseLeave;
            //AssociatedObject.TouchEnter += AssociatedObject_TouchEnter;
            //AssociatedObject.TouchLeave += AssociatedObject_TouchLeave;

        }

        private void AssociatedObject_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            BeginStoryBoard(1, 1.5);
        }

        private void AssociatedObject_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            BeginStoryBoard(1.5, 1);
        }

        private void AssociatedObject_TouchEnter(object sender, System.Windows.Input.TouchEventArgs e)
        {
            BeginStoryBoard(1, 2);
        }

        private void AssociatedObject_TouchLeave(object sender, System.Windows.Input.TouchEventArgs e)
        {
            BeginStoryBoard(2, 1);
        }

        private void BeginStoryBoard(double from, double to)
        {
            doubleAnimationScaleX.From = from;
            doubleAnimationScaleX.To = to;
            doubleAnimationScaleY.From = from;
            doubleAnimationScaleY.To = to;
            storyboardZoom.Begin();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            //AssociatedObject.TouchEnter -= AssociatedObject_TouchEnter;
            //AssociatedObject.TouchLeave -= AssociatedObject_TouchLeave;
            AssociatedObject.MouseEnter -= AssociatedObject_MouseEnter;
            AssociatedObject.MouseLeave -= AssociatedObject_MouseLeave;
        }
    }
}
