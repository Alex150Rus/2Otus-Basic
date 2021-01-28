using UnityEngine;

[AddComponentMenu("Game/AddHealth")]
internal sealed class Health : MonoBehaviour
{
    public float current;

    public void ApplyDamage(float damage)
    {
        current -= damage;
        if (current < 0.0f)
            current = 0.0f;
    }
}