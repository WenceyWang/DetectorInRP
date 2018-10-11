using System ;
using System . Collections . Generic ;
using System . Linq ;

using JetBrains . Annotations ;

using OpenCvSharp ;

namespace Detector
{

	[PublicAPI]
	[Detector]
	public class SpriteDetector : ThingsDetector
	{

		public static byte [ ] SpriteBlueLowerMaskData { get ; } = { 95 , 100 , 80 } ;

		public static byte [ ] SpriteBlueUpperMaskData { get ; } = { 115 , 255 , 255 } ;


		private InputArray SpriteBlueLowerMask { get ; } = InputArray . Create ( SpriteBlueLowerMaskData ) ;

		private InputArray SpriteBlueUpperMask { get ; } = InputArray . Create ( SpriteBlueUpperMaskData ) ;

		public static byte [ ] SpriteGreenLowerMaskData { get ; } = { 70 , 100 , 80 } ;

		public static byte [ ] SpriteGreenUpperMaskData { get ; } = { 80 , 255 , 255 } ;


		private InputArray SpriteGreenLowerMask { get ; } = InputArray . Create ( SpriteGreenLowerMaskData ) ;

		private InputArray SpriteGreenUpperMask { get ; } = InputArray . Create ( SpriteGreenUpperMaskData ) ;


		public override ThingType Thing => ThingType . Sprite ;

		public override ThingLimit Limit => ThingLimit . DoNotCare ;


		public SpriteDetector ( FrameSource source ) : base ( source ) { }

		public override void Update ( )
		{
			Mat blueMask = Source . HsvImage . InRange ( SpriteBlueLowerMask , SpriteBlueUpperMask ) ;

			Mat greenMask = Source . HsvImage . InRange ( SpriteGreenLowerMask , SpriteGreenUpperMask ) ;

			Cv2 . BitwiseOr ( blueMask , greenMask , Mask ) ;


			Mask = Mask . Dilate ( null , iterations : 4 ) ;

			Mask = Mask . MorphologyEx ( MorphTypes . Close , null , iterations : 3 ) ;
			Mask = Mask . Erode ( null , iterations : 4 ) ;
		}

	}

}
