using System ;
using System . Collections . Generic ;
using System . Linq ;

using OpenCvSharp ;

namespace Detector
{

	public class FrameSource
	{

		public Mat CurrentFrame { get ; private set ; } = new Mat ( ) ;

		public Mat GrayFrame { get ; private set ; } = new Mat ( ) ;

		public Mat HsvImage { get ; private set ; } = new Mat ( ) ;

		public Mat InversedFrame { get ; private set ; } = new Mat ( ) ;

		public Mat InversedGrayFrame { get ; private set ; } = new Mat ( ) ;

		public Mat InversedHsvImage { get ; private set ; } = new Mat ( ) ;


		public void UpdateFrame ( Mat frame )
		{
			CurrentFrame = frame ;
			GrayFrame = CurrentFrame . CvtColor ( ColorConversionCodes . BGR2GRAY ) ;
			HsvImage = CurrentFrame . CvtColor ( ColorConversionCodes . BGR2HSV ) ;

			Cv2 . BitwiseNot ( CurrentFrame , InversedFrame ) ;
			InversedGrayFrame = InversedFrame . CvtColor ( ColorConversionCodes . BGR2GRAY ) ;
			InversedHsvImage = InversedFrame . CvtColor ( ColorConversionCodes . BGR2HSV ) ;
		}

	}

}
