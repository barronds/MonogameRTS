using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using RTS.UnitTests;

namespace RTS.RTSMath
{
	struct tCoord
	{
		public int x { get; set; }
		public int y { get; set; }

		public tCoord( int x, int y )
		{
			this.x = x;
			this.y = y;
		}

		public tCoord( double x, double y )
		{
			this.x = (int)x;
			this.y =(int)y;
		}

		public float getLength()
		{
			return (float)System.Math.Sqrt( this.x * this.x + this.y * this.y );
		}

		public tCoord clamp( tCoord min, tCoord max )
		{
			int clamped_x = System.Math.Max( System.Math.Min( max.x, this.x ), min.x );
			int clamped_y = System.Math.Max( System.Math.Min( max.y, this.y ), min.y );
			return new tCoord( clamped_x, clamped_y );
		}

		public static tCoord operator-( tCoord coord )
		{
			return new tCoord( 0 - coord.x, 0 - coord.y );
		}

		public static tCoord operator *( float s, tCoord coord )
		{
			return new tCoord( coord.x * s, coord.y * s );
		}

		public static tCoord operator *( tCoord coord, float s )
		{
			return s * coord;
		}

		public static tCoord operator+( tCoord a, tCoord b )
		{
			return new tCoord( a.x + b.x, a.y + b.y );
		}

		public static tCoord operator-( tCoord a, tCoord b )
		{
			return new tCoord( a.x - b.x, a.y - b.y );
		}

		public static void unitTest()
		{
			//Framework.Assert( false, "hello" );
		}
	}
}
