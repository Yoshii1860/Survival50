using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDamage : MonoBehaviour
{
    [SerializeField] Canvas impactCanvas;
    [SerializeField] float impactTime = 2f;
    [SerializeField] Image img;
    [SerializeField] AudioSource damageTaken;
    [SerializeField] AudioClip damageTakenClip;

    void Start()
    {
        impactCanvas.enabled = false;
    }

    public void ShowDamageImpact()
    {
        StartCoroutine(ShowSplatter());
    }

    IEnumerator ShowSplatter()
    {
        damageTaken.PlayOneShot(damageTakenClip);
        if (impactCanvas.enabled)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
            img.color = new Color (1, 1, 1, i);
            yield return null;
            }
        yield return new WaitForSeconds(impactTime);
        }
        else if (!impactCanvas.enabled)
        {
            impactCanvas.enabled = true;
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                img.color = new Color (1, 1, 1, i);
                yield return null;
            }
            yield return new WaitForSeconds(impactTime);
        }
        impactCanvas.enabled = false;
        img.color = new Color (1, 1, 1, 1);
    }
}
