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
	public class Parser : IDisposable
	{
		private bool is_disposed;
		private bool cached_error;
		private readonly List<CompilerError> problems = new(100);
		private readonly TextReader xml_stream;
		private XmlReaderSettings reader_settings;
		/// <summary>
		/// 获取问题列表
		/// </summary>
		public IEnumerable<CompilerError> Problems => problems;
		/// <summary>
		/// 是否存在关键性错误，即解析失败
		/// </summary>
		public bool HasCriticialError
		{
			get
			{
				if (cached_error == true)
					return true;

				foreach (CompilerError e in problems)
					if (e.Code.Serviety == ErrorServiety.CritialError)
						return cached_error = true;

				return false;
			}
		}
		/// <summary>
		/// 解析前为null，解析失败值无意义
		/// </summary>
		public ScriptDom? AST;

		/// <summary>
		/// 获取当前行号
		/// </summary>
		public int LineNumber { get => reader_settings.LineNumberOffset; }

		/// <summary>
		/// 创建解析器
		/// </summary>
		/// <param name="path">文件路径</param>
		public Parser(string path):this(File.OpenText(path))
		{
		}

		/// <summary>
		/// 创建解析器
		/// </summary>
		/// <param name="xmlStreamReader">XML流</param>
		public Parser(TextReader xmlStreamReader)
		{
			xml_stream = xmlStreamReader;
			reader_settings = new();
		}

		/// <summary>
		/// 同步进行分析
		/// </summary>
		/// <returns>分析结果，成功为true</returns>
		public bool Parse()
		{
			using XmlReader r = XmlReader.Create(xml_stream);
			XmlReaderSettings? rsettings = r.Settings;
			if (rsettings != null)
			{
				rsettings.Async = false;
				rsettings.CloseInput = false;

				rsettings.IgnoreComments = true;
				rsettings.IgnoreWhitespace = true;
			}
			reader_settings = r.Settings ?? new();

			Metadata? md = null;
			EnvironmentInfo? rtenv = null;
			Body? body = null;
			try
			{
				if (r.ReadToFollowing("unigal"))
				{
					// 有效，开始解析
					while (r.Read() &&
						r.NodeType != XmlNodeType.EndElement &&
						r.Name != "unigal") 
					{
						switch (r.NodeType)
						{

							case XmlNodeType.Element:
								{
									switch (r.Name)
									{
										case "head":
											md ??= Responses.on_metadata(r, problems);
											break;
										case "body":
											body ??= Responses.on_scriptbody(r, problems);
											break;
										case "environment":
											rtenv ??= Responses.on_rtenv(r, problems);
											break;
									}
								}
								break;
							default:
								continue;
						}
					}
					AST = new ScriptDom(
						md ?? throw new ParseException(new(3, ErrorServiety.CritialError, new string[] { "缺少元数据<head>" }, "文件不正确")),
						body ?? throw new ParseException(new(3, ErrorServiety.CritialError, new string[] { "缺少主体数据<body>" }, "文件不正确")),
						rtenv
						);
				}
				else
				{
					// 无效，报错
					problems.Add(new ParserError(3, ErrorServiety.CritialError, Array.Empty<string>(), "无效unigal文件"));
					return false;
				}
			}
			catch (XmlException e)
			{
				var errobj = new ParserError(2, ErrorServiety.CritialError, new string[] {
					"",
					e.Message,
					string.Format("第{0}行, {1}个字符", e.LineNumber.ToString(), e.LinePosition.ToString())
				}, "XML文档有问题");

				throw new ParseException(errobj, e);
			}
			catch (ParseException e)
			{
				problems.Add(e.ParserError);
				return false;
				throw;
			}
			return true;
		}

		/// <summary>
		/// 异步进行分析
		/// </summary>
		/// <returns>分析结果，成功为true</returns>
		public async Task<bool> ParseAsync()
		{
			return await Task.Run(Parse);
		}

#pragma warning disable CS1591 // Dispose不用写了，懂得都懂
		protected virtual void Dispose(bool disposing)
		{
			if (!is_disposed)
			{
				if (disposing)
				{
					xml_stream.Dispose();
				}

				is_disposed = true;
			}
		}

		public void Dispose()
		{
			// 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
#pragma warning restore
	}
}
