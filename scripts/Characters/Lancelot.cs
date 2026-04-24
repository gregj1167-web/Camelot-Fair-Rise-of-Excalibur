using UnityEngine;

public class Lancelot : CharacterBase
{
    protected override void OnInteract()
    {
        Debug.Log("Lancelot challenges your skill.");
        GameManager.Instance.AddPoints(30);
    }
}
