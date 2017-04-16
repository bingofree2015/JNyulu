/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 23:34:23
    修 改 者 : 
    修改时间 : 
    描    述 : ReportCard
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
    public class ReportCardService
    {
        #region 私有字段

        private static ReportCardService _instance = new ReportCardService();

        private IReportCardDao _reportCardDao = null;

        #endregion

        #region 构造函数

        private ReportCardService() 
        {
            _reportCardDao = new ReportCardSqlMapDao();
        }

        #endregion

        #region 公共方法

        public static ReportCardService GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// 获取ReportCard列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="reportCard">ReportCard</param>
        /// <returns>ReportCard集合</returns>
        public IList<ReportCard> GetBaseReportCard(ReportCard reportCard)
        {
            return _reportCardDao.GetBaseReportCard(reportCard);
        }

        /// <summary>
        /// 获取ReportCard列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ReportCard</returns>
        public ReportCard GetBaseReportCardByID(int id)
        {
            return _reportCardDao.GetBaseReportCardByID(id);
        }

        /// <summary>
        /// 获取ReportCard列表(包含父对象)
        /// </summary>
        /// <param name="reportCard">ReportCard</param>
        /// <returns>ReportCard集合</returns>
        public IList<ReportCard> GetReportCard(IDictionary dict)
        {
            return _reportCardDao.GetReportCard(dict);
        }

        /// <summary>
        /// 获取ReportCard列表(包含父对象)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ReportCard</returns>
        public ReportCard GetReportCardByID(int id)
        {
            return _reportCardDao.GetReportCardByID(id);
        }

        /// <summary>
        /// 插入ReportCard
        /// </summary>
        /// <param name="reportCard">ReportCard</param>
        public int InsertReportCard(ReportCard reportCard)
        {
            int _id = -1;

            try
            {
                //_daoManager.BeginTransaction();
                _id = _reportCardDao.InsertReportCard(reportCard);
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
        /// 分页 ReportCard
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        public IList<ReportCard> PaginatedReportCard(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum)
        {
            return _reportCardDao.PaginatedReportCard(pageIndex, pageSize, sortFields, conAttr, ref totalNum);
        }
        /// <summary>
        /// 更新ReportCard
        /// </summary>
        /// <param name="reportCard">ReportCard</param>
        public int UpdateReportCard(ReportCard reportCard)
        {
            return _reportCardDao.UpdateReportCard(reportCard);
        }

        /// <summary>
        /// 删除ReportCard
        /// </summary>
        /// <param name="id"></param>
        public int DeleteReportCardByID(int id)
        {
            return _reportCardDao.DeleteReportCardByID(id);
        }

        #endregion
    }
}
