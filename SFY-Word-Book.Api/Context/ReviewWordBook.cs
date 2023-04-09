﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SFY_Word_Book.Api.Context
{
    public class ReviewWordBook 
    {

        public ICollection<Word> Words { get; set; }

        [Key]
        public int Id { get; set; }
    }
}
