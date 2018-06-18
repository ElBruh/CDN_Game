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
  EnemyAttacking,
  HeroDying,
  EnemyDefeated,
  OutofCombat
}

public class CombatManager : MonoBehaviour {


	public float health = 100f;
	
	public Canvas enemyBarPrefab;
	public TextMeshPro mTextPrefab;
  private Animator animator;
	private bool currentLetter = false;
	public bool wordExists = false;
  public bool triggered = false;
	public float timeleft = 10;
	public float damage = 0;
	private float damageToGive;
  //private bool currentWord = false;
  private GameObject hero;
  public CombatStates combatState;
  //private bool changeMusic;
  [HideInInspector]
  public GameObject tempEnemy;
  [HideInInspector]
  public TextMeshPro mText;
  [HideInInspector]
  public Canvas healthBarV2;
  [HideInInspector]
  public WordList list;
  [HideInInspector]
  public Camera cam;
  [HideInInspector]
  public float timer;
  public AudioManager music;
	void Start(){
    animator = GetComponent<Animator>();
    combatState = CombatStates.OutofCombat;
  }
	void Update(){
    switch (combatState)
    {
      case CombatStates.InputSetup:
        //changeMusic = true;
        timer = timeleft;
        NewWord(tempEnemy);
        combatState = CombatStates.TakingInput;
        break;

      case CombatStates.TakingInput:

        if (wordExists == true)
        {

          timer -= Time.deltaTime;
          if (mText.text.Length == 0)
          {
            music.FinishWord();
            AddDamage();
          }

          if (timer <= 0)
          {
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
              music.BadTap();
              WrongLetter();
            }


          }
          //If the current pressed key is the first letter of a word, it will be deleted
          if (currentLetter == true)
          {
            mText.text = mText.text.Remove(0, 1);
            music.Tap();
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
        combatState = CombatStates.EnemyAttacking;
        tempEnemy.GetComponent<Enemy>().Attack();
        break;

      default:
        break;
    }
    
    

  }
	//will call the GetWord function from the WordList class to get a new word
	public void NewWord(GameObject parent){
		tempEnemy = parent;
  
    mText = Instantiate(mTextPrefab, new Vector3(parent.transform.position.x, parent.transform.position.y + 1f, parent.transform.position.z), Quaternion.Euler(0, -90, 0));
    
    if (wordExists == false){
			healthBarV2 = Instantiate(enemyBarPrefab, new Vector3(mText.transform.position.x,mText.transform.position.y+1f,mText.transform.position.z+1.5f), Quaternion.Euler(0,-90,0));//Quaternion.Euler(0,-123.688f,0));
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

	public void ChangeToDead(){
		wordExists = false;		
	}
	void WrongLetter(){
		GameObject.FindGameObjectWithTag("HitPointBar").GetComponent<ManageHitPoints>().Clear();
		health -= 10f;
		damageToGive = 0;
		if(health <=0){
			SceneManager.LoadScene(0);
		}
	}

  public void CombatStart(GameObject hero, GameObject enemy)
  {
    var levelOfRoom = GameObject.Find("TowerManager").GetComponent<LevelManager>().currentFloor;
    this.hero = hero;
    this.tempEnemy = enemy;
    SetEnemyHealth(tempEnemy.name, levelOfRoom);
    combatState = CombatStates.InputSetup;
  }
  public void ResolveEnemyAttack()
  {
    Debug.Log("Resolving enemy attack");
    bool dead = false;
    if (dead)
    {
      
    }
    else
    {
      combatState = CombatStates.InputSetup;
    }
  }
  public void EnemyDeathCleanUp()
  {
    music.EnemyDeadSound();
    GameObject.Find("Main Camera").GetComponent<FollowPlayer>().inCombat = false;
    Destroy(tempEnemy);
    combatState = CombatStates.OutofCombat;
    hero.GetComponent<moveCharacter>().StartMoving();
    music.RunSound();
  }
  public void EnemyHitByHero()
  {
    bool dead = tempEnemy.GetComponent<Life>().TakeDamage(damageToGive);
    if (dead)
    {
      GameObject.FindGameObjectWithTag("HitPointBar").GetComponent<ManageHitPoints>().Clear();
      damageToGive = 0;
      ChangeToDead();
      GameObject.FindGameObjectWithTag("EnemyHealth").GetComponent<ManageEnemyHealthBar>().Die();
      tempEnemy.GetComponent<Enemy>().Die();
      combatState = CombatStates.EnemyDefeated;
    }
    else
    {
      //clearing hitpointbar in case the enemy attacks
      GameObject.FindGameObjectWithTag("HitPointBar").GetComponent<ManageHitPoints>().Clear();
      tempEnemy.GetComponent<Enemy>().Block();
    }
  }

  public void SetEnemyHealth(string enemyName, int levelOfRoom){
    var enemyLevel = 10;
    switch(enemyName){
      case "Mage(Clone)" :
        enemyLevel = 15;
        break;
      case "Bat(Clone)" :
        enemyLevel = 5;
        break;
      case "Skeleton(Clone)" :
        enemyLevel = 20;
        break;
      default :
        break;
    }
    var life = tempEnemy.GetComponent<Life>().life = enemyLevel * levelOfRoom;
    Debug.Log("Enemy Health: " + life);
  }
  public void HeroHitByEnemy()
  {
    bool dead = false;
    if (dead)
    {

    }
    else
    {
      hero.GetComponent<moveCharacter>().Block();
    }
  }
  public void ResolveHeroAttack()
  {
    if(tempEnemy.GetComponent<Life>().life > 0)
      combatState = CombatStates.EnemyAttack;
  }
}
