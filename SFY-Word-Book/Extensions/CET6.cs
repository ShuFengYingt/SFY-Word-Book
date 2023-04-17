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
    public class CET6
    {


        public CET6(string filePath)
        {
            ReadJson(filePath);
            BookName = "六级大纲词汇";
        }

        public string BookName { get; private set; }
        TempExtension.Word ListHeadList { get; set; }
        public List<WordRoot.Root> words = new List<WordRoot.Root>();
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
                    WordRoot.Root wordItem = JsonConvert.DeserializeObject<WordRoot.Root>(line);
                    wordItem.IsSettingNew = false;
                    words.Add(wordItem);
                }
            }

            
            

        }




    }
}
