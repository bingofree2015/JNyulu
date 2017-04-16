/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 21:53:31
    修 改 者 : 
    修改时间 : 
    描    述 : DeviceToken
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
    public class DeviceTokenService
    {
        #region 私有字段

        private static DeviceTokenService _instance = new DeviceTokenService();

        private IDeviceTokenDao _deviceTokenDao = null;

        #endregion

        #region 构造函数

        private DeviceTokenService()
        {
            _deviceTokenDao = new DeviceTokenSqlMapDao();
        }

        #endregion

        #region 公共方法

        public static DeviceTokenService GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// 获取DeviceToken对象(包含父对象)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>DeviceToken</returns>
        public DeviceToken GetDeviceTokenByID(int id)
        {
            return _deviceTokenDao.GetDeviceTokenByID(id);
        }

        /// <summary>
        /// 获取DeviceToken对象(包含父对象)
        /// </summary>
        /// <param name="deviceTokenRegisterInfo">DeviceToken</param>
        /// <returns>DeviceToken</returns>
        public IList<DeviceToken> GetDeviceToken(string platform, string deviceToken, int[] userID)
        {
            return _deviceTokenDao.GetDeviceToken(platform, deviceToken, userID);
        }

        /// <summary>
        /// 插入DeviceToken
        /// </summary>
        /// <param name="deviceTokenRegisterInfo">DeviceToken</param>
        public int InsertDeviceToken(DeviceToken deviceTokenRegisterInfo)
        {
            int _id = -1;
            try
            {
                //_daoManager.BeginTransaction();
                _id = _deviceTokenDao.InsertDeviceToken(deviceTokenRegisterInfo);
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
        /// 分页 DeviceToken
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        public IList<DeviceToken> PaginatedDeviceToken(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum)
        {
            return _deviceTokenDao.PaginatedDeviceToken(pageIndex, pageSize, sortFields, conAttr, ref totalNum);
        }

        /// <summary>
        /// 更新DeviceToken
        /// </summary>
        /// <param name="deviceTokenRegisterInfo">DeviceToken</param>
        public int UpdateDeviceToken(DeviceToken deviceTokenRegisterInfo)
        {
            return _deviceTokenDao.UpdateDeviceToken(deviceTokenRegisterInfo);
        }

        /// <summary>
        /// 删除DeviceToken
        /// </summary>
        /// <param name="id">自增型ID</param>
        public int DeleteDeviceTokenByID(int id)
        {
            return _deviceTokenDao.DeleteDeviceTokenByID(id);
        }

        #endregion
    }
}
