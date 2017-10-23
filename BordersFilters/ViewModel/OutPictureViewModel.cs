using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ViewModel
{
	public class OutPictureViewModel : DependencyObject
	{
		public static void OnStaticSetValue(BitmapSource bm)
		{
			StaticSetValue?.Invoke(bm);
		}

		private static event Action<BitmapSource> StaticSetValue;

		public static readonly DependencyProperty OutImageSourseProperty = DependencyProperty.Register(
			nameof(OutImageSourse), typeof(BitmapSource), typeof(OutPictureViewModel), new PropertyMetadata(default(BitmapSource)));

		public OutPictureViewModel()
		{
			StaticSetValue += bm =>
			{
				OutImageSourse = bm;
			};
		}

		public BitmapSource OutImageSourse
		{
			get { return (BitmapSource)GetValue(OutImageSourseProperty); }
			set
			{
				SetValue(OutImageSourseProperty, value);
			}
		}
	}
}
