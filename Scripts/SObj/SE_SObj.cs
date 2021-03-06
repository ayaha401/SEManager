using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SE
{
    [CreateAssetMenu(menuName = "SEManager/SEData")]
    public class SE_SObj : ScriptableObject
    {
        [Header("音源")]
        public AudioClip audioClip;

        [Header("音量")]
        [Range(0.0f,1.0f)] public float clipVolume = 1.0f;

        [Header("ピッチランダマイズ")]
        [Range(0.0f, 1.0f)] public float clipPitchRange = 0.0f;

        [Header("音源設定")]
        public bool playOnAwake = false;
        public bool loop = false;

        public bool CheckUniqueSE()
        {
            bool enablePitch = clipPitchRange > 0.0f;
            if (enablePitch) return true;
            if (playOnAwake) return true;
            if (loop) return true;
            return false;
        }

    }
}

