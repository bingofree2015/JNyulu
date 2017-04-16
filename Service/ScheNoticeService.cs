/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 23:34:23
    修 改 者 : 
    修改时间 : 
    描    述 : ScheNotice
===================================================== */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using IBatisNet.DataAccess;

using JNyuluSoft.Persistence;
using JNyuluSoft.Persistence.Impl;
using JNyuluSoft.Model;


namespace JNyuluSoft.Service
{
    public class ScheNoticeService
    {
        #region 私有字段

        private static ScheNoticeService _instance = new ScheNoticeService();

        private IScheNoticeDao _scheNoticeDao = null;

        #endregion

        #region 构造函数

        private ScheNoticeService() 
        {
            _scheNoticeDao = new ScheNoticeSqlMapDao();
        }

        #endregion

        #region 公共方法

        public static ScheNoticeService GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// 获取ScheNotice列表(包含父对象)
        /// </summary>
        /// <param name="scheNotice">ScheNotice</param>
        /// <returns>ScheNotice集合</returns>
        public IList<ScheNotice> GetScheNotice(IDictionary dict)
        {
            return _scheNoticeDao.GetScheNotice(dict);
        }

        /// <summary>
        /// 获取ScheNotice列表(包含父对象)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ScheNotice</returns>
        public ScheNotice GetScheNoticeByID(int id)
        {
            return _scheNoticeDao.GetScheNoticeByID(id);
        }

        /// <summary>
        /// 插入ScheNotice
        /// </summary>
        /// <param name="scheNotice">ScheNotice</param>
        public int InsertScheNotice(ScheNotice scheNotice)
        {
            int _id = -1;

            try
            {
                //_daoManager.BeginTransaction();
                _id = _scheNoticeDao.InsertScheNotice(scheNotice);
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
        /// 分页 ScheNotice
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        public IList<ScheNotice> PaginatedScheNotice(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum)
        {
            return _scheNoticeDao.PaginatedScheNotice(pageIndex, pageSize, sortFields, conAttr, ref totalNum);
        }
        /// <summary>
        /// 更新ScheNotice
        /// </summary>
        /// <param name="scheNotice">ScheNotice</param>
        public int UpdateScheNotice(ScheNotice scheNotice)
        {
            return _scheNoticeDao.UpdateScheNotice(scheNotice);
        }

        /// <summary>
        /// 删除ScheNotice
        /// </summary>
        /// <param name="id"></param>
        public int DeleteScheNoticeByID(int id)
        {
            return _scheNoticeDao.DeleteScheNoticeByID(id);
        }

        #endregion
    }
}
