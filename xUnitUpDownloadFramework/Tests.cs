﻿using UpDownloadFramework;
using Xunit;

namespace xUnitUpDownloadFramework
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

        //PostUploadloadFileEngineeringMode 工程模式
        //PostUploadloadFileTestItem 量产模式
        [Fact]
        public void UploadZipTest()
        {
            // var data = ZipFiles.UploadZip("http://10.55.2.25:20005/api/PostUploadloadFileEngineeringMode",
            //     @"C:\Users\ch190006\Desktop\Test\test1\test.zip");    
            
           // var data = ZipFiles.UploadZip("http://10.55.2.25:20005/api/PostUploadloadFileEngineeringMode",
              //  @"D:\sw\model\19.HDT657\HDT657_DLL\MerryDllFramework\bin\TEST\HDT657.zip");      
            
            var data = ZipFiles.UploadZip("http://10.55.2.25:8098/api/v1/common/file-save",
                @"C:\Users\ch190006\Desktop\Test\test1\test.zip");
            Assert.True(data);
        }
    }
}