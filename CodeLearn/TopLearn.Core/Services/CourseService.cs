using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodeLearn.Core.Convertors;
using CodeLearn.Core.DTOs.Courses;
using CodeLearn.Core.Generator;
using CodeLearn.Core.Security;
using CodeLearn.Core.Services.Interfaces;
using CodeLearn.DataLayer.Context;
using CodeLearn.DataLayer.Entities.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CodeLearn.Core.Services
{
    public class CourseService : ICourseService
    {
        private CodeLearnContext _context;

        public CourseService(CodeLearnContext context)
        {
            _context = context;
        }

        public List<CourseGroup> GetAllGroup()
        {
            return _context.CourseGroups.Include(g => g.CourseGroups).ToList();
        }

        public List<SelectListItem> GetGroupForManageCourse()
        {
            return _context.CourseGroups.Where(g => g.ParentId == null)
                .Select(g => new SelectListItem()
                {
                    Value = g.GroupId.ToString(),
                    Text = g.GroupTitle
                }).ToList();
        }

        public List<SelectListItem> GetSubGroupForManageCourse(int groupId)
        {
            return _context.CourseGroups.Where(g => g.ParentId == groupId)
                .Select(g => new SelectListItem()
                {
                    Value = g.GroupId.ToString(),
                    Text = g.GroupTitle
                }).ToList();
        }

        public void AddGroup(CourseGroup group)
        {
            _context.CourseGroups.Add(group);
            _context.SaveChanges();
        }

        public void UpdateGroup(CourseGroup group)
        {
            _context.CourseGroups.Update(group);
            _context.SaveChanges();
        }

        public CourseGroup GetGroupById(int groupId)
        {
            return _context.CourseGroups.Find(groupId);
        }

        public List<SelectListItem> GetTeachers()
        {
            return _context.UserRoles.Where(r => r.RoleId == 2).Include(r => r.User)
                .Select(u => new SelectListItem()
                {
                    Value = u.UserId.ToString(),
                    Text = u.User.UserName
                }).ToList();
        }

        public List<SelectListItem> GetLevels()
        {
            return _context.CourseLevels.Select(l => new SelectListItem()
            {
                Value = l.LevelId.ToString(),
                Text = l.LevelTitle
            }).ToList();
        }

        public List<SelectListItem> GetStatuses()
        {
            return _context.CourseStatuses.Select(s => new SelectListItem()
            {
                Value = s.StatusId.ToString(),
                Text = s.StatusTitle
            }).ToList();
        }

        public List<ShowCourseForAdminViewModel> GetCoursesForAdmin()
        {

            return _context.Courses.Select(c => new ShowCourseForAdminViewModel()
            {
                CourseId = c.CourseId,
                CourseTitle = c.CourseTitle,
                CourseImageName = c.CourseImageName,
                EpisodeCount = c.CourseEpisodes.Count,
            }).ToList();
        }

        public List<ShowCourseForAdminViewModel> GetDeleteCoursesForAdmin()
        {
            return _context.Courses.IgnoreQueryFilters().Where(c => c.IsDelete)
                .Select(c => new ShowCourseForAdminViewModel()
                {
                    CourseId = c.CourseId,
                    CourseTitle = c.CourseTitle,
                    CourseImageName = c.CourseImageName,
                    EpisodeCount = c.CourseEpisodes.Count
                }).ToList();
        }

        public ShowCourseForAdminViewModel GetCourseForAdminById(int courseId)
        {
            return _context.Courses.Where(c => c.CourseId == courseId)
                .Select(c => new ShowCourseForAdminViewModel()
                {
                    CourseId = c.CourseId,
                    CourseTitle = c.CourseTitle,
                    CourseImageName = c.CourseImageName,
                    EpisodeCount = c.CourseEpisodes.Count
                }).Single();
        }

        public int AddCourse(Course course, IFormFile imgCourse, IFormFile courseDemo)
        {
            course.CreateDate = DateTime.Now;
            course.CourseImageName = "no-photo.jpg";
            if (imgCourse != null && imgCourse.IsImage())
            {
                course.CourseImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgCourse.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/image",
                    course.CourseImageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgCourse.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/thumb",
                    course.CourseImageName);
                imgResizer.Image_resize(imagePath, thumbPath, 150);
            }

            if (courseDemo != null)
            {
                course.DemoFileName = NameGenerator.GenerateUniqCode() + Path.GetFileName(courseDemo.FileName);
                string demoPath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/course/demoes", course.DemoFileName);
                using (var stream = new FileStream(demoPath, FileMode.Create))
                {
                    courseDemo.CopyTo(stream);
                }
            }

            _context.Add(course);
            _context.SaveChanges();
            return course.CourseId;
        }

        public Course GetCourseById(int courseId)
        {
            return _context.Courses.Find(courseId);
        }

        public void UpdateCourse(Course course, IFormFile imgCourse, IFormFile courseDemo)
        {
            course.UpdateDate = DateTime.Now;

            if (imgCourse != null && imgCourse.IsImage())
            {
                if (course.CourseImageName != "no-photo.jpg")
                {
                    string deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/image",
                        course.CourseImageName);
                    if (File.Exists(deleteImagePath))
                    {
                        File.Delete(deleteImagePath);
                    }

                    string deleteThumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/thumb",
                        course.CourseImageName);
                    if (File.Exists(deleteThumbPath))
                    {
                        File.Delete(deleteThumbPath);
                    }
                }
                course.CourseImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgCourse.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/image",
                    course.CourseImageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgCourse.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/thumb",
                    course.CourseImageName);
                imgResizer.Image_resize(imagePath, thumbPath, 150);
            }

            if (courseDemo != null)
            {
                if (course.DemoFileName != null)
                {
                    string deleteDemoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/demoes",
                        course.DemoFileName);
                    if (File.Exists(deleteDemoPath))
                    {
                        File.Delete(deleteDemoPath);
                    }
                }
                course.DemoFileName = NameGenerator.GenerateUniqCode() + Path.GetFileName(courseDemo.FileName);
                string demoPath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/course/demoes", course.DemoFileName);
                using (var stream = new FileStream(demoPath, FileMode.Create))
                {
                    courseDemo.CopyTo(stream);
                }
            }

            _context.Courses.Update(course);
            _context.SaveChanges();
        }

        public void DeleteCourse(int courseId)
        {
            var course = GetCourseById(courseId);
            course.IsDelete = true;
            _context.Courses.Update(course);
            _context.SaveChanges();
        }

        public Tuple<List<ShowCourseListItemViewModel>, int> GetCourse(int pageId = 1, string filter = "", string getType = "all", string orderByType = "date",
            int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0)
        {
            if (take == 0)
                take = 8;

            IQueryable<Course> result = _context.Courses;

            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(c => c.CourseTitle.Contains(filter) || c.Tags.Contains(filter));
            }

            switch (getType)
            {
                case "all":
                    break;
                case "buy":
                    {
                        result = result.Where(c => c.CoursePrice != 0);
                        break;
                    }
                case "free":
                    {
                        result = result.Where(c => c.CoursePrice == 0);
                        break;
                    }
            }

            switch (orderByType)
            {
                case "date":
                    {
                        result = result.OrderByDescending(c => c.CreateDate);
                        break;
                    }
                case "updatedate":
                    {
                        result = result.OrderByDescending(c => c.UpdateDate);
                        break;
                    }
            }

            if (startPrice > 0)
            {
                result = result.Where(c => c.CoursePrice > startPrice);
            }

            if (endPrice > 0)
            {
                result = result.Where(c => c.CoursePrice < endPrice);
            }

            if (selectedGroups != null && selectedGroups.Any())
            {
                foreach (var groupId in selectedGroups)
                {
                    result = result.Where(c => c.GroupId == groupId || c.SubGroup == groupId);
                }
            }

            int skip = (pageId - 1) * take;

            var pageCount = result.Include(c => c.CourseEpisodes).Select(c => new ShowCourseListItemViewModel()
            {
                CourseId = c.CourseId,
                ImageName = c.CourseImageName,
                Price = c.CoursePrice,
                Title = c.CourseTitle,
                CourseEpisodes = c.CourseEpisodes,
            }).Count() / take;

            var query = result.Include(c => c.CourseEpisodes).Select(c => new ShowCourseListItemViewModel()
            {
                CourseId = c.CourseId,
                ImageName = c.CourseImageName,
                Price = c.CoursePrice,
                Title = c.CourseTitle,
                CourseEpisodes = c.CourseEpisodes,
            }).Skip(skip).Take(take).ToList();

            return Tuple.Create(query, pageCount);
        }

        public Course GetCourseForShow(int courseId)
        {
            return _context.Courses.Include(c => c.CourseEpisodes)
                .Include(c => c.CourseStatus).Include(c => c.CourseLevel)
                .Include(c => c.User).Include(c => c.UserCourses)
                .FirstOrDefault(c => c.CourseId == courseId);
        }

        public List<ShowCourseListItemViewModel> GetPopularCourse()
        {
            return _context.Courses.Include(c => c.OrderDetails)
                .Include(c => c.CourseEpisodes)
                .Where(c => c.OrderDetails.Any())
                .OrderByDescending(c => c.OrderDetails.Count)
                .Take(8)
                .Select(c => new ShowCourseListItemViewModel()
                {
                    CourseId = c.CourseId,
                    Title = c.CourseTitle,
                    ImageName = c.CourseImageName,
                    Price = c.CoursePrice,
                    CourseEpisodes = c.CourseEpisodes
                })
                .ToList();
        }

        public bool IsFree(int courseId)
        {
            return _context.Courses.Where(c => c.CourseId == courseId).Select(c => c.CoursePrice).First() == 0;
        }

        public List<Course> GetAllMasterCourses(string userName)
        {
            int userId = _context.Users.FirstOrDefault(u => u.UserName == userName).UserId;

            var courses = _context.Courses
                .Include(c => c.CourseStatus)
                .Include(c => c.CourseEpisodes)
                .Where(c => c.TeacherId == userId).ToList();

            return courses;
        }

        public List<CourseEpisode> GetListEpisodeCourse(int courseId)
        {
            return _context.CourseEpisodes.Where(e => e.CourseId == courseId).ToList();
        }

        public bool CheckExistFile(string fileName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles", fileName);
            return File.Exists(path);
        }

        public int AddEpisode(CourseEpisode episode, IFormFile episodeFile)
        {
            episode.EpisodeFileName = episodeFile.FileName;
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles",
                episode.EpisodeFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                episodeFile.CopyTo(stream);
            }

            _context.CourseEpisodes.Add(episode);
            _context.SaveChanges();
            return episode.EpisodeId;
        }

        public CourseEpisode GetCourseEpisodeById(int episodeId)
        {
            return _context.CourseEpisodes.Find(episodeId);
        }

        public void EditEpisode(CourseEpisode episode, IFormFile episodeFile)
        {
            if (episodeFile != null)
            {
                string deleteFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles",
                    episode.EpisodeFileName);
                File.Delete(deleteFilePath);

                episode.EpisodeFileName = episodeFile.FileName;
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles",
                    episode.EpisodeFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    episodeFile.CopyTo(stream);
                }
            }

            _context.CourseEpisodes.Update(episode);
            _context.SaveChanges();
        }

        public bool AddEpisode(AddEpisodeViewModel episodeViewModel, string userName)
        {
            var course = GetCourseById(episodeViewModel.CourseId);

            var userId = _context.Users.FirstOrDefault(u => u.UserName == userName).UserId;

            if (course == null || course.TeacherId != userId)
            {
                return false;
            }

            var episode = new CourseEpisode()
            {
                CourseId = course.CourseId,
                IsFree = episodeViewModel.IsFree,
                EpisodeTitle = episodeViewModel.EpisodeTitle,
                EpisodeTime = episodeViewModel.EpisodeTime,
                EpisodeFileName = episodeViewModel.EpisodeFileName,
            };

            _context.CourseEpisodes.Add(episode);
            _context.SaveChanges();

            return true;
        }

        public void AddComment(CourseComment comment)
        {
            _context.CourseComments.Add(comment);
            _context.SaveChanges();
        }

        public Tuple<List<CourseComment>, int> GetCourseComment(int courseId, int pageId = 1)
        {
            int take = 5;
            int skip = (pageId - 1) * take;

            int pageCount = _context.CourseComments.Where(c => !c.IsDelete && c.CourseId == courseId).Count() / take;

            if ((pageCount % 2) != 0)
            {
                pageCount += 1;
            }

            var query = _context.CourseComments.Include(c => c.User).Where(c => !c.IsDelete && c.CourseId == courseId).Skip(skip).Take(take).OrderByDescending(c => c.CreateDate).ToList();

            return Tuple.Create(query, pageCount);
        }

        public void AddVote(string userName, int courseId, bool vote)
        {
            int userId = _context.Users.Single(u => u.UserName == userName).UserId;

            var userVote = _context.CourseVotes.FirstOrDefault(c => c.UserId == userId && c.CourseId == courseId);

            if (userVote != null)
            {
                userVote.Vote = vote;
            }
            else
            {
                userVote = new CourseVote()
                {
                    CourseId = courseId,
                    UserId = userId,
                    Vote = vote
                };
                _context.CourseVotes.Add(userVote);
            }

            _context.SaveChanges();
        }

        public Tuple<int, int> GetCourseVotes(int courseId)
        {
            var votes = _context.CourseVotes.Where(c => c.CourseId == courseId).Select(c => c.Vote).ToList();

            return Tuple.Create(votes.Count(c => c == true), votes.Count(c => c == false));
        }
    }
}
