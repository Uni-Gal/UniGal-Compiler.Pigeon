using System.IO;
using System.Reflection;
using System;

using UniGal.Compiler.Backend;
#pragma warning disable CS1591
namespace UniGal.Compiler.LibDriver
{
	sealed partial class CompileDriver
	{
		/// <summary>
		/// 加载后端
		/// </summary
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
								errors.Add(new CannotLoadFactory(attrasm.BackendName, attrasm.TargetEngine, new[] { "¿你整个泛型类当工厂，我怎么创建？" }));
								continue;
							}

							var attrfactory = t.GetCustomAttribute<UniGalBackendFactoryAttribute>(false);
							if (attrfactory == null) continue;

							ConstructorInfo ctor = t.GetConstructor(ctorflags, null, Type.EmptyTypes, null)
								?? throw new MissingMethodException(t.FullName, ".ctor");
							object obj = ctor.Invoke(null);

							if (obj is IBackendFactory f)
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
						"无法加载后端程序集" + e.FileName,
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
	}
}