using System ;
using System . Collections . Generic ;
using System . Linq ;

using JetBrains . Annotations ;

using OpenCvSharp ;

namespace Detector
{

	[PublicAPI]
	[Detector]
	public class TennisDetector : ThingsDetector
	{

		public static byte [ ] TennisLowerMaskData { get ; } = { 30 , 100 , 50 } ;

		public static byte [ ] TennisUpperMaskData { get ; } = { 40 , 255 , 255 } ;

		private InputArray TennisLowerMask { get ; } = InputArray . Create ( TennisLowerMaskData ) ;

		private InputArray TennisUpperMask { get ; } = InputArray . Create ( TennisUpperMaskData ) ;


		public override ThingType Thing => ThingType . Tennis ;

		public override ThingLimit Limit => ThingLimit . MustCircle ;

		public TennisDetector ( FrameSource source ) : base ( source ) { }

		public override void Update ( )
		{
			Mask = Source . HsvImage . InRange ( TennisLowerMask , TennisUpperMask ) ;

			Mask = Mask . Dilate ( null , iterations : 2 ) ;
			Mask = Mask . MorphologyEx ( MorphTypes . Close , null ) ;
			Mask = Mask . Erode ( null , iterations : 2 ) ;
		}

	}

}
