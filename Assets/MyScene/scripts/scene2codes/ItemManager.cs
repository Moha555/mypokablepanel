using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public GameObject itemPrefab; // Reference to your item prefab
    public Transform parentTransform; // Parent transform to instantiate items under


    void Start()
    {
        LoadItemsFromFolder("D:/unityProjects/project1/companies");
        // Find the Panel 2 GameObject
        GameObject panel2 = GameObject.FindGameObjectWithTag("Panel2");
        GameObject panel1 = GameObject.FindGameObjectWithTag("Panel1");
        SetPanelVisibility(panel1, true);
        SetPanelVisibility(panel2, false);
    }

    void LoadItemsFromFolder(string mainFolderPath)
    {
        string[] subfoldersArray = System.IO.Directory.GetDirectories(mainFolderPath);

        foreach (string subfolder in subfoldersArray)
        {
            string jsonFilePath = System.IO.Path.Combine(subfolder, "test.json");
            string pngFilePath = System.IO.Path.Combine(subfolder, "logo.jpg");

            if (System.IO.File.Exists(jsonFilePath) && System.IO.File.Exists(pngFilePath))
            {
                string jsonData = System.IO.File.ReadAllText(jsonFilePath);
                JsonClass companyData = JsonUtility.FromJson<JsonClass>(jsonData);

                // Instantiate the item prefab
                GameObject newItem = Instantiate(itemPrefab, parentTransform);
                GameObject cloneParent = GameObject.FindWithTag("ContentsPanel");

                if (cloneParent != null)
                {
                    // Set the Canvas as the parent
                    newItem.transform.SetParent(cloneParent.transform);
                }
                else
                {
                    Debug.LogError("Main Canvas not found. Make sure it is tagged correctly.");
                }

                // Get references to UI elements in the instantiated item
                //TextMeshProUGUI descriptionText = newItem.GetComponentInChildren<TextMeshProUGUI>();
                TextMeshProUGUI descriptionText = newItem.GetComponentInChildren<TextMeshProUGUI>();
                RawImage logoImage = newItem.GetComponentInChildren<RawImage>();
                Button moreInfoBtn = newItem.GetComponentInChildren<Button>();

                if (descriptionText != null)
                { // Update UI with information from JSON and PNG files
                    descriptionText.text = $"شرکت:\n{companyData.CompanyName}\n\nتوضیحات:\n{companyData.CompanyDescription}";
                }

                // Load and set the texture for the RawImage
                byte[] fileData = System.IO.File.ReadAllBytes(pngFilePath);
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(fileData);
                logoImage.texture = texture;

                // Do sth with the button
                // Get the UnityEngine.UI.Button component
                if (moreInfoBtn != null)
                {
                    // Add an onClick listener directly to the button
                    moreInfoBtn.onClick.AddListener(() => OnButtonClick(jsonFilePath, pngFilePath));
                }
            }
        }
    }

    private void OnButtonClick(string jsonFilePath, string pngFilePath)
    {
        // Find the Panel 2 GameObject
        GameObject panel2 = GameObject.FindGameObjectWithTag("Panel2");
        GameObject panel1 = GameObject.FindGameObjectWithTag("Panel1");
        SetPanelVisibility(panel1, false);
        if (panel2 != null)
        {
            // Get the Panel2Script component
            PanelTwoManager panel2Script = panel2.GetComponent<PanelTwoManager>();

            if (panel2Script != null)
            {
                // Set visibility for Panel 1 and Panel 2
                SetPanelVisibility(panel2, true);

                // Call the UpdatePanel method on Panel2Script
                panel2Script.UpdatePanel(jsonFilePath, pngFilePath);
            }
            else
            {
                Debug.LogError("Panel2Script not found on Panel 2 GameObject.");
            }
        }
        else
        {
            Debug.LogError("Panel 2 not found. Make sure it is tagged correctly.");
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

[System.Serializable]
public class JsonClass
{
    public string CompanyName;
    public string CompanyDescription;
}
