/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 21:53:31
    修 改 者 : 
    修改时间 : 
    描    述 : Campus
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
    /// Campus数据库持久化处理类
    /// </summary>
    public class CampusSqlMapDao : BaseSqlMapDao, ICampusDao
    {
        /// <summary>
        /// 获取Campus列表
        /// </summary>
        /// <param name="campus">Campus</param>
        /// <returns>Campus集合</returns>
        public IList<Campus> GetCampus(Campus campus)
        {
            return ExecuteQueryForList<Campus>("JNyulu.GetCampus", campus);
        }

        /// <summary>
        /// 获取Campus列表
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Campus</returns>
        public Campus GetCampusByID(int id)
        {
            return ExecuteQueryForObject<Campus>("JNyulu.GetCampusByID", id);
        }

        /// <summary>
        /// 插入Campus
        /// </summary>
        /// <param name="Campus">Campus</param>
        public int InsertCampus(Campus campus)
        {
            Hashtable _map = new Hashtable();
            _map.Add("Name", campus.Name);
            _map.Add("ID", campus.ID);

            campus.ID = (int)ExecuteInsert("JNyulu.InsertCampus", _map);

            return campus.ID;
        }

        /// <summary>
        /// 分页 Campus
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        public IList<Campus> PaginatedCampus(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum)
        {
            Hashtable _map = new Hashtable();
            _map.Add("pageIndex", pageIndex);
            _map.Add("pageSize", pageSize);
            _map.Add("sortFields", sortFields);
            _map.Add("conAttr", conAttr);
            _map.Add("totalNum", totalNum);

            IList<Campus> _listObj = ExecuteQueryForList<Campus>("JNyulu.PaginatedCampus", _map);

            totalNum = (int)_map["totalNum"];

            return _listObj;
        }

        /// <summary>
        /// 更新Campus
        /// </summary>
        /// <param name="Campus">Campus</param>
        public int UpdateCampus(Campus campus)
        {
            int i = ExecuteUpdate("JNyulu.UpdateCampus", campus);
            return i;
        }

        /// <summary>
        /// 删除Campus
        /// </summary>
        /// <param name="id">自增型ID</param>
        public int DeleteCampusByID(int id)
        {
            return ExecuteDelete("JNyulu.DeleteCampusByID", id);
        }
    }
}
