using System;
using System.Xml.Serialization;
using Prism.Mvvm;

namespace SampleApp
{
    [Serializable]
    public class SimpleDataModel : BindableBase
    {
        private int portfolioID = 0;
        [XmlAttribute]
        public int PortfolioID
        {
            get { return this.portfolioID; }
            set { SetProperty(ref this.portfolioID, value); }
        }

        private string portfolioName = String.Empty;
        [XmlAttribute]
        public string PortfolioName
        {
            get { return this.portfolioName; }
            set { SetProperty(ref this.portfolioName,value); }
        }

        private double portfolioTotal = 0D;
        [XmlAttribute]
        public double PortfolioTotal
        {
            get { return this.portfolioTotal; }
            set { SetProperty(ref this.portfolioTotal, value); }
        }

        public SimpleDataModel()
        { }


    }
}
