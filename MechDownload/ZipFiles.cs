using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace MechDownload
{
    /// <summary>
    /// zip帮助类
    /// </summary>
    public abstract class ZipFiles
    {
        /// <summary>
        /// zip压缩包下载(量产模式)
        /// 下载完成后自动执行解压动作,需传入解压路径unPath
        /// 解压完成自动根据downloadName字段删除zip包
        /// </summary>
        /// <param name="httpPath">HTTP POST请求路径</param>
        /// <param name="zipPath">下载文件到指定路径,如空则下载到当前程序集的执行路径(根目录)</param>
        /// <param name="unPath">解压到指定路径,如空则解压到当前程序集的执行路径(根目录)</param>
        /// <param name="downloadName">文件名称(必须跟后台上传文件名匹配)</param>
        /// <returns>bool</returns>
        public static bool DownloadZip(string httpPath, string zipPath, string unPath, string downloadName)
        {
            const string testName = "TestItem";
            // const string downloadName = "CopyTest";
            //检测下载解压的文件是否存在
            if (IsExistDirectory(zipPath + @"\" + downloadName))
            {
                Directory.Delete((zipPath + @"\" + downloadName), true);
            }

            //下载指定路径
            zipPath += @"\" + downloadName + ".zip";
            // 定义一个字符串变量，用于存储 JSON 格式的数据
            var strContent = "{\"TestName\":\"" + testName + "\",\"DownloadName\":\"" + downloadName + "\"}";

            // 检查目录是否存在，如果不存在则创建目录
            EnsureDirectoryExists(Path.GetDirectoryName(zipPath) ?? string.Empty);

            // 发送HTTP POST请求，下载ZIP文件
            var data = HttpPost(httpPath,
                strContent, "POST", zipPath);

            //下载成功
            if (data)
            {
                // 解压文件
                ExtractZipFile(zipPath, unPath + @"\" + downloadName);
            }
            else
            {
                return false;
            }

            if (File.Exists(zipPath))
            {
                File.Delete(zipPath);
            }

            return true;
        }

        /// <summary>
        /// zip压缩包下载(工程模式)
        /// 下载完成后自动执行解压动作,需传入解压路径unPath
        /// 解压完成自动根据downloadName字段删除zip包
        /// </summary>
        /// <param name="httpPath">HTTP POST请求路径</param>
        /// <param name="zipPath">下载文件到指定路径,如空则下载到当前程序集的执行路径(根目录)</param>
        /// <param name="unPath">解压到指定路径,如空则解压到当前程序集的执行路径(根目录)</param>
        /// <param name="downloadName">文件名称(必须跟后台上传文件名匹配)</param>
        /// <returns>bool</returns>
        public static bool DownloadEngineeringModeZip(string httpPath, string zipPath, string unPath, string downloadName)
        {
            const string testName = "EngineeringMode";
            //检测下载解压的文件是否存在
            if (IsExistDirectory(zipPath + @"\" + downloadName))
            {
                Directory.Delete((zipPath + @"\" + downloadName), true);
            }

            //下载指定路径
            zipPath += @"\" + downloadName + ".zip";
            // 定义一个字符串变量，用于存储 JSON 格式的数据
            var strContent = "{\"TestName\":\"" + testName + "\",\"DownloadName\":\"" + downloadName + "\"}";
            // 检查目录是否存在，如果不存在则创建目录
            EnsureDirectoryExists(Path.GetDirectoryName(zipPath) ?? string.Empty);
            // 发送HTTP POST请求，下载ZIP文件
            var data = HttpPost(httpPath,
                strContent, "POST", zipPath);
            //下载成功
            if (data)
            {
                // 解压文件
                ExtractZipFile(zipPath, unPath);
            }
            else
            {
                return false;
            }

            if (File.Exists(zipPath))
            {
                File.Delete(zipPath);
            }

            return true;
        }

        /// <summary>
        /// zip压缩包下载(el版本)
        /// 下载完成后自动执行解压动作,需传入解压路径unPath
        /// 解压完成自动根据downloadName字段删除zip包
        /// </summary>
        /// <param name="input">为object数组形式,有四个接收字段[httpPath:HTTP POST请求路径,
        /// zipPath:下载文件到指定路径,如空则下载到当前程序集的执行路径,
        /// unPath:解压到指定路径,如空则解压到当前程序集的执行路径,
        /// downloadName:文件名称(必须跟后台上传文件名匹配)]
        /// </param>
        /// <returns>object</returns>
        public async Task<object> ElDownloadZip(object[] input)
        {
            // 根据传入参数的顺序，获取对应的值
            var httpPath = input[0].ToString();
            var zipPath = input[1].ToString();
            var unPath = input[2].ToString();
            var downloadName = input[3].ToString();

            const string testName = "TestItem";
            // const string downloadName = "CopyTest";

            //检测下载解压的文件是否存在
            if (IsExistDirectory(zipPath + @"\" + downloadName))
            {
                Directory.Delete((zipPath + @"\" + downloadName), true);
            }

            //下载指定路径
            zipPath += @"\" + downloadName + ".zip";

            // 定义一个字符串变量，用于存储 JSON 格式的数据
            var strContent = "{\"TestName\":\"" + testName + "\",\"DownloadName\":\"" + downloadName + "\"}";

            // 检查目录是否存在，如果不存在则创建目录
            EnsureDirectoryExists(Path.GetDirectoryName(zipPath) ?? string.Empty);

            // 发送HTTP POST请求，下载ZIP文件
            var data = HttpPost(httpPath,
                strContent, "POST", zipPath);


            //下载成功
            if (data)
            {
                // 解压文件
                ExtractZipFile(zipPath, unPath + @"\" + downloadName);
            }
            else
            {
                return Task.FromResult<object>("false");
            }

            if (File.Exists(zipPath))
            {
                File.Delete(zipPath);
            }

            return Task.FromResult<object>("true");
        }

        /// <summary>
        /// zip文件上传(el版本)
        /// </summary>
        /// <param name="input">为object数组形式,有两个接收字段[httpPath:HTTP POST请求路径,zipPath:指定zip文件上传的路径]</param>
        /// <returns>object</returns>
        public async Task<object> ElUploadZip(object[] input)
        {
            // 根据传入参数的顺序，获取对应的值
            var httpPath = input[0].ToString();
            var zipPath = input[1].ToString();

            //指定上传文件的API地址
            var client = new RestClient(httpPath);
            //指定请求方式为POST
            var request = new RestRequest(Method.POST)
            {
                // 指定请求格式为Json
                RequestFormat = DataFormat.Json
            };
            // 添加上传的文件
            request.AddFile("file", zipPath);
            // 添加请求参数
            request.AddParameter("pictureName", "false");
            // 执行请求并获取响应内容
            var data = client.Execute(request);
            if (data.StatusCode.ToString() == "OK")
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// zip文件上传
        /// </summary>
        /// <param name="httpPath">HTTP POST请求路径</param>
        /// <param name="zipPath">指定zip文件上传的路径</param>
        /// <returns>bool</returns>
        public static bool UploadZip(string httpPath, string zipPath)
        {
            //指定上传文件的API地址
            var client = new RestClient(httpPath);
            //指定请求方式为POST
            var request = new RestRequest(Method.POST)
            {
                // 指定请求格式为Json
                RequestFormat = DataFormat.Json
            };
            // 添加上传的文件
            request.AddFile("file", zipPath);
            // 添加请求参数
            request.AddParameter("pictureName", "false");
            // 执行请求并获取响应内容
            var data = client.Execute(request);
            if (data.StatusCode.ToString() == "OK")
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="zipFilePath">要解压的zip</param>
        /// <param name="extractPath">解压到指定路径</param>
        private static void ExtractZipFile(string zipFilePath, string extractPath)
        {
            ZipFile.ExtractToDirectory(zipFilePath, extractPath);
        }

        /// <summary>
        /// 检查目录是否存在，如果不存在则创建目录
        /// </summary>
        /// <param name="directoryPath"></param>
        private static void EnsureDirectoryExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        /// <summary>
        /// 判断目录是否存在
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns>bool</returns>
        private static bool IsExistDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        /// <summary>
        /// 通过HTTP下载
        /// </summary>
        /// <param name="httpUrl">HTTP请求路径</param>
        /// <param name="writeData">JSON格式属性</param>
        /// <param name="method">请求</param>
        /// <param name="path">下载到指定路径</param>
        /// <returns>bool</returns>
        private static bool HttpPost(string httpUrl, string writeData, string method, string path)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(httpUrl);
                //字符串转换为字节码
                var bs = Encoding.UTF8.GetBytes(writeData);
                //参数类型，这里是json类型
                //还有别的类型如"application/x-www-form-urlencoded"，不过我没用过(逃
                httpWebRequest.ContentType = "application/json";
                //参数数据长度
                httpWebRequest.ContentLength = bs.Length;
                //设置请求类型
                httpWebRequest.Method = method;
                //设置超时时间
                httpWebRequest.Timeout = 20000;
                //将参数写入请求地址中
                httpWebRequest.GetRequestStream().Write(bs, 0, bs.Length);
                //发送请求
                var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                //流对象使用完后自动关闭
                using var stream = httpWebResponse.GetResponseStream();
                //文件流，流信息读到文件流中，读完关闭
                using var fs = File.Create(path);
                //建立字节组，并设置它的大小是多少字节
                var bytes = new byte[102400];
                var n = 1;
                while (n > 0)
                {
                    //一次从流中读多少字节，并把值赋给Ｎ，当读完后，Ｎ为０,并退出循环
                    if (stream != null) n = stream.Read(bytes, 0, 10240);
                    fs.Write(bytes, 0, n); //将指定字节的流信息写入文件流中
                }

                return true;
            }
            catch (Exception ex)
            {
                File.AppendAllText($@".\Log\错误信息{DateTime.Now:MM_dd}.txt", $"{DateTime.Now}\r\n{ex}\r\n\r\n",
                    Encoding.UTF8);
                return false;
            }
        }
    }
}