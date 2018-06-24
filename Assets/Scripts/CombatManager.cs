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
  public float baseEnemyDmg = 10f;
  public float wrongLetterDmg = 5f;
  public Camera cam;
  public GameObject blackScreen;
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
  public string curDifficulty;
  public Dropdown dif;
  

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
    DamageHero(wrongLetterDmg);
  }
  public bool DamageHero(float dmg)
  {
    health -= dmg;
    if (health <= 0)
    {
      PlayDeathSequence();
      return true;
    }
    return false;
  }
  public void OnTextCompleted(int wordLength)
  {
    AddDamage(wordLength);
  }
  void Update(){
    switch (combatState)
    {
      case CombatStates.InputSetup:
        wordDisplay.NewText(tempEnemy.transform, list.GetWord(curDifficulty));
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
  public void PlayDeathSequence()
  {
    music.PlayerDead();
    wordDisplay.StopDisplay();
    hero.GetComponent<moveCharacter>().Die();
    GameObject.Find("GameManager").GetComponent<GameManager>().StartZoomIn();

    BlackScreen blackScreenComp = blackScreen.GetComponent<BlackScreen>();
    StartCoroutine(blackScreenComp.FadeInText("THE HERO IS DEAD", 1f, 2f));
    StartCoroutine(blackScreenComp.FadeToBlack(1f, 3.5f));
    StartCoroutine(blackScreenComp.FadeOutText(1f, 5f));

    Invoke("RestartGame", 7f);
    
  }

  public void RestartGame()
  {
    Cursor.visible = true;
    SceneManager.LoadScene(0);
  }
  void AddDamage(int wordLength)
  {
		damage = wordLength*5;
    Debug.Log("Word Dmg: " + damage);
    damageToGive += damage;
		bool maxed = GameObject.FindGameObjectWithTag("HitPointBar").GetComponent<ManageHitPoints>().BuildUp(damage);
    if (maxed)
    {
      wordDisplay.StopDisplay();
      combatState = CombatStates.HeroAttack;
    }
    else
      wordDisplay.NewText(tempEnemy.transform, list.GetWord(curDifficulty));		
	}


	public void ChangeToDead(){
    wordDisplay.StopDisplay();	
	}


  public void CombatStart(GameObject hero, GameObject enemy)
  {
    var levelOfRoom = GameObject.Find("TowerManager").GetComponent<LevelManager>().currentFloor;
    this.hero = hero;
    this.tempEnemy = enemy;
    
    SetWordDifficulty(GetTrueRoomLevel(levelOfRoom));
    SetEnemyHealth(tempEnemy.name, GetTrueRoomLevel(levelOfRoom));
    
    healthBarV2 = Instantiate(enemyBarPrefab, new Vector3(tempEnemy.transform.position.x, tempEnemy.transform.position.y + 2f, tempEnemy.transform.position.z + 1.5f), Quaternion.Euler(0, -90, 0));
    
    wordDisplay.StartDisplay(OnTextCompleted, OnTimerExpired, OnIncorrectLetter);
    
    combatState = CombatStates.InputSetup;
  }

  public void SetWordDifficulty(int trueRoomLevel){
    if (trueRoomLevel > 25)
    {
      curDifficulty = "Hard";
    }
    else if (trueRoomLevel > 10 ){
      curDifficulty = "Medium";
    }
    else
    {
      curDifficulty = "Easy";
    } 
  }
  public int GetTrueRoomLevel(int levelOfRoom)
  {
    if(levelOfRoom == 0)
    {
      levelOfRoom += 1;
    }
    if(dif.options[dif.value].text == "Easy")
    {
      return levelOfRoom;
    }
    else if (dif.options[dif.value].text == "Normal" || dif.options[dif.value].text == "Medium")
    {
      return levelOfRoom + 10;
    }
    else if (dif.options[dif.value].text == "Hard")
    {
      return levelOfRoom + 25;
    }
    return levelOfRoom;
  }
  public void ResolveEnemyAttack()
  {
    //music.FireSound();
    
    bool dead = DamageHero(baseEnemyDmg);
    if(!dead)
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
  }
  public void EnemyHitByHero()
  {
    music.SwordClash();
    Debug.Log("Round Dmg: " + damageToGive);
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
    tempEnemy.GetComponent<Life>().life = enemyLevel * levelOfRoom/2;
    Debug.Log("Enemy Health: " + tempEnemy.GetComponent<Life>().life);
 }
  public void HeroHitByEnemy()
  {
    if(tempEnemy.name == "Mage(Clone)"){
      music.FireSound();
    }
    
    bool dead = false;
    if (dead)
    {
      PlayDeathSequence();
    }
    else
    {
      music.Block();
      hero.GetComponent<moveCharacter>().Block();
    }
  }
  public void ResolveHeroAttack()
  {
    if(tempEnemy != null && tempEnemy.GetComponent<Life>().life > 0)
      combatState = CombatStates.EnemyAttack;
  }
}
