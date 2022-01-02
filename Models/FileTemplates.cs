using System.Globalization;

namespace Money_CLI.Models;

public static class FileTemplates
{
    public static string[] FileTemplate(string changeType, string fileName, double total) {
        return new string[] {
            $"# {changeType} for {fileName}",
            string.Empty,
            $"## Total: {total.ToString("C", new CultureInfo("el-GR"))}",
            string.Empty,
            "| Title | Amount | Date | Comment |",
            "| --- | --- | --- | --- |",
        };
    }
}