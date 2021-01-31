using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;

using UniGal.Compiler.Backend;

namespace UniGal.Compiler.LibDriver
{


	[Serializable]
	public class BackendNotFoundException : KeyNotFoundException
	{
		private const string datasite = "UniGal.Compiler.LibDriver.BackendNotFoundException ";
		internal BackendNotFoundException(string engine, string name, string? version)
		{
			Data.Add(datasite + "Engine", engine);
			Data.Add(datasite + "Name", name);
			Data.Add(datasite + "Version", version ?? "AutoSelect");
		}
		protected BackendNotFoundException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

		public string Engine { get => (string)Data[datasite + "Engine"]!; }
		public string Name { get => (string)Data[datasite + "Name"]!; }
		public string Version { get => (string)Data[datasite + "Version"]!; }

		public static readonly IR.ErrorCode ErrorCode = new(0006, IR.ErrorServiety.CritialError);
	}

	sealed partial class CompileDriver
	{
		internal partial BackendRecord SelectBackend(string engine, string name, string? version)
		{
			foreach (BackendRecord rec in backends)
			{
				if (rec.Engine != engine)
					continue;

				if (rec.Name != name)
					continue;

				if (version != null && rec.Version != version)
					continue;
				else
					return rec;
			}

			throw new BackendNotFoundException(engine, name, version);
		}
	}
}