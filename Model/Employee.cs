using System;
namespace JNyuluSoft.Model
{
    /// <summary>
    /// 实体类Employee 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Employee
    {
        public Employee()
        { }
        #region Model
        private int _id;
        private byte? _usertype;
        private int? _gradeid;
        private string _name;
        private string _password;
        private string _truename;
        private string _mobile;
        private string _phone;
        private int? _permission;
        private string _description;
        private int _locked;
        private DateTime _lastlogindate;

        private DateTime _logindate;
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
        /// 用户类型：0:学生;1:教师;2:管理人员
        /// </summary>
        public byte? UserType
        {
            set { _usertype = value; }
            get { return _usertype; }
        }
        /// <summary>
        /// 2:管理人员(管理班级) 
        /// </summary>
        public int? GradeID
        {
            set { _gradeid = value; }
            get { return _gradeid; }
        }
        /// <summary>
        /// 用户名或学号
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 
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
        /// 联系电话
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 权限
        /// </summary>
        public int? Permission
        {
            set { _permission = value; }
            get { return _permission; }
        }
        /// <summary>
        /// 描述(课程管理员、销售专员、出纳人员、业务分析员、系统管理员)
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }

        /// <summary>
        /// 0:未锁定;1锁定
        /// </summary>
        public int Locked
        {
            set { _locked = value; }
            get { return _locked; }
        }

        /// <summary>
        /// 最后登录日期
        /// </summary>
        public DateTime LastLoginDate
        {
            set { _lastlogindate = value; }
            get { return _lastlogindate; }
        }

        public DateTime LoginDate
        {
            set { _logindate = value; }
            get { return _logindate; }
        }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime RecordDate
        {
            set { _recorddate = value; }
            get { return _recorddate; }
        }
        #endregion Model

    }
}

