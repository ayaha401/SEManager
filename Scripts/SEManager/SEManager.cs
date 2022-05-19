using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SE
{
    // SE�̏��̍\����
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

        // AudioSource�̏�����Ԃ�ݒ�
        private static void AudioSourceSetting(AudioSource source, SE_SObj data)
        {
            source.clip = data.audioClip;
            source.playOnAwake = false;
            source.volume = data.clipVolume;
            source.playOnAwake = data.playOnAwake;
            source.loop = data.loop;
        }

        // AudioSource��AudioClip�̐����������A���������f�[�^��Dictionary�Ƃ��Ċi�[
        public static Dictionary<string, SE_DS> AddAudioSource(SEListSObj list, GameObject obj)
        {
            Dictionary<string, SE_DS> audioDS_Dic = new Dictionary<string, SE_DS>();
            foreach (SE_SObj data in list.seDatas)
            {
                AudioSource newAudioSource = obj.AddComponent<AudioSource>();
                AudioSourceSetting(newAudioSource, data);

                if (audioDS_Dic.ContainsKey(data.name) == false)
                {
                    SE_DS ds = new SE_DS(newAudioSource, data.name, data.clipVolume, data.clipPitchRange);
                    audioDS_Dic.Add(ds.audioName, ds);
                }
            }
            return audioDS_Dic;
        }

        // ���Ƀ����_���ȃs�b�`�����Z���ă����_������ǉ�����B
        private static void PitchRandomize(AudioSource audioSource, float pitchRange)
        {
            audioSource.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
        }

        // �d�����Ȃ��ōĐ�
        public static void AudioPlay(AudioSourceDictionary audioSourceDictionary, string audioName)
        {
            Dictionary<string, SE_DS> sourceDic = audioSourceDictionary.GetAudioSourceDic();
            if (sourceDic.ContainsKey(audioName) == false)
            {
                Debug.Log("�Ή�����string : " + audioName + "������܂���");
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

        // �d�����čĐ��\
        public static void AudioPlayOneShot(AudioSourceDictionary audioSourceDictionary, string audioName)
        {
            Dictionary<string, SE_DS> sourceDic = audioSourceDictionary.GetAudioSourceDic();
            if (sourceDic.ContainsKey(audioName) == false)
            {
                Debug.Log("�Ή�����string : " + audioName + "������܂���");
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

        // �����~
        public static void AudioStop(AudioSourceDictionary audioSourceDictionary, string audioName)
        {
            Dictionary<string, SE_DS> sourceDic = audioSourceDictionary.GetAudioSourceDic();
            if (sourceDic.ContainsKey(audioName) == false)
            {
                Debug.Log("�Ή�����string : " + audioName + "������܂���");
                return;
            }

            AudioSource source = sourceDic[audioName].audioSource;
            if(source != null)
            {
                source.Stop();
            }
        }

        // ��{�{�����[����ύX
        public static void SetVolume(float volume)
        {
            volume = Mathf.Clamp(volume, 0.0f, 1.0f);
            _volume = volume;
        }

        // ��{�{�����[���𑗂�
        public static float GetVolume()
        {
            return _volume;
        }
    }
}
