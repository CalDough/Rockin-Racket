INCLUDE ../globals.ink

-> H1

=== H1 ===
= Kurt
#speaker: Kurt #portrait: kurt_speaking_normal #portrait: harvey_chill_normal
Oh, h-h-h-hey Harvey. Do you n-n-need something?
    * [Just checking in]
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: kurt_chill_normal
        Just checking in. Are you looking for something?
        
        -> K_H1_C1
    * [Yeah, I wanted to check...]
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: kurt_chill_normal
        Yeah, I wanted to check how your bass was doing? It looked a bit damaged from where I was standing.
        
        -> K_H1_C2
    * [End convo]
        -> END

= K_H1_C1
#speaker: Kurt #portrait: kurt_speaking_normal #portrait: harvey_chill_normal
Y-y-yeah, I am. I lost that l-l-little case I put my guitar p-p-picks in. I thought I left if on the shelf but it's n-n-not there anymore.
    * [That sucks]
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: kurt_chill_normal
        That sucks. I'm sure it's bound to show back up eventually.
        
        #speaker: Kurt #portrait: kurt_speaking_normal #portrait: harvey_chill_normal
        I know, I'm j-j-just stressed about it.
        
            -> END
    * [Have you thought about checking...]
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: kurt_chill_normal
        Have you thought about checking in with MJ? Maybe she took it?
        
        #speaker: Kurt #portrait: kurt_speaking_normal #portrait: harvey_chill_normal
        You have a p-p-point. Thanks!
        
            -> END

//Fix this section
= K_H1_C2
#speaker: Kurt #portrait: kurt_speaking_normal #portrait: harvey_chill_normal
Oh, n-no it's all good. I've j-just had it f-for a while. It w-was a gift from m-mom and dad a f-few Christmas's ago.
    * [Now I remember]
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: kurt_chill_normal
        Oh yeah, now I remember. You were so happy you were bouncing around the room.
        
        #speaker: Kurt #portrait: kurt_speaking_normal #portrait: harvey_chill_normal
        I w-was asking for it m-months in advance. I r-really wanted it. But, I m-may need a new one at s-some point befor it t-totally fives out on m-me.
        
            -> END
    * [Maybe I can...]
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: kurt_chill_normal
        Maybe I can buy you a new one here soon to replace it then. Surely if we hold enough concerts then we can save up the money for it.
        
        #speaker: Kurt #portrait: kurt_speaking_normal #portrait: harvey_chill_normal
        T-that would be awesome! I f-forgot that we c-can make money from our sh-shows.
        
            -> END