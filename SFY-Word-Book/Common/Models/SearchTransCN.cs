using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.Common.Models
{
    public class SearchTransCN : BindableBase
    {
        private string partOfSpeech;
        public string PartOfSpeech { get { return partOfSpeech; } set { partOfSpeech = value; RaisePropertyChanged(); } }

        private string transCN;
        public string TransCN { get {  return transCN; } set {  transCN = value; RaisePropertyChanged(); } }
    }
}
