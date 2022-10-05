using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    public enum CollectableType
    {
        Points,
        Damage,
        Health,
        Win
    }

    public CollectableType _collectableType;

    private float rotAng = 0;

    public int pointValue = 10;

    public float deltaHealth = 0;

    public float deltaSecond = 0;

    public GameObject _WinUI;

    public TextMeshProUGUI _WinScore;

    private void Start()
    {
        _WinUI.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    { 
        if(collision.collider.tag == "Player")
        {
            switch(_collectableType)
            {
                case CollectableType.Points:
                    ScoreManager.instance.AddScore(pointValue);
                    Destroy(gameObject);
                    break;

                case CollectableType.Health:
                    if(deltaSecond == 0)
                    {
                        HealthManager.instance.recoverInstantHealth(deltaHealth);
                        Destroy(gameObject);
                    }
                    else
                    {
                        HealthManager.instance.recoverContinuousHealth(deltaHealth, deltaSecond);
                        Destroy(gameObject);
                    }
                    break;

                case CollectableType.Damage:
                    if (deltaSecond == 0)
                    {
                        HealthManager.instance.instantDamage(deltaHealth);
                        Destroy(gameObject);
                    }
                    else
                    {
                        HealthManager.instance.countinuousDamage(deltaHealth, deltaSecond);
                        Destroy(gameObject);
                    }
                    break;

                case CollectableType.Win:
                    Destroy(gameObject);
                    _WinUI.SetActive(true);
                    int finalScore = ScoreManager.instance.score + (int)HealthManager.instance._health * 2;
                    _WinScore.text = 
                        "Your Points: [" + ScoreManager.instance.score + "]\n"
                        + "Your Health: [" + HealthManager.instance._health + "]\n"
                        + "Your Final Score: [" + ScoreManager.instance.score + "] + [" + HealthManager.instance._health + "] * 2 = [" + finalScore + "]";
                    break;

                default:
                    break;
            }

        }
    }

    private void FixedUpdate()
    {
        rotAng = rotAng >= 360.0f ? 0.0f : rotAng + 1;
        this.transform.rotation = Quaternion.Euler(-90.0f, rotAng, 0.0f);
    }
}
