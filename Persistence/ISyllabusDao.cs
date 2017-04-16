/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 23:34:23
    修 改 者 : 
    修改时间 : 
    描    述 : Syllabus
===================================================== */

using System;
using System.Collections; 
using System.Collections.Generic;
using System.Text;

using JNyuluSoft.Model;

namespace JNyuluSoft.Persistence
{
    /// <summary>
    /// Syllabus: Syllabus的数据库操作接口.
    /// </summary>
    public interface ISyllabusDao
    {
        /// <summary>
        /// 获取Syllabus列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="syllabus">Syllabus</param>
        /// <returns>Syllabus集合</returns>
        IList<Syllabus> GetBaseSyllabus(Syllabus syllabus);

        /// <summary>
        /// 获取Syllabus列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Syllabus</returns>
        Syllabus GetBaseSyllabusByID(int id);

        /// <summary>
        /// 获取Syllabus列表(包含父对象)
        /// </summary>
        /// <param name="syllabus">Syllabus</param>
        /// <returns>Syllabus集合</returns>
        IList<Syllabus> GetSyllabus(IDictionary dict);

        /// <summary>
        /// 获取Syllabus列表(包含父对象)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Syllabus</returns>
        Syllabus GetSyllabusByID(int id);

        /// <summary>
        /// 插入Syllabus
        /// </summary>
        /// <param name="syllabus">Syllabus</param>
        int InsertSyllabus(Syllabus syllabus);

        /// <summary>
        /// 分页 Syllabus
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        IList<Syllabus> PaginatedSyllabus(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum);

        /// <summary>
        /// 更新Syllabus
        /// </summary>
        /// <param name="syllabus">Syllabus</param>
        int UpdateSyllabus(Syllabus syllabus);

        /// <summary>
        /// 删除Syllabus
        /// </summary>
        /// <param name="id"></param>
        int DeleteSyllabusByID(int id);

    }
}
