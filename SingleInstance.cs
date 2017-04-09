using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Ts3Tray
{
	public static class SingleInstance<TApplication> where TApplication : Application, ISingleInstanceApp
	{
		private class IPCRemoteService : MarshalByRefObject
		{
			public void InvokeFirstInstance(IList<string> args)
			{
				if (Application.Current != null)
				{
					Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new DispatcherOperationCallback(SingleInstance<TApplication>.ActivateFirstInstanceCallback), args);
				}
			}

			public override object InitializeLifetimeService()
			{
				return null;
			}
		}

		private const string Delimiter = ":";

		private const string ChannelNameSuffix = "SingeInstanceIPCChannel";

		private const string RemoteServiceName = "SingleInstanceApplicationService";

		private const string IpcProtocol = "ipc://";

		private static Mutex singleInstanceMutex;

		private static IpcServerChannel channel;

		private static IList<string> commandLineArgs;

		public static IList<string> CommandLineArgs
		{
			get
			{
				return SingleInstance<TApplication>.commandLineArgs;
			}
		}

		public static bool InitializeAsFirstInstance(string uniqueName)
		{
			SingleInstance<TApplication>.commandLineArgs = SingleInstance<TApplication>.GetCommandLineArgs(uniqueName);
			string text = uniqueName + Environment.UserName;
			string channelName = text + ":" + "SingeInstanceIPCChannel";
			bool flag;
			SingleInstance<TApplication>.singleInstanceMutex = new Mutex(true, text, out flag);
			if (flag)
			{
				SingleInstance<TApplication>.CreateRemoteService(channelName);
			}
			else
			{
				SingleInstance<TApplication>.SignalFirstInstance(channelName, SingleInstance<TApplication>.commandLineArgs);
			}
			return flag;
		}

		public static void Cleanup()
		{
			if (SingleInstance<TApplication>.singleInstanceMutex != null)
			{
				SingleInstance<TApplication>.singleInstanceMutex.Close();
				SingleInstance<TApplication>.singleInstanceMutex = null;
			}
			if (SingleInstance<TApplication>.channel != null)
			{
				ChannelServices.UnregisterChannel(SingleInstance<TApplication>.channel);
				SingleInstance<TApplication>.channel = null;
			}
		}

		private static IList<string> GetCommandLineArgs(string uniqueApplicationName)
		{
			string[] array = null;
			if (AppDomain.CurrentDomain.ActivationContext == null)
			{
				array = Environment.GetCommandLineArgs();
			}
			else
			{
				string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), uniqueApplicationName);
				string path2 = Path.Combine(path, "cmdline.txt");
				if (File.Exists(path2))
				{
					try
					{
						using (TextReader textReader = new StreamReader(path2, Encoding.Unicode))
						{
							array = NativeMethods.CommandLineToArgvW(textReader.ReadToEnd());
						}
						File.Delete(path2);
					}
					catch (IOException)
					{
					}
				}
			}
			if (array == null)
			{
				array = new string[0];
			}
			return new List<string>(array);
		}

		private static void CreateRemoteService(string channelName)
		{
			BinaryServerFormatterSinkProvider binaryServerFormatterSinkProvider = new BinaryServerFormatterSinkProvider();
			binaryServerFormatterSinkProvider.TypeFilterLevel = TypeFilterLevel.Full;
			IDictionary dictionary = new Dictionary<string, string>();
			dictionary["name"] = channelName;
			dictionary["portName"] = channelName;
			dictionary["exclusiveAddressUse"] = "false";
			SingleInstance<TApplication>.channel = new IpcServerChannel(dictionary, binaryServerFormatterSinkProvider);
			ChannelServices.RegisterChannel(SingleInstance<TApplication>.channel, true);
			SingleInstance<TApplication>.IPCRemoteService obj = new SingleInstance<TApplication>.IPCRemoteService();
			RemotingServices.Marshal(obj, "SingleInstanceApplicationService");
		}

		private static void SignalFirstInstance(string channelName, IList<string> args)
		{
			IpcClientChannel chnl = new IpcClientChannel();
			ChannelServices.RegisterChannel(chnl, true);
			string url = "ipc://" + channelName + "/SingleInstanceApplicationService";
			SingleInstance<TApplication>.IPCRemoteService iPCRemoteService = (SingleInstance<TApplication>.IPCRemoteService)RemotingServices.Connect(typeof(SingleInstance<TApplication>.IPCRemoteService), url);
			if (iPCRemoteService != null)
			{
				iPCRemoteService.InvokeFirstInstance(args);
			}
		}

		private static object ActivateFirstInstanceCallback(object arg)
		{
			IList<string> args = arg as IList<string>;
			SingleInstance<TApplication>.ActivateFirstInstance(args);
			return null;
		}

		private static void ActivateFirstInstance(IList<string> args)
		{
			if (Application.Current == null)
			{
				return;
			}
			TApplication tApplication = (TApplication)((object)Application.Current);
			tApplication.SignalExternalCommandLineArgs(args);
		}
	}
}
