using System ;
using System . Collections . Generic ;
using System . Diagnostics ;
using System . Linq ;
using System . Reflection ;

using OpenCvSharp ;

namespace Detector
{

	public static class Program
	{

		public static List <ThingsDetector> Detectors { get ; } = new List <ThingsDetector> ( ) ;

		public static bool Loaded { get ; set ; }

		public static object Locker { get ; set ; } = new object ( ) ;


		//public static byte[] CokeLowerMask { get; } = { 70, 50, 50 };
		//public static byte[] CokeUpperMask { get; } = { 90, 255, 255 };

		public static readonly FrameSource Source = new FrameSource ( ) ;


		public static void LoadAll ( )
		{
			lock ( Locker )
			{
				if ( Loaded )
				{
					return ;
				}

				//Todo:Load All internal type
				foreach ( TypeInfo type in typeof ( Program ) . GetTypeInfo ( ) .
																Assembly . DefinedTypes .
																Where ( type => type . GetCustomAttributes ( typeof ( DetectorAttribute ) , false ) . Any ( )
																				&& typeof ( ThingsDetector ) . GetTypeInfo ( ) . IsAssignableFrom ( type ) ) )
				{
					Detectors . Add ( Activator . CreateInstance ( type , Source ) as ThingsDetector ) ;
				}

				Loaded = true ;
			}
		}


		public static void Main ( string [ ] args )
		{
			LoadAll ( ) ;

			(ThingType thing , Point2f position , double size) lastThing =
				default ( (ThingType thing , Point2f position , double size) ) ;

			int lastThingCount = 0 ;

			DateTime now = DateTime . Now ;

			int a = 0 ;

			VideoCapture capture = new VideoCapture ( 0 ) ;

			//Console.WriteLine("Hello World!");
			for ( int i = 20 ; ; i++ )
			{
				using ( Mat frame = new Mat ( ) )
				{
					capture . Read ( frame ) ;
					lock ( Source )
					{
						Source . UpdateFrame ( frame ) ;
					}

					List <(ThingType thing , Point2f position , double size)> things =
						new List <(ThingType thing , Point2f position , double size)> ( ) ;


					foreach ( ThingsDetector detector in Detectors )
					{
						detector . Update ( ) ;
					}

					foreach ( ThingsDetector detector in Detectors )
					{
						(ThingType thing , Point2f position , double size) ? thing = detector . GetThing ( ) ;
						if ( thing != null )
						{
							things . Add ( thing . Value ) ;
						}
					}

					//Mat mask = new Mat();

					if ( things . Any (  /*item => item . size < 50000 */ ) )
					{
						(ThingType thing , Point2f position , double size) selectedThing =
							things /* . Where ( item => item . size < 50000 )*/ . OrderByDescending ( thing => thing . size ) . First ( ) ;

						if ( ( selectedThing . position - lastThing . position ) . DistanceTo ( new Point2f ( 0 , 0 ) ) < 15 &&
							selectedThing . size - lastThing . size < Math . Max ( selectedThing . size , lastThing . size ) * 0.3 &&
							lastThing . thing == selectedThing . thing )
						{

							lastThingCount++;
							Debug.WriteLine($"{a++} {lastThingCount} {selectedThing.thing}");
							if ( lastThingCount>=20 )
							{

								Console.WriteLine($"{lastThingCount} {selectedThing.thing}");

							}

						}
						else
						{
							lastThingCount = 0 ;
						}



						Cv2 . Circle ( frame ,
										selectedThing . position ,
										( int ) Math . Sqrt ( selectedThing . size / Math . PI ) ,
										Scalar . Black ,
										2 ) ;
						Cv2 . PutText ( frame ,
										selectedThing . thing + selectedThing . size . ToString ( ) ,
										selectedThing . position ,
										HersheyFonts . HersheySimplex ,
										1 ,
										Scalar . Red ) ;
					}


					Cv2 . ImShow ( nameof(frame) , frame ) ;


					Cv2 . WaitKey ( 1 ) ;


					//	Cv2.BitwiseNot(frame, frame);
					//	Mat grayImage = frame.CvtColor(ColorConversionCodes.BGR2GRAY);
					//	Mat hsvImage = frame.CvtColor(ColorConversionCodes.BGR2HSV);

					//	Cv2.ImShow(nameof(hsvImage), hsvImage);

					//	//
					//	//Vec3b color2 = hsvImage.At<Vec3b>(234, 296);


					//	Mat cokeMask = hsvImage.InRange(InputArray.Create(CokeLowerMask), InputArray.Create(CokeUpperMask));
					//	//Cv2.BitwiseNot(greenMask, greenMask);


					//	cokeMask = cokeMask.Erode(null, iterations: 2);
					//	cokeMask = cokeMask.MorphologyEx(MorphTypes.Close, null);
					//	cokeMask = cokeMask.Dilate(null, iterations: 2);

					//	Cv2.ImShow(nameof(cokeMask), cokeMask);


					//	Cv2.FindContours(cokeMask, out Point[][] points, out HierarchyIndex[] hie, RetrievalModes.External,
					//		ContourApproximationModes.ApproxSimple);

					//	List<Point[]> pointLists = points.ToList();
					//	pointLists.Sort((points1, points2) => (int)(10 * (Cv2.ContourArea(points2) - Cv2.ContourArea(points1))));

					//	Console.WriteLine($"CodaSize:{Cv2.ContourArea(pointLists.First())}");
					//	Console.WriteLine($"AppleSize:{Cv2.ContourArea(pointLists.First())}");

					//	Cv2.MinEnclosingCircle(pointLists.First(), out Point2f center, out float radius);

					//	Cv2.Circle(frame, center, (int)radius, Scalar.Black, 2);

					//	Cv2.ImShow(nameof(frame), frame);

					//	Mat edges = new Mat();

					//	Cv2.Canny(grayImage, edges, 300, 200);


					//	Cv2.ImShow("res", grayImage);

					//	Cv2.ImShow("dege", edges);

					//	//var peri = Cv2.ArcLength(c, true)
					//	//approx = Cv2.ApproxPolyDP(c, 0.04 * peri, true)

					//	Cv2.WaitKey();
					//	//Console.ReadLine();
				}

				GC . Collect ( ) ;
			}

			Console . WriteLine ( DateTime . Now - now ) ;
			Console . ReadLine ( ) ;

			//Console.ReadLine();
		}

	}

}
