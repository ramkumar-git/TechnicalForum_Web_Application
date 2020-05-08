using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalQandAForum.DomainModels;

namespace TechnicalQandAForum.Repositories
{
    public interface IVotesRepository
    {
        void UpdateVotes(int uid, int aid, int value);
    }
    public class VotesRepository : IVotesRepository
    {
        TechnicalQandAForumDbContext dbContext;

        public VotesRepository()
        {
            dbContext = new TechnicalQandAForumDbContext();
        }

        public void UpdateVotes(int uid, int aid, int value)
        {
            int updateValue;
            if (value > 0) updateValue = 1;
            else if (value < 0) updateValue = -1;
            else updateValue = 0;
            Vote vo = dbContext.Votes.Where(v => v.UserId == uid && v.AnswerId == aid).FirstOrDefault();
            if (vo != null)
            {
                vo.VoteValue += value;
            }
            else
            {
                Vote newVote = new Vote() { UserId = uid, AnswerId = aid, VoteValue = updateValue };
                dbContext.Votes.Add(newVote);
            }
            dbContext.SaveChanges();
        }
    }
}
