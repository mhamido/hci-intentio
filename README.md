# Intentio

This project implements the Trail Making Test, a popular psychological test for measuring visual attention, as a game-based platform primarily target at children. 

The system uses TUIO interactive markers as a controller, but the mouse can also be used an alternate form of input.

The game visual, and audible feedback to redirect attention when lost, using pose estimation and hand tracking through the use of a camera, MediaPipe and a classifier to detect inattentiveness.

The system measures various factors such as time taken to complete the test, the number of times attention was lost during the session, and creates personalized sessions for each user using a Bluetooth identification device to separate each user's progression and associated context.

External users, particularly parents and therapists, can use the system to monitor and assess the progression of treatment.

# Demos

https://github.com/mhamido/hci-intentio/blob/master/Demo/Gameplay.mp4
https://github.com/mhamido/hci-intentio/blob/master/Demo/Demo-TUIO.mp4
https://github.com/mhamido/hci-intentio/blob/master/Demo/ParentDashboard.mp4

# License
This project is licensed under the MIT License. See LICENSE.md for more details.
