using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum CombatStates
{
  InputSetup,
  TakingInput,
  HeroAttack,
  HeroAttacking,
  EnemyAttack,
  HeroDying,
  EnemyDefeated,
  OutofCombat
}

public class CombatManager : MonoBehaviour {


	public float health = 100f;
	public AudioClip tap;
	public AudioClip badTap;
	public AudioClip finish;
	public AudioClip win;
	public AudioSource source;
	public Canvas enemyBarPrefab;
	public TextMeshPro mTextPrefab;
	public TextMeshPro mText;
	public Canvas healthBarV2;
  private Animator animator;

  public WordList list;
	public Camera cam;
	public GameObject tempEnemy;
	private bool currentLetter = false;
	public bool wordExists = false;
  public bool triggered = false;
	public float timeleft = 10;
	public float timer;
	public float damage = 0;
	private float damageToGive;
  //private bool currentWord = false;
  private GameObject hero;
  public CombatStates combatState;
  

	void Start(){
    //InvokeRepeating("NewWord", 0, 3);
    //NewWord();
    //timer = timeleft;
    animator = GetComponent<Animator>();
    combatState = CombatStates.OutofCombat;
  }
	void Update(){
    switch (combatState)
    {
      case CombatStates.InputSetup:
        timer = timeleft;
        NewWord(tempEnemy);
        combatState = CombatStates.TakingInput;
        break;

      case CombatStates.TakingInput:
        cam.backgroundColor = Color.black;

        //mText.transform.position = new Vector3(enemy.transform.position.x, 1, enemy.transform.position.z);

        //I want to get the keyboard strokes and compare them between the characters that
        //exist in the current word (later on WORDS).



        if (wordExists == true)
        {

          timer -= Time.deltaTime;
          if (mText.text.Length == 0)
          {

            source.clip = finish;
            source.Play();

            //KillEnemy();
            AddDamage();
            //wordExists = false;
            //NewWord();
          }

          if (timer <= 0)
          {
            wordExists = false;
            mText.text = "";
            combatState = CombatStates.HeroAttack;
          }

          foreach (char c in Input.inputString)
          {
            if (c == mText.text[0])
            {
              currentLetter = true;
            }
            else
            {
              source.clip = badTap;
              source.Play();
              WrongLetter();
            }


          }
          //If the current pressed key is the first letter of a word, it will be deleted
          if (currentLetter == true)
          {
            mText.text = mText.text.Remove(0, 1);
            source.clip = tap;
            source.Play();
            currentLetter = false;
          }
          //if the word no longer exists, it will spawn a new one

        }
        else
          timer = timeleft;

        break;

      case CombatStates.HeroAttack:
        combatState = CombatStates.HeroAttacking;
        hero.GetComponent<moveCharacter>().Attack();
        break;

      case CombatStates.EnemyAttack:
        combatState = CombatStates.InputSetup;
        break;

      default:
        break;
    }
    

  }
	//will call the GetWord function from the WordList class to get a new word
	public void NewWord(GameObject parent){
		
		//mText = GetComponent<TextMeshPro>();
		//enemy = Instantiate(enemyPrefab);
		tempEnemy = parent;
    if (GameObject.FindGameObjectWithTag("Hero").GetComponent<moveCharacter>().nextFloor == false)
    {
      cam.GetComponent<FollowPlayer>().rotOffset.x = -6.51f;
      mText = Instantiate(mTextPrefab, new Vector3(parent.transform.position.x, parent.transform.position.y + 1f, parent.transform.position.z), Quaternion.Euler(0, -90, 0));
    }
    else if (GameObject.FindGameObjectWithTag("Hero").GetComponent<moveCharacter>().nextFloor == true)
    {
      cam.GetComponent<FollowPlayer>().rotOffset.x = 6.51f;
      mText = Instantiate(mTextPrefab, new Vector3(parent.transform.position.x, parent.transform.position.y + 1f, parent.transform.position.z + 7), Quaternion.Euler(0, 90, 0));
    }
    //new enemy
    if (wordExists == false){
			healthBarV2 = Instantiate(enemyBarPrefab, new Vector3(mText.transform.position.x,mText.transform.position.y+1f,mText.transform.position.z+1.5f), Quaternion.Euler(0,-90,0));//Quaternion.Euler(0,-123.688f,0));
			//healthBarV2.text = "aaaaaaaaaa";
		}
		mText.text = list.GetWord();
	
		wordExists = true;
	}

	void AddDamage(){
		damage = 10;
		damageToGive += damage;
		bool maxed = GameObject.FindGameObjectWithTag("HitPointBar").GetComponent<ManageHitPoints>().BuildUp(damage);
    if (maxed)
      timer = 0;
		else
		  NewWord(tempEnemy);
		//Do something cool here, or call another class to do it
		
	}

	void ChangeColor(){
		//Can set an active color for this word.
		//mText.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
	}
	public void ChangeToDead(){
		wordExists = false;
		
		source.clip = win;
		source.Play();
	}
	void WrongLetter(){
		GameObject.FindGameObjectWithTag("HitPointBar").GetComponent<ManageHitPoints>().Clear();
		health -= 10f;
		damageToGive = 0;
		if(health <=0){
			SceneManager.LoadScene(0);
		}
		//Debug.Log("Wrong Letter");
		//falsh the screen red for a little
		cam.backgroundColor = Color.red;
		
		//cam.backgroundColor = Color.black;
	}

  public void CombatStart(GameObject hero, GameObject enemy)
  {
    this.hero = hero;
    this.tempEnemy = enemy;
    combatState = CombatStates.InputSetup;
  }

  public void ResolveHeroAttack()
  {
    bool dead = tempEnemy.GetComponent<Life>().TakeDamage(damageToGive);
    if (dead)
    {
      GameObject.FindGameObjectWithTag("HitPointBar").GetComponent<ManageHitPoints>().Clear();
      damageToGive = 0;
      ChangeToDead();
      GameObject.Find("Main Camera").GetComponent<FollowPlayer>().inCombat = false;
      GameObject.FindGameObjectWithTag("EnemyHealth").GetComponent<ManageEnemyHealthBar>().Die();
      Destroy(tempEnemy);
      combatState = CombatStates.OutofCombat;
      hero.GetComponent<moveCharacter>().StartMoving();
    }
    else
    {
      combatState = CombatStates.EnemyAttack;
    }
  }
}
