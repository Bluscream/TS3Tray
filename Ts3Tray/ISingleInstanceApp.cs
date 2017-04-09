using System;
using System.Collections.Generic;

namespace Ts3Tray
{
	public interface ISingleInstanceApp
	{
		bool SignalExternalCommandLineArgs(IList<string> args);
	}
}
