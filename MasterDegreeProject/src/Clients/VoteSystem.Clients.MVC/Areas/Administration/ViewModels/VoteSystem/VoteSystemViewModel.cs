using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using VoteSystem.Clients.MVC.Infrastructure.Mapping.Contracts;

namespace VoteSystem.Clients.MVC.Areas.Administration.ViewModels.VoteSystem
{
    public class VoteSystemViewModel : IMapFrom<Data.Entities.VoteSystem>, IMapTo<Data.Entities.VoteSystem>
    {
        public Guid Id { get; set; }

        [Required]
        [DisplayName("Име")]
        [MinLength(5, ErrorMessage = "Името на системата не може да бъде по-малко от 5 символа.")]
        [MaxLength(100, ErrorMessage = "Името на системата не може да бъде по-голямо от 100 символа.")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Начална дата")]
        [DataType(DataType.DateTime)]
        public DateTime StarDateTime { get; set; }

        [Required]
        [DisplayName("Крайна дата")]
        [DataType(DataType.DateTime)]
        public DateTime EndDateTime { get; set; }
    }
}