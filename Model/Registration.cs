using System;
using System.Collections;
using System.Collections.Generic;

namespace JNyuluSoft.Model
{
	/// <summary>
	/// 实体类Registration 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Registration
	{
		public Registration()
		{}
		#region Model
		private int _id;
		private int? _employeeid;
        private string _employeename;
        private string _employeepwd;
        private string _truename;
        private string _mobile;
        private string _phone;
        private string _schoolname;
        private int? _gradeid;
        private string _gradename;
        private string _campusname;
        private string _schooltermname;
        private int? _stid;
		private decimal? _schoolfee;
		private int? _status;
        private decimal? _refund;
		private string _reasons;
		private string _assessor;
		private string _remark;
        private string _judgment;

        private IList<ReportCard> _reportcardlist;
        private IList<Syllabus> _syllabuslist;
        
		private DateTime _recorddate = DateTime.Now;
		/// <summary>
		/// 自增型ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 注册用户ID
		/// </summary>
		public int? EmployeeID
		{
			set{ _employeeid=value;}
			get{return _employeeid;}
		}
        /// <summary>
        /// 注册用户名
        /// </summary>
        public string  EmployeeName
        {
            set { _employeename = value; }
            get { return _employeename; }
        }
        /// <summary>
        /// 用户名密码
        /// </summary>
        public string  EmployeePwd
        {
            set { _employeepwd = value; }
            get { return _employeepwd; }
        }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string TrueName
        {
            set { _truename = value; }
            get { return _truename; }
        }
        /// <summary>
        /// 家长手机
        /// </summary>
        public string Mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }
        /// <summary>
        /// 家庭电话
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 原校
        /// </summary>
        public string SchoolName
        {
            set { _schoolname = value; }
            get { return _schoolname; }
        }
        /// <summary>
        /// 原年级ID
        /// </summary>
        public int? GradeID
        {
            set { _gradeid = value; }
            get { return _gradeid; }
        }
        /// <summary>
        /// 原年级
        /// </summary>
        public string GradeName
        {
            set { _gradename = value; }
            get { return _gradename; }
        }
        /// <summary>
        /// 校区名称
        /// </summary>
        public string CampusName
        {
            set { _campusname = value; }
            get { return _campusname; }
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
		/// 报名费用
		/// </summary>
		public decimal? SchoolFee
		{
			set{ _schoolfee=value;}
			get{return _schoolfee;}
		}
		/// <summary>
		/// 0:正常;-1:已退课
		/// </summary>
		public int? Status
		{
			set{ _status=value;}
			get{return _status;}
		}

		/// <summary>
		/// 退还费用
		/// </summary>
        public decimal? Refund
		{
            set { _refund = value; }
            get { return _refund; }
		}

		/// <summary>
		/// 退课原因
		/// </summary>
		public string Reasons
		{
			set{ _reasons=value;}
			get{return _reasons;}
		}
		/// <summary>
		/// 办理人
		/// </summary>
		public string Assessor
		{
			set{ _assessor=value;}
			get{return _assessor;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
        /// <summary>
        /// 考评
        /// </summary>
        public string Judgment
        {
            set { _judgment = value; }
            get { return _judgment; }
        }
        /// <summary>
        /// 试卷
        /// </summary>
        public IList<ReportCard> ReportCardList
        {
            set { _reportcardlist = value; }
            get { return _reportcardlist; }
        }
        /// <summary>
        /// 课程表
        /// </summary>
        public IList<Syllabus> SyllabusList
        {
            set { _syllabuslist = value; }
            get { return _syllabuslist; }
        }
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime RecordDate
		{
			set{ _recorddate=value;}
			get{return _recorddate;}
		}
		#endregion Model

	}
}

