/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 21:53:31
    修 改 者 : 
    修改时间 : 
    描    述 : Campus
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
    public class CampusService
    {
        #region 私有字段

        private static CampusService _instance = new CampusService();

        private ICampusDao _campusDao = null;

        #endregion

        #region 构造函数

        private CampusService() 
        {
            _campusDao = new CampusSqlMapDao();

        }

        #endregion

        #region 公共方法

        public static CampusService GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// 获取Campus列表
        /// </summary>
        /// <param name="campus">Campus</param>
        /// <returns>Campus集合</returns>
        public IList<Campus> GetCampus(Campus campus)
        {
            return _campusDao.GetCampus(campus);
        }

        /// <summary>
        /// 获取Campus列表
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Campus</returns>
        public Campus GetCampusByID(int id)
        {
            return _campusDao.GetCampusByID(id);
        }

        /// <summary>
        /// 插入Campus
        /// </summary>
        /// <param name="campus">Campus</param>
        public int InsertCampus(Campus campus)
        {
            int _id = -1;
            try
            {
                //_daoManager.BeginTransaction();
                _id = _campusDao.InsertCampus(campus);
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
        /// 分页 Campus
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        public IList<Campus> PaginatedCampus(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum)
        {
            return _campusDao.PaginatedCampus(pageIndex, pageSize, sortFields, conAttr, ref totalNum); 
        }

        /// <summary>
        /// 更新Campus
        /// </summary>
        /// <param name="campus">Campus</param>
        public int UpdateCampus(Campus campus)
        {
            return _campusDao.UpdateCampus(campus);
        }

        /// <summary>
        /// 删除Campus
        /// </summary>
        /// <param name="id">自增型ID</param>
        public int DeleteCampusByID(int id)
        {
            return _campusDao.DeleteCampusByID(id);
        }

        #endregion
    }
}
