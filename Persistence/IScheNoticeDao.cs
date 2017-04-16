/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 23:34:23
    修 改 者 : 
    修改时间 : 
    描    述 : ScheNotice
===================================================== */

using System;
using System.Collections; 
using System.Collections.Generic;
using System.Text;

using JNyuluSoft.Model;

namespace JNyuluSoft.Persistence
{
    /// <summary>
    /// ScheNotice: ScheNotice的数据库操作接口.
    /// </summary>
    public interface IScheNoticeDao
    {
        /// <summary>
        /// 获取ScheNotice列表(包含父对象)
        /// </summary>
        /// <param name="scheNotice">ScheNotice</param>
        /// <returns>ScheNotice集合</returns>
        IList<ScheNotice> GetScheNotice(IDictionary dict);

        /// <summary>
        /// 获取ScheNotice列表(包含父对象)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ScheNotice</returns>
        ScheNotice GetScheNoticeByID(int id);

        /// <summary>
        /// 插入ScheNotice
        /// </summary>
        /// <param name="scheNotice">ScheNotice</param>
        int InsertScheNotice(ScheNotice scheNotice);

        /// <summary>
        /// 分页 ScheNotice
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        IList<ScheNotice> PaginatedScheNotice(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum);

        /// <summary>
        /// 更新ScheNotice
        /// </summary>
        /// <param name="scheNotice">ScheNotice</param>
        int UpdateScheNotice(ScheNotice scheNotice);

        /// <summary>
        /// 删除ScheNotice
        /// </summary>
        /// <param name="id"></param>
        int DeleteScheNoticeByID(int id);

    }
}
