/*
 * (Jacob Welch)
 * (Gun)
 * (TemplateMethodPattern)
 * (Description: A base class for all guns that can be fired.)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Gun : MonoBehaviour
{
    #region Fields
    // Hover color effects
    private Color normalColor;
    [SerializeField] private Color hoverColor = Color.black*Color.clear;

    [Tooltip("The amount of rotation to apply for recoil")]
    [SerializeField] protected float recoilAmount = 10.0f;

    /// <summary>
    /// Holds true if the gun is loud and should make noise.
    /// </summary>
    protected bool isLoud = true;

    // Components
    protected SpriteRenderer spriteRenderer;
    protected AudioSource audioSource;
    protected ParticleSystem particleSystem;
    #endregion

    #region Functions
    /// <summary>
    /// Initializes components.
    /// </summary>
    protected virtual void Awake()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        normalColor = spriteRenderer.color;
    }

    /// <summary>
    /// A template method for firing a gun.
    /// </summary>
    protected virtual void Fire()
    {
        ShowParticleEffect();

        StartCoroutine(RecoilRoutine());

        if (isLoud)
        {
            PlaySound();
        }
    }

    /// <summary>
    /// Shows the guns particle effect.
    /// </summary>
    protected abstract void ShowParticleEffect();

    /// <summary>
    /// Plays a sound for the gun.
    /// </summary>
    protected virtual void PlaySound()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }

    /// <summary>
    /// A routine for applying recoil to a gun.
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerator RecoilRoutine();

    /// <summary>
    /// Changes the guns color to a selectd color.
    /// </summary>
    /// <param name="newColor">The new color to set the gun as.</param>
    private void ChangeColor(Color newColor)
    {
        spriteRenderer.color = newColor;
    }

    /// <summary>
    /// When hovering set the guns color to its hover color.
    /// </summary>
    private void OnMouseOver()
    {
        ChangeColor(hoverColor);
    }

    /// <summary>
    /// When the gun is clicked on fire it.
    /// </summary>
    private void OnMouseDown()
    {
        Fire();
    }

    /// <summary>
    /// When no longer hovering resets the guns color.
    /// </summary>
    private void OnMouseExit()
    {
        ChangeColor(normalColor);
    }
    #endregion
}
