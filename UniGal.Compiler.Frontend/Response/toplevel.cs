using System;
using System.Collections.Generic;
using System.Globalization;
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
			while (r.Read())
			{
				if (r.NodeType == XmlNodeType.Element)
				{
					while (r.Read() && r.NodeType != XmlNodeType.EndElement)
					{
						if (r.NodeType == XmlNodeType.Element)
						{
							try
							{
								switch (r.Name)
								{
									case "src":
										ret.Source = r.Value;
										break;
									case "dst":
										ret.Target = r.Value;
										break;
									case "src_engine":
										ret.SourceEngine = r.Value;
										break;
									case "dst_engine":
										ret.TargetEngine = r.Value;
										break;
									case "src_characterset":
										ret.SourceEncoding = Encoding.GetEncoding(r.Value);
										break;
									case "dst_characterset":
										ret.TargetEncoding = Encoding.GetEncoding(r.Value);
										break;
									case "src_language":
										ret.SourceCulture = CultureInfo.GetCultureInfo(r.Value);
										break;
									case "dst_language":
										ret.SourceCulture = CultureInfo.GetCultureInfo(r.Value);
										break;
									case "comment":
										ret.Comment = on_comment(r);
										break;
									default:
										break;
								}
							}
							catch (CultureNotFoundException e)
							{
								errors.Add(new ParserError(
									9002, ErrorServiety.Warning, new string[] { e.Message, e.InvalidCultureName! },
									"出现脚本编译器不支持的语言"));
							}
							catch (ArgumentException e)
							{
								errors.Add(new ParserError(
									9002, ErrorServiety.Warning, new string[] { e.Message, e.ParamName! },
									"出现脚本编译器不支持的编码"));
							}
						}
					}
				}
			}
			return ret;
		}

		internal static EnvironmentInfo on_rtenv(XmlReader r, List<CompilerError> errors)
		{
			List<EnvironmentInfo.RedistPackage> redists = new(4);
			EnvironmentInfo.Display dispProp = new();
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
							case "display":
								dispProp = rtenv.on_display(r, errors);
								break;
							default:
								break;
						}
						break;
					default:
						break;
				}
			}

			EnvironmentInfo ret = new(dispProp, redists);
			return ret;
		}

		internal static Body on_scriptbody(XmlReader r, List<CompilerError> errors)
		{
			List<ScriptText> texts = new(40);
			Codes? code = null;
			string? comment = null;
			while (r.Read() && r.NodeType != XmlNodeType.EndElement)
			{
				if (r.NodeType == XmlNodeType.Element)
				{
					switch (r.Value)
					{
						case "code":
							code = body_code.on_code(r, errors);
							break;
						case "text":
							texts.Add(body_text.on_text(r, errors));
							break;
						case "comment":
							comment = on_comment(r);
							break;
						default:
							break;
					}
				}
			}

			Body ret = new Body()
			{
				Code = code ?? new Codes(),
				Texts = texts,
			};
			ret.Comment = comment ?? "";
			return ret;
		}
	}
}
