using Microsoft.Extensions.Caching.Memory;
using MSEkinci.BasicBlog.Business.Tools.LogTool;

namespace MSEkinci.BasicBlog.Business.Tools.FacadeTool
{
    public interface IFacade
    {
        public IMemoryCache MemoryCache { get; set; }
        public ICustomLogger CustomLogger { get; set; }
    }
}
