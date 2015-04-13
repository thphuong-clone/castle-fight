using UnityEngine;
using UnityEngine.UI;
using GameUtil;

namespace MPSurvivalMode
{
    public class UnitInfoPanel : MonoBehaviour
    {
        public Text swordmanNumber;
        public Text archerNumber;
        public Text knightNumber;
        public Text gladiatorNumber;
        public Text cannonNumber;

        public void UpdateInfo(int unit, int number)
        {
            switch (unit)
            {
                case GameConstant.SWORDMAN:
                    swordmanNumber.text = number.ToString();
                    break;
                case GameConstant.ARCHER:
                    archerNumber.text = number.ToString();
                    break;
                case GameConstant.KNIGHT:
                    knightNumber.text = number.ToString();
                    break;
                case GameConstant.HEAVY_INFANTRY:
                    gladiatorNumber.text = number.ToString();
                    break;
                case GameConstant.CANNON:
                    cannonNumber.text = number.ToString();
                    break;
            }
        }

        public void RevertAll()
        {
            swordmanNumber.text = "0";
            archerNumber.text = "0";
            knightNumber.text = "0";
            gladiatorNumber.text = "0";
            cannonNumber.text = "0";
        }
    }
}
