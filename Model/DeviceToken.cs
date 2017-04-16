using System;
namespace JNyuluSoft.Model
{
	/// <summary>
	/// ʵ����DeviceToken ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class DeviceToken
	{
        public DeviceToken()
		{}
		#region Model
		private int _id;
		private string _clientid;
        private int _userid;
        private string _token;
        private string _platform;
        private string _ver;
        private DateTime _updatetime;
		private DateTime _registertime;
		/// <summary>
		/// ������ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
        /// �ն�Ψһ��ʶ
		/// </summary>
        public string ClientID
		{
            set { _clientid = value; }
            get { return _clientid; }
		}
        /// <summary>
        /// �û�ID
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// �Ự����
        /// </summary>
        public string Token
        {
            set { _token = value; }
            get { return _token; }
        }
        /// <summary>
        /// ƽ̨(�ն�ƽ̨iphone\android)
        /// </summary>
        public string Platform
        {
            set { _platform = value; }
            get { return _platform; }
        }

        /// <summary>
        /// �汾��
        /// </summary>
        public string Ver
        {
            set { _ver = value; }
            get { return _ver; }
        }
        /// <summary>
        /// �������ʱ��
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
		/// <summary>
        /// ע��ʱ��
		/// </summary>
        public DateTime RegisterTime
		{
            set { _registertime = value; }
            get { return _registertime; }
		}
		#endregion Model

	}
}

