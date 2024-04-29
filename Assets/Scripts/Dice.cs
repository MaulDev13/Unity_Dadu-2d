using System.Collections;
using UnityEngine;
using TMPro;

public class Dice : MonoBehaviour
{
    [SerializeField] private Sprite[] diceSides;
    [SerializeField] private SpriteRenderer[] rend;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI totalFinalValue;
    [SerializeField] private TextMeshProUGUI infoTxt;

    [SerializeField] private int repeatTheRollAnim = 20;
    private int maxDice = 3;

    private int[] randomDiceSide;
    private int[] finalDiceSide;

    private bool isRolling = false;
    private bool isHaveSound = true;

    private void Start()
    {
        infoPanel.SetActive(false);

        randomDiceSide = new int[maxDice];
        randomDiceSide[0] = 0;
        randomDiceSide[1] = 0;
        randomDiceSide[2] = 0;

        finalDiceSide = new int[maxDice];
        finalDiceSide[0] = randomDiceSide[0];
        finalDiceSide[1] = randomDiceSide[1];
        finalDiceSide[2] = randomDiceSide[2];

        InvokeRepeating("DiceRoll", 1f, 1f);
        InvokeRepeating("TapAnywhere", 1f, 2.4f);
    }

    private void Update()
    {
        /*
        if (isRolling)
            return;
            
        if(Input.GetMouseButton(0))
        {
            CancelInvoke();

            StartCoroutine(RollTheDice());
        }
        */
    }

    private void TapAnywhere()
    {
        StartCoroutine(TapAnywhereAnim());
    }

    IEnumerator TapAnywhereAnim()
    {
        infoTxt.text = "Tap Anywhere...";

        yield return new WaitForSeconds(1.2f);

        infoTxt.text = "";
    }

    private void DisableTapAnywhere()
    {
        infoTxt.text = "";
    }

    public void OnBtnStart()
    {
        if (isRolling)
            return;

        CancelInvoke();
        DisableTapAnywhere();

        InvokeRepeating("DiceRoll", 1f, 1f);
        InvokeRepeating("TapAnywhere", 1f, 2.4f);

        infoPanel.SetActive(false);
    }

    public void OnGanjilKecil() // final dice 3 - 10
    {
        if (isRolling)
            return;

        int[] newNumb = new int[maxDice];
        int total = 0;
        do
        {
            newNumb[0] = Random.Range(1, 7);
            newNumb[1] = Random.Range(1, 7);
            newNumb[2] = Random.Range(1, 7);

            total = newNumb[0] + newNumb[1] + newNumb[2];
        } while (total >= 11 || ((newNumb[0] == newNumb[1]) && (newNumb[1] == newNumb[2])) || total%2 == 0);

        finalDiceSide[0] = newNumb[0] - 1;
        finalDiceSide[1] = newNumb[1] - 1;
        finalDiceSide[2] = newNumb[2] - 1;

        StartCoroutine(RollTheDice());
    }

    public void OnGanjilBesar() // final dice 11 - 18
    {
        if (isRolling)
            return;

        int[] newNumb = new int[maxDice];
        int total = 0;
        do
        {
            newNumb[0] = Random.Range(1, 7);
            newNumb[1] = Random.Range(1, 7);
            newNumb[2] = Random.Range(1, 7);

            total = newNumb[0] + newNumb[1] + newNumb[2];
        } while (total <= 10 || ((newNumb[0] == newNumb[1]) && (newNumb[1] == newNumb[2])) || total % 2 == 0);

        finalDiceSide[0] = newNumb[0] - 1;
        finalDiceSide[1] = newNumb[1] - 1;
        finalDiceSide[2] = newNumb[2] - 1;

        StartCoroutine(RollTheDice());
    }

