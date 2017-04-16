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

using JNyuluSoft.Model;
using JNyuluSoft.Persistence;
using JNyuluSoft.Util;


namespace JNyuluSoft.Persistence.Impl
{
    /// <summary>
    /// ReportCard数据库持久化处理类
    /// </summary>
    public class ReportCardSqlMapDao : BaseSqlMapDao, IReportCardDao
    {
        /// <summary>
        /// 获取ReportCard列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="reportCard">ReportCard</param>
        /// <returns>ReportCard集合</returns>
        public IList<ReportCard> GetBaseReportCard(ReportCard reportCard)
        {
            return ExecuteQueryForList<ReportCard>("JNyulu.GetBaseReportCard", reportCard);
        }

        /// <summary>
        /// 获取ReportCard列表,不包含父对象,仅返回对象本身。
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ReportCard</returns>
        public ReportCard GetBaseReportCardByID(int id)
        {
            return ExecuteQueryForObject<ReportCard>("JNyulu.GetBaseReportCardByID", id);
        }

        /// <summary>
        /// 获取ReportCard列表(包含父对象)
        /// </summary>
        /// <param name="reportCard">ReportCard</param>
        /// <returns>ReportCard集合</returns>
        public IList<ReportCard> GetReportCard(IDictionary dict)
        {
            return ExecuteQueryForList<ReportCard>("JNyulu.GetReportCard", dict);
        }

        /// <summary>
        /// 获取ReportCard列表(包含父对象)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ReportCard</returns>
        public ReportCard GetReportCardByID(int id)
        {
            return ExecuteQueryForObject<ReportCard>("JNyulu.GetReportCardByID", id);
        }

        /// <summary>
        /// 插入ReportCard
        /// </summary>
        /// <param name="ReportCard">ReportCard</param>
        public int InsertReportCard(ReportCard reportCard)
        {
            Hashtable _map = new Hashtable();
            _map.Add("EmployeeID", reportCard.EmployeeID);
            _map.Add("STID", reportCard.STID);
            _map.Add("ExamDate", reportCard.ExamDate);
            _map.Add("TestPaperUrl", reportCard.TestPaperUrl);

            _map.Add("ID", reportCard.ID);

            reportCard.ID = (int)ExecuteInsert("JNyulu.InsertReportCard", _map);

            return reportCard.ID;
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
            Hashtable _map = new Hashtable();
            _map.Add("pageIndex", pageIndex);
            _map.Add("pageSize", pageSize);
            _map.Add("sortFields", sortFields);
            _map.Add("conAttr", conAttr);
            _map.Add("totalNum", totalNum);

            IList<ReportCard> _listObj = ExecuteQueryForList<ReportCard>("JNyulu.PaginatedReportCard", _map);

            totalNum = (int)_map["totalNum"];

            return _listObj;
        }

        /// <summary>
        /// 更新ReportCard
        /// </summary>
        /// <param name="ReportCard">ReportCard</param>
        public int UpdateReportCard(ReportCard reportCard)
        {
            LogHelper.Info("TestPaperUrl" + reportCard.TestPaperUrl + "");

            int i = ExecuteUpdate("JNyulu.UpdateReportCard", reportCard);
            return i;
        }

        /// <summary>
        /// 删除ReportCard
        /// </summary>
        /// <param name="id"></param>
        public int DeleteReportCardByID(int id)
        {
            return ExecuteDelete("JNyulu.DeleteReportCardByID", id);
        }
    }
}
