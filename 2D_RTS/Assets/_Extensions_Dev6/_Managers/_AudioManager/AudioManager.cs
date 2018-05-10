using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Audio;
using System.Collections.Generic;

namespace Dev6
{
    public class AudioManager : MonoBehaviour, IamSingleton
    {
        [SerializeField] private AudioMixer mainMixer = null;
        public AudioMixer MainMixer { get { return mainMixer; } }
        [SerializeField][ShowOnly] private string mainMixerAssetName = "MainAudioMixer";
        [SerializeField]private AudioMixerSnapshot actualSnapshot = null;
        public AudioMixerSnapshot ActualSnapshot { get { return actualSnapshot; } }

        //fill on iInitialize
        public List<MixerSetGroup> MixerGroups = new List<MixerSetGroup>();

        public void iInitialize()
        {
            InitMainMixerReference();
            InitDefaultMixerGroups();
            //InitMixerVolumes(); // does not work before start #UnityThings
        }

        void Start()
        {
            InitMixerVolumes(); // seems like unity does not alow the SetFloat function to be used outsite playmode and before Start()
        }

        /// <summary>
        /// DO NOT USE THIS FUNCTION! This is for the Editor button only for testing!
        /// </summary>
        public void TransitionToActualSnapshot()
        {
            actualSnapshot.TransitionTo(1f);
        }

        /// <summary>
        /// Change actual snapshot to another and transition to it in specified time.
        /// </summary>
        /// <param name="_SnapShotName"></param>
        /// <param name="_TransitionDuration"></param>
        public void GoToSnapshot(string _SnapShotName, float _TransitionDuration)
        {
            actualSnapshot = mainMixer.FindSnapshot(_SnapShotName);
            actualSnapshot.TransitionTo(_TransitionDuration);
        }

        /// <summary>
        /// Set the volume of a main group. NOTE: the volume of a main group cant be changed via snapshot!
        /// </summary>
        /// <param name="_GroubName"></param>
        /// <param name="_Value"></param>
        public void SetMixerGroupVolume(string _GroubName, float _Value)
        {
            float tValue = Mathf.Clamp(_Value, -80.00f, 20.00f);
            bool found = mainMixer.SetFloat(_GroubName + "_Vol", tValue);
            if (!found && mainMixer != null)
                Debug.LogError(_GroubName + "_Vol" + " was not found! Could not set Value to:  " + _Value);
        }

        void InitMainMixerReference()
        {
            mainMixer = Resources.Load<AudioMixer>(mainMixerAssetName);
            GoToSnapshot("default", 0f); // this is the default snapshot
        }

        void InitMixerVolumes()
        {
            for (int i = 0; i < MixerGroups.Count; i++)
            {
                SetMixerGroupVolume(MixerGroups[i].Name, MixerGroups[i].Volume_dB);
            }
        }

        void InitDefaultMixerGroups()
        {
            bool containsMaster = false;
            bool containsMusic = false;
            bool containsSfx = false;
            bool containsUI = false;
            bool containsAE = false;
            bool containsEffects = false;
            bool containsLanguage= false;

            for (int i = 0; i < MixerGroups.Count; i++)
            {
                if (MixerGroups[i].Name == "Master")
                    containsMaster = true;
                if (MixerGroups[i].Name == "Music")
                    containsMusic = true;
                if (MixerGroups[i].Name == "Sfx")
                    containsSfx = true;
                if (MixerGroups[i].Name == "UI")
                    containsUI = true;
                if (MixerGroups[i].Name == "ActionEffects")
                    containsAE = true;
                if (MixerGroups[i].Name == "Effects")
                    containsEffects = true;
                if (MixerGroups[i].Name == "Language")
                    containsLanguage = true;
            }

            if (!containsMaster)
                MixerGroups.Add(new MixerSetGroup("Master", 0f));
            if (!containsMusic)
                MixerGroups.Add(new MixerSetGroup("Music", 0f));
            if (!containsSfx)
                MixerGroups.Add(new MixerSetGroup("Sfx", 0f));
            if (!containsUI)
                MixerGroups.Add(new MixerSetGroup("UI", 0f));
            if (!containsAE)
                MixerGroups.Add(new MixerSetGroup("ActionEffects", 0f));
            if (!containsEffects)
                MixerGroups.Add(new MixerSetGroup("Effects", 0f));
            if (!containsLanguage)
                MixerGroups.Add(new MixerSetGroup("Language", 0f));
        }

        [System.Serializable]
        public class MixerSetGroup
        {
            [SerializeField]string name = "Name";
            public string Name
            {
                get
                {
                    return name;
                }
            }
            [Range(-80f, 20f)]
            [SerializeField] float volume_dB = 0.0f; // on change set the value directly in mixer
            public float Volume_dB
            {
                get
                {
                    return volume_dB;
                }
            }
            public MixerSetGroup(string _Name, float _db)
            {
                name = _Name;
                volume_dB = _db;
            }
        }
    }
}
