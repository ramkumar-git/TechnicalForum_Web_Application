using System;
using System.Collections.Generic;
using System.Linq;
using TechnicalQandAForum.DomainModels;
using TechnicalQandAForum.ViewModels;
using TechnicalQandAForum.Repositories;
using AutoMapper;
using AutoMapper.Configuration;

namespace TechnicalQandAForum.ServiceLayer
{
    public interface IQuestionService
    {
        void InsertQuestion(NewQuestionViewModel questionViewModel);
        void UpdateQuestionDetails(QuestionViewModel questionViewModel);
        void UpdateQuestionVotesCount(int qid, int value);
        void UpdateQuestionAnswersCount(int qid, int value);
        void UpdateQuestionViewsCount(int qid, int value);
        void DeleteQuestion(int qid);
        List<QuestionViewModel> GetQuestions();
        QuestionViewModel GetQuestionByQuestionId(int qid, int uid);
    }
    public class QuestionService : IQuestionService
    {
        IQuestionRepository questionRepository;

        public QuestionService()
        {
            questionRepository = new QuestionRepository();
        }
        public void InsertQuestion(NewQuestionViewModel questionViewModel)
        {
            var config = new MapperConfiguration(c => { c.CreateMap<NewQuestionViewModel, Question>(); c.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Question question = mapper.Map<NewQuestionViewModel, Question>(questionViewModel);
            questionRepository.InsertQuestion(question);
        }

        public void UpdateQuestionDetails(QuestionViewModel questionViewModel)
        {
            var config = new MapperConfiguration(c => { c.CreateMap<EditQuestionViewModel, Question>(); c.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Question question = mapper.Map<QuestionViewModel, Question>(questionViewModel);
            questionRepository.UpdateQuestionDetails(question);
        }

        public void UpdateQuestionVotesCount(int qid, int value)
        {
            questionRepository.UpdateQuestionVotesCount(qid, value);
        }

        public void UpdateQuestionAnswersCount(int qid, int value)
        {
            questionRepository.UpdateQuestionAnswersCount(qid, value);
        }

        public void UpdateQuestionViewsCount(int qid, int value)
        {
            questionRepository.UpdateQuestionViewsCount(qid, value);
        }

        public void DeleteQuestion(int qid)
        {
            questionRepository.DeleteQuestion(qid);
        }

        public List<QuestionViewModel> GetQuestions()
        {
            List<Question> questions = questionRepository.GetQuestions();
            var config = new MapperConfiguration(c => { c.CreateMap<Question, QuestionViewModel>(); c.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<QuestionViewModel> questionViewModels = mapper.Map<List<Question>, List<QuestionViewModel>>(questions);
            return questionViewModels;
        }
        public QuestionViewModel GetQuestionByQuestionId(int qid, int uid = 0)
        {
            Question question = questionRepository.GetQuestionsById(qid).FirstOrDefault();
            QuestionViewModel questionViewModel = new QuestionViewModel();
            if (question != null)
            {
                var config = new MapperConfiguration(c => { c.CreateMap<Question, QuestionViewModel>(); c.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                questionViewModel = mapper.Map<Question, QuestionViewModel>(question);
                foreach(var ans in questionViewModel.Answers)
                {
                    ans.CurrentUserVoteType = 0;
                    VoteViewModel voteViewModel = ans.Votes.Where(v => v.UserId == uid).FirstOrDefault();
                    if(voteViewModel != null)
                    {
                        ans.CurrentUserVoteType = voteViewModel.VoteValue;
                    }
                }
            }
            return questionViewModel;
        }
    }
}
