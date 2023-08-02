using System;
using System.Collections.Generic;
using CodeLearn.Core.DTOs.Courses;
using CodeLearn.DataLayer.Entities.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeLearn.Core.Services.Interfaces
{
    public interface ICourseService
    {
        #region Group

        List<CourseGroup> GetAllGroup();
        List<SelectListItem> GetGroupForManageCourse();
        List<SelectListItem> GetSubGroupForManageCourse(int groupId);
        void AddGroup(CourseGroup group);
        void UpdateGroup(CourseGroup group);
        CourseGroup GetGroupById(int groupId);

        #endregion

        #region Course

        List<SelectListItem> GetTeachers();
        List<SelectListItem> GetLevels();
        List<SelectListItem> GetStatuses();
        List<ShowCourseForAdminViewModel> GetCoursesForAdmin();
        List<ShowCourseForAdminViewModel> GetDeleteCoursesForAdmin();
        ShowCourseForAdminViewModel GetCourseForAdminById(int courseId);
        int AddCourse(Course course, IFormFile imgCourse, IFormFile courseDemo);
        Course GetCourseById(int courseId);
        void UpdateCourse(Course course, IFormFile imgCourse, IFormFile courseDemo);
        void DeleteCourse(int courseId);
        Tuple<List<ShowCourseListItemViewModel>, int> GetCourse(int pageId = 1, string filter = "", string getType = "all", string orderByType = "date", int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0);
        Course GetCourseForShow(int courseId);
        List<ShowCourseListItemViewModel> GetPopularCourse();
        bool IsFree(int courseId);
        List<Course> GetAllMasterCourses(string userName);

        #endregion

        #region Episode

        List<CourseEpisode> GetListEpisodeCourse(int courseId);
        bool CheckExistFile(string fileName);
        int AddEpisode(CourseEpisode episode, IFormFile episodeFile);
        CourseEpisode GetCourseEpisodeById(int episodeId);
        void EditEpisode(CourseEpisode episode, IFormFile episodeFile);
        bool AddEpisode(AddEpisodeViewModel episodeViewModel, string userName);

        #endregion

        #region Comments

        void AddComment(CourseComment comment);
        Tuple<List<CourseComment>, int> GetCourseComment(int courseId, int pageId = 1);

        #endregion

        #region Votes

        void AddVote(string userName, int courseId, bool vote);
        Tuple<int, int> GetCourseVotes(int courseId);

        #endregion
    }
}
