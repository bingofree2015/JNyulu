using System;
using System.Collections.Generic;
using System.Text;

namespace JNyuluSoft.Service
{
    [Serializable]
    public class  PermissionType
    {
        public const string Essential  = "00100000000000000000";
        public const string Student    = "00010000000000000000";
        public const string Article    = "00000100000000000000";
    }
}
