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
    /* 网站咨询  */
    [ControllerDetails(Area = "Background")]
    public class PhotoGraphController : DefaultController
    {
        protected readonly PhotoGraphService _photoGraphService = PhotoGraphService.GetInstance();

        public void List(int pageIndex)
        {
            PropertyBag["params"] = new
            {
                PageIndex = pageIndex
            };

            int _totalNums = 0, _pageSize = 12;
            if (pageIndex < 1) pageIndex = 1;

            string _conAttr = "1 = 1";

            IList<PhotoGraph> _photoGraphList = _photoGraphService.PaginatedPhotoGraph(pageIndex, _pageSize, "SortNum", _conAttr, ref _totalNums);
            PropertyBag["photoGraphList"] = _photoGraphList;

            string uri = "List.do?pageIndex={0}";
            string ltlShowPager = Paginated.BuildPager(uri, _totalNums, pageIndex, _pageSize);

            PropertyBag["ltlShowPager"] = ltlShowPager;
        }

        public void Create(int pageIndex)
        {
            PropertyBag["params"] = new
            {
                PageIndex = pageIndex
            };
        }

        public void PostCreate(int catalogID, string subject, string imageUrl, string linkUrl)
        {
            int _code = 0;
            string _message = "上传图片失败";
            if (!string.IsNullOrEmpty(imageUrl))
            {
                PhotoGraph _photoGraph = new PhotoGraph();
                _photoGraph.CatalogID = catalogID;
                _photoGraph.Subject = subject;
                _photoGraph.ImageUrl = imageUrl;
                _photoGraph.LinkUrl = linkUrl;

                _code = _photoGraphService.InsertPhotoGraph(_photoGraph);

                if (_code > 0)
                {
                    _message = "上传图片成功";
                }
                else
                {
                    _message = "对不起，上传图片失败，可能有重名！";
                }
            }
            else
            {
                _message = "图片的地址不能为空！";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void Edit(int photoGraphID,int pageIndex)
        {
            PropertyBag["params"] = new
            {
                PageIndex = pageIndex
            };

            PhotoGraph _photoGraph = _photoGraphService.GetPhotoGraphByID(photoGraphID);
            PropertyBag["photoGraph"] = _photoGraph;
        }

        public void PostEdit(int photoGraphID, int catalogID, string subject, string imageUrl, string linkUrl, int sortNum)
        {
            int _code = 0;
            string _message = "修改内容失败";
            if (!string.IsNullOrEmpty(imageUrl))
            {
                PhotoGraph _photoGraph = new PhotoGraph();
                _photoGraph.ID = photoGraphID;
                _photoGraph.CatalogID = catalogID;
                _photoGraph.Subject = subject;
                _photoGraph.ImageUrl = imageUrl;
                _photoGraph.LinkUrl = linkUrl;
                _photoGraph.SortNum = sortNum;

                _code = _photoGraphService.UpdatePhotoGraph(_photoGraph);

                if (_code > 0)
                {
                    _message = "修改图片内容成功";
                }
                else
                {
                    _message = "对不起，修改失败！";
                }
            }
            else
            {
                _message = "图片路径不能为空！";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void PostDelete(int photoGraphID)
        {
            int _code = 0;
            string _message = "删除图片信息失败";

            _code = _photoGraphService.DeletePhotoGraphByID(photoGraphID);

            if (_code > 0)
            {
                _message = "删除图片信息成功";
            }
            else
            {
                _message = "对不起，删除失败！";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }
    }
}
