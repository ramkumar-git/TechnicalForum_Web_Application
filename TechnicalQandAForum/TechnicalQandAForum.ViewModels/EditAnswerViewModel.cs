﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalQandAForum.ViewModels
{
    public class EditAnswerViewModel
    {
        [Required]
        public int AnswerId { get; set; }
        [Required]
        public string AnswerText { get; set; }
        [Required]
        public DateTime AnswerDateAndTime { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public int VotesCount { get; set; }
        public virtual QuestionViewModel Question { get; set; }
    }
}
