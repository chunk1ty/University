namespace DesignPatterns.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class FactureViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Номер на фактура")]
        public int FactureNumber { get; set; }

        public DateTime Date { get; set; }

        public string Provider { get; set; }

        public string Receiver { get; set; }

        public string Goods { get; set; }

        public decimal Price { get; set; }
    }
}