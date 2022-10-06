# Unity Object Pool

--------------------------------------------------------------------------------------  
## Usage 
 * Add `Object Pool` component to your scene.
 * Set pool `ID` , `Pool Size` and `Object Prefab` fields.
 * The Object prefab must have a class that inherits from `Pooled Object`.
 * Call the `Reset()` method from the Base class when the object needs to go back to the pool.

--------------------------------------------------------------------------------------  

| <img src="/.github/screenshots/ss00.png"> |  
|---|  

--------------------------------------------------------------------------------------  

## Object Calling

| method| Description |  
|---|---|
|` GetObject(string ID, Vector3 position, Quaternion quaternion) `| Calls the object with the given ID at the desired position and rotation. If the objects not available, creates new one |

--------------------------------------------------------------------------------------

![](/.github/screenshots/gif00.gif)
