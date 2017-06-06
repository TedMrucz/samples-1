using System;
using Prism.Mvvm;
using Prism.Events;
using VGTrader.Common;

namespace VGTrader.Library
{
    public abstract class ViewModelBase : ViewModelSimple, IViewModelBase
    {
		protected IEventAggregator eventAggregator;
		protected IViewContext viewContext;

        public ViewModelBase() : base()
        {
        }

        protected ViewModelBase(IViewBase view)
        {
            if (view != null)
            {
                this.View = view;
                this.View.Model = this;
                this.Initialize();
            }
        }

        private string name = String.Empty;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        private bool hasError = true;
        public virtual bool HasError
        {
            get { return hasError; }
            set { hasError = value; }
        }

        private bool isActive = false;
        public bool IsActive
        {
            get { return isActive; }
			set { isActive = value; OnPropertyChanged("IsActive"); }
        }

        public virtual void DataObject(object data) { }
        public virtual void SupportObject(object data) { }

        public IViewBase View { get; set; }
        public virtual void Initialize() { }
        public virtual void SetFinalize() { }
		public virtual void SetActiveView(IViewBase view) { }
    }

	//////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////
    public class ViewModelSimple : BindableBase
    {
        public ViewModelSimple()
        {
        }
    }
}
