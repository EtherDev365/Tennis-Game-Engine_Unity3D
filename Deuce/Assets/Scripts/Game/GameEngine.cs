using Assets.Scripts.GamePlay;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum Score
{
    First = 0,
    Second = 15,
    Third = 30,
    Forth = 40
}

public class GameEngine : MonoBehaviour
{
    public static GameEngine Instance;
    public Image[] playerAvatars;
    public Sprite[] tacticCardSprites;
    public GameObject[] tiles;
    public GameObject[] player1ServeTiles;
    public GameObject[] player2ServeTiles;
    public GameObject[] player1whereToServeTiles;
    public GameObject[] player2whereToserveTiles;
    public GameObject[] playerPans;
    public GameObject[] notifications;
    public GameObject[] numberTexts;
    public Sprite [] coach1Commands;
    public Sprite[] coach2Commands;
    public Image[] coachCommandImages;
    public GameObject[] rollBall;
    public GameObject[] tennisBall;
    public GameObject player1Position;
    public GameObject player2Position;
    public GameObject[] lines;
    public GameObject[] rollText;
    public GameObject tacticalCards1;
    public GameObject characterCards;
    public GameObject panel;
    public GameObject[] serveNotifications;
    public Text player1ScoreText;
    public Text player1LevelText;
    public Text player2ScoreText;
    public Text player2LevelText;
    public GameObject strokeCardButton;
    public GameObject strokeCards;
    //public Sprite rollNetSprite;
    public GameObject[] netGameObject;

    int gameRoundNumber;
    public bool isNoRepeat;
    public bool isMoveToStrike;

    public PlayerInfo player1 = new PlayerInfo();
    public PlayerInfo player2 = new PlayerInfo();
    public GamePlayInfo gameplayInfo = new GamePlayInfo();
    int rowNumber;
    int columnNumber;
    int rollNumber;
    int x;
    int y;
    int x1; int y1; int x2; int y2;
    int maxMove;
    int step;    

    public bool isFirstServeFailed;
    bool isHideMessgae;
    public bool isServeState;
    public bool isNoMove;

    int SpecialStrokeCardUseCount;
    public bool isTopSpin;
    public bool isSliceShot;
    public bool isPowerShot;
    public bool isLobShot;
    public bool isDropShot;

    void Awake()
    {
        Instance = this;   
    }

    // Start is called before the first frame update
    void Start()
    {
        player1.Level = 0;
        player1.Score = (int)Score.First;
        player2.Level = 0;
        player2.Level = (int)Score.First;
        player1.StrokeCardCount = 0;
        player2.StrokeCardCount = 0;

        int index = UnityEngine.Random.RandomRange(0, 2);

        if (index == 0)
        {
            player1.IsMyServe = true;
            player2.IsMyServe = false;
            serveNotifications[0].GetComponent<Image>().enabled = true;
            serveNotifications[1].GetComponent<Image>().enabled = false;
            player1.IsMyTurn = true;
            player2.IsMyTurn = false;
        }
        else
        {
            player1.IsMyServe = false;
            player2.IsMyServe = true;
            player1.IsMyTurn = false;
            player2.IsMyTurn = true;
            serveNotifications[0].GetComponent<Image>().enabled = false;
            serveNotifications[1].GetComponent<Image>().enabled = true;
        }       
    }

    public void FormatBoolFlag()
    {
        isTopSpin = false;
        isSliceShot = false;
        isPowerShot = false;
        isLobShot = false;
        isDropShot = false;
        player1.BonusMove = 0;
        player2.BonusMove = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if (player1.IsMyTurn)
        //{
        //    print("Player1");
        //}
        //else if (player2.IsMyTurn)
        //{
        //    print("Player2");
        //}

        if (gameplayInfo.IsStart && isNoRepeat)
        {
            isNoRepeat = false;
            isFirstServeFailed = false;
            gameplayInfo.IsStart = false;
            gameplayInfo.IsServe = true;
            notifications[0].SetActive(true);
            characterCards.SetActive(false);
            tacticalCards1.SetActive(true);
            panel.SetActive(true);
            player1.StrokeCardCount = 0;
            player2.StrokeCardCount = 0;
            FormatBoolFlag();         

            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
                tiles[i].GetComponentsInChildren<Image>()[1].enabled = false;
            }

            for (int i = 0; i < numberTexts.Length; i++)
            {
                numberTexts[i].SetActive(false);
            }
            
            lines[0].SetActive(false);
            lines[1].SetActive(false);
            tennisBall[0].SetActive(false);
            tennisBall[1].SetActive(false);
            playerPans[0].SetActive(false);
            playerPans[1].SetActive(false);

        }
        else if (gameplayInfo.IsServe && isNoRepeat)
        {
            isNoRepeat = false;
            isServeState = true;
            rollBall[0].SetActive(false);
            rollBall[1].SetActive(false);
            rollText[0].SetActive(false);
            rollText[1].SetActive(false);

            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
                tiles[i].GetComponentsInChildren<Image>()[1].enabled = false;
            }

            for (int i = 0; i < numberTexts.Length; i++)
            {
                numberTexts[i].SetActive(false);
            }

            lines[0].SetActive(false);
            lines[1].SetActive(false);
            tennisBall[0].SetActive(false);
            tennisBall[1].SetActive(false);
            playerPans[0].SetActive(false);
            playerPans[1].SetActive(false);
            //notifications[2].SetActive(true);

