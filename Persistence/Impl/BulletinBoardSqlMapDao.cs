/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 21:53:31
    修 改 者 : 
    修改时间 : 
    描    述 : BulletinBoard
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
    /// BulletinBoard数据库持久化处理类
    /// </summary>
    public class BulletinBoardSqlMapDao : BaseSqlMapDao, IBulletinBoardDao
    {
        /// <summary>
        /// 获取BulletinBoard列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="bulletinBoard">BulletinBoard</param>
        /// <returns>BulletinBoard集合</returns>
        public IList<BulletinBoard> GetBaseBulletinBoard(BulletinBoard bulletinBoard)
        {
            return ExecuteQueryForList<BulletinBoard>("JNyulu.GetBaseBulletinBoard", bulletinBoard);
        }

        /// <summary>
        /// 获取BulletinBoard列表,不包含父对象,仅返回对象本身。
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BulletinBoard</returns>
        public BulletinBoard GetBaseBulletinBoardByID(int id)
        {
            return ExecuteQueryForObject<BulletinBoard>("JNyulu.GetBaseBulletinBoardByID", id);
        }

        /// <summary>
        /// 获取BulletinBoard列表(包含父对象)
        /// </summary>
        /// <param name="bulletinBoard">BulletinBoard</param>
        /// <returns>BulletinBoard集合</returns>
        public IList<BulletinBoard> GetBulletinBoard(BulletinBoard bulletinBoard)
        {
            return ExecuteQueryForList<BulletinBoard>("JNyulu.GetBulletinBoard", bulletinBoard);
        }

        /// <summary>
        /// 获取BulletinBoard列表(包含父对象)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BulletinBoard</returns>
        public BulletinBoard GetBulletinBoardByID(int id)
        {
            return ExecuteQueryForObject<BulletinBoard>("JNyulu.GetBulletinBoardByID", id);
        }

        /// <summary>
        /// 插入BulletinBoard
        /// </summary>
        /// <param name="BulletinBoard">BulletinBoard</param>
        public int InsertBulletinBoard(BulletinBoard bulletinBoard)
        {
            Hashtable _map = new Hashtable();
            _map.Add("Sender", bulletinBoard.Sender);
            _map.Add("Receiver", bulletinBoard.Receiver);
            _map.Add("Subject", bulletinBoard.Subject);
            _map.Add("MsgText", bulletinBoard.MsgText);

            bulletinBoard.ID = (int)ExecuteInsert("JNyulu.InsertBulletinBoard", _map);

            return bulletinBoard.ID;
        }

        /// <summary>
        /// 分页 BulletinBoard
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        public IList<BulletinBoard> PaginatedBulletinBoard(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum)
        {
            Hashtable _map = new Hashtable();
            _map.Add("pageIndex", pageIndex);
            _map.Add("pageSize", pageSize);
            _map.Add("sortFields", sortFields);
            _map.Add("conAttr", conAttr);
            _map.Add("totalNum", totalNum);

            IList<BulletinBoard> _listObj = ExecuteQueryForList<BulletinBoard>("JNyulu.PaginatedBulletinBoard", _map);

            totalNum = (int)_map["totalNum"];

            return _listObj;
        }

        /// <summary>
        /// 更新BulletinBoard
        /// </summary>
        /// <param name="BulletinBoard">BulletinBoard</param>
        public int UpdateBulletinBoard(BulletinBoard bulletinBoard)
        {
            int i = ExecuteUpdate("JNyulu.UpdateBulletinBoard", bulletinBoard);
            return i;
        }

        /// <summary>
        /// 删除BulletinBoard
        /// </summary>
        /// <param name="id"></param>
        public int DeleteBulletinBoardByID(int id)
        {
            return ExecuteDelete("JNyulu.DeleteBulletinBoardByID", id);
        }
    }
}
