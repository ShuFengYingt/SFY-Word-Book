using System.ComponentModel.DataAnnotations;

namespace SFY_Word_Book.Api.Context
{
    public class Word
    {
        /// <summary>
        /// 内容
        /// </summary>
        public string HeadWord { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int WordRank { get; set; }

        /// <summary>
        /// 音标
        /// </summary>
        public string PhoneticSymbol { get; set; }

        /// <summary>
        /// 发音参数请求
        /// </summary>
        public string PhoneSpeech { get; set; }

        /// <summary>
        /// 连续正确次数
        /// </summary>
        public int Combo { get; set; }

        /// <summary>
        /// 是否已经学过
        /// </summary>
        public bool IsLearned { get; set; }

        /// <summary>
        /// 所在组群
        /// </summary>
        public int GroupID { get; set; }

        /// <summary>
        /// 例句
        /// </summary>
        public ICollection<Sentence> Sentences { get; set; }    

        /// <summary>
        /// 释义
        /// </summary>
        public ICollection<Translation> Translations { get; set; }

        [Key]
        public int Id { get; set; }


    }
}
