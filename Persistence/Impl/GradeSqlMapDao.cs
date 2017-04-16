/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 21:53:31
    修 改 者 : 
    修改时间 : 
    描    述 : Grade
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
    /// Grade数据库持久化处理类
    /// </summary>
    public class GradeSqlMapDao : BaseSqlMapDao, IGradeDao
    {
        /// <summary>
        /// 获取Grade列表
        /// </summary>
        /// <param name="grade">Grade</param>
        /// <returns>Grade集合</returns>
        public IList<Grade> GetGrade(Grade grade)
        {
            return ExecuteQueryForList<Grade>("JNyulu.GetGrade", grade);
        }

        /// <summary>
        /// 获取Grade列表
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Grade</returns>
        public Grade GetGradeByID(int id)
        {
            return ExecuteQueryForObject<Grade>("JNyulu.GetGradeByID", id);
        }
        /// <summary>
        /// 获取Grade对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Grade GetGradeByName(string name)
        {
            return ExecuteQueryForObject<Grade>("JNyulu.GetGradeByName", name);
        }

        /// <summary>
        /// 插入Grade
        /// </summary>
        /// <param name="Grade">Grade</param>
        public int InsertGrade(Grade grade)
        {
            Hashtable _map = new Hashtable();
            _map.Add("SN", grade.SN);
            _map.Add("Name", grade.Name);
            _map.Add("ID", grade.ID);

            grade.ID = (int)ExecuteInsert("JNyulu.InsertGrade", _map);

            return grade.ID;
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
            Hashtable _map = new Hashtable();
            _map.Add("pageIndex", pageIndex);
            _map.Add("pageSize", pageSize);
            _map.Add("sortFields", sortFields);
            _map.Add("conAttr", conAttr);
            _map.Add("totalNum", totalNum);

            IList<Grade> _listObj = ExecuteQueryForList<Grade>("JNyulu.PaginatedGrade", _map);

            totalNum = (int)_map["totalNum"];

            return _listObj;
        }

        /// <summary>
        /// 更新Grade
        /// </summary>
        /// <param name="Grade">Grade</param>
        public int UpdateGrade(Grade grade)
        {
            int i = ExecuteUpdate("JNyulu.UpdateGrade", grade);
            return i;
        }

        /// <summary>
        /// 删除Grade
        /// </summary>
        /// <param name="id">自增型ID</param>
        public int DeleteGradeByID(int id)
        {
            return ExecuteDelete("JNyulu.DeleteGradeByID", id);
        }
    }
}
