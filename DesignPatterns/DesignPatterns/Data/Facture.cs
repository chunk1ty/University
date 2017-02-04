namespace DesignPatterns.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Facture
    {
        [Key]
        public int Id { get; set; }

        public int FactureNumber { get; set; }

        public DateTime Date { get; set; }

        public string Provider { get; set; }

        public string Receiver { get; set; }

        public string Goods { get; set; }

        public decimal Price { get; set; }
    }
}
