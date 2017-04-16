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

using JNyuluSoft.Model;

namespace JNyuluSoft.Persistence
{
    /// <summary>
    /// DeviceToken: DeviceToken的数据库操作接口.
    /// </summary>
    public interface IDeviceTokenDao
    {
        /// <summary>
        /// 获取DeviceToken对象(包含父对象)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>DeviceToken</returns>
        DeviceToken GetDeviceTokenByID(int id);

        /// <summary>
        /// 获取DeviceToken对象(包含父对象)
        /// </summary>
        /// <param name="deviceTokenRegisterInfo">DeviceToken</param>
        /// <returns>DeviceToken</returns>
        IList<DeviceToken> GetDeviceToken(string platform, string deviceToken, int[] userID);

        /// <summary>
        /// 插入DeviceToken
        /// </summary>
        /// <param name="deviceTokenRegisterInfo">DeviceToken</param>
        int InsertDeviceToken(DeviceToken deviceTokenRegisterInfo);

        /// <summary>
        /// 分页 DeviceToken
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        IList<DeviceToken> PaginatedDeviceToken(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum);

        /// <summary>
        /// 更新DeviceToken
        /// </summary>
        /// <param name="deviceTokenRegisterInfo">DeviceToken</param>
        int UpdateDeviceToken(DeviceToken deviceTokenRegisterInfo);

        /// <summary>
        /// 删除DeviceToken
        /// </summary>
        /// <param name="id">自增型ID</param>
        int DeleteDeviceTokenByID(int id);

    }
}
