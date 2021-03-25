using System.Collections.Generic;
using System.Xml;
using UniGal.Compiler.IR;
using UniGal.Compiler.IR.Script.ScriptBody;
using UniGal.Compiler.IR.Script.Resource;

namespace UniGal.Compiler.Frontend
{
	internal partial class Responses
	{
		internal static class BodyCode
		{
			internal static Codes on_code(XmlReader r, List<CompilerError> errors)
			{
				List<Audio> audios = new(8);
				List<Image> images = new(10);
				List<ActionRecord> actions = new(2);
				Codes ret = new(audios, images, actions);

				while (r.Read() && r.NodeType != XmlNodeType.EndElement && r.Name != "code")
				{
					if(r.NodeType == XmlNodeType.Element)
					{
						try
						{
							switch (r.Value)
							{
								case "resource":
									switch (Resource.check_asset(r, errors))
									{
										case asset_type.not_asset:
											goto stop_parse1;
										case asset_type.audio:
											audios.Add(Resource.on_audio(r, errors));
											break;
										case asset_type.image:
											images.Add(Resource.on_image(r, errors));
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
