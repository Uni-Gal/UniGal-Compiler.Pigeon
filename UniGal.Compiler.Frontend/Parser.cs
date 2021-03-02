using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

using UniGal.Compiler.IR;
using UniGal.Compiler.IR.Script;
using UniGal.Compiler.IR.Utilities;
namespace UniGal.Compiler.Frontend
{
	/// <summary>
	/// 解析器
	/// </summary>
	public class Parser
	{
		private readonly List<IR.CompilerError> errors = new(100);
		private readonly TextReader xml_stream;
		/// <summary>
		/// 获取错误列表
		/// </summary>
		public IEnumerable<IR.CompilerError> Errors => errors;
		/// <summary>
		/// 解析前为null，解析失败值无意义
		/// </summary>
		public ScriptSyntaxTree? AST;
		/// <summary>
		/// 创建解析器
		/// </summary>
		/// <param name="path"></param>
		public Parser(string path)
		{
			xml_stream = File.OpenText(path);
		}

		/// <summary>
		/// 创建解析器
		/// </summary>
		/// <param name="xmlStreamReader"></param>
		public Parser(TextReader xmlStreamReader) : this()
		{
			xml_stream = xmlStreamReader;
		}

		/// <summary>
		/// 同步进行分析
		/// </summary>
		/// <returns>分析结果，成功为true</returns>
		public bool Parse()
		{
			using XmlReader r = XmlReader.Create(xml_stream);
			Metadata? md = null;
			EnvironmentInfo? rtenv = null;
			Body? body = null;
			try
			{
				if (r.ReadToFollowing("unigal"))
				{
					// 有效，开始解析
					while (r.Read())
					{
						switch (r.NodeType)
						{

							case XmlNodeType.Element:
								{
									switch (r.Name)
									{
										case "head":
											md ??= responses.on_metadata(r, errors);
											break;
										case "body":
											body ??= responses.on_scriptbody(r, errors);
											break;
										case "environment":
											rtenv ??= responses.on_rtenv(r, errors);
											break;
									}
								}
								break;
							case XmlNodeType.EndElement:
								goto stop_parse;
							default:
								continue;
						}
					}
				stop_parse:
					AST = new ScriptSyntaxTree(
						md ?? throw new ParseException(),
						body ?? throw new ParseException(),
						rtenv
						);
				}
				else
				{
					// 无效，报错
					errors.Add(new ParserError(3, ErrorServiety.CritialError, Array.Empty<string>(), "无效unigal文件"));
					return false;
				}
			}
			catch (XmlException e)
			{
				var errobj = new ParserError(2, ErrorServiety.CritialError, new string[] {
					"",
					e.Message,
					string.Format("第{0}行, {1}个字符", e.LineNumber.ToString(), e.LinePosition)
				}, "XML文档有问题");

				throw new ParseException(errobj, e);
			}
			catch (ParseException e)
			{
				errors.Add(e.)
				return false;
				throw;
			}
			return true;
		}

		/// <summary>
		/// 异步进行分析
		/// </summary>
		/// <returns>分析结果，成功为true</returns>
		public Task<bool> ParseAsync()
		{
			return Task.Run(Parse);
		}
	}
}
