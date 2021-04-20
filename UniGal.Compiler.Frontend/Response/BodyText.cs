using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
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
			internal static TextStyle on_textstyle(XmlReader r, List<CompilerError> errors)
			{
				TextStyle ret = TextStyle.None;

				while (r.Read() && r.NodeType != XmlNodeType.EndElement && r.Name != "style")
				{
					switch (r.Name)
					{
						case "size":
							if(r.ReadContentAsBoolean())
								ret |= TextStyle.Large;
							break;
						case "bold":
							if (r.ReadContentAsBoolean())
								ret |= TextStyle.Bold;
							break;
						case "italic":
							if (r.ReadContentAsBoolean())
								ret |= TextStyle.Italic;
							break;
						case "deleted":
							if (r.ReadContentAsBoolean())
								ret |= TextStyle.Stroke;
							break;
						case "underlined":
							if (r.ReadContentAsBoolean())
								ret |= TextStyle.Undelined;
							break;
						case "shadow":
							// TODO
							break;
						case "glow":
							// TODO
							break;
						default:
							break;
					}
				}

				return ret;
			}

			internal static CharacterInfo on_complexchar(XmlReader r, List<CompilerError> errors)
			{
				string? name = null;
				string? ruby = null;
				TextStyle style = TextStyle.None;
				Color color = new();

				XmlTextReader r2 = new(r.ReadOuterXml(), XmlNodeType.Element, null);
				while (r2.Read() && r2.NodeType != XmlNodeType.EndElement && r2.Name != "character")
				{
					switch (r2.Name)
					{
						case "name":
							name = r2.Value;
							break;
						case "ruby":
							ruby = r2.Value;
							break;
						case "style":
							style = on_textstyle(r2, errors);
							break;
						case "color":
						// 有意为之
						case "colour":
							{
								string colorRaw = r2.ReadInnerXml();
								string colorStr = "#00000000";
								if (colorRaw.StartsWith('#'))
								{
									if (colorRaw.Length == 7)
										colorStr = colorRaw + "00";
									else
										colorStr = colorRaw;
								}
								else
								{
									// TODO：先套个口袋罪
									errors.Add(new ParserError(0102, ErrorServiety.Warning, new string[] { colorRaw }, "颜色值格式不正确"));
								}
								try
								{
									color = new Color(
										byte.Parse(colorStr.AsSpan().Slice(1, 2), NumberStyles.HexNumber),
										byte.Parse(colorStr.AsSpan().Slice(3, 4), NumberStyles.HexNumber),
										byte.Parse(colorStr.AsSpan().Slice(5, 6), NumberStyles.HexNumber),
										byte.Parse(colorStr.AsSpan().Slice(7, 8), NumberStyles.HexNumber));
								}
								catch (FormatException e)
								{
									// TODO：先套个口袋罪
									errors.Add(new ParserError(
										0102, ErrorServiety.Warning,
										new string[] { colorStr, e.Message },
										"颜色值格式不正确"));
									color = new Color();
								}
							}
							break;
						default:
							break;
					}
				}

				return new CharacterInfo(name ?? "无名氏", ruby, color, style);
			}

			internal static CharacterInfo on_character(XmlReader r, List<CompilerError> errors)
			{
				string inner = r.ReadOuterXml();
				XmlTextReader r2 = new(inner, XmlNodeType.Element, null);
				
				if(!r2.Read())
					throw new ParseException(new ParserError(1, ErrorServiety.CritialError,
						new string[] {"行号：" + r2.LineNumber + "，序号：" + r2.LinePosition },
						"XML解析发生错误"));

				CharacterInfo character = r.NodeType switch
				{
					XmlNodeType.Element => on_complexchar(r, errors),// complex
					XmlNodeType.Text => new(r2.Value),
					_ => new("无名氏"),
				};
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
									if (r.ReadToDescendant("page"))
									{
										while (r.Read() && r.NodeType == XmlNodeType.EndElement && r.Name != "page")
										{
											// body.pages.page
											List<Paragraph> paras = new(8);

											string? label = null;


											while (r.Read() && r.NodeType != XmlNodeType.EndElement && r.Name != "page")
											{
												if (r.NodeType == XmlNodeType.Element)
												{
													switch (r.Name)
													{
														case "character":
															character = on_character(r, errors);
															break;
														case "para":
															XmlTextReader r2 = new(r.ReadOuterXml(), XmlNodeType.Element, null);
															r2.Read();
															if (r2.NodeType == XmlNodeType.Text)
																paras.Add(new(false, r2.Value));
															else if (r2.NodeType == XmlNodeType.Element)
																paras.Add(new(true, r2.ReadInnerXml()));
															break;
														case "comment":
															ret.Comment = r.Value;
															break;
														default:
															break;
													}
												}
											}

											pages.Add(new(paras, character, label));
										}
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