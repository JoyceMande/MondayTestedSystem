using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCRIPTERS.Core.Models
{
    public class BookCategory
    {
        public BookCategory()
        {
            Books = new List<Book>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(maximumLength: 1000, ErrorMessage = "Description can not be more then 1000 characters")]
        public string Description { get; set; }

        public List<Book> Books { get; set; }


    }
}