using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace SFY_Word_Book.Api.Context
{
    public class Sentence
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int SentenceRank { get; set; }


        public int WordId { get; set; }


        /// <summary>
        /// 内容
        /// </summary>
        public string SentenceContent { get; set; }

        /// <summary>
        /// 翻译
        /// </summary>
        public string SentenceCn { get; set; }

        /// <summary>
        /// 单词的序号
        /// </summary>
        public string WordRank { get;set; }

        /// <summary>
        /// 单词内容
        /// </summary>
        public Word Word { get; set; }

        [Key]
        public int Id { get; set; }


    }
}
