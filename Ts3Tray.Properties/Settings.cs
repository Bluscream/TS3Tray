using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Ts3Tray.Properties
{
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0"), CompilerGenerated]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		[DefaultSettingValue(""), UserScopedSetting, DebuggerNonUserCode]
		public string TS3Address
		{
			get
			{
				return (string)this["TS3Address"];
			}
			set
			{
				this["TS3Address"] = value;
			}
		}

		[DefaultSettingValue("10011"), UserScopedSetting, DebuggerNonUserCode]
		public int TS3QueryPort
		{
			get
			{
				return (int)this["TS3QueryPort"];
			}
			set
			{
				this["TS3QueryPort"] = value;
			}
		}

		[DefaultSettingValue("1"), UserScopedSetting, DebuggerNonUserCode]
		public int TS3ServerNr
		{
			get
			{
				return (int)this["TS3ServerNr"];
			}
			set
			{
				this["TS3ServerNr"] = value;
			}
		}

		[DefaultSettingValue("False"), UserScopedSetting, DebuggerNonUserCode]
		public bool DisplayServerInfo
		{
			get
			{
				return (bool)this["DisplayServerInfo"];
			}
			set
			{
				this["DisplayServerInfo"] = value;
			}
		}

		[DefaultSettingValue(""), UserScopedSetting, DebuggerNonUserCode]
		public string Nickname
		{
			get
			{
				return (string)this["Nickname"];
			}
			set
			{
				this["Nickname"] = value;
			}
		}

		[DefaultSettingValue(""), UserScopedSetting, DebuggerNonUserCode]
		public string ServerPw
		{
			get
			{
				return (string)this["ServerPw"];
			}
			set
			{
				this["ServerPw"] = value;
			}
		}

		[DefaultSettingValue("True"), UserScopedSetting, DebuggerNonUserCode]
		public bool Pined
		{
			get
			{
				return (bool)this["Pined"];
			}
			set
			{
				this["Pined"] = value;
			}
		}

		[DefaultSettingValue("10"), UserScopedSetting, DebuggerNonUserCode]
		public byte Update
		{
			get
			{
				return (byte)this["Update"];
			}
			set
			{
				this["Update"] = value;
			}
		}

		[DefaultSettingValue(""), UserScopedSetting, DebuggerNonUserCode]
		public string TS3QueryUser
		{
			get
			{
				return (string)this["TS3QueryUser"];
			}
			set
			{
				this["TS3QueryUser"] = value;
			}
		}

		[DefaultSettingValue(""), UserScopedSetting, DebuggerNonUserCode]
		public string TS3QueryPw
		{
			get
			{
				return (string)this["TS3QueryPw"];
			}
			set
			{
				this["TS3QueryPw"] = value;
			}
		}

		[DefaultSettingValue("False"), UserScopedSetting, DebuggerNonUserCode]
		public bool NotificationJoin
		{
			get
			{
				return (bool)this["NotificationJoin"];
			}
			set
			{
				this["NotificationJoin"] = value;
			}
		}

		[DefaultSettingValue("False"), UserScopedSetting, DebuggerNonUserCode]
		public bool NotificationLeave
		{
			get
			{
				return (bool)this["NotificationLeave"];
			}
			set
			{
				this["NotificationLeave"] = value;
			}
		}

		[DefaultSettingValue("0"), UserScopedSetting, DebuggerNonUserCode]
		public byte NotificationStyle
		{
			get
			{
				return (byte)this["NotificationStyle"];
			}
			set
			{
				this["NotificationStyle"] = value;
			}
		}
	}
}
