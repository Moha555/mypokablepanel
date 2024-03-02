using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Achievement
{
    public string Name;
    public string Description;
}

[System.Serializable]
public class Employee
{
    public string Name;
    public string Family;
    public string Age;
}

[System.Serializable]
public class CompanyData
{
    public string CompanyName;
    public string CompanyAddress;
    public string CompanyDescription;
    public List<Achievement> Achievements;
    public List<Employee> Employees;
}

public class PanelTwoManager : MonoBehaviour
{
    //public TextMeshProUGUI panel2Text;
    public TextMeshProUGUI panel2Text;
    public RawImage panel2Logo;
    public Button backToMainMenuBtn;

    // Reference to CanvasGroup for controlling visibility
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        // Get or add CanvasGroup component
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void UpdatePanel(string jsonFilePath, string pngFilePath)
    {
        // Load and display full info from the JSON file
        string jsonData = System.IO.File.ReadAllText(jsonFilePath);
        CompanyData companyData = JsonUtility.FromJson<CompanyData>(jsonData);

        if (panel2Text != null)
        {
            // Update the TextMeshProUGUI in Panel 2
            string achievementsText = "";
            foreach (var achievement in companyData.Achievements)
            {
                achievementsText += $"{achievement.Name} :{achievement.Description}\n";
            }

            string employeesText = "";
            foreach (var employee in companyData.Employees)
            {
                employeesText += $"نام :{employee.Name}  ,نام خانوادگی :{employee.Family}\n";
            }
            panel2Text.text.faConvert();
            panel2Text.text = $"شرکت :{companyData.CompanyName}\nآدرس:{companyData.CompanyAddress}\nتوضیحات:{companyData.CompanyDescription}\n\nدستاوردها\n{achievementsText}\nاعضای شرکت\n{employeesText}".faConvert();
            panel2Text.text.faConvert();
        }

        if (panel2Logo != null)
        {
            // Load and set the texture for the RawImage in Panel 2
            byte[] fileData = System.IO.File.ReadAllBytes(pngFilePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);
            panel2Logo.texture = texture;
        }

        if (backToMainMenuBtn != null)
        {
            // Add an onClick listener directly to the button
            backToMainMenuBtn.onClick.AddListener(GoBack);
        }
    }

    private void GoBack()
    {
        // Show Panel 1
        GameObject panel1 = GameObject.FindGameObjectWithTag("Panel1");
        // Hide Panel 2
        SetPanelVisibility(gameObject, false);
        if (panel1 != null)
        {
            SetPanelVisibility(panel1, true);
        }
        else
        {
            Debug.LogError("Panel 1 not found. Make sure it is tagged correctly.");
        }
    }

    private void SetPanelVisibility(GameObject panel, bool isVisible)
    {
        CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();

        if (canvasGroup != null)
        {
            canvasGroup.alpha = isVisible ? 1 : 0;
            canvasGroup.interactable = isVisible;
            canvasGroup.blocksRaycasts = isVisible;
        }
        else
        {
            Debug.LogError($"CanvasGroup not found on {panel.name} GameObject.");
        }
    }
}
