using System;
using System.IO;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Threading;

using Castle.MonoRail.Framework;

using JNyuluSoft.Model;
using JNyuluSoft.Service;
using JNyuluSoft.Util;

namespace JNyuluSoft.Web.Controllers
{
    /* ��Ϣ����  */
    [ControllerDetails(Area = "Background")]
    public class NotificationsController : DefaultController
    {
        protected readonly SchoolTermService _schoolTermService = SchoolTermService.GetInstance();

        protected readonly NotificationService _notificationService = NotificationService.GetInstance();
        protected readonly EmployeeService _employeeService = EmployeeService.GetInstance();
        protected readonly RegistrationService _registrationService = RegistrationService.GetInstance();

        protected readonly DeviceTokenService _deviceTokenService = DeviceTokenService.GetInstance();

        public void List(int pageIndex)
        {
            PropertyBag["params"] = new
            {
                PageIndex = pageIndex
            };

            int _totalNums = 0, _pageSize = 12;
            if (pageIndex < 1) pageIndex = 1;

            string _conAttr = "1 = 1";

            IList<Notification> _notificationList = _notificationService.PaginatedNotification(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);
            PropertyBag["notificationList"] = _notificationList;

            string uri = "List.do?pageIndex={0}";
            string ltlShowPager = Paginated.BuildPager(uri, _totalNums, pageIndex, _pageSize);

            PropertyBag["ltlShowPager"] = ltlShowPager;
        }

        public void Create(int pageIndex)
        {
            PropertyBag["params"] = new
            {
                PageIndex = pageIndex
            };

            int _totalNums = 0;
            IList<SchoolTerm> _schoolTermList = _schoolTermService.PaginatedSchoolTerm(1, 120, "ID", "1 = 1", ref _totalNums);
            PropertyBag["schoolTermList"] = _schoolTermList;
        }

