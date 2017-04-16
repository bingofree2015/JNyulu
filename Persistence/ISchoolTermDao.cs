/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 23:34:24
    修 改 者 : 
    修改时间 : 
    描    述 : SchoolTerm
===================================================== */

using System;
using System.Collections.Generic;
using System.Text;

using JNyuluSoft.Model;

namespace JNyuluSoft.Persistence
{
    /// <summary>
    /// SchoolTerm: SchoolTerm的数据库操作接口.
    /// </summary>
    public interface ISchoolTermDao
    {
        /// <summary>
        /// 获取SchoolTerm列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="schoolTerm">SchoolTerm</param>
        /// <returns>SchoolTerm集合</returns>
        IList<SchoolTerm> GetBaseSchoolTerm(SchoolTerm schoolTerm);

        /// <summary>
        /// 获取SchoolTerm列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>SchoolTerm</returns>
        SchoolTerm GetBaseSchoolTermByID(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schoolTermName"></param>
        /// <returns></returns>
        SchoolTerm GetBaseSchoolTermByName(string schoolTermName);

        /// <summary>
        /// 获取SchoolTerm列表(包含父对象)
        /// </summary>
        /// <param name="schoolTerm">SchoolTerm</param>
        /// <returns>SchoolTerm集合</returns>
        IList<SchoolTerm> GetSchoolTerm(SchoolTerm schoolTerm);

        /// <summary>
        /// 获取SchoolTerm列表(包含父对象)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>SchoolTerm</returns>
        SchoolTerm GetSchoolTermByID(int id);

        /// <summary>
        /// 插入SchoolTerm
        /// </summary>
        /// <param name="schoolTerm">SchoolTerm</param>
        int InsertSchoolTerm(SchoolTerm schoolTerm);

        /// <summary>
        /// 分页 SchoolTerm
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        IList<SchoolTerm> PaginatedSchoolTerm(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum);

        /// <summary>
        /// 更新SchoolTerm
        /// </summary>
        /// <param name="schoolTerm">SchoolTerm</param>
        int UpdateSchoolTerm(SchoolTerm schoolTerm);

        /// <summary>
        /// 删除SchoolTerm
        /// </summary>
        /// <param name="id">自增型ID</param>
        int DeleteSchoolTermByID(int id);

    }
}
