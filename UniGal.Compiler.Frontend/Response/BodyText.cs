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
			internal static CharacterInfo on_complexchar(XmlReader r, List<CompilerError> errors)
			{
				do
				{
					switch (r.Name)
					{

					}
				}
				while (r.NodeType != XmlNodeType.EndElement && r.Name != "character" && r.Read());
			}

			internal static CharacterInfo on_character(XmlReader r, List<CompilerError> errors)
			{
				CharacterInfo? character = null;

				r.Read();
				switch (r.NodeType)
				{
					case XmlNodeType.Element:
						// complex
						character = on_complexchar(r, errors);
						break;
					case XmlNodeType.Text:
						character = new(r.ReadContentAsString());
						while (r.Read() && r.NodeType != XmlNodeType.EndElement && r.Name != "character") ;
						break;
					default:
						character = new("无名氏");
						break;
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
														character = on_character(r, errors);
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