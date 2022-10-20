using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    [SerializeField] private Dropdown resolutionDropdown;  //изменение размера экрана
    private Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();  //очищаем что там было записано - скрываем

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)  //для каждого расширения экрана мы будем добавлять ширину, Х и добавлять высоту
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option); //в options добавляем option


            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();  //обновился список и стало расширение то,которая там у нас стоит  - переводится
                                                 //как Обновленое значение
    }

    public void SetVolume (float volume)  //мы будем посылать в качестве громкости значение громкости volume - SetVolume,
                                          //значение дробных чисел
    {
        SoundEffects.Instance.audioSource.volume = volume;
    }

    public void SetQuality(int qualityIndex)   //это для графики,чтобы менять 
    {
        QualitySettings.SetQualityLevel(qualityIndex);  //мы обращаемся к встреонному классу и передаем туда загрузку индекса 
    }

    public void SetfullScreen(bool isFullScreen)  //это для изменения разрешения Resolution  == добавялем в Toogle
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)  //это для того чтобы заданные нами разрешения были выполнены
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
