using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManagerV2 : MonoBehaviour
{

    //just like an enemy list.this is where all our diff prefabs go.
    public List<GameObject> buffet;

    //Assume that  want to battle a whole list of enemies, we will fight them one at a time until theyare defeated.Theyll fight only 3 out of our list
    //so we need a new list
    public List<GameObject> plate;

    //FoodItem to eat - Enemy to fight
    public GameObject foodToEat;

    //how many items do we want on our plat?(how many enemies on the team to defeat?)
    public int numOfFoodItemsOnPlate;
    // Start is called before the first frame update
    void Start()
    {
       //select enemies to fight out from the giant list of enemy types. 
       for(int i = 0; i < numOfFoodItemsOnPlate; i++)
        {
            //add rando food item into buffet to our plate, remember they are already different types!!
            GameObject spawnedFood = Instantiate(buffet[Random.Range(0, buffet.Count)], transform);
            plate.Add(spawnedFood);
        }
        //actually start setting which food to eat
        SetNewFoodToEat();
    }
    void SetNewFoodToEat()
    {
        foodToEat = plate[Random.Range(0,plate.Count)];
        StartCoroutine(Eating());
    }

    void Eatfood()
    {
        //remove the current food
        plate.Remove(foodToEat);
        Destroy(foodToEat);
        //check if we have eaten all the food, if not eat something else
        if (plate.Count == 0)
        {
            //if we have, we win
            Debug.Log("You ate all the food");
        }
        else
        {
            //eat something else
            SetNewFoodToEat();
        }
    }

    IEnumerator Eating()
    {
        yield return new WaitForSeconds(5);
        Eatfood();
    }
}
