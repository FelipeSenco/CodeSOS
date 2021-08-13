using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodeSOSProject.ServiceLayer;

namespace CodeSOS.ApiControllers
{
    public class QuestionsController : ApiController
    {        
        IAnswerService answerService;

        //Constructor
        public QuestionsController(IAnswerService answerService)
        {           
            this.answerService = answerService;
        }

        //Post
        public void Post(int AnswerID, int UserID, int value)
        {
            this.answerService.UpdateAnswerVotesCount(AnswerID, UserID, value);
        }
    }
}
