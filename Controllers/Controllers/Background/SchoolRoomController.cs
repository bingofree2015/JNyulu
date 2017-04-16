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
    /* 基本信息管理  */
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
            string _message = "创建教室失败";
            if (!string.IsNullOrEmpty(name))
            {
                SchoolRoom _schoolRoom = new SchoolRoom();
                _schoolRoom.Name = name;
                _schoolRoom.CampusID = campusID;

                _code = _schoolRoomService.InsertSchoolRoom(_schoolRoom);

                if (_code > 0)
                {
                    _message = "创建教室成功";
                }
                else
                {
                    _message = "对不起，创建失败，可能有重名！";
                }
            }
            else
            {
                _message = "教室名称不能为空！";
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
            string _message = "修改教室失败";
            if (!string.IsNullOrEmpty(name))
            {
                SchoolRoom _schoolRoom = new SchoolRoom();
                _schoolRoom.ID = schoolRoomID;
                _schoolRoom.CampusID = campusID;

                _schoolRoom.Name = name;

                _code = _schoolRoomService.UpdateSchoolRoom(_schoolRoom);

                if (_code > 0)
                {
                    _message = "修改教室档案";
                }
                else
                {
                    _message = "对不起，修改失败！";
                }
            }
            else
            {
                _message = "教室名称不能为空！";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void PostDelete(int schoolRoomID)
        {
            int _code = 0;
            string _message = "删除教室失败";

            _code = _schoolRoomService.DeleteSchoolRoomByID(schoolRoomID);

            if (_code > 0)
            {
                _message = "删除教室成功";
            }
            else
            {
                _message = "对不起，记录不存在！";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }
    }
}
