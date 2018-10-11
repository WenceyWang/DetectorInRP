using System ;
using System . Collections . Generic ;
using System . Diagnostics ;
using System . Linq ;

using JetBrains . Annotations ;

using OpenCvSharp ;

namespace Detector
{

	[PublicAPI]
	[Detector]
	public class PcmacgDetector : ThingsDetector
	{

		public static byte[] PcmacgBrownLowerMaskData { get; } = { 105, 100, 100 };

		public static byte[] PcmacgBrownUpperMaskData { get; } = { 110, 255, 255 };


		private InputArray PcmacgBrownLowerMask { get; } = InputArray.Create(PcmacgBrownLowerMaskData);

		private InputArray PcmacgBrownUpperMask { get; } = InputArray.Create(PcmacgBrownUpperMaskData);

		public static byte[] PcmacgOrangeLowerMaskData { get; } = { 92, 100, 200 };

		public static byte[] PcmacgOrangeUpperMaskData { get; } = { 97, 255, 255 };


		public static byte[] PcmacgSkinLowerMaskData { get; } = { 60, 45, 32 };

		public static byte[] PcmacgSkinUpperMaskData { get; } = { 80, 125, 115};


		private InputArray PcmacgSkinLowerMask { get; } = InputArray.Create(PcmacgSkinUpperMaskData);

		private InputArray PcmacgSkinUpperMask { get; } = InputArray.Create(PcmacgSkinLowerMaskData);


		private InputArray PcmacgOrangeLowerMask { get; } = InputArray.Create(PcmacgOrangeLowerMaskData);

		private InputArray PcmacgOrangeUpperMask { get; } = InputArray.Create(PcmacgOrangeUpperMaskData);


		public override ThingType Thing => ThingType.Pcmacg;

		public override ThingLimit Limit => ThingLimit.DoNotCare;


		public PcmacgDetector(FrameSource source) : base(source) { }

		public override void Update()
		{
			Mat brownMask = Source.InversedHsvImage.InRange(PcmacgBrownLowerMask, PcmacgBrownUpperMask);
			Mat orangeMask = Source.InversedHsvImage.InRange(PcmacgOrangeLowerMask, PcmacgOrangeUpperMask);
			Mat skinMask = Source.InversedHsvImage.InRange(PcmacgSkinLowerMask, PcmacgSkinUpperMask);

			Cv2.BitwiseOr(brownMask, orangeMask, Mask);
			Cv2.BitwiseOr(Mask, skinMask, Mask);

			Vec3b a= Source . InversedHsvImage . At<Vec3b> ( 107,368) ;

			Debug . WriteLine ( $"{a.Item0},{a.Item1},{a.Item2}") ;

			Mask = Mask.Dilate(null, iterations: 5);

			Mask = Mask.MorphologyEx(MorphTypes.Close, null, iterations: 3);
			Mask = Mask.Erode(null, iterations: 4);
		}

	}

}