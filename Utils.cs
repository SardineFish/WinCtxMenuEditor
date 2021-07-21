using Microsoft.Win32;

namespace DefaultNamespace
{
    public static class Utils
    {
        

        public static RegistryKey OpenOrCreateKey(this RegistryKey root, string key)
        {
            return root.OpenSubKey(key, true) ?? root.CreateSubKey(key, true);
        }

    }
}