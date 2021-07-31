using System;
using System.Collections.Generic;
using System.Linq;
using CodeSOSProject.DomainModels;

namespace CodeSOSProject.Repositories
{
    public interface IQuestionsRepository
    {
        void InsertQuestion(Question q);
        void UpdateQuestionDetails(Question q);
        void UpdateQuestionVotesCount(int QuestionID, int value);
        void UpdateQuestionAnswersCount(int QuestionID, int value);
        void UpdateQuestionViewsCount(int QuestionID, int value);
        void DeleteQuestion(int QuestionID);
        List<Question> GetQuestions();
        Question GetQuestionByQuestionID(int QuestionID);      
    }

    public class QuestionsRepository : IQuestionsRepository
    {
        CodeSOSDatabaseDbContext db;
        public QuestionsRepository()
        {
            db = new CodeSOSDatabaseDbContext();
        }

        public void InsertQuestion(Question q)
        {
            db.Questions.Add(q);
            db.SaveChanges();
        }

        public void UpdateQuestionDetails(Question q)
        {
            Question question = db.Questions.Where(temp => temp.QuestionID == q.QuestionID).FirstOrDefault();
            if (question != null)
            {
                question.QuestionName = q.QuestionName;
                question.QuestionDateAndTime = q.QuestionDateAndTime;
                question.CategoryID = q.CategoryID;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionVotesCount(int QuestionID, int value)
        {            
            Question question = db.Questions.Where(temp => temp.QuestionID == QuestionID).FirstOrDefault();
            if (question != null)
            {
                question.VotesCount += value;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionAnswersCount(int QuestionID, int value)
        {
            Question question = db.Questions.Where(temp => temp.QuestionID == QuestionID).FirstOrDefault();
            if (question != null)
            {
                question.AnswersCount += value;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionViewsCount(int QuestionID, int value)
        {
            Question question = db.Questions.Where(temp => temp.QuestionID == QuestionID).FirstOrDefault();
            if (question != null)
            {
                question.ViewsCount += value;
                db.SaveChanges();
            }
        }

        public void DeleteQuestion(int QuestionID)
        {
            Question question = db.Questions.Where(temp => temp.QuestionID == QuestionID).FirstOrDefault();
            if (question != null)
            {
                db.Questions.Remove(question);
                db.SaveChanges();
            }
        }

        public List<Question> GetQuestions()
        {
            List<Question> questions = db.Questions.OrderByDescending(temp => temp.QuestionDateAndTime).ToList();
            return questions;
        }

        public Question GetQuestionByQuestionID(int QuestionID)
        {
            Question question = db.Questions.Where(temp => temp.QuestionID == QuestionID).FirstOrDefault();
            return question;
        }        
    }
}
