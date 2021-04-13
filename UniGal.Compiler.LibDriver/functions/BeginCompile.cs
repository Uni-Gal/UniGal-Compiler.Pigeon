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
		/// <summary>
		/// 开始编译
		/// </summary>
		public void BeginCompile()
		{
			foreach (FileInfo src in opt.Sources)
			{
				FileStream srcStream = src.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
				TextReader r = new StreamReader(srcStream, Encoding.UTF8, false);
				using Parser p = new(r);

				if (p.Parse())
				{
					errors.AddRange(p.Errors);
					OnErrorsAdded?.Invoke(p.Errors);

					ScriptDom dom = p.Dom;
					try
					{
						BackendRecord backendFactory;

						if (opt.BackendName == null)
							backendFactory = select_factory(opt.TargetEngine ?? dom.Metadata.TargetEngine,
								opt.TargetLanguage, opt.BackendVersion);
						else
							backendFactory = backends.Find((b) => b.Name == opt.BackendName) ??
								select_factory(opt.TargetEngine ?? dom.Metadata.TargetEngine,
								opt.TargetLanguage, opt.BackendVersion);

						Backend.IBackend backend = backendFactory.Factory.CreateBackend(
							OutputDirectory, opt.TargetLanguage);
						backend.Compile(dom);

						if (backend.HasCriticialError)
						{
							errors.AddRange(backend.Errors);
							OnErrorsAdded?.Invoke(backend.Errors);
							continue;
						}
					}
					catch (BackendNotFoundException e)
					{
						errors.Add(new CompilerDriverError(9013, ErrorServiety.CritialError,
							e.ToString(), new string[] { e.Engine, e.Version }));
						OnErrorsAdded?.Invoke(errors.GetRange(errors.Count-1, 1));
					}
				}
				else
				{
					var errs = p.Errors;
					errors.AddRange(errs);
					OnErrorsAdded?.Invoke(errs);
					continue;
				}
			}
		}
	}
}