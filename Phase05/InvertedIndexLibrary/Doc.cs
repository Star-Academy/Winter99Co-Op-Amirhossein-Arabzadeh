﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;

namespace InvertedIndexLibrary
{
    public class Doc
    {
        public Doc(int id)
        {
            this.Id = id;
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Id { get; set; }

        public List<SearchItem> SearchItems { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Doc doc &&
                   Id == doc.Id &&
                   EqualityComparer<List<SearchItem>>.Default.Equals(SearchItems, doc.SearchItems);
        }
    }
}