using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UniGal.Compiler.IR;
using UniGal.Compiler.IR.Script;
using UniGal.Compiler.IR.Script.ScriptBody;
using UniGal.Compiler.IR.Utilities;

namespace UniGal.Compiler.Frontend
{
	internal partial class responses
	{
		// 可视化设计器的注释
		internal static string on_comment(XmlReader r)
		{
			return r.ReadContentAsString();
		}
		// 元数据
		internal static Metadata on_metadata(XmlReader r, List<CompilerError> errors)
		{
			Metadata ret = new();

			// r.
			return ret;
		}

		internal static EnvironmentInfo on_rtenv(XmlReader r, List<CompilerError> errors)
		{
			uint w = 0;
			uint h = 0;
			bool fullscr=true;
			List<EnvironmentInfo.RedistPackage> redists = new(4);

			while (r.Read() && r.NodeType != XmlNodeType.EndElement)
			{

				switch (r.NodeType)
				{
					case XmlNodeType.Element:
						switch (r.Name)
						{
							case "redists":
								redists = rtenv.on_redist(r, errors);
								break;
							default:
								break;
						}
						break;
					default:
						break;
				}
			}			
			
			EnvironmentInfo ret = new(w, h, fullscr, redists);
			return ret;
		}

		internal static Body on_scriptbody(XmlReader r, List<CompilerError> errors)
		{
			Codes code = new();
			List<ScriptText> texts = new(40);

			Body ret = new Body()
			{
				Code = code,
				Texts = texts
			};
			return ret;
		}
	}
}
