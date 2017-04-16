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
    public class CampusController : DefaultController
    {
        protected readonly CampusService _campusService = CampusService.GetInstance();

        public void List()
        {
            int _totalNums = 0,_pageSize = 12,pageIndex = 1;
            string _conAttr = "1 = 1";

            IList<Campus> _campusList = _campusService.PaginatedCampus(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);

            PropertyBag["campusList"] = _campusList;
        }

        public void Edit(int campusID)
        {

            Campus _campus = _campusService.GetCampusByID(campusID);
            PropertyBag["campus"] = _campus;
        }

        public void PostEdit(int campusID, string name)
        {
            int _code = 0;
            string _message = "�޸�У��ʧ��";
            if (!string.IsNullOrEmpty(name))
            {
                Campus _campus = new Campus();
                _campus.ID = campusID;

                _campus.Name = name;

                _code = _campusService.UpdateCampus(_campus);

                if (_code > 0)
                {
                    _message = "�޸�У������";
                }
                else
                {
                    _message = "�Բ����޸�ʧ�ܣ�";
                }
            }
            else
            {
                _message = "У�����Ʋ���Ϊ�գ�";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void PostDelete(int campusID)
        {
            int _code = 0;
            string _message = "ɾ��У��ʧ��";

            _code = _campusService.DeleteCampusByID(campusID);

            if (_code > 0)
            {
                _message = "ɾ��У���ɹ�";
            }
            else
            {
                _message = "�Բ����޸�ʧ�ܣ�";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void PostCreate(string name)
        {
            int _code = 0;
            string _message = "����У��ʧ��";
            if (!string.IsNullOrEmpty(name))
            {
                Campus _campus = new Campus();
                _campus.Name = name;
                _code = _campusService.InsertCampus(_campus);

                if (_code > 0)
                {
                    _message = "����У���ɹ�";
                }
                else
                {
                    _message = "�Բ��𣬴���ʧ�ܣ�������������";
                }
            }
            else
            {
                _message = "У�����Ʋ���Ϊ�գ�";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }
    }
}
