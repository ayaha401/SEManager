using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SE
{
    // SEの情報の構造体
    public struct SE_DS
    {
        public AudioSource audioSource;
        public string audioName;
        public float clipVolume;
        public float clipPitchRange;

        public SE_DS(AudioSource source, string name, float volume, float pitch)
        {
            audioSource = source;
            audioName = name;
            clipVolume = volume;
            clipPitchRange = pitch;
        }
    }

    public class SEManager : MonoBehaviour
    {
        private static float _volume = 0.5f;

        private void Start()
        {
            
        }

        // AudioSourceの初期状態を設定
        private static void AudioSourceSetting(AudioSource source, SE_SObj data)
        {
            source.clip = data.audioClip;
            source.playOnAwake = false;
            source.volume = data.clipVolume;
        }

        // AudioSourceをAudioClipの数だけ生成、生成したデータをDictionaryとして格納
        public static Dictionary<string, SE_DS> AddAudioSource(SEListSObj list, GameObject obj)
        {
            Dictionary<string, SE_DS> audioDS_Dic = new Dictionary<string, SE_DS>();
            foreach (SE_SObj data in list.seDatas)
            {
                AudioSource newAudioSource = obj.AddComponent<AudioSource>();
                AudioSourceSetting(newAudioSource, data);

                if (audioDS_Dic.ContainsKey(data.audioClipName) == false)
                {
                    SE_DS ds = new SE_DS(newAudioSource, data.audioClipName, data.clipVolume, data.clipPitchRange);
                    audioDS_Dic.Add(data.audioClipName, ds);
                }
            }
            return audioDS_Dic;
        }

        // 音にランダムなピッチを加算してランダム性を追加する。
        private static void PitchRandomize(AudioSource audioSource, float pitchRange)
        {
            audioSource.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
        }

        // 重複しないで再生
        public static void AudioPlay(AudioSourceDictionary audioSourceDictionary, string audioName)
        {
            Dictionary<string, SE_DS> sourceDic = audioSourceDictionary.GetAudioSourceDic();
            if (sourceDic.ContainsKey(audioName) == false)
            {
                Debug.Log("対応するstring : " + audioName + "がありません");
                return;
            }

            AudioSource source = sourceDic[audioName].audioSource;
            if (source != null)
            {
                source.volume = _volume * sourceDic[audioName].clipVolume;
                PitchRandomize(source, sourceDic[audioName].clipPitchRange);
                source.Play();
            }
        }

        // 重複して再生可能
        public static void AudioPlayOneShot(AudioSourceDictionary audioSourceDictionary, string audioName)
        {
            Dictionary<string, SE_DS> sourceDic = audioSourceDictionary.GetAudioSourceDic();
            if (sourceDic.ContainsKey(audioName) == false)
            {
                Debug.Log("対応するstring : " + audioName + "がありません");
                return;
            }

            AudioSource source = sourceDic[audioName].audioSource;
            if (source != null)
            {
                AudioClip clip = source.clip;
                source.volume = _volume * sourceDic[audioName].clipVolume;
                PitchRandomize(source, sourceDic[audioName].clipPitchRange);
                source.PlayOneShot(clip);
            }
        }

        // 基本ボリュームを変更
        public static void SetVolume(float volume)
        {
            volume = Mathf.Clamp(volume, 0.0f, 1.0f);
            _volume = volume;
        }

        // 基本ボリュームを送る
        public static float GetVolume()
        {
            return _volume;
        }
    }
}
