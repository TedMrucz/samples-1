using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Media;

namespace SampleApp
{
    public class SampleViewModel : BindableBase
    {
        public DelegateCommand ControlLoaded { get; set; }
        public DelegateCommand LoadData { get; set; }

        private ObservableCollection<SimpleDataModel> items;
        public ObservableCollection<SimpleDataModel> Items
        {
            get { return this.items; }
            set { SetProperty(ref this.items, value); }
        }

        public SampleViewModel()
        {
            this.ControlLoaded = new DelegateCommand(OnControlLoaded);
            this.LoadData = new DelegateCommand(OnLoadData);
        }

        private IList<ChartItem> chartItems = new List<ChartItem>();
        public IList<ChartItem> ChartItems
        {
            get { return chartItems; }
            set { SetProperty(ref chartItems, value);  }
        }

        private void OnControlLoaded()
        {
            ObservableCollection<SimpleDataModel> list = null;
            Task task = Task.Factory.StartNew(() =>
            {
                list = new ObservableCollection<SimpleDataModel>( this.ParseDataFile() );
            });

            task.ContinueWith((t) =>
            {
                this.Items = list;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void OnLoadData()
        {
            Random random = new Random();
            List<ChartItem>  items = new List<ChartItem>();

            for (int i = 0; i < 100; i++)
            {
                items.Add(new ChartItem(random.NextDouble() * 10D, Brushes.AliceBlue));
            }
            this.ChartItems = items;
         }

        private IList<SimpleDataModel> ParseDataFile()
        {
            IList<SimpleDataModel> list = new List<SimpleDataModel>();

            string fileName = "..\\Config\\datafile.csv";

            if (!File.Exists(fileName))
                return list;

            using (TextReader textReader = new StreamReader(fileName))
            {
                string line;
                while ((line = textReader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');

                    if (2 < data.Length)
                    {
                        SimpleDataModel simpleDataMode = new SimpleDataModel();
                        int portfolioID = 0;
                        if (int.TryParse(data[0], out portfolioID))
                            simpleDataMode.PortfolioID = portfolioID;

                        simpleDataMode.PortfolioName = data[1];

                        double portfolioTotal = 0D;
                        if (double.TryParse(data[2], out portfolioTotal))
                            simpleDataMode.PortfolioTotal = portfolioTotal;

                        list.Add(simpleDataMode);
                    }
                }
            }
            return list;
        }
    }

    public class ChartItem
    {
        public ChartItem(double width, Brush brush)
        {
            this.Width = width;
            this.Brush = brush;
        }

        public double Width { get; set; }
        public Brush Brush { get; set; }

    }
}
