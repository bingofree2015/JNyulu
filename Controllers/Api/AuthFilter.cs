using System;
using System.Collections.Specialized;
using System.Linq;
using Castle.MonoRail.Framework;
using JNyuluSoft.Model;
using JNyuluSoft.Service;
using JNyuluSoft.Util;
using Newtonsoft.Json;

namespace JNyuluSoft.Web.Api.Controllers
{
    public class AuthFilter : IFilter
    {
        public bool Perform(ExecuteEnum exec, IRailsEngineContext context, Controller controller)
        {
            controller.Response.ContentType = "text/plain; charset=utf-8";

            if (JNyuluUtils.CheckSign)
            {
                string _clientSign = context.Request.Params["sign"];
                if (!string.IsNullOrEmpty(_clientSign))
                {
                    LogHelper.Info("HttpMethod:" + context.Request.HttpMethod);

                    NameValueCollection _allParams = new NameValueCollection();

                    if (context.Request.HttpMethod == "GET")
                    {
                        _allParams = controller.Request.QueryString;
                    }
                    else if (context.Request.HttpMethod == "POST")
                    {
                        _allParams = controller.Request.Form;
                    }

                    LogHelper.Info("url:"+ context.Request.RawUrl);

                    string signUrl = string.Join("", _allParams.AllKeys.Where(s => JNyuluUtils.NoSignField.IndexOf("|" + s + "|") == -1).OrderBy(s => s).Select(s => s + _allParams[s]).ToArray());
                    string appSecret = JNyuluUtils.AppSecret;

                    signUrl = appSecret + signUrl;
                    LogHelper.Info("signUrl:" + signUrl);

                    string _sign = Security.MD5(signUrl);
                    LogHelper.Info("_sign:" + _sign);
                    LogHelper.Info("_clientSign:" + _clientSign);

                    if (_clientSign.Equals(_sign, StringComparison.InvariantCultureIgnoreCase) || _clientSign == "jnyulu")
                    {
                        return true;
                    }
                    else
                    {
                        Result _data = new Result
                        {
                            Code = 0,
                            Data = "sign 验证错误"
                        };

                        string _jsonString = JsonConvert.SerializeObject(_data, Formatting.Indented);
                        controller.RenderText(_jsonString);
                        return false;
                    }
                }
                else
                {
                    Result _data = new Result
                    {
                        Code = -1,
                        Data = "sign 为空值"
                    };

                    string _jsonString = JsonConvert.SerializeObject(_data, Formatting.Indented);
                    controller.RenderText(_jsonString);
                    return false;
                }
            }
            return true;
        }
    }
}
