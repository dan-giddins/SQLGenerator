using System;
using System.IO;

namespace SQLGenerator
{
	class Program
	{
		private const string Dir = @"C:\Users\uizdg\The University of Nottingham\Official RIS Team - Documents\Dan Giddins RIS Work\";
		private static readonly string InputFile = $@"{Dir}level_7_spines.csv";
		private static readonly string OutputFile = $@"{Dir}SQL\{Command}_level_7_spines.sql";
		private const string Command = "INSERT";

		static void Main()
			=> Spines();

		private static void Spines()
		{
			// sql string
			var sql = "USE [RIS_Integration]\n" +
				"GO\n";
			// load csv file
			using (var sr = new StreamReader(InputFile))
			{
				// discard header
				sr.ReadLine();
				var line = sr.ReadLine();
				while (line != null)
				{
					var lineArray = line.Split(',');
					switch (Command)
					{
						case "INSERT":
							sql += $"INSERT INTO [dbo].[Spine]\n" +
								$"VALUES ({int.Parse(lineArray[0])}, {(int) decimal.Parse(lineArray[1])}, 1)\n";
							break;
						case "UPDATE":
							sql += $"UPDATE [dbo].[Spine]\n" +
								$"SET [Salary] = {(int) decimal.Parse(lineArray[1])}\n" +
								$"WHERE [SpinePoint] = {int.Parse(lineArray[0])}\n" +
								$"AND [Level7] = 1\n";
							break;
					}
					line = sr.ReadLine();
				}
			}
			sql += $"GO\n";
			Console.WriteLine(sql);
			File.WriteAllText(OutputFile, sql);
		}
	}
}
