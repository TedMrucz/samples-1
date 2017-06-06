namespace VGTrader.Common
{
	public interface IViewModelBase
	{
		string Name { get; set; }
		bool HasError { get; set; }
		bool IsActive { get; set; }
		IViewBase View { get; set; }
		void DataObject(object data);
		void SupportObject(object data);
		void SetActiveView(IViewBase view);
		void Initialize();
		void SetFinalize();
	}
}
