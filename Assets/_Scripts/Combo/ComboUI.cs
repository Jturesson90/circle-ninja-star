using System;
using System.Collections;
using System.Collections.Generic;
using DoozyUI;
using Drolegames.Events;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ComboUI : MonoBehaviour
{
    [SerializeField] private int _visibleComboThreshold = 3;
    [SerializeField] private IntVariable _combo = null;
    private UIElement _comboUIElement;
    private UIButton _comboUIButton;
    private Text _text;
    void Awake()
    {
        _comboUIElement = GetComponentInParent<UIElement>();
        _comboUIButton = GetComponentInParent<UIButton>();
        _text = GetComponent<Text>();
    }

    void OnEnable()
    {
        if (_combo != null)
            SetText(_combo.InitialValue);
        _combo?.SubscribeOnValueChanged(UpdateText);
    }

    void OnDisable()
    {
        _combo.UnsubscribeOnValueChanged(UpdateText);
    }

    public void UpdateText(int combo)
    {
        HandleUIElement(combo);
        if (combo > 0)
        {
            SetText(combo);

            _comboUIButton?.ExecuteClick();
        }
    }
    private void SetText(int combo)
    {
        _text.text = "Combo \n" + combo;
    }
    private void HandleUIElement(int combo)
    {
        var comboVisible = combo >= _visibleComboThreshold;
        if (comboVisible != _comboUIElement?.isVisible)
        {
            if (comboVisible)
            {
                _comboUIElement?.Show(false);
            }
            else
            {
                _comboUIElement?.Hide(false);
            }
        }
    }
}
