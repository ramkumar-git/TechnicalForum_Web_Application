using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalQandAForum.DomainModels;

namespace TechnicalQandAForum.Repositories
{
    public interface IAnswerRepository
    {
        void InsertAnswer(Answer answer);
        void UpdateAnswer(Answer answer);
        void UpdateAnswerVotesCount(int aid,int uid, int value);
        void DeleteAnswer(int aid);
        List<Answer> GetAnswersByQuestionId(int qid);
        List<Answer> GetAnswersByAnswerId(int aid);


    }
    public class AnswerRepository : IAnswerRepository
    {
        TechnicalQandAForumDbContext dbContext;
        IQuestionRepository questionRepository;
        IVotesRepository votesRepository;

        public AnswerRepository()
        {
            dbContext = new TechnicalQandAForumDbContext();
            questionRepository = new QuestionRepository();
            votesRepository = new VotesRepository();
        }
        public void DeleteAnswer(int aid)
        {
            Answer ans = dbContext.Answers.Where(a => a.AnswerId == aid).FirstOrDefault();
            if(ans != null)
            {
                dbContext.Answers.Remove(ans);
                dbContext.SaveChanges();
                questionRepository.UpdateQuestionAnswersCount(ans.QuestionId, -1);
            }
        }

        public List<Answer> GetAnswersByAnswerId(int aid)
        {
            List<Answer> answer = dbContext.Answers.Where(a => a.AnswerId == aid).ToList();
            return answer;

        }

        public List<Answer> GetAnswersByQuestionId(int qid)
        {
            List<Answer> answer = dbContext.Answers.Where(a => a.QuestionId == qid).OrderByDescending(a => a.AnswerDateAndTime).ToList();
            return answer;
        }

        public void InsertAnswer(Answer answer)
        {
            dbContext.Answers.Add(answer);
            dbContext.SaveChanges();
            questionRepository.UpdateQuestionAnswersCount(answer.QuestionId, 1);
        }

        public void UpdateAnswer(Answer answer)
        {
            Answer ans = dbContext.Answers.Where(a => a.AnswerId == answer.AnswerId).FirstOrDefault();
            if (ans != null)
            {
                ans.AnswerText = answer.AnswerText;
                dbContext.SaveChanges();
            }
        }

        public void UpdateAnswerVotesCount(int aid, int uid, int value)
        {
            Answer ans = dbContext.Answers.Where(a => a.AnswerId == aid).FirstOrDefault();
            if (ans != null)
            {
                ans.VotesCount += value;
                dbContext.SaveChanges();
                questionRepository.UpdateQuestionVotesCount(ans.QuestionId, value);
                votesRepository.UpdateVotes(uid, aid, value);
            }
        }
    }
}
