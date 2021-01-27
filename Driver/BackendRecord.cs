using System;
using System.Collections.Generic;

using UniGal.Compiler.Backend;

namespace UniGal.Compiler.Driver
{
	internal record BackendRecord(BackendFactory Factory, string Name, string Engine, string Version);
}
