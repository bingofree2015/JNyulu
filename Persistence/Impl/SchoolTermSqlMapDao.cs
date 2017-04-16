/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 23:34:25
    修 改 者 : 
    修改时间 : 
    描    述 : SchoolTerm
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
    /// SchoolTerm数据库持久化处理类
    /// </summary>
    public class SchoolTermSqlMapDao : BaseSqlMapDao, ISchoolTermDao
    {
        /// <summary>
        /// 获取SchoolTerm列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="schoolTerm">SchoolTerm</param>
        /// <returns>SchoolTerm集合</returns>
        public IList<SchoolTerm> GetBaseSchoolTerm(SchoolTerm schoolTerm)
        {
            return ExecuteQueryForList<SchoolTerm>("JNyulu.GetBaseSchoolTerm", schoolTerm);
        }

        /// <summary>
        /// 获取SchoolTerm列表,不包含父对象,仅返回对象本身。
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>SchoolTerm</returns>
        public SchoolTerm GetBaseSchoolTermByID(int id)
        {
            return ExecuteQueryForObject<SchoolTerm>("JNyulu.GetBaseSchoolTermByID", id);
        }

        public SchoolTerm GetBaseSchoolTermByName(string schoolTermName)
        {
            return ExecuteQueryForObject<SchoolTerm>("JNyulu.GetBaseSchoolTermByName", schoolTermName);
        }

        /// <summary>
        /// 获取SchoolTerm列表(包含父对象)
        /// </summary>
        /// <param name="schoolTerm">SchoolTerm</param>
        /// <returns>SchoolTerm集合</returns>
        public IList<SchoolTerm> GetSchoolTerm(SchoolTerm schoolTerm)
        {
            return ExecuteQueryForList<SchoolTerm>("JNyulu.GetSchoolTerm", schoolTerm);
        }

        /// <summary>
        /// 获取SchoolTerm列表(包含父对象)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>SchoolTerm</returns>
        public SchoolTerm GetSchoolTermByID(int id)
        {
            return ExecuteQueryForObject<SchoolTerm>("JNyulu.GetSchoolTermByID", id);
        }

        /// <summary>
        /// 插入SchoolTerm
        /// </summary>
        /// <param name="SchoolTerm">SchoolTerm</param>
        public int InsertSchoolTerm(SchoolTerm schoolTerm)
        {
            Hashtable _map = new Hashtable();
            _map.Add("GradeID", schoolTerm.GradeID);
            _map.Add("Name", schoolTerm.Name);
            _map.Add("id", schoolTerm.ID);

            schoolTerm.ID = (int)ExecuteInsert("JNyulu.InsertSchoolTerm", _map);

            return schoolTerm.ID;
        }

        /// <summary>
        /// 分页 SchoolTerm
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        public IList<SchoolTerm> PaginatedSchoolTerm(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum)
        {
            Hashtable _map = new Hashtable();
            _map.Add("pageIndex", pageIndex);
            _map.Add("pageSize", pageSize);
            _map.Add("sortFields", sortFields);
            _map.Add("conAttr", conAttr);
            _map.Add("totalNum", totalNum);

            IList<SchoolTerm> _listObj = ExecuteQueryForList<SchoolTerm>("JNyulu.PaginatedSchoolTerm", _map);

            totalNum = (int)_map["totalNum"];

            return _listObj;
        }

        /// <summary>
        /// 更新SchoolTerm
        /// </summary>
        /// <param name="SchoolTerm">SchoolTerm</param>
        public int UpdateSchoolTerm(SchoolTerm schoolTerm)
        {
            int i = ExecuteUpdate("JNyulu.UpdateSchoolTerm", schoolTerm);
            return i;
        }

        /// <summary>
        /// 删除SchoolTerm
        /// </summary>
        /// <param name="id">自增型ID</param>
        public int DeleteSchoolTermByID(int id)
        {
            return ExecuteDelete("JNyulu.DeleteSchoolTermByID", id);
        }
    }
}
