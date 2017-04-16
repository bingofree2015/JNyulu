using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MonoRail.Framework;

using Newtonsoft.Json;

using JNyuluSoft.Model;
using JNyuluSoft.Service;
using System.Collections;

namespace JNyuluSoft.Web.Api.Controllers
{
    [ControllerDetails(Area = "Api")]
    public class StudentController : ApiController
    {
        protected readonly EmployeeService _employeeService = EmployeeService.GetInstance();

        protected readonly RegistrationService _registrationService = RegistrationService.GetInstance();
        protected readonly AssessmentService _assessmentService = AssessmentService.GetInstance();

        protected readonly ReportCardService _reportCardService = ReportCardService.GetInstance();
        protected readonly SyllabusService _syllabusService = SyllabusService.GetInstance();
        protected readonly ScheNoticeService _scheNoticeService = ScheNoticeService.GetInstance();

        protected readonly NotificationService _notificationService = NotificationService.GetInstance();

        protected readonly CampusService _campusService = CampusService.GetInstance();

        /// <summary>
        /// 班级考评列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="pageIndex"></param>
        public void AssessmentList(int userID)
        {
            Assessment _assessment = new Assessment();
            _assessment.EmployeeID = userID;
            IList<Assessment> _assessmentList = _assessmentService.GetAssessment(_assessment);

            _result.Code = 0;
            _result.Data = _assessmentList;

            string _jsonString = JsonConvert.SerializeObject(_result, Formatting.Indented);

            RenderText(_jsonString);
            CancelLayout();
        }

        /// <summary>
        /// 学生成绩列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        public void ReportCardList(int userID)
        {
            IList<Registration> _registrationList = null;

            Registration _registration = new Registration();
            _registration.EmployeeID = userID;

            _registrationList = _registrationService.GetRegistration(_registration);
            foreach (Registration Registration in _registrationList)
            {
                IDictionary _hash = new Hashtable();

                _hash.Add("EmployeeID", Registration.EmployeeID);

                Registration.ReportCardList = _reportCardService.GetReportCard(_hash);
            }

            _result.Code = 0;
            _result.Data = _registrationList;
            string _jsonString = JsonConvert.SerializeObject(_result, Formatting.Indented);

            RenderText(_jsonString);
            CancelLayout();
        }

        /// <summary>
        /// 学生课程表
        /// </summary>
        /// <param name="userID">用户ID</param>
        public void SyllabusList(int userID)
        {
            IDictionary _hash = new Hashtable();

            _hash.Add("EmployeeID", userID);

            IList<Syllabus> _syllabusList = _syllabusService.GetSyllabus(_hash);

            _result.Code = 0;
            _result.Data = _syllabusList;
            string _jsonString = JsonConvert.SerializeObject(_result, Formatting.Indented);

            RenderText(_jsonString);
            CancelLayout();
        }

        /// <summary>
        /// 开课通知
        /// </summary>
        /// <param name="userName">手机号</param>
        public void ScheNoticeList(string userName)
        {
            IDictionary _hash = new Hashtable();

            _hash.Add("UserName", userName);

            IList<ScheNotice> _scheNoticeList = _scheNoticeService.GetScheNotice(_hash);

            _result.Code = 0;
            _result.Data = _scheNoticeList;
            string _jsonString = JsonConvert.SerializeObject(_result, Formatting.Indented);

            RenderText(_jsonString);
            CancelLayout();
        }

        /// <summary>
        /// 我收到的推送消息
        /// </summary>
        /// <param name="userID">用户ID</param>
        public void NotificationList(int userID)
        {
            Employee _employee = _employeeService.GetEmployeeByID(userID);
            if (_employee != null)
            {
                int _totalNum = 0;

                string _conAttr = "Receivers LIKE '%," + _employee.Name + ",%' OR stID IN(SELECT stID FROM [JNyulu_db].[dbo].[Registration] WHERE EmployeeID = " + _employee.ID + ") OR stID = 0";
                //Response.Write(_conAttr);

                IList<Notification> _notificationList = _notificationService.PaginatedNotification(1, 120, "ID", _conAttr, ref _totalNum);
                _result.Code = 0;
                _result.Data = _notificationList;
            }
            string _jsonString = JsonConvert.SerializeObject(_result, Formatting.Indented);

            RenderText(_jsonString);
            CancelLayout();
        }

    }
}