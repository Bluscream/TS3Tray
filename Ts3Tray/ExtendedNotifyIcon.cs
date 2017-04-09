using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Resources;

namespace Ts3Tray
{
	public class ExtendedNotifyIcon : IDisposable
	{
		public delegate void MouseLeaveHandler();

		public delegate void MouseMoveHandler();

		public NotifyIcon targetNotifyIcon;

		private System.Drawing.Point notifyIconMousePosition;

		private Timer delayMouseLeaveEventTimer;

		private bool _IsDisposed;

		public event ExtendedNotifyIcon.MouseLeaveHandler MouseLeave;

		public event ExtendedNotifyIcon.MouseMoveHandler MouseMove;

		public ExtendedNotifyIcon(int millisecondsToDelayMouseLeaveEvent)
		{
			this.targetNotifyIcon = new NotifyIcon();
			this.targetNotifyIcon.Visible = true;
			this.targetNotifyIcon.MouseMove += new MouseEventHandler(this.targetNotifyIcon_MouseMove);
			this.delayMouseLeaveEventTimer = new Timer();
			this.delayMouseLeaveEventTimer.Tick += new EventHandler(this.delayMouseLeaveEventTimer_Tick);
			this.delayMouseLeaveEventTimer.Interval = 100;
			Uri uriResource = new Uri("pack://application:,,,/Ts3Tray;component/Resources/AppIcon.ico");
			StreamResourceInfo resourceStream = System.Windows.Application.GetResourceStream(uriResource);
			this.targetNotifyIcon.Icon = new Icon(resourceStream.Stream, new System.Drawing.Size(16, 16));
		}

		public ExtendedNotifyIcon() : this(100)
		{
		}

		public void StartMouseLeaveTimer()
		{
			this.delayMouseLeaveEventTimer.Start();
		}

		public void StopMouseLeaveEventFromFiring()
		{
			this.delayMouseLeaveEventTimer.Stop();
		}

		public void targetNotifyIcon_MouseMove(object sender, MouseEventArgs e)
		{
			this.notifyIconMousePosition = Control.MousePosition;
			this.MouseMove();
			this.delayMouseLeaveEventTimer.Start();
		}

		private void delayMouseLeaveEventTimer_Tick(object sender, EventArgs e)
		{
			if (this.notifyIconMousePosition != Control.MousePosition)
			{
				this.MouseLeave();
				this.delayMouseLeaveEventTimer.Stop();
			}
		}

		~ExtendedNotifyIcon()
		{
			this.Dispose(false);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(true);
		}

		protected virtual void Dispose(bool IsDisposing)
		{
			if (this._IsDisposed)
			{
				return;
			}
			if (IsDisposing)
			{
				this.targetNotifyIcon.Dispose();
			}
			this._IsDisposed = true;
		}
	}
}
