using System.Collections.Generic;
using UniGal.Compiler.IR;
#pragma warning disable CS1591
namespace UniGal.Compiler.Backend
{
	/// <summary>
	/// 代表后端发出的编译器错误
	/// </summary>
	public class BackendError : CompilerError
	{
		protected BackendError(ErrorCode errc, IEnumerable<string> messages) : base(errc, messages)
		{
			Explaination = "后端发生错误";
		}

		protected BackendError(ushort id, ErrorServiety serviety, IEnumerable<string> msgs) : this(new(id, serviety), msgs) { }
	}
}
