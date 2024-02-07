namespace Ilumisoft.BubblePop
{
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(AudioSource))]
    public class SFXPlayer : MonoBehaviour
    {
        [SerializeField]
        AudioClip selectSFX = null;

        [SerializeField]
        AudioClip popSFX = null;

        [SerializeField] AudioSource audioSource;
        [SerializeField] AudioSource musicSource;
        public Image soundsImage;
        public Image musicImage;

        [SerializeField] Sprite soundsOn;
        [SerializeField] Sprite soundsOff;
        [SerializeField] Sprite musicOn;
        [SerializeField] Sprite musicOff;

        [SerializeField] Toggle soundsToggle;
        [SerializeField] Toggle musicToggle;
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
        private void Start()
        {
            CheckVolume();
        }
        private void CheckVolume()
        {
            musicSource.volume = PlayerPrefs.GetFloat("MusicVolume");
            audioSource.volume = PlayerPrefs.GetFloat("SoundsVolume");
            if (PlayerPrefs.GetFloat("SoundsVolume") == 0)
            {
                soundsImage.sprite = soundsOff;
                soundsToggle.isOn = true;
            } else
            {
                soundsImage.sprite = soundsOn;
                soundsToggle.isOn = false;
            }
            if (PlayerPrefs.GetFloat("MusicVolume") == 0)
            {
                musicImage.sprite = musicOff;
                musicToggle.isOn = true;
            }
            else
            {
                musicImage.sprite = musicOn;
                musicToggle.isOn = false;   
            }
        }
        public void ChangePitch(int i)
        {
            audioSource.pitch = 1 + (i * 0.05f);
        }
        public void MuteSounds(bool b)
        {
            if (b)
            {
                audioSource.volume = 0;
                soundsImage.sprite = soundsOff;
                PlayerPrefs.SetFloat("SoundsVolume", 0);
            } else
            {
                audioSource.volume = 0.8f;
                soundsImage.sprite = soundsOn;
                PlayerPrefs.SetFloat("SoundsVolume", 0.8f);
            }
            
        }
        public void MuteMusic(bool b)
        {
            if (b)
            {
                musicSource.volume = 0;
                musicImage.sprite = musicOff;
                PlayerPrefs.SetFloat("MusicVolume", 0);
            }
            else
            {
                musicSource.volume = 0.35f;
                musicImage.sprite = musicOn;
                PlayerPrefs.SetFloat("MusicVolume", 0.35f);
            }
        }
        public void PlayPopSFX()
        {
            PlayOneShot(popSFX);
        }

        public void PlaySelectSFX()
        {
            PlayOneShot(selectSFX);
        }

        void PlayOneShot(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}