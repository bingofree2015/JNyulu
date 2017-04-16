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
    /* �༶ - ���� */
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
            string _message = "�Բ������ʧ��";

            if (!string.IsNullOrEmpty(name))
            {
                SchoolTerm _schoolTerm = new SchoolTerm();

                _schoolTerm.Name = name;
                _schoolTerm.GradeID = gradeID;

                _code = _schoolTermService.InsertSchoolTerm(_schoolTerm);

                if(_code > 0)
                    _message = "��ӳɹ�";
                else
                    _message = "�Բ������ʧ�ܣ��༶���п��������ˣ�";
             }
            else
            {
                _message = "�Բ��𣬰༶���Ʋ���Ϊ��";
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
            string _message = "�޸İ༶����ʧ��";
            if (!string.IsNullOrEmpty(name))
            {
                SchoolTerm _schoolTerm = new SchoolTerm();
                _schoolTerm.ID = schoolTermID;

                _schoolTerm.Name = name;
                _schoolTerm.GradeID = gradeID; 

                _code = _schoolTermService.UpdateSchoolTerm(_schoolTerm);

                if (_code > 0)
                {
                    _message = "�޸İ༶����";
                }
                else
                {
                    _message = "�Բ����޸�ʧ�ܣ�";
                }
            }
            else
            {
                _message = "�༶���Ʋ���Ϊ�գ�";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void PostDelete(int schoolTermID)
        {
            int _code = 0;
            string _message = "�Բ���ɾ��ʧ��";

            if (schoolTermID > 0)
            {
                _code = _schoolTermService.DeleteSchoolTermByID(schoolTermID); 

                if (_code > 0)
                    _message = "ɾ���ɹ�";
                else
                    _message = "�Բ���ɾ��ʧ�ܣ��˼�¼���ܲ����ڣ�";
            }
            else
            {
                _message = "�Բ��𣬲�������С����";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }
    }
}
