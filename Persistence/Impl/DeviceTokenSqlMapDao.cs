/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 21:53:31
    修 改 者 : 
    修改时间 : 
    描    述 : DeviceToken
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
    /// DeviceToken数据库持久化处理类
    /// </summary>
    public class DeviceTokenSqlMapDao : BaseSqlMapDao, IDeviceTokenDao
    {
        /// <summary>
        /// 获取DeviceToken列表(包含父对象)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>DeviceToken</returns>
        public DeviceToken GetDeviceTokenByID(int id)
        {
            return ExecuteQueryForObject<DeviceToken>("JNyulu.GetDeviceTokenByID", id);
        }

        /// <summary>
        /// 获取DeviceToken对象(包含父对象)
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="deviceToken"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public IList<DeviceToken> GetDeviceToken(string platform, string token, int[] userID)
        {
            Hashtable _map = new Hashtable();
            _map.Add("Platform", platform);
            _map.Add("Token", token);
            if (userID != null && userID.Length > 0)
            {
                _map.Add("UserID", userID);
            }

            return ExecuteQueryForList<DeviceToken>("JNyulu.GetDeviceToken", _map);
        }

        /// <summary>
        /// 插入DeviceToken
        /// </summary>
        /// <param name="DeviceToken">DeviceToken</param>
        public int InsertDeviceToken(DeviceToken deviceTokenRegisterInfo)
        {
            Hashtable _map = new Hashtable();
            _map.Add("ClientID", deviceTokenRegisterInfo.ClientID);
            _map.Add("UserID", deviceTokenRegisterInfo.UserID);
            _map.Add("Token", deviceTokenRegisterInfo.Token);
            _map.Add("Platform", deviceTokenRegisterInfo.Platform);
            _map.Add("Ver", deviceTokenRegisterInfo.Ver);
            _map.Add("UpdateTime", DateTime.Now);

            _map.Add("ID", deviceTokenRegisterInfo.ID);

            deviceTokenRegisterInfo.ID = (int)ExecuteInsert("JNyulu.InsertDeviceToken", _map);

            return deviceTokenRegisterInfo.ID;
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
            Hashtable _map = new Hashtable();
            _map.Add("pageIndex", pageIndex);
            _map.Add("pageSize", pageSize);
            _map.Add("sortFields", sortFields);
            _map.Add("conAttr", conAttr);
            _map.Add("totalNum", totalNum);

            IList<DeviceToken> _listObj = ExecuteQueryForList<DeviceToken>("JNyulu.PaginatedDeviceToken", _map);

            totalNum = (int)_map["totalNum"];

            return _listObj;
        }

        /// <summary>
        /// 更新DeviceToken
        /// </summary>
        /// <param name="DeviceToken">DeviceToken</param>
        public int UpdateDeviceToken(DeviceToken deviceTokenRegisterInfo)
        {
            int i = ExecuteUpdate("JNyulu.UpdateDeviceToken", deviceTokenRegisterInfo);
            return i;
        }

        /// <summary>
        /// 删除DeviceToken
        /// </summary>
        /// <param name="id">自增型ID</param>
        public int DeleteDeviceTokenByID(int id)
        {
            return ExecuteDelete("JNyulu.DeleteDeviceTokenByID", id);
        }
    }
}
