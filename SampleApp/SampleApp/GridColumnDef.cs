using System;
using System.Xml.Serialization;
using Prism.Mvvm;

namespace SampleApp
{
    public enum ColumnType { Text, Int, Currency, DateTime, Template, Combo, Check }

    public class GridColumnDef : BindableBase
    {
        public GridColumnDef(string header, string path, int width, ColumnType columnType )
        {
            this.Header = header;
            this.Path = path;
            this.Width = width;
            this.Type = columnType;
        }

        public string Header { get; set; }
        public string Path { get; set; }
        public int Width { get; set; }
        public ColumnType Type { get; set; }
    }
}
