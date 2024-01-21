using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSheetController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public SpriteData spriteData;
    
    private SpriteLayer[] _spriteLayers;
    
    private const string NO_SPRITE_RENDERERER_ERROR = "SpriteRenderer not found on the GameObject or provided in the script.";
    private const string NO_SPRITE_ARRAY = "The provided sprite array is empty.";
    private const int SPRITE_ARRAY_THRESHOLD = 0;

    void Start()
    {
        // Check if a SpriteRenderer is assigned
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogError(NO_SPRITE_RENDERERER_ERROR);
            }
        }

        _spriteLayers = GetComponentsInChildren<SpriteLayer>();
        ChangeSpriteIndex(0);
    }

    // Function to change the sprite array
    public void ChangeSpriteData(SpriteData newSpriteData, int layer)
    {
        if (layer == 0) // CHANGE THE DEFAULT SPRITE DATA
        {
            if (spriteRenderer != null)
            {
                if (newSpriteData == null)
                {
                    spriteData = null;
                    spriteRenderer.sprite = null;
                    return;
                }
                spriteData = newSpriteData;
                // Set the initial sprite from the new array (you can modify this as needed)
                if (spriteData.spriteArray.Length > SPRITE_ARRAY_THRESHOLD)
                {
                    spriteRenderer.sprite = spriteData.spriteArray[0];
                }
                else
                {
                    Debug.LogWarning(NO_SPRITE_ARRAY);
                }
            }
            else
            {
                Debug.LogError(NO_SPRITE_RENDERERER_ERROR);
            }
        }
        else // CHANGE SOME LAYER SPRITE DATA AS HEAD OR CLOTHES
        {
            int correctedLayer = layer - 1; // THIS SUBTRACTION HAS TO BE DONE BECAUSE ARRAYS STARTS AT 0
            _spriteLayers[correctedLayer].SetSpriteLayer(newSpriteData);
        }
    }
    
    // Function to change the sprite array
    public void ChangeSpriteIndex(int index)
    {
        if (spriteRenderer != null)
        {
            if (spriteData.spriteArray.Length > SPRITE_ARRAY_THRESHOLD)
            {
                spriteRenderer.sprite = spriteData.spriteArray[index];
                foreach (var layer in _spriteLayers)
                {
                    layer.SetSpriteLayerIndex(index);
                }
            }
            else
            {
                Debug.LogWarning(NO_SPRITE_ARRAY);
            }
        }
        else
        {
            Debug.LogError(NO_SPRITE_RENDERERER_ERROR);
        }
    }
}
