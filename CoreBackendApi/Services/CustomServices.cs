using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CoreBackendApi.Services
{
    //用于自定义注入测试
    public interface IMailService
    {
        void Send(string subject, string msg);
    }

    public class LocalMailService : IMailService
    {
        private string _mailTo = Startup.Configuration["mailSettings:mailToAddress"];//数据来自配置文件
        private string _mailFrom = Startup.Configuration["mailSettings:mailFromAddress"];


        public void Send(string subject, string msg)
        {
            Debug.WriteLine(subject + "  " + msg);
            Debug.WriteLine($"从{_mailFrom}给{_mailTo}通过{nameof(LocalMailService)}发送了邮件");
        }
    }
    public class CloudMailService : IMailService //设定为非Dbug 模式的测试 
    {
        #region Debug 只有的Debug模式下才有效 这里用Logging来查看效果
        //private readonly string _mailTo = "admin@qq.com";
        //private readonly string _mailFrom = "noreply@alibaba.com";

        //public void Send(string subject, string msg)
        //{
        //    Debug.WriteLine($"从{_mailFrom}给{_mailTo}通过{nameof(CloudMailService)}发送了邮件");
        //} 
        private readonly string _mailTo = "admin@qq.com";
        private readonly string _mailFrom = "noreply@alibaba.com";
        private readonly ILogger<CloudMailService> _logger;

        public CloudMailService(ILogger<CloudMailService> logger)
        {
            _logger = logger;
        }

        public void Send(string subject, string msg)
        {
            _logger.LogInformation(subject + "  " + msg);
            _logger.LogInformation($"从{_mailFrom}给{_mailTo}通过{nameof(CloudMailService)}发送了邮件");
        }
        #endregion
    }
}
