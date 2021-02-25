using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniGal.Compiler.IR;

namespace UniGal.Compiler.Frontend
{
	internal class parser_error : CompilerError
	{

		public parser_error(ushort errc, ErrorServiety serviety, IEnumerable<string> msgs, string explain):base(new(errc,serviety), msgs)
		{
			Explaination = explain;
		}
	}
}
