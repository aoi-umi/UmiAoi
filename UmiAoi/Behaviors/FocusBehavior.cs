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
    public sealed class FocusBehavior: Behavior<UIElement>
    {
        private bool IsBusy = false;
        private Storyboard storyboard;
        private DoubleAnimation doubleAnimationX;
        private DoubleAnimation doubleAnimationY;
        #region Property
        public CompositeTransformType TransformType
        {
            get { return (CompositeTransformType)GetValue(TransformTypeProperty); }
            set { SetValue(TransformTypeProperty, value); }
        }

        public static readonly DependencyProperty TransformTypeProperty =
            DependencyProperty.Register(nameof(TransformType), typeof(CompositeTransformType), typeof(FocusBehavior), new PropertyMetadata(CompositeTransformType.Rotation));
        
        public double From
        {
            get { return (double)GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }

        public static readonly DependencyProperty FromProperty =
            DependencyProperty.Register(nameof(From), typeof(double), typeof(FocusBehavior), new PropertyMetadata(0.0));

        public double To
        {
            get { return (double)GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }

        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register(nameof(To), typeof(double), typeof(FocusBehavior), new PropertyMetadata(0.0));



        public double DurationMilliseconds
        {
            get { return (double)GetValue(DurationMillisecondsProperty); }
            set { SetValue(DurationMillisecondsProperty, value); }
        }

        public static readonly DependencyProperty DurationMillisecondsProperty =
            DependencyProperty.Register(nameof(DurationMilliseconds), typeof(double), typeof(FocusBehavior), new PropertyMetadata(300.0));

        #endregion
        protected override void OnAttached()
        {
            if (From == To || DurationMilliseconds == 0) return;
            base.OnAttached();
            AssociatedObject.RenderTransformOrigin = new Point(0.5, 0.5);
            TransformGroup tg = new TransformGroup();
            AssociatedObject.RenderTransform = tg;

            storyboard = new Storyboard() { FillBehavior = FillBehavior.HoldEnd };
            doubleAnimationX = new DoubleAnimation() { Duration = TimeSpan.FromMilliseconds(DurationMilliseconds) };
            doubleAnimationY = new DoubleAnimation() { Duration = TimeSpan.FromMilliseconds(DurationMilliseconds) };
            Storyboard.SetTarget(doubleAnimationX, AssociatedObject); 
            Storyboard.SetTarget(doubleAnimationY, AssociatedObject);
            switch (TransformType)
            {
                case CompositeTransformType.Rotation:
                    RotateTransform roate = new RotateTransform();
                    tg.Children.Add(roate);
                    Storyboard.SetTargetProperty(doubleAnimationX, new PropertyPath("RenderTransform.Children[0].Angle"));
                    break;
                case CompositeTransformType.Scale:
                    ScaleTransform scale = new ScaleTransform();
                    tg.Children.Add(scale);
                    Storyboard.SetTargetProperty(doubleAnimationX, new PropertyPath("RenderTransform.Children[0].ScaleX"));
                    Storyboard.SetTargetProperty(doubleAnimationY, new PropertyPath("RenderTransform.Children[0].ScaleY"));
                    break;
                case CompositeTransformType.Skew:
                    SkewTransform skew = new SkewTransform();
                    tg.Children.Add(skew);
                    Storyboard.SetTargetProperty(doubleAnimationX, new PropertyPath("RenderTransform.Children[0].AngleX"));
                    Storyboard.SetTargetProperty(doubleAnimationY, new PropertyPath("RenderTransform.Children[0].AngleY"));
                    break;
                case CompositeTransformType.Translate:
                    TranslateTransform translate = new TranslateTransform();
                    tg.Children.Add(translate);
                    Storyboard.SetTargetProperty(doubleAnimationX, new PropertyPath("RenderTransform.Children[0].X"));
                    Storyboard.SetTargetProperty(doubleAnimationY, new PropertyPath("RenderTransform.Children[0].Y"));
                    break;
            }
            if (TransformType == CompositeTransformType.Rotation)
            {
                storyboard.Children.Add(doubleAnimationX);
            }
            else
            {
                storyboard.Children.Add(doubleAnimationX);
                storyboard.Children.Add(doubleAnimationY);
            }

            AssociatedObject.MouseEnter += AssociatedObject_MouseEnter;
            AssociatedObject.MouseLeave += AssociatedObject_MouseLeave;
            storyboard.Completed += Storyboard_Completed;
            //AssociatedObject.TouchEnter += AssociatedObject_TouchEnter;
            //AssociatedObject.TouchLeave += AssociatedObject_TouchLeave;

        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            IsBusy = false;
        }

        private void AssociatedObject_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!IsBusy)
            {
                BeginStoryBoard(From, To);
                IsBusy = true;
            }
        }

        private void AssociatedObject_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            BeginStoryBoard(To, From);
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
            doubleAnimationX.From = from;
            doubleAnimationX.To = to;
            doubleAnimationY.From = from;
            doubleAnimationY.To = to;
            storyboard.Begin();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            //AssociatedObject.TouchEnter -= AssociatedObject_TouchEnter;
            //AssociatedObject.TouchLeave -= AssociatedObject_TouchLeave;
            AssociatedObject.MouseEnter -= AssociatedObject_MouseEnter;
            AssociatedObject.MouseLeave -= AssociatedObject_MouseLeave;
            storyboard.Completed -= Storyboard_Completed;
        }
    }

    public enum CompositeTransformType
    {
        Rotation,
        Scale,
        Skew,
        Translate
    }
}
