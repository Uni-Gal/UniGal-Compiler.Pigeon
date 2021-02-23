using System;
using System.Collections.Generic;

using UniGal.Compiler.IR.Utilities;

namespace UniGal.Compiler.IR.Script.ScriptBody
{
	public class Action
	{
		/// <summary>
		/// 不是无参数的Action就自己建一个数组
		/// </summary>
		protected KeyValuePair<string, StringView>[] ArgsImpl = Array.Empty<KeyValuePair<string, StringView>>();
		public IReadOnlyList<KeyValuePair<string, StringView>> Args { get => ArgsImpl; }
		/// <summary>
		/// Action名称
		/// </summary>
		public string Name { get; init; }
		/// <summary>
		/// 附加的参数
		/// </summary>
		public StringView AdditionalArg { get; init; }
		/// <summary>
		/// 获取参数
		/// </summary>
		/// <param name="argname"></param>
		/// <returns></returns>
		public StringView FindArg(string argname)
		{
			foreach (var arg in ArgsImpl)
				if (arg.Key == argname)
					return arg.Value;

			throw new KeyNotFoundException($"不存在名为{argname}的参数");
		}

		/// <summary>
		/// 创建Action代码元素
		/// </summary>
		/// <param name="name"></param>
		public Action(string name)
		{
			Name = name;
		}
	}
}
