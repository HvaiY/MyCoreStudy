using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Day15Configuration组态设定.Common
{
    /// <summary>
    /// 用于自定义的配置资源类
    /// </summary>
    public class CustomConfigurationSource:IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
           return new CustomConfigurationProvider();
        }
    }
    public class CustomConfigurationProvider : ConfigurationProvider
    {
        public override void Load()
        {
            Data = new Dictionary<string, string>
            {
                { "Custom:Site:Name", "John Wu's Blog" },
                { "Custom:Site:Domain", "blog.johnwu.cc" }
            };
        }
    }
}
