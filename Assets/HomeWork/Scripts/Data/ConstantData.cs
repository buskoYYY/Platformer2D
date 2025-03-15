using UnityEngine;
public static class ConstantData
{
    public static class AnimatorParametr
    {
        public static readonly int MoveX = Animator.StringToHash(nameof(MoveX));
        public static readonly int MoveY = Animator.StringToHash(nameof(MoveY));
        public static readonly int IsMoving = Animator.StringToHash(nameof(IsMoving));
        public static readonly int IsOpen = Animator.StringToHash(nameof(IsOpen));
        public static readonly int IsClose = Animator.StringToHash(nameof(IsClose));
        public static readonly int Attack = Animator.StringToHash(nameof(Attack));
    }

    public static class SaveData
    {
        public const string MUSIC_KEY = "Music";
        public const string MUSIC_MUTE_KEY = "MusicIsOn";
        public const string SOUND_KEY = "Sound";
        public const string SOUND_MUTE_KEY = "SoundIsOn";
        public const int IS_ON_VALUE = 1;
        public const int IS_OF_VALUE = 0;
        public const float DEFAULT_VOLUME = 1;
    }
}
