using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Baidu.Notifications;
using JNyuluSoft.Model;

using BaiduNotification = Baidu.Notifications.Notification;

namespace JNyuluSoft.Util
{
    public class BaiduPush
    {
        private static string ApiKey = "6MH7PHs6FVfVYLoR0k9onNwf";
        private static string SecretKey = "TafVQUrbeM0c2TIFGG8DHeYOxgqxGLHq";
        private String Method = "push_msg";
        private uint DeviceType = 3;
        private uint MessageType = 1; //通知（对应设备上的消息通知）

        //Create our push services broker
        protected Push _notificationService;

        #region 线程传递参数
        private List<PushMessage> _pushMessageList = new List<PushMessage>();
        #endregion

        //包含参数的构造函数
        public BaiduPush(List<PushMessage> pushMessageList, string platform = "android")
        {
            #region 消息体参数
            _pushMessageList = pushMessageList;
            #endregion

            try
            {
                //Wire up the events for all the services that the broker registers
                _notificationService = new Push("POST", SecretKey);
            }
            catch (Exception ex)
            {
                LogHelper.Error("NotificationService初始化失败: " + ex.ToString());
            }
        }

        public void PushMessage()
        {
            if (_pushMessageList != null)
            {
                for (int i = 0; i < _pushMessageList.Count; i++)
                {
                    PushMessage _pushMessage = _pushMessageList[i];

                    if (_pushMessage.Token.Length > 0)
                    {
                        try
                        {
                            TimeSpan ts = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
                            uint _timestamp = (uint)ts.TotalSeconds;

                            var notification = new BaiduNotification();

                            notification.title = string.Empty;
                            notification.description = _pushMessage.Body;
                            string messages = notification.getJsonString();
                            string msg_keys = messages.GetHashCode().ToString();

                            Options _opts;

                            //点发
                            string _userID = _pushMessage.Token;
                            string _channelID = _pushMessage.ClientID;

                            _opts = new Options(Method, ApiKey, _userID, _channelID, DeviceType, messages, msg_keys, _timestamp);

                            //群发
                            //string _tag = string.Empty;
                            //_opts = new Options(Method, ApiKey, _tag, DeviceType, messages, msg_keys, _timestamp);

                            //广播
                            //_opts = new Options(Method, ApiKey, DeviceType, messages, msg_keys, _timestamp);

                            _opts.message_type = MessageType;
                            _opts.deploy_status = 2; //IOS 1:DEV;2:PRO

                            string _result = _notificationService.PushMessage(_opts);
                            LogHelper.Info(_result);

                            int _timeOut = 2;
                            System.Threading.Thread.Sleep(_timeOut);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Info(ex.ToString());
                        }
                    }
                }
                //Stop and wait for the queues to drains
                if (_notificationService != null)
                {
                    _pushMessageList.Clear();
                }
            }
        }
    }
}
