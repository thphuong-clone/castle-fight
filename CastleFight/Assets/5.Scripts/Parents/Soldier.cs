/*
 *The parent soldier. Every soldier inherit from this class
 *Soldier inherit from Unit
*/
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using PathFinder;


public class Soldier : Unit
{
    public static int SOLDIER_STATE_ATTACKING = -1;
    public static int SOLDIER_STATE_IDLE = 0;
    public static int SOLDIER_STATE_MOVE = 1;
    public static int SOLDIER_STATE_ATTACK_MOVE = 2;

    //The destinated position
    public Vector2 destinatedPos;
    public SearchNode nextPathNode;
    private Vector2 nextPosition;
    private bool doneMoving = true;

    public bool isDebugMode;

    protected Vector2 tempPos;

    public int soldierState; //0 = idle, 1 = move only , 2 = move and attack , -1 = busy doing something else (mostly in a midle of a animation)

    //the movement speed of the class
    public float moveSpeed;

    public bool isAttacking;

    public float animationTime;

    public GameObject soldierSprite;
    protected Animator ani;

    //There are 2 attack type, light attack and heavy attack.
    public float attackDamage;
    public int attackType;

    //this float is to check if the soldier is stuck
    float lastY;

    //[SerializeField]
    //List<Unit> listOfCollidingEnemy = new List<Unit>();

    //to move to the position, left or right, not important, will replace with A* anyway 
    //	int direction;

    //[SerializeField]
    //bool collideEnemy;

    protected Rigidbody2D r;

