using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MonoRail.Framework;

using Newtonsoft.Json;

using JNyuluSoft.Model;
using JNyuluSoft.Service;

namespace JNyuluSoft.Web.Api.Controllers
{
    [ControllerDetails(Area = "Api")]
    public class ArticleController : ApiController
    {
        protected readonly CatalogService _catalogService = CatalogService.GetInstance();
        protected readonly ArticleService _articleService = ArticleService.GetInstance();
        protected readonly PhotoGraphService _photoGraphService = PhotoGraphService.GetInstance();
        protected readonly VideoService _videoService = VideoService.GetInstance();
        /// <summary>
        /// 返回幻灯片图版列表
        /// </summary>
        /// <param name="catalogID"></param>
        public void SlidePhotoGraphList(int catalogID = 5)
        {
            int _totalNums = 0, _pageSize = 6, pageIndex = 1;

            IList<PhotoGraph> _photoGrapList = _photoGraphService.PaginatedPhotoGraph(pageIndex, _pageSize, "SortNum", "catalogID = " + catalogID, ref _totalNums);

            _result.Code = 0;
            _result.Data = new
            {
                List = _photoGrapList,
                CurrentPage = pageIndex,
                TotalPage = (_totalNums % _pageSize) > 0 ? (_totalNums / _pageSize) + 1 : _totalNums / _pageSize
            };
            string _jsonString = JsonConvert.SerializeObject(_result, Formatting.Indented);

            RenderText(_jsonString);
            CancelLayout();
        }

        /// <summary>
        /// 返回分类列表
        /// </summary>
        /// <param name="pageIndex"></param>
        public void GetCatalogList(string moduleCn = "Article", int pageIndex = 1)
        {
            int _totalNums = 0, _pageSize = 12;
            pageIndex = pageIndex < 1 ? 1 : pageIndex;

            string _conAttr = "1 = 1";
            if (!string.IsNullOrEmpty(moduleCn))
            {
                _conAttr += " AND moduleCn = '" + moduleCn + "'";
            }

            IList<Catalog> _catalogList = _catalogService.PaginatedCatalog(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);

            _result.Code = 0;
            //_result.Data = _catalogList;
            _result.Data = new
            {
                List = _catalogList,
                CurrentPage = pageIndex,
                TotalPage = (_totalNums % _pageSize) > 0 ? (_totalNums / _pageSize) + 1 : _totalNums / _pageSize
            };
            string _jsonString = JsonConvert.SerializeObject(_result, Formatting.Indented);

            RenderText(_jsonString);
            CancelLayout();
        }

        /// <summary>
        /// 返回指定分类下的文章列表
        /// </summary>
        /// <param name="catalogID">分类ID</param>
        /// <param name="pageIndex"></param>
        public void ArticleList(int catalogID, int pageIndex)
        {
            int _totalNums = 0;
            int _pageSize = 20;
            if (pageIndex < 1) pageIndex = 1;

            string _conAttr = "catalogID = " + catalogID + "";

            IList<Article> _articleList = _articleService.PaginatedArticle(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);

            _result.Code = 0;
            //_result.Data = _articleList;
            _result.Data = new
            {
                List = _articleList,
                CurrentPage = pageIndex,
                TotalPage = (_totalNums % _pageSize) > 0 ? (_totalNums / _pageSize) + 1 : _totalNums / _pageSize
            };
            string _jsonString = JsonConvert.SerializeObject(_result, Formatting.Indented);

            RenderText(_jsonString);
            CancelLayout();
        }

        /// <summary>
        /// 文章信息
        /// </summary>
        /// <param name="articleID"></param>
        public void Article(int articleID)
        {
            Article _article = _articleService.GetArticleByID(articleID);

            _result.Code = 0;
            _result.Data = _article;
            string _jsonString = JsonConvert.SerializeObject(_result, Formatting.Indented);

            RenderText(_jsonString);
            CancelLayout();
        }

        /// <summary>
        /// 返回视频列表(雨露微课堂)
        /// </summary>
        /// <param name="pageIndex"></param>
        public void VideoList(int stID = 0,int pageIndex = 1, int permit = -1)
        {
            int _totalNums = 0, _pageSize = 12;
            if (pageIndex < 1) pageIndex = 1;

            string _conAttr = "1 = 1";
            if (stID > 0) {
                _conAttr += "STID = " + stID;
            }
            if (permit >= 0) {
                _conAttr += "Permit=" + permit;
            }

            IList<Video> _videoList = _videoService.PaginatedVideo(pageIndex, _pageSize, "SortNum", _conAttr, ref _totalNums);

            _result.Code = 0;
            _result.Data = new
            {
                List = _videoList,
                CurrentPage = pageIndex,
                TotalPage = (_totalNums % _pageSize) > 0 ? (_totalNums / _pageSize) + 1 : _totalNums / _pageSize
            };
            string _jsonString = JsonConvert.SerializeObject(_result, Formatting.Indented);
            //_jsonString = _jsonString.Replace("height=498", "height=292").Replace("width=510", "width=300");

            RenderText(_jsonString);
            CancelLayout();
        }

        /// <summary>
        /// 视频信息
        /// </summary>
        /// <param name="videoID"></param>
        public void Video(int videoID)
        {
            Video _video = _videoService.GetVideoByID(videoID);

            _result.Code = 0;
            _result.Data = _video;
            string _jsonString = JsonConvert.SerializeObject(_result, Formatting.Indented);
            _jsonString = _jsonString.Replace("height=498", "height=292").Replace("width=510", "width=300");

            RenderText(_jsonString);
            CancelLayout();
        }
    }
}