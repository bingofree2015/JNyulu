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
    /* ������Ϣ����  */
    [ControllerDetails(Area = "Background")]
    public class GradeController : DefaultController
    {
        protected readonly GradeService _gradeService = GradeService.GetInstance();

        public void List()
        {
            Employee _logonUser = Session["logonUser"] as Employee;

            int _totalNums = 0, _pageSize = 12, pageIndex = 1;
            string _conAttr = "1 = 1";

            if (_logonUser.UserType == 2 && _logonUser.GradeID > 0)
                _conAttr = "Grade.ID = " + _logonUser.GradeID;

            IList<Grade> _gradeList = _gradeService.PaginatedGrade(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);

            PropertyBag["gradeList"] = _gradeList;
        }

        public void PostCreate(string sn,string name)
        {
            int _code = 0;
            string _message = "�����꼶ʧ��";
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(sn))
            {
                Grade _grade = new Grade();
                _grade.SN = sn;
                _grade.Name = name;
                _code = _gradeService.InsertGrade(_grade);

                if (_code > 0)
                {
                    _message = "�����꼶�ɹ�";
                }
                else
                {
                    _message = "�Բ��𣬴���ʧ�ܣ�������������";
                }
            }
            else
            {
                _message = "�꼶���Ʋ���Ϊ�գ�";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void Edit(int gradeID)
        {

            Grade _grade = _gradeService.GetGradeByID(gradeID);
            PropertyBag["grade"] = _grade;
        }

        public void PostEdit(int gradeID,string sn, string name)
        {
            int _code = 0;
            string _message = "�޸��꼶ʧ��";
            if (!string.IsNullOrEmpty(name))
            {
                Grade _grade = new Grade();
                _grade.ID = gradeID;
                _grade.SN = sn;
                _grade.Name = name;

                _code = _gradeService.UpdateGrade(_grade);

                if (_code > 0)
                {
                    _message = "�޸��꼶�ɹ�";
                }
                else
                {
                    _message = "�Բ����޸�ʧ�ܣ�";
                }
            }
            else
            {
                _message = "�꼶���Ʋ���Ϊ�գ�";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void PostDelete(int gradeID)
        {
            int _code = 0;
            string _message = "ɾ���꼶ʧ��";

            _code = _gradeService.DeleteGradeByID(gradeID);

            if (_code > 0)
            {
                _message = "ɾ���꼶�ɹ�";
            }
            else
            {
                _message = "�Բ����޸�ʧ�ܣ�";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }
    }
}
