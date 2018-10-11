using System ;
using System . Collections . Generic ;
using System . Linq ;

using JetBrains . Annotations ;

using OpenCvSharp ;

namespace Detector
{

	[PublicAPI]
	[Detector]
	public class BeerDetector : ThingsDetector
	{

		public static byte [ ] BeerLowerMaskData { get ; } = { 70 , 100 , 50 } ;

		public static byte [ ] BeerUpperMaskData { get ; } = { 80 , 255 , 255 } ;


		private InputArray BeerLowerMask { get ; } = InputArray . Create ( BeerLowerMaskData ) ;

		private InputArray BeerUpperMask { get ; } = InputArray . Create ( BeerUpperMaskData ) ;


		public override ThingType Thing => ThingType . Beer ;

		public override ThingLimit Limit => ThingLimit . DoNotCare ;


		////Cv2.Circle(frame, center, (int)radius, Scalar.Black, 2);

		////Cv2.ImShow("sou", frame);

		////Mat edges = new Mat();

		////Cv2.Canny(grayImage, edges, 300, 200);


		////Cv2.ImShow("res", grayImage);

		////Cv2.ImShow("dege", edges);


		public BeerDetector ( FrameSource source ) : base ( source ) { }

		public override void Update ( )
		{
			Mask = Source . HsvImage . InRange ( BeerLowerMask , BeerUpperMask ) ;

			//Mat edgeMask = new Mat();

			//Cv2.Canny(Source.GrayFrame, edgeMask, 300, 200);


			//edgeMask = edgeMask.Dilate(null, iterations: 4);
			//edgeMask = edgeMask.MorphologyEx(MorphTypes.Close, null, iterations: 8);
			//edgeMask = edgeMask.Erode(null, iterations: 4);

			//Cv2.BitwiseNot(edgeMask, edgeMask);

			//Cv2.BitwiseAnd(edgeMask, Mask, Mask);

			Mask = Mask . Dilate ( null , iterations : 2 ) ;

			Mask = Mask . MorphologyEx ( MorphTypes . Close , null , iterations : 5 ) ;
			Mask = Mask . Erode ( null , iterations : 2 ) ;
		}

	}

}
