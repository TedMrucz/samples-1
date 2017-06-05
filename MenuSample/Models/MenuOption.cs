using System;
using Prism.Mvvm;

namespace MenuSample.Models
{
    public class MenuOption : BindableBase
    {
        public MenuOption() { }
        public MenuOption(string option) 
        {
            this.Option = option;
        }

        private string option;
        public string Option { get { return option; } set { option = value; base.OnPropertyChanged(() => this.Option); } }
    }
}

