# Peep — 3D Physics-Based Escape Game

A 3D physics-based escape game developed in Unity and C#, where players explore rooms, interact with objects, and solve puzzles to escape.

## Gameplay Demo

https://github.com/user-attachments/assets/5a78e97d-dec4-42de-86cc-82d0b88ed04c

## About

Peep is a 3D escape game where players explore different rooms, interact with physics-based objects, find clues and solve puzzles to progress through the level.


## Key Features

- Physics-based object grabbing and manipulation
- Environmental puzzles involving objects, clues, locks, and doors
- First-person player movement and camera controls
- Connected puzzles where solving one can affect another part of the level
- In-game interaction prompts and clues

## Technical Implementation

Peep was developed in Unity using C#, with around 30 scripts used to handle player controls, object interactions, puzzles, progression, and feedback.

### Physics-Based Object Interaction

Raycasting is used to detect objects that the player can pick up and interact with. When an object is grabbed, a temporary `ConfigurableJoint` is used to move it while still allowing it to behave as a physics object through its
`Rigidbody`.

Doors are also physics-based, using `Rigidbody` and `HingeJoint` components for opening and closing. Locked doors cannot be opened until the player finds and uses the correct key.

### Puzzle & Progression Systems

Different C# scripts are used to handle the puzzles throughout the game, including keys and locks, powered keypads, password-protected containers, book sequence puzzles, rotatable pipe puzzles, and environmental clues.

Some puzzles affect to other part of the level. For example, inserting a battery turns the lights back on and powers a keypad, while completing the pipe puzzle unlocks a another interactable object. This allows puzzles to build on one
another as the player progresses through the level.

### Player & Camera System

Player movement is implemented using Unity's `CharacterController` and Input System. Movement follows the direction of the camera with Cinemachine used for the first-person camera.

### Interaction & Feedback

Raycasting is used to check what the player is looking at and whether it can be interacted with. Interaction prompts, object information, and clue interfaces are shown when needed to help the player understand what they can examine, pick up or use.

## 3D Character Modelling

The main character, Peep, was designed and modelled in Blender before being imported into Unity for use in the game.

<img width="967" height="680" alt="image" src="https://github.com/user-attachments/assets/a63f3554-b182-4c07-9203-afcadca86e2e" />

## Level Design

<img width="798" height="650" alt="peep-game-level1" src="https://github.com/user-attachments/assets/1b63214a-5c89-4853-a149-45d897209c84" />

The main level was assembled in Unity and consists of several connected rooms, each containing different puzzles and interactable objects. Clues and puzzle elements were placed throughout the rooms to encourage the player to explore.

## Testing & Evaluation

The game was tested with 10 participants to evaluate player movement, object interaction, puzzle progression, feedback, and overall usability.

Testing identified areas for improvement including occasional physics jitter, interaction feedback, and puzzle replayability.

If developed further, I would use a more structured testing process and collect more measurable feedback from players.

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
