using DryIoc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA.ViewModles
{
    public class ViewAViewModle: BindableBase, INavigationAware
    {
        public ViewAViewModle()
        {

        }
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged(); } 
        }



        /// <summary>
        /// 每次重启导航时，该实例是否重新创建
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("Title"))
            {
                Title = navigationContext.Parameters.GetValue<string>("Title");
            }
        }
    }
}
