/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 21:53:31
    修 改 者 : 
    修改时间 : 
    描    述 : Video
===================================================== */

using System;
using System.Collections.Generic;
using System.Text;

using IBatisNet.DataAccess;

using JNyuluSoft.Persistence;
using JNyuluSoft.Persistence.Impl;
using JNyuluSoft.Model;


namespace JNyuluSoft.Service
{
    public class VideoService
    {
        #region 私有字段

        private static VideoService _instance = new VideoService();

        private IVideoDao _videoDao = null;
        #endregion

        #region 构造函数

        private VideoService() 
        {
            _videoDao =  new VideoSqlMapDao();
        }

        #endregion

        #region 公共方法

        public static VideoService GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// 获取Video列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="video">Video</param>
        /// <returns>Video集合</returns>
        public IList<Video> GetBaseVideo(Video video)
        {
            return _videoDao.GetBaseVideo(video);
        }

        /// <summary>
        /// 获取Video列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Video</returns>
        public Video GetBaseVideoByID(int id)
        {
            return _videoDao.GetBaseVideoByID(id);
        }

        /// <summary>
        /// 获取Video列表(包含父对象)
        /// </summary>
        /// <param name="video">Video</param>
        /// <returns>Video集合</returns>
        public IList<Video> GetVideo(Video video)
        {
            return _videoDao.GetVideo(video);
        }

        /// <summary>
        /// 获取Video列表(包含父对象)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Video</returns>
        public Video GetVideoByID(int id)
        {
            return _videoDao.GetVideoByID(id);
        }

        /// <summary>
        /// 插入Video
        /// </summary>
        /// <param name="video">Video</param>
        public int InsertVideo(Video video)
        {
            int _id = -1;
            try
            {
                //_daoManager.BeginTransaction();
                _id = _videoDao.InsertVideo(video);
                //_daoManager.CommitTransaction();
            }
            catch (Exception ex)
            {
                //_daoManager.RollBackTransaction();
                throw ex;
            }
            return _id;
        }

        /// <summary>
        /// 分页 Video
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        public IList<Video> PaginatedVideo(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum)
        {
            return _videoDao.PaginatedVideo(pageIndex, pageSize, sortFields, conAttr, ref totalNum); 
        }

        /// <summary>
        /// 更新Video
        /// </summary>
        /// <param name="video">Video</param>
        public int UpdateVideo(Video video)
        {
            return _videoDao.UpdateVideo(video);
        }

        /// <summary>
        /// 删除Video
        /// </summary>
        /// <param name="id">自增型ID</param>
        public int DeleteVideoByID(int id)
        {
            return _videoDao.DeleteVideoByID(id);
        }

        #endregion
    }
}
