using System.Collections.Generic;
using UniGal.Compiler.IR;

namespace UniGal.Compiler.Backend
{
	public class BackendError : CompilerError
	{
		protected BackendError(ErrorCode errc, IEnumerable<string> messages) : base(errc, messages)
		{
			Explaination = "后端发生错误";
		}

		protected BackendError(ushort id, ErrorServiety serviety, IEnumerable<string> msgs) : this(new(id, serviety), msgs) { }
	}
}
