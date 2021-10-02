using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    [SerializeField, Tooltip("Effect")]
    List<AudioClip> SEObjList = new List<AudioClip>();

    private static Dictionary<string, AudioClip> SEObjDic = new Dictionary<string, AudioClip>();
    
    void Awake() 
    {
        foreach (AudioClip item in SEObjList)
        {
            if(SEObjDic.ContainsKey(item.name) == true)
            {
                return;
            }
            SEObjDic.Add(item.name, item);
        }
    } 

    public static AudioClip SendSE(string effectName)
    {
        if(SEObjDic.ContainsKey(effectName) != false)
        {
            return SEObjDic[effectName];
        }
        else
        {
            return null;
        }
    }
}
