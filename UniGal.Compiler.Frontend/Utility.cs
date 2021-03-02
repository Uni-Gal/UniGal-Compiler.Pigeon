using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniGal.Compiler.Frontend
{
	internal static class util
	{
		[return: NotNull]
		internal static T assert_notnull<T>(T? val)
		{
			if (val == null)
			{
				System.Diagnostics.Debug.Fail("Assertion failure, a non nullable value is null.");
			}
			return val!;
		}

	}
}
