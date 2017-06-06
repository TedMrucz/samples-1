using ItemsFilter.Model;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ItemsFilter.View
{
    /// <summary>
    /// Defile View control for IMultiValueFilter model.
    /// </summary>
    [ModelView]
    [TemplatePart(Name = MultiValueFilterView.PART_ItemsTemplateName, Type = typeof(ListBox))]
    public class MultiValueFilterView : FilterViewBase<IMultiValueFilter>
    {
         public const string PART_ItemsTemplateName = "PART_Items";
         static MultiValueFilterView()
         {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MultiValueFilterView), new FrameworkPropertyMetadata(typeof(MultiValueFilterView)));
        }
        private bool isModelAttached;
        private ListBox itemsCtrl;
        /// <summary>
        /// Create new instance of MultiValueFilterView;
        /// </summary>
        public MultiValueFilterView()
        {
            this.Unloaded += MultiValueFilterView_Unloaded;
            this.Loaded += MultiValueFilterView_Loaded;
        }
        /// <summary>
        /// Create new instance of MultiValueFilterView and accept model.
        /// </summary>
        /// <param name="model">IMultiValueFilter model</param>
        public MultiValueFilterView(object model):this()
        {
            base.Model = model as IMultiValueFilter;
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the Model property.
        /// </summary>
        protected override void OnModelChanged(IMultiValueFilter oldModel, IMultiValueFilter newModel)
        {
            DetachModel(itemsCtrl, oldModel);
            AttachModel(itemsCtrl, newModel);
        }
        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            DetachModel(itemsCtrl, Model);
            base.OnApplyTemplate();
            itemsCtrl = GetTemplateChild(MultiValueFilterView.PART_ItemsTemplateName) as ListBox;
            AttachModel(itemsCtrl, Model); 
        }
        private void MultiValueFilterView_Loaded(object sender, RoutedEventArgs e)
        {
            AttachModel(itemsCtrl, Model);
        }
        private void MultiValueFilterView_Unloaded(object sender, RoutedEventArgs e)
        {
            DetachModel(itemsCtrl, Model);
        }
        private void AttachModel(ListBox itemsCtrl, IMultiValueFilter newModel)
        {
            if (!isModelAttached && itemsCtrl != null && newModel != null) 
            {
                if (DesignerProperties.GetIsInDesignMode(this)) 
                {
                    var enumerator = newModel.AvailableValues.GetEnumerator();
                    if (enumerator.MoveNext())
                        itemsCtrl.SelectedItems.Add(enumerator.Current);
                    return;
                }
                IList selectedItems = itemsCtrl.SelectedItems;
                selectedItems.Clear();
                foreach (var item in newModel.SelectedValues)
                {
                    selectedItems.Add(item);
                }
                itemsCtrl.SelectionChanged += newModel.SelectedValuesChanged;
                ((INotifyCollectionChanged)(newModel.SelectedValues)).CollectionChanged += MultiValueFilterView_CollectionChanged;

                isModelAttached = true;
            }
        }
        private void MultiValueFilterView_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            itemsCtrl.SelectionChanged -= Model.SelectedValuesChanged;
            if (e.Action == NotifyCollectionChangedAction.Reset) 
            {
                itemsCtrl.SelectedItems.Clear();
            }
            else 
            {
                if (e.OldItems != null)
                {
                    foreach (var item in e.OldItems)
                    {
                        int itemIndex = itemsCtrl.SelectedItems.IndexOf(item);
                        if (itemIndex >= 0)
                            itemsCtrl.SelectedItems.RemoveAt(itemIndex);
                    }
                }
                if (e.NewItems != null) 
                {
                    foreach (var item in e.NewItems)
                    {
                        int itemIndex = itemsCtrl.SelectedItems.IndexOf(item);
                        if (itemIndex < 0)
                            itemsCtrl.SelectedItems.Add(item);
                    }
                }
            }
            itemsCtrl.SelectionChanged += Model.SelectedValuesChanged;
        }
        private void DetachModel(ListBox itemsCtrl, IMultiValueFilter oldModel)
        {
            if (isModelAttached && itemsCtrl != null && oldModel != null)
            {
                ((INotifyCollectionChanged)(oldModel.SelectedValues)).CollectionChanged -= MultiValueFilterView_CollectionChanged;
                itemsCtrl.SelectionChanged -= oldModel.SelectedValuesChanged;
                isModelAttached = false;
            }
        }
    }
}
