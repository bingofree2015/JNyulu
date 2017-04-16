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
            string _message = "修改校区失败";
            if (!string.IsNullOrEmpty(name))
            {
                Campus _campus = new Campus();
                _campus.ID = campusID;

                _campus.Name = name;

                _code = _campusService.UpdateCampus(_campus);

                if (_code > 0)
                {
                    _message = "修改校区档案";
                }
                else
                {
                    _message = "对不起，修改失败！";
                }
            }
            else
            {
                _message = "校区名称不能为空！";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void PostDelete(int campusID)
        {
            int _code = 0;
            string _message = "删除校区失败";

            _code = _campusService.DeleteCampusByID(campusID);

            if (_code > 0)
            {
                _message = "删除校区成功";
            }
            else
            {
                _message = "对不起，修改失败！";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void PostCreate(string name)
        {
            int _code = 0;
            string _message = "创建校区失败";
            if (!string.IsNullOrEmpty(name))
            {
                Campus _campus = new Campus();
                _campus.Name = name;
                _code = _campusService.InsertCampus(_campus);

                if (_code > 0)
                {
                    _message = "创建校区成功";
                }
                else
                {
                    _message = "对不起，创建失败，可能有重名！";
                }
            }
            else
            {
                _message = "校区名称不能为空！";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }
    }
}
