using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public TextInput Text;
    public ActionsInput Actions;
    public PlayerPreviewManager Display;

    public Scene GameboardScene;
    public Scene WheelScene;

    public Wheel GameWheel;
    public GameBoard Board;

    public Player playerPrefab;

    public static int VowelCost = 250;

    public Player currentPlayer {  get; private set; }
    public char letterPressed { get; private set; }

    private PlayManagerState m_currentState;
    private Queue<PlayManagerEvent> m_eventBuffer = new Queue<PlayManagerEvent>();

    WheelSegment.SegmentData m_spunSegment;

    public Profile[] profiles;
    public TextAsset[] loadedPacks;

    public List<Player> players = new List<Player>();
 
    int playerTurn = 0;

    void Start()
    {
        Actions.OnButtonPressed.AddListener(HandleEvent);
        GameWheel.OnSpinComplete.AddListener(OnSpinComplete);
        Text.OnButtonPressed.AddListener(OnKeyboardPressed);

        Board.InitializeBoard();
        SetRandomSolution();

        foreach(Profile profile in profiles) //TODO: Debug
        {
            Player player = Instantiate(playerPrefab, transform);
            player.gameObject.transform.name = profile.Name;
            player.profile = profile;
            Display.Init(player);
            players.Add(player);
        }

        Display.setActivePlayer(playerTurn);
        currentPlayer = players[playerTurn];

        m_currentState = new PMIntro(this);
        m_currentState.Entry();

    }

    // Update is called once per frame
    void Update()
    {
        if (m_eventBuffer.Count > 0)
        {
            var newState = m_currentState.HandleEvent(m_eventBuffer.Dequeue());

            if (newState != m_currentState)
            {
                m_currentState.Exit();
                m_currentState = newState;
                newState.Entry();
            }
        }

        m_currentState.Do();
    }

    public void SetRandomSolution()
    {
        var package = JsonImporter.JsonToQuestion(loadedPacks[Random.Range(0, loadedPacks.Length)].text);
        string solution = package.questions[Random.Range(0, package.questions.Length)];
        Board.SetSolution(package.category, solution);
    }

    public void HandleEvent(PlayManagerEvent PMevent)
    {
        m_eventBuffer.Enqueue(PMevent);
    }

    private void OnSpinComplete(WheelSegment segment)
    {
        m_spunSegment = segment.GetSegmentData();
        switch (m_spunSegment.type)
        {
            case WheelSegment.SegmentType.bankrupt:
                players[playerTurn].roundCash.ResetValue();
                HandleEvent(PlayManagerEvent.SpinFucked);
                break;
            case WheelSegment.SegmentType.loseTurn:
                HandleEvent(PlayManagerEvent.SpinFucked);
                break;
            default:
                HandleEvent(PlayManagerEvent.SpinDone);
                break;
        }
    }

    private void OnKeyboardPressed(char input)
    {
        letterPressed = input;
        HandleEvent(PlayManagerEvent.ChooseLetterDone);
    }

    public void IncrementTurn()
    {
        playerTurn++;
        if (playerTurn >= players.Count) 
        {
            playerTurn = 0;
        }
        currentPlayer = players[playerTurn];
        Display.setActivePlayer(playerTurn);
    }    
}
