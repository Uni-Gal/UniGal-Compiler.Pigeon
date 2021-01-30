using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniGal.Compiler.IR;

namespace UniGal.Compiler.Driver
{
	public class CompilerDriverError : CompilerError
	{
		internal static readonly ErrorCode ice_basic_errc = new() { NumericCode = 9001, Serviety = ErrorServiety.CritialError };
		/// <summary>建议少用，标准没写的错误、警告可以先用这个</summary>
		internal CompilerDriverError(ushort n, ErrorServiety serviety, string explain, IEnumerable<string> msgs) : base(new(n,serviety), msgs)
		{
			Explanation = explain;
		}
		protected CompilerDriverError(ErrorCode errc, IEnumerable<string> msgs):
			base(errc, msgs)
		{
			
		}

		public CompilerDriverError(IEnumerable<string> msgs):base(ice_basic_errc, msgs)
		{
			Explanation = "发生了严重的内部编译器错误";
		}
	}

	public class CannotLoadFactory : CompilerDriverError
	{
		public CannotLoadFactory(string backendName, string engine,IEnumerable<string> msgs):
			base(new ErrorCode(9011, ErrorServiety.Warning), msgs)
		{
			Explanation = $"无法加载后端工厂：后端名称：{backendName}，面向引擎：{engine}。";
		}
	}
}
