# BoardUnitFight

**Task description**

Create a 2D game scene made of a grid with a size 6 x 8. There should be a 'Start fight' button, after which a random count of units of Team 1 appear on the left, and Team 2 units appear on the right.
Units are fighting by themselves until only one team survives (or none of them - a draw).
Units should have health bars.
Units can move in any direction for 1 cell.
Units should attack opponent units that are within 1 cell. The attack can be done in any direction.

It is not allowed to use ready assets for this task.

**Implementation**

* Generatable field of 8 by 6 cells (size can be changed in the component on the prefab)
* Event system is used
* 2 fighting teams - you can set the color, name, number of units, and the spawn zone
* 2 UI screens - Main Menu / Level Complete (start button and victory text), Gameplay (health bars)
* Units are using multiple components and have additional settings, such as:
  * Direction of movement: straight, diagonal, both
  * Direction of attack: straight, diagonal, both
  * Maximum health, attack damage
* If there is an obstacle in front of the unit, he will try to go around it.
* All units are updated at a certain frequency - as a result of a battle between teams, a draw may occur
* Scene reloading is not used to restart the game - old objects are deleted
* In a real project, it would not be worth using Destroy, but instead use the same objects in the next battle for the second time
* The game was tested on Mid-range Android - Google Nexus 5, and showed 60 fps (in general, the game itself is elementary, so there should be no problems)
