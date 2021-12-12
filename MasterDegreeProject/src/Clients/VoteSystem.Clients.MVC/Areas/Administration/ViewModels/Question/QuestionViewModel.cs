using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using VoteSystem.Clients.MVC.Areas.Administration.ViewModels.Answer;
using VoteSystem.Clients.MVC.Infrastructure.Mapping.Contracts;

namespace VoteSystem.Clients.MVC.Areas.Administration.ViewModels.Question
{
    public class QuestionViewModel : IMapFrom<Data.Entities.Question>, IMapTo<Data.Entities.Question>
    {
        public QuestionViewModel()
        {
            Answers = new List<AnswerViewModel>();
        }
       
        public int Id { get; set; }

        public Guid VoteSystemId { get; set; }

        [DisplayName("Име на въпроса")]
        [Required(ErrorMessage = "Името на въпросът е задължително.")]
        [MinLength(2, ErrorMessage = "Въпросът не може да е по-малък от 2 символа.")]
        [MaxLength(100, ErrorMessage = "Въпросът не може да е по-голям от 100 символа.")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Бихте ли желали да има повече от един отговор?")]
        public bool HasMultipleAnswers { get; set; }

        public IList<AnswerViewModel> Answers { get; set; }
    }
}