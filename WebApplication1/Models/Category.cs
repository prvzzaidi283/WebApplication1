using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    [Table("Category")]
    public class Category
    {
        public Category()
        {
            CreatedDate = DateTime.Now;
            Isdelete = false;
        }
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage ="Name is required.")]
        [MaxLength(50,ErrorMessage ="{0} can not be more than {1} characters.")]
        [MinLength(5,ErrorMessage = "{0} required atleast {1} characters.")]
        public string Name { get; set; }
        public bool Isdelete { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
    public partial class CategoryDetails_Result
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int TotalRecords { get; set; }
    }
}