using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UniGal.Compiler.IR;
using UniGal.Compiler.IR.Script.Resource;

namespace UniGal.Compiler.Frontend
{
	internal enum asset_type
	{
		not_asset = 0,
		audio,
		image
	}

	internal partial class responses
	{
		internal static class res
		{
			// 因为检查不严格，所以unsafe
			private static FileSystemPath make_fspath_unsafe(string pathStr)
			{
				if(pathStr.StartsWith('(') && pathStr.EndsWith(')'))
				{
					// in package
					var spanFull = pathStr.AsSpan();
					var firstMBrace = spanFull.IndexOf('[');
					var firstQuote = pathStr.IndexOf('"');
					var secondQuote = spanFull[firstQuote..].IndexOf('"');
					var secondMBrace = spanFull[secondQuote..].IndexOf(']');
					var firstLBrace = spanFull[secondMBrace..].IndexOf('{');
					var packageQuote = spanFull[firstLBrace..].IndexOf('"');
					var packageQuote2 = spanFull[packageQuote..].IndexOf('"');

					var packageFile = spanFull[packageQuote..packageQuote2].ToString();
					var innerPath = spanFull[firstQuote..secondQuote].ToString();
					return new FileSystemPath(
						packageFile ?? throw new ParseException(
							new (9001, ErrorServiety.CritialError,
							new string[] { pathStr },
							"包内路径格式不对，建议查看详细信息检查")),
						innerPath ?? throw new ParseException(
							new(9001, ErrorServiety.CritialError,
							new string[] { pathStr },
							"包内路径格式不对，建议查看详细信息检查"))
						);
				}
				else
				{
					if (pathStr.StartsWith('"') && pathStr.EndsWith('"'))
						pathStr = pathStr[1..^1];
					return new FileSystemPath(pathStr);
				}
			}
			internal static asset_type check_asset(XmlReader r, List<CompilerError> errors)
			{
				if(r.MoveToAttribute("type"))
				{
					return r.Value switch
					{
						"audio" => asset_type.audio,
						"image" => asset_type.image,
						_ => asset_type.not_asset,
					};
				}
				else
				{
					return asset_type.not_asset;
				}
			}

			internal static Image on_image(XmlReader r)
			{
				XmlDocument dom = new();
				dom.Load(r);
				XmlNode image = dom["image"] ??
					throw new ParseException(
						new(9001, ErrorServiety.CritialError,
						new string[] { dom.InnerText }, "\"type\"属性为\"image\"的resource元素没有image元素"));
				string id = image["img_ID"]?.Value
					?? throw new ParseException(
						new(9001, ErrorServiety.CritialError,
						new string[] { image.InnerText }, "resource.image.img_ID不存在，或img_ID为空字符串"));
				string pathStr = image["file"]?.Value
					?? throw new ParseException(
						new(9001, ErrorServiety.CritialError,
						new string[] { image.InnerText }, "resource.image.file不存在，或file为空字符串"));

				XmlNode size = image["size"]
					?? throw new ParseException(
						new(9001, ErrorServiety.CritialError,
						new string[] { image.InnerText }, "resource.image.size为空"));


				XmlNode xWidth = size["x"];
				XmlNode xHeight = size["y"];

				uint w, h;

				if(xWidth != null && xHeight != null)
				{

				}
				else
				{

				}

				return new Image(id, make_fspath_unsafe(pathStr), w, h);
			}

			internal static Audio on_audio(XmlReader r)
			{
				XmlDocument dom = new();
				dom.Load(r);
				XmlNode snd = dom["sound"] ??
					throw new ParseException(
						new(9001, ErrorServiety.CritialError,
						new string[] { dom.InnerText }, "\"type\"属性为\"sound\"的resource元素没有sound元素"));
				string id = snd["sound_ID"]?.Value
					?? throw new ParseException(
						new(9001, ErrorServiety.CritialError,
						new string[] { snd.InnerText }, "resource.image.sound_ID不存在，或sound_ID为空字符串"));

				string pathStr = snd["file"]?.Value
					?? throw new ParseException(
						new(9001, ErrorServiety.CritialError,
						new string[] { snd.InnerText }, "resource.image.file不存在，或file为空字符串"));

				return new Audio(id, make_fspath_unsafe(pathStr));
			}
		}
	}
}