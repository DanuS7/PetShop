using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;

public static class FileHelper
{
    public static byte[] ConvertToByteArray(HttpPostedFileBase file)
    {
        if (file == null || file.ContentLength == 0)
            return null;

        using (var memoryStream = new MemoryStream())
        {
            file.InputStream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