    public override void Awake()
    {
        base.Awake();
        //PlayerController.orderSoldier ();
        //collideEnemy = false;

        r = gameObject.GetComponent<Rigidbody2D>();

        try
        {
            ani = soldierSprite.gameObject.GetComponent<Animator>();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
        //		direction = 1;

        isDead = false;

        StartCoroutine(soldierLoop());
        //soldierState = 0;
        //check if the unit is attacking something, which is not, he is just born, dude.
        isAttacking = false;
    }

    //This loop will be caled almost every frame, for the soldier to do his action.
    IEnumerator soldierLoop()
    {
        while (!this.isDead)
        {
            if (isDebugMode)
            {
                Debug.Log(r.velocity);
            }
            switch (soldierState)
            {
                case -1: // soldier is busy killing enemy
                    yield return null;
                    break;
                case 0: //idle - find enemy
                    findEnemy();
                    if (!functionDoneMoving())
                        soldierState = 2;
                    yield return new WaitForSeconds(0.2f);
                    //find enemy, if find, then attack.
                    break;
                case 1: // move only - 
                    if (isAttacking)
                    {
                        yield return null;
                    }
                    else
                    {
                        MoveToPosition(nextPathNode);
                        yield return new WaitForSeconds(0.05f);
                    }
                    break;
                case 2: // move and attack
                    if (isAttacking)
                    {
                        yield return null;
                    }
                    else
                    {
                        MoveToPosition(nextPathNode);
                        findEnemy();
                        yield return new WaitForSeconds(0.05f);
                    }
                    break;
                default:
                    yield return new WaitForSeconds(0.05f);
                    break;
            }
//            if (listOfCollidingEnemy.Count == 0)
//                collideEnemy = false;
            yield return null;
        }
        yield return null;
    }


    //move straight to the destinated position. this unit won't stop, and won't listen to anything.
    void moveToPosition(Vector2 pos)
    {
        //bool doneMoving = false;
        //if the unit is done moving, than change him to idle state
        r.drag = 12;
        //wait is doneMoving always false here. Why check it????????????????????
        if (!doneMoving && !isAttacking)
        {
            //the game will give an error if this case happend, so we put it here to avoid problems
            //if (transform.position.x == pos.x && transform.position.y == pos.y)
            //{
            //    doneMoving = true;
            //    soldierState = 0;
            //    return;
            //}
            //Make the unit move to the destinated position - set up the velocity
            r.velocity = calculateVelocity(pos);

            //if the unit is sooo close to the destinated position
            //if (functionDoneMoving())
            //{
            //    //it will be false again anyway so why set it here
            //    doneMoving = true;
            //    soldierState = 0;
            //}
            if (ReachedPoint(pos))
            {
                doneMoving = true;
                soldierState = 0;
            }
        }
        else
        {//if done moving and attacking
            //here again ._.
            doneMoving = true;
            soldierState = 0;
        }

    }

    //This functions make the unit move to the next node in the list/
    public void MoveToPosition(SearchNode node)
    {
        //if there are next node.
        if (node != null)
        {
            //if the unit is in the next node.
            if (doneMoving)
            {
                //Debug.Log("ended");
                //Debug.Log("reached node " + node.position);
                nextPathNode = nextPathNode.next;
                //Debug.Log("next position is " + nextPosition);
                if (nextPathNode != null)
                {
                    doneMoving = false;
                    nextPosition = GridMapUtils.GetRealPosition(nextPathNode.position);
                }
            }

            //Debug.Log("moving to " + nextPosition);
            moveToPosition(nextPosition);

        }
        else
        {
            //Debug.Log("done moving");
            soldierState = 0;
        }
    }

    //To find if there are any enemy in the sight of this unit.
    protected virtual void findEnemy()
    {
        if (!isAttacking)
        {

            Vector2 thisPos = new Vector2(transform.position.x, transform.position.y);
            //find all the collider in the range of the unit, which is 1.5 radius - maybe 2, I can't decide yet
            Collider2D[] col = Physics2D.OverlapCircleAll(thisPos, 2f);
            List<GameObject> enemyList = new List<GameObject>();
            if (col.Length == 0) { }
            else
            {
                foreach (Collider2D c in col)
                {
                    try
                    {
                        //if enemy, then add him to the enemy list.
                        if (c.gameObject.GetComponent<Unit>().isPlayerOne != this.isPlayerOne)
                        {
                            //add to the enemy list if he is not dead yet.
                            if (isHuman)
                            {
                                if (!c.gameObject.GetComponent<Unit>().isDead)
                                {
                                    enemyList.Add(c.gameObject);
                                }
                            }
                            else
                            {
                                if (!c.gameObject.GetComponent<Unit>().isDead && c.gameObject.GetComponent<Unit>().isHuman)
                                {
                                    enemyList.Add(c.gameObject);
                                }
                            }
                                

                        }
                    }
                    //that mean it's a moutain or whatever, I haven't invented it yet.
                    catch (Exception e)
                    {
                        debugException(e.ToString());
                    }
                }
                //if there is no enemy
                if (enemyList.Count == 0)
                {
                    //do nothing, maybe, do whatever you are meant to do, like move to the destinated position ?
                }
                //there is enemy here bitch.
                else
                {
                    float distance = 99999;
                    GameObject nearest = this.gameObject;
                    foreach (GameObject c in enemyList)
                    {
                        //find the nearest enemy
                        if (calculateDistance(c) < distance)
                        {
                            nearest = c;
                            distance = calculateDistance(c);
                        }
                    }
                    //attack him
                    StartCoroutine(attackEnemy(nearest));
                }
            }
        }
    }

    //This is the attack function. The soldier will charge to the enemy and attack the shit out of him.
    public virtual IEnumerator attackEnemy(GameObject enemy)
    {
        isAttacking = true;
        int exState = soldierState;
        soldierState = -1;

        Unit en = enemy.GetComponent<Unit>();

        //		float dis = this.radius + en.radius + 0.35f;

        //move close the the eemey
		Collider2D thisCollider = this.gameObject.GetComponent<Collider2D> ();
		Collider2D enemyCollider = enemy.gameObject.GetComponent<Collider2D> ();
        int count = 0;
        while (!Physics2D.IsTouching(thisCollider,enemyCollider))
        {
            //while (calculateDistance(enemy) > dis) {
            r.velocity = calculateVelocity(new Vector2(enemy.transform.position.x, enemy.transform.position.y));

            count++;
            if (en.isDead || count == 20 || this.isDead)
            {
                goto StopAttack;
                //StopCoroutine("attackEnemy");
                //break;
            }
            yield return new WaitForSeconds(0.05f);
        }
        r.velocity = calculateVelocity(new Vector2(enemy.transform.position.x, enemy.transform.position.y));
        r.velocity = Vector2.zero;
        //If touch the enemy

        r.drag = 100;

        if (!en.isDead)
        {
            //Deal damage to him, if this unit is not dead
            if (!this.isDead)
            {
                ani.SetBool("Attack", true);
                yield return new WaitForSeconds(animationTime);
                if (this.isDead)
                {
                    goto StopAttack;
                }
                AttackInformation attackInfo = new AttackInformation(this.attackDamage, this.attackType);
                en.SendMessage("receiveDamage", attackInfo);
                yield return new WaitForSeconds(2 - animationTime);

            }

            if (en.isDead)
            {
                ani.SetBool("Attack", false);
                isAttacking = false;
            }
        }
    StopAttack:
        if (soldierState == -1)
            soldierState = exState;

        r.drag = 12;
        ani.SetBool("Attack", false);
        isAttacking = false;
        yield return null;
    }

    //The function is to check if the unit is dead, or not. heh. 
    public override IEnumerator checkHealth()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(0.1f);
            if (health <= 0)
            {
                isDead = true;
                //push the unit back
                r.velocity = Vector2.zero;
                Vector2 force = calculateVelocity(destinatedPos);
                force = new Vector2(force.x * 10 / moveSpeed, force.y * 10 / moveSpeed);
                r.AddForce(-calculateVelocity(destinatedPos) * 5000 / moveSpeed);
                //remove the unit from the damn list
            }
        }
        yield return new WaitForSeconds(0.5f);
        this.gameObject.transform.position = new Vector3(100, 100, 100);
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }

    //What the hell does this do ? Oh, to check if stuck in wall, will delete till I have A*
    IEnumerator upgradeYPos()
    {
        while (true)
        {
            if (Mathf.Abs(transform.position.y - lastY) < 0.02f)
            {

            }
            else
            {
                lastY = transform.position.y;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }


//    public virtual void OnCollisionEnter2D(Collision2D col)
//    {
//        if (col.gameObject.tag == "Building" || col.gameObject.tag == "Soldier")
//        {
//            if (col.gameObject.GetComponent<Unit>().isPlayerOne != this.isPlayerOne)
//            {
//                listOfCollidingEnemy.Add(col.gameObject.GetComponent<Unit>());
//                collideEnemy = true;
//            }
//        }
//
//    }

    //This function need rework
//    public virtual void OnCollisionStay2D(Collision2D unit)
//    {
//        //		//if collide a "friendly" wall
//        //		try{
//        //			//if collider a friendly unit - this code will be get rid off by the A* pathfinding smart algorithm
//        //			if (unit.gameObject.GetComponent<Unit>().isPlayerOne == this.isPlayerOne){
//        //				if (unit.gameObject.tag == "building"){
//        //					if (Mathf.Abs (transform.position.y - lastY) < 0.02f){
//        //						r.velocity = new Vector2 (1, 0) * moveSpeed  / 2 * direction;
//        //					}
//        //				}
//        //			}
//        //			//enemy !!!!
//        //			else{
//        //				if (unit.gameObject.tag == "Building" && this.soldierState == 1) {
//        //						collideEnemy = true;
//        //					soldierState = 2;
//        //				}
//        //			}
//        //		}
//        //		//collide on a moutain, hmmmm
//        //		catch(Exception e){
//        //			string s = e.ToString();
//        //			s = "";
//        //			if (s != ""){
//        //				Debug.Log(s);
//        //			}
//        ////			Debug.Log(e.ToString());
//        //		}
//
//    }



    //On collision exit, I just want to make sure that there is 
//    public virtual void OnCollisionExit2D(Collision2D col)
//    {
//        if (col.gameObject.tag == "Building" || col.gameObject.tag == "Soldier")
//        {
//            if (col.gameObject.GetComponent<Unit>().isPlayerOne != this.isPlayerOne)
//            {
//                listOfCollidingEnemy.Remove(col.gameObject.GetComponent<Unit>());
//                if (listOfCollidingEnemy.Count == 0)
//                    collideEnemy = false;
//            }
//        }
//    }


    //Calculate the distance between two gameobject
    protected float calculateDistance(GameObject col)
    {
        float x = col.gameObject.transform.position.x;
        float y = col.gameObject.transform.position.y;

        x = Mathf.Abs(this.transform.position.x - x);
        y = Mathf.Abs(this.transform.position.y - y);

        float distance = new Vector2(x, y).magnitude;
        return distance;
    }


    //Calculate the velocity to the destination, based on the current position
    public virtual Vector2 calculateVelocity(Vector2 _position)
    {
        float angle = 0;
        float x = 0;
        float y = 0;
        try
        {
            x = Mathf.Abs(_position.x - transform.position.x);
            y = Mathf.Abs(_position.y - transform.position.y);
            angle = Mathf.Atan(x / y);

            float a = -Mathf.Atan((_position.x - transform.position.x) / (_position.y - transform.position.y));

            if (transform.position.y > _position.y)
                a += 180 * Mathf.Deg2Rad;

            if (!float.IsNaN(a))
            {
                transform.localRotation = Quaternion.Euler(0, 0, a * Mathf.Rad2Deg);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                angle = 0;
                x = 0;
                y = 0;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            angle = 0;
            x = 0;
            y = 0;
        }
        //calculate the angle


        x = Mathf.Sin(angle);
        y = Mathf.Sqrt(1 - x * x);

        if (transform.position.x > _position.x)
            x = -x;

        if (transform.position.y > _position.y)
            y = -y;

        Vector2 returnVector = new Vector2(x, y) * moveSpeed / 4;
        return returnVector;
    }

    bool FloatEqual(float x, float y)
    {
        return (Math.Abs(x - y) < 0.07f);
    }

    public void EndCurrentMove()
    {
        doneMoving = true;
    }

    bool ReachedPoint(Vector2 p2)
    {
        //if (FloatEqual(transform.position.x, p2.x) && FloatEqual(transform.position.y, p2.y))
        //            Debug.Log("current: " + transform.position + "\nreached: " + p2);
        return (FloatEqual(transform.position.x, p2.x) && FloatEqual(transform.position.y, p2.y));
    }

    //Check if the unit has reach the destinated position. Might increase a little bit more.
    bool functionDoneMoving()
    {
        if (Mathf.Abs(destinatedPos.x - transform.position.x) <= 0.5f && Mathf.Abs(destinatedPos.y - transform.position.y) < 0.5f)
        {
            //soldierState = 0; //idle the guy
            return true;
        }
        else
            return false;
    }

    //This is a function to check if an enemy is in range with the current unit. Fairly useless, might delete later.
    bool enemyInRange(GameObject en)
    {
        Vector2 thisPos = new Vector2(transform.position.x, transform.position.y);
        Collider2D[] col = Physics2D.OverlapCircleAll(thisPos, 2f);

        if (col.Length == 0)
            return false;

        foreach (Collider2D c in col)
        {
            if (c.transform.gameObject == en)
                return true;
        }

        return false;
    }

    /// <summary>
    /// Deploy soldier to desired position
    /// </summary>
    public void Deploy(Vector2 position)
    {
        Position2D start = GridMapUtils.GetTile(transform.position);
        Position2D end = GridMapUtils.GetTile(position);
        nextPathNode = PathFinder.PathFinder.FindPath(PlayerController.knownWorld, start, end);
        EndCurrentMove();
    }

    /// <summary>
    /// Deploy soldier to desired position, set the state of the soldier
    /// </summary>
	public void Deploy(Vector3 _vector){
		Position2D start = GridMapUtils.GetTile(transform.position);
		Position2D end = GridMapUtils.GetTile(new Vector2(_vector.x,_vector.y));
		nextPathNode = PathFinder.PathFinder.FindPath(PlayerController.knownWorld, start, end);
		soldierState = (int)_vector.z;
		EndCurrentMove();
	}

    public void Deploy(Vector2 position, int state)
    {
        Position2D start = GridMapUtils.GetTile(transform.position);
        Position2D end = GridMapUtils.GetTile(position);
        nextPathNode = PathFinder.PathFinder.FindPath(PlayerController.knownWorld, start, end);
        soldierState = state;
        EndCurrentMove();
    }

    /// <summary>
    /// Deploy soldier to desired position
    /// </summary>
    public void Deploy(float x, float y)
    {
        Position2D start = GridMapUtils.GetTile(transform.position);
        Position2D end = GridMapUtils.GetTile(x, y);
        nextPathNode = PathFinder.PathFinder.FindPath(PlayerController.knownWorld, start, end);
        EndCurrentMove();
    }

    /// <summary>
    /// Deploy soldier to desired position, set the state of the soldier
    /// </summary>
    public void Deploy(float x, float y, int state)
    {
        Position2D start = GridMapUtils.GetTile(transform.position);
        Position2D end = GridMapUtils.GetTile(x, y);
        nextPathNode = PathFinder.PathFinder.FindPath(PlayerController.knownWorld, start, end);
        soldierState = state;
        EndCurrentMove();
    }

    //	void OnDestroy(){
    //		Debug.LogWarning ("I am killed :(");
    //	}
}