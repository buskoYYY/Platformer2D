using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteVariation : MonoBehaviour
{
    [SerializeField] private Sprite[] _directionSprites;
    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void ChangeSprites(Vector2 moveDirection)
    {
        if (moveDirection.y > 0)

        {
            _spriteRenderer.sprite = _directionSprites[0];
        }

        else if (moveDirection.y < 0)

        {
            _spriteRenderer.sprite = _directionSprites[1];
        }

        else if (moveDirection.x < 0)

        {
            _spriteRenderer.sprite = _directionSprites[2];
        }

        else if (moveDirection.x > 0)

        {
            _spriteRenderer.sprite = _directionSprites[3];
        }
    }
}
