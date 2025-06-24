# Code Review Report

## Course: CCL4 SS 2025 (5 ECTS, 3 SWS)
Student IDs: cc231052, cc231049, cc231055, cc231070

BCC Group: C, B, C, C

Names: Julia Hummer, Kaja Kitzmüller, Simon Koller, Zsanett Waldau

Your Project Name: INTE

Project Group: 9

### A Short Summary to Promote the Project (Background and Motivation of project):
With 2 weeks to develop a 3D application, we chose to challenge ourselves while still attempting tp be reasonable. 
This led us to two ideas, which we combined: Making a VR game and procedural dungeon. With a maze generator from a 
previous course already in our hands, we came up with INTE, our department store inspired maze.
Once we came up with this idea, the rest came naturally, our vision aligning. The goal became a game between a dream 
and a nightmare, something eerie yet familiar, with graphics reminiscent of old PS1 games.


### **Key Features and Implementation Detail**

#### 3D Modeling

For the Modeling we made 3 characters as well as various props. Initially, we wanted to emulate a PS1 style, meaning 
low-poly models with low-resolution textures. However, we ended up not liking the look of the textures, and decided to 
go with flat colors instead, which also created more cohesion with the third-party assets we used.

The characters are all modeled in that style, were UV unwrapped, and textured using 2 layers, one for the base color 
and one for the details (face, decals, paws, etc.). The goal in this process was to minimize face counts to optimize 
the performance of the graphics rendering, especially once animated. To this end, we also removed the body faces of the 
employees that would be hidden under the clothes, as well as some other faces that wouldn’t be visible. This made all 
of our characters sub 5000 faces, after triangulating (also done for better rendering).

We then rigged these characters using Mixamo. For the companion we wanted to invoke the feeling of a stuffed animal 
more, by making certain body parts react to movements in the main body in a flowy, uncontrolled manner, such as her 
head bobbing around or arms swinging. We made a longer idle animation for her, where she looks around the room, a 
walking animation full of spunk and a cheering animation when the player picks up a furniture piece in the level. The 
employees both have custom idle animations, the “Yapper” checking her nails and the “Stealer” warming up and preparing 
to run at any moment, while looking around to see if anything is amiss. We also used third party animations to 
supplement ours, specifically walking, running and talking animations.

For the props, we made a parasol, lawnchair, lamp, LACK Table and various sizes of Kallax models, as well as separate 
parts for the later two. We also made a dowel model and adapted an existing screw model. The parasol, lawn chair and 
lamp were also UV-unwrapped and textured using ucupaint. Here we first tried an approach where we also gave them normal, 
smoothness and metallic layers, to adhere to our original vision, but we decided to go for the flat colors for more 
consistency.

We created 2D assets for the UI elements in Adobe Illustrator. For the menu scenes we went with assets inspired by the 
style of IKEA manuals. For the UI elements in the clipboard menu, we chose to also apply a brush to the elements, to 
give it a hand drawn look.


#### Game Audio
We sourced our audio from two places: public samples on freesound.org and original recordings made by the team. Each 
member voiced one or more characters:
Zsanett – PA announcements, Yapper (Swedish), Bear Yrsa
Kaja – PA announcements, Yapper (English)
Simon – Stealer
Julia – Player

We aimed to create an authentic IKEA-like atmosphere by inserting announcements between the background music. For the 
companion character, we chose a warm and comforting tone to make her feel supportive and friendly. In contrast, the 
employee characters were designed to be more annoying and intrusive. The Stealer mainly produced random noises and 
mocking sounds, while the Yapper was intended to talk nonstop. When we came up with the idea to overlap English and 
Swedish dialogue, the perfect level of chaotic energy for the Yapper was achieved.

Most tracks were edited in Reaper before being imported into Wwise. We placed assets mainly in Random Containers, with 
Sequence Containers reserved for structured content such as PA system music and messages and Yrsa’s introductory 
dialogue.

To achieve proper spatialisation, we applied attenuation curves to match each character’s in-game location and created 
an RTPC for the PA so its level can be driven by code—a feature we’re particularly proud of. We generated separate 
soundbanks for each scene and also added a reverb effect to the warehouse scene.

In Unity, each scene loads its soundbank through the WwiseGlobal object. Wwise sound events were assigned by dragging 
them directly into code components in the Inspector, where we also specified the source and trigger objects. This 
setup allowed us to link Wwise events to in-game actions easily and maintain a clear structure for handling sound 
interactions.

