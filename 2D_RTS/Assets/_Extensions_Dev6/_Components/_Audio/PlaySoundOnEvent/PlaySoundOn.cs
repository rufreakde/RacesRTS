/*********************
*	Rudolf Chrispens
***********************/

#region USE
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Audio;
#endregion

namespace Dev6
{
    //[SelectionBase]
    [AddComponentMenu("Dev6/AUDIO/Play Sound On Event")]
    public class PlaySoundOn : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IcanGetTriggered
    {
        [Split("PlaySoundOnMouse")]
        public bool DIRECT_SOUND = false;

        //[Header("#State:")]
        public PlaySoundSettings _Awake = new PlaySoundSettings();
        public PlaySoundSettings _Start = new PlaySoundSettings();
        public PlaySoundSettings _OnEnable = new PlaySoundSettings();
        public PlaySoundSettings _OnDisable = new PlaySoundSettings();

        //[Header("#Collision:")]
        public PlaySoundSettings _TriggerEnter = new PlaySoundSettings();
        public PlaySoundSettings _TriggerExit = new PlaySoundSettings();

        //[Header("#Mouse/Touch:")]
        public PlaySoundSettings _OnClick = new PlaySoundSettings();
        public PlaySoundSettings _OnEnter = new PlaySoundSettings();
        public PlaySoundSettings _OnExit = new PlaySoundSettings();

        public PlaySoundSettings _ScriptTrigger = new PlaySoundSettings();

        private AudioSource MainSource = null;

        #region State play
        void Awake()
        {
            //always adds a source to play a sound
            MainSource = this.gameObject.AddComponent<AudioSource>();

            if (_Awake.Play)
                PlaySound(_Awake, _Awake.Source);
        }

        void Start()
        {
            if (_Start.Play)
                PlaySound(_Start, _Start.Source);
        }

        void OnEnable()
        {
            if (_OnEnable.Play)
                PlaySound(_OnEnable, _OnEnable.Source);
        }

        void OnDisable()
        {
            if (_OnDisable.Play)
                PlaySound(_OnDisable, _OnDisable.Source);
        }
        #endregion

        #region State play
        void OnTriggerEnter()
        {
            if (_TriggerEnter.Play)
                PlaySound(_TriggerEnter, _TriggerEnter.Source);
        }

        void OnTriggerExit()
        {
            if (_TriggerExit.Play)
                PlaySound(_TriggerExit, _TriggerExit.Source);
        }
        #endregion

        #region Mouse/Touch/Eventsystem play

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_OnClick.Play)
                PlaySound(_OnClick, _OnClick.Source);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_OnEnter.Play)
                PlaySound(_OnEnter, _OnEnter.Source);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_OnExit.Play)
                PlaySound(_OnExit, _OnExit.Source);
        }
        #endregion

        void PlaySound(PlaySoundSettings _Set, AudioSource _CustomSource)
        {
            if (_Set.Clip)
            {
                if (DIRECT_SOUND)
                {
                    if (_CustomSource != null)
                    {
                        _Set.Source.volume = _Set.Loudness.DbToVolume();
                        _Set.Source.PlayOneShot(_Set.Clip);
                    }
                    else
                    {
                        MainSource.volume = _Set.Loudness.DbToVolume();
                        MainSource.PlayOneShot(_Set.Clip);
                    }
                }
                else if( _Set.Mixer != null)
                {
                    if (_CustomSource != null)
                    {
                        _Set.Source.volume = _Set.Loudness.DbToVolume();
                        _Set.Source.outputAudioMixerGroup = _Set.Mixer;
                        _Set.Source.PlayOneShot(_Set.Clip);
                    }
                    else
                    {
                        MainSource.volume = _Set.Loudness.DbToVolume();
                        MainSource.outputAudioMixerGroup = _Set.Mixer;
                        MainSource.PlayOneShot(_Set.Clip);
                    }
                }
                else
                {
                    Debug.LogError("Error: (" + this.ToString() + ")  Clip: "+ _Set.Clip.name +" is set on Directsound = (" + DIRECT_SOUND + ") but there is no mixer chosen!" );
                }

                if (_Set.OnlyOnce)//if the sound should only be played once disable the sound
                {
                    _Set.Play = false;
                }
            }
        }

        public void iTrigger(string _Trigger)
        {
            if (_ScriptTrigger.Play)
                PlaySound(_ScriptTrigger, _ScriptTrigger.Source);
        }

        [System.Serializable]
        public class PlaySoundSettings
        {
            public bool Play = false;
            public bool OnlyOnce = false;
            public AudioClip Clip = null;
            public AudioSource Source = null;
            public AudioMixerGroup Mixer = null;
            [Range(-60f, 0f)]
            public float Loudness = 0f;

            public PlaySoundSettings()
            {
                Play = false;
                OnlyOnce = false;
                Clip = null;
                Source = null;
                Mixer = null;
                Loudness = 0f;
            }
        }
    }

}