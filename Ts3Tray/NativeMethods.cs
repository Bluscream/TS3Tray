using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;

namespace Ts3Tray
{
	[SuppressUnmanagedCodeSecurity]
	internal static class NativeMethods
	{
		public delegate IntPtr MessageHandler(WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled);

		[DllImport("shell32.dll", CharSet = CharSet.Unicode, EntryPoint = "CommandLineToArgvW")]
		private static extern IntPtr _CommandLineToArgvW([MarshalAs(UnmanagedType.LPWStr)] string cmdLine, out int numArgs);

		[DllImport("kernel32.dll", EntryPoint = "LocalFree", SetLastError = true)]
		private static extern IntPtr _LocalFree(IntPtr hMem);

		public static string[] CommandLineToArgvW(string cmdLine)
		{
			IntPtr intPtr = IntPtr.Zero;
			string[] result;
			try
			{
				int num = 0;
				intPtr = NativeMethods._CommandLineToArgvW(cmdLine, out num);
				if (intPtr == IntPtr.Zero)
				{
					throw new Win32Exception();
				}
				string[] array = new string[num];
				for (int i = 0; i < num; i++)
				{
					IntPtr ptr = Marshal.ReadIntPtr(intPtr, i * Marshal.SizeOf(typeof(IntPtr)));
					array[i] = Marshal.PtrToStringUni(ptr);
				}
				result = array;
			}
			finally
			{
				NativeMethods._LocalFree(intPtr);
			}
			return result;
		}
	}
}
