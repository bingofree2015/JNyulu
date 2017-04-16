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

using JNyuluSoft.Model;

namespace JNyuluSoft.Persistence
{
    /// <summary>
    /// Registration: Registration的数据库操作接口.
    /// </summary>
    public interface IRegistrationDao
    {
        /// <summary>
        /// 获取Registration列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="register">Registration</param>
        /// <returns>Registration集合</returns>
        IList<Registration> GetBaseRegistration(Registration register);

        /// <summary>
        /// 获取Registration列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Registration</returns>
        Registration GetBaseRegistrationByID(int id);

        Registration GetRegistrationByName(string name);
        /// <summary>
        /// 获取Registration列表(包含父对象)
        /// </summary>
        /// <param name="register">Registration</param>
        /// <returns>Registration集合</returns>
        IList<Registration> GetRegistration(Registration register);

        /// <summary>
        /// 获取Registration列表(包含父对象)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Registration</returns>
        Registration GetRegistrationByID(int id);

        /// <summary>
        /// 插入Registration
        /// </summary>
        /// <param name="register">Registration</param>
        int InsertRegistration(Registration register);

        /// <summary>
        /// 分页 Registration
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        IList<Registration> PaginatedRegistration(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum);

        /// <summary>
        /// 更新Registration
        /// </summary>
        /// <param name="register">Registration</param>
        int UpdateRegistration(Registration register);

        /// <summary>
        /// 删除Registration
        /// </summary>
        /// <param name="id">自增型ID</param>
        int DeleteRegistrationByID(int id);

    }
}
