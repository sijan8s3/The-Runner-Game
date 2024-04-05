using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public LaneSwitch player;
    public GameStates game;
    public SoundSystem soundEffects;

    Orb red = Orb.red;
    Orb green = Orb.green;
    Orb blue = Orb.blue;
    Orb white = Orb.white;
    Orb activeOrb;

    void Start()
    {
        //reference the only game states object in the scene
        game = GameObject.FindObjectOfType<GameStates>();
        //reference the only player object in the scene
        player = GameObject.FindObjectOfType<LaneSwitch>();
        soundEffects = GameObject.FindObjectOfType<SoundSystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameStates.isInvincible)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            return;
        }
        else
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;

        }
        activeOrb = GameStates.activeOrb;
        if (collision.collider.CompareTag("Player"))
        {
            if (!GameStates.isShieldActive && activeOrb == Orb.white)
            {
                Destroy(player.GetComponent<LaneSwitch>().gameObject);
                game.GetComponent<GameStates>().GameOver();
            } else
            {
                Destroy(gameObject);
                if (!GameStates.isShieldActive)
                {
                    game.GetComponent<GameStates>().ChangeOrb(white);
                }
                GameStates.isShieldActive = false;
                player.transform.GetChild(1).gameObject.SetActive(false);

            }
        }
        if (MusicToggle.isMute == false)
        {
            soundEffects.PlaySFX(Events.hitObstacle);
        }
    }
}
