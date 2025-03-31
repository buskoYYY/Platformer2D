using UnityEngine;

public class EnemySound : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private AudioClip _stepSound;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private AudioClip _attackSound;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private float _lowPitch = 0;
    [SerializeField] private float _topPitch = 2;

    private float _nextPlayStep;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public void PlayStepSound()
    {
       if (_audioManager.CanBeHeard(_transform.position))
       {
            if (_nextPlayStep < Time.time)
            {
                _nextPlayStep = _stepSound.length + Time.time;
                _audioManager.PlayRandomPitchSound(_stepSound, _lowPitch, _topPitch);
            }
       }
    }
    public void PlayHitSound() => _audioManager.PlaySound(_hitSound);

    public void PlayAttackSound() => _audioManager.PlaySound(_attackSound);

    public void PlayDeathSound() => _audioManager.PlaySound(_deathSound);
}
