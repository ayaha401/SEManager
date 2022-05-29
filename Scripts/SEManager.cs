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
        private static GameObject rootObj = null;
        private static Dictionary<string, SE_DS> seDS_Dic;
        private static float volume = 0.5f;

        private void Awake()
        {
            seDS_Dic = new Dictionary<string, SE_DS>();
            rootObj = this.gameObject;
        }

        // AudioSourceの初期状態を設定
        private static void AudioSourceInit(AudioSource source, SE_SObj data)
        {
            source.clip = data.audioClip;
            source.volume = data.clipVolume;
            source.playOnAwake = data.playOnAwake;
            source.loop = data.loop;
        }

        // ディクショナリにDSを追加
        private static void AddAudioSObj(SE_SObj seData, GameObject soundPlayerObj, Dictionary<string, SE_DS> _seDS_Dic)
        {
            AudioSource newAudioSource = soundPlayerObj.AddComponent<AudioSource>();
            AudioSourceInit(newAudioSource, seData);
            SE_DS ds = new SE_DS(newAudioSource, seData.name, seData.clipVolume, seData.clipPitchRange);
            _seDS_Dic.Add(seData.name, ds);
        }

        // AudioSourceを生成
        public static void GenerateAudioSource(SEListSObj list, Dictionary<string, SE_DS> _seDS_Dic, GameObject soundPlayerObj)
        {
            foreach (SE_SObj data in list.seDatas)
            {
                if (data.CheckUniqueSE())
                {
                    if (_seDS_Dic.ContainsKey(data.name)) continue;

                    AddAudioSObj(data, soundPlayerObj, _seDS_Dic);
                }
                else
                {
                    if (seDS_Dic.ContainsKey(data.name)) continue;

                    AddAudioSObj(data, rootObj, seDS_Dic);
                }
            }
        }

        // 音にランダムなピッチを加減算してランダム性を追加する。
        private static void PitchRandomize(AudioSource source, float pitchRange)
        {
            source.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
        }

        // 重複しないで再生
        public static void AudioPlay(string audioName, Dictionary<string, SE_DS> _seDS_Dic)
        {
            if (_seDS_Dic.ContainsKey(audioName))
            {
                AudioSource uniqueSource = _seDS_Dic[audioName].audioSource;
                uniqueSource.volume = volume * _seDS_Dic[audioName].clipVolume;
                PitchRandomize(uniqueSource, _seDS_Dic[audioName].clipPitchRange);
                uniqueSource.Play();
                return;
            }

            if (!seDS_Dic.ContainsKey(audioName)) { return; }

            AudioSource source = seDS_Dic[audioName].audioSource;
            source.volume = volume * seDS_Dic[audioName].clipVolume;
            source.Play();
        }

        // 重複して再生可能
        public static void AudioPlayOneShot(string audioName, Dictionary<string, SE_DS> _seDS_Dic)
        {
            if (_seDS_Dic.ContainsKey(audioName))
            {
                AudioSource uniqueSource = _seDS_Dic[audioName].audioSource;
                uniqueSource.volume = volume * _seDS_Dic[audioName].clipVolume;
                PitchRandomize(uniqueSource, _seDS_Dic[audioName].clipPitchRange);
                uniqueSource.PlayOneShot(uniqueSource.clip);
                return;
            }

            if (!seDS_Dic.ContainsKey(audioName)) { return; }

            AudioSource source = seDS_Dic[audioName].audioSource;
            source.volume = volume * seDS_Dic[audioName].clipVolume;
            source.PlayOneShot(source.clip);
        }

        // 再生を停止させる
        public static void AudioStop(string audioName, Dictionary<string, SE_DS> _seDS_Dic)
        {
            if (_seDS_Dic.ContainsKey(audioName))
            {
                AudioSource uniqueSource = _seDS_Dic[audioName].audioSource;
                uniqueSource.Stop();
                return;
            }

            if (!seDS_Dic.ContainsKey(audioName)) { return; }
            AudioSource source = seDS_Dic[audioName].audioSource;
            source.Stop();
        }

        // 基本ボリュームを変更
        public static void SetVolume(float _volume)
        {
            float clampVolume = Mathf.Clamp(_volume, 0.0f, 1.0f);
            volume = clampVolume;
        }

        // 基本ボリュームを送る
        public static float GetVolume()
        {
            return volume;
        }
    }
}
