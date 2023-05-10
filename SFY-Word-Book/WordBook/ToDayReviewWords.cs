using ImTools;
using SFY_Word_Book.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
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
            //查看哪些是今天要复习的，并添加至列表当中
            foreach (var todayReviewWord in ReviewWordBook.ReviewWords)
            {
                if (todayReviewWord.ReviewDays <= DateTime.Today)
                {
                    //迁移至今日待复习
                    TodayReviewWords.Add(todayReviewWord);
                }
            }
            //从ReviewWords中移除,复习后再加入
            foreach (var todayReviewWord in TodayReviewWords)
            {
                ReviewWordBook.ReviewWords.Remove(todayReviewWord);
            }
        }

        public static void CheckOrSetBack()
        {
            //如果今天的记完了
            if (TodayReviewWords.Count == 0)
            {
                return;
            }
            //否则加回ReviewWordBook
            else
            {
                TodayReviewWords.Reverse();
                foreach (WordRoot.Root word in TodayReviewWords)
                {
                    ReviewWordBook.ReviewWords.Insert(0, word);
                }
            }

        }
    }
}
