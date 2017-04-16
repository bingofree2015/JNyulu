/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 23:34:24
    修 改 者 : 
    修改时间 : 
    描    述 : SchoolRoom
===================================================== */

using System;
using System.Collections.Generic;
using System.Text;

using JNyuluSoft.Model;

namespace JNyuluSoft.Persistence
{
    /// <summary>
    /// SchoolRoom: SchoolRoom的数据库操作接口.
    /// </summary>
    public interface ISchoolRoomDao
    {
        /// <summary>
        /// 获取SchoolRoom列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="schoolRoom">SchoolRoom</param>
        /// <returns>SchoolRoom集合</returns>
        IList<SchoolRoom> GetBaseSchoolRoom(SchoolRoom schoolRoom);

        /// <summary>
        /// 获取SchoolRoom列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>SchoolRoom</returns>
        SchoolRoom GetBaseSchoolRoomByID(int id);

        /// <summary>
        /// 获取SchoolRoom列表(包含父对象)
        /// </summary>
        /// <param name="schoolRoom">SchoolRoom</param>
        /// <returns>SchoolRoom集合</returns>
        IList<SchoolRoom> GetSchoolRoom(SchoolRoom schoolRoom);

        /// <summary>
        /// 获取SchoolRoom列表(包含父对象)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>SchoolRoom</returns>
        SchoolRoom GetSchoolRoomByID(int id);

        /// <summary>
        /// 插入SchoolRoom
        /// </summary>
        /// <param name="schoolRoom">SchoolRoom</param>
        int InsertSchoolRoom(SchoolRoom schoolRoom);

        /// <summary>
        /// 分页 SchoolRoom
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        IList<SchoolRoom> PaginatedSchoolRoom(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum);

        /// <summary>
        /// 更新SchoolRoom
        /// </summary>
        /// <param name="schoolRoom">SchoolRoom</param>
        int UpdateSchoolRoom(SchoolRoom schoolRoom);

        /// <summary>
        /// 删除SchoolRoom
        /// </summary>
        /// <param name="id">自增型ID</param>
        int DeleteSchoolRoomByID(int id);

    }
}
