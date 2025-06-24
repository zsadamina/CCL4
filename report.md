# Code Review Report

## Course: CCL4 SS 2025 (5 ECTS, 3 SWS)
Student IDs: cc231052, cc231049, cc231055, cc231070
BCC Group: C, B, C, C
Names: Julia Hummer, Kaja Kitzmüller, Simon Koller, Zsanett Waldau

Your Project Name: INTE
Project Group: 9

A Short Summary to Promote the Project (What are the Background and the Motivation of the project?):
(Approx. 100 words)

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



#### C# & Theory of Computer Graphics & Animation



#### Implementation Logic Explanation:
Maze:

We implemented a completely random maze generation with a bunch of different room type. For the maze, we used Kajas 
Code for the final project in C# where she implemented the Kruskal algorithm. We then changed the walls to be rooms 
instead which left us with a 3 by 3 grid with a path in the middle. To still have the option to balance this, we made 
it that every room can have different probabilities which also made it easier to test.

Enemies:

We have two enemies:
Yapper
Stealer

These were implemented by using the Nav-Agent from Unity, the problem there was, that the navmesh normally is built 
and built time and not runtime which we had to find a workaround for.
The yapper and stealer have an remaining path distance which we used to implement the radius in which they can make 
their action (yapping or stealing).
This worked pretty good at first, the big problem just was that the new destination after their action took a couple of 
frames to refresh the remaining path which led to instantly disappearing enemies after their action.


Three Important Achievements:
(List down and explain 3 important achievements you are proud of (e.g., features, techniques, etc.) in the project. Please explain in detail.) 1. Item 2. Item, and so forth

Learned Knowledge from the Project
Major Challenges and Solutions:
(List down and explain the major challenges. Did you solve it? How? Please explain in detail.) 1. Item 2. Item, and so forth

Minor Challenges and Solutions:
(List down and explain the minor challenges. Did you solve it? How? Please explain in detail.) 1. Item 2. Item, and so forth

Reflections on the Own Project:
(List down and explain what you could improve and add if you have more time.) 1. Item 2. Item, and so forth