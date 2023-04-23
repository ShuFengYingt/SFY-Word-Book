using SFY_Word_Book.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.WordBook
{
    public class ToDayReviewWords
    {
        public ToDayReviewWords() { ReadReviewWordBook(); }

        public static ObservableCollection<WordRoot.Root> TodayReviewWords { get; set; } = new ObservableCollection<WordRoot.Root>();

        /// <summary>
        /// 在ReviewWordBook的基础上，读取当日需要记忆的单词
        /// </summary>
        public static void ReadReviewWordBook()
        {
            for(int i = 0;i < ReviewWordBook.ReviewWords.Count;i++)
            {
                if (ReviewWordBook.ReviewWords[i].ReviewDays == DateTime.Today)
                {
                    //迁移至今日待复习
                    TodayReviewWords.Add(ReviewWordBook.ReviewWords[i]);
                    //从其中去除
                    ReviewWordBook.ReviewWords.RemoveAt(i);
                }
            }
        }
    }
}
