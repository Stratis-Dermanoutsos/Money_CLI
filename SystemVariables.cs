namespace Money_CLI;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public static class SystemVariables
{
    private static string _AppDomain { get => AppDomain.CurrentDomain.BaseDirectory; }

    private static string SystemVariablesFileName { get => @$"{_AppDomain}SystemVariables.json"; }

    public static string ExportFolder {
        get {
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        set {
            
        }
    }

    public static string DatabaseFolder {
        get {
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        set {
            
        }
    }

    public static string Currency {
        get {
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        set {
            
        }
    }

    public static void Create() {
        if (File.Exists(SystemVariablesFileName))
            return;

        File.Create(SystemVariablesFileName).Close();

        Dictionary<string, string> _data = new Dictionary<string, string>();
        _data.Add("EXPORT_FOLDER", _AppDomain);
        _data.Add("DATABASE_FOLDER", _AppDomain);
        _data.Add("CURRENCY", "USD");

        string json = JsonSerializer.Serialize(_data);
        File.WriteAllText(SystemVariablesFileName, json);
    }
}