using Microsoft.VisualStudio.Services.WebApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.Extensions
{
    public class CET4
    {


        public CET4(string filePath)
        {
            ReadJson(filePath);
            BookName = "六级大纲词汇";
        }

        public string BookName { get; private set; }
        TempExtension.Word ListHeadList { get; set; }
        public List<Root> words = new List<Root>();
        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private void ReadJson(string filePath)
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                string line = string.Empty;
                while ((line = r.ReadLine()) != null)
                {
                    Root wordItem = JsonConvert.DeserializeObject<Root>(line);
                    words.Add(wordItem);
                }
            }

            
            

        }




        /// <summary>
        /// 例句
        /// </summary>
        public class Sentences
        {
            /// <summary>
            /// 例句内容
            /// </summary>
            public string sContent { get; set; }
            /// <summary>
            /// 例句释义
            /// </summary>
            public string sCn { get; set; }
        }

        /// <summary>
        /// 例句列表
        /// </summary>
        public class Sentence
        {
            /// <summary>
            /// 例句列表
            /// </summary>
            public List<Sentences> sentences { get; set; }
        }



        /// <summary>
        /// 近义词
        /// </summary>
        public class Hwds
        {
            /// <summary>
            /// 内容
            /// </summary>
            public string w { get; set; }
        }

        /// <summary>
        /// 近义词
        /// </summary>
        public class Synos
        {
            /// <summary>
            /// 词性
            /// </summary>
            public string pos { get; set; }
            /// <summary>
            /// 释义
            /// </summary>
            public string tran { get; set; }
            /// <summary>
            /// 同释义词列表
            /// </summary>
            public List<Hwds> hwds { get; set; }
        }

        /// <summary>
        /// 近义词列表
        /// </summary>
        public class Syno
        {
            /// <summary>
            /// 近义词列表
            /// </summary>
            public List<Synos> synos { get; set; }
        }

        /// <summary>
        /// 短语
        /// </summary>
        public class Phrases
        {
            /// <summary>
            /// 内容
            /// </summary>
            public string pContent { get; set; }
            /// <summary>
            /// 中文
            /// </summary>
            public string pCn { get; set; }
        }

        /// <summary>
        /// 短语列表
        /// </summary>
        public class Phrase
        {
            /// <summary>
            /// 短语列表
            /// </summary>
            public List<Phrases> phrases { get; set; }
        }

        public class Words
        {
            /// <summary>
            /// 内容
            /// </summary>
            public string hwd { get; set; }
            /// <summary>
            /// 翻译
            /// </summary>
            public string tran { get; set; }
        }

        public class Rels
        {
            /// <summary>
            /// 词性
            /// </summary>
            public string pos { get; set; }
            /// <summary>
            /// 单词体
            /// </summary>
            public List<Words> words { get; set; }
        }
        /// <summary>
        /// 同根词
        /// </summary>
        public class RelWord
        {
            /// <summary>
            /// 同根词
            /// </summary>
            public List<Rels> rels { get; set; }
        }
        /// <summary>
        /// 释义
        /// </summary>
        public class Trans
        {
            /// <summary>
            /// 释义中文
            /// </summary>
            public string tranCn { get; set; }
            /// <summary>
            /// 词性
            /// </summary>
            public string pos { get; set; }
        }

        public class Contents
        {
            /// <summary>
            /// 例句
            /// </summary>
            public Sentence sentence { get; set; }
            /// <summary>
            /// 美式发音
            /// </summary>
            public string usphone { get; set; }
            /// <summary>
            /// 英式发音请求url
            /// </summary>
            public string ukspeech { get; set; }
            /// <summary>
            /// 美式发音请求url
            /// </summary>
            public string usspeech { get; set; }
            /// <summary>
            /// 近义词
            /// </summary>
            public Syno syno { get; set; }
            /// <summary>
            /// 英式音标
            /// </summary>
            public string ukphone { get; set; }
            /// <summary>
            /// 短语
            /// </summary>
            public Phrase phrase { get; set; }
            /// <summary>
            /// 同根词
            /// </summary>
            public RelWord relWord { get; set; }
            /// <summary>
            /// 释义
            /// </summary>
            public List<Trans> trans { get; set; }
        }

        /// <summary>
        /// 详细内容Word，请继续展开
        /// </summary>
        public class Word
        {
            /// <summary>
            /// 继续展开
            /// </summary>
            public Contents content { get; set; }
        }

        /// <summary>
        /// 一级内容，请继续展开，次级为Word
        /// </summary>
        public class Content
        {
            /// <summary>
            /// 详细内容，继续展开
            /// </summary>
            public Word word { get; set; }
        }

        /// <summary>
        /// 根类
        /// </summary>
        public class Root
        {
            /// <summary>
            /// 单词序号
            /// </summary>
            public int wordRank { get; set; }
            /// <summary>
            /// 单词名
            /// </summary>
            public string headWord { get; set; }
            /// <summary>
            /// 详细内容，一级，需要继续展开，很麻烦
            /// </summary>
            public Content content { get; set; }
        }
    }
}
