using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SE
{
    [CreateAssetMenu(menuName = "SEManager/SEListData")]
    public class SEListSObj : ScriptableObject
    {
        [Header("SE�f�[�^")]
        public List<SE_SObj> seDatas = new List<SE_SObj>();
    }
}

