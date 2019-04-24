using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Effect : MonoBehaviour
{
    private Sprite[] AllSprite;
    [SerializeField]
    private float times = 0.1f;
    private Image effect;
    [SerializeField]
    private string EffectName;
    [SerializeField]
    private bool IsLoop;
    private void OnEnable()
    {
        effect = GetComponent<Image>();
        AllSprite = ConfigManager.GetConfigManager.EffectBody.GetValueGroup(EffectName);
        if (!this.gameObject.activeInHierarchy || !ConfigManager.GetConfigManager.IsLoaded)
            return;
        Play();
    }

    private bool IsPaly;
    private Coroutine OpenEffect;
    public void Play()
    {
        if (IsPaly)
            return;
        OpenEffect = StartCoroutine("PlayAnimator");
    }

    public void Stop()
    {
        if (OpenEffect != null)
            StopCoroutine(OpenEffect);
        IsPaly = true;
    }


    private IEnumerator PlayAnimator()
    {
        int i = 0;
        while (i < AllSprite.Length)
        {
            if (AllSprite[i] == null)
                continue;
            effect.sprite = AllSprite[i];
            yield return new WaitForSeconds(times);
            i++;
            if (i == AllSprite.Length - 1 && IsLoop)
                i = 0;
        }
        IsPaly = false;
    }
}
