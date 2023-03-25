/*
 * (Jacob Welch)
 * (Pistol)
 * (TemplateMethodPattern)
 * (Description: Handles funtionality for a pistol that can be fired and silenced.)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    #region Fields
    /// <summary>
    /// The starting sprite for the pistol's spriterenderer.
    /// </summary>
    private Sprite startingSprite;

    [Tooltip("The sprite to use when the pistol is silenced")]
    [SerializeField] private Sprite silencedSprite;
    #endregion

    #region Functions
    /// <summary>
    /// Initializes components.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        startingSprite = spriteRenderer.sprite;
    }

    /// <summary>
    /// Gets user input to change the silenced state of the weapon.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeSilenced();
        }
    }

    /// <summary>
    /// Changes between silenced and unsilenced.
    /// </summary>
    public void ChangeSilenced()
    {
        isLoud = !isLoud;

        var newSprite = isLoud ? startingSprite : silencedSprite;
        spriteRenderer.sprite = newSprite;

        // Resets the guns collider
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }

    /// <summary>
    /// Plays the particle effect of the weapon.
    /// </summary>
    protected override void ShowParticleEffect()
    {
        particleSystem.Play();
    }

    /// <summary>
    /// Handles recoil of the wepaon.
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator RecoilRoutine()
    {
        var time = 0.1f;

        while (time > 0)
        {
            yield return new WaitForFixedUpdate();

            time -= Time.fixedDeltaTime;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(recoilAmount, 0, time / 0.1f));
        }

        time = 0.1f;
        while (time > 0)
        {
            yield return new WaitForFixedUpdate();

            time -= Time.fixedDeltaTime;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(0, recoilAmount, time / 0.1f));
        }
    }
    #endregion
}
