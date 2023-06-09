﻿using SFY_Word_Book.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SFY_Word_Book.WordBook
{
     public class ReviewWordBook
     {
        public ReviewWordBook()
        {

        }

        public static ObservableCollection<WordRoot.Root> ReviewWords { get; set; } = new ObservableCollection<WordRoot.Root>();

        /// <summary>
        /// 读取生词本
        /// </summary>
        public static void ReadJson()
        {
            string fileName = "ReviewWordBook.json";
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
                    ReviewWords.Add(wordItem);

                }
            }
        }

        /// <summary>
        /// 输出待复习词本
        /// </summary>
        public static void OutReviewWordBook()
        {
            string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WordBooks");
            string filePath = System.IO.Path.Combine(folderPath, "ReviewWordBook.json");

            // 如果目录不存在，则创建目录
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.None,  //添加缩进
            };
            string ReviewWordBookJson = "";
            foreach (var word in ReviewWords)
            {
                ReviewWordBookJson += JsonConvert.SerializeObject(word, settings) + Environment.NewLine;
            }
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(ReviewWordBookJson);
            }
        }



    }
}
