using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonMoving : MonoBehaviour
{
    public Text Score;
    public GameObject Disc;
    float speed = 1.5f;
    int collide, AnimLifetimeEnded, animstate;
    public int clicking, score;
    Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
        score = PlayerPrefs.GetInt("Score");
    }

    private void Update() {
        //На начало анимации AnimLifetimeEnded = 1, а концу AnimLifetimeEnded = 2. В остальное время AnimLifetimeEnded = 0.
        if (AnimLifetimeEnded == 2) {
            transform.position = new Vector3(-5, 0, 0);
            anim.SetInteger("State", 0);
            AnimLifetimeEnded = 0;
        }
        //Если анимация не проигрывается, то можно двигаться
        if (AnimLifetimeEnded == 0) {
            gameObject.GetComponent<Animator>().applyRootMotion = true;
        }
        //Если анимация проигрывается, то нельзя двигаться
        if (AnimLifetimeEnded == 1) {
            gameObject.GetComponent<Animator>().applyRootMotion = false;
        }

        Score.text = score.ToString();
        //Двигаемя непрерывно вперед
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(10, transform.position.y, 0), speed * Time.deltaTime);
        if (transform.position.x == 10) {
            transform.position = new Vector3(-5, 0, 0);
        }

        //Если мы на диске и при этом кликнули, то проигрывается анимация
        if (clicking == 1 && collide == 1) {
            anim.SetInteger("State", animstate);
            score += 1;
            PlayerPrefs.SetInt("Score", score);
        }
        clicking = 0;

        //Относительно счета выбираем размер диска и высоту прыжка
        if (score == 0 || score == 1 || score == 2) {
            if (score == 0) {
                Disc.transform.localScale = new Vector3(1, 1, 0.04f);
                animstate = 1;
            }
            if (score == 1) {
                Disc.transform.localScale = new Vector3(2, 2, 0.04f);
                animstate = 2;
            }
            if (score == 2) {
                Disc.transform.localScale = new Vector3(3, 3, 0.04f);
                animstate = 3;
            }
        }
        else {
            if (score % 3 == 0) {
                Disc.transform.localScale = new Vector3(1, 1, 0.04f);
                animstate = 1;
            }
            else if (score % 3 == 1) {
                Disc.transform.localScale = new Vector3(2, 2, 0.04f);
                animstate = 2;
            }
            else if (score % 3 == 2) {
                Disc.transform.localScale = new Vector3(3, 3, 0.04f);
                animstate = 3;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        collide = 1;
    }

    private void OnTriggerExit(Collider other) {
        collide = 0;
    }

    //Анимация начинается
    public void Started() {
        AnimLifetimeEnded = 1;
    }

    //Анимаця заканчивается
    public void Ended() {
        AnimLifetimeEnded = 2;
    }
}
