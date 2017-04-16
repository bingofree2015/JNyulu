using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using JdSoft.Apple.Apns.Notifications;
using Newtonsoft.Json.Linq;
using JNyuluSoft.Model;

using ANPSNotification = JdSoft.Apple.Apns.Notifications.Notification;

namespace JNyuluSoft.Util
{
    public class ANPSPush
    {
        private static int _sendRetires = 2;
        private static int _reconnectDelay = 100;
        private static int _connectionCount = 1;
        private string _p12FileName = "jnyulu_{0}_dist.p12";
        private static string _p12FilePassword = "abcd1234!";

        //Create our push services broker
        protected NotificationService _notificationService;

        #region 线程传递参数
        //private string _host = "gateway.push.apple.com"; //gateway.sandbox.push.apple.com 2195
        private string _host = "gateway.push.apple.com"; // 2195
        #endregion

        //包含参数的构造函数
        public ANPSPush(string platform = "iphone")
        {
            #region 消息体参数
            _p12FileName = string.Format(_p12FileName, platform);
            #endregion

            try
            {
                var _p12File = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _p12FileName);
                //NLog.Info("证书 " + _p12File + "密码 " + _p12FilePassword);

                //Wire up the events for all the services that the broker registers
                _notificationService = new NotificationService(_host, 2195, _p12File, _p12FilePassword, _connectionCount)
                {
                    SendRetries = _sendRetires,
                    ReconnectDelay = _reconnectDelay,
                    DistributionType = NotificationServiceDistributionType.Random
                };

                _notificationService.NotificationTooLong += new NotificationService.OnNotificationTooLong(Service_NotificationTooLong);
                _notificationService.BadDeviceToken += new NotificationService.OnBadDeviceToken(Service_BadDeviceToken);
                _notificationService.NotificationFailed += new NotificationService.OnNotificationFailed(Service_NotificationFailed);
                _notificationService.NotificationSuccess += new NotificationService.OnNotificationSuccess(Service_NotificationSuccess);

                _notificationService.Error += new NotificationService.OnError(Service_Error);
            }
            catch (Exception ex)
            {
                LogHelper.Error("NotificationService初始化失败: " + ex.ToString());
            }
        }

        public void PushMessage(PushMessage pushMessage)
        {
            List<PushMessage> pushMessageList = new List<Model.PushMessage>();
            pushMessageList.Add(pushMessage);

            LogHelper.Info("_pushMessageList:  " + pushMessageList.Count);

            if (pushMessageList != null)
            {
                for (int i = 0; i < pushMessageList.Count; i++)
                {
                    PushMessage _pushMessage = pushMessageList[i];

                    if (_pushMessage.Token.Length == 64)
                    {
                        try
                        {
                            var notification = new ANPSNotification(_pushMessage.Token);
                            notification.Payload.Alert.Body = _pushMessage.Body;
                            //notification.Payload.Sound = "default";
                            if (!string.IsNullOrEmpty(_pushMessage.CustomItem))
                            {
                                var custom = JObject.Parse(_pushMessage.CustomItem);
                                notification.Payload.AddCustom("page", custom);
                            }
                            notification.Tag = _pushMessage;

                            var _result = _notificationService.QueueNotification(notification);
                            if (_result)
                            {
                                int _timeOut = 1;
                                if (i % 100 == 0)
                                {
                                    _timeOut = 2;
                                }
                                System.Threading.Thread.Sleep(_timeOut);
                            }
                            else
                            {
                                LogHelper.Info("消息[" + notification.DeviceToken + "]Push至IOS服务器的时候失败");
                            }
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
                    pushMessageList.Clear();
                    _notificationService.Close();
                    _notificationService.Dispose();
                }
            }
        }

        #region 消息发送事件
        protected virtual void Service_BadDeviceToken(object sender, BadDeviceTokenException ex)
        {
            LogHelper.Info("Bad Device Token:" + ex.Message);
        }

        protected virtual void Service_NotificationTooLong(object sender, NotificationLengthException ex)
        {
            LogHelper.Info("Notification Too Long: " + ex.Notification.ToString());
        }

        protected virtual void Service_NotificationSuccess(object sender, ANPSNotification notification)
        {
            PushMessage _pushMessage = (PushMessage)notification.Tag;
            LogHelper.Info(notification.DeviceToken + "[" + _pushMessage.MessageID + "] {" + _p12FileName.PadRight(25) + " 密码:" + _p12FilePassword + "} success...");
        }

        protected virtual void Service_NotificationFailed(object sender, ANPSNotification notification)
        {
            PushMessage _pushMessage = (PushMessage)notification.Tag;
            LogHelper.Info(notification.DeviceToken + "[" + _pushMessage.MessageID + "] failed...");
        }

        protected virtual void Service_Error(object sender, Exception ex)
        {
            LogHelper.Info("Service Error: " + ex.Message);
        }

        #endregion
    }
}
