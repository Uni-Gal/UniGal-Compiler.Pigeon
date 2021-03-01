using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UniGal.Compiler.IR;
using UniGal.Compiler.IR.Script;

namespace UniGal.Compiler.Frontend
{
	internal partial class responses
	{
		internal class rtenv
		{
			internal static List<EnvironmentInfo.RedistPackage> on_redist(XmlReader r, List<CompilerError> errors)
			{
				List<EnvironmentInfo.RedistPackage> ret = new(4);
				string? redistName = null;
				string? redistVer = null;
				// <redists>
				while (r.Read() && r.NodeType != XmlNodeType.Element)
				{
					// <redist ...>
					while (r.Read())
					{
						if (r.NodeType == XmlNodeType.Element)
						{
							redistName = r.Name;
							// props
							while(r.Read() && r.NodeType != XmlNodeType.EndElement)
								if (r.NodeType == XmlNodeType.Attribute && r.Name == "version")
									redistVer = r.Value;
						}
						ret.Add(new()
						{
							Name = redistName ?? throw new ParseException("某再分发包不存在Name"),
							Version = redistVer ?? ""
						});
					}
				}
				return ret;
			}

			internal static EnvironmentInfo.Display on_display(XmlReader r, List<CompilerError> errors)
			{
				uint w = 1920;
				uint h = 1080;
				bool fullscr = true;

				while (r.Read())
				{
					if(r.NodeType == XmlNodeType.Element)
					{
						switch (r.Name)
						{
							case "resolution":
								string res = r.Value;
								var xPos = res.IndexOf('x');
								try
								{
									w = uint.Parse(res.AsSpan()[0..xPos]);
									h = uint.Parse(res.AsSpan()[xPos..]);
								}
								catch (FormatException) { continue; }
								catch (OverflowException) { continue; }
								break;
							case "fullscreen":
								string strRepersent = r.Value;
								if (bool.TryParse(strRepersent, out fullscr))
									continue;
								else
									fullscr = true;
								break;
							default:
								break;
						}
					}
				}

				return new(w, h, fullscr);
			}
		}
	}
}
