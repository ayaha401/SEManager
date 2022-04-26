using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SE
{
    [CreateAssetMenu(menuName = "SEManager/SEData")]
    public class SE_SObj : ScriptableObject
    {
        [Header("����")]
        public AudioClip audioClip;

        [Header("����")]
        [Range(0.0f,1.0f)] public float clipVolume = 1.0f;

        [Header("�s�b�`�����_�}�C�Y")]
        [Range(0.0f, 1.0f)] public float clipPitchRange = 0.0f;

        [Header("������")]
        public string audioClipName;
    }
}

