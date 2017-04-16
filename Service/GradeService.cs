/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 21:53:31
    修 改 者 : 
    修改时间 : 
    描    述 : Grade
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
    public class GradeService
    {
        #region 私有字段

        private static GradeService _instance = new GradeService();

        private IGradeDao _gradeDao = null;
        private RegistrationService _registrationService = RegistrationService.GetInstance();

        #endregion

        #region 构造函数

        private GradeService() 
        {
            _gradeDao = new GradeSqlMapDao();
        }

        #endregion

        #region 公共方法

        public static GradeService GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// 获取Grade列表
        /// </summary>
        /// <param name="grade">Grade</param>
        /// <returns>Grade集合</returns>
        public IList<Grade> GetGrade(Grade grade)
        {
            return _gradeDao.GetGrade(grade);
        }

        /// <summary>
        /// 获取Grade列表
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Grade</returns>
        public Grade GetGradeByID(int id)
        {
            return _gradeDao.GetGradeByID(id);
        }
        /// <summary>
        /// 获取Grade对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Grade GetGradeByName(string name)
        {
            return _gradeDao.GetGradeByName(name);
        }

        /// <summary>
        /// 插入Grade
        /// </summary>
        /// <param name="grade">Grade</param>
        public int InsertGrade(Grade grade)
        {
            int _id = -1;
            try
            {
                //_daoManager.BeginTransaction();
                _id = _gradeDao.InsertGrade(grade);
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
        /// 分页 Grade
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        public IList<Grade> PaginatedGrade(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum)
        {
            return _gradeDao.PaginatedGrade(pageIndex, pageSize, sortFields, conAttr, ref totalNum); 
        }

        /// <summary>
        /// 更新Grade
        /// </summary>
        /// <param name="grade">Grade</param>
        public int UpdateGrade(Grade grade)
        {
            return _gradeDao.UpdateGrade(grade);
        }

        /// <summary>
        /// 删除Grade
        /// </summary>
        /// <param name="id">自增型ID</param>
        public int DeleteGradeByID(int id)
        {
            Registration _registration = new Registration();
            _registration.GradeID = id;
            IList<Registration> _registrationList = _registrationService.GetBaseRegistration(_registration);
            foreach (Registration Registration in _registrationList)
            {
              _registrationService.DeleteRegistrationByID(Registration.ID);
            }

            return _gradeDao.DeleteGradeByID(id);
        }

        #endregion
    }
}
