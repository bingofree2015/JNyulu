/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 23:34:26
    修 改 者 : 
    修改时间 : 
    描    述 : Assessment
===================================================== */

using System;
using System.Collections.Generic;
using System.Text;

using JNyuluSoft.Model;

namespace JNyuluSoft.Persistence
{
    /// <summary>
    /// Assessment: Assessment的数据库操作接口.
    /// </summary>
    public interface IAssessmentDao
    {
        /// <summary>
        /// 获取Assessment列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="studentAssessment">Assessment</param>
        /// <returns>Assessment集合</returns>
        IList<Assessment> GetBaseAssessment(Assessment studentAssessment);

        /// <summary>
        /// 获取Assessment列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Assessment</returns>
        Assessment GetBaseAssessmentByID(int id);

        /// <summary>
        /// 获取Assessment列表(包含父对象)
        /// </summary>
        /// <param name="studentAssessment">Assessment</param>
        /// <returns>Assessment集合</returns>
        IList<Assessment> GetAssessment(Assessment studentAssessment);

        /// <summary>
        /// 获取Assessment列表(包含父对象)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Assessment</returns>
        Assessment GetAssessmentByID(int id);

        /// <summary>
        /// 插入Assessment
        /// </summary>
        /// <param name="studentAssessment">Assessment</param>
        int InsertAssessment(Assessment studentAssessment);

        /// <summary>
        /// 分页 Assessment
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        IList<Assessment> PaginatedAssessment(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum);

        /// <summary>
        /// 更新Assessment
        /// </summary>
        /// <param name="studentAssessment">Assessment</param>
        int UpdateAssessment(Assessment studentAssessment);

        /// <summary>
        /// 删除Assessment
        /// </summary>
        /// <param name="id">自增型ID</param>
        int DeleteAssessmentByID(int id);

    }
}
