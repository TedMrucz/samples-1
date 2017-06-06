using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Glass.UX.Windows.Controls;


namespace Glass.UX.Behaviours
{
    public class DataGridDynamicBehavior : Behavior<DataGrid>
    {
        public static readonly DependencyProperty ColumnSetProperty = DependencyProperty.Register("ColumnSet", typeof(string), typeof(DataGridDynamicBehavior), new PropertyMetadata(null));
        public string ColumnSet
        {
            get { return (string)GetValue(ColumnSetProperty); }
            set { SetValue(ColumnSetProperty, value); }
        }

        protected override void OnAttached()
        {
            //LoadData(this.ColumnSet);
        }

        protected override void OnDetaching()
        {
        }

		//private void LoadData(string columnSet)
		//{
		//	var dataGrid =  TradeLink.DBContext.DBService.Data.DataGridGroups.Expand("DataGridColumns").Where(p => p.Name == columnSet).FirstOrDefault();
		//	this.CreateColumn(dataGrid);
		//}

		//private void CreateColumn(DataGridGroup dataGrid)
		//{
		//	var dgcg = new DataGridColumnGen();
		//	foreach (var column in dataGrid.DataGridColumns)
		//	{
		//		dgcg.Width = column.Width;
		//		dgcg.Type = column.Type;
		//		dgcg.Sequence = column.Sequence;
		//		dgcg.UpdateTriggerId = column.UpdateTriggerId;
		//		dgcg.HasFilter = column.HasFilter;
		//		dgcg.Header = column.Header;
		//		dgcg.Path = column.Path;
		//		dgcg.Format = column.Format;
		//		dgcg.CellTemplate = column.CellTemplate;
		//		dgcg.CellStyle = column.CellStyle;
		//		if (column.Binding_Id.HasValue)
		//			dgcg.Binding_Id = column.Binding_Id.Value;

		//		this.GenerateDataGridColumn(dgcg);
		//	}
		//}

        public void GenerateDataGridColumn(DataGridColumnGen column)
        {
            System.Windows.Controls.DataGridColumn col = null;

            switch (column.Type)
            {
                case 1:    //Text
                    col = new Glass.UX.Windows.Controls.DataGridTextColumn();
                    break;
                case 2:    //Combo
					col = new Glass.UX.Windows.Controls.DataGridComboBoxColumn();
                    break;
                case 3:    //Check
					col = new Glass.UX.Windows.Controls.DataGridCheckBoxColumn();
                    break;
                case 4:    //Numeric
                    col = new DataGridNumericColumn();
                    break;
                case 5:    //Currency
					col = new DataGridNumericColumn();
                    break;
                case 6:    //Date  
                    col = new DataGridDateColumn();
                    break;
                case 7:    //Template
                    col = new DataGridTemplateColumn();
                    ((DataGridTemplateColumn)col).CellTemplate = AssociatedObject.TryFindResource(column.CellTemplate) as DataTemplate;
                    break;
                case 8:    //Details
                    col = new DataGridDetailColumn();
                    col.Width = column.Width;
                    break;
            }

            if (col != null)
            {
                Binding binding = null;
                if (!String.IsNullOrEmpty(column.Path))
                    binding = new Binding(column.Path);
                if (!String.IsNullOrEmpty(column.Format) && binding != null)
                    binding.StringFormat = column.Format;
                if (!String.IsNullOrEmpty(column.Header))
                    col.Header = column.Header;
                if (!String.IsNullOrEmpty(column.CellStyle))
                    col.CellStyle = AssociatedObject.TryFindResource(column.CellStyle) as Style;

                if (column.HasFilter)
                    col.HeaderStyle = Application.Current.Resources["DataGridColumnFilterHeaderStyle"] as Style;

                if (col is DataGridBoundColumn)
                {
                    ((DataGridBoundColumn)col).Binding = binding;
                }
                AssociatedObject.Columns.Add(col);
            }
        }
    }
}
