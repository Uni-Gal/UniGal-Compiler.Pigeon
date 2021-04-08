using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;

using UniGal.Compiler.Backend;

namespace UniGal.Compiler.LibDriver
{
	/// <summary>
	/// 找不到合适的后端时触发
	/// </summary>
	[Serializable]
	public class BackendNotFoundException : KeyNotFoundException
	{
#pragma warning disable CS1591
		private const string datasite = "UniGal.Compiler.LibDriver.BackendNotFoundException ";
		internal BackendNotFoundException(string engine, string name, string lang, string? version)
		{
			Data.Add(datasite + "Engine", engine);
			Data.Add(datasite + "Name", name);
			Data.Add(datasite + "Version", version ?? "AutoSelect");
			Data.Add(datasite + "Language", lang);
		}
		protected BackendNotFoundException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

		public string Engine { get => (string)Data[datasite + "Engine"]!; }
		public string Name { get => (string)Data[datasite + "Name"]!; }
		public string Version { get => (string)Data[datasite + "Version"]!; }
		public string Language { get => (string)Data[datasite + "Language"]!; }


		public static readonly IR.ErrorCode ErrorCode = new(6, IR.ErrorServiety.CritialError);
#pragma warning restore
	}

	sealed partial class CompileDriver
	{
		internal partial BackendRecord select_factory(string engine, string name, string language, string? version)
		{
			foreach (BackendRecord rec in backends)
			{
				if (rec.Engine != engine)
					continue;

				if (rec.Name != name)
					continue;

				if (!string.IsNullOrEmpty(version) && rec.Version != version)
					continue;
				
				if(rec.Factory.SupportedLanguages.Contains(language))
					return rec;
			}

			throw new BackendNotFoundException(engine, name, language, version);
		}
	}
}