#### Unity Coding
Our plan was to make it as abstract as possible so we created a lot of serialized fields to always be able to balance 
the game. The scripts are kept very small which unfortunately led to problems with the build Script execution order 
because I had circular dependencies.
Which is also why we had to change the script a couple of times but I think all in all it should now be fine.

**Scenes:**

Main Scene: Menu to start the tutorial scene or the first level.

Warehouse Scene: The Tutorial is in this scene where you learn how to move, the story and how you can interact with items.

Level Scene: This is the first level where you need to collect all the items and escape from the enemies to go to the Winscreen Scene.

Winscreen Scene: After winning the game you get to the Winscreen where you can get back to the menu or exit.

**Game Objects:**

Rooms:

In the first Level the Mazegenerator script creates a maze where the walls get replaced by the Room Prefab which gets 
initialized procedurally. Every room then needs to be rotated so that the player can enter it.

Collectable Items:

There are items which can be collected which get generated on random spawnpoints we create in every room on a certain 
location (for example on the bed). At the start of the game the PickupItems get generated and initialized in a random 
spawnpoint.
The goal of the game is to collect the items without dying to the yapper and also the stealer can take an item of your 
inventory and replace it on a random location again.

Enemies:

The enemies are also Prefabs which get initialized on a random corner tile after a certain time interval. For that we 
first had to create a NavMesh but because the maze gets generated procedurally this has to happen at buildtime. The 
enemies first destination is the player where they activate their action if they are in a certain radius (yapping or 
stealing). When the enemies are finished with their action they return to the [0,0,0] of the maze and get destroyed.
Yapping: The yapper is talking to you which disables you controls for a certain amount of time and you lose a life. 
After you have no hearts left it is game over.
Stealing: The stealer will take an item from your inventory and the items respawns in a random location.


Clipboard:

The clipboard is attached to the Player-Rigs left hand, you can check the list on it which items you still need to 
collect, your left hearts, and also return to the main menu if you click the button at the bottom right and then 
select exit.

Companion:

We implemented a Companion which we are very proud of. Her name is Yrsa, and she follows you around and cheers if 
you collect an item.



#### C# & Theory of Computer Graphics & Animation
Animation:

We have to say that the animation handling in Unity was very straightforward and innovative because you just had to 
change parameters.

C#:

Main feature within C# coding was our procedurally generated maze, which also automatically fills the maze walls with
randomised rooms, as well as spawning points for the objects, which are put in randomly as well after.

A very good thing with the maze was, that the code was just normal c# code which could be tested in the console 
so it was also easier to debug. We had a lot of advantages from the decoupled code from the unity project and were 
able to keep it abstract and also implement generics and inheritance which made it easy to extend if we wanted some 
specific new features.

#### Implementation Logic Explanation:
**Maze:**

We implemented a completely random maze generation with a bunch of different room type. For the maze, we used Kajas 
Code for the final project in C# where she implemented the Kruskal algorithm. We then changed the walls to be rooms 
instead which left us with a 3 by 3 grid with a path in the middle. To still have the option to balance this, we made 
it that every room can have different probabilities which also made it easier to test.

**Enemies:**

We have two enemies: the Yapper & Stealer.
These were implemented by using the Nav-Agent from Unity, the problem there was, that the navmesh normally is built 
and built time and not runtime which we had to find a workaround for.
The yapper and stealer have an remaining path distance which we used to implement the radius in which they can make 
their action (yapping or stealing).
This worked pretty good at first, the big problem just was that the new destination after their action took a couple of 
frames to refresh the remaining path which led to instantly disappearing enemies after their action.


### Three Important Achievements:
#### 1. Maze Generator:
One of our biggest achievements is the Maze Generator. While we had a basic script to generate a maze using the Kruskal 
Algorithm from C#, applying it to our needs here came with its own challenges. This was for example, making each cell a 
grid by itself, making sure the prefabs are loaded in properly (e.g. right rotation depending on location) and adding 
'probabilities to certain rooms.
#### 2. Clipboard UI:
The clipboard menu on the left hand controller was another big achievement of ours. Figuring out the logic of the 
inventory itself was already challenging, making the functionality to actually render it and all the information 
properly on the clipboard another. While we did get some help with the initial setup of the vertical layout group of 
the menu, no tutors or lecturers were available when we actually did the logic. And while there are still some things 
to improve, we are happy with our result, especially the ability to switch between pages.
#### 3. Implementing our recorded sounds:
We are really happy that we managed to record and implement our own dialog. This gave our game a more personalized 
feel and meant we could get way closer to our vision of the game. This allowed us to create some very unique sounds, 
for example the yapper, which is various voicelines repeating the same voiceline in both Swedish and English layed on 
top of each other, to give it an overwhelming feeling.


