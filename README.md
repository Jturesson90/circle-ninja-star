# circle-ninja-star

## Setup
- Unity 2018.4.15f1
- Open the scene "SampleScene" in folder "_Scenes"

### Platforms
This game is developed mobile first portrait mode.
***

## Motivation
This project is experimenting with [Observable Pattern](https://gameprogrammingpatterns.com/observer.html) in a combination with [Scriptable Objects](https://docs.unity3d.com/Manual/class-ScriptableObject.html). Unite 2017 had an amazing talk about this, [Link here](https://www.youtube.com/watch?v=raQ3iHhE_Kk).

Instead of a life meter or life count, this project is using [Post-processing](https://docs.unity3d.com/Manual/PostProcessingOverview.html) to simulate taking damage and health. 

For the circles, this project is using realtime [Mesh](https://docs.unity3d.com/ScriptReference/Mesh.html) manipulation. 