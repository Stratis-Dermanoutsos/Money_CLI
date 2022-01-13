namespace Money_CLI;

#pragma warning disable CS8602

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Money_CLI.Controllers;

public static class SystemVariables
{
    /// <summary>
    /// The path to the executable's directory.
    /// </summary>
    private static string _AppDomain { get => AppDomain.CurrentDomain.BaseDirectory; }

    /// <summary>
    /// The full path to the System Variables file.
    /// </summary>
    private static string SystemVariablesFileName { get => @$"{_AppDomain}SystemVariables.json"; }

    /// <summary>
    /// The string containing System Variables in JSON format.
    /// </summary>
    private static string SystemVariablesString { get => File.ReadAllText(SystemVariablesFileName); }

    /// <summary>
    /// The System Variables as Dictionary object.
    /// </summary>
    private static Dictionary<string, string>? SystemVariablesJSON { get => JsonSerializer.Deserialize<Dictionary<string, string>>(SystemVariablesString); }

    /// <summary>
    /// Handles the path for the folder used to export data.
    /// </summary>
    public static string ExportFolder {
        get {
            try {
                return SystemVariablesJSON["EXPORT_FOLDER"];
            } catch (Exception) {
                return string.Empty;
            }
        }

        set {
            try {
                Dictionary<string, string>? _data = SystemVariablesJSON;

                _data["EXPORT_FOLDER"] = GenericController.EnsureDirectory(value);

                string json = JsonSerializer.Serialize(_data);
                File.WriteAllText(SystemVariablesFileName, json);

                GenericController.PrintSuccess($"The export folder has successfully been set.");
            } catch (Exception) {
                GenericController.PrintError("Could not set the export folder.");
            }
        }
    }

    /// <summary>
    /// Handles the path for the folder used to host our database.
    /// </summary>
    public static string DatabaseFolder {
        get {
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        set {
            
        }
    }

    /// <summary>
    /// Handles the currency that is returned on export.
    /// </summary>
    public static string Currency {
        get {
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        set {
            
        }
    }

    /// <summary>
    /// Creates the system variables file.
    /// </summary>
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