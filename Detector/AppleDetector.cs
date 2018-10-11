using System ;
using System . Collections . Generic ;
using System . Linq ;

using JetBrains . Annotations ;

using OpenCvSharp ;

namespace Detector
{

	[PublicAPI]
	[Detector]
	public class AppleDetector : ThingsDetector
	{

		public static byte [ ] AppleLowerMaskData { get ; } = { 80 , 20 , 200 } ;

		public static byte [ ] AppleUpperMaskData { get ; } = { 100 , 255 , 255 } ;


		private InputArray AppleLowerMask { get ; } = InputArray . Create ( AppleLowerMaskData ) ;

		private InputArray AppleUpperMask { get ; } = InputArray . Create ( AppleUpperMaskData ) ;


		public override ThingType Thing => ThingType . Apple ;

		public override ThingLimit Limit => ThingLimit . MustCircle ;


		////Cv2.Circle(frame, center, (int)radius, Scalar.Black, 2);

		////Cv2.ImShow("sou", frame);

		////Mat edges = new Mat();

		////Cv2.Canny(grayImage, edges, 300, 200);


		////Cv2.ImShow("res", grayImage);

		////Cv2.ImShow("dege", edges);


		public AppleDetector ( FrameSource source ) : base ( source ) { }

		public override void Update ( )
		{
			Mask = Source . InversedHsvImage . InRange ( AppleLowerMask , AppleUpperMask ) ;

			Mat edgeMask = new Mat ( ) ;

			Cv2 . Canny ( Source . GrayFrame , edgeMask , 300 , 200 ) ;


			edgeMask = edgeMask . Dilate ( null , iterations : 4 ) ;
			edgeMask = edgeMask . MorphologyEx ( MorphTypes . Close , null , iterations : 8 ) ;
			edgeMask = edgeMask . Erode ( null , iterations : 4 ) ;

			Cv2 . BitwiseNot ( edgeMask , edgeMask ) ;

			Cv2 . BitwiseAnd ( edgeMask , Mask , Mask ) ;

			Mask = Mask . Dilate ( null , iterations : 2 ) ;

			Mask = Mask . MorphologyEx ( MorphTypes . Close , null ) ;
			Mask = Mask . Erode ( null , iterations : 2 ) ;
		}

	}

}
