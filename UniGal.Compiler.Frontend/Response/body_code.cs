using System.Collections.Generic;
using System.Xml;
using UniGal.Compiler.IR;
using UniGal.Compiler.IR.Script.ScriptBody;
using UniGal.Compiler.IR.Script.Resource;

namespace UniGal.Compiler.Frontend
{
	internal partial class responses
	{
		internal static class body_code
		{
			internal static Codes on_code(XmlReader r, List<CompilerError> errors)
			{
				List<Audio> audios = new(8);
				List<Image> images = new(10);
				List<Action> actions = new(2);
				Codes ret = new(audios, images, actions);

				while (r.Read())
				{
					if(r.NodeType == XmlNodeType.Element)
					{
						try
						{
							switch (r.Value)
							{
								case "resource":
									switch (res.check_asset(r, errors))
									{
										case asset_type.not_asset:
											goto stop_parse1;
										case asset_type.audio:
											audios.Add(res.on_audio(r));
											break;
										case asset_type.image:
											images.Add(res.on_image(r));
											break;
										default:
											goto stop_parse1;
									}
									break;
								case "action":
									break;
								case "comment":
									ret.Comment = on_comment(r);
									break;
								default:
									break;
							}
						stop_parse1:;
						}
						catch (ParseException e)
						{
							errors.Add(e.ParserError);
							continue;
						}
					}
				}

				return ret;
			}
		}
	}
}
