using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI
{
    public class Toggle : MonoBehaviour
    {
        public Image      CheckMark;
        public AudioMixer Mixer;

        private Color _cachedColor;

        private bool IsSoundEnabled()
        {
            if (Mixer.GetFloat("Volume", out float value))
            {
                if (value == -80f)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        private void OnEnable()
        {
            _cachedColor = CheckMark.color;

            if (IsSoundEnabled())
            {
                CheckMark.color = _cachedColor;
            }
            else
            {
                CheckMark.color = new Vector4(0f, 0f, 0f, 0f);
            }
        }

        public void ToggleMethod()
        {
            if (IsSoundEnabled())
            {
                Mixer.SetFloat("Volume", -80f);
            }
            else
            {
                Mixer.SetFloat("Volume", 0f);
            }

            if (IsSoundEnabled())
            {
                CheckMark.color = _cachedColor;
            }
            else
            {
                CheckMark.color = new Vector4(0f, 0f, 0f, 0f);
            }
        }
    }
}