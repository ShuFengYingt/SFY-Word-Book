using Newtonsoft.Json;
using Prism.Mvvm;
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
    public class NewWordBook:BindableBase
    {
        public NewWordBook() 
        {
            
        }

        
        public static ObservableCollection<WordRoot.Root> NewWords { get; set; } = new ObservableCollection<WordRoot.Root>();
        /// <summary>
        /// 读取生词本
        /// </summary>
        public static void ReadJson()
        {
            string fileName = "NewWordBook.json";
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
                    NewWords.Add(wordItem);

                }
            }
        }

        /// <summary>
        /// 输出生词本
        /// </summary>
        public static void OutNewWordBook()
        {
            string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WordBooks");
            string filePath = System.IO.Path.Combine(folderPath, "NewWordBook.json");

            // 如果目录不存在，则创建目录
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.None,  //添加缩进
            };
            string NewWordBookJson = "";
            foreach (var word in NewWordBook.NewWords)
            {
                NewWordBookJson += JsonConvert.SerializeObject(word, settings) + Environment.NewLine;
            }
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(NewWordBookJson);
            }
        }


    }
}
