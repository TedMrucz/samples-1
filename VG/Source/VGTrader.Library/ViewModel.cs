using Prism.Mvvm;
using VGTrader.Common;

namespace VGTrader.Library
{
	public abstract class ViewModel : BindableBase
	{
		public IView View
		{
			get;
			private set;
		}

		protected ViewModel(IView view)
		{
			this.View = view;
			this.View.DataContext = this;
		}
	}
}
