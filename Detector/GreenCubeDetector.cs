using System ;
using System . Collections . Generic ;
using System . Linq ;

using JetBrains . Annotations ;

using OpenCvSharp ;

namespace Detector
{

	[PublicAPI]
	[Detector]
	public class GreenCubeDetector : ThingsDetector
	{

		public static byte [ ] GreenCubeLowerMaskData { get ; } = { 50 , 50 , 50 } ;

		public static byte [ ] GreenCubeUpperMaskData { get ; } = { 70 , 255 , 255 } ;


		private InputArray GreenCubeLowerMask { get ; } = InputArray . Create ( GreenCubeLowerMaskData ) ;

		private InputArray GreenCubeUpperMask { get ; } = InputArray . Create ( GreenCubeUpperMaskData ) ;


		public override ThingType Thing => ThingType . GreenCube ;

		public override ThingLimit Limit => ThingLimit . MustCube ;


		public GreenCubeDetector ( FrameSource source ) : base ( source ) { }

		public override void Update ( )
		{
			Mask = Source . HsvImage . InRange ( GreenCubeLowerMask , GreenCubeUpperMask ) ;

			Mask = Mask . Dilate ( null , iterations : 2 ) ;

			Mask = Mask . MorphologyEx ( MorphTypes . Close , null ) ;
			Mask = Mask . Erode ( null , iterations : 2 ) ;
		}

	}

}
