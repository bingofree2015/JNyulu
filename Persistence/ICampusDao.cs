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

using JNyuluSoft.Model;

namespace JNyuluSoft.Persistence
{
    /// <summary>
    /// Campus: Campus的数据库操作接口.
    /// </summary>
    public interface ICampusDao
    {
        /// <summary>
        /// 获取Campus列表
        /// </summary>
        /// <param name="campus">Campus</param>
        /// <returns>Campus集合</returns>
        IList<Campus> GetCampus(Campus campus);

        /// <summary>
        /// 获取Campus列表
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Campus</returns>
        Campus GetCampusByID(int id);

        /// <summary>
        /// 插入Campus
        /// </summary>
        /// <param name="campus">Campus</param>
        int InsertCampus(Campus campus);

        /// <summary>
        /// 分页 Campus
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        IList<Campus> PaginatedCampus(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum);

        /// <summary>
        /// 更新Campus
        /// </summary>
        /// <param name="campus">Campus</param>
        int UpdateCampus(Campus campus);

        /// <summary>
        /// 删除Campus
        /// </summary>
        /// <param name="id">自增型ID</param>
        int DeleteCampusByID(int id);

    }
}
