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
    public class BulletinBoardController : ApiController
    {
        protected readonly BulletinBoardService _bulletinBoardService = BulletinBoardService.GetInstance();

        /// <summary>
        /// 返回公告列表
        /// </summary>
        /// <param name="pageIndex"></param>
        public void List(int pageIndex)
        {
            int _totalNums = 0;
            int _pageSize = 20;
            if (pageIndex < 1) pageIndex = 1;

            //string _conAttr = "Receiver in (-2,-1)";
            string _conAttr = "1 = 1";

            IList<BulletinBoard> _bulletinBoardList = _bulletinBoardService.PaginatedBulletinBoard(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);

            _result.Code = 0;
            //_result.Data = _bulletinBoardList;
            _result.Data = new {
                List = _bulletinBoardList,
                CurrentPage = pageIndex,
                TotalPage = (_totalNums % _pageSize) > 0 ? (_totalNums / _pageSize) + 1 : _totalNums / _pageSize
            };
            string _jsonString = JsonConvert.SerializeObject(_result, Formatting.Indented);

            RenderText(_jsonString);
            CancelLayout();
        }

        /// <summary>
        /// 公告内容
        /// </summary>
        /// <param name="id"></param>
        public void Detail(int id)
        {

            BulletinBoard _bulletinBoard = _bulletinBoardService.GetBulletinBoardByID(id);

            _result.Code = 0;
            _result.Data = _bulletinBoard;
            string _jsonString = JsonConvert.SerializeObject(_result, Formatting.Indented);

            RenderText(_jsonString);
            CancelLayout();
        }

    }
}