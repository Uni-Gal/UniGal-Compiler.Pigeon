using System;
using System.Collections.Generic;
using System.Xml;
using UniGal.Compiler.IR;
using ActionRecord = UniGal.Compiler.IR.Script.ScriptBody.ActionRecord;

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
			private class action_handler
			{
				public delegate ActionRecord HandlerFunction(Dictionary<string, string> args);
				public string Name { get; init; }
				public HandlerFunction Handler { get; init; }

				public action_handler(string name, HandlerFunction handler)
				{
					Name = name;
					Handler = handler;
				}
			}
			private readonly static List<action_handler> predefined = new(24);
			// IReadOnlyList<T>
			// private static List<ActionRecord> custom = new(8);
			static action()
			{

			}

			internal static void on_action(XmlReader r, List<CompilerError> errors)
			{

			}
		}
	}
}