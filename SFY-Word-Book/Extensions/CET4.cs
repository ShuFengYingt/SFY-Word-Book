using Microsoft.VisualStudio.Services.WebApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
        }

        public string BookName { get; private set; }
        TempExtension.Word ListHeadList { get; set; }
        public List<Word> words = new List<Word>();
        public Word Word_1 { get; private set; }
        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private void ReadJson(string filePath)
        {
            string json = string.Empty;
            using (FileStream file = new FileStream(filePath,FileMode.OpenOrCreate,FileAccess.ReadWrite,FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(file,Encoding.UTF8)) 
                {
                    json = sr.ReadToEnd().ToString();
                }
            }
            Word_1 = JsonConvert.DeserializeObject<Word>("{\"wordRank\":1,\"headWord\":\"refuse\",\"content\":{\"word\":{\"wordHead\":\"refuse\",\"wordId\":\"CET4luan_2_1\",\"content\":{\"sentence\":{\"sentences\":[{\"sContent\":\"She asked him to leave, but he refused.\",\"sCn\":\"她叫他走，但他不肯。\"},{\"sContent\":\"When he offered all that money, I could hardly refuse (= could not refuse ) , could I?\",\"sCn\":\"他愿意给那么多钱，我怎么可能拒绝呢？\"}],\"desc\":\"例句\"},\"usphone\":\"ri'fjʊz\",\"syno\":{\"synos\":[{\"pos\":\"n\",\"tran\":\"垃圾；[环境]废物\",\"hwds\":[{\"w\":\"garbage\"},{\"w\":\"waste\"},{\"w\":\"junk\"},{\"w\":\"rubbish\"},{\"w\":\"trash\"}]},{\"pos\":\"vt\",\"tran\":\"拒绝；不愿；抵制\",\"hwds\":[{\"w\":\"negative\"},{\"w\":\"turn down\"}]},{\"pos\":\"vi\",\"tran\":\"拒绝\",\"hwds\":[{\"w\":\"object\"},{\"w\":\"turn down\"}]}],\"desc\":\"同近\"},\"ukphone\":\"rɪ'fjuːz\",\"ukspeech\":\"refuse&type=1\",\"star\":0,\"phrase\":{\"phrases\":[{\"pContent\":\"municipal refuse\",\"pCn\":\"城市垃圾\"},{\"pContent\":\"refuse to do\",\"pCn\":\"拒绝做某事\"},{\"pContent\":\"refuse treatment\",\"pCn\":\"垃圾处理；废物处理\"},{\"pContent\":\"refuse collection\",\"pCn\":\"垃圾收集\"},{\"pContent\":\"refuse disposal\",\"pCn\":\"废物处理\"},{\"pContent\":\"coal refuse\",\"pCn\":\"煤矸石\"},{\"pContent\":\"refuse dump\",\"pCn\":\"n. 垃圾场\"},{\"pContent\":\"refuse transfer station\",\"pCn\":\"垃圾转运站；废物转运站\"}],\"desc\":\"短语\"},\"phone\":\"ri'fju:z, ri:-\",\"speech\":\"refuse\",\"relWord\":{\"desc\":\"同根\",\"rels\":[{\"pos\":\"n\",\"words\":[{\"hwd\":\"refusal\",\"tran\":\"拒绝；优先取舍权；推却；取舍权\"}]}]},\"usspeech\":\"refuse&type=2\",\"trans\":[{\"tranCn\":\"拒绝\",\"descOther\":\"英释\",\"pos\":\"v\",\"descCn\":\"中释\",\"tranOther\":\"to say firmly that you will not do something that someone has asked you to do\"}]}}},\"bookId\":\"CET4luan_2\"}\r\n");

            
            

        }





        // Word 类型的定义
        public class Word
        {
            public int wordRank { get; set; }
            public string headWord { get; set; }
            public Content content { get; set; }
            public string bookId { get; set; }
        }

        public class Content
        {
            public WordContent word { get; set; }
        }

        public class WordContent
        {
            public string wordHead { get; set; }
            public string wordId { get; set; }
            public Exam[] exam { get; set; }
            public Sentence sentence { get; set; }
            public string usphone { get; set; }
            public Syno syno { get; set; }
            public string ukphone { get; set; }
            public string ukspeech { get; set; }
            public Phrase phrase { get; set; }
            public RelWord relWord { get; set; }
            public Trans[] trans { get; set; }
        }

        public class Exam
        {
            public string question { get; set; }
            public Answer answer { get; set; }
            public int examType { get; set; }
            public Choice[] choices { get; set; }
        }

        public class Answer
        {
            public string explain { get; set; }
            public int rightIndex { get; set; }
        }

        public class Choice
        {
            public int choiceIndex { get; set; }
            public string choice { get; set; }
        }

        public class Sentence
        {
            public Sent[] sentences { get; set; }
            public string desc { get; set; }
        }

        public class Sent
        {
            public string sContent { get; set; }
            public string sCn { get; set; }
        }

        public class Syno
        {
            public Syn[] synos { get; set; }
            public string desc { get; set; }
        }

        public class Syn
        {
            public string pos { get; set; }
            public string tran { get; set; }
            public Hwd[] hwds { get; set; }
        }

        public class Hwd
        {
            public string w { get; set; }
        }

        public class Phrase
        {
            public Ph[] phrases { get; set; }
            public string desc { get; set; }
        }

        public class Ph
        {
            public string pContent { get; set; }
            public string pCn { get; set; }
        }

        public class RelWord
        {
            public Rel[] rels { get; set; }
            public string desc { get; set; }
        }

        public class Rel
        {
            public string pos { get; set; }
            public Word[] words { get; set; }
        }

        public class Trans
        {
            public string tranCn { get; set; }
            public string descOther { get; set; }
            public string pos { get; set; }
            public string descCn { get; set; }
            public string tranOther { get; set; }
        }

    }
}
