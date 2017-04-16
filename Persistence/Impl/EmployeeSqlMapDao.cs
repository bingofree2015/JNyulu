/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 21:53:32
    修 改 者 : 
    修改时间 : 
    描    述 : Employee
===================================================== */

using System;
using System.Collections;
using System.Data;  
using System.Collections.Generic;
using System.Text;

using JNyuluSoft.Model;
using JNyuluSoft.Persistence;


namespace JNyuluSoft.Persistence.Impl
{
    /// <summary>
    /// Employee数据库持久化处理类
    /// </summary>
    public class EmployeeSqlMapDao : BaseSqlMapDao, IEmployeeDao
    {
        /// <summary>
        /// 获取Employee列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="employee">Employee</param>
        /// <returns>Employee集合</returns>
        public IList<Employee> GetBaseEmployee(Employee employee)
        {
            return ExecuteQueryForList<Employee>("JNyulu.GetBaseEmployee", employee);
        }

        /// <summary>
        /// 获取Employee列表,不包含父对象,仅返回对象本身。
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Employee集合</returns>
        public Employee GetBaseEmployeeByID(int id)
        {
            return ExecuteQueryForObject<Employee>("JNyulu.GetBaseEmployeeByID", id);
        }

        /// <summary>
        /// 获取Employee列表(包含父对象)
        /// </summary>
        /// <param name="employee">Employee</param>
        /// <returns>Employee集合</returns>
        public IList<Employee> GetEmployee(Employee employee)
        {
            return ExecuteQueryForList<Employee>("JNyulu.GetEmployee", employee);
        }

        /// <summary>
        /// 获取Employee列表(包含父对象)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Employee</returns>
        public Employee GetEmployeeByID(int id)
        {
            return ExecuteQueryForObject<Employee>("JNyulu.GetEmployeeByID", id);
        }

        /// <summary>
        /// 获取Employee对象
        /// </summary>
        /// <param name="name">学号</param>
        /// <returns>Employee</returns>
        public Employee GetEmployeeByName(string name)
        {
            return ExecuteQueryForObject<Employee>("JNyulu.GetEmployeeByName", name);
        }

        /// <summary>
        /// 获取Employee列表
        /// </summary>
        /// <param name="names">学号</param>
        /// <returns>Employee</returns>
        public IList<Employee> GetEmployeeByNames(string[] names)
        {
          Hashtable _map = new Hashtable();
          _map.Add("Names", names);

          return ExecuteQueryForList<Employee>("JNyulu.GetEmployeeByNames", _map);
        }

        /// <summary>
        /// 插入Employee
        /// </summary>
        /// <param name="Employee">Employee</param>
        public int InsertEmployee(Employee employee)
        {
            Hashtable _map = new Hashtable();
            _map.Add("UserType", employee.UserType);
            _map.Add("Name", employee.Name);
            _map.Add("PassWord", employee.PassWord);
            _map.Add("TrueName", employee.TrueName);
            _map.Add("Mobile", employee.Mobile);
            _map.Add("Phone", employee.Phone);
            _map.Add("Permission", employee.Permission);
            _map.Add("Description", employee.Description);

            _map.Add("ID", employee.ID);

            employee.ID = (int)ExecuteInsert("JNyulu.InsertEmployee", _map);

            return employee.ID;
        }

        /// <summary>
        /// 分页 Employee
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        public IList<Employee> PaginatedEmployee(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum)
        {
            Hashtable _map = new Hashtable();
            _map.Add("pageIndex", pageIndex);
            _map.Add("pageSize", pageSize);
            _map.Add("sortFields", sortFields);
            _map.Add("conAttr", conAttr);
            _map.Add("totalNum", totalNum);

            IList<Employee> _listObj = ExecuteQueryForList<Employee>("JNyulu.PaginatedEmployee", _map);

            totalNum = (int)_map["totalNum"];

            return _listObj;
        }

        /// <summary>
        /// 更新Employee
        /// </summary>
        /// <param name="Employee">Employee</param>
        public int UpdateEmployee(Employee employee)
        {
            int i = ExecuteUpdate("JNyulu.UpdateEmployee", employee);
            return i;
        }

        /// <summary>
        /// 删除Employee
        /// </summary>
        /// <param name="id">自增型ID</param>
        public int DeleteEmployeeByID(int id)
        {
            return ExecuteDelete("JNyulu.DeleteEmployeeByID", id);
        }

        public int VerifyEmployee(string name, string passWord)
        {
            PropertyCollection _fields = new PropertyCollection();
            _fields.Add("name", name);
            _fields.Add("passWord", passWord);

            ExecuteDelete("JNyulu.VerifyEmployee", _fields);

            int _id = (int)_fields["id"];

            return _id;
        }
    }
}
