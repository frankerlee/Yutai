using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

namespace Yutai.ArcGIS.Common
{
    public class RegistryTools
    {
        private static UIntPtr HKEY_CLASSES_ROOT;
        private static UIntPtr HKEY_CURRENT_CONFIG;
        private static UIntPtr HKEY_CURRENT_USER;
        private static UIntPtr HKEY_LOCAL_MACHINE;
        private static UIntPtr HKEY_USERS;
        internal const ushort PROCESSOR_ARCHITECTURE_AMD64 = 9;
        internal const ushort PROCESSOR_ARCHITECTURE_IA64 = 6;
        internal const ushort PROCESSOR_ARCHITECTURE_INTEL = 0;
        internal const ushort PROCESSOR_ARCHITECTURE_UNKNOWN = 0xffff;

        static RegistryTools()
        {
            old_acctor_mc();
        }

        public static string Get32BitRegistryKey(string string_0, string string_1, string string_2)
        {
            try
            {
                RegistryKey key = TransferKeyName32(string_0);
                if (key == null)
                {
                    return "";
                }
                foreach (string str2 in string_1.Split(new char[] { '\\' }))
                {
                    key = key.OpenSubKey(str2);
                    if (key == null)
                    {
                        return "";
                    }
                }
                if (key != null)
                {
                    return Convert.ToString(key.GetValue(string_2));
                }
            }
            catch
            {
            }
            return "";
        }

        public static string Get64BitRegistryKey(string string_0, string string_1, string string_2)
        {
            int num = 1 | 0x100;
            try
            {
                UIntPtr ptr = TransferKeyName(string_0);
                IntPtr zero = IntPtr.Zero;
                StringBuilder builder = new StringBuilder("".PadLeft(0x400));
                uint num2 = 0x400;
                uint num3 = 0;
                IntPtr ptr3 = new IntPtr();
                if (Wow64DisableWow64FsRedirection(ref ptr3))
                {
                    RegOpenKeyEx(ptr, string_1, 0, num, out zero);
                    RegDisableReflectionKey(zero);
                    RegQueryValueEx(zero, string_2, 0, out num3, builder, ref num2);
                    RegEnableReflectionKey(zero);
                }
                Wow64RevertWow64FsRedirection(ptr3);
                return builder.ToString().Trim();
            }
            catch (Exception)
            {
                return null;
            }
        }

        [DllImport("kernel32.dll")]
        internal static extern void GetNativeSystemInfo(ref SYSTEM_INFO system_INFO_0);
        public static Platform GetPlatform()
        {
            SYSTEM_INFO system_info = new SYSTEM_INFO();
            if ((Environment.OSVersion.Version.Major > 5) || ((Environment.OSVersion.Version.Major == 5) && (Environment.OSVersion.Version.Minor >= 1)))
            {
                GetNativeSystemInfo(ref system_info);
            }
            else
            {
                GetSystemInfo(ref system_info);
            }
            ushort wProcessorArchitecture = system_info.wProcessorArchitecture;
            if (wProcessorArchitecture == 0)
            {
                return Platform.X86;
            }
            if ((wProcessorArchitecture != 6) && (wProcessorArchitecture != 9))
            {
                return Platform.Unknown;
            }
            return Platform.X64;
        }

        public static string GetRegistryKey(string string_0, string string_1, string string_2)
        {
            if (GetPlatform() == Platform.X64)
            {
                return Get64BitRegistryKey(string_0, string_1, string_2);
            }
            return Get32BitRegistryKey(string_0, string_1, string_2);
        }

        [DllImport("kernel32.dll")]
        internal static extern void GetSystemInfo(ref SYSTEM_INFO system_INFO_0);
        private static void old_acctor_mc()
        {
            HKEY_CLASSES_ROOT = (UIntPtr) (-2147483648);
            HKEY_CURRENT_USER = (UIntPtr) (-2147483647);
            HKEY_LOCAL_MACHINE = (UIntPtr) (-2147483646);
            HKEY_USERS = (UIntPtr) (-2147483645);
            HKEY_CURRENT_CONFIG = (UIntPtr) (-2147483643);
        }

        [DllImport("Advapi32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        public static extern long RegDisableReflectionKey(IntPtr intptr_0);
        [DllImport("Advapi32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        public static extern long RegEnableReflectionKey(IntPtr intptr_0);
        [DllImport("Advapi32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        public static extern uint RegOpenKeyEx(UIntPtr uintptr_0, string string_0, uint uint_0, int int_0, out IntPtr intptr_0);
        [DllImport("Advapi32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern int RegQueryValueEx(IntPtr intptr_0, string string_0, int int_0, out uint uint_0, StringBuilder stringBuilder_0, ref uint uint_1);
        private static UIntPtr TransferKeyName(string string_0)
        {
            string str = string_0;
            switch (str)
            {
                case null:
                    break;

                case "HKEY_CLASSES_ROOT":
                    return HKEY_CLASSES_ROOT;

                case "HKEY_CURRENT_USER":
                    return HKEY_CURRENT_USER;

                case "HKEY_LOCAL_MACHINE":
                    return HKEY_LOCAL_MACHINE;

                default:
                    if (!(str == "HKEY_USERS"))
                    {
                        if (str == "HKEY_CURRENT_CONFIG")
                        {
                            return HKEY_CURRENT_CONFIG;
                        }
                    }
                    else
                    {
                        return HKEY_USERS;
                    }
                    break;
            }
            return HKEY_CLASSES_ROOT;
        }

        private static RegistryKey TransferKeyName32(string string_0)
        {
            string str = string_0;
            switch (str)
            {
                case null:
                    break;

                case "HKEY_CLASSES_ROOT":
                    return Registry.ClassesRoot;

                case "HKEY_CURRENT_USER":
                    return Registry.CurrentUser;

                case "HKEY_LOCAL_MACHINE":
                    return Registry.LocalMachine;

                default:
                    if (!(str == "HKEY_USERS"))
                    {
                        if (str == "HKEY_CURRENT_CONFIG")
                        {
                            return Registry.CurrentConfig;
                        }
                    }
                    else
                    {
                        return Registry.Users;
                    }
                    break;
            }
            return Registry.ClassesRoot;
        }

        [DllImport("Kernel32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        public static extern bool Wow64DisableWow64FsRedirection(ref IntPtr intptr_0);
        [DllImport("Kernel32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        public static extern bool Wow64RevertWow64FsRedirection(IntPtr intptr_0);

        public enum Platform
        {
            X86,
            X64,
            Unknown
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct SYSTEM_INFO
        {
            public ushort wProcessorArchitecture;
            public ushort wReserved;
            public uint dwPageSize;
            public IntPtr lpMinimumApplicationAddress;
            public IntPtr lpMaximumApplicationAddress;
            public UIntPtr dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public ushort wProcessorLevel;
            public ushort wProcessorRevision;
        }
    }
}

