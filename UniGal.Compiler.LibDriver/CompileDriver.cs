using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using UniGal.Compiler.IR;
using UniGal.Compiler.Backend;
using UniGal.Compiler.Frontend;
using System.Security;

namespace UniGal.Compiler.LibDriver
{
	public class CompileDriver
	{
		private readonly CompileOptions opt;
		private readonly List<CompilerError> errors;
		private readonly List<BackendRecord> backends;

		
		public DirectoryInfo OutputDirectory => opt.OutDir;
		public IEnumerable<FileInfo> SourceFiles => opt.Sources;
		public IEnumerable<CompilerError> Errors => errors;
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

				try
				{
					Assembly asm = Assembly.LoadFrom(file.FullName);
					

					UniGalBackendAssemblyAttribute? attrasm = asm.GetCustomAttribute<UniGalBackendAssemblyAttribute>();
					if (attrasm == null) continue;

					foreach (Type t in asm.GetTypes())
					{
						try
						{
							// ctor = Constructor
							const BindingFlags ctorflags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance;

							if (t.ContainsGenericParameters)
							{
								// TODO: 这里该报个什么错
								// 弄个泛型类怎么整？
								// 虚空创建¿
								continue;
							}

							var attrfactory = t.GetCustomAttribute<UniGalBackendFactoryAttribute>(false);
							if (attrfactory == null) continue;

							object? obj = t.InvokeMember("", ctorflags, null, null, null);
							if (obj is BackendFactory f)
								backends.Add(new(f, attrasm.BackendName, attrasm.TargetEngine, attrfactory.Version));
							else continue;
						}
						catch (MissingMethodException e)
						{
							var msg = new string[] { "该工厂缺少无参构造函数", e.ToString() };
							errors.Add(new CannotLoadFactory(attrasm.BackendName, attrasm.TargetEngine, msg));
							continue;
						}
						catch (MemberAccessException e)
						{
							var msg = new string[] { "无法访问后端工厂的构造器，这通常意味着后端工厂有问题", e.ToString() };
							errors.Add(new CannotLoadFactory(attrasm.BackendName, attrasm.TargetEngine, msg));
							continue;
						}
						catch (NotSupportedException e)
						{
							var msg = new string[] { e.ToString(), "说吧，你在整什么活" };
							errors.Add(new CannotLoadFactory(attrasm.BackendName, attrasm.TargetEngine, msg));
							continue;
						}
						catch (Exception e)
						{
							var msg = new string[]
							{
							"创建工厂时出现了异常",
							e.ToString()
							};
							errors.Add(new CannotLoadFactory(attrasm.BackendName, attrasm.TargetEngine, msg));
							continue;
						}
					}
				}
				catch (FileLoadException e)
				{
					errors.Add(new CannotLoadBackend(file.FullName, new string[]
					{
						"无法加载后端程序集" + e.FileName!,
						e.Message
					}));
					continue;

				}
				catch (BadImageFormatException e)
				{
					errors.Add(new CannotLoadBackend(file.FullName, new string[]
					{
						"无法加载后端程序集" + e.FileName,
						e.Message
					}));
					continue;
				}

			}
		}

		public void BeginCompile()
		{

		}
	}
}
