using System.Collections.Generic;
using CodeLearn.DataLayer.Entities.Question;

namespace CodeLearn.Core.DTOs.Question
{
    public class ShowQuestionViewModel
    {
        public DataLayer.Entities.Question.Question Question { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
