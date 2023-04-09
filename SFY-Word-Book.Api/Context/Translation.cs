using System.ComponentModel.DataAnnotations;

namespace SFY_Word_Book.Api.Context
{
    public class Translation
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int TransRank { get; set; }
        public int WordId { get; set; }


        /// <summary>
        /// 词性
        /// </summary>
        public string PartOfSpeech { get;set; }

        /// <summary>
        /// 译文
        /// </summary>
        public string TransCN { get; set; }


        /// <summary>
        /// 单词的序号
        /// </summary>
        public string WordRank { get; set; }

        /// <summary>
        /// 单词内容
        /// </summary>
        public Word Word { get; set; }

        [Key]
        public int Id { get; set; }

    }
}
