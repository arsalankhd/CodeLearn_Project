using System.Collections.Generic;
using CodeLearn.Core.DTOs.Question;
using CodeLearn.DataLayer.Entities.Question;

namespace CodeLearn.Core.Services.Interfaces
{
    public interface IForumService
    {
        #region Question

        int AddQuestion(Question question);
        ShowQuestionViewModel ShowQuestion(int questionId);

        #endregion

        #region Answer

        void AddAnswer(Answer answer);
        void ChangeIsTrueAnswer(int questionId, int answerId);
        IEnumerable<Question> GetQuestions(int? courseId, string filter = "");

        #endregion
    }
}
