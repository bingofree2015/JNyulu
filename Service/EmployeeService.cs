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

using IBatisNet.DataAccess;

using JNyuluSoft.Persistence;
using JNyuluSoft.Persistence.Impl;
using JNyuluSoft.Model;


namespace JNyuluSoft.Service
{
    public class EmployeeService
    {
        #region 私有字段

        private static EmployeeService _instance = new EmployeeService();

        private IEmployeeDao _employeeDao = null;
        private RegistrationService _registrationService = RegistrationService.GetInstance();
        private ReportCardService _reportCardService = ReportCardService.GetInstance();
        private AssessmentService _assessmentService = AssessmentService.GetInstance();
        private DeviceTokenService _deviceTokenService = DeviceTokenService.GetInstance();

        #endregion

        #region 构造函数

        private EmployeeService()
        {
            _employeeDao = new EmployeeSqlMapDao();
        }

        #endregion

        #region 公共方法

        public static EmployeeService GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// 获取Employee列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="employee">Employee</param>
        /// <returns>Employee集合</returns>
        public IList<Employee> GetBaseEmployee(Employee employee)
        {
            return _employeeDao.GetBaseEmployee(employee);
        }

        /// <summary>
        /// 获取Employee列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Employee</returns>
        public Employee GetBaseEmployeeByID(int id)
        {
            return _employeeDao.GetBaseEmployeeByID(id);
        }

        /// <summary>
        /// 获取Employee列表(包含父对象)
        /// </summary>
        /// <param name="employee">Employee</param>
        /// <returns>Employee集合</returns>
        public IList<Employee> GetEmployee(Employee employee)
        {
            return _employeeDao.GetEmployee(employee);
        }

        /// <summary>
        /// 获取Employee列表(包含父对象)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Employee</returns>
        public Employee GetEmployeeByID(int id)
        {
            return _employeeDao.GetEmployeeByID(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Employee GetEmployeeByName(string name)
        {
            return _employeeDao.GetEmployeeByName(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public IList<Employee> GetEmployeeByNames(string[] names)
        {

            return _employeeDao.GetEmployeeByNames(names);
        }

        /// <summary>
        /// 插入Employee
        /// </summary>
        /// <param name="employee">Employee</param>
        public int InsertEmployee(Employee employee)
        {
            int _id = -1;

            try
            {
                //_daoManager.BeginTransaction();
                _id = _employeeDao.InsertEmployee(employee);
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
        /// 分页 Employee
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        public IList<Employee> PaginatedEmployee(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum)
        {
            return _employeeDao.PaginatedEmployee(pageIndex, pageSize, sortFields, conAttr, ref totalNum);
        }
        /// <summary>
        /// 更新Employee
        /// </summary>
        /// <param name="employee">Employee</param>
        public int UpdateEmployee(Employee employee)
        {
            return _employeeDao.UpdateEmployee(employee);
        }

        /// <summary>
        /// 删除Employee
        /// </summary>
        /// <param name="id">自增型ID</param>
        public int DeleteEmployeeByID(int id)
        {
            Registration _registration = new Registration();
            _registration.EmployeeID = id;
            IList<Registration> _registrationList = _registrationService.GetBaseRegistration(_registration);
            foreach (Registration Registration in _registrationList)
            {
              _registrationService.DeleteRegistrationByID(Registration.ID);
            }

            ReportCard _reportCard = new ReportCard();
            _reportCard.EmployeeID = id;
            IList<ReportCard> _reportCardList = _reportCardService.GetBaseReportCard(_reportCard);
            foreach (ReportCard reportCard in _reportCardList)
            {
              _reportCardService.DeleteReportCardByID(reportCard.ID);
            }

            Assessment _assessment = new Assessment();
            _assessment.EmployeeID = id;
            IList<Assessment> _assessmentList = _assessmentService.GetBaseAssessment(_assessment);
            foreach (Assessment studentAssessment in _assessmentList)
            {
              _assessmentService.DeleteAssessmentByID(studentAssessment.ID);
            }

            int[] _userID = new int[] { id };
            IList<DeviceToken> _deviceTokenList = _deviceTokenService.GetDeviceToken(string.Empty, string.Empty, _userID);
            foreach (DeviceToken deviceTokenRegisterInfo in _deviceTokenList)
            {
              _deviceTokenService.DeleteDeviceTokenByID(deviceTokenRegisterInfo.ID);
            }

            return _employeeDao.DeleteEmployeeByID(id);
        }

        public int VerifyEmployee(string name, string passWord)
        {
            return _employeeDao.VerifyEmployee(name, passWord);
        }

        #endregion
    }
}
