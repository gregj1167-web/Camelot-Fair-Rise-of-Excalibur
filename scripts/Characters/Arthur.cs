using UnityEngine;

public class Arthur : CharacterBase
{
    protected override void OnInteract()
    {
        Debug.Log("Arthur grants jousting access.");
        GameManager.Instance.AddPoints(20);
    }
}
