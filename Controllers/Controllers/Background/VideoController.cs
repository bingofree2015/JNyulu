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
    /* 微课堂  */
    [ControllerDetails(Area = "Background")]
    public class VideoController : DefaultController
    {
        protected readonly SchoolTermService _schoolTermService = SchoolTermService.GetInstance();
        protected readonly VideoService _videoService = VideoService.GetInstance();

        public void List(int pageIndex, int stID = 0)
        {
            PropertyBag["params"] = new
            {
                STID = stID,
                PageIndex = pageIndex
            };

            int _totalNums = 0, _pageSize = 12;
            if (pageIndex < 1) pageIndex = 1;

            IList<SchoolTerm> _schoolTermList = _schoolTermService.PaginatedSchoolTerm(1, 120, "ID", "1 = 1", ref _totalNums);
            PropertyBag["schoolTermList"] = _schoolTermList;

            string _conAttr = "1 = 1";
            if (stID > 0)
            {
                _conAttr += " AND STID = " + stID;
            }

            IList<Video> _videoList = _videoService.PaginatedVideo(pageIndex, _pageSize, "SortNum", _conAttr, ref _totalNums);
            PropertyBag["videoList"] = _videoList;

            string uri = "List.do?stID=" + stID + "&pageIndex={0}";
            string ltlShowPager = Paginated.BuildPager(uri, _totalNums, pageIndex, _pageSize);
            PropertyBag["ltlShowPager"] = ltlShowPager;
        }

        public void Create(int pageIndex, int stID = 0)
        {
            PropertyBag["params"] = new
            {
                STID = stID,
                PageIndex = pageIndex
            };
            int _totalNums = 0;
            IList<SchoolTerm> _schoolTermList = _schoolTermService.PaginatedSchoolTerm(1, 120, "ID", "1 = 1", ref _totalNums);
            PropertyBag["schoolTermList"] = _schoolTermList;

        }

        public void PostCreate(int stid, string title, string coversUrl, string mediaUrl, string mediaHtml, int sortNum, int permit)
        {
            int _code = 0;
            string _message = "上传视频失败";
            if (!string.IsNullOrEmpty(coversUrl))
            {
                Video _video = new Video();
                _video.STID = stid;
                _video.Title = title;
                _video.CoversUrl = coversUrl;
                _video.MediaUrl = mediaUrl;
                _video.MediaHtml = mediaHtml;
                _video.SortNum = sortNum;
                _video.Permit = permit;

                _code = _videoService.InsertVideo(_video);

                if (_code > 0)
                {
                    _message = "上传视频成功";
                }
                else
                {
                    _message = "对不起，上传视频失败，可能有重名！";
                }
            }
            else
            {
                _message = "图片的地址不能为空！";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void Edit(int videoID, int pageIndex, int stID = 0)
        {
            PropertyBag["params"] = new
            {
                STID = stID,
                PageIndex = pageIndex
            };
            int _totalNums = 0;
            IList<SchoolTerm> _schoolTermList = _schoolTermService.PaginatedSchoolTerm(1, 120, "ID", "1 = 1", ref _totalNums);
            PropertyBag["schoolTermList"] = _schoolTermList;

            Video _video = _videoService.GetVideoByID(videoID);
            PropertyBag["video"] = _video;
        }

        public void PostEdit(int videoID, int stid, string title, string coversUrl, string mediaUrl, string mediaHtml, int sortNum, int permit)
        {
            int _code = 0;
            string _message = "修改内容失败";
            if (!string.IsNullOrEmpty(coversUrl))
            {
                Video _video = new Video();
                _video.ID = videoID;

                _video.STID = stid;
                _video.Title = title;
                _video.CoversUrl = coversUrl;
                _video.MediaUrl = mediaUrl;
                _video.MediaHtml = mediaHtml;
                _video.Permit = permit;
                _video.SortNum = sortNum;

                _code = _videoService.UpdateVideo(_video);

                if (_code > 0)
                {
                    _message = "修改视频内容成功";
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

        public void PostDelete(int videoID)
        {
            int _code = 0;
            string _message = "删除视频信息失败";

            _code = _videoService.DeleteVideoByID(videoID);

            if (_code > 0)
            {
                _message = "删除视频信息成功";
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
