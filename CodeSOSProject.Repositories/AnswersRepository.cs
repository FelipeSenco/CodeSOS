using System;
using System.Collections.Generic;
using System.Linq;
using CodeSOSProject.DomainModels;


namespace CodeSOSProject.Repositories
{
    public interface IAnswersRepository
    {
        void InsertAnswer(Answer a);
        void UpdateAnswer(Answer a);
        void UpdateAnswerVotesCount(int AnswerID, int UserID, int value);
        void DeleteAnswer(int AnswerID);
        List<Answer> GetAnswersByQuestionID(int QuestionID);
        List<Answer> GetAnswersByAnswerID(int AnswerID);
    }
    public class AnswersRepository : IAnswersRepository
    {
        CodeSOSDatabaseDbContext db;
        IQuestionsRepository qr;
        IVotesRepository vr;
        public AnswersRepository()
        {
            db = new CodeSOSDatabaseDbContext();
            qr = new QuestionsRepository();
            vr = new VotesRepository();
        }
        public void InsertAnswer(Answer a)
        {
            db.Answers.Add(a);
            db.SaveChanges();
        }

        public void UpdateAnswer(Answer a)
        {
            Answer answer = db.Answers.Where(temp => temp.AnswerID == a.AnswerID).FirstOrDefault();
            if (answer != null)
            {
                answer.AnswerText = a.AnswerText;                
                db.SaveChanges();
            }
        }

        public void UpdateAnswerVotesCount(int AnswerID, int UserID, int value)
        {
            Answer answer = db.Answers.Where(temp => temp.AnswerID == AnswerID).FirstOrDefault();
            if (answer != null)
            {
                answer.VotesCount += value;
                db.SaveChanges();
                qr.UpdateQuestionVotesCount(answer.QuestionID, value);
                vr.UpdateVote(AnswerID, UserID, value);
            }
        }

        public void DeleteAnswer(int AnswerID)
        {
            Answer answer = db.Answers.Where(temp => temp.AnswerID == AnswerID).FirstOrDefault();
            if (answer != null)
            {
                db.Answers.Remove(answer);
                db.SaveChanges();
                qr.UpdateQuestionAnswersCount(answer.QuestionID, -1);
            }
        }

        public List<Answer> GetAnswersByQuestionID(int QuestionID)
        {
            List<Answer> answers = db.Answers.Where(temp => temp.QuestionID == QuestionID).ToList();
            return answers;
        }

        public List<Answer> GetAnswersByAnswerID(int AnswerID)
        {
            List<Answer> answers = db.Answers.Where(temp => temp.AnswerID == AnswerID).ToList();
            return answers;
        }
    }
}
