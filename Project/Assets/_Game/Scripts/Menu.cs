using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Game
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private GameObject _settingsPanel;

        [SerializeField] private Scrollbar _music;
        [SerializeField] private Scrollbar _sounds;

        public void StartGame()
        {
            SceneManager.LoadScene("Game");
        }

        public void OpenSettings()
        {
            _music.value = SaveData.Instance.data.musicVolume;
            _sounds.value = SaveData.Instance.data.soundsVolume;
            _settingsPanel.SetActive(true);
        }

        public void CloseSettings()
        {
            SaveData.Instance.SaveFileJSON(_music.value, _sounds.value);
            _settingsPanel.SetActive(false);
        }
    }
}