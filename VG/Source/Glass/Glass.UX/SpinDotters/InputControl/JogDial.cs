// (c) Norbert Huffschmid
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.ComponentModel;
using SpinDotters.InputControl;

namespace SpinDotters
{
    /// <summary>
    /// Jog dial controller
    /// </summary>
    [TemplatePart(Name = "PART_Wheel",   Type = typeof(SpinPanel))]
    [TemplateVisualState(Name = "Vertical",   GroupName = "OrientationStates")]
    [TemplateVisualState(Name = "Horizontal", GroupName = "OrientationStates")]
    public class JogDial : Control, IDragControl
    {
        /// <summary>
        /// Event fired when spin angle has changed
        /// </summary>
        public event EventHandler SpinAngleChanged;

        /// <summary>
        /// Event fired when the dragging condition has changed
        /// </summary>
        public event EventHandler DraggingChanged;


        private SpinPanel wheel;


        /// <summary>
        /// Orientation dependency property.
        /// </summary>
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(
                "Orientation",
                typeof(Orientation),
                typeof(JogDial),
                new PropertyMetadata(UpDownBase.ORIENTATION_DEFAULT,
                    new PropertyChangedCallback(OnOrientationChanged)));

        /// <summary>
        /// Gets or sets the spin orientation.
        /// </summary>
        [Category("SpinDotters")]
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        /// <summary>
        /// Spin angle property.
        /// </summary>
        public static readonly DependencyProperty SpinAngleProperty =
            DependencyProperty.Register(
                "SpinAngle",
                typeof(double),
                typeof(JogDial),
                new PropertyMetadata(new PropertyChangedCallback(OnSpinAngleChanged)));

        /// <summary>
        /// Gets or sets the spin angle.
        /// </summary>
        public double SpinAngle
        {
            get { return (double)GetValue(SpinAngleProperty); }
            set { SetValue(SpinAngleProperty, value); }
        }

        /// <summary>
        /// Dragging dependency property.
        /// </summary>
        public static readonly DependencyProperty DraggingProperty =
            DependencyProperty.Register(
                "Dragging",
                typeof(bool),
                typeof(JogDial),
                new PropertyMetadata(false, new PropertyChangedCallback(OnDraggingChanged)));

        /// <summary>
        /// Gets or sets the dragging condition.
        /// </summary>
        public bool Dragging
        {
            get { return (bool)GetValue(DraggingProperty); }
            set { SetValue(DraggingProperty, value); }
        }

#if WPF
        static JogDial()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(JogDial),
                new FrameworkPropertyMetadata(typeof(JogDial)));
        }
#endif

        /// <summary>
        /// Constructor
        /// </summary>
        public JogDial()
        {
#if !WPF
            this.DefaultStyleKey = typeof(JogDial);
#endif
            this.SizeChanged += new SizeChangedEventHandler(SegmentSizeHandler);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.wheel = this.GetTemplateChild("PART_Wheel") as SpinPanel;

            if (this.wheel != null)
            {
                // doing these bindings in XAML will cause BindingExpression path errors within widgets!

                Binding spinAngleBinding = new Binding("SpinAngleProperty");
                spinAngleBinding.Source = this;
                spinAngleBinding.Path = new PropertyPath("SpinAngle");
                spinAngleBinding.Mode = BindingMode.TwoWay;
                this.wheel.SetBinding(SpinPanel.SpinAngleProperty, spinAngleBinding);

                Binding draggingBinding = new Binding("DraggingProperty");
                draggingBinding.Source = this;
                draggingBinding.Path = new PropertyPath("Dragging");
                draggingBinding.Mode = BindingMode.TwoWay;
                this.wheel.SetBinding(SpinPanel.DraggingProperty, draggingBinding);
            }

            this.wheel.DataContext = this;

            if (this.Orientation == Orientation.Vertical)
                VisualStateManager.GoToState(this, "Vertical", false);
            else
                VisualStateManager.GoToState(this, "Horizontal", false);
        }

        protected virtual void OnSpinAngleChanged()
        {
            if (this.SpinAngleChanged != null)
                this.SpinAngleChanged(this, EventArgs.Empty);
        }

        protected virtual void OnDraggingChanged()
        {
            if (this.DraggingChanged != null)
                this.DraggingChanged(this, EventArgs.Empty);
        }


        private void SegmentSizeHandler(object sender, EventArgs e)
        {
            if (this.wheel != null)
            {
                // resize wheel segment so that available size is filled
                foreach (FrameworkElement item in this.wheel.Children)
                {
                    if (this.Orientation == Orientation.Vertical)
                    {
                        item.ClearValue(WidthProperty);
                        item.Height = this.ActualHeight / 6;
                    }
                    else
                    {
                        item.ClearValue(HeightProperty);
                        item.Width = this.ActualWidth / 6;
                    }
                }
            }
        }

        private static void OnOrientationChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            JogDial jogDial = o as JogDial;
            jogDial.SegmentSizeHandler(jogDial, EventArgs.Empty);
        }

        private static void OnSpinAngleChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            JogDial jogDial = o as JogDial;

            if (jogDial != null)
            {
                jogDial.OnSpinAngleChanged();
            }
        }

        private static void OnDraggingChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            JogDial jogDial = o as JogDial;

            if (jogDial != null)
            {
                jogDial.OnDraggingChanged();
            }
        }
    }
}
