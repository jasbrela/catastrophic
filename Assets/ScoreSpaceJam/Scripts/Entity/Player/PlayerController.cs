using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float invulnerabilityTime;

    [Header("Dependencies")]
    [SerializeField] private Collider2D col;

    private Coroutine invulnerabilityCoroutine = null;

    public void OnHit()
    {
        if (invulnerabilityCoroutine == null)
            invulnerabilityCoroutine = StartCoroutine(InvulnerabilityCoroutine());
    }

    public void OnDeath()
    {
        // TODO: Gameover logic
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        col.enabled = false;
        // TODO: Show to player that he is invulnerable (flashing sprite, etc)
        yield return new WaitForSeconds(invulnerabilityTime);
        col.enabled = true;

        invulnerabilityCoroutine = null;
    }
}
