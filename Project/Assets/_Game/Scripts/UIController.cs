using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Game
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Text _killsCounter;

        [SerializeField] private float _timeAnimateKills;

        public void SetKills(int value)
        {
            _killsCounter.rectTransform.DOScale(Vector2.one * 1.2f, _timeAnimateKills / 2f)
                .OnComplete(() =>
                {
                    _killsCounter.text = "Kills: " + value.ToString();
                    _killsCounter.rectTransform.DOScale(Vector2.one, _timeAnimateKills / 2f);
                });
        }
    }
}
