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
    public interface IAnswerService
    {
        void InsertAnswer(NewAnswerViewModel avm);
        void UpdateAnswer(EditAnswerViewModel avm);
        void UpdateAnswerVotesCount(int aid, int uid, int value);
        void DeleteAnswer(int aid);
        List<AnswerViewModel> GetAnswersByQuestionId(int qid);
        AnswerViewModel GetAnswerByAnswerId(int AnswerId);
    }
    public class AnswerService : IAnswerService
    {
        IAnswerRepository answerRepository;

        public AnswerService()
        {
            answerRepository = new AnswerRepository();
        }

        public void InsertAnswer(NewAnswerViewModel avm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<NewAnswerViewModel, Answer>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Answer a = mapper.Map<NewAnswerViewModel, Answer>(avm);
            answerRepository.InsertAnswer(a);
        }
        public void UpdateAnswer(EditAnswerViewModel avm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditAnswerViewModel, Answer>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Answer a = mapper.Map<EditAnswerViewModel, Answer>(avm);
            answerRepository.UpdateAnswer(a);
        }
        public void UpdateAnswerVotesCount(int aid, int uid, int value)
        {
            answerRepository.UpdateAnswerVotesCount(aid, uid, value);
        }
        public void DeleteAnswer(int aid)
        {
            answerRepository.DeleteAnswer(aid);
        }

        public List<AnswerViewModel> GetAnswersByQuestionId(int qid)
        {
            List<Answer> a = answerRepository.GetAnswersByQuestionId(qid);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Answer, AnswerViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<AnswerViewModel> avm = mapper.Map<List<Answer>, List<AnswerViewModel>>(a);
            return avm;
        }

        public AnswerViewModel GetAnswerByAnswerId(int AnswerId)
        {
            Answer a = answerRepository.GetAnswersByAnswerId(AnswerId).FirstOrDefault();
            AnswerViewModel avm = null;
            if (a != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Answer, AnswerViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                avm = mapper.Map<Answer, AnswerViewModel>(a);
            }
            return avm;
        }
    }
}


