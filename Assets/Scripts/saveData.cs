
using System.IO;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public static class saveSystem 
{
    public static readonly string SAVE_Folder = Application.persistentDataPath + "/saves/";
    public static readonly string FILE_EXT = ".json";

    public static void Save(string filename, string dataToSave) { 
        if (!Directory.Exists(SAVE_Folder)) {  Directory.CreateDirectory(SAVE_Folder); }

        File.WriteAllText(SAVE_Folder + filename + FILE_EXT, dataToSave);
    }
    public static string Load(string fileName) {
        string fileLoc = SAVE_Folder + fileName + FILE_EXT;
        if (File.Exists(SAVE_Folder + fileName + FILE_EXT)) {
            string loadedData = File.ReadAllText(fileLoc);
            return loadedData;
        }
        else { return null; }
    }
}
