using GeneralUpDownload_FrameworkV452;
using Xunit;

namespace xUnit_GeneralUpDownload_FrameworkV452
{
    public class Tests
    {
        [Fact]
        public void DownloadFileTest()
        {
            var data = ZipFiles.DownloadZip("http://10.55.2.25:20005/api/PostDownloadZIP",
                @"C:\Users\ch190006\Desktop\Test", @"C:\Users\ch190006\Desktop\Test"
                , "test");
            Assert.True(data);
        }

        [Fact]
        public void UploadZipTest()
        {
            var data = ZipFiles.UploadZip("http://10.55.2.25:20005/api/PostUploadloadFileTestItem",
                @"C:\Users\ch190006\Desktop\Test\test1\test.zip");
            Assert.True(data);
        }
    }
}