## Learned Knowledge from the Project
### Major Challenges and Solutions:

1. Not being able to use the same script for both employees because the Capsule Collider worked on the Stealer but not on 
the Yapper. The solution was to trigger the Talking Event on a certain distance to the Yapper, making me write a new 
seperate script for it.
2. setting up the vr Headset and prevent the blackscreen
   solution: reinstall meta quest link (was very demotivating and annoying)
3. use the remaining path of the navagents to activate their action because this needs a view frames to recalculate 
because we have a big navmesh after setting a new destination which caused the agents to disappear because the 
remaining path used the old destination
4. Understanding what to do with the Clipboard of the UI, because the logic was unclear in the beginning (aided by the
lack of sleep), especially because inventory was a completely new thing to us

### Minor Challenges and Solutions:
1. Making Yrsa have footstep Sounds because at first it seemed to only trigger the Event when the SIDES of her Character 
hit the floor. The solution then was just to know if she is Walking or not and then time the steps accordingly. 
2. Finding Fitting Sound assets online is like looking for a needle in a haystack sometimes. That is probably why the 
Collection of all sounds took way longer than expected. The solution was just to allocate more time to this as soon
as we noticed the issue, which we did in time, thankfully.
3. merge all the branches together, but this went very well most of the time, so we might have been lucky and just 
were careful enough 
4. Yrsa is still floating where i can not find out the reason why this happens 
5. The requirement with the persistent statemanager then caused some problems because we needed to reset every stat 
which then broke the game
6. Understanding when to stop with modeling and animations, since creative projects can always be better. The solution
was asking Zsanett and her deciding.

### Reflections on the Own Project (by all team members):
* Wwise is not that hard to set up and problems mostly source from Unity. I would have loved to create better 
fixes for each problem we had - especially the ones at the end.
* There is a list of features and functionality I had in the vision for the project that were not feasible to fix
in time. I learned that while I focus on MVP in projects generally, it is hard to make an MVP for a game the same way,
as a games minimum viability includes the game loop being interesting, fun and the visuals communicating the idea.
This means a lot bigger of an MVP. The functionalities I would like to add, I have in a prioritised list, but some of
the main ones are: safe zone at the end of the level and building furniture by hand (this is what would have added
more to the VR specific experience), the third enemy type, and more.
* I did not realise how much time disappears to the combination role of game designer, product owner and manager,
which meant that I had to delegate a lot of tasks that I initially thought I would have time for. I would love to 
continue work on all the parts I am interested in working on, like with animation, the sceneswitching, the in game UI,
and the logic.
* Vr programming in general was nice but the buggs with the Headset made it very exhausting and hard to progress. 
I would also make the enemies more abstract and use Delegation to easily implement more behaviours to them.
* I would like to improve the soundscape, the music playing, and the lighting and environment to fit the idea I had 
more.
* There is a lot about this project I would change, and like to improve, but what is there makes me excited to sit down
again with it in the future.

### Credits

#### Main Responsibility Division

###### **Julia**

Main responsibility: Audio Engineering
Other: Recording, wwise integration with Unity, debugging, voice, dialogue writing

###### **Simon**
Main responsibility: C# & Coding CG&A, Unity Functionality
Other: Animation support/start, voice, footage, VR setups

###### **Kaja**
Main responsibility: Modelling, Texturing & Animation
Other: Coding, Debugging, UI within Unity, hifi mockups in figma, graphic assets creation (logo, furniture UI, 
menu items), voice, promotion

###### **Zsanett**
Main responsibility: Game Designer (cohesiveness, design, mechanics, decisions), Product Owner → Manager 
(Organisation…git, kanban, meetings, documentation (website/portfolio, readme, promotion), deadlines, organising help, 
time plan) & UI (clipboard, handmenu design & setup, logic)
Other: Recording & ‘Coaching’ for it, Coding, Debugging, Animation (Yapper Idle), voice, dialogue writing

###### **Voices:**
Player: July

Yrsa, the bear: Zsanett

Yapper, english: Kaja

Yapper, swedish, Zsanett

Stealer: Simon

**Music:** Never Gonna Give You Up - Rick Astley, Wake Me Up Before You Go-Go - Wham!
