using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.ResponseCompression;

namespace Day29Compression压缩.Common
{
    public class CustomCompressionProvider : ICompressionProvider
    {
        public string EncodingName => "CustomPression";

        public bool SupportsFlush => true;

        public Stream CreateStream(Stream outputStream)
        {
            //将流进行封装压缩 再返回 自己实现
            return outputStream;
        }
    }
}
