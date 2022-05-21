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
        private static GameObject rootObj = null;
        private static Dictionary<string, SE_DS> seDS_Dic = new Dictionary<string, SE_DS>();
        private static float volume = 0.5f;

        private void Awake()
        {
            rootObj = this.gameObject;
        }

        // AudioSource�̏�����Ԃ�ݒ�
        private static void AudioSourceInit(AudioSource source, SE_SObj data)
        {
            source.clip = data.audioClip;
            source.volume = data.clipVolume;
            source.playOnAwake = data.playOnAwake;
            source.loop = data.loop;
        }

        // �f�B�N�V���i����DS��ǉ�
        private static void AddAudioSObj(SE_SObj seData, GameObject soundPlayerObj, Dictionary<string, SE_DS> _seDS_Dic)
        {
            AudioSource newAudioSource = soundPlayerObj.AddComponent<AudioSource>();
            AudioSourceInit(newAudioSource, seData);
            SE_DS ds = new SE_DS(newAudioSource, seData.name, seData.clipVolume, seData.clipPitchRange);
            _seDS_Dic.Add(seData.name, ds);
        }

        // AudioSource�𐶐�
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

        // ���Ƀ����_���ȃs�b�`�������Z���ă����_������ǉ�����B
        private static void PitchRandomize(AudioSource source, float pitchRange)
        {
            source.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
        }

        // �d�����Ȃ��ōĐ�
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

        // �d�����čĐ��\
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

        // �Đ����~������
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

        // ��{�{�����[����ύX
        public static void SetVolume(float _volume)
        {
            float clampVolume = Mathf.Clamp(_volume, 0.0f, 1.0f);
            volume = clampVolume;
        }

        // ��{�{�����[���𑗂�
        public static float GetVolume()
        {
            return volume;
        }
    }
}
