using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbProperties : MonoBehaviour, ICollectable
{
    public GameStates game;
    public SoundSystem soundEffects;

    public Orb orbType;

    int threshold;
    int currentMultiplier;
    int highMultiplier;
    bool isMultiplierActive;
    int scoreIncrease;

    Orb red = Orb.red;
    Orb green = Orb.green;
    Orb blue = Orb.blue;
    Orb white = Orb.white;
    Orb activeOrb;

    void Start()
    {
        game = GameObject.FindObjectOfType<GameStates>();
        soundEffects = GameObject.FindObjectOfType<SoundSystem>();
    }

    void OnTriggerEnter(Collider other)
    {
        threshold = GameStates.switchingThreshold;
        currentMultiplier = GameStates.currentMultiplier;
        highMultiplier = GameStates.highMultiplier;
        isMultiplierActive = currentMultiplier == highMultiplier;
        scoreIncrease = GameStates.scorePerOrb;
        activeOrb = GameStates.activeOrb;

        if (other.CompareTag("Player"))
        {
            //increase score & orb score accordingly
            OnPlayerCollect(orbType);
            //resets currentMultiplier after first orb is collected
            GameStates.currentMultiplier = GameStates.baseMultiplier;
            //destroy collected orb
            Destroy(gameObject);
        }
    }

    public void OnPlayerCollect(Orb collectableType)
    {
        int redOrbs = game.GetComponent<GameStates>().redOrbs;
        int greenOrbs = game.GetComponent<GameStates>().greenOrbs;
        int blueOrbs = game.GetComponent<GameStates>().blueOrbs;

        switch (collectableType)
        {
            case Orb.blue:
                if(blueOrbs < threshold)
                {
                    //if the active orb is not nmatching the collected orb then
                       //if there is no multiplier then
                       //add the score increase else
                           //if the blue orbs is less than the threshold after the increase
                           //add double the increase else
                           //set the orbs to the threshold else
                    //else keep the orbs as they are
                        if(activeOrb != blue)
                        {
                            if (!isMultiplierActive)
                            {
                                blueOrbs = blueOrbs + scoreIncrease;
                            } else
                            {
                                if(blueOrbs + (2 * scoreIncrease) < threshold)
                                {
                                    blueOrbs = blueOrbs + (2 * scoreIncrease);
                                } else
                                {
                                    blueOrbs = threshold;
                                }
                            }
                        }
                }
                //if the active orb is matching the collected orb then
                // add double the score increase else
                // if there is NO active multiplier then
                // add the score increase else
                // add the multiplier * the score increase
                game.GetComponent<GameStates>().score +=
                    activeOrb == blue ?
                    2 * scoreIncrease :
                    !isMultiplierActive ?
                    scoreIncrease : highMultiplier * scoreIncrease;
                break;
            case Orb.red:
                if(redOrbs < threshold)
                {
                    if (activeOrb != red)
                    {
                        if (!isMultiplierActive)
                        {
                            redOrbs = redOrbs + scoreIncrease;
                        }
                        else
                        {
                            if (redOrbs + (2 * scoreIncrease) < threshold)
                            {
                                redOrbs = redOrbs + (2 * scoreIncrease);
                            }
                            else
                            {
                                redOrbs = threshold;
                            }
                        }
                    }
                }

                game.GetComponent<GameStates>().score +=
                    activeOrb == red ? 2 * scoreIncrease :
                    !isMultiplierActive ?
                    scoreIncrease : highMultiplier * scoreIncrease;
                break;
            case Orb.green:
                if(greenOrbs < threshold)
                {
                    greenOrbs =
                        activeOrb != green ?
                        greenOrbs + scoreIncrease : greenOrbs;
                }
                //if the active orb is green and the collected orb is green then
                //if there is no active currentMultiplier then
                //add double the score else
                //add double the multplier as it is green else
                //add the normal score
                game.GetComponent<GameStates>().score +=
                    activeOrb == green ?
                    !isMultiplierActive ? 2 * scoreIncrease :
                    2 * highMultiplier * scoreIncrease : scoreIncrease;
                break;
            default: break;
        }
        game.GetComponent<GameStates>().redOrbs = redOrbs;
        game.GetComponent<GameStates>().greenOrbs = greenOrbs;
        game.GetComponent<GameStates>().blueOrbs = blueOrbs;
        if (MusicToggle.isMute == false)
        {
            soundEffects.PlaySFX(Events.collectOrbs);
        }
        game.GetComponent<GameStates>().UpdateScore();
    }
}
