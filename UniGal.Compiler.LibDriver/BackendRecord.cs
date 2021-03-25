using System;
using System.Collections.Generic;

using UniGal.Compiler.Backend;

namespace UniGal.Compiler.LibDriver
{
	/// <summary>
	/// 
	/// </summary>
	public record BackendRecord(IBackendFactory Factory, string Name, string Engine, string Version);
}
