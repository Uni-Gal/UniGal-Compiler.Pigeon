using System.Collections.Generic;

namespace UniGal.Compiler.Backend
{
	public class BackendError : IR.CompilerError
	{
		public BackendError(IR.ErrorCode errc, IEnumerable<string> messages) : base(errc, messages)
		{

		}
	}
}
