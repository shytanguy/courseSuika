using UnityEngine;
using UnityEngine.UI;

namespace YG.Example
{
    public class SaverTest : MonoBehaviour
    {
        [SerializeField] InputField integerText;
        [SerializeField] InputField stringifyText;
        [SerializeField] Text systemSavesText;
        [SerializeField] Toggle[] booleanArrayToggle;

        private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
        private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

        private void Awake()
        {
            if (YandexGame.SDKEnabled)
                GetLoad();
        }

        public void Save()
        {
            YandexGame.savesData.score = int.Parse(integerText.text);
            YandexGame.savesData.newPlayerName = stringifyText.text.ToString();


            YandexGame.SaveProgress();
        }

        public void Load() => YandexGame.LoadProgress();

        public void GetLoad()
        {
            integerText.text = string.Empty;
            stringifyText.text = string.Empty;

            integerText.placeholder.GetComponent<Text>().text = YandexGame.savesData.score.ToString();
            stringifyText.placeholder.GetComponent<Text>().text = YandexGame.savesData.newPlayerName;

         

            systemSavesText.text = $"Language - {YandexGame.savesData.language}\n" +
            $"First Session - {YandexGame.savesData.isFirstSession}\n" +
            $"Prompt Done - {YandexGame.savesData.promptDone}\n";
        }
    }
}