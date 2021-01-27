﻿using System.IO;
using System.Xml;

namespace UniGal.Compiler.IR
{
	/// <summary>
	/// 语法树
	/// </summary>
	public class ScriptSyntaxTree
	{
		/// <summary>
		/// 元数据
		/// </summary>
		public Metadata? Metadata { get; private set; }

		public ScriptSyntaxTree(Metadata file)
		{
			
		}
	}
}