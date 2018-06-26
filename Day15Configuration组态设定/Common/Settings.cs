using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day15Configuration组态设定.Common
{
    /// <summary>
    /// 用于定制配置文件强类型的方式 对应的对象
    /// </summary>
    //格式就是Configuration/settings.json对应格式
    public class Settings
    {
        public string[] SupportedCultures { get; set; }
        public CustomObject CustomObject { get; set; }
    }

    public class CustomObject
    {
        public Property1 Property { get; set; }
    }

    public class Property1
    {
        public int SubProperty1 { get; set; }
        public bool SubProperty2 { get; set; }
        public string SubProperty3 { get; set; }
    }
}
