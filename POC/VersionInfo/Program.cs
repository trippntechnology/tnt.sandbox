using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using TNT.Data.Tools;

namespace VersionInfo
{
	class Program
	{
		static void Main(string[] args)
		{
			Parameters parms = new Parameters();

			if (!parms.ParseArgs(args))
			{
				return;
			}

			foreach (FileQuery fq in parms.Environment.FileQueries)
			{
				List<AssemblyInformation> assInfoList = new List<AssemblyInformation>();

				foreach (string path in fq.FilePaths)
				{
					Console.WriteLine($"Processing {path}");
					assInfoList.Add(new AssemblyInformation(path));
				}

				using (TextWriter tw = new StringWriter())
				{
					CsvWriter writer = new CsvWriter(tw);
					writer.WriteRecords(assInfoList);

					Console.WriteLine($"Saving assembly information to {fq.OutputFileName}.");
					File.WriteAllText(fq.OutputFileName, tw.ToString());
				}
			}

			foreach (DBQuery dbq in parms.Environment.DBQueries)
			{
				using (QueryHelper qh = new QueryHelper("System.Data.SqlClient", dbq.ConnectionString))
				{
					qh.ExecuteQuery(dbq.SQL, System.Data.CommandType.Text, null, dr =>
					{
						TextWriter tw = new StringWriter();
						CsvWriter writer = null;

						while (dr.Read())
						{
							if (writer == null)
							{
								writer = new CsvWriter(tw);

								for (int ordinal = 0; ordinal < dr.FieldCount; ordinal++)
								{
									writer.WriteField(dr.GetName(ordinal));
								}

								writer.NextRecord();
							}

							for (int ordinal = 0; ordinal < dr.FieldCount; ordinal++)
							{
								writer.WriteField(dr.GetValue(ordinal));
							}

							writer.NextRecord();
						}

						Console.WriteLine($"Saving query results to {dbq.OutputFileName}.");
						File.WriteAllText(dbq.OutputFileName, tw.ToString());
					});
				}
			}
		}
	}
}