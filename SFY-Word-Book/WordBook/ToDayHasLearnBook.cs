using Newtonsoft.Json;
using SFY_Word_Book.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.WordBook
{
    /// <summary>
    /// 当日学习过的单词，需要做日期校验和日期更新
    /// </summary>
    public class ToDayHasLearnBook
    {
        public ToDayHasLearnBook() 
        { 
           
        
        }

        public static ObservableCollection<WordRoot.Root> ToDayHasLearnWords {  get; set; } = new ObservableCollection<WordRoot.Root>();    

        /// <summary>
        /// 读取生词本
        /// </summary>
        public static void ReadJson()
        {
            string fileName = "ToDayHasLearnBook.json";
            string filePath = Path.Combine(Environment.CurrentDirectory, "WordBooks", fileName);
            //不存在就返回
            if (!File.Exists(filePath))
            {
                return;
            }
            using (StreamReader r = new StreamReader(filePath))
            {
                string line = string.Empty;
                while ((line = r.ReadLine()) != null && line != "")
                {
                    WordRoot.Root wordItem = JsonConvert.DeserializeObject<WordRoot.Root>(line);
                    //只加载同一天的
                    if (wordItem.DateTime == DateTime.Today)
                    {
                        ToDayHasLearnWords.Add(wordItem);

                    }

                }
            }
        }

        /// <summary>
        /// 输出当日学习过的词本
        /// </summary>
        public static void OutHasLearnWordBook()
        {
            string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WordBooks");
            string filePath = System.IO.Path.Combine(folderPath, "ToDayHasLearnBook.json");

            // 如果目录不存在，则创建目录
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.None,  //添加缩进
            };
            string ToDayHasLearnWordBookJson = "";
            foreach (var word in ToDayHasLearnWords)
            {
                ToDayHasLearnWordBookJson += JsonConvert.SerializeObject(word, settings) + Environment.NewLine;
            }
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(ToDayHasLearnWordBookJson);
            }
        }


    }
}
