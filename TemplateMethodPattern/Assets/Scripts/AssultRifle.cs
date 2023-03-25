/*
 * (Jacob Welch)
 * (AssultRifle)
 * (TemplateMethodPattern)
 * (Description: Handles the functionality for an assult rifle with its unique recoil and particle effect.)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssultRifle : Gun
{
    #region Functions
    /// <summary>
    /// Plays the particle effect of the weapon and sets its color.
    /// </summary>
    protected override void ShowParticleEffect()
    {
        particleSystem.startColor = Color.black;
        particleSystem.Play();
    }

    /// <summary>
    /// Handles recoil of the wepaon.
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator RecoilRoutine()
    {
        var time = 0.04f;

        while (time > 0)
        {
            yield return new WaitForFixedUpdate();

            time -= Time.fixedDeltaTime;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(recoilAmount, 0, time / 0.04f));
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
