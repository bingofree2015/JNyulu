using System;
using System.IO;
using System.Reflection;

using IBatisNet.Common.Utilities;
using IBatisNet.DataAccess;
using IBatisNet.DataAccess.Configuration;
using IBatisNet.DataAccess.Interfaces;
using JNyuluSoft.Util;

namespace JNyuluSoft.Service
{
    /// <summary>
    /// Summary description for ServiceConfig.
    /// </summary>
    public class ServiceConfig
    {
        static private object _synRoot = new Object();
        static private ServiceConfig _instance;

        private IDaoManager _daoManager = null;

        /// <summary>
        /// Remove public constructor. prevent instantiation.
        /// </summary>
        private ServiceConfig() { }

        static public ServiceConfig GetInstance()
        {
            if (_instance == null)
            {
                lock (_synRoot)
                {
                    if (_instance == null)
                    {
                        try
                        {
                            ConfigureHandler _handler = new ConfigureHandler(ServiceConfig.Reset);
                            DomDaoManagerBuilder _builder = new DomDaoManagerBuilder();

                            //_builder.Configure();
                            _builder.ConfigureAndWatch("dao.config", _handler); 

                            _instance = new ServiceConfig();
                            _instance._daoManager = IBatisNet.DataAccess.DaoManager.GetInstance("SqlMapDao");
                        }
                        catch (Exception e)
                        {
                            LogHelper.Error(e.Message);
                        }
                    }
                }
            }
            return _instance;
        }

        static public void Reset(object obj)
        {
            _instance = null;
        }

        public IDaoManager DaoManager
        {
            get
            {
                return _daoManager;
            }
        }

        public IDaoManager GetDaoManager(string contextName)
        {
            return IBatisNet.DataAccess.DaoManager.GetInstance(contextName);
        }
    }
}
