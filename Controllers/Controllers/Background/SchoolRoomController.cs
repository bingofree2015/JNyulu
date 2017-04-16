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
    public class SchoolRoomController : DefaultController
    {
        protected readonly SchoolRoomService _schoolRoomService = SchoolRoomService.GetInstance();
        protected readonly CampusService _campusService = CampusService.GetInstance();

        public void List()
        {
            int _totalNums = 0, _pageSize = 12, pageIndex = 1;
            string _conAttr = "1 = 1";

            IList<Campus> _campusList = _campusService.PaginatedCampus(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);
            PropertyBag["campusList"] = _campusList;

            IList<SchoolRoom> _schoolRoomList = _schoolRoomService.PaginatedSchoolRoom(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);
            PropertyBag["schoolRoomList"] = _schoolRoomList;
        }

        public void PostCreate(string name, int campusID)
        {
            int _code = 0;
            string _message = "��������ʧ��";
            if (!string.IsNullOrEmpty(name))
            {
                SchoolRoom _schoolRoom = new SchoolRoom();
                _schoolRoom.Name = name;
                _schoolRoom.CampusID = campusID;

                _code = _schoolRoomService.InsertSchoolRoom(_schoolRoom);

                if (_code > 0)
                {
                    _message = "�������ҳɹ�";
                }
                else
                {
                    _message = "�Բ��𣬴���ʧ�ܣ�������������";
                }
            }
            else
            {
                _message = "�������Ʋ���Ϊ�գ�";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void Edit(int schoolRoomID)
        {
            int _totalNums = 0, _pageSize = 12, pageIndex = 1;
            string _conAttr = "1 = 1";

            IList<Campus> _campusList = _campusService.PaginatedCampus(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);
            PropertyBag["campusList"] = _campusList;

            SchoolRoom _schoolRoom = _schoolRoomService.GetSchoolRoomByID(schoolRoomID);
            PropertyBag["schoolRoom"] = _schoolRoom;
        }

        public void PostEdit(int schoolRoomID, string name, int campusID)
        {
            int _code = 0;
            string _message = "�޸Ľ���ʧ��";
            if (!string.IsNullOrEmpty(name))
            {
                SchoolRoom _schoolRoom = new SchoolRoom();
                _schoolRoom.ID = schoolRoomID;
                _schoolRoom.CampusID = campusID;

                _schoolRoom.Name = name;

                _code = _schoolRoomService.UpdateSchoolRoom(_schoolRoom);

                if (_code > 0)
                {
                    _message = "�޸Ľ��ҵ���";
                }
                else
                {
                    _message = "�Բ����޸�ʧ�ܣ�";
                }
            }
            else
            {
                _message = "�������Ʋ���Ϊ�գ�";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void PostDelete(int schoolRoomID)
        {
            int _code = 0;
            string _message = "ɾ������ʧ��";

            _code = _schoolRoomService.DeleteSchoolRoomByID(schoolRoomID);

            if (_code > 0)
            {
                _message = "ɾ�����ҳɹ�";
            }
            else
            {
                _message = "�Բ��𣬼�¼�����ڣ�";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }
    }
}
