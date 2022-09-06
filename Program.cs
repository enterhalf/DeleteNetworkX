using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Windows;
using Microsoft.Win32;

namespace MyApp {
    static class Program {
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        public static extern bool FreeConsole();

        [STAThread]
        private static void Main() {
            _ = AllocConsole();

            //检查是否是以管理员启动
            if (!IsRunAsAdmin()) {
                //创建启动对象
                var startInfo = new ProcessStartInfo {
                    UseShellExecute = true,
                    WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory,
                    FileName =
                        Process.GetCurrentProcess().MainModule.FileName ??
                        throw new InvalidOperationException("获取本进程的文件位置失败"),
                    Verb = "runas",
                };
                _ = Process.Start(startInfo);

                return;
            }

            //上面是申请权限的代码


            String path1 = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\NetworkList\\Profiles";
            String path2 = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\NetworkList\\Signatures\\Unmanaged";
            String deletePattern = "网络";

            Reg reg1 = new Reg(path1, deletePattern);
            reg1.Delete();
            Reg reg2 = new Reg(path2, deletePattern);
            reg2.Delete();


        }
        private static bool IsRunAsAdmin() {
            //检查本程序是否是以Windows管理员身份运行
            using var wi = WindowsIdentity.GetCurrent();
            var wp = new WindowsPrincipal(wi);

            return wp.IsInRole(WindowsBuiltInRole.Administrator);
        }

    }
    
}