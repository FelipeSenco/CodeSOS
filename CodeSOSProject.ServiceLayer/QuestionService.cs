using System;
using System.Collections.Generic;
using System.Linq;
using CodeSOSProject.DomainModels;
using CodeSOSProject.ViewModels;
using CodeSOSProject.Repositories;
using AutoMapper;
using AutoMapper.Configuration;

namespace CodeSOSProject.ServiceLayer
{
    public interface IQuestionService
    {
        void InsertQuestion(NewQuestionViewModel nqvm);
        void UpdateQuestionDetails(EditQuestionViewModel eqvm);
        void UpdateQuestionVotesCount(int QuestionID, int value);
        void UpdateQuestionAnswersCount(int QuestionID, int value);
        void UpdateQuestionViewsCount(int QuestionID, int value);
        void DeleteQuestion(int QuestionID);
        List<QuestionViewModel> GetQuestions();
        QuestionViewModel GetQuestionByQuestionID(int QuestionID, int UserID);        
    }

    public class QuestionService : IQuestionService
    {
        IQuestionsRepository questionsRepository;

        public QuestionService()
        {
            questionsRepository = new QuestionsRepository();
        }

        public void InsertQuestion(NewQuestionViewModel nqvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<NewQuestionViewModel, Question>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Question question = mapper.Map<NewQuestionViewModel, Question>(nqvm);
            questionsRepository.InsertQuestion(question);
        }

        public void UpdateQuestionDetails(EditQuestionViewModel eqvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditQuestionViewModel, Question>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Question question = mapper.Map<EditQuestionViewModel, Question>(eqvm);
            questionsRepository.UpdateQuestionDetails(question);
        }

        public void UpdateQuestionVotesCount(int QuestionID, int value)
        {
            questionsRepository.UpdateQuestionVotesCount(QuestionID, value);
        }

        public void UpdateQuestionAnswersCount(int QuestionID, int value)
        {
            questionsRepository.UpdateQuestionAnswersCount(QuestionID, value);
        }

        public void UpdateQuestionViewsCount(int QuestionID, int value)
        {
            questionsRepository.UpdateQuestionViewsCount(QuestionID, value);
        }

        public void DeleteQuestion(int QuestionID)
        {
            questionsRepository.DeleteQuestion(QuestionID);
        }

        public List<QuestionViewModel> GetQuestions()
        {
            List<Question> questions = questionsRepository.GetQuestions();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Question, QuestionViewModel>();
                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<Category, CategoryViewModel>();
                cfg.CreateMap<Answer, AnswerViewModel>();
                cfg.CreateMap<Vote, VoteViewModel>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            List<QuestionViewModel> qvm = mapper.Map<List<Question>, List<QuestionViewModel>>(questions);
            return qvm;
        }

        public QuestionViewModel GetQuestionByQuestionID(int QuestionID, int UserID = 0)
        {
            Question question = questionsRepository.GetQuestionByQuestionID(QuestionID);
            QuestionViewModel qvm = null;
            if (question != null)
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Question, QuestionViewModel>();
                    cfg.CreateMap<User, UserViewModel>();
                    cfg.CreateMap<Category, CategoryViewModel>();
                    cfg.CreateMap<Answer, AnswerViewModel>();
                    cfg.CreateMap<Vote, VoteViewModel>();
                    cfg.IgnoreUnmapped();
                });
                IMapper mapper = config.CreateMapper();
                qvm = mapper.Map<Question, QuestionViewModel>(question);

                foreach (var answer in qvm.Answers)
                {
                    answer.CurrentUserVoteType = 0;
                    VoteViewModel vote = answer.Votes.Where(temp => temp.UserID == UserID).FirstOrDefault();
                    if (vote != null)
                    {
                        answer.CurrentUserVoteType = vote.VoteValue;
                    }
                }                    
            }
            return qvm;
        }        
    }
}
