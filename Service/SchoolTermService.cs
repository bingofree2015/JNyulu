/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 23:34:25
    修 改 者 : 
    修改时间 : 
    描    述 : SchoolTerm
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
    public class SchoolTermService
    {
        #region 私有字段(年级)

        private static SchoolTermService _instance = new SchoolTermService();

        private ISchoolTermDao _schoolTermDao = null;

        #endregion

        #region 构造函数

        private SchoolTermService() 
        {
            _schoolTermDao = new SchoolTermSqlMapDao();
        }

        #endregion

        #region 公共方法

        public static SchoolTermService GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// 获取SchoolTerm列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="schoolTerm">SchoolTerm</param>
        /// <returns>SchoolTerm集合</returns>
        public IList<SchoolTerm> GetBaseSchoolTerm(SchoolTerm schoolTerm)
        {
            return _schoolTermDao.GetBaseSchoolTerm(schoolTerm);
        }

        /// <summary>
        /// 获取SchoolTerm列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>SchoolTerm</returns>
        public SchoolTerm GetBaseSchoolTermByID(int id)
        {
            return _schoolTermDao.GetBaseSchoolTermByID(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schoolTermName"></param>
        /// <returns></returns>
        public SchoolTerm GetBaseSchoolTermByName(string schoolTermName)
        {
            return _schoolTermDao.GetBaseSchoolTermByName(schoolTermName);
        }

        /// <summary>
        /// 获取SchoolTerm列表(包含父对象)
        /// </summary>
        /// <param name="schoolTerm">SchoolTerm</param>
        /// <returns>SchoolTerm集合</returns>
        public IList<SchoolTerm> GetSchoolTerm(SchoolTerm schoolTerm)
        {
            return _schoolTermDao.GetSchoolTerm(schoolTerm);
        }

        /// <summary>
        /// 获取SchoolTerm列表(包含父对象)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>SchoolTerm</returns>
        public SchoolTerm GetSchoolTermByID(int id)
        {
            return _schoolTermDao.GetSchoolTermByID(id);
        }

        /// <summary>
        /// 插入SchoolTerm
        /// </summary>
        /// <param name="schoolTerm">SchoolTerm</param>
        public int InsertSchoolTerm(SchoolTerm schoolTerm)
        {
            int _id = -1;

            try
            {
                //_daoManager.BeginTransaction();
                _id = _schoolTermDao.InsertSchoolTerm(schoolTerm);
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
        /// 分页 SchoolTerm
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        public IList<SchoolTerm> PaginatedSchoolTerm(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum)
        {
            return _schoolTermDao.PaginatedSchoolTerm(pageIndex, pageSize, sortFields, conAttr, ref totalNum);
        }
        /// <summary>
        /// 更新SchoolTerm
        /// </summary>
        /// <param name="schoolTerm">SchoolTerm</param>
        public int UpdateSchoolTerm(SchoolTerm schoolTerm)
        {
            return _schoolTermDao.UpdateSchoolTerm(schoolTerm);
        }

        /// <summary>
        /// 删除SchoolTerm
        /// </summary>
        /// <param name="id">自增型ID</param>
        public int DeleteSchoolTermByID(int id)
        {
            return _schoolTermDao.DeleteSchoolTermByID(id);
        }

        #endregion
    }
}
