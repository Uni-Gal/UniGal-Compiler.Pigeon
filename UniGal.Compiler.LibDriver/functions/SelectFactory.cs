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
		internal BackendNotFoundException(string engine, string? lang, string? version)
		{
			Data.Add(datasite + "Engine", engine);
			Data.Add(datasite + "Version", version ?? "AutoSelect");
			Data.Add(datasite + "Language", lang ?? "AutoSelect");
		}
		protected BackendNotFoundException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

		public string Engine { get => (string)Data[datasite + "Engine"]!; }
		public string Version { get => (string)Data[datasite + "Version"]!; }
		public string Language { get => (string)Data[datasite + "Language"]!; }


		public static readonly IR.ErrorCode ErrorCode = new(6, IR.ErrorServiety.CritialError);
#pragma warning restore
	}

	sealed partial class CompileDriver
	{
		internal BackendRecord select_factory(string engine, string? language, string? version)
		{
			foreach (BackendRecord rec in backends)
			{
				if (rec.Engine != engine)
					continue;

				if (!string.IsNullOrEmpty(version) && rec.Version != version)
					continue;
				
				if(language == null || rec.Factory.SupportedLanguages.Contains(language))
					return rec;
			}

			throw new BackendNotFoundException(engine, language, version);
		}
	}
}