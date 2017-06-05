using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Diagnostics;
using System.ComponentModel.Composition;
using Microsoft.Practices.ServiceLocation;
using Prism.Mvvm;
using Prism.Commands;
using Hierarchy.Interfaces;
using Prism.Regions;
using Hierarchy.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Hierarchy.ViewModels
{
    [Export(typeof(IShellViewModel))]
    public class MainWindowModel : BindableBase, IShellViewModel
    {
        private readonly IServiceLocator serviceLocator;
        private readonly IRegionManager regionManager;

        public DelegateCommand ControlLoaded { get; set; }


        [ImportingConstructor]
        public MainWindowModel(IServiceLocator serviceLocator, IRegionManager regionManager)
        {
            this.serviceLocator = serviceLocator;
            this.regionManager = regionManager;

            this.ControlLoaded = new DelegateCommand(OnControlLoaded);
        }

        private void OnControlLoaded()
        {
            if (this.regionManager != null)
            {
                //var vm = serviceLocator.GetInstance<IViewModelBase>("TreeViewModel");
                this.regionManager.RequestNavigate("MainRegion", "Tree");
            }
        }

        /// /////////////////////////////////////////////////////////////////////
        /// /////////////////////////////////////////////////////////////////////
        /// /////////////////////////////////////////////////////////////////////

        private IList<TreeItem> items;
        private void DeSerializeGrid()
        {
            try
            {
                using (var fs = new FileStream("e:\\temp\\gridhierarchy.xml", FileMode.Open))
                {
                    XmlSerializer xmls = new XmlSerializer(typeof(TreeItem));
                    var items = xmls.Deserialize(fs) as TreeItem;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private void SerializeGrid()
        {
            try
            {
                using (var fs = new FileStream("d:\\temp\\gridhierarchy.xml", FileMode.Create, FileAccess.Write))
                {
                    XmlSerializer xmls = new XmlSerializer(typeof(TreeItem));
                    xmls.Serialize(fs, items);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private void JsonSerialize()
        {
            var data = JsonConvert.SerializeObject(items);

            var tree = JsonConvert.DeserializeObject<TreeItem>(data);
        }

        private void SortString()
        {
            string input = "sfwefuv8e";
            var output = input.ToArray().OrderBy(p => p);

        }

        /// /////////////////////////////////////////////////////////////////////
        /// /////////////////////////////////////////////////////////////////////
        /// /////////////////////////////////////////////////////////////////////
    }
}