using Prism.Mvvm;
using SFY_Word_Book.Common.Models;
using SFY_Word_Book.Extensions;
using SFY_Word_Book.WordBook;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.ViewModels
{
    public class NewWordBookViewModel:BindableBase
    {
        public NewWordBookViewModel() 
        {



            //声明
            NewWords = new ObservableCollection<WordRoot.Root>();


            //显示在页面中
            foreach(var word in NewWordBook.NewWords)
            {
                NewWords.Add(word);
            }

            //订阅更新
            NewWordBook.NewWords.CollectionChanged += OnNewWordBookCollectionChanged;

        }

        private ObservableCollection<WordRoot.Root> newWords;
        /// <summary>
        /// 单词集合
        /// </summary>
        public ObservableCollection<WordRoot.Root> NewWords
        {
            get { return newWords; }    
            set { newWords = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 订阅更新
        /// </summary>
        /// <param name="sendet"></param>
        /// <param name="e"></param>
        private void OnNewWordBookCollectionChanged(object sendet, NotifyCollectionChangedEventArgs  e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    NewWords.Add((WordRoot.Root)item);  
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach(var item in e.OldItems)
                {
                    NewWords.Remove((WordRoot.Root)item);   
                }
            }
        }

        private ObservableCollection<SearchTransCN> transCNs;
        /// <summary>
        /// 单词释义
        /// </summary>
        public ObservableCollection<SearchTransCN> TransCNs
        {
            get { return transCNs; }
            set { transCNs = value; RaisePropertyChanged(); }
        }



    }
}
