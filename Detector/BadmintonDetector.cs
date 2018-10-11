using System ;
using System . Collections . Generic ;
using System . Linq ;

using JetBrains . Annotations ;

using OpenCvSharp ;

namespace Detector
{

	//[PublicAPI]
	//[Detector]
	//public class BadmintonDetector : ThingsDetector
	//{

	//	public static byte [ ] BadmintonLowerMaskData { get ; } = { 0 , 0 , 230 } ;

	//	public static byte [ ] BadmintonUpperMaskData { get ; } = { 181 , 255 , 255 } ;


	//	private InputArray BadmintonLowerMask { get ; } = InputArray . Create ( BadmintonLowerMaskData ) ;

	//	private InputArray BadmintonUpperMask { get ; } = InputArray . Create ( BadmintonUpperMaskData ) ;


	//	public override ThingType Thing => ThingType . Badminton ;

	//	public override ThingLimit Limit => ThingLimit . MustCircle ;

	//	//public override (ThingType thing, Point2f position, double size)? GetThing()
	//	//{
	//	//	Cv2.FindContours(mask,
	//	//					out Point[][] cnts,
	//	//					out HierarchyIndex[] hierarchyIndices,
	//	//					RetrievalModes.External,
	//	//					ContourApproximationModes.ApproxSimple);

	//	//	if (cnts.Any())
	//	//	{
	//	//		Point[] firstItem = cnts.OrderByDescending((points) => Cv2.ContourArea(points)).
	//	//								First();


	//	//		Mat color = mask.CvtColor(ColorConversionCodes.GRAY2BGR);
	//	//		Cv2.DrawContours(color, new[] { firstItem }, -1, Scalar.Red);
	//	//		Cv2.ImShow(nameof(AppleDetector), color);


	//	//		Cv2.MinEnclosingCircle(firstItem, out Point2f center, out float radius);

	//	//		return (ThingType.Badminton, center, Cv2.ContourArea(firstItem));
	//	//	}
	//	//	else
	//	//	{
	//	//		return null;
	//	//	}


	//	//	////Cv2.Circle(frame, center, (int)radius, Scalar.Black, 2);

	//	//	////Cv2.ImShow("sou", frame);

	//	//	////Mat edges = new Mat();

	//	//	////Cv2.Canny(grayImage, edges, 300, 200);


	//	//	////Cv2.ImShow("res", grayImage);

	//	//	////Cv2.ImShow("dege", edges);
	//	//}


	//	public BadmintonDetector ( FrameSource source ) : base ( source ) { }

	//	public override void Update ( )
	//	{
	//		Mask = Source . HsvImage . InRange ( BadmintonLowerMask , BadmintonUpperMask ) ;


	//		//Vec3b color = Source.HsvImage.At<Vec3b>(461, 161);
	//		//Vec3b color2 = Source.HsvImage.At<Vec3b>(409, 135);

	//		Mask = Mask . Dilate ( null , iterations : 2 ) ;
	//		Mask = Mask . MorphologyEx ( MorphTypes . Close , null , iterations : 3 ) ;
	//		Mask = Mask . Erode ( null , iterations : 2 ) ;
	//	}

	//}

}
