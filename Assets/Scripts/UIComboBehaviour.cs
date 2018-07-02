using ODT.Util.Scriptable;
using UnityEngine;
using UnityEngine.UI;

namespace ODT.MaG.UI
{
    public class UIComboBehaviour : MonoBehaviour
    {
        [SerializeField]
        private IntVariable comboValue;
        [SerializeField]
        private GameObject bgImage;
        [SerializeField]
        private GameObject comboLabel;
        [SerializeField]
        private Text comboValueText;

        private void OnEnable()
        {
            OnComboUpdate();
        }

        public void OnComboUpdate()
        {
            if (comboValue.Value > 1)
            {
                comboValueText.text = comboValue.Value.ToString();

                bgImage.SetActive(true);
                comboValueText.gameObject.SetActive(true);
                iTween.PunchScale(comboValueText.gameObject, iTween.Hash(
                    "x", 0.5f,
                    "y", 0.5f
                    ));

                comboLabel.SetActive(true);
            }
            else
            {
                bgImage.SetActive(false);
                comboValueText.gameObject.SetActive(false);
                comboLabel.SetActive(false);
            }
        }
    }
}