        public void PostCreate(string title, string body, string receivers, string[] platform, int schoolTermID)
        {
            int _code = 0;
            string _message = "����������Ϣʧ��";
            Employee _employee = Session["logonUser"] as Employee;

            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(body))
            {
                List<string> _platform = new List<string>(platform);

                Notification _notification = new Notification();

                _notification.Title = title;
                _notification.Body = body;
                _notification.Platform = string.Join(",", platform);
                _notification.Receivers = receivers;
                _notification.STID = schoolTermID;

                _code = _notificationService.InsertNotification(_notification);

                if (_code > 0)
                {
                    List<int> _userIDs = new List<int>();

                    string[] _receivers = receivers.Split(',');
                    if (_receivers.Length > 0)
                    {
                        IList<Employee> _employeeList = _employeeService.GetEmployeeByNames(_receivers);
                        foreach (Employee _item in _employeeList)
                        {
                            _userIDs.Add(_item.ID);
                        }
                    }

                    if (schoolTermID > 0)
                    {
                        Registration _registration = new Registration();
                        _registration.STID = schoolTermID;

                        IList<Registration> _registrationList = _registrationService.GetRegistration(_registration);
                        foreach (Registration _item in _registrationList)
                        {
                            _userIDs.Add(_item.EmployeeID.Value);
                        }
                    }

                    if (_platform.Contains("iphone"))
                    {

                        IList<DeviceToken> _deviceTokenList = _deviceTokenService.GetDeviceToken("iphone", null, _userIDs.ToArray());

                        List<PushMessage> _pushMessageList = new List<PushMessage>();
                        foreach (DeviceToken _deviceToken in _deviceTokenList)
                        {
                            PushMessage _pushMessage = new PushMessage();
                            _pushMessage.MessageID = _code;
                            _pushMessage.Token = _deviceToken.Token;
                            _pushMessage.Platform = "iphone";
                            _pushMessage.Body = body;

                            _pushMessageList.Add(_pushMessage);
                        }

                        var _thread = new Thread(() =>
                        {
                            foreach (PushMessage _pushMessage in _pushMessageList)
                            {
                                ANPSPush _anpsPush = new ANPSPush();
                                _anpsPush.PushMessage(_pushMessage);
                            }
                        });
                        _thread.Start();
                    }

                    if (_platform.Contains("android"))
                    {
                        IList<DeviceToken> _deviceTokenList = _deviceTokenService.GetDeviceToken("android", null, _userIDs.ToArray());

                        List<PushMessage> _pushMessageList = new List<PushMessage>();
                        foreach (DeviceToken _deviceToken in _deviceTokenList)
                        {
                            PushMessage _pushMessage = new PushMessage();
                            _pushMessage.ClientID = _deviceToken.ClientID;
                            _pushMessage.Token = _deviceToken.Token;
                            _pushMessage.Platform = "android";
                            _pushMessage.Body = body;

                            _pushMessageList.Add(_pushMessage);
                        }

                        var _thread = new Thread(() =>
                        {
                            BaiduPush _baiduPush = new BaiduPush(_pushMessageList);
                            _baiduPush.PushMessage();
                        });
                        _thread.Start();
                    }

                    _message = "����������Ϣ�ɹ�";
                }
                else
                {
                    _message = "�Բ��𣬷���������Ϣʧ�ܣ�������������";
                }
            }
            else
            {
                _message = "������Ϣ���ⲻ��Ϊ�գ�";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void TestPush()
        {
            string _token = "819449d621a891b545b12b34a41c1fe45d522d3c15461d53bbaae719d1d20a1c";

            List<PushMessage> _pushMessageList = new List<PushMessage>();
            {
                PushMessage _pushMessage = new PushMessage();
                _pushMessage.MessageID = 22;
                _pushMessage.Token = _token;
                _pushMessage.Platform = "iphone";
                _pushMessage.Body = "2014���¿����ޱ���������";

                _pushMessageList.Add(_pushMessage);
            }
            /*
            var _thread = new Thread(() =>
            {
                ANPSPush _anpsPush = new ANPSPush();
                _anpsPush.PushMessage(_pushMessageList);
            });
            _thread.Start();
            */
            RenderText("{\"code\":\"" + 22 + "\",\"message\":\"�ɹ�\"}");

            CancelLayout();
        }

        public void Edit(int notificationID, int pageIndex)
        {
            PropertyBag["params"] = new
            {
                PageIndex = pageIndex
            };
            int _totalNums = 0;
            IList<SchoolTerm> _schoolTermList = _schoolTermService.PaginatedSchoolTerm(1, 120, "ID", "1 = 1", ref _totalNums);
            PropertyBag["schoolTermList"] = _schoolTermList;

            Notification _notification = _notificationService.GetNotificationByID(notificationID);
            PropertyBag["notification"] = _notification;
        }

        public void PostEdit(int notificationID, string title, string body, string[] platform, string receivers, int schoolTermID)
        {
            int _code = 0;
            string _message = "�޸�������Ϣʧ��";
            Employee _employee = Session["logonUser"] as Employee;

            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(body))
            {
                List<string> _platform = new List<string>(platform);

                Notification _notification = new Notification();

                _notification.ID = notificationID;
                _notification.Title = title;
                _notification.Body = body;
                _notification.Platform = string.Join(",", platform);

                _notification.Receivers = receivers;
                _notification.STID = schoolTermID;

                _code = _notificationService.UpdateNotification(_notification);

                if (_code > 0)
                {
                    _message = "�޸�������Ϣ�ɹ�";
                }
                else
                {
                    _message = "�Բ����޸�ʧ�ܣ�";
                }
            }
            else
            {
                _message = "������Ϣʧ�ܲ���Ϊ�գ�";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void PostDelete(int notificationID)
        {
            int _code = 0;
            string _message = "ɾ��������Ϣ��Ϣʧ��";

            _code = _notificationService.DeleteNotificationByID(notificationID);

            if (_code > 0)
            {
                _message = "ɾ��������Ϣ��Ϣ�ɹ�";
            }
            else
            {
                _message = "�Բ���ɾ��ʧ�ܣ�";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }
    }
}
