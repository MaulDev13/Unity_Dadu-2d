using System.Collections;
using UnityEngine;
using TMPro;

public class NormalDice : MonoBehaviour
{
    [SerializeField] private DropdownHandler dropdown;

    [SerializeField] private Sprite[] diceSides;
    [SerializeField] private SpriteRenderer[] rend;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI totalFinalValue;
    [SerializeField] private TextMeshProUGUI infoTxt;

    [SerializeField] private int repeatTheRollAnim = 20;
    private int maxDice = 6; // Max dice on game
    private int currentDice = 6; // Current dice on game

    private int[] randomDiceSide; // Dice number on every dice
    private int[] finalDiceSide; // Value on every dice

    private bool isRolling = false;
    private bool isHaveSound = true;

    private void Start()
    {
        infoPanel.SetActive(false);

        randomDiceSide = new int[currentDice];
        finalDiceSide = new int[currentDice];
        for (int i = 0; i < currentDice; i++)
        {
            randomDiceSide[i] = 0;
            finalDiceSide[i] = randomDiceSide[i];
        }

        // Animation
        //ChangeCurrentDice(currentDice);

        dropdown.Init(currentDice);
    }

    // Change current dice value
    public void ChangeCurrentDice(int value)
    {
        if (isRolling)
            return;

        currentDice = value + 1;

        CancelInvoke();
        DisableTapAnywhere();

        // Make it empty
        for (int i = 0; i < maxDice; i++)
        {
            rend[i].gameObject.SetActive(false);
        }

        // Reanimate dice
        for (int i = 0; i < currentDice; i++)
        {
            rend[i].gameObject.SetActive(true);
        }

        OnBtnStart();

        Debug.Log($"Normal dce change value {value}");
    }

    private void TapAnywhere()
    {
        StartCoroutine(TapAnywhereAnim());
    }

    // Animation tap anywhere
    IEnumerator TapAnywhereAnim()
    {
        infoTxt.text = "Tap Anywhere...";

        yield return new WaitForSeconds(1.2f);

        infoTxt.text = "";
    }

    // Pause tap anywhere animation
    private void DisableTapAnywhere()
    {
        infoTxt.text = "";
    }

    // When player click refresh button. Start tap anywhere animation again
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

    // Roll the dice
    public void OnNormalRoll()
    {
        if (isRolling)
            return;

        for (int i = 0; i < maxDice; i++)
        {
            finalDiceSide[i] = 0;
        }

        for (int i = 0; i < currentDice; i++)
        {
            finalDiceSide[i] = Random.Range(0, 6);
        }

        StartCoroutine(RollTheDice());
    }

    // Dice change animation when not rolling
    private void DiceRoll()
    {
        for (int i = 0; i < currentDice; i++)
        {
            int newNumb = 0;

            // Make the number not the same for the last
            do
            {
                newNumb = Random.Range(0, 6);
            } while (randomDiceSide[i] == newNumb);

            randomDiceSide[i] = newNumb;

            // Change dice sprite
            rend[i].sprite = diceSides[randomDiceSide[i]];
        }
    }

    // Dice rolling animation
    private void OneDiceRoll(int i)
    {
        int newNumb = 0;

        // Make the number not the same for the last
        do
        {
            newNumb = Random.Range(0, 6);
        } while (randomDiceSide[i] == newNumb);

        randomDiceSide[i] = newNumb;

        // Change dice sprite
        rend[i].sprite = diceSides[randomDiceSide[i]];
    }

    IEnumerator RollTheDice()
    {
        isRolling = true;

        infoPanel.SetActive(false);

        CancelInvoke();
        DisableTapAnywhere();

        Sound();

        for (int i = 0; i < currentDice; i++)
        {
            for (int j = 0; j < repeatTheRollAnim; j++)
            {
                if (j == repeatTheRollAnim - 1)
                {
                    randomDiceSide[i] = finalDiceSide[i];

                    // Change dice sprite
                    rend[i].sprite = diceSides[randomDiceSide[i]];
                }
                else
                {
                    OneDiceRoll(i);
                }

                if (j > Mathf.RoundToInt(repeatTheRollAnim / 4) && i < currentDice - 1)
                {
                    OneDiceRoll(i + 1);

                    if (j > Mathf.RoundToInt(repeatTheRollAnim / 2) && i < currentDice - 2)
                    {
                        OneDiceRoll(i + 2);
                    }
                }

                yield return new WaitForSeconds(0.1f);
            }

            finalDiceSide[i] = randomDiceSide[i];
        }

        infoPanel.SetActive(true);

        var total = 0;
        for(int i = 0; i < currentDice; i++)
        {
            total += finalDiceSide[i] + 1;
        }

        totalFinalValue.text = total.ToString();

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
