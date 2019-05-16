using System.Collections;
using System.Collections.Generic;
using Drolegames.Events;
using UnityEngine;

public class RecoveringHealth : MonoBehaviour, IInitializeable
{
    [SerializeField] private GameEvent _onNoHealthLeft = null;
    [SerializeField] private GameEvent _onDamaged = null;
    [SerializeField] private IntVariable _healthPercentInteger = null;
    [SerializeField] private BoolVariable _isAlive = null;
    [SerializeField] private int _damagePerHit = 33;
    [SerializeField] private int _recoveryInterval = 1;
    [SerializeField] private int _healthPercentPerInterval = 5;

    public int HealthPercent
    {
        get { return _healthPercentInteger.Value; }
        set
        {
            value = Mathf.Min(100, value);
            if (!value.Equals(_healthPercentInteger.Value))
            {
                _healthPercentInteger.Value = value;
            }
            _isAlive.Value = value > 0;
        }
    }

    public bool IsAlive => _isAlive.Value;
    public bool NeedRecovery => HealthPercent < 100;
    public bool IsRecovering { get; private set; }

    private WaitForSeconds _waitForOneSecond;


    private void Awake()
    {
        _waitForOneSecond = new WaitForSeconds(_recoveryInterval);
    }

    public void Initialize()
    {
        HealthPercent = _healthPercentInteger.InitialValue;
    }

    public void Damage()
    {
        _onDamaged?.Invoke();
        if (!IsAlive)
        {
            _onNoHealthLeft?.Invoke();
        }
        if (!IsRecovering)
        {
            StartCoroutine(Recover());
        }
        HealthPercent -= _damagePerHit;
    }

    private IEnumerator Recover()
    {
        IsRecovering = true;
        while (IsAlive && NeedRecovery)
        {
            yield return _waitForOneSecond;
            AddHealth(_healthPercentPerInterval);
        }
        IsRecovering = false;
    }

    public void Heal(int percent)
    {
        if (IsAlive)
        {
            AddHealth(percent);
        }
    }

    private void AddHealth(int percent) => HealthPercent += percent;

}
