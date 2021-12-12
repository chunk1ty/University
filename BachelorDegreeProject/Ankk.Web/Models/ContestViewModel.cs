namespace Ankk.Web.Models
{
    using Ankk.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    public class ContestViewModel
    {
        [Display(Name = "№")]       
        public int Id { get; set; }

        
        [Display(Name = "Name")]
        [Required]
        [StringLength(60,MinimumLength=3)]
        public string Name { get; set; }
        
        [Required]
        [Display(Name = "Visible ?")]
        public bool IsVisible { get; set; }

        [Required]
        [Display(Name = "Is strict check?")]
        public bool IsStrict { get; set; }
    }
}