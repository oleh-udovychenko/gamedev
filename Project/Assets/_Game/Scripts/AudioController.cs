using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private List<AudioSource> _musics;
        [SerializeField] private List<AudioSource> _sounds;

        private void OnEnable()
        {
            foreach (var music in _musics)
                music.volume = SaveData.Instance.data.musicVolume;
            foreach (var sound in _sounds)
                sound.volume = SaveData.Instance.data.soundsVolume;
        }
    }
}