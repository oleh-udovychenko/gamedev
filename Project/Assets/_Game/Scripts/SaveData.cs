using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Game
{
    public class SaveData : MonoBehaviour
    {
        public static SaveData Instance { get; private set; }

        public Data data;

        private string _JSONFilePath;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            _JSONFilePath = Path.Combine(Application.persistentDataPath, "Save.json");
            data = LoadJSON<Data>();

            if (data == null)
            {
                data = new Data();
                data.musicVolume = 1f;
                data.soundsVolume = 1f;
            }
        }

        private T LoadJSON<T>()
        {
            if (File.Exists(_JSONFilePath))
                return JsonUtility.FromJson<T>(File.ReadAllText(_JSONFilePath));
            else
                return default(T);
        }

        public void SaveFileJSON(float musicVolume, float soundsVolume)
        {
            data.musicVolume = musicVolume;
            data.soundsVolume = soundsVolume;

            File.WriteAllText(_JSONFilePath, JsonUtility.ToJson(data));
        }

        public class Data
        {
            public float musicVolume;
            public float soundsVolume;
        }
    }
}
