using Hierarchy.Interfaces;

namespace Hierarchy.ViewModels
{
    public class ViewModelBase
    {
        public ViewModelBase() : base()
        {
        }

        protected ViewModelBase(IViewBase view)
        {
            //if (view != null)
            //{
            //    this.View = view;
            //    this.View.Model = this;
            //}
        }
    }
}
