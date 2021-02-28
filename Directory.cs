using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Threading;

namespace testTask
{
    public class Directory
    {
        public void bypassDirectory(DirectoryInfo root)
        {
            FileInfo[] files;
            DirectoryInfo[] subDirs;
                        
            files = root.GetFiles("*.*");

            if(files != null)
            {
                foreach (FileInfo fi in files)
                {
                    addToArchThread(fi.DirectoryName, fi.Name); 
                }

                subDirs = root.GetDirectories();

                foreach(DirectoryInfo dirInfo in subDirs)
                {
                    bypassDirectory(dirInfo);
                }
            }
        }

        private static void addToArchive(string directoryName, string fileName)
        {
            using (ZipOutputStream stream = new ZipOutputStream(File.Create(Path.Combine(directoryName, fileName + ".zip"))))
            {
                stream.SetLevel(9);
                
                byte[] buffer = new byte[4096];
                var entry = new ZipEntry(fileName);
                entry.DateTime = DateTime.Now;
                stream.PutNextEntry(entry);

                using (FileStream fs = File.OpenRead(Path.Combine(directoryName, fileName)))
                {
                    int sourceBytes;
                    do
                    {
                        sourceBytes = fs.Read(buffer, 0, buffer.Length);
                        stream.Write(buffer, 0, sourceBytes);
                    }
                    while (sourceBytes > 0);
                }
                //Автор библиотеки указал, что эти действия очень важны
                stream.Finish();
                stream.Close();
            }
        }

        private void addToArchThread(string fileName, string archiveName)
        {
            ThreadStart threadStart = new ThreadStart(()=>addToArchive(fileName, archiveName));
            Thread th = new Thread(threadStart);
            th.Start();
        }
    }
}
