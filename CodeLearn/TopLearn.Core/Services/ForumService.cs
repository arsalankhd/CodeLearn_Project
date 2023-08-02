using System;
using System.Collections.Generic;
using System.Linq;
using CodeLearn.Core.DTOs.Question;
using CodeLearn.Core.Services.Interfaces;
using CodeLearn.DataLayer.Context;
using CodeLearn.DataLayer.Entities.Question;
using Microsoft.EntityFrameworkCore;

namespace CodeLearn.Core.Services
{
    public class ForumService : IForumService
    {
        private CodeLearnContext _context;

        public ForumService(CodeLearnContext context)
        {
            _context = context;
        }

        public int AddQuestion(Question question)
        {
            question.CreateDate = DateTime.Now;
            question.ModifiedDate = DateTime.Now;
            _context.Questions.Add(question);
            _context.SaveChanges();
            return question.QuestionId;
        }

        public ShowQuestionViewModel ShowQuestion(int questionId)
        {
            var question = new ShowQuestionViewModel();
            question.Question = _context.Questions.Include(q => q.User)
                .FirstOrDefault(q => q.QuestionId == questionId);
            question.Answers = _context.Answers.Include(a => a.User)
                .Where(a => a.QuestionId == questionId).ToList();
            return question;
        }

        public void AddAnswer(Answer answer)
        {
            _context.Answers.Add(answer);
            _context.SaveChanges();
        }

        public void ChangeIsTrueAnswer(int questionId, int answerId)
        {
            var answers = _context.Answers.Where(a => a.QuestionId == questionId);
            foreach (var ans in answers)
            {
                ans.IsTrue = false;
                if (ans.AnswerId == answerId)
                {
                    ans.IsTrue = true;
                }
            }

            _context.UpdateRange(answers);
            _context.SaveChanges();
        }

        public IEnumerable<Question> GetQuestions(int? courseId, string filter = "")
        {
            IQueryable<Question> result = _context.Questions
                .Where(q => EF.Functions.Like(q.Title, $"%{filter}%"));

            if (courseId != null)
            {
                result = result.Where(q => q.CourseId == courseId);
            }

            return result.Include(q => q.User)
                .Include(q => q.Course).ToList();
        }
    }
}