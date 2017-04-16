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

namespace JNyuluSoft.Persistence
{
    /// <summary>
    /// ReportCard: ReportCard的数据库操作接口.
    /// </summary>
    public interface IReportCardDao
    {
        /// <summary>
        /// 获取ReportCard列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="reportCard">ReportCard</param>
        /// <returns>ReportCard集合</returns>
        IList<ReportCard> GetBaseReportCard(ReportCard reportCard);

        /// <summary>
        /// 获取ReportCard列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ReportCard</returns>
        ReportCard GetBaseReportCardByID(int id);

        /// <summary>
        /// 获取ReportCard列表(包含父对象)
        /// </summary>
        /// <param name="reportCard">ReportCard</param>
        /// <returns>ReportCard集合</returns>
        IList<ReportCard> GetReportCard(IDictionary dict);

        /// <summary>
        /// 获取ReportCard列表(包含父对象)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ReportCard</returns>
        ReportCard GetReportCardByID(int id);

        /// <summary>
        /// 插入ReportCard
        /// </summary>
        /// <param name="reportCard">ReportCard</param>
        int InsertReportCard(ReportCard reportCard);

        /// <summary>
        /// 分页 ReportCard
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        IList<ReportCard> PaginatedReportCard(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum);

        /// <summary>
        /// 更新ReportCard
        /// </summary>
        /// <param name="reportCard">ReportCard</param>
        int UpdateReportCard(ReportCard reportCard);

        /// <summary>
        /// 删除ReportCard
        /// </summary>
        /// <param name="id"></param>
        int DeleteReportCardByID(int id);

    }
}
