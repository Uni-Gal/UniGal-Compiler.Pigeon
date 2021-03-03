using System;
using System.Collections.Generic;
using System.Xml;
using UniGal.Compiler.IR;
using Action = UniGal.Compiler.IR.Script.ScriptBody.Action;

namespace UniGal.Compiler.Frontend
{
	internal partial class responses
	{
		internal static class action
		{
			private class action_handler
			{
				public delegate Action HandlerFunction(Dictionary<string, string> args);
				public string Name { get; init; }
				public HandlerFunction Handler { get; init; }

				public action_handler(string name, HandlerFunction handler)
				{
					Name = name;
					Handler = handler;
				}
			}
			private class action_handler_custom
			{

			}
			private static List<action_handler> predefined = new(24);
			private static List<action_handler_custom> custom = new(8);
			static action()
			{

			}

			internal static void on_action(XmlReader r, List<CompilerError> errors)
			{

			}
		}
	}
}