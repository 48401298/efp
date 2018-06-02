using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace LAF.Common.Util
{
    /// <summary>
    /// 压缩工具
    /// </summary>
    public class CompressionTool
    {
        #region 压缩文件

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="filePath">原文件</param>
        /// <param name="zipPath">压缩文件</param>
        public static void Compress(string filePath, string zipPath)
        {
            FileStream sourceFile = File.OpenRead(filePath);
            FileStream destinationFile = File.Create(zipPath);
            byte[] buffer = new byte[sourceFile.Length];
            GZipStream zip = null;
            try
            {
                sourceFile.Read(buffer, 0, buffer.Length);
                zip = new GZipStream(destinationFile, CompressionMode.Compress);
                zip.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                zip.Close();
                sourceFile.Close();
                destinationFile.Close();
            }
        }

        #endregion

        #region 解压文件

        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="zipPath">压缩文件</param>
        /// <param name="filePath">解压后的文件</param>
        public static void Decompress(string zipPath, string filePath)
        {
            GZipStream unzip = null;
            FileStream sourceFile = File.OpenRead(zipPath);

            string path = filePath.Replace(Path.GetFileName(filePath), "");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            FileStream destinationFile = File.Create(filePath);

            byte[] buffer = new byte[destinationFile.Length];
            try
            {
                unzip = new GZipStream(sourceFile, CompressionMode.Decompress, true);
                int numberOfBytes = unzip.Read(buffer, 0, buffer.Length);
                destinationFile.Write(buffer, 0, numberOfBytes);

                int len;
                int position = 0;
                byte[] buf = new byte[1024];

                while ((len = unzip.Read(buf, 0, buf.Length)) > 0)
                {
                    destinationFile.Write(buf, 0, len);
                    position += len;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sourceFile.Close();
                destinationFile.Close();
                unzip.Close();
            }
        }

        #endregion

        /// <summary>
        /// 压缩字节数组
        /// </summary>
        /// <param name="bytes">压缩前</param>
        /// <returns>压缩后</returns>
        public static byte[] Compress(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                GZipStream compress = new GZipStream(ms, CompressionMode.Compress);

                compress.Write(bytes, 0, bytes.Length);
                compress.Close();
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 解压缩字节数组
        /// </summary>
        /// <param name="bytes">解压前</param>
        /// <returns>解压后</returns>
        public static byte[] Decompress(byte[] bytes)
        {
            using (MemoryStream tempMs = new MemoryStream())
            {
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    GZipStream Decompress = new GZipStream(ms, CompressionMode.Decompress); Decompress.CopyTo(tempMs);
                    Decompress.Close();
                    return tempMs.ToArray();
                }
            }
        }

        /// <summary>
        /// 压缩字符串
        /// </summary>
        /// <param name="str">压缩前</param>
        /// <returns>压缩后</returns>
        public static string Compress(string str)
        {
            string compressString = "";  
            byte[] compressBeforeByte = Encoding.GetEncoding("UTF-8").GetBytes(str);  
            byte[] compressAfterByte = Compress(compressBeforeByte);     
            compressString = Convert.ToBase64String(compressAfterByte);  
            return compressString; 

        }

        /// <summary>
        /// 解压字符串
        /// </summary>
        /// <param name="str">解压前</param>
        /// <returns>解压后</returns>
        public static string Decompress(string str)
        {
            string compressString = "";  
            byte[] compressBeforeByte = Convert.FromBase64String(str);  
            byte[] compressAfterByte = Decompress(compressBeforeByte);  
            compressString = Encoding.GetEncoding("UTF-8").GetString(compressAfterByte);  
            return compressString; 

        }
    }
}
