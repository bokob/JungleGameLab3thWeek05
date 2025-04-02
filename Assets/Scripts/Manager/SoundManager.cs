using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    AudioSource _bgm;
    AudioSource _effect;
    AudioClip _bgmClip;
    AudioClip[] _effectClip;

    Dictionary<string, AudioClip> _bgmSounds = new Dictionary<string, AudioClip>();
    Dictionary<string, AudioClip> _effectSounds = new Dictionary<string, AudioClip>();

    public void Init()
    {
        // AudioSource 찾기
        AudioSource[] audioSource = Manager.Instance.GetComponentsInChildren<AudioSource>();
        _bgm = audioSource[0];
        _effect = audioSource[1];

        /* AudioClip 찾기 */

    }

    public void PlayBgm()
    {

    }

    public void PlayEffect()
    {

    }
}