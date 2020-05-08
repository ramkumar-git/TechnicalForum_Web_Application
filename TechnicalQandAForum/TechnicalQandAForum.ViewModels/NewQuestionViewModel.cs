using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalQandAForum.ViewModels
{
    public class NewQuestionViewModel
    {

        [Required]
        public string QuestionName { get; set; }
        [Required]
        public DateTime QuestionDateAndTime { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int VotesCount { get; set; }
        [Required]
        public int AnswersCount { get; set; }
        [Required]
        public int ViewsCount { get; set; }
    }
}
