/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 21:53:32
    修 改 者 : 
    修改时间 : 
    描    述 : Registration
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
    /// Registration数据库持久化处理类
    /// </summary>
    public class RegistrationSqlMapDao : BaseSqlMapDao, IRegistrationDao
    {
        /// <summary>
        /// 获取Registration列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="Registration">Registration</param>
        /// <returns>Registration集合</returns>
        public IList<Registration> GetBaseRegistration(Registration Registration)
        {
            return ExecuteQueryForList<Registration>("JNyulu.GetBaseRegistration", Registration);
        }

        /// <summary>
        /// 获取Registration列表,不包含父对象,仅返回对象本身。
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Registration</returns>
        public Registration GetBaseRegistrationByID(int id)
        {
            return ExecuteQueryForObject<Registration>("JNyulu.GetBaseRegistrationByID", id);
        }

        /// <summary>
        /// 获取Registration列表(包含父对象)
        /// </summary>
        /// <param name="Registration">Registration</param>
        /// <returns>Registration集合</returns>
        public IList<Registration> GetRegistration(Registration registration)
        {
            return ExecuteQueryForList<Registration>("JNyulu.GetRegistration", registration);
        }

        /// <summary>
        /// 获取Registration列表(包含父对象)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Registration</returns>
        public Registration GetRegistrationByID(int id)
        {
            return ExecuteQueryForObject<Registration>("JNyulu.GetRegistrationByID", id);
        }

        public Registration GetRegistrationByName(string name)
        {
            return ExecuteQueryForObject<Registration>("JNyulu.GetRegistrationByName", name);
        }
        

        /// <summary>
        /// 插入Registration
        /// </summary>
        /// <param name="Registration">Registration</param>
        public int InsertRegistration(Registration Registration)
        {
            Hashtable _map = new Hashtable();
            _map.Add("EmployeeID", Registration.EmployeeID);
            _map.Add("SchoolName", Registration.SchoolName);
            _map.Add("GradeID", Registration.GradeID);
            _map.Add("GradeName", Registration.GradeName);
            _map.Add("STID", Registration.STID);
            _map.Add("SchoolFee", Registration.SchoolFee);
            _map.Add("Reasons", Registration.Reasons);
            _map.Add("Assessor", Registration.Assessor);
            _map.Add("Remark", Registration.Remark);
            _map.Add("RecordDate", Registration.RecordDate);
            _map.Add("ID", Registration.ID);

            Registration.ID = (int)ExecuteInsert("JNyulu.InsertRegistration", _map);

            return Registration.ID;
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
            Hashtable _map = new Hashtable();
            _map.Add("pageIndex", pageIndex);
            _map.Add("pageSize", pageSize);
            _map.Add("sortFields", sortFields);
            _map.Add("conAttr", conAttr);
            _map.Add("totalNum", totalNum);

            IList<Registration> _listObj = ExecuteQueryForList<Registration>("JNyulu.PaginatedRegistration", _map);

            totalNum = (int)_map["totalNum"];

            return _listObj;
        }

        /// <summary>
        /// 更新Registration
        /// </summary>
        /// <param name="Registration">Registration</param>
        public int UpdateRegistration(Registration Registration)
        {
            int i = ExecuteUpdate("JNyulu.UpdateRegistration", Registration);
            return i;
        }

        /// <summary>
        /// 删除Registration
        /// </summary>
        /// <param name="id">自增型ID</param>
        public int DeleteRegistrationByID(int id)
        {
            return ExecuteDelete("JNyulu.DeleteRegistrationByID", id);
        }
    }
}
