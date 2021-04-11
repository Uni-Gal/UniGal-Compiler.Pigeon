using System.Text;
using System.IO;
using System.Collections.Generic;
using System;

using UniGal.Compiler.Frontend;
using UniGal.Compiler.IR;
#pragma warning disable CS1591
namespace UniGal.Compiler.LibDriver
{
	sealed partial class CompileDriver
	{
		public partial void BeginCompile()
		{
			foreach (FileInfo src in opt.Sources)
			{
				FileStream srcStream = src.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
				TextReader r = new StreamReader(srcStream, Encoding.UTF8, false);
				using Parser p = new(r);

				if(p.Parse())
				{
					ScriptDom dom = p.Dom;

					// TODO：null以后改成从opt中获得
					BackendRecord backendFactory =
						select_factory(dom.Metadata.TargetEngine,
						dom.Metadata.TargetLanguage, opt.BackendVersion);
					Backend.IBackend backend = backendFactory.Factory.CreateBackend(OutputDirectory, dom.Metadata.TargetLanguage);

					backend.Compile(dom);

					if(backend.HasCriticialError)
					{
						// 输出错误信息，不停止

					}
				}
				else
				{
					// 输出错误信息，不停止
					continue;
				}
			}
		}
	}
}