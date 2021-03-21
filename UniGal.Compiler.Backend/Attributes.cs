using System;
#pragma warning disable CS1591
namespace UniGal.Compiler.Backend
{
	/// <summary>
	/// 用于标记一个后端程序集
	/// </summary>
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public class UniGalBackendAssemblyAttribute : Attribute
	{
		public string BackendName { get; private set; }
		public string TargetEngine { get; private set; }
		public UniGalBackendAssemblyAttribute(string name, string targetEngine, string[] targetLanguages)
		{
			BackendName = name;
			TargetEngine = targetEngine;
		}
	}

	/// <summary>
	/// 用于标记一个后端工厂类
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class UniGalBackendFactoryAttribute : Attribute
	{
		public string Version { get; set; }
		public UniGalBackendFactoryAttribute()
		{
			Version = "1.0.0.0";
		}
	}
}
