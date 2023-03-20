import mediapipe as mp
import cv2
from dollarpy import Recognizer,Template,Point
import os
import numpy as np

mp_drawing = mp.solutions.drawing_utils 
mp_holistic = mp.solutions.holistic 
templates=[]


def getpoints(videoURL,label):
    cap=cv2.VideoCapture(videoURL)
    with mp_holistic.Holistic(min_detection_confidence=0.5,min_tracking_confidence=0.5) as holistic:
        points=[]
        left_eye_inner=[]
        left_eye=[]
        left_eye_outer=[]
        right_eye_inner=[]
        right_eye=[]
        right_eye_outer=[]
        indlist=[1,2,3,4,5,6,7]
        
        
        while cap.isOpened():
            
         ret,frame=cap.read()
         if ret==True:
             image=cv2.cvtColor(frame,cv2.COLOR_BGR2RGB)
             image.flags.writeable=False
             #Make detections
             results=holistic.process(image)
             image.flags.writeable=True
             image=cv2.cvtColor(image,cv2.COLOR_RGB2BGR)
             
             mp_drawing.draw_landmarks(image, results.face_landmarks, mp_holistic.FACEMESH_TESSELATION, 
                                 mp_drawing.DrawingSpec(color=(80,110,10), thickness=1, circle_radius=1),
                                 mp_drawing.DrawingSpec(color=(80,256,121), thickness=1, circle_radius=1)
                                 )
             
              # 2. Right hand
             mp_drawing.draw_landmarks(image, results.right_hand_landmarks, mp_holistic.HAND_CONNECTIONS, 
                                 mp_drawing.DrawingSpec(color=(80,22,10), thickness=2, circle_radius=4),
                                 mp_drawing.DrawingSpec(color=(80,44,121), thickness=2, circle_radius=2)
                                 )

            # 3. Left Hand
             mp_drawing.draw_landmarks(image, results.left_hand_landmarks, mp_holistic.HAND_CONNECTIONS, 
                                 mp_drawing.DrawingSpec(color=(121,22,76), thickness=2, circle_radius=4),
                                 mp_drawing.DrawingSpec(color=(121,44,250), thickness=2, circle_radius=2)
                                 )

             # 4. Pose Detections
             mp_drawing.draw_landmarks(image, results.pose_landmarks, mp_holistic.POSE_CONNECTIONS, 
                                 mp_drawing.DrawingSpec(color=(245,117,66), thickness=2, circle_radius=4),
                                 mp_drawing.DrawingSpec(color=(245,66,230), thickness=2, circle_radius=2)
                                 )
             
             
             
             #Extract keypoints
             
             try:
                 face=results.face_landmarks.landmark
                 index=0
                 newlist=[]
                 for lnd in face:
                     
                     if(index in [1,2,3,4,5,6,7]):
                         
                            newlist.append(lnd)
                         
                     index +=1
                         
                 left_eye_inner.append(Point(newlist[0].x,newlist[0].y,1))
                 left_eye.append(Point(newlist[1].x,newlist[1].y,2))
                 left_eye_outer.append(Point(newlist[2].x,newlist[2].y,3))
                 right_eye_inner.append(Point(newlist[3].x,newlist[3].y,4))
                 right_eye.append(Point(newlist[4].x,newlist[4].y,5))
                 right_eye_outer.append(Point(newlist[5].x,newlist[5].y,6))
                         
             
             except:
                 pass
                 
             cv2.imshow(label,image)
        
         if cv2.waitKey(1) & 0xFF == ord('q'):
            
                break
        cap.release()
        cv2.destroyAllWindows()
        points= left_eye_inner+left_eye+left_eye_outer+right_eye+right_eye_inner+right_eye_outer
        print(label)
        print(points)
        return points


#straight look
vid="straight.mp4"
points=getpoints(vid,"straight")
tmpl_2=Template('straight',points)
templates.append(tmpl_2)


#right look
vid="C:/Users/START/OneDrive/Desktop/Hci/right.mp4"
points=getpoints(vid,"right")
tmpl_2=Template('right',points)
templates.append(tmpl_2)


#left look
vid="C:/Users/START/OneDrive/Desktop/Hci/left.mp4"
points=getpoints(vid,"left")
tmpl_2=Template('left',points)
templates.append(tmpl_2)


#down look
vid="C:/Users/START/OneDrive/Desktop/Hci/down.mp4"
points=getpoints(vid,"down")
tmpl_2=Template('down',points)
templates.append(tmpl_2)

#up look




#Test

cap="filename.avi"
points=getpoints(cap,"should be right")



import time
start=time.time()
recognizer=Recognizer(templates)
result=recognizer.recognize(points)
end=time.time()
duration=end-start
print(result)
print(duration)