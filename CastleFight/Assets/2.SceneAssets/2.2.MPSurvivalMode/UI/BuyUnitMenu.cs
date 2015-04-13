using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameUtil;

namespace MPSurvivalMode
{
    public class BuyUnitMenu : MonoBehaviour
    {
        public Button buySwordmanButton;
        public Button buyArcherButton;
        public Button buyKnightButton;
        public Button buyGladiatorButton;
        public Button buyCannonButton;

        public MPSurvivalMode.UI containerUI;

        public UnitInfoPanel displayUnit;

        private Dictionary<int, int> buyUnitIndex;
        public List<int> buyUnitList;

        void Awake()
        {
            buyUnitIndex = new Dictionary<int, int>();
            buyUnitList = new List<int>();

            buySwordmanButton.onClick.AddListener(() => AddUnit(GameConstant.SWORDMAN));
            buyArcherButton.onClick.AddListener(() => AddUnit(GameConstant.ARCHER));
            buyKnightButton.onClick.AddListener(() => AddUnit(GameConstant.KNIGHT));
            buyGladiatorButton.onClick.AddListener(() => AddUnit(GameConstant.HEAVY_INFANTRY));
            buyCannonButton.onClick.AddListener(() => AddUnit(GameConstant.CANNON));
        }

        private void AddUnit(int unit)
        {
            int price;
            GameUtil.GameConstant.UNIT_PRICE.TryGetValue(unit, out price);
            if (price > 0)
            {
                if (containerUI.RemainingGold < price)
                    return;
                else
                {
                    containerUI.RemainingGold -= price;
                    containerUI.gold.text = containerUI.RemainingGold.ToString();
                }
            }
            else
                return;

            if (buyUnitIndex.ContainsKey(unit))
            {
                buyUnitIndex[unit] += 1;
            }
            else
            {
                buyUnitIndex.Add(unit, 1);
            }

            buyUnitList.Add(unit);

            displayUnit.UpdateInfo(unit, buyUnitIndex[unit]);
        }

        public void Revert()
        {
            buyUnitIndex.Clear();
            buyUnitList.Clear();
            displayUnit.RevertAll();
        }
    }
}
