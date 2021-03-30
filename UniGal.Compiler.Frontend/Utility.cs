using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System;

namespace UniGal.Compiler.Frontend
{
	internal static class util
	{
		[return: NotNull]
		internal static T assert_notnull<T>(T? val)
		{
			if (val == null)
			{
				Debug.Fail("A non nullable assigned a null value");
				throw new ArgumentNullException(nameof(val), "A non nullable assigned a null value");
			}
			return val;
		}

	}
}
