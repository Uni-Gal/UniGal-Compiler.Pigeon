using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

using UniGal.Compiler.IR;

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
			
			if(r.ReadToFollowing("unigal"))
			{
				// 有效，开始解析

			}
			else
			{
				// 无效，报错
				errors.Add(new parser_error(3, ErrorServiety.CritialError, Array.Empty<string>(), "无效unigal文件"));
				return false;
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
