/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 21:53:32
    修 改 者 : 
    修改时间 : 
    描    述 : Employee
===================================================== */

using System;
using System.Collections.Generic;
using System.Text;

using JNyuluSoft.Model;

namespace JNyuluSoft.Persistence
{
    /// <summary>
    /// Employee: Employee的数据库操作接口.
    /// </summary>
    public interface IEmployeeDao
    {
        /// <summary>
        /// 获取Employee列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="employee">Employee</param>
        /// <returns>Employee集合</returns>
        IList<Employee> GetBaseEmployee(Employee employee);

        /// <summary>
        /// 获取Employee列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Employee</returns>
        Employee GetBaseEmployeeByID(int id);

        /// <summary>
        /// 获取Employee列表(包含父对象)
        /// </summary>
        /// <param name="employee">Employee</param>
        /// <returns>Employee集合</returns>
        IList<Employee> GetEmployee(Employee employee);

        /// <summary>
        /// 获取Employee列表(包含父对象)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Employee</returns>
        Employee GetEmployeeByID(int id);
        /// <summary>
        /// 获取Employee对象
        /// </summary>
        /// <param name="name">学号</param>
        /// <returns></returns>
        Employee GetEmployeeByName(string name);

        IList<Employee> GetEmployeeByNames(string[] names);

        /// <summary>
        /// 插入Employee
        /// </summary>
        /// <param name="employee">Employee</param>
        int InsertEmployee(Employee employee);

        /// <summary>
        /// 分页 Employee
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        IList<Employee> PaginatedEmployee(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum);

        /// <summary>
        /// 更新Employee
        /// </summary>
        /// <param name="employee">Employee</param>
        int UpdateEmployee(Employee employee);

        /// <summary>
        /// 删除Employee
        /// </summary>
        /// <param name="id">自增型ID</param>
        int DeleteEmployeeByID(int id);

        int VerifyEmployee(string name, string passWord);

    }
}
