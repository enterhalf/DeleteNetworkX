using Microsoft.Win32;
using System.Text.RegularExpressions;

public class Reg {
    public String path;
    public String deletePattern;
    public String[] deleteList;

    public Reg(string path, string deletePattern) {
        this.path = path;
        this.deletePattern = deletePattern;
    }

    public void GenList() {
        RegistryKey _ = Registry.LocalMachine;
        RegistryKey rk = _.OpenSubKey(path);
        deleteList = rk.GetSubKeyNames();
    }

    public void DeleteList() {
        foreach (String key in deleteList) {
            String keyname = $"HKEY_LOCAL_MACHINE\\{path}\\{key}";
            var res = Registry.GetValue(keyname, "Description", null);

            //若是有网络
            if (Regex.IsMatch((String)res, deletePattern)) {
                RegistryKey _ = Registry.LocalMachine;
                RegistryKey rk = _.OpenSubKey(path, true);
                rk.DeleteSubKey(key);
                Console.WriteLine($"{key}已删除");
            }

        }
    }
    public void Delete() {
        GenList();
        DeleteList();
    }
}