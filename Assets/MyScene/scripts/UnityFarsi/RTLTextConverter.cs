using UnityEngine;
using UnityEngine.UI;

public class RTLTextConverter : MonoBehaviour
{
    private Text textComponent;

    private void Start()
    {
        // Get the Text component attached to the GameObject
        textComponent = GetComponent<Text>();

        if (textComponent != null)
        {
            // Convert the text to RTL
            string convertedText = ConvertToRTL(textComponent.text);

            // Update the Text component with the converted text
            textComponent.text = convertedText;
        }
        else
        {
            Debug.LogError("RTLTextConverter: No Text component found on the GameObject.");
        }
    }

    private string ConvertToRTL(string input)
    {
        // Reverse the string to simulate RTL
        char[] charArray = input.ToCharArray();
        System.Array.Reverse(charArray);
        return new string(charArray);
    }
}
