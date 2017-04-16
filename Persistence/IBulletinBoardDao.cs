/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 21:53:31
    修 改 者 : 
    修改时间 : 
    描    述 : BulletinBoard
===================================================== */

using System;
using System.Collections.Generic;
using System.Text;

using JNyuluSoft.Model;

namespace JNyuluSoft.Persistence
{
    /// <summary>
    /// BulletinBoard: BulletinBoard的数据库操作接口.
    /// </summary>
    public interface IBulletinBoardDao
    {
        /// <summary>
        /// 获取BulletinBoard列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="bulletinBoard">BulletinBoard</param>
        /// <returns>BulletinBoard集合</returns>
        IList<BulletinBoard> GetBaseBulletinBoard(BulletinBoard bulletinBoard);

        /// <summary>
        /// 获取BulletinBoard列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BulletinBoard</returns>
        BulletinBoard GetBaseBulletinBoardByID(int id);

        /// <summary>
        /// 获取BulletinBoard列表(包含父对象)
        /// </summary>
        /// <param name="bulletinBoard">BulletinBoard</param>
        /// <returns>BulletinBoard集合</returns>
        IList<BulletinBoard> GetBulletinBoard(BulletinBoard bulletinBoard);

        /// <summary>
        /// 获取BulletinBoard列表(包含父对象)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BulletinBoard</returns>
        BulletinBoard GetBulletinBoardByID(int id);

        /// <summary>
        /// 插入BulletinBoard
        /// </summary>
        /// <param name="bulletinBoard">BulletinBoard</param>
        int InsertBulletinBoard(BulletinBoard bulletinBoard);

        /// <summary>
        /// 分页 BulletinBoard
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        IList<BulletinBoard> PaginatedBulletinBoard(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum);

        /// <summary>
        /// 更新BulletinBoard
        /// </summary>
        /// <param name="bulletinBoard">BulletinBoard</param>
        int UpdateBulletinBoard(BulletinBoard bulletinBoard);

        /// <summary>
        /// 删除BulletinBoard
        /// </summary>
        /// <param name="id"></param>
        int DeleteBulletinBoardByID(int id);

    }
}
