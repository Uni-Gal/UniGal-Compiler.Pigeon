using System;
using System.Collections.Generic;
using System.Xml;
using UniGal.Compiler.IR;

namespace UniGal.Compiler.Frontend
{
	/// <summary>
	/// 拓展
	/// </summary>
	public record ExtensionRecord
	{
		/// <summary>
		/// 拓展的名称
		/// </summary>
		public string Name;
		/// <summary>
		/// 包围的XML
		/// </summary>
		public string InnerText;
		/// <summary>
		/// XML属性形式参数的列表
		/// </summary>
		public IReadOnlyDictionary<string, string> Args;

		/// <summary>
		/// 
		/// </summary>
		public ExtensionRecord(string name, string inner, IReadOnlyDictionary<string, string> args)
		{
			Name = name;
			InnerText = inner;
			Args = args;
		}
	}

	internal partial class responses
	{
		internal static class action
		{
			internal static void on_action(XmlReader r, List<CompilerError> errors)
			{
				try
				{
					string name = r.Name;
					Dictionary<string, string> args = new();
					XmlDocument dom = new();
					dom.Load(r);
					XmlAttributeCollection? attrs = dom.Attributes;

					if (attrs != null)
						foreach (XmlAttribute attr in attrs)
							args.Add(attr.Name, attr.Value);

					string inner = dom.InnerText;

					Actions.PredefinedActions.Get()[name](name, args, inner);
				}
				catch (KeyNotFoundException e)
				{
					errors.Add(new ParserError(9002, ErrorServiety.Warning, new string[] { e.Message }, "不是预定义的action"));
				}

			}

			internal static void on_extension(XmlReader r, List<CompilerError> errors)
			{

			}
		}
	}
}