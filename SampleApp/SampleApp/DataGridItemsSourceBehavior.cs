using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Threading.Tasks;


namespace SampleApp
{
    public class DataGridItemsSourceBehavior : Behavior<DataGrid>
    {
        public static readonly DependencyProperty ColumnsSourceProperty = DependencyProperty.Register("ColumnsSource", typeof(string), typeof(DataGridItemsSourceBehavior), 
                                                                                            new FrameworkPropertyMetadata(String.Empty, new PropertyChangedCallback(OnColumnsSourceChanged)));
        public string ColumnsSource
        {
            get { return (string)GetValue(ColumnsSourceProperty); }
            set { SetValue(ColumnsSourceProperty, value); }
        }
        private static void OnColumnsSourceChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var control = obj as DataGridItemsSourceBehavior;
            if (control != null && args.NewValue != null)
                control.ColumnsSource = args.NewValue.ToString();
        }

        protected override void OnAttached()
        {
            this.AssociatedObject.Loaded += OnLoaded;
        }

        protected override void OnDetaching()
        {
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            string columnsSource = this.ColumnsSource;
            IList<GridColumnDef> list = null;
            Task task = Task.Factory.StartNew(() =>
            {
                list = new List<GridColumnDef>(this.ParseGridColumnDefFile(columnsSource));
            });

            task.ContinueWith((t) =>
            {
                this.CreateColumn(list);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private IList<GridColumnDef> ParseGridColumnDefFile(string columnsSource)
        {
            IList<GridColumnDef> list = new List<GridColumnDef>();

            if (String.IsNullOrEmpty(columnsSource))
                return list;

            string fileName = "..\\Config\\" + columnsSource + ".csv";

            if (!File.Exists(fileName))
                return list;

            using (TextReader textReader = new StreamReader(fileName))
            {
                string line;
                while ((line = textReader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');

                    if (3 < data.Length)
                    {

                        int width = 0;
                        int.TryParse(data[2], out width);
                        ColumnType columnType = ColumnType.Text;

                        switch (data[3])
                        {
                            case "Template":
                                columnType = ColumnType.Template;
                                break;
                            case "Int":
                                columnType = ColumnType.Int;
                                break;
                            case "Currency":
                                columnType = ColumnType.Currency;
                                break;
                            case "DateTime":
                                columnType = ColumnType.DateTime;
                                break;
                        }

                        GridColumnDef gridColumnDef = new GridColumnDef(data[0], data[1], width, columnType);

                        list.Add(gridColumnDef);
                    }
                }
            }
            return list;
        }

        private void CreateColumn(IList<GridColumnDef> list)
        {
            if (list == null)
                return;

            foreach (var column in list)
            {
                this.GenerateDataGridColumn(column);
            }
        }

        public void GenerateDataGridColumn(GridColumnDef column)
        {
            System.Windows.Controls.DataGridColumn col = null;

            switch (column.Type)
            {
                case ColumnType.Text:    
                    col = new DataGridTextColumn();
                    break;
                case ColumnType.Int:
                case ColumnType.Currency:
                    col = new DataGridTextColumn(); 
                    break;
                case ColumnType.Check:    
                    col = new DataGridCheckBoxColumn();
                    break;
                case ColumnType.Combo:    
                    col = new DataGridComboBoxColumn();
                    break;
                case ColumnType.Template:    //Template
                    col = new DataGridTemplateColumn();
                    //((DataGridTemplateColumn)col).CellTemplate = dataTemplate as DataTemplate;
                    break;
            }

            if (col != null)
            {
                Binding binding = null;
                if (!String.IsNullOrEmpty(column.Path))
                    binding = new Binding(column.Path);
                if (!String.IsNullOrEmpty(column.Header))
                    col.Header = column.Header;

                col.Width = column.Width;
               
                if (col is DataGridBoundColumn)
                {
                    ((DataGridBoundColumn)col).Binding = binding;
                }
                this.AssociatedObject.Columns.Add(col);
            }
        }
    }
}
