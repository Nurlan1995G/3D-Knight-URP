using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    [SerializeField] private Dropdown resolutionDropdown;  //��������� ������� ������
    private Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();  //������� ��� ��� ���� �������� - ��������

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)  //��� ������� ���������� ������ �� ����� ��������� ������, � � ��������� ������
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option); //� options ��������� option


            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();  //��������� ������ � ����� ���������� ��,������� ��� � ��� �����  - �����������
                                                 //��� ���������� ��������
    }

    public void SetVolume (float volume)  //�� ����� �������� � �������� ��������� �������� ��������� volume - SetVolume,
                                          //�������� ������� �����
    {
        SoundEffects.Instance.audioSource.volume = volume;
    }

    public void SetQuality(int qualityIndex)   //��� ��� �������,����� ������ 
    {
        QualitySettings.SetQualityLevel(qualityIndex);  //�� ���������� � ����������� ������ � �������� ���� �������� ������� 
    }

    public void SetfullScreen(bool isFullScreen)  //��� ��� ��������� ���������� Resolution  == ��������� � Toogle
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)  //��� ��� ���� ����� �������� ���� ���������� ���� ���������
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
