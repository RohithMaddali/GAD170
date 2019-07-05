using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    //imagine all instances of food = enemies.
    //prefab that we made for various food types.
    public GameObject defaultFoodPrefab;

    //list to pick from
    public List<GameObject> buffetItems;

    //list of current items
    public List<GameObject> plateItems;

    //no of items to randomly create
    public int numOfItems;
    // Start is called before the first frame update
    void Start()
    {
        //generate our buffet items based on how many we set using noOfItems
        for(int i  = 0; i < numOfItems; i++)
        {
            //add items to the list.
            buffetItems.Add(defaultFoodPrefab);
            //(float)(int/int)
            
        }
        //select something to put on our plate.
        //taking our list "Buffetitems" and getting a random item using random.range.
        //Random.Range has two values , amin and a max, 0 is the start of the array and count gives us the max eclusive of the last value.
        AddToPlate(buffetItems[Random.Range(0, buffetItems.Count)]);
        AddToPlate(buffetItems[Random.Range(0, buffetItems.Count)]);
        AddToPlate(buffetItems[Random.Range(0, buffetItems.Count)]);
        AddToPlate(buffetItems[Random.Range(0, buffetItems.Count)]);
        // moved here so it only runs once
        StartCoroutine(Eating());
    }
    void AddToPlate(GameObject foodToAdd)
    {
        //Step 1.spawn in the item
        //you can replace transform with a set position  to make a spawn location for the object in the scene.
        GameObject spawnedFood = Instantiate(foodToAdd, transform);
        //Step 2.
        //cast to enum type using (typeof)
        //randomize the type of food.
        spawnedFood.GetComponent<FoodItem>().myType = (FoodItem.FoodTypes)Random.Range(0, 11);
        //set the name of the item to be the type of food it is.
        spawnedFood.name = spawnedFood.GetComponent<FoodItem>().myType.ToString();

        //step 3. Add item to plate.
        plateItems.Add(spawnedFood);
        //step 4. Eat the Food
        
    }

    //could also be called remove
    void Eat(GameObject foodToEat)
    {
        //remove given item from our list.
        plateItems.Remove(foodToEat);
        //Remove it from the level (cause we ate it!)
        Destroy(foodToEat);
    }

    IEnumerator Eating()
    {
        yield return new WaitForSeconds(5F);
        Debug.Log("Nom");
        Debug.Log(plateItems[0]);
        //remove item from our plate(first item)
        Eat(plateItems[0]);
        //go about adding new item to plate from the buffet.
        AddToPlate(buffetItems[Random.Range(0, buffetItems.Count)]);
        //put here, so we can do loop!
        StartCoroutine(Eating());
    }


}
