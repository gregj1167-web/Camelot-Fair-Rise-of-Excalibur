using UnityEngine;
using TMPro;

public class CharacterPopup : MonoBehaviour
{
    public static CharacterPopup Instance;

    public GameObject panel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void ShowDialogue(string charName, string dialogue)
    {
        panel.SetActive(true);
        nameText.text = charName;
        dialogueText.text = dialogue;
    }

    public void Close()
    {
        panel.SetActive(false);
    }
}
