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

using IBatisNet.DataAccess;

using JNyuluSoft.Persistence;
using JNyuluSoft.Persistence.Impl;
using JNyuluSoft.Model;


namespace JNyuluSoft.Service
{
    public class BulletinBoardService
    {
        #region 私有字段

        private static BulletinBoardService _instance = new BulletinBoardService();

        private IBulletinBoardDao _bulletinBoardDao = null;

        #endregion

        #region 构造函数

        private BulletinBoardService() 
        {
            _bulletinBoardDao = new BulletinBoardSqlMapDao();
        }

        #endregion

        #region 公共方法

        public static BulletinBoardService GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// 获取BulletinBoard列表(包含父对象)
        /// </summary>
        /// <param name="bulletinBoard">BulletinBoard</param>
        /// <returns>BulletinBoard集合</returns>
        public IList<BulletinBoard> GetBulletinBoard(BulletinBoard bulletinBoard)
        {
            return _bulletinBoardDao.GetBulletinBoard(bulletinBoard);
        }

        /// <summary>
        /// 获取BulletinBoard列表(包含父对象)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BulletinBoard</returns>
        public BulletinBoard GetBulletinBoardByID(int id)
        {
            return _bulletinBoardDao.GetBulletinBoardByID(id);
        }

        /// <summary>
        /// 插入BulletinBoard
        /// </summary>
        /// <param name="bulletinBoard">BulletinBoard</param>
        public int InsertBulletinBoard(BulletinBoard bulletinBoard)
        {
            int _id = -1;

            try
            {
                //_daoManager.BeginTransaction();
                _id = _bulletinBoardDao.InsertBulletinBoard(bulletinBoard);
                //_daoManager.CommitTransaction();
            }
            catch (Exception ex)
            {
                //_daoManager.RollBackTransaction();
                throw ex;
            }
            return _id;
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
            return _bulletinBoardDao.PaginatedBulletinBoard(pageIndex, pageSize, sortFields, conAttr, ref totalNum);
        }
        /// <summary>
        /// 更新BulletinBoard
        /// </summary>
        /// <param name="bulletinBoard">BulletinBoard</param>
        public int UpdateBulletinBoard(BulletinBoard bulletinBoard)
        {
            return _bulletinBoardDao.UpdateBulletinBoard(bulletinBoard);
        }

        /// <summary>
        /// 删除BulletinBoard
        /// </summary>
        /// <param name="id"></param>
        public int DeleteBulletinBoardByID(int id)
        {
            return _bulletinBoardDao.DeleteBulletinBoardByID(id);
        }

        #endregion
    }
}
