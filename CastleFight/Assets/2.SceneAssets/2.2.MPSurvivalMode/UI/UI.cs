using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using GameUtil;

namespace MPSurvivalMode
{
    public class UI : MonoBehaviour
    {
        public BuyUnitMenu attackForce;
        public BuyUnitMenu defenseForce;

        public UnitInfoPanel waveAnnouncement;
        public Text waveNumber;
        public Text waveCountdown;
        public SurvivalMode game;

        public Button revertButton;
        public Text gold;

        private int currentGold;
        private int remainingGold;

        public int Gold
        {
            get {return currentGold;}
            set {this.currentGold = value;}
        }

        public int RemainingGold
        {
            get { return remainingGold; }
            set { this.remainingGold = value; }
        }

        void Awake()
        {
            revertButton.onClick.AddListener(() => Revert());
        }

        public void ShowWaveAnnouncement(Dictionary<int, int> unitList)
        {
            gold.text = currentGold.ToString();
            remainingGold = currentGold;

            waveAnnouncement.RevertAll();
            foreach (int unit in unitList.Keys)
            {
                waveAnnouncement.UpdateInfo(unit, unitList[unit]);
            }

            attackForce.Revert();
            defenseForce.Revert();

            foreach (RectTransform com in this.gameObject.GetComponentsInChildren<RectTransform>())
            {
                if (com.gameObject.GetComponent<Image>() != null)
                    LeanTween.alpha(com, 1, 0.5f).setFrom(0);
                LeanTween.textAlpha(com, 1, 0.5f).setFrom(0);
            }

            waveNumber.text = game.wave.ToString();

            StartCoroutine(StartTimer());
        }

        IEnumerator StartTimer()
        {
            int remainingTime = game.wavePrepareDuration;

            waveCountdown.text = remainingTime.ToString();

            while (remainingTime >= 0)
            {
                waveCountdown.text = remainingTime.ToString();
                yield return new WaitForSeconds(1);

                remainingTime--;
            }

            StartCoroutine(HideAnnouncementText());
        }

        IEnumerator HideAnnouncementText()
        {
            foreach (RectTransform com in this.gameObject.GetComponentsInChildren<RectTransform>())
            {
                if (com.gameObject.GetComponent<Image>() != null)
                    LeanTween.alpha(com, 0, 0.5f);
                LeanTween.textAlpha(com, 0, 0.5f);
            }

            //attackForce.Revert();
            //defenseForce.Revert();

            yield return new WaitForSeconds(0.7f);

            this.gameObject.SetActive(false);
        }

        public void Revert()
        {
            attackForce.Revert();
            defenseForce.Revert();
            remainingGold = currentGold;
            gold.text = currentGold.ToString();
        }
    }
}