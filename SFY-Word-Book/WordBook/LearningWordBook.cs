using Newtonsoft.Json;
using SFY_Word_Book.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.WordBook
{
    public class LearningWordBook
    {
        public LearningWordBook() { }

        public static List<WordRoot.Root> LearningWords { get; set; } = new List<WordRoot.Root>();

        /// <summary>
        /// 读取待学习单词
        /// </summary>
        public static void ReadJson()
        {
            string fileName = "LearningWordBook.json";
            string filePath = Path.Combine(Environment.CurrentDirectory, "WordBooks", fileName);
            //不存在就返回,而且说明是第一次使用，将CET6全部添加进去
            if (!File.Exists(filePath))
            {
                //这里有指针问题，得一个个加进去,不然修改LeaningWordBook时会造成CET6的变动
                for (int i = 0;i < CET6.words.Count;i++)
                {
                    LearningWords.Add(CET6.words[i]);
                }
                return;
            }
            using (StreamReader r = new StreamReader(filePath))
            {
                string line = string.Empty;
                while ((line = r.ReadLine()) != null && line != "")
                {
                    WordRoot.Root wordItem = JsonConvert.DeserializeObject<WordRoot.Root>(line);
                    LearningWords.Add(wordItem);

                }
            }

        }

        /// <summary>
        /// 输出待学习单词
        /// </summary>
        public static void OutLearningWordBook()
        {
            string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WordBooks");
            string filePath = System.IO.Path.Combine(folderPath, "LearningWordBook.json");

            // 如果目录不存在，则创建目录
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.None,  //添加缩进
            };
            string LearingWordBookJson = "";
            foreach (var word in LearningWords)
            {
                LearingWordBookJson += JsonConvert.SerializeObject(word, settings) + Environment.NewLine;
            }
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(LearingWordBookJson);
            }

        }
    }
}