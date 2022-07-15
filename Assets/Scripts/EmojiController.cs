using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiController : MonoBehaviour
{
    [SerializeField] GameObject emojiSpooked, emojiCornered;
    WallsDetector wallsDetector;
    Sheep sheep;
    private void Awake()
    {
        sheep = GetComponent<Sheep>();
        wallsDetector = GetComponent<WallsDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        if(wallsDetector.isCornered && sheep.isSpooked)
        {
            emojiSpooked.SetActive(false);
            emojiCornered.SetActive(true);
        }
        else if(!wallsDetector.isCornered && sheep.isSpooked)
        {
            emojiSpooked.SetActive(true);
            emojiCornered.SetActive(false);
        }
        else
        {
            emojiSpooked.SetActive(false);
            emojiCornered.SetActive(false);
        }
    }
}
