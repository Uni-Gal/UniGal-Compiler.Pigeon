using System;
using System.Collections.Generic;
using System.IO;
using UniGal.Compiler.IR;
using UniGal.Compiler.LibDriver;
#pragma warning disable CS1591
namespace UniGal.Compiler.Driver
{
	public class EntryPoint
	{
		/// <summary>
		/// Uni-Gal编译器命令行入口
		/// </summary>
		/// <param name="srcs">源文件列表，用空格分隔每个文件</param>
		/// <param name="outDir">输出目录</param>
		/// <param name="backendName">指定的后端名称，可选</param>
		/// <param name="targetLanguage">目标语言，可选</param>
		/// <param name="targetEngine">目标引擎，可选</param>
		public static void Main(IEnumerable<FileInfo> srcs, DirectoryInfo outDir, string? backendName = null, string? targetLanguage = null , string? targetEngine = null)
		{
			CompileOptions options = new()
			{
				BackendName = backendName,
				Sources = srcs,
				OutDir = outDir,
				TargetEngine = targetEngine,
				TargetLanguage = targetLanguage
			};

			CompileDriver driver = new(options);
			driver.OnErrorsAdded += driver_onerrorsadded;

			try
			{
				driver.BeginCompile();
			}
			catch (FileNotFoundException e)
			{
				Console.WriteLine("找不到文件" + e.FileName);
			}
		}

		private static void driver_onerrorsadded(IEnumerable<IR.CompilerError> errs)
		{
			foreach (CompilerError err in errs)
			{
				Console.WriteLine(err.ToString());
				Console.WriteLine();
			}
		}
	}
}
