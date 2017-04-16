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

using IBatisNet.DataAccess;

using JNyuluSoft.Persistence;
using JNyuluSoft.Persistence.Impl;
using JNyuluSoft.Model;


namespace JNyuluSoft.Service
{
    public class SchoolRoomService
    {
        #region 私有字段

        private static SchoolRoomService _instance = new SchoolRoomService();

        private ISchoolRoomDao _schoolRoomDao = null;

        #endregion

        #region 构造函数

        private SchoolRoomService() 
        {
            _schoolRoomDao = new SchoolRoomSqlMapDao();
        }

        #endregion

        #region 公共方法

        public static SchoolRoomService GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// 获取SchoolRoom列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="schoolRoom">SchoolRoom</param>
        /// <returns>SchoolRoom集合</returns>
        public IList<SchoolRoom> GetBaseSchoolRoom(SchoolRoom schoolRoom)
        {
            return _schoolRoomDao.GetBaseSchoolRoom(schoolRoom);
        }

        /// <summary>
        /// 获取SchoolRoom列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>SchoolRoom</returns>
        public SchoolRoom GetBaseSchoolRoomByID(int id)
        {
            return _schoolRoomDao.GetBaseSchoolRoomByID(id);
        }

        /// <summary>
        /// 获取SchoolRoom列表(包含父对象)
        /// </summary>
        /// <param name="schoolRoom">SchoolRoom</param>
        /// <returns>SchoolRoom集合</returns>
        public IList<SchoolRoom> GetSchoolRoom(SchoolRoom schoolRoom)
        {
            return _schoolRoomDao.GetSchoolRoom(schoolRoom);
        }

        /// <summary>
        /// 获取SchoolRoom列表(包含父对象)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>SchoolRoom</returns>
        public SchoolRoom GetSchoolRoomByID(int id)
        {
            return _schoolRoomDao.GetSchoolRoomByID(id);
        }

        /// <summary>
        /// 插入SchoolRoom
        /// </summary>
        /// <param name="schoolRoom">SchoolRoom</param>
        public int InsertSchoolRoom(SchoolRoom schoolRoom)
        {
            int _id = -1;

            try
            {
                //_daoManager.BeginTransaction();
                _id = _schoolRoomDao.InsertSchoolRoom(schoolRoom);
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
        /// 分页 SchoolRoom
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        public IList<SchoolRoom> PaginatedSchoolRoom(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum)
        {
            return _schoolRoomDao.PaginatedSchoolRoom(pageIndex, pageSize, sortFields, conAttr, ref totalNum);
        }
        /// <summary>
        /// 更新SchoolRoom
        /// </summary>
        /// <param name="schoolRoom">SchoolRoom</param>
        public int UpdateSchoolRoom(SchoolRoom schoolRoom)
        {
            return _schoolRoomDao.UpdateSchoolRoom(schoolRoom);
        }

        /// <summary>
        /// 删除SchoolRoom
        /// </summary>
        /// <param name="id">自增型ID</param>
        public int DeleteSchoolRoomByID(int id)
        {
            return _schoolRoomDao.DeleteSchoolRoomByID(id);
        }

        #endregion
    }
}
