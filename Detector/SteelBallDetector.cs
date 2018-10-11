using System ;
using System . Collections . Generic ;
using System . Linq ;

using JetBrains . Annotations ;

using OpenCvSharp ;

namespace Detector
{

	[PublicAPI]
	[Detector]
	public class SteelBallDetector : ThingsDetector
	{

		public static byte [ ] SteelBallLowerMaskData { get ; } = { 0 , 0 , 0 } ;

		public static byte [ ] SteelBallUpperMaskData { get ; } = { 181 , 120 , 255 } ;

		private InputArray SteelBallLowerMask { get ; } = InputArray . Create ( SteelBallLowerMaskData ) ;

		private InputArray SteelBallUpperMask { get ; } = InputArray . Create ( SteelBallUpperMaskData ) ;


		public override ThingType Thing => ThingType . SteelBall ;

		public override ThingLimit Limit => ThingLimit . MustCircle ;

		//public override (ThingType thing, Point2f position, double size)? GetThing()
		//{


		//	Cv2.FindContours(mask,
		//					out Point[][] cnts,
		//					out HierarchyIndex[] hierarchyIndices,
		//					RetrievalModes.External,
		//					ContourApproximationModes.ApproxSimple);


		//	if (cnts.Any())
		//	{
		//		Point[] firstItem = cnts.OrderByDescending((points) => Cv2.ContourArea(points)).
		//				First();


		//		Mat color = mask.CvtColor(ColorConversionCodes.GRAY2BGR);
		//		Cv2.DrawContours(color, new[] { firstItem }, -1, Scalar.Red);
		//		Cv2.ImShow(nameof(SteelBallDetector), color);

		//		Cv2.MinEnclosingCircle(firstItem, out Point2f center, out float radius);

		//		return (ThingType.SteelBall, center, Cv2.ContourArea(firstItem));

		//	}

		//	else
		//	{
		//		return null;
		//	}


		////Cv2.Circle(frame, center, (int)radius, Scalar.Black, 2);

		////Cv2.ImShow("sou", frame);

		////Mat edges = new Mat();

		////Cv2.Canny(grayImage, edges, 300, 200);


		////Cv2.ImShow("res", grayImage);

		////Cv2.ImShow("dege", edges);

		//}


		public SteelBallDetector ( FrameSource source ) : base ( source ) { }

		//public static byte[] YakultLowerMaskData { get; } = { 105, 50, 50 };

		//public static byte[] YakultUpperMaskData { get; } = { 117, 255, 255 };

		//private InputArray YakultLowerMask { get; } = InputArray.Create(YakultLowerMaskData);

		//private InputArray YakultUpperMask { get; } = InputArray.Create(YakultUpperMaskData);

		public override void Update ( )
		{
			Mat edgeMask = new Mat ( ) ;

			Cv2 . Canny ( Source . GrayFrame , edgeMask , 300 , 200 ) ;

			edgeMask = edgeMask . Dilate ( null , iterations : 3 ) ;
			edgeMask = edgeMask . MorphologyEx ( MorphTypes . Close , null , iterations : 8 ) ;
			edgeMask = edgeMask . Erode ( null , iterations : 4 ) ;

			Mat colorMask = Source . HsvImage . InRange ( SteelBallLowerMask , SteelBallUpperMask ) ;
			colorMask = colorMask . MorphologyEx ( MorphTypes . Close , null ) ;


			Cv2 . BitwiseAnd ( edgeMask , colorMask , Mask ) ;
		}

	}

}
