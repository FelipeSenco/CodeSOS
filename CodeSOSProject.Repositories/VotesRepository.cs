using System;
using System.Collections.Generic;
using System.Linq;
using CodeSOSProject.DomainModels;

namespace CodeSOSProject.Repositories
{
    public interface IVotesRepository
    {
        void UpdateVote(int AnswerID, int UserID, int value);
    }
    public class VotesRepository : IVotesRepository
    {
        CodeSOSDatabaseDbContext db;
        public VotesRepository()
        {
            db = new CodeSOSDatabaseDbContext();
        }

        public void UpdateVote(int AnswerID, int UserID, int value)
        {
            int updateValue;
            if (value > 0) updateValue = -1;
            else if (value < 0) updateValue = +1;
            else updateValue = 0;

            Vote vote = db.Votes.Where(temp => temp.AnswerID == AnswerID && temp.UserID == UserID).FirstOrDefault();
            if (vote != null)
            {
                vote.VoteValue = updateValue;               
            }
            else
            {
                Vote newVote = new Vote() { AnswerID = AnswerID, UserID = UserID, VoteValue = updateValue };
                db.Votes.Add(newVote);
            }
            db.SaveChanges();
        }
    }
}
