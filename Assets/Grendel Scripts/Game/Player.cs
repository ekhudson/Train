using UnityEngine;
using System.Collections;

public class Player : Entity
{

    public CharacterController ctrl;
    public Cannon pCannon;  
	public GameObject DamageParticles;
	public SearchRadius MySearchRadius;

    public float playerMoveSpeed = 1f;
    public float pJumpSpeed = 1f;
	public float pMaxJumpSpeed = 15f;
	public float pMinJumpSpeed = 5f;
	public float pCooldown = 1f;
	public float BulletCooldown = 0.01f;
	private float _currentCooldown = 1f;
	private float _currentBulletCooldown = 0.05f;   
    Vector3 pFallSpeed = Vector3.zero;	
	private Vector3 _tempRotation;
   	private Vector3 _move;
	//private bool _isPounding = false;	
	public enum PLAYERSTATES
	{
		RUNNING,
		JUMPING,
		FALLING,
		CINEMATIC,		
	}
	
	private PLAYERSTATES _state = PLAYERSTATES.RUNNING;
	
	private static Player instance;	
	
	public static Player Instance
	{
		get { return instance; }
	}
	
	// Use this for initialization
    protected override void Awake()
    {		
		base.Awake();
		instance = this;		
    }
	
	protected override void Start()
	{
		
		_rigidbody.WakeUp();
	}

    // Update is called once per frame
    void Update()
    {
		if (!Console.Instance.ShowConsole) //HACK: Replace with Pause gamestate
		{
			//Stuff to do when not paused
			
			_move = Vector3.zero;
			
			switch(_state)
			{
				case PLAYERSTATES.RUNNING:
				
					if (Input.GetKey(KeyCode.Space) && ctrl.isGrounded)
		            {
		                _rigidbody.AddForceAtPosition(new Vector3(0, Random.Range(0,10), Random.Range(0,10)),new Vector3(0, Random.Range(0,10), Random.Range(0,10)));
						pFallSpeed.y = pMinJumpSpeed;
						SetState(PLAYERSTATES.JUMPING);
					}
					else if (!ctrl.isGrounded)
					{
						SetState(PLAYERSTATES.FALLING);
					}
				
				break;
				
				case PLAYERSTATES.JUMPING:
				
					if (Input.GetKey(KeyCode.Space) && pFallSpeed.y < pMaxJumpSpeed)
					{
						pFallSpeed.y += pJumpSpeed;
					}
					else if (Input.GetKey(KeyCode.Space) && pFallSpeed.y >= pMaxJumpSpeed && !ctrl.isGrounded)
					{
						SetState(PLAYERSTATES.FALLING);
					}
					else if (!Input.GetKey(KeyCode.Space))
					{
						SetState(PLAYERSTATES.FALLING);
					}
					else if (ctrl.isGrounded)
					{
						SetState(PLAYERSTATES.RUNNING);
					}
				
				break;
				
				case PLAYERSTATES.FALLING:
				
					if (!ctrl.isGrounded)
					{
						pFallSpeed += Physics.gravity * Time.deltaTime;
					}
					else if (ctrl.isGrounded)
					{
						SetState(PLAYERSTATES.RUNNING);
					}
				
				break;
				
				case PLAYERSTATES.CINEMATIC:
				break;			
			}
			
			 _move += pFallSpeed;

            ctrl.Move( _move  * Time.deltaTime);
			
		}
		else
		{
			//Stuff to do when Paused
		}		
		
    }//end update
	
	public void SetState(PLAYERSTATES newState)
	{
		switch(newState)
		{
			case PLAYERSTATES.RUNNING:
			
				switch(_state)
				{
					case PLAYERSTATES.RUNNING:
				
					break;
					
					case PLAYERSTATES.JUMPING:
						_transform.rotation = Quaternion.identity;						
						_state = PLAYERSTATES.RUNNING;
					break;
					
					case PLAYERSTATES.FALLING:
						_transform.rotation = Quaternion.identity;
						_state = PLAYERSTATES.RUNNING;
					break;
					
					case PLAYERSTATES.CINEMATIC:
						_state = PLAYERSTATES.RUNNING;
					break;
				
					default:
						_transform.rotation = Quaternion.identity;
						_state = PLAYERSTATES.RUNNING;
					break;
				}
			
			break;
			
			case PLAYERSTATES.JUMPING:
			
				switch(_state)
				{
					case PLAYERSTATES.RUNNING:
						_state = PLAYERSTATES.JUMPING;
					break;
					
					case PLAYERSTATES.JUMPING:
					break;
					
					case PLAYERSTATES.FALLING:
						_state = PLAYERSTATES.JUMPING;
					break;
					
					case PLAYERSTATES.CINEMATIC:
						_state = PLAYERSTATES.JUMPING;
					break;
				
					default:
						_state = PLAYERSTATES.JUMPING;
					break;
				}
			
			break;
			
			case PLAYERSTATES.FALLING:
			
				switch(_state)
				{
					case PLAYERSTATES.RUNNING:
						_state = PLAYERSTATES.FALLING;
					break;
					
					case PLAYERSTATES.JUMPING:
						_state = PLAYERSTATES.FALLING;
					break;
					
					case PLAYERSTATES.FALLING:
						
					break;
					
					case PLAYERSTATES.CINEMATIC:
						_state = PLAYERSTATES.FALLING;
					break;
				
					default:
						_state = PLAYERSTATES.FALLING;
					break;
				}
			
			break;
			
			case PLAYERSTATES.CINEMATIC:
			
				switch(_state)
				{
					case PLAYERSTATES.RUNNING:
						_state = PLAYERSTATES.CINEMATIC;
					break;
					
					case PLAYERSTATES.JUMPING:
						_state = PLAYERSTATES.CINEMATIC;
					break;
					
					case PLAYERSTATES.FALLING:
						_state = PLAYERSTATES.CINEMATIC;
					break;
					
					case PLAYERSTATES.CINEMATIC:
					break;
				
					default:
						_state = PLAYERSTATES.CINEMATIC;
					break;
				}
			
			break;
			
			default:
				
				_state = PLAYERSTATES.RUNNING;
			
			break;
		}
	}
	
	public override void CalledUpdate()
	{
		//do nothing
	}
	
	public override int TakeDamage(int amount)
	{		
		Instantiate(DamageParticles, _transform.position, _transform.rotation);		
		
		return base.TakeDamage(amount);
	}	
	
	public void ResetPlayer()
	{
		_transform.position = new Vector3(-1f, 1.3f, 1f);
	}
	
	public void ResetPlayer(ConsoleCommandParams parameters)
	{
		ResetPlayer();
	}
	
}//end class
