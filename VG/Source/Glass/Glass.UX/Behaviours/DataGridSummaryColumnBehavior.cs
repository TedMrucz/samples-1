using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Glass.UX.Windows.Controls;

namespace Glass.UX.Behaviours
{
	public class DataGridSummaryColumnBehavior : Behavior<DataGridExt>, IDataGridBehavior
	{
		private DockPanel summaryRow;
		private ICollectionView sourceCollectionView;
		private int itemsCount = 0;

		public DataGridSummaryColumnBehavior()
		{ }

		protected override void OnAttached()
        {
			this.AssociatedObject.SummaryRowBehavior = this;
			this.AssociatedObject.ColumnReordering += OnColumnReordering;
			this.AssociatedObject.ColumnReordered += OnColumnReordered;
        }

        protected override void OnDetaching()
        {
			this.sourceCollectionView.CollectionChanged -= OnNotifyCollectionChanged;
			this.AssociatedObject.ColumnReordering -= OnColumnReordering;
			this.AssociatedObject.ColumnReordered -= OnColumnReordered;

			this.summaryRow.Children.Clear();
        }

		private int columnReorderingIndex = -1;
		private void OnColumnReordering(object sender, DataGridColumnEventArgs args)
		{
			this.columnReorderingIndex = args.Column.DisplayIndex;
		}

		private void OnColumnReordered(object sender, DataGridColumnEventArgs args)
		{
			if (-1 < this.columnReorderingIndex)
			{
				this.ReorderingColumnHeader(this.columnReorderingIndex, args.Column.DisplayIndex);
				this.columnReorderingIndex = -1;
			}
		}

		public void OnLoad(DockPanel summaryRow)
        {
            if (summaryRow == null)
                return;

			this.summaryRow = summaryRow;

			this.sourceCollectionView = this.AssociatedObject.ItemsSource as ICollectionView;
			if (this.sourceCollectionView == null)
				this.sourceCollectionView = CollectionViewSource.GetDefaultView(this.AssociatedObject.ItemsSource);

			if (this.sourceCollectionView == null)
				return;

			this.sourceCollectionView.CollectionChanged += OnNotifyCollectionChanged;
			ReadOnlyCollection<ItemPropertyInfo> itemProperties = ((IItemProperties)sourceCollectionView).ItemProperties; 

            foreach (DataGridColumn column in this.AssociatedObject.Columns)
            {
				DockPanel stackPanel = new DockPanel();
				stackPanel.HorizontalAlignment = HorizontalAlignment.Left;
				TextBlock pipe = new TextBlock();
				pipe.Text = "|";

				stackPanel.Children.Add(pipe);
                TextBlock element = new TextBlock();
				element.Padding = new Thickness(0, 0, 2, 0);
                element.TextAlignment = TextAlignment.Right;

				stackPanel.Children.Add(element);
				
				stackPanel.Background = Brushes.Bisque;
                
                Binding widthBinding = new Binding("ActualWidth");
                widthBinding.Mode = BindingMode.OneWay;
                widthBinding.Source = column;
				BindingOperations.SetBinding(stackPanel, StackPanel.WidthProperty, widthBinding);

				summaryRow.Children.Add(stackPanel);

                if (column is DataGridBoundColumn && column is DataGridNumericColumn)
                {
                    DataGridNumericColumn numericColumn = (DataGridNumericColumn)column;
                    if (numericColumn.IsTotaled)
                    {
						ItemPropertyInfo propertyInfo = itemProperties.FirstOrDefault(item => item.Name == (string)column.Header);
						Func<object, object> getterItem = ((PropertyDescriptor)(propertyInfo.Descriptor)).GetValue;
						Func<object, decimal> getter = row => ((decimal)getterItem(row));
						numericColumn.Getter = getter;

						Binding binding = new Binding("Total");
						binding.Mode = BindingMode.OneWay;
						binding.StringFormat = numericColumn.Binding.StringFormat;
						binding.Source = column;

						element.SetBinding(TextBlock.TextProperty, binding);
                    }
                }
            }
        }

		private void OnNotifyCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			int count = ((CollectionView)sender).Count;
			if (count != this.itemsCount)
			{
				this.UpdateTotals();
				this.itemsCount = count;
			}
		}

		private void UpdateTotals()
		{
			foreach (DataGridColumn column in this.AssociatedObject.Columns)
			{
				if (column is DataGridBoundColumn && column is DataGridNumericColumn)
				{
					DataGridNumericColumn numericColumn = (DataGridNumericColumn)column;
					if (numericColumn.IsTotaled)
					{
						numericColumn.Total = this.sourceCollectionView.OfType<object>().AsParallel().Sum(item => numericColumn.Getter(item));
					}
				}
			}
		}

		public void ReorderingColumnHeader(int original, int destination)
		{
			UIElement child = this.summaryRow.Children[original];
			this.summaryRow.Children.RemoveAt(original);
			this.summaryRow.Children.Insert(destination, child); 
		}
	}
}