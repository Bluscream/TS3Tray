using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Ts3Tray
{
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public class AboutBox : Window, IComponentConnector
	{
		internal AboutBox AboutTS3Tray;

		internal Grid uiGridMain;

		internal SlickToggleButton CloseButton;

		internal Label TextLabel;

		internal Label TitleLabel;

		private bool _contentLoaded;

		public AboutBox()
		{
			this.InitializeComponent();
			base.Left = SystemParameters.WorkArea.Width / 2.0 - base.Width / 2.0;
			base.Top = SystemParameters.WorkArea.Height / 2.0 - base.Height / 2.0;
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			Version version = executingAssembly.GetName().Version;
			this.TextLabel.Content = "TS3Tray v" + version.ToString();
		}

		private void Close_Button_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/Ts3Tray;component/aboutbox.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		[DebuggerNonUserCode]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		[EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.AboutTS3Tray = (AboutBox)target;
				return;
			case 2:
				this.uiGridMain = (Grid)target;
				return;
			case 3:
				this.CloseButton = (SlickToggleButton)target;
				return;
			case 4:
				this.TextLabel = (Label)target;
				return;
			case 5:
				((Button)target).Click += new RoutedEventHandler(this.Close_Button_Click);
				return;
			case 6:
				this.TitleLabel = (Label)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}
	}
}
