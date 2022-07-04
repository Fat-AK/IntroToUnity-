using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Monster : MonoBehaviour
{
    //set Sprite in order to change character when it dies
    [SerializeField] Sprite _deadSprite;
    [SerializeField] ParticleSystem _particleSystem;
    //when we die ,we set to true so we dont die 2x
    bool _hasDied;
   
    // On collision - reference to the object - bird 
    // We want to determine whether we should die from collision or not
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieFromCollision(collision))
        {
            StartCoroutine(Die());
        }
    }
    bool ShouldDieFromCollision(Collision2D collision)
    {
        if (_hasDied)
            return false;
        
        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird != null)
            return true;
        //contacts - collision of array
        // .y negative value will check if it comes from above
        if (collision.contacts[0].normal.y < -0.5)
            return true;
        
        return false;
    }

    IEnumerator Die()
    {
        _hasDied = true;
        GetComponent<SpriteRenderer>().sprite = _deadSprite;
        _particleSystem.Play();
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
    
