using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;
using Glass.UX.Windows.Controls;


namespace Glass.UX.Behaviours
{
    public class DataGridRowDetailsBehavior : Behavior<DataGrid>
    {
        public static readonly DependencyProperty ColumnSetProperty = DependencyProperty.Register("ColumnSet", typeof(string), typeof(DataGridDynamicBehavior), new PropertyMetadata(null));
        public string ColumnSet
        {
            get { return (string)GetValue(ColumnSetProperty); }
            set { SetValue(ColumnSetProperty, value); }
        }

        protected override void OnAttached()
        {
        }

        protected override void OnDetaching()
        {
        }

		public static DependencyProperty DetailsVisibilityProperty = DependencyProperty.Register("DetailsVisibility", typeof(bool), typeof(DataGridExt),
														  new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnDetailsVisibilityChanged)));

		public bool DetailsVisibility
		{
			get { return (bool)GetValue(DetailsVisibilityProperty); }
			set
			{
				SetValue(DetailsVisibilityProperty, value);

				DataGridDetailColumn detailColumn = this.AssociatedObject.Columns[0] as DataGridDetailColumn;
				if (detailColumn != null)
					detailColumn.ClearDetailsVisibility();
			}
		}

		private static void OnDetailsVisibilityChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			DataGridExt control = obj as DataGridExt;
			if (control != null && args.NewValue != null)
				control.DetailsVisibility = (bool)args.NewValue;
		}

		public bool HasRowDetails
		{
			get { return (bool)GetValue(HasRowDetailsProperty); }
			set { SetValue(HasRowDetailsProperty, value); }
		}

		public static readonly DependencyProperty HasRowDetailsProperty =
						DependencyProperty.Register("HasRowDetails", typeof(bool), typeof(DataGridExt), new PropertyMetadata(false, new PropertyChangedCallback(OnHasRowDetailsChanged)));

		private static void OnHasRowDetailsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			DataGridExt p = d as DataGridExt;
			if (p != null && e.NewValue != null)
				p.HasRowDetails = (bool)e.NewValue;
		}

		private void OnItemsChanged(object sender, System.Windows.Controls.Primitives.ItemsChangedEventArgs e)
		{
			if (0 < this.AssociatedObject.Items.Count)
			{
				if (this.AssociatedObject.RowDetailsTemplate == null && this.HasRowDetails)
				{
					this.CreateDataGridRowDetailsTemplate();
				}
			}
		}

		       /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////
        // DataGridColumn
        //    DataGridComboBoxColumn
        //    DataGridTemplateColumn
        //    DataGridBoundColumn
        //       DataGridCheckBoxColumn
        //       DataGridHyperlinkColumn
        //       DataGridTextColumn
        /// </summary>
        private void CreateDataGridRowDetailsTemplate()
        {
            IList<ColumnData> columns = new List<ColumnData>();

            foreach (DataGridColumn column in this.AssociatedObject.Columns)
            {
                Binding binding = null;
                string path = String.Empty;
                string stringFormat = String.Empty;
                IEnumerable comboSource = new List<string>();

                Type type = column.GetType();
                string header = (column.Header == null) ? String.Empty : column.Header.ToString();
                if (column is DataGridBoundColumn)
                {
                    DataGridBoundColumn boundColumn = column as DataGridBoundColumn;
                    binding = boundColumn.Binding as Binding;
                    if (binding != null)
                    {
                        path = binding.Path.Path;
                        stringFormat = binding.StringFormat;
                    }
                }
                else if (column is Glass.UX.Windows.Controls.DataGridComboBoxColumn)
                {
                    Glass.UX.Windows.Controls.DataGridComboBoxColumn boundColumn = column as Glass.UX.Windows.Controls.DataGridComboBoxColumn;
                    binding = boundColumn.SelectedItemBinding as Binding;
                    if (binding != null)
                    {
                        path = binding.Path.Path;
                        comboSource = boundColumn.ItemsSource;
                    }

                }
                columns.Add(new ColumnData(type, binding, header, path, stringFormat, comboSource));
            }

            DataTemplate template = new DataTemplate();

            template.VisualTree = this.CreateContainerlElement(columns);

            this.AssociatedObject.RowDetailsTemplate = template;
        }

        private FrameworkElementFactory CreateContainerlElement(IList<ColumnData> columns)
        {
            Dictionary<int, int> columnSplit = new Dictionary<int, int>();
            columnSplit.Add(1, 12);
            columnSplit.Add(2, 24);
            columnSplit.Add(3, 35);
            columnSplit.Add(4, 60);

            Brush background = Application.Current.Resources["CanvasBackground"] as Brush;

            FrameworkElementFactory borderElement = new FrameworkElementFactory(typeof(Border));
            borderElement.SetValue(Border.BackgroundProperty, background);
            borderElement.SetValue(Border.PaddingProperty, new Thickness(10D));
            borderElement.SetValue(Border.BorderThicknessProperty, new Thickness(0D));

            FrameworkElementFactory stackElement = new FrameworkElementFactory(typeof(StackPanel));
            FrameworkElementFactory columnElement = new FrameworkElementFactory(typeof(StackPanel));
            stackElement.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            int columnStack = 1;
            for (int i = 1; i < 5; i++)
            {
                columnStack = columnSplit[i];
                if (columns.Count < columnStack)
                {
                    columnStack = i + 4;
                    break;
                }
            }

            int column = 0;
            foreach (ColumnData cd in columns)
            {
                if (column < columnStack)
                {
                    column++;
                    this.StackColumnElement(columnElement, cd);
                }
                else
                {
                    stackElement.AppendChild(columnElement);
                    columnElement = new FrameworkElementFactory(typeof(StackPanel));
                    columnElement.SetValue(StackPanel.MarginProperty, new Thickness(10, 0, 0, 0));
                    this.StackColumnElement(columnElement, cd);

                    column = 0;
                }
            }

            stackElement.AppendChild(columnElement);
            borderElement.AppendChild(stackElement);

            return borderElement;
        }

        private void StackColumnElement(FrameworkElementFactory columnElement, ColumnData cd)
        {
            FrameworkElementFactory inputElement = this.CreateStackElement(cd);
            if (inputElement != null)
                columnElement.AppendChild(inputElement);
        }

        #region EntryElements

        private FrameworkElementFactory CreateStackElement(ColumnData cd)
        {
            FrameworkElementFactory stackElement = null;

            switch (cd.Type.Name)
            {
                case "DataGridCheckBoxColumn":
                    stackElement = this.CreateCheckBoxElement(cd);
                    break;
                case "DataGridComboBoxColumn":
                    stackElement = this.CreateComboBoxElement(cd);
                    break;
                case "DataGridDateColumn":
                    stackElement = this.CreateDatePickerElement(cd);
                    break;
                case "DataGridTextColumn":
                case "DataGridNumericColumn":
                    stackElement = this.CreateTextBoxElement(cd, false);
                    break;
                case "DataGridTemplateColumn":
                    stackElement = this.CreateDefaultElement(cd);
                    break;
                case "DataGridDetailColumn":
                    break;
                default:
                    stackElement = this.CreateDefaultElement(cd);
                    break;
            }

            return stackElement;
        }

        private FrameworkElementFactory CreateStackPanelElement(ColumnData cd)
        {
            FrameworkElementFactory stackElement = new FrameworkElementFactory(typeof(StackPanel));
            stackElement.SetValue(StackPanel.MarginProperty, new Thickness(2, 1, 2, 1));
            stackElement.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            FrameworkElementFactory labelElement = new FrameworkElementFactory(typeof(TextBlock));
            labelElement.SetValue(TextBlock.WidthProperty, 100D);
            labelElement.SetValue(TextBlock.TextProperty, cd.Header);

            stackElement.AppendChild(labelElement);

            return stackElement;
        }

        private FrameworkElementFactory CreateTextBoxElement(ColumnData cd, bool isRightAligned)
        {
            FrameworkElementFactory stackElement = this.CreateStackPanelElement(cd);

            FrameworkElementFactory element = new FrameworkElementFactory(typeof(TextBox));
            element.SetValue(TextBox.MarginProperty, new Thickness(2, 0, 2, 0));
            element.SetValue(TextBox.WidthProperty, 220D);
            if (isRightAligned)
                element.SetValue(TextBox.TextAlignmentProperty, TextAlignment.Right);

            Binding binding = new Binding(cd.Path);
            binding.Mode = BindingMode.TwoWay;
            if (!String.IsNullOrEmpty(cd.StringFormat))
                binding.StringFormat = cd.StringFormat;
            element.SetBinding(TextBox.TextProperty, binding);

            stackElement.AppendChild(element);

            return stackElement;
        }

        private FrameworkElementFactory CreateCheckBoxElement(ColumnData cd)
        {
            FrameworkElementFactory stackElement = this.CreateStackPanelElement(cd);

            FrameworkElementFactory element = new FrameworkElementFactory(typeof(CheckBox));
            element.SetValue(CheckBox.MarginProperty, new Thickness(2));

            Binding binding = new Binding();
            binding.Path = new PropertyPath(cd.Path);
            binding.Mode = BindingMode.OneWay;

            element.SetBinding(CheckBox.IsCheckedProperty, binding);

            stackElement.AppendChild(element);

            return stackElement;
        }

        private FrameworkElementFactory CreateDatePickerElement(ColumnData cd)
        {
            FrameworkElementFactory stackElement = this.CreateStackPanelElement(cd);

            FrameworkElementFactory element = new FrameworkElementFactory(typeof(DatePicker));
            element.SetValue(TextBlock.WidthProperty, 220D);

            Binding binding = new Binding();
            binding.Path = new PropertyPath(cd.Path);
            binding.Mode = BindingMode.OneWay;

            element.SetBinding(DatePicker.SelectedDateProperty, binding);

            stackElement.AppendChild(element);

            return stackElement;
        }

        private FrameworkElementFactory CreateComboBoxElement(ColumnData cd)
        {
            FrameworkElementFactory stackElement = this.CreateStackPanelElement(cd);

            FrameworkElementFactory element = new FrameworkElementFactory(typeof(ComboBox));
            element.SetValue(TextBlock.WidthProperty, 220D);
            element.SetValue(ComboBox.ItemsSourceProperty, cd.ComboSource);

            Binding binding = new Binding();
            binding.Path = new PropertyPath(cd.Path);
            binding.Mode = BindingMode.OneWay;

            element.SetBinding(ComboBox.SelectedValueProperty, binding);

            stackElement.AppendChild(element);

            return stackElement;
        }

        private FrameworkElementFactory CreateDefaultElement(ColumnData cd)
        {
            FrameworkElementFactory stackElement = this.CreateStackPanelElement(cd);

            return stackElement;
        }

        #endregion
    }
}