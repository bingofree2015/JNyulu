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

using IBatisNet.DataAccess;

using JNyuluSoft.Persistence;
using JNyuluSoft.Persistence.Impl;
using JNyuluSoft.Model;


namespace JNyuluSoft.Service
{
    public class SyllabusService
    {
        #region 私有字段

        private static SyllabusService _instance = new SyllabusService();

        private ISyllabusDao _syllabusDao = null;

        #endregion

        #region 构造函数

        private SyllabusService() 
        {
            _syllabusDao = new SyllabusSqlMapDao();
        }

        #endregion

        #region 公共方法

        public static SyllabusService GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// 获取Syllabus列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="syllabus">Syllabus</param>
        /// <returns>Syllabus集合</returns>
        public IList<Syllabus> GetBaseSyllabus(Syllabus syllabus)
        {
            return _syllabusDao.GetBaseSyllabus(syllabus);
        }

        /// <summary>
        /// 获取Syllabus列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Syllabus</returns>
        public Syllabus GetBaseSyllabusByID(int id)
        {
            return _syllabusDao.GetBaseSyllabusByID(id);
        }

        /// <summary>
        /// 获取Syllabus列表(包含父对象)
        /// </summary>
        /// <param name="syllabus">Syllabus</param>
        /// <returns>Syllabus集合</returns>
        public IList<Syllabus> GetSyllabus(IDictionary dict)
        {
            return _syllabusDao.GetSyllabus(dict);
        }

        /// <summary>
        /// 获取Syllabus列表(包含父对象)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Syllabus</returns>
        public Syllabus GetSyllabusByID(int id)
        {
            return _syllabusDao.GetSyllabusByID(id);
        }

        /// <summary>
        /// 插入Syllabus
        /// </summary>
        /// <param name="syllabus">Syllabus</param>
        public int InsertSyllabus(Syllabus syllabus)
        {
            int _id = -1;

            try
            {
                //_daoManager.BeginTransaction();
                _id = _syllabusDao.InsertSyllabus(syllabus);
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
        /// 分页 Syllabus
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        public IList<Syllabus> PaginatedSyllabus(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum)
        {
            return _syllabusDao.PaginatedSyllabus(pageIndex, pageSize, sortFields, conAttr, ref totalNum);
        }
        /// <summary>
        /// 更新Syllabus
        /// </summary>
        /// <param name="syllabus">Syllabus</param>
        public int UpdateSyllabus(Syllabus syllabus)
        {
            return _syllabusDao.UpdateSyllabus(syllabus);
        }

        /// <summary>
        /// 删除Syllabus
        /// </summary>
        /// <param name="id"></param>
        public int DeleteSyllabusByID(int id)
        {
            return _syllabusDao.DeleteSyllabusByID(id);
        }

        #endregion
    }
}
