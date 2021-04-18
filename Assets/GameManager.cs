using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float spawnTime = 0.5f;
    [SerializeField] private float moveDuration = 0.5f;
    [SerializeField] private int scoreMultiplier = 1;
    [SerializeField] private float timeScaleMultiplier = 0.1f;
    [Header("Text Components")]
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text countDownTimer;
    [SerializeField] private TMP_Text highScore;
    [SerializeField] private TMP_Text roundScore;
    [SerializeField] private GameObject gameOverSplash;
    [SerializeField] private Image sliderImage;
    [SerializeField] private TMP_Text addedScore;
    [SerializeField] private TMP_Text missCounta;
    [SerializeField] private TMP_Text maxCounta;
    [SerializeField] private TMP_Text TimerText;
    [SerializeField] private string startMenuScene;
    [Header("Spawners")]
    [SerializeField] private GameObject[] hpBars = new GameObject[4];
    [SerializeField] private Transform[] spawnPoint = new Transform[7];
    [SerializeField] private Transform[] endPoint = new Transform[7];
    [SerializeField] private GameObject[] spawners = new GameObject[7];
    [SerializeField] private Volume postProccessURP;
    [Header("Audio Files FX")]
    [SerializeField] private AudioSource mainSong;
    [SerializeField] private AudioSource wrongHitFx;
    [SerializeField] private AudioSource rightHitFx;
    [SerializeField] private RotatingGridSaw gridSawMode;
    [SerializeField] private RotatingPow bonusPow;
    [SerializeField] private CollideScript godModeCall;
    [SerializeField] private Camera camx;
    [SerializeField] private ElectricScript electss;
    [SerializeField] private ObstaclesFiveScript obsFivee;
    private Bloom blooms;
    private string activeScenes;
    private int rightCounter;
    private int maxRightCounter;
    private int wrongCounter;
    private int scoreContainer;
    private bool go = false;
    private Color defaultUrpColor = new Color(110, 18, 255);
    public Transform[] SpawnPoint { get { return spawnPoint; } }
    public bool BonusStages { get { return bonusStage; } }
    float timer = 0.0f;
    private bool gidSawModeOne = false;
    private bool gidSawModeTwo = false;
    private bool thirty = false;
    private bool ten = false;
    private float minutes;
    private float seconds;
    public void BonusPowaActivate(bool state)
    {
        bonusStage = state;
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        minutes = Mathf.FloorToInt(timer / 60);
        seconds = Mathf.FloorToInt(timer % 60);
        TimerText.SetText(new StringBuilder(string.Format("{0:00}:{1:00}", minutes, seconds)));
    }
    void OnEnable()
    {
        postProccessURP.profile.TryGet(out blooms);
        activeScenes = SceneManager.GetActiveScene().name;
    }

    void Start()
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].SetActive(false);
        }

        StartCoroutine(SpawnPointToEndPoint());
        StartCoroutine(ObstaclesStartTimer());
        if(mainSong != null)
        {
            mainSong.Play();
        }
    }
    private float minutesCounter = 30f;
    WaitForSecondsRealtime waitSpeed = new WaitForSecondsRealtime(1f);
    WaitForSecondsRealtime waitAbit = new WaitForSecondsRealtime(0.06f);
    private IEnumerator ObstaclesStartTimer()
    {
        while (true)
        {
            yield return waitAbit;
            if (!thirty) //Kids at home, this is to not to let your game check unnecessary things once done!
            {
                if (seconds == 40 && minutes == 00 && gidSawModeOne == false)
                {
                    thirty = true;
                    gidSawModeOne = true;
                    gridSawMode.PlayGridSaw(0);
                }
            }
            if (!ten) //Kids at home, this is to not to let your game check unnecessary things once done!
            {
                if (seconds == 30 && minutes == 00 && gidSawModeTwo == false)
                {
                    ten = true;
                    gidSawModeTwo = true;
                    gridSawMode.PlayGridSaw(1);
                }
            }
            if (minutesCounter == seconds)
            {
                Time.timeScale += timeScaleMultiplier;
                yield return waitSpeed;
            }
            if(seconds == 50f)
            {
                electss.StartObstaclesFour();
            }
            if(ten)
            {
                if(seconds == 20f)
                {
                    electss.StartObstaclesFour();
                }
            }
            if(seconds == 10f && minutes == 1f)
            {
                obFive = true;
                yield return null;
            }
            if(obFive)
            {
                if(seconds == 10)
                {
                    obsFivee.ObstaclesFivee();
                }
            }
        }
    }
    private bool obFive = false;
    //private int randPosVc;
    private bool bonusStage = false;
    private int missCounter = 0;
    private List<float> cacheEndPointYpos = new List<float>(); 
    private List<Vector3> cacheSpawnPoint = new List<Vector3>(); 
    //private Color cacheRedsColor;
    private IEnumerator SpawnPointToEndPoint()
    {
        //Cache End point to a list bcos transform.position is ASS!
        for(int p = 0; p < endPoint.Length; p++)
        {
            cacheEndPointYpos.Add(endPoint[p].transform.position.y);
        }
        //Cache Spawn point to a list bcos transform.position is ASS!
        for(int o = 0; o < spawnPoint.Length; o++)
        {
            cacheSpawnPoint.Add(spawnPoint[o].transform.position);
        }
        StartCoroutine(CountDownTimer());
        yield return new WaitUntil(() => go == true);

        while (true)
        {            
            for (int i = 0; i < 1; i++)
            {
                    int randInt = Random.Range(0, spawners.Length);
                    if (!spawners[randInt].activeSelf)
                    {
                        //int randSpanP = Random.Range(0, spawnPoint.Length);
                        spawners[randInt].SetActive(true);
                        //int randPosVc = Random.Range(0, spawnPoint.Length);
                        spawners[randInt].transform.position = cacheSpawnPoint[Random.Range(0, cacheSpawnPoint.Count)];    
                        if(spawners[randInt].name == "Donutss")
                        {
                            spawners[randInt].transform.rotation = Quaternion.identity;
                            LeanTween.rotateAround(spawners[randInt], Vector3.right, -360f,  0.5f).setLoopClamp();
                        }
                        if(spawners[randInt].name == "Whitess" || spawners[randInt].name == "Redss")
                        {
                            LeanTween.rotateAround(spawners[randInt], Vector3.forward, -360f,  1f).setLoopClamp();
                        }
                        
                        LeanTween.moveY(spawners[randInt], cacheEndPointYpos[Random.Range(0, spawnPoint.Length)], moveDuration).setOnComplete(
                                        () =>
                                        {
                                            if (spawners[randInt].name == "Redss")
                                            {
                                                missCounter++;
                                                missCounta.SetText(new StringBuilder(missCounter.ToString()));
                                            }
                                            
                                            LeanTween.cancel(spawners[randInt]);
                                            //int randPosVcTwo = Random.Range(0, spawnPoint.Length);
                                            spawners[randInt].transform.position = cacheSpawnPoint[Random.Range(0, cacheSpawnPoint.Count)];     
                                            spawners[randInt].SetActive(false);                                   
                                        });
                                        yield return new WaitForSeconds(spawnTime);                                    
                    }
                    else
                    {
                        yield return null;
                        continue;
                    }

                yield return null;
            }
        }
    }

    private float jumpIter = 9.5f;
    public void ScreenShake()
    {
        /**************
        * Camera Shake
        **************/
        if (!LeanTween.isTweening(camx.gameObject))
        {
            float height = Mathf.PerlinNoise(jumpIter, 0f) * 5f;
            height = height * height * 0.2f;

            float shakeAmt = height * 0.9f; // the degrees to shake the camera
            float shakePeriodTime = 0.2f; // The period of each shake
            float dropOffTime = 0.3f; // How long it takes the shaking to settle down to nothing
            LTDescr shakeTween = LeanTween.rotateAroundLocal(camx.gameObject, Vector3.right, shakeAmt, shakePeriodTime)
            .setEase(LeanTweenType.easeShake) // this is a special ease that is good for shaking
            .setLoopClamp()
            .setRepeat(-1);

            // Slow the camera shake down to zero
            LeanTween.value(camx.gameObject, shakeAmt, 0f, dropOffTime).setOnUpdate(
                (float val) =>
                {
                    shakeTween.setTo(Vector3.right * val);
                }
            ).setEase(LeanTweenType.easeOutQuad).setOnComplete(
                () =>
                {
                    LeanTween.cancel(camx.gameObject);
                    camx.transform.rotation = Quaternion.identity;
                });
        }
    }
    public void CounterLogic(int counter)
    {
        //1 = REDSS, 0 = WHITESS
        if (counter == 1 && go == true)
        {
            if (rightCounter != 0)
            {
                int formula = 10 * scoreMultiplier * rightCounter / 2;
                TweenAddedScore(formula);
                scoreContainer += formula;
                StringBuilder strB = new StringBuilder(scoreContainer.ToString());
                score.SetText(strB);
                rightHitFx.Play();
            }
            else
            {
                int formula = 10 * scoreMultiplier;
                TweenAddedScore(formula);
                scoreContainer += formula;
                StringBuilder strB = new StringBuilder(scoreContainer.ToString());
                score.SetText(strB);
                rightHitFx.Play();
            }
            //Tint Effects
            ColorTintEffects();

            rightCounter++;
            maxRightCounter++;
            maxCounta.SetText(new StringBuilder(maxRightCounter.ToString()));

            if (sliderImage.fillAmount != 1 && bonusStage == false)
            {
                sliderImage.fillAmount += 0.02f;
            }
            if (sliderImage.fillAmount == 1 && bonusStage == false)
            {
                BonusPowaActivate(true);
                bonusPow.Powa();
            }
        }
        if (counter == 0 && go == true)
        {
            wrongHitFx.Play();
            if (wrongCounter == 5)
            {
                
                //Game over
                int hgScore = PlayerPrefs.GetInt("HighScores");

                if (scoreContainer > hgScore)
                {
                    PlayerPrefs.SetInt("HighScores", scoreContainer);
                }

                gameOverSplash.SetActive(true);
                go = false;
                blooms.tint.Override(defaultUrpColor);
                roundScore.SetText("Round Score:" + score.text);
                highScore.SetText("High Score:" + PlayerPrefs.GetInt("HighScores").ToString());
                
            }
            wrongCounter++;
            switch (wrongCounter)
            {
                case 1:
                    hpBars[4].SetActive(false);
                    break;
                case 2:
                    hpBars[3].SetActive(false);
                    break;
                case 3:
                    hpBars[2].SetActive(false);
                    break;
                case 4:
                    hpBars[1].SetActive(false);
                    break;
                case 5:
                    hpBars[0].SetActive(false);
                    break;
            }
            rightCounter = 0;
        }
    }
    public void BonusTimer()
    {
        LeanTween.value(sliderImage.gameObject, 2f, 0f, 20f).setIgnoreTimeScale(true).setOnUpdate(
            (float val) =>
            {
                sliderImage.fillAmount = val;
            }).setOnComplete(
            () =>
            {
                LeanTween.cancel(sliderImage.gameObject);
                godModeCall.ResetColliderSize(false, 0);
                BonusPowaActivate(false);                
            });
    }
    private Vector3 defTxtAddePos;
    private void TweenAddedScore(int addedscor)
    {
        if (defTxtAddePos.x == 0)
        {
            defTxtAddePos = addedScore.transform.position;
        }
        addedScore.SetText(new StringBuilder("+" + addedscor.ToString() + "  CHAIN:" + rightCounter.ToString()));
        LeanTween.moveY(addedScore.gameObject, defTxtAddePos.y, 0.2f).setFrom(defTxtAddePos.y - 50f).updateNow().setOnComplete(
                () =>
                {
                    addedScore.transform.position = defTxtAddePos;
                }).setIgnoreTimeScale(true);
    }
    private int colorTintCounter = 0;
    private Color colorOne = new Color(18, 255, 245);
    private Color colorTwo = new Color(255, 18, 239);
    private void ColorTintEffects()
    {
        colorTintCounter++;

        switch (colorTintCounter)
        {
            case 1:
                blooms.tint.Override(Color.red);
                break;
            case 2:
                blooms.tint.Override(colorOne);
                break;
            case 3:
                blooms.tint.Override(colorTwo);
                break;
            case 4:
                blooms.tint.Override(defaultUrpColor);
                break;
            case 5:
                blooms.tint.Override(Color.cyan);
                colorTintCounter = 0;
                break;
        }
    }

    public void ResetScene()
    {
        if (!String.IsNullOrEmpty(activeScenes))
            SceneManager.LoadScene(activeScenes);
    }
    public void BackToMainMenu()
    {
        if (!String.IsNullOrEmpty(startMenuScene))
            SceneManager.LoadScene(startMenuScene);
    }
    WaitForSeconds waitCountDown = new WaitForSeconds(1f);
    private IEnumerator CountDownTimer()
    {
        if (countDownTimer != null)
        {
            countDownTimer.gameObject.SetActive(true);
            int dummyCounter = 3;
            for (int i = 0; i < 3; i++)
            {
                countDownTimer.SetText(dummyCounter.ToString());
                yield return waitCountDown;
                dummyCounter--;
            }
            countDownTimer.SetText("GO!");
            yield return waitCountDown;
            LeanTween.scale(countDownTimer.gameObject, Vector3.zero, 0.5f).setEaseInBack().setOnComplete(
                () =>
                {
                    go = true;
                    countDownTimer.gameObject.SetActive(false);
                });
            LeanTween.rotateAround(countDownTimer.gameObject, Vector3.forward, 180f, 0.3f).setDelay(0.2f);
        }
    }
}
