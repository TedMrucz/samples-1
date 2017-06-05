using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace AdornerTest
{
    public enum AdornerPlacement
    {
        Inside,
        Outside
    }

    public class ControlAdorner : Adorner
    {
        private readonly FrameworkElement adoriningElement = null;
        public AdornerPlacement Placement { get; set; }
        public double OffsetX { get; set; }
        public double OffsetY { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }

        public new FrameworkElement AdornedElement
        {
            get { return (FrameworkElement)base.AdornedElement; }
        }

        public ControlAdorner(UIElement adornedElement, FrameworkElement adoriningElement) : base(adornedElement)
        {
            this.adoriningElement = adoriningElement;

            base.AddLogicalChild(this.adoriningElement);
            base.AddVisualChild(this.adoriningElement);
        }

        public void RemoveChild()
        {
            base.RemoveLogicalChild(this.adoriningElement);
            base.RemoveVisualChild(this.adoriningElement);
        }

        protected override int VisualChildrenCount
        {
            get { return this.adoriningElement == null ? 0 : 1; }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index == 0 && this.adoriningElement != null)
                return this.adoriningElement;

            return base.GetVisualChild(index);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            this.adoriningElement.Measure(constraint);
            return this.adoriningElement.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double x = this.PositionX;
            if (Double.IsNaN(x))
                x = this.DetermineX();
            double y = this.PositionY;
            if (Double.IsNaN(y))
                y = this.DetermineY();

            this.adoriningElement.Arrange(new Rect(x, y, this.DetermineWidth(), this.DetermineHeight()));
            return finalSize;
        }

        private double DetermineX()
        {
            switch (this.adoriningElement.HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    {
                        if (this.Placement == AdornerPlacement.Outside)
                            return - this.adoriningElement.DesiredSize.Width + this.OffsetX;
                        else
                            return this.OffsetX;
                    }
                case HorizontalAlignment.Right:
                    {
                        if (this.Placement == AdornerPlacement.Outside)
                            return this.AdornedElement.ActualWidth + this.OffsetX;
                        else
                            return this.AdornedElement.ActualWidth - this.adoriningElement.DesiredSize.Width + this.OffsetX;
                    }
                case HorizontalAlignment.Center:
                    {
                        return (this.AdornedElement.ActualWidth / 2) - (this.adoriningElement.DesiredSize.Width / 2) + this.OffsetX;
                    }
                case HorizontalAlignment.Stretch:
                    {
                        return 0D;
                    }
            }

            return 0.0;
        }

        private double DetermineY()
        {
            switch (this.adoriningElement.VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    {
                        if (this.Placement == AdornerPlacement.Outside)
                            return - this.adoriningElement.DesiredSize.Height + this.OffsetY;
                        else
                            return this.OffsetY;
                    }
                case VerticalAlignment.Bottom:
                    {
                        if (this.Placement == AdornerPlacement.Outside)
                            return this.AdornedElement.ActualHeight + this.OffsetY;
                        else
                            return this.AdornedElement.ActualHeight - this.adoriningElement.DesiredSize.Height + this.OffsetY;
                    }
                case VerticalAlignment.Center:
                    {
                        return (this.adoriningElement.DesiredSize.Height / 2) - (this.adoriningElement.DesiredSize.Height / 2) + this.OffsetY;
                    }
                case VerticalAlignment.Stretch:
                    {
                        return 0D;
                    }
            }

            return 0.0;
        }

        private double DetermineWidth()
        {
            if (!Double.IsNaN(PositionX))
            {
                return this.adoriningElement.DesiredSize.Width;
            }

            switch (this.adoriningElement.HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    {
                        return this.adoriningElement.DesiredSize.Width;
                    }
                case HorizontalAlignment.Right:
                    {
                        return this.adoriningElement.DesiredSize.Width;
                    }
                case HorizontalAlignment.Center:
                    {
                        return this.adoriningElement.DesiredSize.Width;
                    }
                case HorizontalAlignment.Stretch:
                    {
                        return AdornedElement.ActualWidth;
                    }
            }

            return 0D;
        }

        private double DetermineHeight()
        {
            if (!Double.IsNaN(PositionY))
            {
                return this.adoriningElement.DesiredSize.Height;
            }

            switch (this.adoriningElement.VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    {
                        return this.adoriningElement.DesiredSize.Height;
                    }
                case VerticalAlignment.Bottom:
                    {
                        return this.adoriningElement.DesiredSize.Height;
                    }
                case VerticalAlignment.Center:
                    {
                        return this.adoriningElement.DesiredSize.Height;
                    }
                case VerticalAlignment.Stretch:
                    {
                        return AdornedElement.ActualHeight;
                    }
            }

            return 0D;
        }
    }
}
