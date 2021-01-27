using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using UniGal.Compiler.IR;
using UniGal.Compiler.Backend;
using UniGal.Compiler.Frontend;

namespace UniGal.Compiler.Driver
{
	public class CompileDriver
	{
		private readonly CompileOptions opt;
		private readonly List<IR.CompilerError> errors;
		private readonly List<BackendRecord> backends;
		public DirectoryInfo OutputDirectory => opt.OutDir;
		public IEnumerable<FileInfo> SourceFiles => opt.Sources;
		public IEnumerable<IR.CompilerError> Errors => errors;
		public CompileDriver(CompileOptions options)
		{
			opt = options;
			errors = new(48);
			backends = new(8);
		}

		public void LoadBackends()
		{
			DirectoryInfo backendDir = new(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backends"));
			foreach (FileInfo file in backendDir.EnumerateFiles())
			{
				if (file.Extension != ".dll")
					continue;

				Assembly asm = Assembly.LoadFile(file.FullName);

				UniGalBackendAssemblyAttribute? attrasm = asm.GetCustomAttribute<UniGalBackendAssemblyAttribute>();
				if (attrasm == null) continue;

				foreach (Type t in asm.GetTypes())
				{
					try
					{
						var attrfactory = t.GetCustomAttribute<UniGalBackendFactoryAttribute>(false);
						if (attrfactory == null) continue;

						var ctor = t.GetConstructor(Array.Empty<Type>());
						if (ctor == null) continue;

						object obj = ctor.Invoke(null);
						if (obj is not BackendFactory) continue;

						backends.Add(new((BackendFactory)obj, attrasm.BackendName, attrasm.TargetEngine, attrfactory.Version));
					}
					catch (MemberAccessException e)
					{
						var msg = new string[] { "无法访问后端工厂的构造器，这通常意味着后端工厂有问题", e.Message };
						errors.Add(new CannotLoadBackend(attrasm.BackendName, attrasm.TargetEngine, msg));
						continue;
					}
					catch (TargetInvocationException e)
					{
						Console.WriteLine("无法创建后端工厂");
						Console.WriteLine(e);
						continue;
					}
				}

			}
		}

		public void BeginCompile()
		{

		}
	}
}