            if (player1.IsMyTurn)
            {
                coachCommandImages[0].gameObject.SetActive(true);
                coachCommandImages[0].sprite = coach1Commands[6];

            }
            else if (player2.IsMyTurn)
            {
                coachCommandImages[1].gameObject.SetActive(true);
                coachCommandImages[1].sprite = coach2Commands[6];

            }

            //StartCoroutine("ServeMessgaeShowDelay");            

            for (int i = 0; i < player1ServeTiles.Length; i++)
            {
                player1ServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
                //player1ServeTiles[i].GetComponentsInChildren<Image>()[1].enabled = false;
            }

            if (player1.IsMyServe)
            {
                player1.IsMyTurn = true;
                player2.IsMyTurn = false;

                if (((int)player1.Score + (int)player2.Score) % 2 == 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        player1ServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                    }
                }
                else
                {
                    for (int i = 3; i < 6; i++)
                    {
                        player1ServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                    }
                }
                
            }
            else if (player2.IsMyServe)
            {
                player1.IsMyTurn = false;
                player2.IsMyTurn = true;

                if (((int)player1.Score + (int)player2.Score) % 2 == 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        player2ServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                    }
                }
                else
                {
                    for (int i = 3; i < 6; i++)
                    {
                        player2ServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                    }
                }
                
            }            
        }
        else if (gameplayInfo.IsReceive && isNoRepeat)
        {
            isNoRepeat = false;
            notifications[2].SetActive(false);
            //notifications[3].SetActive(true);
            MessgaeShowDelay();

            if (player1.IsMyTurn)
            {
                coachCommandImages[0].gameObject.SetActive(true);
                coachCommandImages[0].sprite = coach1Commands[6];

            }
            else if (player2.IsMyTurn)
            {
                coachCommandImages[1].gameObject.SetActive(true);
                coachCommandImages[1].sprite = coach2Commands[6];

            }

            //StartCoroutine("ReceiveMessgaeShowDelay");            

            if (player2.IsMyTurn)
            {
                if (((int)player1.Score + (int)player2.Score) % 2 == 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        player2ServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                    }
                }
                else
                {
                    for (int i = 3; i < 6; i++)
                    {
                        player2ServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                    }
                }                
            }
            else if (player1.IsMyTurn)
            {
                if (((int)player1.Score + (int)player2.Score) % 2 == 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        player1ServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                    }
                }
                else
                {
                    for (int i = 3; i < 6; i++)
                    {
                        player1ServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                    }
                }
                
            }

        }
        else if (gameplayInfo.IsWhereToServe && isNoRepeat)
        {
            isNoRepeat = false;
            notifications[3].SetActive(false);
            //notifications[10].SetActive(true);
            MessgaeShowDelay();

            if (player1.IsMyTurn)
            {
                coachCommandImages[0].gameObject.SetActive(true);
                coachCommandImages[0].sprite = coach1Commands[5];

            }
            else if (player2.IsMyTurn)
            {
                coachCommandImages[1].gameObject.SetActive(true);
                coachCommandImages[1].sprite = coach2Commands[5];

            }

            //StartCoroutine("AimShotMessgaeShowDelay");           

            if (player2.IsMyTurn)
            {
                if (((int)player1.Score + (int)player2.Score) % 2 == 0)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        player2whereToserveTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                    }
                }
                else
                {
                    for (int i = 9; i < 18; i++)
                    {
                        player2whereToserveTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                    }
                }                
            }
            else if (player1.IsMyTurn)
            {
                if (((int)player1.Score + (int)player2.Score) % 2 == 0)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        player1whereToServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                    }
                }
                else
                {
                    for (int i = 9; i < 18; i++)
                    {
                        player1whereToServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                    }
                }

               
            }
        }
        else if (gameplayInfo.IsRoll && isNoRepeat)
        {
            isNoRepeat = false;
            //notifications[10].SetActive(false);
            //notifications[4].SetActive(true);
            MessgaeShowDelay();

            if (player1.IsMyTurn)
            {
                coachCommandImages[0].gameObject.SetActive(true);
                coachCommandImages[0].sprite = coach1Commands[0];
                rollBall[0].SetActive(true);
                rollBall[0].GetComponent<Animator>().enabled = true;
                //netGameObject[0].SetActive(true);
            }
            else if (player2.IsMyTurn)
            {
                coachCommandImages[1].gameObject.SetActive(true);
                coachCommandImages[1].sprite = coach2Commands[0];
                rollBall[1].SetActive(true);
                rollBall[1].GetComponent<Animator>().enabled = true;
                //netGameObject[1].SetActive(true);
            }

           
            StartCoroutine("RollMessgaeShowDelay"); 
        }
        else if (gameplayInfo.IsRoll && isHideMessgae)
        {
            isHideMessgae = false;           
            
            notifications[4].SetActive(false);

            gameplayInfo.IsRoll = false;

            if (rollNumber == 0)
            {
                gameplayInfo.IsHitNet = true;
                isNoRepeat = true;
                //notifications[11].SetActive(true);
                MessgaeShowDelay();                

                if (player1.IsMyTurn)
                {
                    coachCommandImages[0].gameObject.SetActive(true);
                    coachCommandImages[0].sprite = coach1Commands[1];

                }
                else if (player2.IsMyTurn)
                {
                    coachCommandImages[1].gameObject.SetActive(true);
                    coachCommandImages[1].sprite = coach2Commands[1];

                }

                //StartCoroutine("NetMessgaeShowDelay");
            }
            else
            {
                PlaceTennisBall(rollNumber);

                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i].SetActive(true);
                }

                if (player1.IsMyTurn)
                {
                    DrawLine(lines[0], playerPans[0], tennisBall[1]);
                    DrawLine(lines[1], tennisBall[0], tennisBall[1]);
                    ShowMoveRange(player1Position);
                }
                else if (player2.IsMyTurn)
                {
                    DrawLine(lines[0], playerPans[1], tennisBall[1]);
                    DrawLine(lines[1], tennisBall[0], tennisBall[1]);
                    ShowMoveRange(player2Position);
                }

                if (isServeState)
                {
                    if (OutOfRangeServe(numberTexts[rollNumber - 1]))
                    {
                        if (isFirstServeFailed)
                        {
                            isFirstServeFailed = false;
                            

                            if (player1.IsMyTurn)
                            {
                                GetScore(player2);
                            }
                            else if (player2.IsMyTurn)
                            {
                                GetScore(player1);
                            }
                        }
                        else
                        {
                            isFirstServeFailed = true;
                            gameplayInfo.IsServe = true;
                            isNoRepeat = true;
                        }
                    }
                    else
                    {
                        gameplayInfo.IsRecoveryMove = true;
                        //notifications[5].SetActive(true);
                        MessgaeShowDelay();

                        if (player1.IsMyTurn)
                        {
                            coachCommandImages[0].gameObject.SetActive(true);
                            coachCommandImages[0].sprite = coach1Commands[4];

                        }
                        else if (player2.IsMyTurn)
                        {
                            coachCommandImages[1].gameObject.SetActive(true);
                            coachCommandImages[1].sprite = coach2Commands[4];

                        }

                        //StartCoroutine("RecoverMessgaeShowDelay");
                    }
                }
                else
                {
                    if (OutOfRange(numberTexts[rollNumber - 1]))
                    {
                        isFirstServeFailed = false;                        

                        if (player1.IsMyTurn)
                        {
                            GetScore(player2);
                        }
                        else if (player2.IsMyTurn)
                        {
                            GetScore(player1);
                        }
                    }
                    else
                    {
                        gameplayInfo.IsRecoveryMove = true;
                        //notifications[5].SetActive(true);
                        MessgaeShowDelay();

                        if (player1.IsMyTurn)
                        {
                            coachCommandImages[0].gameObject.SetActive(true);
                            coachCommandImages[0].sprite = coach1Commands[4];

                        }
                        else if (player2.IsMyTurn)
                        {
                            coachCommandImages[1].gameObject.SetActive(true);
                            coachCommandImages[1].sprite = coach2Commands[4];

                        }

                        RecoverMessgaeShowDelay();
                        //StartCoroutine("RecoverMessgaeShowDelay");
                    }
                }
                   
            }
        }
        else if (gameplayInfo.IsHitNet && isNoRepeat)
        {
            isNoRepeat = false;
            notifications[11].SetActive(false);
            gameplayInfo.IsHitNet = false;
            ShowRollNumber();

            if (isServeState)
            {
                if (isFirstServeFailed)
                {
                    isFirstServeFailed = false;                   

                    if (player1.IsMyTurn)
                    {
                        GetScore(player2);
                    }
                    else if (player2.IsMyTurn)
                    {
                        GetScore(player1);
                    }
                }
                else
                {
                    isFirstServeFailed = true;
                    gameplayInfo.IsServe = true;
                    isNoRepeat = true;
                }
            }
            else
            {
                isFirstServeFailed = false;                

                if (player1.IsMyTurn)
                {
                    GetScore(player2);
                }
                else if (player2.IsMyTurn)
                {
                    GetScore(player1);
                }
            }           
        }
        else if (gameplayInfo.IsMoveToStrike && isNoRepeat)
        {           
            isNoRepeat = false;
            isServeState = false;
            notifications[5].SetActive(false);
            //notifications[9].SetActive(true);                      
            MessgaeShowDelay();

            if (player1.IsMyTurn)
            {
                ShowMoveRange(player1Position);                

                coachCommandImages[0].gameObject.SetActive(true);
                coachCommandImages[0].sprite = coach1Commands[3];
            }
            else if (player2.IsMyTurn)
            {
                ShowMoveRange(player2Position);
                
                coachCommandImages[1].gameObject.SetActive(true);
                coachCommandImages[1].sprite = coach2Commands[3];

            }

            ShowMoveToStrike(tennisBall[0]);

            //StartCoroutine("MoveMessgaeShowDelay");
        }        
        else if (gameplayInfo.IsWhereToStrike && isNoRepeat)
        {
            isNoRepeat = false;
            MessgaeShowDelay();

            if (player1.IsMyTurn)
            {                
                coachCommandImages[0].gameObject.SetActive(true);
                coachCommandImages[0].sprite = coach1Commands[2];
            }
            else if (player2.IsMyTurn)
            {                
                coachCommandImages[1].gameObject.SetActive(true);
                coachCommandImages[1].sprite = coach2Commands[2];
            }

            //StartCoroutine("HitMessgaeShowDelay");


            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
                tiles[i].GetComponentsInChildren<Image>()[1].enabled = false;
            }

            if (GameEngine.Instance.player1.IsMyTurn)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    int x = (i + 1) / 28;
                    int y = (i + 1) % 28;

                    if (x >= 4 && x <= 9)
                    {
                        if (y >= 17 && y <= 23)
                        {
                            tiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                        }
                    }
                }
            }
            else if (GameEngine.Instance.player2.IsMyTurn)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    int x = (i + 1) / 28;
                    int y = (i + 1) % 28;

                    if (x >= 4 && x <= 9)
                    {
                        if (y >= 6 && y <= 12)
                        {
                            tiles[i].GetComponentsInChildren<Image>()[0].enabled = true;
                        }
                    }
                }                            
            }
        }
    }

    public void MessgaeShowDelay()
    {        
        coachCommandImages[0].gameObject.SetActive(false);
        coachCommandImages[1].gameObject.SetActive(false);
    }

    IEnumerator ReceiveMessgaeShowDelay()
    {
        yield return new WaitForSeconds(3);

        coachCommandImages[0].gameObject.SetActive(false);
        coachCommandImages[1].gameObject.SetActive(false);
    }

    IEnumerator AimShotMessgaeShowDelay()
    {
        yield return new WaitForSeconds(3);

        coachCommandImages[0].gameObject.SetActive(false);
        coachCommandImages[1].gameObject.SetActive(false);
    }

    IEnumerator RollMessgaeShowDelay()
    {
        yield return new WaitForSeconds(3);
        if (isTopSpin)
        {
            rollNumber = UnityEngine.Random.RandomRange(1, 10);
        }
        else
        {
            rollNumber = UnityEngine.Random.RandomRange(0, 10);
        }        
        
        isHideMessgae = true;
        if (player1.IsMyTurn)
        {
            rollBall[0].GetComponent<Animator>().enabled = false;

            if (rollNumber == 0)
            {
                netGameObject[0].SetActive(true);
            }
            else
            {
                rollText[0].GetComponent<Text>().text = "" + rollNumber;
                rollText[0].SetActive(true);
                
            }

        }
        else if (player2.IsMyTurn)
        {
            rollBall[1].GetComponent<Animator>().enabled = false;

            if (rollNumber == 0)
            {
                netGameObject[1].SetActive(true);
            }
            else
            {
                rollText[1].SetActive(true);
                rollText[1].GetComponent<Text>().text = "" + rollNumber;
                
            }
        }
       // StartCoroutine("ShowRollNumber");
    }

    IEnumerator NetMessgaeShowDelay()
    {
        yield return new WaitForSeconds(3);

        coachCommandImages[0].gameObject.SetActive(false);
        coachCommandImages[1].gameObject.SetActive(false);
    }

    IEnumerator RecoverMessgaeShowDelay()
    {
        yield return new WaitForSeconds(3);

        coachCommandImages[0].gameObject.SetActive(false);
        coachCommandImages[1].gameObject.SetActive(false);
    }

    IEnumerator MoveMessgaeShowDelay()
    {
        yield return new WaitForSeconds(3);

        coachCommandImages[0].gameObject.SetActive(false);
        coachCommandImages[1].gameObject.SetActive(false);
    }

    IEnumerator HitMessgaeShowDelay()
    {
        yield return new WaitForSeconds(3);

        coachCommandImages[0].gameObject.SetActive(false);
        coachCommandImages[1].gameObject.SetActive(false);
    }

    IEnumerator ShowGamePlayMessage()
    {
        yield return new WaitForSeconds(3);

        if (OutOfRange(numberTexts[rollNumber - 1]))
        {
            gameplayInfo.IsStart = true;
            isNoRepeat = true;
            notifications[6].SetActive(false);
        }        
    }

    public void ShowRollNumber()
    {        
        rollText[0].SetActive(false);
        rollText[1].SetActive(false);
        rollBall[0].SetActive(false);
        rollBall[1].SetActive(false);
        netGameObject[0].SetActive(false);
        netGameObject[1].SetActive(false);
    }

    public void Player2TacticCardClick()
    {
        gameplayInfo.IsServe = true;
        isNoRepeat = true;
    }

    public void ShowNumbers(GameObject tileObject)
    {       
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] == tileObject)
            {
                columnNumber = (i + 1) / 28;
                rowNumber = (i + 1) % 28;
            }
        }

        for (int i = 0; i < 9; i++)
        {
            numberTexts[i].SetActive(true);
            numberTexts[i].transform.position = tiles[(columnNumber - 1 + (i / 3)) * 28 + rowNumber - 2 + (i % 3)].transform.position;


            if (player2.IsMyTurn)
            {
                player2whereToserveTiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
            }
            else if (player1.IsMyTurn)
            {
                player1whereToServeTiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
            }
        }             
    }

    public void PlaceTennisBall(int index)
    {
        for (int i = 0; i < 9; i++)
        {
            numberTexts[i].SetActive(false);
        }
        
        tennisBall[1].SetActive(true);
        tennisBall[1].transform.position = tiles[(columnNumber - 1 + ((index - 1) / 3)) * 28 + rowNumber - 2 + ((index - 1) % 3)].transform.position;
        

        if (player1.IsMyTurn)
        {
            ExtraDistance(playerPans[0],tennisBall[1]);

            tennisBall[0].SetActive(true);

            if (y2 + step > 28)
            {
                if (x1 > x2)
                {
                    tennisBall[0].transform.position = tiles[(columnNumber - 1 + ((index - 1) / 3) - 1) * 28 + 27].transform.position;
                }
                else if (x2 == x1)
                {
                    tennisBall[0].transform.position = tiles[(columnNumber - 1 + ((index - 1) / 3)) * 28 + 27].transform.position;
                }
                else
                {
                    tennisBall[0].transform.position = tiles[(columnNumber - 1 + ((index - 1) / 3) + 1) * 28 + 27].transform.position;
                }
                //print("player1" + "zero");
                //print(step);

            }
            else
            {
                //print("player1" + "up");
                //print(step);
                if (x1 > x2)
                {
                    tennisBall[0].transform.position = tiles[(columnNumber - 1 + ((index - 1) / 3) - 1) * 28 + rowNumber - 2 + ((index - 1) % 3) + step].transform.position;
                }
                else if (x2 == x1)
                {
                    tennisBall[0].transform.position = tiles[(columnNumber - 1 + ((index - 1) / 3)) * 28 + 27].transform.position;
                }
                else
                {
                    tennisBall[0].transform.position = tiles[(columnNumber - 1 + ((index - 1) / 3) + 1) * 28 + rowNumber - 2 + ((index - 1) % 3) + step].transform.position;
                }
                
            }            
        }
        else if (player2.IsMyTurn)
        {
            ExtraDistance(playerPans[1], tennisBall[1]);

            tennisBall[0].SetActive(true);

            if (y2 - step < 0)
            {
                if (x1 > x2)
                {
                    tennisBall[0].transform.position = tiles[(columnNumber - 1 + ((index - 1) / 3) - 1) * 28 + 1].transform.position;
                }
                else if (x2 == x1)
                {
                    tennisBall[0].transform.position = tiles[(columnNumber - 1 + ((index - 1) / 3)) * 28 + 1].transform.position;
                }
                else
                {
                    tennisBall[0].transform.position = tiles[(columnNumber - 1 + ((index - 1) / 3) + 1) * 28 + 1].transform.position;
                }
                //print("player2" + "zero");
                //print(step);

            }
            else
            {
                if (x1 > x2)
                {
                    tennisBall[0].transform.position = tiles[(columnNumber - 1 + ((index - 1) / 3) - 1) * 28 + rowNumber - 2 + ((index - 1) % 3) - step].transform.position;
                }
                else if (x2 == x1)
                {
                    tennisBall[0].transform.position = tiles[(columnNumber - 1 + ((index - 1) / 3)) * 28 + rowNumber - 2 + ((index - 1) % 3) - step].transform.position;
                }
                else
                {
                    tennisBall[0].transform.position = tiles[(columnNumber - 1 + ((index - 1) / 3) + 1) * 28 + rowNumber - 2 + ((index - 1) % 3) - step].transform.position;
                }
                //print("player2" + "up");
                //print(step);

            }            
        }
    }

    public void ExtraDistance(GameObject startObject, GameObject endObject)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].transform.position == endObject.transform.position)
            {
                x2 = (i + 1) / 28;
                y2 = (i + 1) % 28;

                if ((y2 >= 1 && y2 <= 5) || (y2 >= 24 && y2 <= 28))
                {
                    step = 6;                    
                }
                else if ((y2 >= 6 && y2 <= 8) || (y2 >= 21 && y2 <= 23))
                {
                    step = 5;
                }
                else if ((y2 >= 9 && y2 <= 11) || (y2 >= 18 && y2 <= 20))
                {
                    step = 4;
                }
                else if ((y2 >= 12 && y2 <= 17))
                {
                    step = 3;
                }

                if (isTopSpin)
                {
                    step++;
                }
                else if (isSliceShot)
                {
                    step--;
                }
                else if (isDropShot)
                {
                    step = 1;
                }
            }

            if (tiles[i].transform.position == startObject.transform.position)
            {
                x1 = (i + 1) / 28;
                y1 = (i + 1) % 28;
            }
        }
    }

    public void ShowMoveRange(GameObject originObject)
    {        
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].transform.position == originObject.transform.position)
            {
                x = (i + 1) / 28;
                y = (i + 1) % 28;

                if (player1.IsMyTurn)
                {
                    if ((y >= 1 && y <= 5) || (y >= 24 && y <= 28))
                    {
                        maxMove = 5;
                    }
                    else if ((y >= 6 && y <= 8) || (y >= 21 && y <= 23))
                    {
                        maxMove = 4;
                    }
                    else if ((y >= 9 && y <= 11) || (y >= 18 && y <= 20))
                    {
                        maxMove = 3;
                    }
                    else if ((y >= 12 && y <= 17))
                    {
                        maxMove = 2;
                    }

                    maxMove = maxMove + player1.BonusMove;
                }
                else
                {
                    if ((y >= 1 && y <= 5) || (y >= 24 && y <= 28))
                    {
                        maxMove = 5;
                    }
                    else if ((y >= 6 && y <= 8) || (y >= 21 && y <= 23))
                    {
                        maxMove = 4;
                    }
                    else if ((y >= 9 && y <= 11) || (y >= 18 && y <= 20))
                    {
                        maxMove = 3;
                    }
                    else if ((y >= 12 && y <= 17))
                    {
                        maxMove = 2;
                    }

                    maxMove = maxMove + player2.BonusMove;
                }
                          
            }
        }

        for (int i = x - maxMove; i <= x + maxMove; i++)
        {
            if (i >= 0 && i < 14)
            {
                for(int k = 0; k <= maxMove ;k++)
                {
                    if (x - i == maxMove - k || i - x == maxMove - k)
                    {
                        for (int j = 0; j < 2 * k + 1; j++)
                        {
                            if (y - k + j > 0 && y - k + j < 29)
                            {
                                tiles[i * 28 + y - 1 - k + j].GetComponentsInChildren<Image>()[0].enabled = true;
                            }
                        }                        
                    }

                }
                //if (i == x - 5 || i == x + 5)
                //{
                //    if (y > 0 && y < 29)
                //    {                        
                //        tiles[i * 28 + y - 1].GetComponentsInChildren<Image>()[0].enabled = true;
                //    }
                //}

                //if (i == x - 4 || i == x + 4)
                //{
                //    for (int j = 0; j < 3; j++)
                //    {
                //        if (y - 1 + j > 0 && y - 1 + j< 29)
                //        {
                //            tiles[i * 28 + y - 2 + j].GetComponentsInChildren<Image>()[0].enabled = true;
                //        }                       
                //    }                    
                //}

                //if (i == x - 3 || i == x + 3)
                //{
                //    for (int j = 0; j < 5; j++)
                //    {
                //        if (y - 2 + j > 0 && y - 2 + j < 29)
                //        {
                //            tiles[i * 28 + y - 3 + j].GetComponentsInChildren<Image>()[0].enabled = true;
                //        }                        
                //    }                    
                //}

                //if (i == x - 2 || i == x + 2)
                //{
                //    for (int j = 0; j < 7; j++)
                //    {
                //        if (y - 3 + j > 0 && y - 3 + j < 29)
                //        {
                //            tiles[i * 28 + y - 4 + j].GetComponentsInChildren<Image>()[0].enabled = true;
                //        }                        
                //    }                  
                //}

                //if (i == x - 1 || i == x + 1)
                //{
                //    for (int j = 0; j < 9; j++)
                //    {
                //        if (y - 4 + j > 0 && y + j - 4 < 29)
                //        {
                //            tiles[i * 28 + y - 5 + j].GetComponentsInChildren<Image>()[0].enabled = true;
                //        }
                //    }
                //}

                //if (i == x)
                //{
                //    for (int j = 0; j < 11; j++)
                //    {
                //        if (y - 5 + j > 0 && y - 5 + j < 29)
                //        {
                //            tiles[i * 28 + y - 6 + j].GetComponentsInChildren<Image>()[0].enabled = true;
                //        }                       
                //    }                    
                //}
            }            
        }
    }

    public void DrawLine(GameObject line, GameObject startObject, GameObject endObject)
    {
        if (line == lines[0])
        {
            //print(Mathf.Atan((endObject.transform.position.y - startObject.transform.position.y) / (endObject.transform.position.x - startObject.transform.position.x)) * 180f / Mathf.PI);
        }

        line.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
        line.transform.position = new Vector2((endObject.transform.position.x + startObject.transform.position.x) / 2, (endObject.transform.position.y + startObject.transform.position.y) / 2);
        line.GetComponent<RectTransform>().sizeDelta = new Vector2(Vector2.Distance(startObject.transform.position, endObject.transform.position), line.GetComponent<RectTransform>().sizeDelta.y);        
        //line.GetComponent<RectTransform>().Rotate(0, 0, Mathf.Atan((endObject.transform.position.y - startObject.transform.position.y) / (endObject.transform.position.x - startObject.transform.position.x)) * 180f / Mathf.PI);
        line.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Mathf.Atan((endObject.transform.position.y - startObject.transform.position.y) / (endObject.transform.position.x - startObject.transform.position.x)) * 180f / Mathf.PI);
        line.GetComponent<BoxCollider2D>().size = new Vector2(Vector2.Distance(startObject.transform.position, endObject.transform.position), line.GetComponent<RectTransform>().sizeDelta.y);
    }


    public void ShowMoveToStrike(GameObject tileObject)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].transform.position == tileObject.transform.position)
            {
                x = (i + 1) / 28;
                y = (i + 1) % 28;
            }
        }

        //for (int i = x - 1; i <= x + 1; i++)
        //{
        //    for (int j = y - 1; j <= y + 1; j++)
        //    {
        //        if ((i == x - 1 || i == x + 1) && j == y)
        //        {
        //            if (tiles[(i) * 28 + j - 1].GetComponentsInChildren<Image>()[0].enabled)
        //            {
        //                tiles[(i) * 28 + j - 1].GetComponentsInChildren<Image>()[0].enabled = false;
        //                tiles[(i) * 28 + j - 1].GetComponentsInChildren<Image>()[1].enabled = true;
        //                isNoMove = true;
        //            }
        //        }
        //        else if (i == x)
        //        {
        //            if (tiles[(i) * 28 + j - 1].GetComponentsInChildren<Image>()[0].enabled)
        //            {
        //                tiles[(i) * 28 + j - 1].GetComponentsInChildren<Image>()[0].enabled = false;
        //                tiles[(i) * 28 + j - 1].GetComponentsInChildren<Image>()[1].enabled = true;
        //                isNoMove = true;
        //            }
        //        }

        //    }
        //}    

        if (isLobShot)
        {           
            for (int i = 0; i < tiles.Length; i++)
            {
                if (lines[0].GetComponent<BoxCollider2D>().bounds.Intersects(tiles[i].GetComponent<BoxCollider2D>().bounds))
                {
                    //print(tiles[i].gameObject.name);
                    if (tiles[i].GetComponentsInChildren<Image>()[0].enabled)
                    {
                        tiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
                        tiles[i].GetComponentsInChildren<Image>()[1].enabled = true;
                        isNoMove = true;
                    }
                }
            }
        }
        else
        {
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (tiles[(i) * 28 + j - 1].GetComponentsInChildren<Image>()[0].enabled)
                    {
                        tiles[(i) * 28 + j - 1].GetComponentsInChildren<Image>()[0].enabled = false;
                        tiles[(i) * 28 + j - 1].GetComponentsInChildren<Image>()[1].enabled = true;
                        isNoMove = true;
                    }
                }
            }

            for (int i = 0; i < tiles.Length; i++)
            {
                if (lines[0].GetComponent<BoxCollider2D>().bounds.Intersects(tiles[i].GetComponent<BoxCollider2D>().bounds))
                {
                    //print(tiles[i].gameObject.name);
                    if (tiles[i].GetComponentsInChildren<Image>()[0].enabled)
                    {
                        tiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
                        tiles[i].GetComponentsInChildren<Image>()[1].enabled = true;
                        isNoMove = true;
                    }
                }
            }

            for (int i = 0; i < tiles.Length; i++)
            {
                if (lines[1].GetComponent<BoxCollider2D>().bounds.Intersects(tiles[i].GetComponent<BoxCollider2D>().bounds))
                {
                    //print(tiles[i].gameObject.name);
                    if (tiles[i].GetComponentsInChildren<Image>()[0].enabled)
                    {
                        tiles[i].GetComponentsInChildren<Image>()[0].enabled = false;
                        tiles[i].GetComponentsInChildren<Image>()[1].enabled = true;
                        isNoMove = true;
                    }
                }
            }
        }
        
    }

    public void IsMoveAvaliable(GameObject tileObject)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].transform.position == tileObject.transform.position)
            {
                x = (i + 1) / 28;
                y = (i + 1) % 28;
            }
        }

        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (tiles[(i) * 28 + j - 1].GetComponentsInChildren<Image>()[0].enabled)
                {                    
                    isNoMove = true;
                }
            }
        }

        for (int i = 0; i < tiles.Length; i++)
        {
            if (lines[1].GetComponent<BoxCollider2D>().bounds.Intersects(tiles[i].GetComponent<BoxCollider2D>().bounds))
            {
                if (tiles[i].GetComponentsInChildren<Image>()[0].enabled)
                {                    
                    isNoMove = true;
                }
            }
        }

    }

    public void GetScore( PlayerInfo winner)
    {
        if (winner == player1)
        {
            switch (player1.Score)
            {
                case (int)Score.First:
                    {
                        player1.Score = (int)Score.Second;
                        break;
                    }
                case (int)Score.Second:
                    {
                        player1.Score = (int)Score.Third;
                        break;
                    }
                case (int)Score.Third:
                    {
                        player1.Score = (int)Score.Forth;

                        if (player2.Score == (int)Score.Forth)
                        {
                            gameplayInfo.IsQus = true;
                        }

                        break;
                    }
                case (int)Score.Forth:
                    {
                        if (gameplayInfo.IsQus && !player1.IsQusUp)
                        {
                            player1.IsQusUp = true;
                            player2.IsQusUp = false;
                        }
                        else
                        {
                            gameplayInfo.IsQus = false;
                            player1.Score = (int)Score.First;
                            player2.Score = (int)Score.First;
                            player1.Level++;

                            if (player1.IsMyServe)
                            {
                                player1.IsMyServe = false;
                                player2.IsMyServe = true;
                                player1.IsMyTurn = false;
                                player2.IsMyTurn = true;
                                serveNotifications[0].GetComponent<Image>().enabled = true;
                                serveNotifications[1].GetComponent<Image>().enabled = false;
                            }
                            else if (player2.IsMyServe)
                            {
                                player1.IsMyServe = true;
                                player2.IsMyServe = false;
                                player1.IsMyTurn = true;
                                player2.IsMyTurn = false;
                                serveNotifications[0].GetComponent<Image>().enabled = false;
                                serveNotifications[1].GetComponent<Image>().enabled = true;
                            }

                            //StartCoroutine("ShowMessage");
                        }
                        break;
                    }
            }
        }
        else if (winner == player2)
        {
            switch (player2.Score)
            {
                case (int)Score.First:
                    {
                        player2.Score = (int)Score.Second;
                        break;
                    }
                case (int)Score.Second:
                    {
                        player2.Score = (int)Score.Third;
                        break;
                    }
                case (int)Score.Third:
                    {
                        player2.Score = (int)Score.Forth;

                        if (player1.Score == (int)Score.Forth)
                        {
                            gameplayInfo.IsQus = true;
                        }

                        break;
                    }
                case (int)Score.Forth:
                    {
                        if (gameplayInfo.IsQus && !player2.IsQusUp)
                        {
                            player2.IsQusUp = true;
                            player1.IsQusUp = false;
                        }
                        else
                        {
                            gameplayInfo.IsQus = false;
                            player1.Score = (int)Score.First;
                            player2.Score = (int)Score.First;
                            player2.Level++;

                            if (player1.IsMyServe)
                            {
                                player1.IsMyServe = false;
                                player2.IsMyServe = true;
                                player1.IsMyTurn = false;
                                player2.IsMyTurn = true;
                                serveNotifications[1].GetComponent<Image>().enabled = true;
                                serveNotifications[0].GetComponent<Image>().enabled = false;
                            }
                            else if (player2.IsMyServe)
                            {
                                player1.IsMyServe = true;
                                player2.IsMyServe = false;
                                player1.IsMyTurn = true;
                                player2.IsMyTurn = false;
                                serveNotifications[1].GetComponent<Image>().enabled = false;
                                serveNotifications[0].GetComponent<Image>().enabled = true;
                            }
                        }
                        break;
                    }
            }
        }

        player1LevelText.text = "" + player1.Level;
        player1ScoreText.text = "" + player1.Score;
        player2LevelText.text = "" + player2.Level;
        player2ScoreText.text = "" + player2.Score;

        if (player1.IsMyServe)
        {            
            player1.IsMyTurn = true;
            player2.IsMyTurn = false;
            serveNotifications[1].GetComponent<Image>().enabled = false;
            serveNotifications[0].GetComponent<Image>().enabled = true;
        }
        else if (player2.IsMyServe)
        {            
            player1.IsMyTurn = false;
            player2.IsMyTurn = true;
            serveNotifications[1].GetComponent<Image>().enabled = true;
            serveNotifications[0].GetComponent<Image>().enabled = false;
        }

        StartCoroutine("DelayStart");
    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(2);

        GameEngine.Instance.notifications[12].SetActive(false);
        GameEngine.Instance.coachCommandImages[0].gameObject.SetActive(false);
        GameEngine.Instance.coachCommandImages[1].gameObject.SetActive(false);
        gameplayInfo.IsStart = true;
        isNoRepeat = true;
    }

    public bool OutOfRangeServe(GameObject originObject)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].transform.position == originObject.transform.position)
            {
                x = (i + 1) / 28;
                y = (i + 1) % 28;
            }
        }

        if (player1.IsMyServe)
        {
            if (((int)player1.Score + (int)player2.Score) % 2 == 0)
            {
                if (x >= 4 && x <= 6 && y >= 15 && y <= 19)
                {
                    //print(false);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (x >= 7 && x <= 9 && y >= 15 && y <= 19)
                {
                    //print(false);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            
        }
        else
        {
            if (((int)player1.Score + (int)player2.Score) % 2 == 0)
            {
                if (x >= 7 && x <= 9 && y >= 10 && y <= 14)
                {

                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (x >= 4 && x <= 6 && y >= 10 && y <= 14)
                {

                    return false;
                }
                else
                {
                    return true;
                }
            }
           
        }
    }

    public bool OutOfRange(GameObject originObject)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].transform.position == originObject.transform.position)
            {
                x = (i + 1) / 28;
                y = (i + 1) % 28;
            }
        }

        if (x >= 4 && x <= 9 && y >= 6 && y <= 23)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void TopSpinClick()
    {
        FormatBoolFlag();        
        isTopSpin = true;

        if (player1.IsMyTurn)
        {           
            player1.StrokeCardCount++;
        }
        else
        {            
            player2.StrokeCardCount++;
        }

    }

    public void SliceShotClick()
    {
        FormatBoolFlag();

        if (player1.IsMyTurn)
        {
            player1.BonusMove = 1;
            player1.StrokeCardCount++;
        }
        else
        {
            player2.BonusMove = 1;
            player2.StrokeCardCount++;
        }

        isSliceShot = true;        
    }

    public void PoweShotClick()
    {
        FormatBoolFlag();

        if (player1.IsMyTurn)
        {
            player1.BonusMove = -1;
            player2.BonusMove = -1;
            player1.StrokeCardCount++;
        }
        else
        {
            player1.BonusMove = -1;
            player2.BonusMove = -1;
            player2.StrokeCardCount++;
        }

        isPowerShot = true;       
    }

    public void LobShotClick()
    {
        FormatBoolFlag();

        if (player1.IsMyTurn)
        {
            player2.BonusMove = 3;
            player1.StrokeCardCount++;
        }
        else
        {
            player1.BonusMove = 3;
            player2.StrokeCardCount++;
        }

        isLobShot = true;       
    }

    public void DropShotClick()
    {
        FormatBoolFlag();

        if (player1.IsMyTurn)
        {
            player2.BonusMove = 3;
            player1.StrokeCardCount++;
        }
        else
        {
            player1.BonusMove = 3;
            player2.StrokeCardCount++;
        }

        isDropShot = true;        
    }

    public void SpecialStrokeClick()
    {
        if (player1.IsMyTurn && player1.StrokeCardCount < 2)
        {
            strokeCardButton.SetActive(false);
            strokeCards.SetActive(true);
        }
        else if (player2.IsMyTurn && player2.StrokeCardCount < 2)
        {
            strokeCardButton.SetActive(false);
            strokeCards.SetActive(true);
        }       
    }
    
}

