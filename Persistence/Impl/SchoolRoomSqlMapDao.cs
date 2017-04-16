/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 23:34:24
    修 改 者 : 
    修改时间 : 
    描    述 : SchoolRoom
===================================================== */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using JNyuluSoft.Model;
using JNyuluSoft.Persistence;


namespace JNyuluSoft.Persistence.Impl
{
    /// <summary>
    /// SchoolRoom数据库持久化处理类
    /// </summary>
    public class SchoolRoomSqlMapDao : BaseSqlMapDao, ISchoolRoomDao
    {
        /// <summary>
        /// 获取SchoolRoom列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="schoolRoom">SchoolRoom</param>
        /// <returns>SchoolRoom集合</returns>
        public IList<SchoolRoom> GetBaseSchoolRoom(SchoolRoom schoolRoom)
        {
            return ExecuteQueryForList<SchoolRoom>("JNyulu.GetBaseSchoolRoom", schoolRoom);
        }

        /// <summary>
        /// 获取SchoolRoom列表,不包含父对象,仅返回对象本身。
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>SchoolRoom</returns>
        public SchoolRoom GetBaseSchoolRoomByID(int id)
        {
            return ExecuteQueryForObject<SchoolRoom>("JNyulu.GetBaseSchoolRoomByID", id);
        }

        /// <summary>
        /// 获取SchoolRoom列表(包含父对象)
        /// </summary>
        /// <param name="schoolRoom">SchoolRoom</param>
        /// <returns>SchoolRoom集合</returns>
        public IList<SchoolRoom> GetSchoolRoom(SchoolRoom schoolRoom)
        {
            return ExecuteQueryForList<SchoolRoom>("JNyulu.GetSchoolRoom", schoolRoom);
        }

        /// <summary>
        /// 获取SchoolRoom列表(包含父对象)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>SchoolRoom</returns>
        public SchoolRoom GetSchoolRoomByID(int id)
        {
            return ExecuteQueryForObject<SchoolRoom>("JNyulu.GetSchoolRoomByID", id);
        }

        /// <summary>
        /// 插入SchoolRoom
        /// </summary>
        /// <param name="SchoolRoom">SchoolRoom</param>
        public int InsertSchoolRoom(SchoolRoom schoolRoom)
        {
            Hashtable _map = new Hashtable();
            _map.Add("CampusID", schoolRoom.CampusID);
            _map.Add("Name", schoolRoom.Name);

            _map.Add("ID", schoolRoom.ID);

            schoolRoom.ID = (int)ExecuteInsert("JNyulu.InsertSchoolRoom", _map);

            return schoolRoom.ID;
        }

        /// <summary>
        /// 分页 SchoolRoom
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        public IList<SchoolRoom> PaginatedSchoolRoom(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum)
        {
            Hashtable _map = new Hashtable();
            _map.Add("pageIndex", pageIndex);
            _map.Add("pageSize", pageSize);
            _map.Add("sortFields", sortFields);
            _map.Add("conAttr", conAttr);
            _map.Add("totalNum", totalNum);

            IList<SchoolRoom> _listObj = ExecuteQueryForList<SchoolRoom>("JNyulu.PaginatedSchoolRoom", _map);

            totalNum = (int)_map["totalNum"];

            return _listObj;
        }

        /// <summary>
        /// 更新SchoolRoom
        /// </summary>
        /// <param name="SchoolRoom">SchoolRoom</param>
        public int UpdateSchoolRoom(SchoolRoom schoolRoom)
        {
            int i = ExecuteUpdate("JNyulu.UpdateSchoolRoom", schoolRoom);
            return i;
        }

        /// <summary>
        /// 删除SchoolRoom
        /// </summary>
        /// <param name="id">自增型ID</param>
        public int DeleteSchoolRoomByID(int id)
        {
            return ExecuteDelete("JNyulu.DeleteSchoolRoomByID", id);
        }
    }
}
