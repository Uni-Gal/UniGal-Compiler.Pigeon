using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using UniGal.Compiler.IR;
using UniGal.Compiler.IR.Script.ScriptBody;


namespace UniGal.Compiler.Frontend
{
	internal partial class responses
	{
		// 体量原因，特别放出来
		internal static class body_text
		{
			internal static ScriptText on_text(XmlReader r, List<CompilerError> errors)
			{
				List<Page> pages = new(20);
				ScriptText ret = new(pages);
				while (r.Read() && r.NodeType != XmlNodeType.EndElement && r.Name != "text")
				{
					if (r.NodeType == XmlNodeType.Element)
					{
						switch (r.Value)
						{
							case "character":
								break;
							case "pages":
								{
									r.ReadToDescendant("page");
									while (r.Name == "page" && r.Read())
									{
										XmlDocument dom = new();
										List<Paragraph> paras = new(8);
										CharacterInfo? character = null;
										dom.Load(r);

										string? label = dom.Attributes?.GetNamedItem("label")?.Value;
										foreach (XmlNode pagesChild in dom.ChildNodes)
										{
											if(pagesChild.NodeType == XmlNodeType.Element)
											{
												switch (pagesChild.Name)
												{
													case "character":
														{
															if (pagesChild.ChildNodes.Count != 0)
															{
																XmlNode? colorNode =
																	pagesChild.SelectSingleNode("./character/color") ??
																	pagesChild.SelectSingleNode("./character/colour");
																string colorStr = "#00000000";
																if(colorNode != null)
																{
																	string colorRaw = colorNode.InnerText;

																	if (colorRaw.StartsWith('#'))
																		if (colorRaw.Length == 7)
																			colorStr = colorRaw + "00";
																		else
																			colorStr = colorRaw;	
																}
																try
																{
																	character = new(
																		pagesChild.SelectSingleNode("./character/name")?.Value ?? "",
																		new IR.Utilities.Color()
																		{
																			R = byte.Parse(colorStr.AsSpan().Slice(1, 2), NumberStyles.HexNumber),
																			G = byte.Parse(colorStr.AsSpan().Slice(3, 2), NumberStyles.HexNumber),
																			B = byte.Parse(colorStr.AsSpan().Slice(5, 2), NumberStyles.HexNumber),
																			A = byte.Parse(colorStr.AsSpan().Slice(7, 2), NumberStyles.HexNumber),
																		});
																}
																catch (FormatException e)
																{
																	errors.Add(new ParserError(
																		9002, ErrorServiety.Warning,
																		new string[] { colorStr, e.Message },
																		"颜色值格式不正确"));
																}

															}
															else
															{
																character = new(pagesChild.InnerText, new());
															}
														}
														break;
													case "pages":
														break;
													case "comment":
														ret.Comment = pagesChild.InnerText;
														break;
													default:
														break;
												}
											}
										}

										pages.Add(new(paras));
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