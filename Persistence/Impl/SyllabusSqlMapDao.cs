/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 23:34:23
    修 改 者 : 
    修改时间 : 
    描    述 : Syllabus
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
    /// Syllabus数据库持久化处理类
    /// </summary>
    public class SyllabusSqlMapDao : BaseSqlMapDao, ISyllabusDao
    {
        /// <summary>
        /// 获取Syllabus列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="syllabus">Syllabus</param>
        /// <returns>Syllabus集合</returns>
        public IList<Syllabus> GetBaseSyllabus(Syllabus syllabus)
        {
            return ExecuteQueryForList<Syllabus>("JNyulu.GetBaseSyllabus", syllabus);
        }

        /// <summary>
        /// 获取Syllabus列表,不包含父对象,仅返回对象本身。
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Syllabus</returns>
        public Syllabus GetBaseSyllabusByID(int id)
        {
            return ExecuteQueryForObject<Syllabus>("JNyulu.GetBaseSyllabusByID", id);
        }

        /// <summary>
        /// 获取Syllabus列表(包含父对象)
        /// </summary>
        /// <param name="syllabus">Syllabus</param>
        /// <returns>Syllabus集合</returns>
        public IList<Syllabus> GetSyllabus(IDictionary dict)
        {
            return ExecuteQueryForList<Syllabus>("JNyulu.GetSyllabus", dict);
        }

        /// <summary>
        /// 获取Syllabus列表(包含父对象)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Syllabus</returns>
        public Syllabus GetSyllabusByID(int id)
        {
            return ExecuteQueryForObject<Syllabus>("JNyulu.GetSyllabusByID", id);
        }

        /// <summary>
        /// 插入Syllabus
        /// </summary>
        /// <param name="Syllabus">Syllabus</param>
        public int InsertSyllabus(Syllabus syllabus)
        {
            Hashtable _map = new Hashtable();
            _map.Add("EmployeeID", syllabus.EmployeeID);
            _map.Add("STID", syllabus.STID);
            _map.Add("SyllabusUrl", syllabus.SyllabusUrl);

            _map.Add("ID", syllabus.ID);

            syllabus.ID = (int)ExecuteInsert("JNyulu.InsertSyllabus", _map);

            return syllabus.ID;
        }

        /// <summary>
        /// 分页 Syllabus
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        public IList<Syllabus> PaginatedSyllabus(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum)
        {
            Hashtable _map = new Hashtable();
            _map.Add("pageIndex", pageIndex);
            _map.Add("pageSize", pageSize);
            _map.Add("sortFields", sortFields);
            _map.Add("conAttr", conAttr);
            _map.Add("totalNum", totalNum);

            IList<Syllabus> _listObj = ExecuteQueryForList<Syllabus>("JNyulu.PaginatedSyllabus", _map);

            totalNum = (int)_map["totalNum"];

            return _listObj;
        }

        /// <summary>
        /// 更新Syllabus
        /// </summary>
        /// <param name="Syllabus">Syllabus</param>
        public int UpdateSyllabus(Syllabus syllabus)
        {
            LogHelper.Info("SyllabusUrl" + syllabus.SyllabusUrl + "");

            int i = ExecuteUpdate("JNyulu.UpdateSyllabus", syllabus);
            return i;
        }

        /// <summary>
        /// 删除Syllabus
        /// </summary>
        /// <param name="id"></param>
        public int DeleteSyllabusByID(int id)
        {
            return ExecuteDelete("JNyulu.DeleteSyllabusByID", id);
        }
    }
}
