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
  public Camera cam;

	public Canvas enemyBarPrefab;
	
	public Canvas healthBarV2;

  public WordList list;
	
	public GameObject tempEnemy;
	public float damage = 0;
	private float damageToGive;
  private GameObject hero;
  public CombatStates combatState;
  private WordDisplay wordDisplay;
  
  public AudioManager music;
  

	void Start(){
    //timer = timeleft;
    wordDisplay = GetComponent<WordDisplay>();
    combatState = CombatStates.OutofCombat;
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
  }
  public void OnTextCompleted()
  {
    AddDamage();
  }
  void Update(){
    switch (combatState)
    {
      case CombatStates.InputSetup:
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
  }

  void AddDamage(){
		damage = 10;
		damageToGive += damage;
		bool maxed = GameObject.FindGameObjectWithTag("HitPointBar").GetComponent<ManageHitPoints>().BuildUp(damage);
    if (maxed)
    {
      wordDisplay.StopDisplay();
      combatState = CombatStates.HeroAttack;
    }
    else
      wordDisplay.NewText(tempEnemy.transform, list.GetWord());		
	}

	void ChangeColor(){
		//Can set an active color for this word.
		//mText.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
	}
	public void ChangeToDead(){
    wordDisplay.StopDisplay();	
	}


  public void CombatStart(GameObject hero, GameObject enemy)
  {
    var levelOfRoom = GameObject.Find("TowerManager").GetComponent<LevelManager>().currentFloor;
    this.hero = hero;
    this.tempEnemy = enemy;
    
    SetEnemyHealth(tempEnemy.name, levelOfRoom);
    
    healthBarV2 = Instantiate(enemyBarPrefab, new Vector3(tempEnemy.transform.position.x, tempEnemy.transform.position.y + 2f, tempEnemy.transform.position.z + 1.5f), Quaternion.Euler(0, -90, 0));
    
    wordDisplay.StartDisplay(OnTextCompleted, OnTimerExpired, OnIncorrectLetter);
    
    combatState = CombatStates.InputSetup;
  }
  public void ResolveEnemyAttack()
  {
    //music.FireSound();
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
    hero.GetComponent<moveCharacter>().moveSpeed = 4;
    hero.GetComponent<moveCharacter>().StartMoving();
  }
  public void EnemyHitByHero()
  {
    music.SwordClash();
    bool dead = tempEnemy.GetComponent<Life>().TakeDamage(damageToGive);
    damageToGive = 0;
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
  public void SetEnemyHealth(string enemyName, int levelOfRoom)
  {
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
    //Debug.Log("Enemy Health: " + life);
 }
  public void HeroHitByEnemy()
  {
    if(tempEnemy.name == "Mage(Clone)"){
      music.FireSound();
    }
    
    bool dead = false;
    if (dead)
    {

    }
    else
    {
      music.Block();
      hero.GetComponent<moveCharacter>().Block();
    }
  }
  public void ResolveHeroAttack()
  {
    if(tempEnemy.GetComponent<Life>().life > 0)
      combatState = CombatStates.EnemyAttack;
  }
}
