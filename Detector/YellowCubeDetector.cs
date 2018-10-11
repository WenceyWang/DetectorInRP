using System ;
using System . Collections . Generic ;
using System . Linq ;

using JetBrains . Annotations ;

using OpenCvSharp ;

namespace Detector
{

	[PublicAPI]
	[Detector]
	public class YellowCubeDetector : ThingsDetector
	{

		public static byte [ ] YellowCubeLowerMaskData { get ; } = { 15 , 150 , 70 } ;

		public static byte [ ] YellowCubeUpperMaskData { get ; } = { 45 , 255 , 255 } ;


		private InputArray YellowCubeLowerMask { get ; } = InputArray . Create ( YellowCubeLowerMaskData ) ;

		private InputArray YellowCubeUpperMask { get ; } = InputArray . Create ( YellowCubeUpperMaskData ) ;


		public override ThingType Thing => ThingType . YellowCube ;

		public override ThingLimit Limit => ThingLimit . MustCube ;


		public YellowCubeDetector ( FrameSource source ) : base ( source ) { }

		public override void Update ( )
		{
			Mask = Source . HsvImage . InRange ( YellowCubeLowerMask , YellowCubeUpperMask ) ;

			Mask = Mask . Dilate ( null , iterations : 2 ) ;

			Mask = Mask . MorphologyEx ( MorphTypes . Close , null ) ;
			Mask = Mask . Erode ( null , iterations : 2 ) ;
		}

	}

}
