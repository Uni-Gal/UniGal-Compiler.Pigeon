using System;
using System.Collections.Generic;

using UniGal.Compiler.IR.Utilities;

namespace UniGal.Compiler.IR.Script.ScriptBody
{
	public record ActionRecord
	{
		/// <summary>
		/// 不是无参数的Action就自己建一个数组
		/// </summary>
		protected KeyValuePair<string, string>[] ArgsImpl = Array.Empty<KeyValuePair<string, string>>();
		/// <summary>
		/// 附带参数列表
		/// </summary>
		public IReadOnlyList<KeyValuePair<string, string>> Args { get => ArgsImpl; }
		/// <summary>
		/// Action名称
		/// </summary>
		public string Name { get; init; }
		/// <summary>
		/// 包围的文本
		/// </summary>
		public string InnerText { get; init; }
		/// <summary>
		/// 获取参数
		/// </summary>
		/// <param name="argname"></param>
		/// <returns></returns>
		public string FindArg(string argname)
		{
			foreach (var arg in ArgsImpl)
				if (arg.Key == argname)
					return arg.Value;

			throw new KeyNotFoundException($"不存在名为{argname}的参数");
		}

		/// <summary>
		/// 创建Action代码元素
		/// </summary>
		protected ActionRecord(string name, string inner)
		{
			Name = name;
			InnerText = inner;
		}
	}
}
