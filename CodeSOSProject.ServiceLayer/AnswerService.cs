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
    public interface IAnswerService
    {
        void InsertAnswer(NewAnswerViewModel navm);
        void UpdateAnswer(EditAnswerViewModel eavm);
        void UpdateAnswerVotesCount(int AnswerID, int UserID, int value);
        void DeleteAnswer(int AnswerID);
        List<AnswerViewModel> GetAnswersByQuestionID(int QuestionID);
        AnswerViewModel GetAnswersByAnswerID(int AnswerID);
    }
    public class AnswerService : IAnswerService
    {
        IAnswersRepository answersRepository;

        public AnswerService()
        {
            answersRepository = new AnswersRepository();
        }

        public void InsertAnswer(NewAnswerViewModel navm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<NewAnswerViewModel, Answer>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Answer answer = mapper.Map<NewAnswerViewModel, Answer>(navm);
            answersRepository.InsertAnswer(answer);
        }
        public void UpdateAnswer(EditAnswerViewModel eavm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditAnswerViewModel, Answer>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Answer answer = mapper.Map<EditAnswerViewModel, Answer>(eavm);
            answersRepository.UpdateAnswer(answer);
        }

        public void UpdateAnswerVotesCount(int AnswerID, int UserID, int value)
        {
            answersRepository.UpdateAnswerVotesCount(AnswerID, UserID, value);
        }

        public void DeleteAnswer(int AnswerID)
        {
            answersRepository.DeleteAnswer(AnswerID);
        }

        public List<AnswerViewModel> GetAnswersByQuestionID(int QuestionID)
        {
            List<Answer> answers = answersRepository.GetAnswersByQuestionID(QuestionID);
            List<AnswerViewModel> avm = null;

            if (answers != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Answer, AnswerViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                avm = mapper.Map<List<Answer>, List<AnswerViewModel>>(answers);
            }
            return avm;
        }

        public AnswerViewModel GetAnswersByAnswerID(int AnswerID)
        {
            Answer answer = answersRepository.GetAnswersByAnswerID(AnswerID).FirstOrDefault();
            AnswerViewModel avm = null;

            if (answer != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Answer, AnswerViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                avm = mapper.Map<Answer, AnswerViewModel>(answer);
            }
            return avm;
        }
    }
}
