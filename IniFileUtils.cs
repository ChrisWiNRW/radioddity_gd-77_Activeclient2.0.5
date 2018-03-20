using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

internal class IniFileUtils
{
	private static string iniPath;

	[DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
	private static extern long WritePrivateProfileString(string string_0, string string_1, string string_2, string string_3);

	[DllImport("kernel32.DLL ", CharSet = CharSet.Ansi)]
	private static extern int GetPrivateProfileInt(string string_0, string string_1, int int_0, string string_2);

	[DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
	private static extern int GetPrivateProfileString(string string_0, string string_1, string string_2, StringBuilder stringBuilder_0, int int_0, string string_3);

	[DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
	public static extern int GetPrivateProfileSectionNames(IntPtr intptr_0, int int_0, string string_0);

	[DllImport("kernel32.DLL ", CharSet = CharSet.Ansi)]
	private static extern int GetPrivateProfileSection(string string_0, byte[] byte_0, int int_0, string string_1);

	public static string smethod_0()
	{
		return IniFileUtils.iniPath;
	}

	public static void smethod_1(string string_0)
	{
		IniFileUtils.iniPath = string_0;
	}

	private IniFileUtils(string string_0)
	{
		//Class5.XCUF1frzK2Woy();
		//base._002Ector();
		IniFileUtils.smethod_1(string_0);
	}

	public static int smethod_2(string string_0, string string_1, int int_0)
	{
		return IniFileUtils.GetPrivateProfileInt(string_0, string_1, int_0, IniFileUtils.iniPath);
	}

	public static void smethod_3(string string_0, string string_1, int int_0)
	{
		IniFileUtils.WritePrivateProfileString(string_0, string_1, int_0.ToString(), IniFileUtils.iniPath);
	}

	public static string smethod_4(string string_0, string string_1, string string_2)
	{
		StringBuilder stringBuilder = new StringBuilder(1024);
		IniFileUtils.GetPrivateProfileString(string_0, string_1, string_2, stringBuilder, 1024, IniFileUtils.iniPath);
		return stringBuilder.ToString();
	}

	public static string smethod_5(string string_0, string string_1, string string_2, int int_0)
	{
		StringBuilder stringBuilder = new StringBuilder();
		IniFileUtils.GetPrivateProfileString(string_0, string_1, string_2, stringBuilder, int_0, IniFileUtils.iniPath);
		return stringBuilder.ToString();
	}

	public static void smethod_6(string string_0, string string_1, string string_2)
	{
		IniFileUtils.WritePrivateProfileString(string_0, string_1, string_2, IniFileUtils.iniPath);
	}

	public static void smethod_7(string string_0, string string_1)
	{
		IniFileUtils.WritePrivateProfileString(string_0, string_1, null, IniFileUtils.iniPath);
	}

	public static void smethod_8(string string_0)
	{
		IniFileUtils.WritePrivateProfileString(string_0, null, null, IniFileUtils.iniPath);
	}

	public static int smethod_9(out string[] string_0)
	{
		IntPtr intPtr = Marshal.AllocCoTaskMem(32767);
		int privateProfileSectionNames = IniFileUtils.GetPrivateProfileSectionNames(intPtr, 32767, IniFileUtils.iniPath);
		if (privateProfileSectionNames == 0)
		{
			string_0 = null;
			return -1;
		}
		string text = Marshal.PtrToStringAnsi(intPtr, privateProfileSectionNames).ToString();
		Marshal.FreeCoTaskMem(intPtr);
		string text2 = text.Substring(0, text.Length - 1);
		char[] separator = new char[1];
		string_0 = text2.Split(separator);
		return 0;
	}

	public static int smethod_10(string string_0, out string[] string_1, out string[] string_2)
	{
		byte[] array = new byte[65535];
		IniFileUtils.GetPrivateProfileSection(string_0, array, array.Length, IniFileUtils.iniPath);
		string @string = Encoding.Default.GetString(array);
		string text = @string;
		char[] separator = new char[1];
		string[] array2 = text.Split(separator);
		ArrayList arrayList = new ArrayList();
		string[] array3 = array2;
		foreach (string text2 in array3)
		{
			if (text2 != string.Empty)
			{
				arrayList.Add(text2);
			}
		}
		string_1 = new string[arrayList.Count];
		string_2 = new string[arrayList.Count];
		for (int j = 0; j < arrayList.Count; j++)
		{
			string[] array4 = arrayList[j].ToString().Split('=');
			if (array4.Length == 2)
			{
				string_1[j] = array4[0].Trim();
				string_2[j] = array4[1].Trim();
			}
			else if (array4.Length == 1)
			{
				string_1[j] = array4[0].Trim();
				string_2[j] = "";
			}
			else if (array4.Length == 0)
			{
				string_1[j] = "";
				string_2[j] = "";
			}
		}
		return 0;
	}

	static IniFileUtils()
	{
		//Class5.XCUF1frzK2Woy();
		IniFileUtils.iniPath = Application.StartupPath + "\\Setup.ini";
	}
}
