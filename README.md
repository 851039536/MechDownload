## 通用上传下载库

封装HTTP请求zip上传下载

- 基于.NET 7 框架
- 使用RestSharp HTTP库(106.15.0)
- xunit库进行单元测试

### 版本

当前版本 : v1.0.0

### 概念

在理解本代码之前，需要了解以下几个关键概念：

- HTTP POST请求：HTTP协议中的一种请求方式，用于向服务器提交数据。
- zip压缩包：一种常见的文件压缩格式，可以将多个文件压缩成一个文件，以减小文件大小。
- 解压：将压缩文件还原成原始文件的过程。

### 执行下载

本代码包含一个名为的静态方法，该方法接受四个参数：`DownloadZip`

- `httpPath`：HTTP POST请求路径。
- `zipPath`：下载文件到指定路径，如为空则下载到当前程序集的执行路径（根目录）。
- `unPath`：解压到指定路径，如为空则解压到当前程序集的执行路径（根目录）。
- `downloadName`：文件名称，必须跟后台上传文件名匹配。

该方法返回一个布尔值，表示下载和解压是否成功。

#### 方法定义

```csharp
    /// <summary>
    /// zip压缩包下载
    /// 下载完成后自动执行解压动作,需传入解压路径unPath
    /// 解压完成自动根据downloadName字段删除zip包
    /// </summary>
    /// <param name="httpPath">HTTP POST请求路径</param>
    /// <param name="zipPath">下载文件到指定路径,如空则下载到当前程序集的执行路径(根目录)</param>
    /// <param name="unPath">解压到指定路径,如空则解压到当前程序集的执行路径(根目录)</param>
    /// <param name="downloadName">文件名称(必须跟后台上传文件名匹配)</param>
    /// <returns>bool</returns>
    public static bool DownloadZip(string httpPath, string zipPath, string unPath, string downloadName)
```

#### 单元测试

执行zip下载测试

```csharp
    [Fact]
    public void DownloadFileTest()
    {
        var data = ZipFiles.DownloadZip("http://10.55.2.25:20005/api/PostDownloadZIP",
            @"C:\Users\ch190006\Desktop\Test", @"C:\Users\ch190006\Desktop\Test"
            , "test");
        Assert.True(data);
    }
```

#### 调用示例

```csharp
string httpPath = "http://example.com/download.zip";
string zipPath = "C:\\Downloads\\";
string unPath = "C:\\Unzip\\";
string downloadName = "download.zip";

bool success = DownloadZip(httpPath, zipPath, unPath, downloadName);

if (success)
{
    Console.WriteLine("下载和解压成功！");
}
else
{
    Console.WriteLine("下载和解压失败！");
}
```



### 执行上传

该方法包含两个参数：HTTP POST请求路径和zip文件路径。它返回一个布尔值，表示上传是否成功

#### 方法定义

```csharp
    /// <summary>
    /// zip文件上传
    /// </summary>
    /// <param name="httpPath">HTTP POST请求路径</param>
    /// <param name="zipPath">指定zip文件上传的路径</param>
    /// <returns>bool</returns>
    public static bool UploadZip(string httpPath, string zipPath)
```

#### 单元测试

执行zip上传测试

```csharp
    [Fact]
    public void UploadZipTest()
    {
        var data = ZipFiles.UploadZip("http://10.55.2.25:20005/api/PostUploadloadFileTestItem",
            @"C:\Users\ch190006\Desktop\Test\test1\test.zip");
        Assert.True(data);
    }
```

#### 调用示例

```csharp
string httpPath = "http://example.com/upload";
string zipPath = "C:/example.zip";

bool result = UploadZip(httpPath, zipPath);

if (result)
{
    Console.WriteLine("上传成功！");
}
else
{
    Console.WriteLine("上传失败！");
}
```



### 测试结果

+![image-20230522170555039](C:\Users\ch190006\AppData\Roaming\Typora\typora-user-images\image-20230522170555039.png)