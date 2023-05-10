using SFY_Word_Book.Common.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.ViewModels
{
    public class AboutViewModel
    {
        public AboutViewModel() 
        {
            ShowGithubCommand = new Command(ShowGithub);
            ShowGameCoreCommand = new Command(ShowGameCore);
        }
        public Command ShowGithubCommand { get; set; }
        public void ShowGithub()
        {
            var flow = "https://github.com/ShuFengYingt";
            var sInfo = new ProcessStartInfo(flow)
            {
                UseShellExecute = true,
            };
            Process.Start(sInfo);
        }public Command ShowGameCoreCommand { get; set; }
        public void ShowGameCore()
        {
            var flow = "https://www.gcores.com/users/460496/talks";
            var sInfo = new ProcessStartInfo(flow)
            {
                UseShellExecute = true,
            };
            Process.Start(sInfo);
        }
    }
}
