using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;


namespace SFY_Word_Book.Api.Context
{
    public class LearningWordBook
    {


        /// <summary>
        /// 词书
        /// </summary>
        public string BookName { get; set; }

        public ICollection<Word> Words { get; set; }

        [Key]
        public int Id { get;set; }


    }
    
}