    public void OnGenapKecil() // final dice 3 - 10
    {
        if (isRolling)
            return;

        int[] newNumb = new int[maxDice];
        int total = 0;
        do
        {
            newNumb[0] = Random.Range(1, 7);
            newNumb[1] = Random.Range(1, 7);
            newNumb[2] = Random.Range(1, 7);

            total = newNumb[0] + newNumb[1] + newNumb[2];
        } while (total >= 11 || ((newNumb[0] == newNumb[1]) && (newNumb[1] == newNumb[2])) || total % 2 == 1);

        finalDiceSide[0] = newNumb[0] - 1;
        finalDiceSide[1] = newNumb[1] - 1;
        finalDiceSide[2] = newNumb[2] - 1;

        StartCoroutine(RollTheDice());
    }

    public void OnGenapBesar() // final dice 11 - 18
    {
        if (isRolling)
            return;

        int[] newNumb = new int[maxDice];
        int total = 0;
        do
        {
            newNumb[0] = Random.Range(1, 7);
            newNumb[1] = Random.Range(1, 7);
            newNumb[2] = Random.Range(1, 7);

            total = newNumb[0] + newNumb[1] + newNumb[2];
        } while (total <= 10 || ((newNumb[0] == newNumb[1]) && (newNumb[1] == newNumb[2])) || total % 2 == 1);

        finalDiceSide[0] = newNumb[0] - 1;
        finalDiceSide[1] = newNumb[1] - 1;
        finalDiceSide[2] = newNumb[2] - 1;

        StartCoroutine(RollTheDice());
    }

    public void OnKembar() // final dice is same
    {
        if (isRolling)
            return;

        int newNumb = Random.Range(0, 6);

        finalDiceSide[0] = finalDiceSide[1] = finalDiceSide[2] = newNumb;

        StartCoroutine(RollTheDice());
    }

    public void OnNormalRoll()
    {
        if (isRolling)
            return;

        finalDiceSide[0] = Random.Range(0, 6);
        finalDiceSide[1] = Random.Range(0, 6);
        finalDiceSide[2] = Random.Range(0, 6);

        StartCoroutine(RollTheDice());
    }

    private void DiceRoll()
    {
        for(int i = 0; i < maxDice; i++)
        {
            int newNumb = 0;

            do
            {
                newNumb = Random.Range(0, 6);
            } while (randomDiceSide[i] == newNumb);

            randomDiceSide[i] = newNumb;

            rend[i].sprite = diceSides[randomDiceSide[i]];
        }
    }

    private void OneDiceRoll(int i)
    {
        int newNumb = 0;

        do
        {
            newNumb = Random.Range(0, 6);
        } while (randomDiceSide[i] == newNumb);

        randomDiceSide[i] = newNumb;

        rend[i].sprite = diceSides[randomDiceSide[i]];
    }

    IEnumerator RollTheDice()
    {
        isRolling = true;

        infoPanel.SetActive(false);

        CancelInvoke();
        DisableTapAnywhere();

        Sound();

        for (int i = 0; i < maxDice; i++)
        {
            for (int j = 0; j < repeatTheRollAnim; j++)
            {
                //DiceRoll();

                if (j == repeatTheRollAnim - 1)
                {
                    randomDiceSide[i] = finalDiceSide[i];

                    rend[i].sprite = diceSides[randomDiceSide[i]];
                } else
                {
                    OneDiceRoll(i);
                }

                if (j > Mathf.RoundToInt(repeatTheRollAnim / 4) && i < maxDice - 1)
                {
                    OneDiceRoll(i+1);

                    if (j > Mathf.RoundToInt(repeatTheRollAnim / 2) && i < maxDice - 2)
                    {
                        OneDiceRoll(i + 2);
                    }
                }

                yield return new WaitForSeconds(0.1f);
            }

            finalDiceSide[i] = randomDiceSide[i];
        }

        infoPanel.SetActive(true);
        totalFinalValue.text = (finalDiceSide[0] + finalDiceSide[1] + finalDiceSide[2] + 3).ToString();

        yield return new WaitForSeconds(1f);

        isRolling = false;
    }

    private void Sound()
    {
        if (!isHaveSound)
            return;

        AudioManager.instance.Play("Roll", Vector2.zero);
    }

    public void OnBtnSound()
    {
        isHaveSound = !isHaveSound;
    }
}
