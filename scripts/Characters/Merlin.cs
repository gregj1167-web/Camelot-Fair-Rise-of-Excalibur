using UnityEngine;

public class Merlin : CharacterBase
{
    public bool unlocked = false;

    public override void TryInteract()
    {
        if (!unlocked)
        {
            CharacterPopup.Instance.ShowDialogue("Merlin", "The magic is not ready yet...");
            return;
        }

        base.TryInteract();
    }

    protected override void OnInteract()
    {
        Debug.Log("Merlin awakens magic systems.");
        GameManager.Instance.AddPoints(100);

        // Future hook: unlock abilities, dragons, or Excalibur mechanics
    }
}
