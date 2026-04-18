using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class SaveAndLoad
{
    public static void Save(int realindex, string NodeID)
    {
        //节点ID由StoryController控制,index
        DataOfSave data = new DataOfSave ( NodeID);
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
#if UNITY_STANDALONE_WIN||UNITY_STANDALONE_LINUX||UNITY_STANDALONE_OSX
        string path = Application.dataPath + "/SaveData/save"+realindex  + ".json";
        Directory.CreateDirectory(Application.dataPath + "/SaveData/");
        
#elif UNITY_ANDROID
        string path=path.Combine(Application.persistentDataPath, fileName + ".json");
        
#endif
        File.WriteAllText(path, json);
        Debug.Log("Save file created at: " + path);
    }
    public static DataOfSave Load(int realindex)
    {
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX
        string path = Application.dataPath + "/SaveData/save" + realindex + ".json";
        
#elif UNITY_ANDROID
        string path=Path.Combine(Application.persistentDataPath, fileName + ".json");
        
#endif
        if (!File.Exists(path))
        {
            Debug.LogError("Save file not found: " + path);
            return null;
        }
        string json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<DataOfSave>(json);//反序列化
    }
    public static void Delete(int realindex)
    {
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX
        string path = Application.dataPath + "/SaveData/save" + realindex + ".json";
#elif UNITY_ANDROID
        string path=Path.Combine(Application.persistentDataPath, fileName + ".json");
#endif
        File.Delete(path);
        
    }
}