/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 23:34:26
    修 改 者 : 
    修改时间 : 
    描    述 : Assessment
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
    public class AssessmentService
    {
        #region 私有字段

        private static AssessmentService _instance = new AssessmentService();

        private IAssessmentDao _assessmentDao = null;

        #endregion

        #region 构造函数

        private AssessmentService() 
        {
            _assessmentDao = new AssessmentSqlMapDao();
        }

        #endregion

        #region 公共方法

        public static AssessmentService GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// 获取Assessment列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="studentAssessment">Assessment</param>
        /// <returns>Assessment集合</returns>
        public IList<Assessment> GetBaseAssessment(Assessment studentAssessment)
        {
            return _assessmentDao.GetBaseAssessment(studentAssessment);
        }

        /// <summary>
        /// 获取Assessment列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Assessment</returns>
        public Assessment GetBaseAssessmentByID(int id)
        {
            return _assessmentDao.GetBaseAssessmentByID(id);
        }

        /// <summary>
        /// 获取Assessment列表(包含父对象)
        /// </summary>
        /// <param name="studentAssessment">Assessment</param>
        /// <returns>Assessment集合</returns>
        public IList<Assessment> GetAssessment(Assessment studentAssessment)
        {
            return _assessmentDao.GetAssessment(studentAssessment);
        }

        /// <summary>
        /// 获取Assessment列表(包含父对象)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Assessment</returns>
        public Assessment GetAssessmentByID(int id)
        {
            return _assessmentDao.GetAssessmentByID(id);
        }

        /// <summary>
        /// 插入Assessment
        /// </summary>
        /// <param name="studentAssessment">Assessment</param>
        public int InsertAssessment(Assessment studentAssessment)
        {
            int _id = -1;

            try
            {
                //_daoManager.BeginTransaction();
                _id = _assessmentDao.InsertAssessment(studentAssessment);
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
        /// 分页 Assessment
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        public IList<Assessment> PaginatedAssessment(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum)
        {
            return _assessmentDao.PaginatedAssessment(pageIndex, pageSize, sortFields, conAttr, ref totalNum);
        }
        /// <summary>
        /// 更新Assessment
        /// </summary>
        /// <param name="studentAssessment">Assessment</param>
        public int UpdateAssessment(Assessment studentAssessment)
        {
            return _assessmentDao.UpdateAssessment(studentAssessment);
        }

        /// <summary>
        /// 删除Assessment
        /// </summary>
        /// <param name="id">自增型ID</param>
        public int DeleteAssessmentByID(int id)
        {
            return _assessmentDao.DeleteAssessmentByID(id);
        }

        #endregion
    }
}
