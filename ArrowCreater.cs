using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Ts3Tray
{
	internal class ArrowCreater : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			double num = (double)values[0];
			double num2 = (double)values[1];
			if (num2 == 0.0 || num == 0.0)
			{
				return null;
			}
			Thickness thickness = (Thickness)values[2];
			bool isUp = (bool)values[3];
			double height = num2 - thickness.Top - thickness.Bottom;
			double width = num - thickness.Left - thickness.Right;
			return this.CreateArrow(width, height, isUp);
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		private PointCollection CreateArrow(double width, double height, bool isUp)
		{
			double num = height * 0.2;
			double y;
			double y2;
			if (isUp)
			{
				y = num;
				y2 = height - num;
			}
			else
			{
				y2 = num;
				y = height - num;
			}
			return new PointCollection
			{
				new Point(num, y2),
				new Point(width / 2.0, y),
				new Point(width - num, y2)
			};
		}
	}
}
