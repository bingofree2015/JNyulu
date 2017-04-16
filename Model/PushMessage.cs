using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace JNyuluSoft.Model
{
    [Serializable]
    public class PushMessage : ICloneable
    {
        /// <summary>
        /// Platform
        /// </summary>
        private string _platform = "iphone";
        public string Platform { get { return _platform; } set { _platform = value; } }

        /// <summary>
        /// ClientID
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 消息体
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 自定义参数
        /// </summary>
        public string CustomItem { get; set; }
        /// <summary>
        /// 消息ID
        /// </summary>
        private int _messageId = 0;
        public int MessageID { get { return _messageId; } set { _messageId = value; } }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        //深拷贝，需要重新生成对象，返回的新对象的实例
        public PushMessage DeepClone()
        {
            //深复制
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, this);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as PushMessage;
            }
        }

        public PushMessage ShallowClone()
        {
            return Clone() as PushMessage;
        }

        override public string ToString()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}", Platform, Token, Body, CustomItem, MessageID);
        }
    }
}
