using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace BooksMagazine.Helpers
{
    public static class Methods
    {
        public static IHostingEnvironment hostingEnvironment;
        public static void CreateItem(IFormFile item)
        {
            item.CopyTo(new FileStream(Path.Combine(Path.Combine(hostingEnvironment.WebRootPath, "images"), item.FileName), FileMode.Create));
        }
        public static void CreateItem(IFormFile item , bool type)
        {
            if(type)
                item.CopyTo(new FileStream(Path.Combine(Path.Combine(hostingEnvironment.WebRootPath, "Books"), item.FileName), FileMode.Create));
            else
                item.CopyTo(new FileStream(Path.Combine(Path.Combine(hostingEnvironment.WebRootPath, "images"), item.FileName), FileMode.Create));
        }
        public static void DeleteItem(string link)
        {
            System.IO.File.Delete(Path.Combine(hostingEnvironment.WebRootPath, "images" + link));
        }
    }
}
