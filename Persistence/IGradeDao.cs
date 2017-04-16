/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 21:53:31
    修 改 者 : 
    修改时间 : 
    描    述 : Grade
===================================================== */

using System;
using System.Collections.Generic;
using System.Text;

using JNyuluSoft.Model;

namespace JNyuluSoft.Persistence
{
    /// <summary>
    /// Grade: Grade的数据库操作接口.
    /// </summary>
    public interface IGradeDao
    {
        /// <summary>
        /// 获取Grade列表
        /// </summary>
        /// <param name="grade">Grade</param>
        /// <returns>Grade集合</returns>
        IList<Grade> GetGrade(Grade grade);

        /// <summary>
        /// 获取Grade列表
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Grade</returns>
        Grade GetGradeByID(int id);
        /// <summary>
        /// 获取Grade对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Grade GetGradeByName(string name);
        /// <summary>
        /// 插入Grade
        /// </summary>
        /// <param name="grade">Grade</param>
        int InsertGrade(Grade grade);

        /// <summary>
        /// 分页 Grade
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        IList<Grade> PaginatedGrade(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum);

        /// <summary>
        /// 更新Grade
        /// </summary>
        /// <param name="grade">Grade</param>
        int UpdateGrade(Grade grade);

        /// <summary>
        /// 删除Grade
        /// </summary>
        /// <param name="id">自增型ID</param>
        int DeleteGradeByID(int id);

    }
}
