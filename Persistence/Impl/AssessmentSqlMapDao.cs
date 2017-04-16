/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 23:34:26
    修 改 者 : 
    修改时间 : 
    描    述 : Assessment
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
    /// Assessment数据库持久化处理类
    /// </summary>
    public class AssessmentSqlMapDao : BaseSqlMapDao, IAssessmentDao
    {
        /// <summary>
        /// 获取Assessment列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="studentAssessment">Assessment</param>
        /// <returns>Assessment集合</returns>
        public IList<Assessment> GetBaseAssessment(Assessment studentAssessment)
        {
            return ExecuteQueryForList<Assessment>("JNyulu.GetBaseAssessment", studentAssessment);
        }

        /// <summary>
        /// 获取Assessment列表,不包含父对象,仅返回对象本身。
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Assessment</returns>
        public Assessment GetBaseAssessmentByID(int id)
        {
            return ExecuteQueryForObject<Assessment>("JNyulu.GetBaseAssessmentByID", id);
        }

        /// <summary>
        /// 获取Assessment列表(包含父对象)
        /// </summary>
        /// <param name="studentAssessment">Assessment</param>
        /// <returns>Assessment集合</returns>
        public IList<Assessment> GetAssessment(Assessment studentAssessment)
        {
            return ExecuteQueryForList<Assessment>("JNyulu.GetAssessment", studentAssessment);
        }

        /// <summary>
        /// 获取Assessment列表(包含父对象)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Assessment</returns>
        public Assessment GetAssessmentByID(int id)
        {
            return ExecuteQueryForObject<Assessment>("JNyulu.GetAssessmentByID", id);
        }

        /// <summary>
        /// 插入Assessment
        /// </summary>
        /// <param name="Assessment">Assessment</param>
         public int InsertAssessment(Assessment studentAssessment)
        {
            Hashtable _map = new Hashtable();
            _map.Add("STID", studentAssessment.STID);
            _map.Add("EmployeeID", studentAssessment.EmployeeID);
            _map.Add("TrueName", studentAssessment.TrueName);
            _map.Add("DisciplineRank", studentAssessment.DisciplineRank);
            _map.Add("IntegrateRank", studentAssessment.IntegrateRank);
            _map.Add("Judgment", studentAssessment.Judgment);

            _map.Add("ID", studentAssessment.ID);

            studentAssessment.ID = (int)ExecuteInsert("JNyulu.InsertAssessment", _map);

            return studentAssessment.ID;
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
             Hashtable _map = new Hashtable();
             _map.Add("pageIndex", pageIndex);
             _map.Add("pageSize", pageSize);
             _map.Add("sortFields", sortFields);
             _map.Add("conAttr", conAttr);
             _map.Add("totalNum", totalNum);

             IList<Assessment> _listObj = ExecuteQueryForList<Assessment>("JNyulu.PaginatedAssessment", _map);

             totalNum = (int)_map["totalNum"];

             return _listObj;
         }

        /// <summary>
        /// 更新Assessment
        /// </summary>
        /// <param name="Assessment">Assessment</param>
        public int UpdateAssessment(Assessment studentAssessment)
        {
            int i = ExecuteUpdate("JNyulu.UpdateAssessment", studentAssessment);
            return i;
        }

        /// <summary>
        /// 删除Assessment
        /// </summary>
        /// <param name="id">自增型ID</param>
        public int DeleteAssessmentByID(int id)
        {
            return ExecuteDelete("JNyulu.DeleteAssessmentByID", id);
        }
    }
}
