using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 4;
    public const int gridCols = 10;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;

    [SerializeField] private MemoryCard originalCard;
    [SerializeField] private Sprite[] images;
    [SerializeField] private TextMesh scoreLabel;

    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;
    private int _score = 0;

    public bool CanReveal
    {
        get { return _secondRevealed == null; }
    }

    public void CardRevealed(MemoryCard card)
    {
        if (_firstRevealed == null)
            _firstRevealed = card;
        else
            _secondRevealed = card;
        StartCoroutine(CheckMatch());
    }

    private IEnumerator CheckMatch()
    {
        if (_firstRevealed.id == _secondRevealed.id)
        {
            yield return new WaitForSeconds(.5f);
            scoreLabel.text = "Score: " + ++_score;
            _firstRevealed.gameObject.SetActive(false);
            _secondRevealed.gameObject.SetActive(false);
        }
        else
            yield return new WaitForSeconds(.5f);
        _firstRevealed.Unrevealed();
        _secondRevealed.Unrevealed();

        _firstRevealed = null;
        _secondRevealed = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPos = originalCard.transform.position;
        int[] numbers = new int[40];
        for (int k=0; k<numbers.Length-1;k=k+2)
        {
            if (k == 0)
                numbers[k] = numbers[k + 1] = k;
            else
            {
                int temp = k / 2;
                numbers[k] = numbers[k + 1] = temp;
            }
        }

        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MemoryCard card;
                if (i == 0 && j == 0)
                    card = originalCard;
                else
                    card = Instantiate(originalCard) as MemoryCard;

                int index = j * gridCols + i;

                int id = numbers[index];
                card.SetCard(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i=0; i<newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
