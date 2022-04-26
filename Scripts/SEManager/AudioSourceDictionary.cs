using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SE
{
    public class AudioSourceDictionary : MonoBehaviour
    {
        [SerializeField] private SEListSObj _listSObj = null;

        private Dictionary<string, SE_DS> _audioSourceDic = null;

        // ディクショナリを作成
        private void MakeDictionary()
        {
            _audioSourceDic = SEManager.AddAudioSource(_listSObj, this.gameObject);
        }

        // ディクショナリを取得
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

