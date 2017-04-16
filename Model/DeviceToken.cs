using System;
namespace JNyuluSoft.Model
{
	/// <summary>
	/// 实体类DeviceToken 。(属性说明自动提取数据库字段的描述信息)
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
		/// 自增型ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
        /// 终端唯一标识
		/// </summary>
        public string ClientID
		{
            set { _clientid = value; }
            get { return _clientid; }
		}
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 会话令牌
        /// </summary>
        public string Token
        {
            set { _token = value; }
            get { return _token; }
        }
        /// <summary>
        /// 平台(终端平台iphone\android)
        /// </summary>
        public string Platform
        {
            set { _platform = value; }
            get { return _platform; }
        }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Ver
        {
            set { _ver = value; }
            get { return _ver; }
        }
        /// <summary>
        /// 最近更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
		/// <summary>
        /// 注册时间
		/// </summary>
        public DateTime RegisterTime
		{
            set { _registertime = value; }
            get { return _registertime; }
		}
		#endregion Model

	}
}

