using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip potionBreakClip;
    public AudioSource potionBreakSource;

	// Use this for initialization
	void Start () {
        EventManager.OnPotionDestroy += PotionDestroy;
	}

    private void PotionDestroy(IPotion potion)
    {
        if (potionBreakClip != null)
        {
            potionBreakSource.clip = potionBreakClip;
            potionBreakSource.Play();
        }
    }
}
