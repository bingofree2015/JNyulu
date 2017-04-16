using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MonoRail.Framework;

using Newtonsoft.Json;

using JNyuluSoft.Model;
using JNyuluSoft.Service;

namespace JNyuluSoft.Web.Api.Controllers
{
    [ControllerDetails(Area = "Api")]
    public class DeviceTokenController : ApiController
    {
        protected readonly DeviceTokenService _deviceTokenService = DeviceTokenService.GetInstance();

        /// <summary>
        /// 注册DeviceToken
        /// </summary>
        /// <param name="clientID">终端唯一标识</param>
        /// <param name="userID">用户ID</param>
        /// <param name="token">会话令牌</param>
        /// <param name="platform">平台(终端平台iphone\android)</param>
        /// <param name="ver">版本号</param>
        /// http://61.135.144.37:8090/Api/DeviceToken/GetDeviceTokenInfo.do?clientID=12868&userID=12868&token=12868&platform=android&ver=1.0
        public void GetDeviceTokenInfo(string clientID, int? userID, string token, string platform, string ver)
        {
            if (userID == null)
            {
                userID = 0;
            }
            if (!string.IsNullOrEmpty(clientID) && !string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(platform) && !string.IsNullOrEmpty(ver))
            {
                DeviceToken _deviceToken = new DeviceToken();
                _deviceToken.ClientID = clientID;
                _deviceToken.UserID = userID.Value;
                _deviceToken.Token = token;
                _deviceToken.Platform = platform;
                _deviceToken.Ver = ver;

                _result.Code = _deviceTokenService.InsertDeviceToken(_deviceToken);
                if (_result.Code > 0)
                {
                    _result.Data = "ok";
                }
                else
                {
                    _result.Data = "记录存在";
                }
            }
            else
            {
                _result.Data = "对不起，参数不能为空！";
            }

            string _jsonString = JsonConvert.SerializeObject(_result, Formatting.Indented);

            RenderText(_jsonString);
            CancelLayout();

        }
    }
}