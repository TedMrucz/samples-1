using System.Windows.Controls;
using System.ComponentModel.Composition;
using VGTrader.Common;


namespace VGTrader.Library
{
    //[Export(typeof(ViewBase))]
    public abstract class ViewBase : UserControl, IViewBase
    {
        public ViewBase()
        {
        }

        public virtual IViewModelBase Model 
        {
            get { return this.DataContext as ViewModelBase; }
            set { this.DataContext = value; }
        }

        public string ViewName { set; get; }
        public string ViewTitle { set; get; }
		public string RegionName { set; get; }
        public virtual void InitializeView() { }

		public virtual void OnValidationError(object sender, ValidationErrorEventArgs e)
		{
			bool hasError = false;

			if (this.DataContext != null && this.DataContext is ViewModelBase)
			{
				((ViewModelBase)this.DataContext).HasError = hasError;
			}
		}
        public bool IsActive 
        {
            set { this.Visibility = System.Windows.Visibility.Visible; }
            get { return this.IsVisible; } 
        }
    }
}
