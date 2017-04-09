using System;

namespace Ts3Tray
{
	internal enum WM
	{
		NULL,
		CREATE,
		DESTROY,
		MOVE,
		SIZE = 5,
		ACTIVATE,
		SETFOCUS,
		KILLFOCUS,
		ENABLE = 10,
		SETREDRAW,
		SETTEXT,
		GETTEXT,
		GETTEXTLENGTH,
		PAINT,
		CLOSE,
		QUERYENDSESSION,
		QUIT,
		QUERYOPEN,
		ERASEBKGND,
		SYSCOLORCHANGE,
		SHOWWINDOW = 24,
		ACTIVATEAPP = 28,
		SETCURSOR = 32,
		MOUSEACTIVATE,
		CHILDACTIVATE,
		QUEUESYNC,
		GETMINMAXINFO,
		WINDOWPOSCHANGING = 70,
		WINDOWPOSCHANGED,
		CONTEXTMENU = 123,
		STYLECHANGING,
		STYLECHANGED,
		DISPLAYCHANGE,
		GETICON,
		SETICON,
		NCCREATE,
		NCDESTROY,
		NCCALCSIZE,
		NCHITTEST,
		NCPAINT,
		NCACTIVATE,
		GETDLGCODE,
		SYNCPAINT,
		NCMOUSEMOVE = 160,
		NCLBUTTONDOWN,
		NCLBUTTONUP,
		NCLBUTTONDBLCLK,
		NCRBUTTONDOWN,
		NCRBUTTONUP,
		NCRBUTTONDBLCLK,
		NCMBUTTONDOWN,
		NCMBUTTONUP,
		NCMBUTTONDBLCLK,
		SYSKEYDOWN = 260,
		SYSKEYUP,
		SYSCHAR,
		SYSDEADCHAR,
		COMMAND = 273,
		SYSCOMMAND,
		MOUSEMOVE = 512,
		LBUTTONDOWN,
		LBUTTONUP,
		LBUTTONDBLCLK,
		RBUTTONDOWN,
		RBUTTONUP,
		RBUTTONDBLCLK,
		MBUTTONDOWN,
		MBUTTONUP,
		MBUTTONDBLCLK,
		MOUSEWHEEL,
		XBUTTONDOWN,
		XBUTTONUP,
		XBUTTONDBLCLK,
		MOUSEHWHEEL,
		CAPTURECHANGED = 533,
		ENTERSIZEMOVE = 561,
		EXITSIZEMOVE,
		IME_SETCONTEXT = 641,
		IME_NOTIFY,
		IME_CONTROL,
		IME_COMPOSITIONFULL,
		IME_SELECT,
		IME_CHAR,
		IME_REQUEST = 648,
		IME_KEYDOWN = 656,
		IME_KEYUP,
		NCMOUSELEAVE = 674,
		DWMCOMPOSITIONCHANGED = 798,
		DWMNCRENDERINGCHANGED,
		DWMCOLORIZATIONCOLORCHANGED,
		DWMWINDOWMAXIMIZEDCHANGE,
		DWMSENDICONICTHUMBNAIL = 803,
		DWMSENDICONICLIVEPREVIEWBITMAP = 806,
		USER = 1024,
		TRAYMOUSEMESSAGE = 2048,
		APP = 32768
	}
}
