using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SE
{
    public class AudioSourceDictionary : MonoBehaviour
    {
        [SerializeField] private SEListSObj _listSObj = null;

        private Dictionary<string, SE_DS> _audioSourceDic = null;

        // �f�B�N�V���i�����쐬
        private void MakeDictionary()
        {
            _audioSourceDic = SEManager.AddAudioSource(_listSObj, this.gameObject);
        }

        // �f�B�N�V���i�����擾
        public Dictionary<string, SE_DS> GetAudioSourceDic()
        {
            if(_audioSourceDic == null)
            {
                MakeDictionary();
            }

            return _audioSourceDic;
        }
    }
}

