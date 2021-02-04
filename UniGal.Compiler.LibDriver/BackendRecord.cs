using System;
using System.Collections.Generic;

using UniGal.Compiler.Backend;

namespace UniGal.Compiler.LibDriver
{
	internal record backend_record(IBackendFactory Factory, string Name, string Engine, string Version);
}
