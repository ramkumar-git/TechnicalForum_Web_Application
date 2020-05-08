using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalQandAForum.DomainModels;

namespace TechnicalQandAForum.Repositories
{
    public interface IQuestionRepository
    {
        void InsertQuestion(Question question);
        void UpdateQuestionDetails(Question question);
        void UpdateQuestionVotesCount(int qid, int value);
        void UpdateQuestionAnswersCount(int qid, int value);
        void UpdateQuestionViewsCount(int qid, int value);
        void DeleteQuestion(int qid);
        List<Question> GetQuestionsById(int qid);
        List<Question> GetQuestions();
    }
    public class QuestionRepository : IQuestionRepository
    {
        TechnicalQandAForumDbContext dbContext;

        public QuestionRepository()
        {
            dbContext = new TechnicalQandAForumDbContext();
        }
        public void DeleteQuestion(int qid)
        {
            Question qu = dbContext.Questions.Where(q => q.QuestionId == qid).FirstOrDefault();
            if (qu != null)
            {
                dbContext.Questions.Remove(qu);
                dbContext.SaveChanges();
            }
        }

        public List<Question> GetQuestions()
        {
            List<Question> questions = dbContext.Questions.OrderByDescending(q => q.QuestionDateAndTime).ToList();
            return questions;
        }

        public List<Question> GetQuestionsById(int qid)
        {
            List<Question> questions = dbContext.Questions.Where(q => q.QuestionId == qid).ToList();
            return questions;
        }

        public void InsertQuestion(Question question)
        {
            dbContext.Questions.Add(question);
            dbContext.SaveChanges();
        }

        public void UpdateQuestionAnswersCount(int qid, int value)
        {
            Question qu = dbContext.Questions.Where(q => q.QuestionId == qid).FirstOrDefault();
            if(qu != null)
            {
                qu.AnswersCount += value;
                dbContext.SaveChanges();
            }
        }

        public void UpdateQuestionDetails(Question question)
        {
            Question qu = dbContext.Questions.Where(q => q.QuestionId == question.QuestionId).FirstOrDefault();
            if (qu != null)
            {
                qu.QuestionName = question.QuestionName;
                qu.QuestionDateAndTime = question.QuestionDateAndTime;
                qu.CategoryId = question.CategoryId;
                dbContext.SaveChanges();
            }
        }

        public void UpdateQuestionViewsCount(int qid, int value)
        {
            Question qu = dbContext.Questions.Where(q => q.QuestionId == qid).FirstOrDefault();
            if (qu != null)
            {
                qu.ViewsCount += value;
                dbContext.SaveChanges();
            }
        }

        public void UpdateQuestionVotesCount(int qid, int value)
        {
            Question qu = dbContext.Questions.Where(q => q.QuestionId == qid).FirstOrDefault();
            if (qu != null)
            {
                qu.VotesCount += value;
                dbContext.SaveChanges();
            }
        }
    }
}
