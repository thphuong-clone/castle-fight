using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using GameUtil;

namespace MPSurvivalMode
{
    public class UI : MonoBehaviour
    {
        public RectTransform waveAnnouncementText;
        public Text wavePreparationCountdown;
        public SurvivalMode game;

        public void ShowWaveAnnouncement(Dictionary<int, int> units)
        {
            if (units.Count > 0)
            {
                waveAnnouncementText.position = new Vector2(Screen.width / 2, Screen.height / 2);

                System.Text.StringBuilder announcementTextBuilder = new System.Text.StringBuilder();

                foreach (KeyValuePair<int, int> unit_number in units)
                {
                    announcementTextBuilder.Append(unit_number.Value + " ");
                    switch (unit_number.Key)
                    {
                        case GameConstant.SWORDMAN:
                            announcementTextBuilder.Append("Swordman");
                            break;
                        case GameConstant.ARCHER:
                            announcementTextBuilder.Append("Archer");
                            break;
                        case GameConstant.KNIGHT:
                            announcementTextBuilder.Append("Knight");
                            break;
                        case GameConstant.HEAVY_INFANTRY:
                            announcementTextBuilder.Append("Heavy Infantry");
                            break;
                        case GameConstant.CANNON:
                            announcementTextBuilder.Append("Cannon");
                            break;
                        default:
                            announcementTextBuilder.Append("Unit(s)");
                            break;
                    }
                    announcementTextBuilder.Append("\n");
                }
                waveAnnouncementText.gameObject.GetComponent<Text>().text = announcementTextBuilder.ToString();

                ShowAnnouncementText();
                StartCoroutine(StartTimer());
            }
        }

        IEnumerator StartTimer()
        {
            int remainingTime = game.wavePrepareDuration;

            wavePreparationCountdown.text = remainingTime.ToString();

            while (remainingTime > 0)
            {
                yield return new WaitForSeconds(1);

                remainingTime--;
                wavePreparationCountdown.text = remainingTime.ToString();
            }

            StartCoroutine(HideAnnouncementText());
        }

        IEnumerator HideAnnouncementText()
        {
            LeanTween.textAlpha(waveAnnouncementText, 0, 1);

            while (LeanTween.isTweening(waveAnnouncementText.gameObject))
                yield return null;

            waveAnnouncementText.gameObject.SetActive(false);
        }

        void ShowAnnouncementText()
        {
            waveAnnouncementText.gameObject.SetActive(true);

            LeanTween.textAlpha(waveAnnouncementText, 1, 1).setFrom(0);
        }
    }
    
}