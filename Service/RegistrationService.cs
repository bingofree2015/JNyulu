/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 21:53:32
    修 改 者 : 
    修改时间 : 
    描    述 : Registration
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
    public class RegistrationService
    {
        #region 私有字段

        private static RegistrationService _instance = new RegistrationService();

        private IRegistrationDao _registrationDao = null;

        #endregion

        #region 构造函数

        private RegistrationService() 
        {
            _registrationDao = new RegistrationSqlMapDao();
        }

        #endregion

        #region 公共方法

        public static RegistrationService GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// 获取Registration列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="Registration">Registration</param>
        /// <returns>Registration集合</returns>
        public IList<Registration> GetBaseRegistration(Registration Registration)
        {
            return _registrationDao.GetBaseRegistration(Registration);
        }

        /// <summary>
        /// 获取Registration列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Registration</returns>
        public Registration GetBaseRegistrationByID(int id)
        {
            return _registrationDao.GetBaseRegistrationByID(id);
        }

        public  Registration GetRegistrationByName(string name)
        {
            return _registrationDao.GetRegistrationByName(name);
        }
        /// <summary>
        /// 获取Registration列表(包含父对象)
        /// </summary>
        /// <param name="Registration">Registration</param>
        /// <returns>Registration集合</returns>
        public IList<Registration> GetRegistration(Registration Registration)
        {
            return _registrationDao.GetRegistration(Registration);
        }

        /// <summary>
        /// 获取Registration列表(包含父对象)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Registration</returns>
        public Registration GetRegistrationByID(int id)
        {
            return _registrationDao.GetRegistrationByID(id);
        }

        /// <summary>
        /// 插入Registration
        /// </summary>
        /// <param name="Registration">Registration</param>
        public int InsertRegistration(Registration Registration)
        {
            int _id = -1;

            try
            {
                //_daoManager.BeginTransaction();
                _id = _registrationDao.InsertRegistration(Registration);
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
        /// 分页 Registration
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        public IList<Registration> PaginatedRegistration(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum)
        {
            return _registrationDao.PaginatedRegistration(pageIndex, pageSize, sortFields, conAttr, ref totalNum);
        }
        /// <summary>
        /// 更新Registration
        /// </summary>
        /// <param name="Registration">Registration</param>
        public int UpdateRegistration(Registration Registration)
        {
            return _registrationDao.UpdateRegistration(Registration);
        }

        /// <summary>
        /// 删除Registration
        /// </summary>
        /// <param name="id">自增型ID</param>
        public int DeleteRegistrationByID(int id)
        {
            return _registrationDao.DeleteRegistrationByID(id);
        }

        #endregion
    }
}
