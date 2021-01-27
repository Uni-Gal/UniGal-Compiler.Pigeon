using System;

namespace UniGal.Compiler.Backend
{
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
