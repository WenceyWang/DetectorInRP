using System ;
using System . Collections . Generic ;
using System . Linq ;

using JetBrains . Annotations ;

using OpenCvSharp ;

namespace Detector
{

	[PublicAPI]
	[Detector]
	public class BlueCubeDetector : ThingsDetector
	{

		public static byte [ ] BlueCubeLowerMaskData { get ; } = { 95 , 50 , 50 } ;

		public static byte [ ] BlueCubeUpperMaskData { get ; } = { 105 , 255 , 255 } ;


		private InputArray BlueCubeLowerMask { get ; } = InputArray . Create ( BlueCubeLowerMaskData ) ;

		private InputArray BlueCubeUpperMask { get ; } = InputArray . Create ( BlueCubeUpperMaskData ) ;


		public override ThingType Thing => ThingType . BlueCube ;

		public override ThingLimit Limit => ThingLimit . MustCube ;


		public BlueCubeDetector ( FrameSource source ) : base ( source ) { }

		public override void Update ( )
		{
			Mask = Source . HsvImage . InRange ( BlueCubeLowerMask , BlueCubeUpperMask ) ;

			Mask = Mask . Dilate ( null , iterations : 2 ) ;

			Mask = Mask . MorphologyEx ( MorphTypes . Close , null ) ;
			Mask = Mask . Erode ( null , iterations : 2 ) ;
		}

	}

}
