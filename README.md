## HTTP上传下载库

封装HTTP请求zip上传下载

GeneralUpDownload

- 适应版本:.NET7 , FrameworkV4.5.2
- 使用RestSharp HTTP库
- xunit库进行单元测试

### 版本

当前版本 : v1.0.0

### 概念

了解以下几个关键概念：

- HTTP POST请求：HTTP协议中的一种请求方式，用于向服务器提交数据。
- zip压缩包：一种常见的文件压缩格式，可以将多个文件压缩成一个文件，以减小文件大小。
- 解压：将压缩文件还原成原始文件的过程。

### 文件下载

本代码包含一个名为的静态方法，该方法接受四个参数：`DownloadZip`

- `httpPath`：HTTP POST请求路径。
- `zipPath`：下载文件到指定路径，如为空则下载到当前程序集的执行路径（根目录）。
- `unPath`：解压到指定路径，如为空则解压到当前程序集的执行路径（根目录）。
- `downloadName`：文件名称，必须跟后台上传文件名匹配。

该方法返回一个布尔值，表示下载和解压是否成功。

#### DownloadZip

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

使用**DownloadZip**方法执行下载

```csharp
[Fact]
public void DownloadFileTest()
{
    var data = ZipFiles.DownloadZip("http://10.55.2.25:20005/api/PostDownloadZIP", @"C:\Users\ch190006\Desktop\Test", @"C:\Users\ch190006\Desktop\Test", "test");
    Assert.True(data);
}
```

#### ElDownloadZip

(el版本)

```csharp
/// <summary>
/// zip压缩包下载(el版本)
/// 下载完成后自动执行解压动作,需传入解压路径unPath
/// 解压完成自动根据downloadName字段删除zip包
/// </summary>
/// <param name="input">为object数组形式,有四个接收字段[httpPath:HTTP POST请求路径,
/// zipPath:下载文件到指定路径,如空则下载到当前程序集的执行路径,
/// unPath:解压到指定路径,如空则解压到当前程序集的执行路径,
/// downloadName:文件名称(必须跟后台上传文件名匹配)]
///按顺序传入即可
/// </param>
/// <returns>bool</returns>
public async Task < object > ElDownloadZip(object[] input)
```

使用**ElDownloadZip**方法在前端执行下载

```js
//下载到指定路径
const zipDownload = ref('');
//解压到指定路径
const uNzip = ref('');
//解压后的文件夹名称(跟上传路名称径一致)
const zipName = ref('');
//上传接口
const DownloadPath = ref('http://10.55.2.25:20005/api/PostDownloadZIP');

const DownloadZip = () => {
const input = [DownloadPath.value, zipDownload.value, uNzip.value, zipName.value];
  ElDownloadZip(input, function (err: any, result: any) {
    console.log(err);
    console.log(result.Result);
    if (result.Result === 'true') {
      ElMessage({
        message: '文件下载完成',
        type: 'success'
      });
      return;
    }
  });
};
```

### 文件上传

上传之前先压缩.zip

该方法包含两个参数：HTTP POST请求路径和zip文件路径。它返回一个布尔值，表示上传是否成功

#### UploadZip

```csharp
/// <summary>
/// zip文件上传
/// </summary>
/// <param name="httpPath">HTTP POST请求路径</param>
/// <param name="zipPath">指定zip文件上传的路径</param>
/// <returns>bool</returns>
public static bool UploadZip(string httpPath, string zipPath)
```

使用**UploadZip**执行zip文件上传

```csharp
[Fact]
public void UploadZipTest()
{
    var data = ZipFiles.UploadZip("http://10.55.2.25:20005/api/PostUploadloadFileTestItem", @"C:\Users\ch190006\Desktop\Test\test1\test.zip");
    Assert.True(data);
}
```

#### ElUploadZip

(el版本) 

```csharp
/// <summary>
/// zip文件上传(el版本)
/// </summary>
/// <param name="input">为object数组形式,有两个接收字段[httpPath:HTTP POST请求路径,zipPath:指定zip文件上传的路径]</param>
/// <returns></returns>
public async Task < object > ElUploadZip(object[] input)
```

使用**ElUploadZip**方法执行上传

```js
//上传路径
const zipUpPath = ref('');
//上传接口
const uploadPath = ref('http://10.55.2.25:20005/api/PostUploadloadFileTestItem');

const UploadZip = () => {
const path = [uploadPath.value, zipUpPath.value];

  ElUploadZip(path, function (err: any, result: any) {
    if (result) {
      ElMessage({
        message: 'zip文件上传成功',
        type: 'success'
      });
      return;
    }
    console.log(err);
  });
};
```

