using TMPro;
using UnityEngine;

public class ModifyTextScript : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private TMP_Text output;

    public void ChangeText()
    {

        string memText = input.text;
        if (memText.Length < 11)
        {
            if (input.text.Length > 0) memText = memText.Insert(0, "(");
            if (input.text.Length > 3) memText = memText.Insert(4, ")");
            if (input.text.Length > 5) memText = memText.Insert(8, "-");
            if (input.text.Length > 7) memText = memText.Insert(11, "-");
            output.text ="+7" + memText;
            if (memText.Length == 11)
            {
                Debug.Log(memText);
                
            }
        }
    }

    public void Seven()
    {
        output.text = "+7";
    }
}
