using UnityEngine;


public class gameController : MonoBehaviour 
{
	public Camera currentCamera;
	public Game game;
	public BoardView boardView;
	//public PieceView nextPieceView;
	public ScoreView scoreView;
	public LevelView levelView;
	public AlertView alertView;
	public SettingsView settingsView;
	public AudioPlayer audioPlayer;
	public GameObject screenButtons;
	public AudioSource musicAudioSource;

	public float speed = 10f;
	public float rotationSpeed = 100f;
	public float jumpForce = 5f;
	public float groundRaycastDistance = 1f;

	private Rigidbody rb;
	private bool isGrounded;

	private UniversalInput universalInput;

	public int pointsPerCollectible = 100;
	public int pointsPerTrick = 50;
	private int totalScore;

	private void Awake()
	{
		HandlePlayerSettings();
		Settings.ChangedEvent += HandlePlayerSettings;
	}

	void Start()
	{
		Board board = new Board(10, 20);

		boardView.SetBoard(board);
		nextPieceView.SetBoard(board);

		universalInput = new UniversalInput(new KeyboardInput(), boardView.touchInput);

		game = new Game(board, universalInput);
		game.FinishedEvent += OnGameFinished;
		game.PieceFinishedFallingEvent += audioPlayer.PlayPieceDropClip;
		game.PieceRotatedEvent += audioPlayer.PlayPieceRotateClip;
		game.PieceMovedEvent += audioPlayer.PlayPieceMoveClip;
		game.Start();

		scoreView.game = game;
		levelView.game = game;
	}

	public void OnPauseButtonTap()
	{
		game.Pause();
		ShowPauseView();
	}

	public void OnMoveLeftButtonTap()
	{
		game.SetNextAction(PlayerAction.MoveLeft);
	}

	public void OnMoveRightButtonTap()
	{
		game.SetNextAction(PlayerAction.MoveRight);
	}

	public void OnMoveDownButtonTap()
	{
		game.SetNextAction(PlayerAction.MoveDown);
	}

	public void OnFallButtonTap()
	{
		game.SetNextAction(PlayerAction.Fall);
	}

	public void OnRotateButtonTap()
	{
		game.SetNextAction(PlayerAction.Rotate);
	}

	void OnGameFinished()
	{
		alertView.SetTitle(Constant.Text.GameFinished);
		alertView.AddButton(Constant.Text.PlayAgain, game.Start, audioPlayer.PlayNewGameClip);
		alertView.Show();
	}

	void Update()
	{
		game.Update(Time.deltaTime);
	}

	void ShowPauseView()
	{
		alertView.SetTitle(Constant.Text.GamePaused);
		alertView.AddButton(Constant.Text.Resume, game.Resume, audioPlayer.PlayResumeClip);
		alertView.AddButton(Constant.Text.NewGame, game.Start, audioPlayer.PlayNewGameClip);
		alertView.AddButton(Constant.Text.Settings, ShowSettingsView, audioPlayer.PlayResumeClip);
		alertView.Show();
	}

	void ShowSettingsView()
	{
		settingsView.Show(ShowPauseView);
	}

	void HandlePlayerSettings()
	{
		screenButtons.SetActive(Settings.ScreenButonsEnabled);
		boardView.touchInput.Enabled = !Settings.ScreenButonsEnabled;
		musicAudioSource.gameObject.SetActive(Settings.MusicEnabled);
	}
	

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}
	private void Update()
	{
		// Handle player input 
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		// Calculate movement direction
		Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
		movement = movement.normalized * speed * Time.deltaTime;

		// Apply movement to the rigidbody 
		rb.AddForce(movement);

		// Rotate the player based on horizontal input 
		float rotation = moveHorizontal * rotationSpeed * Time.deltaTime;
		Quaternion deltaRotation = Quaternion.Euler(new Vector3(0f, rotation, 0f));
		rb.MoveRotation(rb.rotation * deltaRotation);

		// Handle jump input 
		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		}

	}

	private void FixedUpdate()
	{

		// Check if the player is grounded 
		isGrounded = CheckGrounded();
	}

	private bool CheckGrounded()
	{
		RaycastHit hit;
		Vector3 raycastOrigin = transform.position + Vector3.up * 0.1f;  // Offset slightly above the ground 

		// Perform a raycast downwards to check if the player is touching the ground 
		if (Physics.Raycast(raycastOrigin, Vector3.down, out hit, groundRaycastDistance))
		{
			// Adjust the ground detection based on your snowboarding terrain 
			return true;
		}
		return false;
	}
 

	private void Start() 
	{
		totalScore = 0; 
	}
	private void OnTriggerEnter(Collider other)
	{
		// Check if the player collects a collectible object 
		if (other.CompareTag("Collectible")) 
		{
			Collectible collectible = other.GetComponent<Collectible>(); 
			if (collectible != null && !collectible.IsCollected) 
		{
				collectible.Collect(); 
				totalScore += pointsPerCollectible;
				Debug.Log("Collectible collected! Current score: " + totalScore);
			}
		}
	}
	public void PerformTrick() 
	{ 
		totalScore += pointsPerTrick; 
		Debug.Log("Trick performed! Current score: " + totalScore);
	}
	public void CompleteObjective(int objectivePoints)
	{
		totalScore += objectivePoints; 
		Debug.Log("Objective completed! Current score: " + totalScore); 
	}
 }

