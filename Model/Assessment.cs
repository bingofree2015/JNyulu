using System;
namespace JNyuluSoft.Model
{
    /// <summary>
    /// 实体类Assessment 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Assessment
    {
        public Assessment()
        { }
        #region Model
        private int _id;
        private int? _stid;
        private string _schooltermname;
        private int? _employeeid;
        private string _truename;
        private string _disciplinerank;
        private string _integraterank;
        private string _judgment;
        private DateTime _recorddate;

        /// <summary>
        /// 自增型ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 班级ID
        /// </summary>
        public int? STID
        {
            set { _stid = value; }
            get { return _stid; }
        }
        /// <summary>
        /// 班级名称
        /// </summary>
        public string SchoolTermName
        {
            set { _schooltermname = value; }
            get { return _schooltermname; }

        }
        /// <summary>
        /// 学生ID
        /// </summary>
        public int? EmployeeID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string TrueName
        {
            set { _truename = value; }
            get { return _truename; }
        }
        /// <summary>
        /// 纪律评价
        /// </summary>
        public string DisciplineRank
        {
            set { _disciplinerank = value; }
            get { return _disciplinerank; }
        }
        /// <summary>
        /// 综合评价
        /// </summary>
        public string IntegrateRank
        {
            set { _integraterank = value; }
            get { return _integraterank; }
        }
        /// <summary>
        /// 评语
        /// </summary>
        public string Judgment
        {
            set { _judgment = value; }
            get { return _judgment; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime RecordDate
        {
            set { _recorddate = value; }
            get { return _recorddate; }
        }
        #endregion Model

    }
}

