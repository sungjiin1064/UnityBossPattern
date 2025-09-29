using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGameUI : MonoBehaviour
{
    private MushRoom mushroom;

    [SerializeField] Image Bosshealthbar;
    [SerializeField] Image HUDhealthbar;
    [SerializeField] TextMeshProUGUI rangedText;

    private void Awake()
    {
        mushroom = GetComponent<MushRoom>();
    }

    private void Start()
    {
        rangedText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        mushroom.OnHealthbarUpdate += OnUpdateHealthBar;
        mushroom.OnPatternStart += OnRangde;
    }
    private void OnDisable()
    {
        mushroom.OnHealthbarUpdate -= OnUpdateHealthBar;
        mushroom.OnPatternStart -= OnRangde;

    }

    private void OnRangde(bool enable)
    {
        if (rangedText.gameObject.activeSelf) return;

        rangedText.gameObject.SetActive(enable);
    }

    private void OnUpdateHealthBar(int current, int max)
    {
        if (Bosshealthbar != null)
        {
            Bosshealthbar.fillAmount = (float)current / max;
        }
        if (HUDhealthbar != null)
        {
            HUDhealthbar.fillAmount = (float)current / max;
        }
        //HUD.fillAmount = (float) current / max;
    }
}
