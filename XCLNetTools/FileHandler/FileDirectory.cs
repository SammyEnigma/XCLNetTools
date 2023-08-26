/*
һ��������Ϣ��
��ԴЭ�飺https://github.com/xucongli1989/XCLNetTools/blob/master/LICENSE
��Ŀ��ַ��https://github.com/xucongli1989/XCLNetTools
Create By: XCL @ 2012

 */

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace XCLNetTools.FileHandler
{
    /// <summary>
    /// �ļ�Ŀ¼������
    /// </summary>
    public static class FileDirectory
    {
        #region Ŀ¼����

        /// <summary>
        /// ���Ŀ¼�Ƿ�Ϊ��Ŀ¼����û���ļ��У�Ҳû���ļ���
        /// </summary>
        public static bool IsEmpty(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return true;
            }
            var files = Directory.GetFiles(path);
            var dirs = Directory.GetDirectories(path);
            return (null == files || files.Length == 0) && (null == dirs || dirs.Length == 0);
        }

        /// <summary>
        /// �ж�Ŀ¼�Ƿ����
        /// </summary>
        public static bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>
        /// ����Ŀ¼
        /// </summary>
        public static bool MakeDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                return true;
            }
            try
            {
                Directory.CreateDirectory(path);
            }
            catch
            {
                //
            }
            return Directory.Exists(path);
        }

        /// <summary>
        /// ���ļ�·������Ŀ¼
        /// </summary>
        public static void MakeDirectoryForFile(string path)
        {
            XCLNetTools.FileHandler.FileDirectory.MakeDirectory(XCLNetTools.FileHandler.ComFile.GetFileFolderPath(path));
        }

        /// <summary>
        /// ɾ��Ŀ¼��ɾ�����µ���Ŀ¼�����ļ�
        /// </summary>
        public static bool DelTree(string path)
        {
            if (DirectoryExists(path))
            {
                Directory.Delete(path, true);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ���ָ��Ŀ¼
        /// </summary>
        public static bool ClearDirectory(string rootPath)
        {
            //ɾ����Ŀ¼
            string[] subPaths = System.IO.Directory.GetDirectories(rootPath);
            foreach (string path in subPaths)
            {
                DelTree(path);
            }
            //ɾ���ļ�
            string[] files = XCLNetTools.FileHandler.ComFile.GetFolderFiles(rootPath);
            if (null != files && files.Length > 0)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    XCLNetTools.FileHandler.ComFile.DeleteFile(files[i]);
                }
            }
            return true;
        }

        /// <summary>
        /// ��ȡָ��Ŀ¼�µ������ļ����ļ�����Ϣ
        /// </summary>
        /// <param name="dirPath">Ҫ��ȡ��Ϣ��Ŀ¼·��</param>
        /// <param name="rootPath">��·�������ø�ֵ�󣬷��ص���Ϣʵ���н���������ڸø�·�������·����Ϣ��</param>
        /// <param name="webRootPath">web��·�����������ɸ��ļ����ļ��е�web·�������磺http://www.a.com/web/</param>
        /// <returns>�ļ���Ϣlist</returns>
        public static List<XCLNetTools.Entity.FileInfoEntity> GetFileList(string dirPath, string rootPath = "", string webRootPath = "")
        {
            List<XCLNetTools.Entity.FileInfoEntity> result = new List<Entity.FileInfoEntity>();
            if (!string.IsNullOrEmpty(dirPath))
            {
                dirPath = XCLNetTools.FileHandler.ComFile.MapPath(dirPath);
            }
            if (!string.IsNullOrEmpty(rootPath))
            {
                rootPath = XCLNetTools.FileHandler.ComFile.MapPath(rootPath);
            }

            if (string.IsNullOrEmpty(dirPath) || FileDirectory.IsEmpty(dirPath))
            {
                return result;
            }
            int idx = 1;

            XCLNetTools.Entity.FileInfoEntity tempFileInfoEntity = null;
            //�ļ���
            var directories = System.IO.Directory.EnumerateDirectories(dirPath);
            if (null != directories && directories.Any())
            {
                directories.ToList().ForEach(k =>
                {
                    var dir = new System.IO.DirectoryInfo(k);
                    if (dir.Exists)
                    {
                        tempFileInfoEntity = new Entity.FileInfoEntity();
                        tempFileInfoEntity.ID = idx++;
                        tempFileInfoEntity.Name = dir.Name;
                        tempFileInfoEntity.IsFolder = true;
                        tempFileInfoEntity.Path = k;
                        tempFileInfoEntity.RootPath = rootPath;
                        tempFileInfoEntity.RelativePath = ComFile.GetUrlRelativePath(rootPath, k);
                        tempFileInfoEntity.WebPath = webRootPath.TrimEnd('/') + "/" + tempFileInfoEntity.RelativePath;
                        tempFileInfoEntity.ModifyTime = dir.LastWriteTime;
                        tempFileInfoEntity.CreateTime = dir.CreationTime;
                        result.Add(tempFileInfoEntity);
                    }
                });
            }

            //�ļ�
            string[] files = XCLNetTools.FileHandler.ComFile.GetFolderFiles(dirPath);
            if (null != files && files.Length > 0)
            {
                files.ToList().ForEach(k =>
                {
                    var file = new System.IO.FileInfo(k);
                    if (file.Exists)
                    {
                        tempFileInfoEntity = new Entity.FileInfoEntity();
                        tempFileInfoEntity.ID = idx++;
                        tempFileInfoEntity.Name = file.Name;
                        tempFileInfoEntity.IsFolder = false;
                        tempFileInfoEntity.Path = k;
                        tempFileInfoEntity.RootPath = rootPath;
                        tempFileInfoEntity.RelativePath = ComFile.GetUrlRelativePath(rootPath, k);
                        tempFileInfoEntity.WebPath = webRootPath.TrimEnd('/') + "/" + tempFileInfoEntity.RelativePath;
                        tempFileInfoEntity.ModifyTime = file.LastWriteTime;
                        tempFileInfoEntity.CreateTime = file.CreationTime;
                        tempFileInfoEntity.Size = file.Length;
                        tempFileInfoEntity.ExtName = (file.Extension ?? "").Trim('.');
                        result.Add(tempFileInfoEntity);
                    }
                });
            }

            return result;
        }

        /// <summary>
        /// ����ָ���ļ���·���ĸ��ļ��е�ַ���磺"C:\a\b\c\d\" ---> "C:\a\b\c"
        /// </summary>
        public static string GetDirParentPath(string dirPath)
        {
            if (string.IsNullOrWhiteSpace(dirPath))
            {
                return string.Empty;
            }
            return Path.GetDirectoryName(Path.GetDirectoryName(dirPath.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar));
        }

        #endregion Ŀ¼����

        #region �ļ�����

        /// <summary>
        /// ����һ���ļ�
        /// </summary>
        public static bool CreateTextFile(string path)
        {
            var info = new FileInfo(path);
            if (info.Exists)
            {
                return true;
            }
            try
            {
                MakeDirectoryForFile(path);
                //������̲����ڣ�using �����쳣
                using (var fs = info.Create())
                {
                    //
                }
            }
            catch
            {
                //
            }
            return System.IO.File.Exists(path);
        }

        /// <summary>
        /// ���ļ���׷������
        /// </summary>
        /// <param name="filePathName">�ļ���</param>
        /// <param name="writeWord">׷������</param>
        public static void AppendText(string filePathName, string writeWord)
        {
            AppendText(filePathName, writeWord, System.Text.Encoding.Default);
        }

        /// <summary>
        /// ���ļ���׷������
        /// </summary>
        /// <param name="filePathName">�ļ���</param>
        /// <param name="writeWord">׷�ӵ�����</param>
        /// <param name="encode">����</param>
        public static void AppendText(string filePathName, string writeWord, System.Text.Encoding encode)
        {
            CreateTextFile(filePathName);
            System.IO.File.AppendAllText(filePathName, writeWord, encode);
        }

        /// <summary>
        /// ��ȡ�ı��ļ�������ݣ��Զ�ʶ����룬���ԭ������ascii����Ĭ����utf8��ȡ��
        /// </summary>
        /// <param name="filePathName">·��</param>
        /// <returns>�ļ�����</returns>
        public static string ReadFileData(string filePathName)
        {
            if (!System.IO.File.Exists(filePathName))
            {
                return "";
            }
            var encode = XCLNetTools.FileHandler.ComFile.GetFileEncoding(filePathName);
            if (encode == System.Text.Encoding.ASCII)
            {
                encode = System.Text.Encoding.UTF8;
            }
            return System.IO.File.ReadAllText(filePathName, encode) ?? "";
        }

        /// <summary>
        /// ��·����д�����ݣ�����·���е��������ݣ����·�������ڣ����Զ�������·������������� ascii �Ұ��� Unicode �ַ�����Ĭ���� utf8 д�룩
        /// </summary>
        public static void WriteFileData(string filePathName, string content, System.Text.Encoding encode)
        {
            if (encode == System.Text.Encoding.ASCII && XCLNetTools.Encode.Unicode.HasUnicode(content))
            {
                encode = System.Text.Encoding.UTF8;
            }
            System.IO.File.WriteAllText(filePathName, content, encode);
        }

        /// <summary>
        /// ɾ���ļ�
        /// </summary>
        /// <param name="absoluteFilePath">�ļ����Ե�ַ</param>
        /// <returns>true:ɾ���ļ��ɹ�,false:ɾ���ļ�ʧ��</returns>
        public static bool FileDelete(string absoluteFilePath)
        {
            try
            {
                FileInfo objFile = new FileInfo(absoluteFilePath);
                if (objFile.Exists)//�������
                {
                    //ɾ���ļ�.
                    objFile.Delete();
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        #endregion �ļ�����

        #region ����

        /// <summary>
        /// ��ȡ��ǰ����ϵͳ���������·�����磺C:\Users\XCL\Desktop
        /// </summary>
        public static string GetDesktopPath()
        {
            return System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        }

        #endregion ����
    }
}