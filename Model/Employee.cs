using System;
namespace JNyuluSoft.Model
{
    /// <summary>
    /// ʵ����Employee ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        /// ������ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// �û����ͣ�0:ѧ��;1:��ʦ;2:������Ա
        /// </summary>
        public byte? UserType
        {
            set { _usertype = value; }
            get { return _usertype; }
        }
        /// <summary>
        /// 2:������Ա(����༶) 
        /// </summary>
        public int? GradeID
        {
            set { _gradeid = value; }
            get { return _gradeid; }
        }
        /// <summary>
        /// �û�����ѧ��
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// ����
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
        /// �ҳ��ֻ�
        /// </summary>
        public string Mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }
        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// Ȩ��
        /// </summary>
        public int? Permission
        {
            set { _permission = value; }
            get { return _permission; }
        }
        /// <summary>
        /// ����(�γ̹���Ա������רԱ��������Ա��ҵ�����Ա��ϵͳ����Ա)
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }

        /// <summary>
        /// 0:δ����;1����
        /// </summary>
        public int Locked
        {
            set { _locked = value; }
            get { return _locked; }
        }

        /// <summary>
        /// ����¼����
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
        /// ��������
        /// </summary>
        public DateTime RecordDate
        {
            set { _recorddate = value; }
            get { return _recorddate; }
        }
        #endregion Model

    }
}

