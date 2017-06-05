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
using System.ComponentModel.DataAnnotations;


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

            string json = File.ReadAllText("data.json");
            var roles = JsonConvert.DeserializeObject<IList<Role>>(json);
            foreach (Role f in roles)
            {
                //Console.WriteLine("{0} {1} {2}", f.Key, f.Value.Name, f.Value.Hash);
            }
        }

        private void SortString()
        {
            string input = "sfwefuv8e";
            var ret = new String(input.OrderBy(c => c).ToArray());

        }

        /// /////////////////////////////////////////////////////////////////////
        /// /////////////////////////////////////////////////////////////////////
        /// /////////////////////////////////////////////////////////////////////
        /// 

    }

    public partial class Role
    {
        public Role()
        {
            Participants = new HashSet<Participant>();
        }
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [StringLength(255), JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [StringLength(255), JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonIgnore]
        public virtual ICollection<Participant> Participants { get; set; }
    }

    public partial class Participant
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

    }
}