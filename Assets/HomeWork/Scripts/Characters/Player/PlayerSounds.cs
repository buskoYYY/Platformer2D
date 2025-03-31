using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private AudioClip _stepSound;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private AudioClip _attackSound;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioClip _accelerationSound;
    [SerializeField] private float _lowPitch = 0;
    [SerializeField] private float _topPitch = 2;

    private float _nextPlayStep;

    public void PlayStepSound()
    {
        if (CanPlaySound(_stepSound, ref _nextPlayStep))
        {
            _audioManager.PlayRandomPitchSound(_stepSound, _lowPitch, _topPitch);
        }
    }

    public void PlayHitSound() => _audioManager.PlaySound(_hitSound);

    public void PlayAttackSound() => _audioManager.PlaySound(_attackSound);

    public void PlayDeathSound() => _audioManager.PlaySound(_deathSound);

    public void PlayAccelerationSound() => _audioManager.PlaySound(_accelerationSound);

    private bool CanPlaySound(AudioClip sound, ref float nextPlayTime)
    {
        if (nextPlayTime < Time.time)
        {
            nextPlayTime = sound.length + Time.time;
            return true;
        }
        return false;
    }
}
