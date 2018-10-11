using System ;
using System . Collections . Generic ;
using System . Linq ;

using JetBrains . Annotations ;

using OpenCvSharp ;

namespace Detector
{

	[PublicAPI]
	[Detector]
	public class RedCubeDetector : ThingsDetector
	{

		public static byte [ ] RedCubeLowerMaskData { get ; } = { 80 , 50 , 50 } ;

		public static byte [ ] RedCubeUpperMaskData { get ; } = { 100 , 255 , 255 } ;


		private InputArray RedCubeLowerMask { get ; } = InputArray . Create ( RedCubeLowerMaskData ) ;

		private InputArray RedCubeUpperMask { get ; } = InputArray . Create ( RedCubeUpperMaskData ) ;


		public override ThingType Thing => ThingType . RedCube ;

		public override ThingLimit Limit => ThingLimit . MustCube ;


		public RedCubeDetector ( FrameSource source ) : base ( source ) { }

		public override void Update ( )
		{
			Mask = Source . InversedHsvImage . InRange ( RedCubeLowerMask , RedCubeUpperMask ) ;

			Mask = Mask . Dilate ( null , iterations : 2 ) ;

			Mask = Mask . MorphologyEx ( MorphTypes . Close , null ) ;
			Mask = Mask . Erode ( null , iterations : 2 ) ;
		}

	}

}
