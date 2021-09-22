using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public SlingShooter SlingShooter;
    public List<Bird> Birds;
    public List<Enemy> Enemies;
    public TrailController TrailController;
    public BoxCollider2D TapCollider;
    public List<GameObject> panel;
    private Bird _shotBird;
    private bool _isGameEnded = false;

    void Start()
    {
        for(int i = 0; i < Birds.Count; i++)
        {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }
        for(int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }
        TapCollider.enabled = false;
        SlingShooter.InitiateBird(Birds[0]);
        _shotBird = Birds[0];
    }
    public void ChangeBird()
    {   
        print(Enemies.Count);
        TapCollider.enabled = false;
        if (_isGameEnded)
        {
            panel[0].SetActive(true);
            return;
        }

        Birds.RemoveAt(0);

        if(Birds.Count > 0)
        {
            SlingShooter.InitiateBird(Birds[0]);
            _shotBird = Birds[0];
        }
        else
        {
            panel[1].SetActive(true);
        }
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for(int i = 0; i < Enemies.Count; i++)
        {
            if(Enemies[i].gameObject == destroyedEnemy)
            {
                Enemies.RemoveAt(i);
                break;
            }
        }

        if(Enemies.Count == 0)
        {
            _isGameEnded = true;
        }
    }
    public void AssignTrail(Bird bird)
    {
        TrailController.SetBird(bird);
        StartCoroutine(TrailController.SpawnTrail());
        TapCollider.enabled = true;
    }
    void OnMouseUp()
    {
        print(_shotBird);
        if(_shotBird != null)
        {
            // print("klik GM");
            _shotBird.OnTap();
        }
    }
    private void Update() 
    {
        Scene currentScene = SceneManager.GetActiveScene();
       if(Input.GetKeyDown(KeyCode.R) && (TapCollider.enabled == false))
        {
            SceneManager.LoadScene(currentScene.name);
        }
        if(SceneManager.GetActiveScene().buildIndex+1 == 2)
        {
            if(Input.GetKeyDown(KeyCode.Space) && panel[0].gameObject.activeInHierarchy)
            {
                SceneManager.LoadScene(0);
            }

        }
        else
        {
            if(Input.GetMouseButtonDown(0) && panel[0].gameObject.activeInHierarchy)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
                print("Klik panel win");
            
            }

        }
    }
}