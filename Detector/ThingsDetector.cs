using System ;
using System . Collections . Generic ;
using System . Linq ;

using OpenCvSharp ;

namespace Detector
{

	public abstract class ThingsDetector
	{

		protected Mat Mask { get ; set ; } = new Mat ( ) ;

		public abstract ThingType Thing { get ; }

		public abstract ThingLimit Limit { get ; }

		public FrameSource Source { get ; }

		protected ThingsDetector ( FrameSource source ) { Source = source ; }

		public abstract void Update ( ) ;

		public static int GetApprox ( Point [ ] contor )
		{
			double peri = Cv2 . ArcLength ( contor , true ) ;
			Point [ ] approx = Cv2 . ApproxPolyDP ( contor , 0.02 * peri , true ) ;
			return approx . Count ( ) ;
		}

		public static bool IsCircle ( Point [ ] contor ) { return GetApprox ( contor ) > 7 ; }


		public virtual (ThingType thing , Point2f position , double size) ? GetThing ( )
		{
			Cv2 . FindContours ( Mask ,
								out Point [ ] [ ] cnts ,
								out HierarchyIndex [ ] hierarchyIndices ,
								RetrievalModes . External ,
								ContourApproximationModes . ApproxSimple ) ;
			Mat color = Mask . CvtColor ( ColorConversionCodes . GRAY2BGR ) ;

			if ( cnts . Any ( ) )
			{
				Point [ ] firstItem = cnts . OrderByDescending ( points => Cv2 . ContourArea ( points ) ) .
											First ( ) ;

				Cv2 . DrawContours ( color , new [ ] { firstItem } , - 1 , Scalar . Red ) ;


				bool isCircle = IsCircle ( firstItem ) ;


				Cv2 . MinEnclosingCircle ( firstItem , out Point2f center , out float radius ) ;

				double size = Cv2 . ContourArea ( firstItem ) ;


				Cv2 . PutText ( color ,
								$"{size} by {GetApprox ( firstItem )}" ,
								center ,
								HersheyFonts . HersheySimplex ,
								1 ,
								Scalar . Red ) ;

				Cv2 . ImShow ( GetType ( ) . Name , color ) ;

				if ( size < 500 ||
					! isCircle && Limit == ThingLimit . MustCircle ||
					isCircle && Limit == ThingLimit . MustCube )
				{
					return null ;
				}


				return ( Thing , center , size ) ;
			}
			else
			{
				Cv2 . ImShow ( GetType ( ) . Name , color ) ;

				return null ;
			}
		}

	}

}
