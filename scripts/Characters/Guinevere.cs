using UnityEngine;

public class Guinevere : CharacterBase
{
    protected override void OnInteract()
    {
        Debug.Log("Guinevere provides guidance.");
        GameManager.Instance.AddPoints(10);
    }
}
