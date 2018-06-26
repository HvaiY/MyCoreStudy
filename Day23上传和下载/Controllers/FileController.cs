using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Day23上传和下载.Filters;
using Day23上传和下载.Helpers;
using Day23上传和下载.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Day23上传和下载.Controllers
{

    /// <summary>
    /// 简单小文件上传和下载
    /// </summary>

    [Route("api/[controller]")]
    public class FileController : Controller
    {
        private static readonly Dictionary<string, string> _contentTypes = new Dictionary<string, string>
        {
            {".png", "image/png"},
            {".jpg", "image/jpeg"},
            {".jpeg", "image/jpeg"},
            {".gif", "image/gif"}
        };

        private readonly string _folder;

        public FileController(IHostingEnvironment env)
        {
            _folder = $@"{env.WebRootPath}\UploadFolder"; //文件夹所在目录为静态文件目录
        }
        [HttpPost] //测试文件提交文件过来的地址为静态文件地址 //单文件的话 Upload的参数为： IFormFile
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            var size = files.Sum(f => f.Length);

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var path = $@"{_folder}\{file.FileName}";
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }

            return Ok(new { count = files.Count, size });
        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> Download(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return NotFound();
            }

            var path = $@"{_folder}\{fileName}";
            var memoryStream = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memoryStream);
            }
            memoryStream.Seek(0, SeekOrigin.Begin);

            // 回傳檔案到 Client 需要附上 Content Type，否則瀏覽器會解析失敗。
            return new FileStreamResult(memoryStream, _contentTypes[Path.GetExtension(path).ToLowerInvariant()]);
        }
        [Route("album")]
        [HttpPost] //页面 请求静态页面：http://localhost:50166/Upload.html
        public async Task<IActionResult> Album(AlbumModel model)
        {
            foreach (var file in model.Photos)
            {
                var path = $@"{_folder}\{file.FileName}";
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            return Ok(new { title = model.Title, date = model.Date, photoCount = model.Photos.Count });
        }
        [Route("bigalbum")]
        [HttpPost]
        [DisableFormValueModelBindingFilter]
        public async Task<IActionResult> BigAlbum()
        {
            var photoCount = 0;
            //自己实现的Request的扩展方法
            var formValueProvider = await Request.StreamFile((file) =>
            {
                photoCount++;
                return System.IO.File.Create($@"{_folder}\{file.FileName}");
            });

            var model = new AlbumModel
            {
                Title = formValueProvider.GetValue("title").ToString(),
                Date = Convert.ToDateTime(formValueProvider.GetValue("date").ToString())
            };

            // ...

            return Ok(new
            {
                title = model.Title,
                date = model.Date.ToString("yyyy/MM/dd"),
                photoCount = photoCount
            });
        }
    }
}