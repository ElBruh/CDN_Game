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
	
	
	public AudioClip win;
  public AudioClip introSong;
  public AudioSource source;
  public Camera cam;
  public AudioSource EncounterMusic;
  public AudioSource MainMusic;
	public Canvas enemyBarPrefab;
	
	public Canvas healthBarV2;

  public WordList list;
	
	public GameObject tempEnemy;
	
	
	public float damage = 0;
	private float damageToGive;
  private GameObject hero;
  public CombatStates combatState;
  private WordDisplay wordDisplay;
  //private bool changeMusic;
  

	void Start(){
    //timer = timeleft;
    wordDisplay = GetComponent<WordDisplay>();
    combatState = CombatStates.OutofCombat;
    /*We can decide when to start and end the intro music */
    //MainMusic.clip = introSong;
    //MainMusic.Play();
  }
  public void OnTimerExpired()
  {
    combatState = CombatStates.HeroAttack;
  }
  public void OnIncorrectLetter()
  {
    GameObject.FindGameObjectWithTag("HitPointBar").GetComponent<ManageHitPoints>().Clear();
    health -= 10f;
    damageToGive = 0;
    if (health <= 0)
    {
      SceneManager.LoadScene(0);
    }
    //Debug.Log("Wrong Letter");
    //falsh the screen red for a little
    cam.backgroundColor = Color.red;

    //cam.backgroundColor = Color.black;
  }
  public void OnTextCompleted()
  {
    //KillEnemy();
    AddDamage();
    //wordExists = false;
    //NewWord();
  }
  void Update(){
    switch (combatState)
    {
      case CombatStates.InputSetup:
        //changeMusic = true;
        wordDisplay.NewText(tempEnemy.transform, list.GetWord());
        combatState = CombatStates.TakingInput;
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
    /*if(changeMusic == true){
      MainMusic.volume -= 1 * Time.deltaTime/2;
      EncounterMusic.volume += 0.5f * Time.deltaTime/2;
    }
    else if (changeMusic == false){
      MainMusic.volume += 0.5f * Time.deltaTime/2;
      EncounterMusic.volume -= 1 * Time.deltaTime/2;
    }*/
    

  }

  void AddDamage(){
		damage = 10;
		damageToGive += damage;
		bool maxed = GameObject.FindGameObjectWithTag("HitPointBar").GetComponent<ManageHitPoints>().BuildUp(damage);
    if (maxed)
    {
      wordDisplay.Stop();
      combatState = CombatStates.HeroAttack;
    }
    else
      wordDisplay.NewText(tempEnemy.transform, list.GetWord());
		//Do something cool here, or call another class to do it
		
	}

	void ChangeColor(){
		//Can set an active color for this word.
		//mText.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
	}
	public void ChangeToDead(){
    wordDisplay.Stop();	
		source.clip = win;
		source.Play();
	}


  public void CombatStart(GameObject hero, GameObject enemy)
  {
    var levelOfRoom = GameObject.Find("TowerManager").GetComponent<LevelManager>().currentFloor;
    this.hero = hero;
    this.tempEnemy = enemy;
    tempEnemy.GetComponent<Life>().life = 10 * levelOfRoom;
    healthBarV2 = Instantiate(enemyBarPrefab, new Vector3(tempEnemy.transform.position.x, tempEnemy.transform.position.y + 2f, tempEnemy.transform.position.z + 1.5f), Quaternion.Euler(0, -90, 0));
    wordDisplay.Start(OnTextCompleted, OnTimerExpired, OnIncorrectLetter);
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
    //changeMusic = false;
    GameObject.Find("Main Camera").GetComponent<FollowPlayer>().inCombat = false;
    Destroy(tempEnemy);
    combatState = CombatStates.OutofCombat;
    hero.GetComponent<moveCharacter>().StartMoving();
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
