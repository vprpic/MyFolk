# MyFolk
This is a challenge project I worked on for a month. Inspired by The Sims and the upcoming game Paralives.
The following sections show the main systems implemented in the game.

## Furniture Mesh Edit
I wanted to recreate the [Paralives](https://youtu.be/qEHtQcbAmAQ) system of editing the furniture sizes in game. The first step is creating the object in Blender.

### Blender
After creating a basic shape the shape keys are added which determine a certain value of the item. To the created table I've added the next values: depth, height, width, thickness and condition.

<img src="https://i.imgur.com/SJdSupq.gif" style="width: 50%; height: auto;">

### Unity
The Blender shape keys can be used the same way after importing into Unity, but in Unity they are called Blend Shapes.

<img src="https://i.imgur.com/XTQHjeB.gif" style="width: 50%; height: auto;">

After modifying the mesh we get to an issue of the non modified mesh collider, so we have to modify it as well.

<img src="https://i.imgur.com/YGPJDFu.gif" style="width: 50%; height: auto;">

## Game Modes
There are three game modes: The menu, Play mode and Build mode. They represent different states of the game, display different UI elements, and pause the game when needed.

<img src="https://i.imgur.com/jgSrf2F.gif" style="width: 50%; height: auto;">

## Radial Menu
As in The Sims, the radial menu displays buttons for actions a character can take with certain object in the game.

<img src="https://i.imgur.com/mWi7Ld2.gif" style="width: 50%; height: auto;">

## Character Needs
Each character has their own needs that decay at different rates, depending on the type of need. The UI displaying the needs for the selected character gets notified for each change so it can update and gradually change the color.

<img src="https://i.imgur.com/b6nnLgQ.gif" style="width: 50%; height: auto;">

## Character Interactions
Character interactions represent interactions characters can have with objects in the game. They consist of actions that are performed sequentially. Each object has its own set of interactions, but the interactions can also be used on other objects.
The following subsections describe several possible interactions.

### Movement
Clicking on an object that is set as walkable will show the option "Walk here" in the radial menu after being clicked on.
Movement is accomplished with a combination of a Nav Mesh Agent (which does the pathfinding), and animation movement (which actually moves the character in the space so that the character doesn't glide on the ground).
In the gif below the simulation speed is increased so the character would move faster.

<img src="https://i.imgur.com/81ka4wz.gif" style="width: 50%; height: auto;">

### Picking up and putting items down
Clicking on smaller items that are set as carriable will show the option "Pick up" in the radial menu after being clicked on.
Each carriable item can be carried in one hand or two hands. So the character can carry two smaller items at the same time. Also the item has a placement type which determines if the item can be placed on horizontal, vertical surface, or both.
After picking up an item clicking on any interactable object that has the same placement type as the item will show an additional button in the radial menu for placing the object down on the spot that was clicked.

<img src="https://i.imgur.com/c8rBHrs.gif" style="width: 50%; height: auto;">

### Sleep Interaction
Sleeping is an interaction that consists of two actions.
The first action is walking to a interaction point. The interaction point is a predesignated point added to the interactable item that the character has to walk to in order to interact with the item. Some items don't need an interaction point so the character can approach that item from any side.
The second action is sleeping, which increases the Energy need.

<img src="https://i.imgur.com/gJ8Qeki.gif" style="width: 50%; height: auto;">

### Get Food Interaction
Get Food is an interaction that consists of four actions. Walking to the interaction point, looking at the fridge, opening the fridge, and eating.

<img src="https://i.imgur.com/C2Y3ACa.gif" style="width: 50%; height: auto;">
