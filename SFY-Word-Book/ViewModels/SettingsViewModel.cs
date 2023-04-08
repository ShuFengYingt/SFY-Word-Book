using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SFY_Word_Book.Common.Models;
using SFY_Word_Book.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.ViewModels
{
    public class SettingsViewModel:BindableBase
    {
        public SettingsViewModel(IRegionManager regionManager) 
        { 
            //初始化
            SettingsBars = new ObservableCollection<SettingsBar>();
            this.regionManager = regionManager;
            SettingsNavigation = new DelegateCommand<SettingsBar>(SettingsNavigate);
            CreateSettingsBar();
            
        }

        /*导航区*/
        public DelegateCommand<SettingsBar> SettingsNavigation { get; set; }
        private ObservableCollection<SettingsBar> settingsBars;
        public ObservableCollection<SettingsBar> SettingsBars 
        { 
            get { return settingsBars; } 
            private set { settingsBars=value;RaisePropertyChanged(); } 
        }

        IRegionManager regionManager;

        void CreateSettingsBar()
        {
            SettingsBars.Add(new SettingsBar { Icon = "Palette", Title = "个性化", NameSpace = "SkinView" });
            SettingsBars.Add(new SettingsBar { Icon = "Information", Title = "关于", NameSpace = "AboutView" });
        }

        private void SettingsNavigate(SettingsBar settingsBar)
        {
            if (settingsBar == null || string.IsNullOrWhiteSpace(settingsBar.NameSpace))
            {
                return;
            }
            regionManager.Regions[PrismManager.SettingsViewRegionName].RequestNavigate(settingsBar.NameSpace);
        }
    }
}
