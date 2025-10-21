/*
һ��������Ϣ��
��ԴЭ�飺https://github.com/xucongli1989/XCLNetTools/blob/master/LICENSE
��Ŀ��ַ��https://github.com/xucongli1989/XCLNetTools
Create By: XCL @ 2012

 */

using System.IO;

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