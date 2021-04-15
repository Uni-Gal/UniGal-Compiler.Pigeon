using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using UniGal.Compiler.IR;
using UniGal.Compiler.IR.Script.ScriptBody;
using UniGal.Compiler.IR.Utilities;

namespace UniGal.Compiler.Frontend
{
	internal partial class Responses
	{
		// 体量原因，特别放出来
		internal static class BodyText
		{
			internal static CharacterInfo on_character(XmlDocument dom, List<CompilerError> errors)
			{
				CharacterInfo character;
				if (dom.ChildNodes.Count != 0)
				{
					XmlNode? colorNode =
						dom.SelectSingleNode("./character/color") ??
						dom.SelectSingleNode("./character/colour");
					string colorStr = "#00000000";
					Color charcolor;
					if (colorNode != null)
					{
						string colorRaw = colorNode.InnerText;

						if (colorRaw.StartsWith('#'))
							if (colorRaw.Length == 7)
								colorStr = colorRaw + "00";
							else
								colorStr = colorRaw;
						try
						{
								charcolor = new Color()
								{
									R = byte.Parse(colorStr.AsSpan().Slice(1, 2), NumberStyles.HexNumber),
									G = byte.Parse(colorStr.AsSpan().Slice(3, 2), NumberStyles.HexNumber),
									B = byte.Parse(colorStr.AsSpan().Slice(5, 2), NumberStyles.HexNumber),
									A = byte.Parse(colorStr.AsSpan().Slice(7, 2), NumberStyles.HexNumber),
								};
						}
						catch (FormatException e)
						{
							errors.Add(new ParserError(
								9002, ErrorServiety.Warning,
								new string[] { colorStr, e.Message },
								"颜色值格式不正确"));
							charcolor = new Color() { Packed = 0 };
						}
					}
					else
					{
						charcolor = new Color() { Packed = 0 };
					}
					character = new(
						dom.SelectSingleNode("./character/name")?.Value ?? "",
						charcolor);
				}
				else
				{
					character = new(dom.InnerText, new());
				}
				return character;
			}


			internal static Scenario on_scenario(XmlReader r, List<CompilerError> errors)
			{
				List<Page> pages = new(20);
				Scenario ret = new(pages);
				while (r.Read() && r.NodeType != XmlNodeType.EndElement && r.Name != "text")
				{
					CharacterInfo? character = null;

					if (r.NodeType == XmlNodeType.Element)
					{
						switch (r.Name)
						{
							case "pages":
								{
									r.ReadToDescendant("page");
									while (r.Name == "page" && r.Read())
									{
										// body.pages.page
										List<Paragraph> paras = new(8);

										string? label = null;


										while(r.Read() && r.NodeType != XmlNodeType.EndElement && r.Name != "page")
										{
											if(r.NodeType == XmlNodeType.Element)
											{
												switch (r.Name)
												{
													case "character":
														character = on_character(dom, errors);
														break;
													case "para":
														if (!pagesChild.HasChildNodes)
															paras.Add(new(false, pagesChild.InnerText));
														else
															paras.Add(new(true, pagesChild.InnerXml));
														break;
													case "comment":
														ret.Comment = pagesChild.InnerText;
														break;
													default:
														break;
												}
											}
										}

										pages.Add(new(paras, character, label));
									}
								}
								break;
							case "comment":
								ret.Comment = on_comment(r);
								break;
							default:
								break;
						}
					}
				}

				return ret;
			}
		}
	}
}