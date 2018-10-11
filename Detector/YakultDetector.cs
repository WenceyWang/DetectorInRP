using System ;
using System . Collections . Generic ;
using System . Linq ;

using JetBrains . Annotations ;

using OpenCvSharp ;

namespace Detector
{

	[PublicAPI]
	[Detector]
	public class YakultDetector : ThingsDetector
	{

		public static byte [ ] YakultLowerMaskData { get ; } = { 85 , 90 , 70 } ;

		public static byte [ ] YakultUpperMaskData { get ; } = { 95 , 255 , 255 } ;

		private InputArray YakultLowerMask { get ; } = InputArray . Create ( YakultLowerMaskData ) ;

		private InputArray YakultUpperMask { get ; } = InputArray . Create ( YakultUpperMaskData ) ;


		public override ThingType Thing => ThingType . Yakult ;

		public override ThingLimit Limit => ThingLimit . DoNotCare ;

		//public override (ThingType thing, Point2f position, double size)? GetThing()
		//{
		//	Cv2.FindContours(Mask,
		//						 out Point[][] cnts,
		//						 out HierarchyIndex[] hierarchyIndices,
		//						 RetrievalModes.External,
		//						 ContourApproximationModes.ApproxSimple);

		//	List<Point[]> couList = cnts.ToList();


		//	if (cnts.Any())
		//	{
		//		Point[] firstItem = cnts.OrderByDescending((points) => Cv2.ContourArea(points)).
		//									First();

		//		Mat color = Mask.CvtColor(ColorConversionCodes.GRAY2BGR);
		//		Cv2.DrawContours(color, new[] { firstItem }, -1, Scalar.Red);
		//		Cv2.ImShow(nameof(YakultDetector), color);


		//		Cv2.MinEnclosingCircle(firstItem, out Point2f center, out float radius);

		//		return (ThingType.Yakult, center, Cv2.ContourArea(firstItem));
		//	}
		//	else
		//	{
		//		return null;
		//	}


		//}


		public YakultDetector ( FrameSource source ) : base ( source ) { }

		public override void Update ( )
		{
			Mask = Source . InversedHsvImage . InRange ( YakultLowerMask , YakultUpperMask ) ;

			Mat edgeMask = new Mat ( ) ;

			Cv2 . Canny ( Source . GrayFrame , edgeMask , 300 , 200 ) ;

			edgeMask = edgeMask . Dilate ( null , iterations : 4 ) ;
			edgeMask = edgeMask . MorphologyEx ( MorphTypes . Close , null , iterations : 9 ) ;

			edgeMask = edgeMask . Dilate ( null , iterations : 5 ) ;

			//edgeMask = edgeMask.Erode(null, iterations: 4);

			Cv2 . BitwiseAnd ( edgeMask , Mask , Mask ) ;

			//Cv2.BitwiseAnd()

			Mask = Mask . Dilate ( null , iterations : 5 ) ;
			Mask = Mask . MorphologyEx ( MorphTypes . Close , null , iterations : 9 ) ;

			//Mask = Mask . Erode ( null , iterations : 2 ) ;
		}

	}

}
