using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechnicalQandAForum.ServiceLayer;
using TechnicalQandAForum.ViewModels;

namespace TechnicalQandAForum.ApiControllers
{
    public class QuestionsController : ApiController
    {
        IAnswerService _answerService;

        public QuestionsController(IAnswerService answerService)
        {
            _answerService = answerService;
        }
        public void PostAnswerVote(int AnswerId, int UserId, int value)
        {
            _answerService.UpdateAnswerVotesCount(AnswerId, UserId, value);
        }
    }
}
