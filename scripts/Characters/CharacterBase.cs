using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public string characterName;
    [TextArea]
    public string dialogue;

    public int requiredPoints = 0;

    private void OnMouseDown()
    {
        TryInteract();
    }

    public virtual void TryInteract()
    {
        if (GameManager.Instance.eventPoints >= requiredPoints)
        {
            CharacterPopup.Instance.ShowDialogue(characterName, dialogue);
            OnInteract();
        }
        else
        {
            CharacterPopup.Instance.ShowDialogue(characterName, "You are not ready yet...");
        }
    }

    protected virtual void OnInteract() { }
}
