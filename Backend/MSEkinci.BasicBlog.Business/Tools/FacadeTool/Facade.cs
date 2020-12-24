using Microsoft.Extensions.Caching.Memory;
using MSEkinci.BasicBlog.Business.Tools.LogTool;

namespace MSEkinci.BasicBlog.Business.Tools.FacadeTool
{
    public class Facade : IFacade
    {
        public IMemoryCache MemoryCache { get; set; }
        public ICustomLogger CustomLogger { get; set; }

        public Facade(IMemoryCache memoryCache, ICustomLogger customLogger)
        {
            MemoryCache = memoryCache;
            CustomLogger = customLogger;
        }
    }
}
