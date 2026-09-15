# Peep — 3D Physics-Based Escape Game

A 3D physics-based escape game developed in Unity and C#, featuring interactive puzzles, physics-driven object interactions, and modular gameplay systems.

## Gameplay Demo

https://github.com/user-attachments/assets/5a78e97d-dec4-42de-86cc-82d0b88ed04c

## About

Peep is a 3D escape game where players explore an interactive environment, manipulate physics-based objects, and solve interconnected puzzles to progress through the game.

The project focuses on physics-driven interaction, environmental puzzle-solving, and modular gameplay systems.


## Key Features

- Physics-based object grabbing and manipulation
- Interactive environmental puzzles
- Player movement and first-person camera controls
- Interconnected puzzle and progression systems
- In-game clues and player feedback
- Modular gameplay architecture

## Technical Implementation

The project was developed in Unity using C#, with gameplay logic organised across approximately 30 scripts covering player systems, puzzles, progression, and feedback.

### Physics-Based Object Interaction

Peep uses Unity's physics system to support interactive object manipulation within the environment. Grabbable objects are detected through raycasting and connected to a temporary `ConfigurableJoint`, allowing them to be moved while remaining controlled by `Rigidbody` physics.

Doors use `Rigidbody` and `HingeJoint` components to support physics-based opening and closing. Locked doors restrict physical movement until the player uses the correct key, after which the door's physics behaviour is enabled.

### Puzzle & Progression Systems

The game includes several interconnected puzzle mechanics implemented through individual C# components. These include key-and-lock interactions, powered keypads, password-protected containers, sequence puzzles, rotatable pipe puzzles, environmental clues, and object-based interactions.

Puzzle completion can change the state of other objects in the environment. For example, inserting a battery restores scene lighting and powers a keypad, while completing the pipe puzzle unlocks a previously inaccessible object. This creates progression through interactions between multiple gameplay systems rather than relying on a single puzzle mechanic.

### Player & Camera System

Player movement is implemented using Unity's `CharacterController`, with camera-relative movement and first-person camera behaviour. Cinemachine is used to manage the player camera, while Unity's Input System handles player input.

### Interaction & Feedback

Raycasting is also used to identify interactable objects and provide contextual feedback to the player. Interaction prompts, object information, and clue interfaces help communicate when objects can be examined or used during puzzle solving.

## 3D Character Modelling

The main character, Peep, was designed and modelled in Blender before being imported into Unity for use in the game.

<img width="967" height="680" alt="image" src="https://github.com/user-attachments/assets/a63f3554-b182-4c07-9203-afcadca86e2e" />

## Level Design

<img width="798" height="650" alt="peep-game-level1" src="https://github.com/user-attachments/assets/1b63214a-5c89-4853-a149-45d897209c84" />

The main playable environment was assembled in Unity as an indoor escape-room setting containing multiple rooms and puzzle areas. The layout was structured around exploration and puzzle progression, with interactable objects, environmental clues, and gameplay triggers distributed throughout the environment.

## Testing & Evaluation

The game was tested with 10 participants to evaluate player movement, object interaction, puzzle progression, feedback, and overall usability.

Testing identified areas for improvement including occasional physics jitter, interaction feedback, and puzzle replayability.

Future evaluation could use a more structured game-testing methodology and quantitative visualisation of results.

## Technologies

- Unity
- C#
- Unity Physics
- Cinemachine
- Blender

## Project Context

**Individual Final Year Project**  
Bachelor of Science in Computer Science  
University of London
