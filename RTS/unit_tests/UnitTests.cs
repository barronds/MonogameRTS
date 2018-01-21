using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTS.math;

namespace RTS.unit_tests
{
	public class UnitTests
	{
		private static bool sPerformUnitTests = true;

		public static void Assert( bool b, string msg = null )
		{
			if( !b )
			{
				// breakpoint here
				System.Diagnostics.Debug.Assert( b, msg );
			}
		}

		public static void RunUnitTests()
		{
			if( !sPerformUnitTests )
			{
				return;
			}

			tCoord.unitTest();
		}
	}
}
