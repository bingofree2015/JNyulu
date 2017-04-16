/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 23:34:23
    修 改 者 : 
    修改时间 : 
    描    述 : ScheNotice
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
    /// ScheNotice数据库持久化处理类
    /// </summary>
    public class ScheNoticeSqlMapDao : BaseSqlMapDao, IScheNoticeDao
    {
        /// <summary>
        /// 获取ScheNotice列表(包含父对象)
        /// </summary>
        /// <param name="scheNotice">ScheNotice</param>
        /// <returns>ScheNotice集合</returns>
        public IList<ScheNotice> GetScheNotice(IDictionary dict)
        {
            return ExecuteQueryForList<ScheNotice>("JNyulu.GetScheNotice", dict);
        }

        /// <summary>
        /// 获取ScheNotice列表(包含父对象)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ScheNotice</returns>
        public ScheNotice GetScheNoticeByID(int id)
        {
            return ExecuteQueryForObject<ScheNotice>("JNyulu.GetScheNoticeByID", id);
        }

        /// <summary>
        /// 插入ScheNotice
        /// </summary>
        /// <param name="ScheNotice">ScheNotice</param>
        public int InsertScheNotice(ScheNotice scheNotice)
        {
            Hashtable _map = new Hashtable();
            _map.Add("UserName", scheNotice.UserName);
            _map.Add("TrueName", scheNotice.TrueName);
            _map.Add("Context", scheNotice.Context);

            _map.Add("ID", scheNotice.ID);

            scheNotice.ID = (int)ExecuteInsert("JNyulu.InsertScheNotice", _map);

            return scheNotice.ID;
        }

        /// <summary>
        /// 分页 ScheNotice
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        public IList<ScheNotice> PaginatedScheNotice(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum)
        {
            Hashtable _map = new Hashtable();
            _map.Add("pageIndex", pageIndex);
            _map.Add("pageSize", pageSize);
            _map.Add("sortFields", sortFields);
            _map.Add("conAttr", conAttr);
            _map.Add("totalNum", totalNum);

            IList<ScheNotice> _listObj = ExecuteQueryForList<ScheNotice>("JNyulu.PaginatedScheNotice", _map);

            totalNum = (int)_map["totalNum"];

            return _listObj;
        }

        /// <summary>
        /// 更新ScheNotice
        /// </summary>
        /// <param name="ScheNotice">ScheNotice</param>
        public int UpdateScheNotice(ScheNotice scheNotice)
        {
            int i = ExecuteUpdate("JNyulu.UpdateScheNotice", scheNotice);
            return i;
        }

        /// <summary>
        /// 删除ScheNotice
        /// </summary>
        /// <param name="id"></param>
        public int DeleteScheNoticeByID(int id)
        {
            return ExecuteDelete("JNyulu.DeleteScheNoticeByID", id);
        }
    }
}
