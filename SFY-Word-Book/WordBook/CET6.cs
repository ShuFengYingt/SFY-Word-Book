using Microsoft.VisualStudio.Services.WebApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SFY_Word_Book.WordBook;
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
    public class CET6
    {


        public CET6()
        {
        }

        public string BookName { get; private set; }
        TempExtension.Word ListHeadList { get; set; }
        public static List<WordRoot.Root> words = new List<WordRoot.Root>();

        /// <summary>
        /// 读取六级词汇
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static void ReadJson()
        {
            string fileName = "CET6.json";
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
                    wordItem.Combo = 0;
                    words.Add(wordItem);
                }
            }
        }

        /// <summary>
        /// 保存标记过的单词本
        /// </summary>
        public static void OutCET6WordBook()
        {
            string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WordBooks");
            string filePath = System.IO.Path.Combine(folderPath, "CET6.json");

            // 如果目录不存在，则创建目录
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.None,  //添加缩进
            };
            string CET6WordBookJson = "";
            foreach (var word in words)
            {
                CET6WordBookJson += JsonConvert.SerializeObject(word, settings) + Environment.NewLine;
            }
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(CET6WordBookJson);
            }

        }





    }
}
