using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] TMP_Dropdown graphicsDropdown;

    Resolution[] resolutionsArray;
    List<string> resolutions = new List<string>();
    
    int currentResolutionIndex = 0;

    void Start()
    {
        resolutionsArray = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        for (int i = 0; i < resolutionsArray.Length; i++)
        {
            string resolutionString = resolutionsArray[i].width + " x " + resolutionsArray[i].height;
            resolutions.Add(resolutionString);

            if (resolutionsArray[i].width == Screen.currentResolution.width &&
                resolutionsArray[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(resolutions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        graphicsDropdown.value = 5;
    }

    public void SetResolution(int resolutionIndex)
    {
        Debug.Log("Current Resolution : " + Screen.currentResolution);
        Resolution currentResolution = resolutionsArray[resolutionIndex];
        Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetGraphicsQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
