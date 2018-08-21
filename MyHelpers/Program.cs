using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHelpers.FileUtilities.SizeChecks
{
    class Program
    {
	   private const string RootDirectory = "C:\\";

	   static void Main(string[] args)
	   {

		  var dirInfos = new List<MyDirectoryInfo>();
		  GetDirecoryWithSizes(new DirectoryInfo(RootDirectory), dirInfos);
		  PrintLargeDirectories(dirInfos, 900);

		  Console.WriteLine("Press any key to close");
		  Console.ReadKey();
	   }

	   private static void PrintLargeDirectories(IEnumerable<MyDirectoryInfo> dirInfos, int megaBytesLargerThan)
	   {
		  foreach (var dirInfo in dirInfos.Where(x => x.Size > megaBytesLargerThan).OrderByDescending(x => x.Size))
		  {
			 Console.WriteLine($"Full Path: {dirInfo.FullName} - Size: {dirInfo.Size} MB");
		  }
	   }

	   private static void GetDirecoryWithSizes(DirectoryInfo dirInfo, ICollection<MyDirectoryInfo> dirInfos)
	   {
		  try
		  {
			 foreach (var subdirInfo in dirInfo.EnumerateDirectories())
			 {
				GetDirecoryWithSizes(subdirInfo, dirInfos);
			 }

			 var size = GetDirectorySize(dirInfo.FullName);
			 dirInfos.Add(new MyDirectoryInfo
			 {
				FullName = dirInfo.FullName,
				Size = size
			 });
		  }
		  catch (Exception)
		  {
		  }
	   }

	   private static long GetDirectorySize(string directory)
	   {
		  var files = Directory.GetFiles(directory, "*.*");
		  return files.Select(name => new FileInfo(name)).Select(info => info.Length / 1024 / 1024).Sum();
	   }
    }
}
