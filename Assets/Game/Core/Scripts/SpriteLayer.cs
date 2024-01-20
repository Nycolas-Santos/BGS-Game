using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLayer : MonoBehaviour
{
    public SpriteData spriteData;

    public SpriteRenderer _spriteRenderer;
    
    private const int SPRITE_ARRAY_THRESHOLD = 0;

    private void Start()
    {
        if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSpriteLayer(SpriteData newSpriteData)
    {
        spriteData = newSpriteData;
        _spriteRenderer.sprite = spriteData.spriteArray[0];
    }

    public void SetSpriteLayerIndex(int index)
    {
        if (spriteData.spriteArray.Length > SPRITE_ARRAY_THRESHOLD)
        {
            _spriteRenderer.sprite = spriteData.spriteArray[index];
        }
    }
}
