using System;
namespace JNyuluSoft.Model
{
	/// <summary>
	/// 实体类ReportCard 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class ReportCard
	{
		public ReportCard()
		{}
		#region Model
		private int _id;
        private int? _stid;
        private string _schooltermname;
		private int? _employeeid;
        private string _employeename;
        private string _truename;
		private DateTime? _examdate;
        private string _testpaperurl;
		private DateTime _recorddate;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
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
			set{ _employeeid=value;}
			get{return _employeeid;}
		}
		/// <summary>
		/// 学生学号
		/// </summary>
        public string EmployeeName
		{
            set { _employeename = value; }
            get { return _employeename; }
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
		/// 考试日期
		/// </summary>
		public DateTime? ExamDate
		{
			set{ _examdate=value;}
			get{return _examdate;}
        }
        /// <summary>
        /// 试卷路径 
        /// </summary>
        public string TestPaperUrl
        {
            set {
                if (!string.IsNullOrEmpty(value) && !value.ToLower().Contains("testpaperfiles"))
                {
                    _testpaperurl = "/UploadFiles/testPaperFiles/" + value;
                }
                else { _testpaperurl = value; }
            }
            get {
                return _testpaperurl; 
            }
        }

		/// <summary>
        /// 创建日期
		/// </summary>
		public DateTime RecordDate
		{
			set{ _recorddate=value;}
			get{return _recorddate;}
		}

		#endregion Model
	}
}

