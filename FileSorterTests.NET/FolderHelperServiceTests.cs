namespace FileSorterTests.NET
{
    public class FolderHelperServiceTests
    {
        [Theory]
        [InlineData("", true)]
        [InlineData(".", true)]
        [InlineData("", false)]
        [InlineData(".", false)]
        public void GetFolder_InputIsEmpty_Custom_ReturnFILE(string value, bool custom)
        {
            IFolderHelperService folderHelperService = new FolderHelperSerivce();
            var folder = folderHelperService.GetFolder(custom, value);

            Assert.Equal("FILE", folder);
        }

        [Theory]
        [InlineData(".mp4", true, "Videos")]
        [InlineData(".MOV", true, "Videos")]
        [InlineData(".AvI", true, "Videos")]
        [InlineData(".mp4", false, "MP4")]
        [InlineData("ODF", true, "Documents")]
        [InlineData(".pdf", true, "Documents")]
        [InlineData(".pPt", true, "Documents")]
        [InlineData(".xlsx", false, "XLSX")]
        public void GetFolder_InputIsFileType_ReturnsFolder(string value, bool custom, string expectedResult)
        {
            IFolderHelperService folderHelperService = new FolderHelperSerivce();
            var folder = folderHelperService.GetFolder(custom, value);

            Assert.Equal(expectedResult, folder);
        }
    }
}