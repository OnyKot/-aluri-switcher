using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Switcher.utils
{
    class HostsFile
    {
        private static string fpath;

            static HostsFile()
        {
            fpath = HostsPath(); //TODO: зделать метод получения файла хостов

        }

        public static List<string> AllLines()
        {
            var lines = File.ReadAllLines(fpath);
            return lines.ToList();
        }
        public static void WriteAllLines(IEnumerable<string> lines)
        {
            bool rFlag = ReadOnlyFlag(fpath);
            if (rFlag) DisableReadOnly(fpath);
            File.WriteAllLines(fpath, lines);
            if (rFlag) EnableReadOnly(fpath);

        }
        private static string HostsPath()
        {
            string windir = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            string res = Path.Combine(windir, "System32", "drivers", "etc", "hosts");
            return res;
        }


        private static void CreateIfNotExists(string filename)
        {
            if (!File.Exists(filename))
            {
                File.AppendAllText(filename, "#127.0.0.0    example.com\r\n");
            }
        }
        /*
         * Flags
         */
        private static bool ReadOnlyFlag(string filepath)
        {
            var att = File.GetAttributes(filepath);
            return att.HasFlag(FileAttributes.ReadOnly);
        }

        private static void DisableReadOnly(string filepath)
        {
            var att = File.GetAttributes(filepath);
            var a = att ^ FileAttributes.ReadOnly;
            File.SetAttributes(filepath, a);
        }

        private static void EnableReadOnly(string filepath)
        {
            var att = File.GetAttributes(filepath);
            var a = att | FileAttributes.ReadOnly;
            File.SetAttributes(filepath, a);
        }
    }
}
