/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 21:53:31
    修 改 者 : 
    修改时间 : 
    描    述 : PhotoGraph
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
    public class PhotoGraphService
    {
        #region 私有字段

        private static PhotoGraphService _instance = new PhotoGraphService();

        private IPhotoGraphDao _photoGraphDao = null;

        #endregion

        #region 构造函数

        private PhotoGraphService() 
        {
            _photoGraphDao =  new PhotoGraphSqlMapDao();
        }

        #endregion

        #region 公共方法

        public static PhotoGraphService GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// 获取PhotoGraph列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="photoGraph">PhotoGraph</param>
        /// <returns>PhotoGraph集合</returns>
        public IList<PhotoGraph> GetBasePhotoGraph(PhotoGraph photoGraph)
        {
            return _photoGraphDao.GetBasePhotoGraph(photoGraph);
        }

        /// <summary>
        /// 获取PhotoGraph列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>PhotoGraph</returns>
        public PhotoGraph GetBasePhotoGraphByID(int id)
        {
            return _photoGraphDao.GetBasePhotoGraphByID(id);
        }

        /// <summary>
        /// 获取PhotoGraph列表(包含父对象)
        /// </summary>
        /// <param name="photoGraph">PhotoGraph</param>
        /// <returns>PhotoGraph集合</returns>
        public IList<PhotoGraph> GetPhotoGraph(PhotoGraph photoGraph)
        {
            return _photoGraphDao.GetPhotoGraph(photoGraph);
        }

        /// <summary>
        /// 获取PhotoGraph列表(包含父对象)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>PhotoGraph</returns>
        public PhotoGraph GetPhotoGraphByID(int id)
        {
            return _photoGraphDao.GetPhotoGraphByID(id);
        }

        /// <summary>
        /// 插入PhotoGraph
        /// </summary>
        /// <param name="photoGraph">PhotoGraph</param>
        public int InsertPhotoGraph(PhotoGraph photoGraph)
        {
            int _id = -1;
            try
            {
                //_daoManager.BeginTransaction();
                _id = _photoGraphDao.InsertPhotoGraph(photoGraph);
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
        /// 分页 PhotoGraph
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        public IList<PhotoGraph> PaginatedPhotoGraph(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum)
        {
            return _photoGraphDao.PaginatedPhotoGraph(pageIndex, pageSize, sortFields, conAttr, ref totalNum); 
        }

        /// <summary>
        /// 更新PhotoGraph
        /// </summary>
        /// <param name="photoGraph">PhotoGraph</param>
        public int UpdatePhotoGraph(PhotoGraph photoGraph)
        {
            return _photoGraphDao.UpdatePhotoGraph(photoGraph);
        }

        /// <summary>
        /// 删除PhotoGraph
        /// </summary>
        /// <param name="id">自增型ID</param>
        public int DeletePhotoGraphByID(int id)
        {
            return _photoGraphDao.DeletePhotoGraphByID(id);
        }

        #endregion
    }
}
