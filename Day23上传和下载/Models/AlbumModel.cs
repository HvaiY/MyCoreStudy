using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Day23上传和下载.Models
{
    public class AlbumModel
    {
        //多资料文件上传(表单＋图片) 使用对象封装
        public string Title { get; set; }

        public DateTime Date { get; set; }

        public List<IFormFile> Photos { get; set; }

    }
}
