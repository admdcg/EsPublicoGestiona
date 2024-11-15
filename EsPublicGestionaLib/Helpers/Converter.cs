using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsPublicGestionaLib.Helpers
{
    public static class Converter
    {
        public static void ConverBase64ToFile(string base64BinaryStr, string filename)
        {
            byte[] bytes = Convert.FromBase64String(base64BinaryStr);
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            System.IO.FileStream stream = new FileStream(filename, FileMode.CreateNew);
            System.IO.BinaryWriter writer =
                new BinaryWriter(stream);
            writer.Write(bytes, 0, bytes.Length);
            writer.Close();
        }
    }
}
