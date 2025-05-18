using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    [SerializeField] private GameObject isOnImage;
    [SerializeField] private GameObject isOffImage;
    
    private bool _isSoundMute = false;
    private Button _soundsToggleButton;

    private void Awake()
    {
        _soundsToggleButton = GetComponent<Button>();
        _soundsToggleButton.onClick.AddListener(HandleSound);
    }

    private void HandleSound()
    {
        _isSoundMute = !_isSoundMute;

        if (!_isSoundMute)
        {
            isOnImage.SetActive(true);
            isOffImage.SetActive(false);
        }
        else
        {
            isOnImage.SetActive(false);
            isOffImage.SetActive(true);
        }
        GameManager.Instance.GetAudioManager().Mute(_isSoundMute);
    }

    private void OnDestroy()
    {
        _soundsToggleButton.onClick.RemoveListener(HandleSound);
    }
}
