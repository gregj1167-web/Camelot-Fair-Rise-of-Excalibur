using UnityEngine;

public class HitFeedback : MonoBehaviour
{
    public static void Play(Vector3 position)
    {
        Debug.Log("Hit FX at " + position);

        // Placeholder:
        // later we add particles, sound, screen shake
    }
}
