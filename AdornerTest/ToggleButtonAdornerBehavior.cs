using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Documents;

namespace AdornerTest
{
    public class ToggleButtonAdornerBehavior : Behavior<ToggleButton>
    {
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(ControlTemplate), typeof(ToggleButtonAdornerBehavior),
                                                                               new FrameworkPropertyMetadata(OnContentChanged));
        public ControlTemplate Content
        {
            get { return (ControlTemplate)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        private static void OnContentChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var control = obj as ToggleButtonAdornerBehavior;
            control.Content = (ControlTemplate)args.NewValue;
        }

        public static readonly DependencyProperty PlacementProperty = DependencyProperty.Register("Placement", typeof(AdornerPlacement), typeof(ToggleButtonAdornerBehavior),
                                                                             new FrameworkPropertyMetadata(AdornerPlacement.Outside));
        public AdornerPlacement Placement
        {
            get { return (AdornerPlacement)GetValue(PlacementProperty); }
            set { SetValue(PlacementProperty, value); }
        }

        public static readonly DependencyProperty OffsetXProperty = DependencyProperty.Register("OffsetX", typeof(double), typeof(ToggleButtonAdornerBehavior));
        public double OffsetX
        {
            get { return (double)GetValue(OffsetXProperty); }
            set { SetValue(OffsetXProperty, value); }
        }

        public static readonly DependencyProperty OffsetYProperty = DependencyProperty.Register("OffsetY", typeof(double), typeof(ToggleButtonAdornerBehavior));
        public double OffsetY
        {
            get { return (double)GetValue(OffsetYProperty); }
            set { SetValue(OffsetYProperty, value); }
        }

        public static readonly DependencyProperty PositionXProperty = DependencyProperty.Register("PositionX", typeof(double), typeof(ToggleButtonAdornerBehavior));
        public double PositionX
        {
            get { return (double)GetValue(PositionXProperty); }
            set { SetValue(PositionXProperty, value); }
        }

        public static readonly DependencyProperty PositionYProperty = DependencyProperty.Register("PositionY", typeof(double), typeof(ToggleButtonAdornerBehavior));
        public double PositionY
        {
            get { return (double)GetValue(PositionYProperty); }
            set { SetValue(PositionYProperty, value); }
        }

        private AdornerLayer _adornerLayer = null;
        private ControlAdorner _adorner = null;

        protected override void OnAttached()
        {
            base.OnAttached();
            base.AssociatedObject.MouseEnter += OnMouseEnter;
            base.AssociatedObject.MouseLeave += OnMouseLeave;
        }

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (this.Content != null && _adorner == null)
            {
                Control control = new Control();
                control.Template = this.Content;
                control.DataContext = base.AssociatedObject.DataContext;

                _adorner = new ControlAdorner(base.AssociatedObject, control);
                _adorner.Placement = this.Placement;
                _adorner.OffsetX = this.OffsetX;
                _adorner.OffsetY = this.OffsetY;

                _adorner.PositionX = this.PositionX;
                _adorner.PositionY = this.PositionY;

                _adornerLayer = AdornerLayer.GetAdornerLayer(base.AssociatedObject);
                _adornerLayer.Add(_adorner);
            }
        }
        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (_adornerLayer != null && _adorner != null && !(base.AssociatedObject.IsChecked.HasValue && base.AssociatedObject.IsChecked.Value))
            {
                _adorner.RemoveChild();
                _adornerLayer.Remove(_adorner);
                _adorner = null;
                _adornerLayer = null;
            }
        }
    }
}
