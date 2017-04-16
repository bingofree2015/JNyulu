using System;
using System.IO;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;

using Castle.MonoRail.Framework;

using JNyuluSoft.Model;
using JNyuluSoft.Service;

namespace JNyuluSoft.Web.Controllers
{
    /* 班级 - 管理 */
    [ControllerDetails(Area = "Background")]
    public class SchoolTermController : DefaultController 
    {
        protected readonly SchoolTermService _schoolTermService = SchoolTermService.GetInstance();
        protected readonly GradeService _gradeService = GradeService.GetInstance();

        public void List()
        {
            Employee _logonUser = Session["logonUser"] as Employee;

            int _totalNums = 0, _pageSize = 12, pageIndex = 1;
            string _conAttr = "1 = 1";
            if (_logonUser.UserType == 2 && _logonUser.GradeID > 0)
                _conAttr = "SchoolTerm.GradeID = " + _logonUser.GradeID;

            IList<SchoolTerm> _schoolTermList = _schoolTermService.PaginatedSchoolTerm(pageIndex, _pageSize, "SchoolTerm.ID", _conAttr, ref _totalNums);
            PropertyBag["schoolTermList"] = _schoolTermList;
        }

        public void Create()
        {
            Employee _logonUser = Session["logonUser"] as Employee;

            int _totalNums = 0, _pageSize = 12, pageIndex = 1;
            string _conAttr = "1 = 1";
            if (_logonUser.UserType == 2 && _logonUser.GradeID > 0)
                _conAttr = "Grade.ID = " + _logonUser.GradeID;

            IList<Grade> _gradeList = _gradeService.PaginatedGrade(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);
            PropertyBag["gradeList"] = _gradeList;
        }

        public void PostCreate(string name,int gradeID)
        {
            int _code = 0;
            string _message = "对不起，添加失败";

            if (!string.IsNullOrEmpty(name))
            {
                SchoolTerm _schoolTerm = new SchoolTerm();

                _schoolTerm.Name = name;
                _schoolTerm.GradeID = gradeID;

                _code = _schoolTermService.InsertSchoolTerm(_schoolTerm);

                if(_code > 0)
                    _message = "添加成功";
                else
                    _message = "对不起，添加失败，班级名有可能重名了！";
             }
            else
            {
                _message = "对不起，班级名称不能为空";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void Edit(int schoolTermID)
        {
            Employee _logonUser = Session["logonUser"] as Employee; 

            SchoolTerm _schoolTerm = _schoolTermService.GetSchoolTermByID(schoolTermID);
            PropertyBag["schoolTerm"] = _schoolTerm;

            int _totalNums = 0, _pageSize = 12, pageIndex = 1;
            string _conAttr = "1 = 1";
            if (_logonUser.UserType == 2 && _logonUser.GradeID > 0)
                _conAttr = "Grade.ID = " + _logonUser.GradeID;

            IList<Grade> _gradeList = _gradeService.PaginatedGrade(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);
            PropertyBag["gradeList"] = _gradeList;
        }

        public void PostEdit(int schoolTermID, string name,int gradeID)
        {
            int _code = 0;
            string _message = "修改班级名称失败";
            if (!string.IsNullOrEmpty(name))
            {
                SchoolTerm _schoolTerm = new SchoolTerm();
                _schoolTerm.ID = schoolTermID;

                _schoolTerm.Name = name;
                _schoolTerm.GradeID = gradeID; 

                _code = _schoolTermService.UpdateSchoolTerm(_schoolTerm);

                if (_code > 0)
                {
                    _message = "修改班级名称";
                }
                else
                {
                    _message = "对不起，修改失败！";
                }
            }
            else
            {
                _message = "班级名称不能为空！";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void PostDelete(int schoolTermID)
        {
            int _code = 0;
            string _message = "对不起，删除失败";

            if (schoolTermID > 0)
            {
                _code = _schoolTermService.DeleteSchoolTermByID(schoolTermID); 

                if (_code > 0)
                    _message = "删除成功";
                else
                    _message = "对不起，删除失败，此记录可能不存在！";
            }
            else
            {
                _message = "对不起，参数不能小于零";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }
    }
}
