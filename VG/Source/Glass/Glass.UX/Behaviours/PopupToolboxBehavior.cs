using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;

namespace Glass.UX.Behaviours
{
	public class PopupToolboxBehavior : Behavior<Popup>
	{
		protected override void OnAttached()
		{
			base.AssociatedObject.MouseEnter += OnMouseEnter;
			base.AssociatedObject.LostFocus += OnLostFocus;
		}

		protected override void OnDetaching()
		{
			base.AssociatedObject.MouseEnter -= OnMouseEnter;
			base.AssociatedObject.LostFocus -= OnLostFocus;
		}

		public static DependencyProperty IsPopupOpenProperty = DependencyProperty.Register("IsPopupOpen", typeof(bool), typeof(PopupToolboxBehavior),
												  new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnIsPopupOpenChanged)));

		public bool IsPopupOpen
		{
			get { return (bool)GetValue(IsPopupOpenProperty); }
			set
			{
				if (!value && base.AssociatedObject.IsMouseOver)
					return;

				SetValue(IsPopupOpenProperty, value);
				base.AssociatedObject.IsOpen = value;
			}
		}

		private static void OnIsPopupOpenChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			PopupToolboxBehavior control = obj as PopupToolboxBehavior;
			if (control != null && args.NewValue != null)
				control.IsPopupOpen = (bool)args.NewValue;
		}

		private void OnMouseEnter(object sender, RoutedEventArgs e)
		{
			base.Attach((Popup)sender);
			base.AssociatedObject.Focus();
		}

		private void OnLostFocus(object sender, RoutedEventArgs e)
		{
			this.IsPopupOpen = false;
		}
	}
}
