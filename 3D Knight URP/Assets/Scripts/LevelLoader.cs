using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider slider;


    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronosly(sceneIndex));
    } 

    private IEnumerator LoadAsynchronosly(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);  //��������� ��������� �������.LoadSceneAsync -
                                                                             //��������� ��������.��� ��� ��� ����� ������� �� �������
                                                                             //�� ��������� AsyncOperation ���������� operation
        loadingScreen.SetActive(true);
        while (operation.isDone == true)        //����� ���� �� ������ ����������,������� ��������
        {
            float progress = operation.progress;
            slider.value = progress;
            yield return null;
        }
    }
}
