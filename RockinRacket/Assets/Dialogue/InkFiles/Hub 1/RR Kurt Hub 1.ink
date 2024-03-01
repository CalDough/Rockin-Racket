INCLUDE ../globals.ink

-> H1

=== H1 ===
= Kurt

#speaker: Kurt #portrait: harvey_chill_normal #portrait: kurt_speaking_normal-alt
Oh, h-hey Harvey. Do you need something?
    * [Just checking in]
        
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: kurt_chill_confused
        Just wanted to see what you got going on. Are you looking for something?
        
        -> K_H1_C1
    * [Yeah, I wanted to check...]
        
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: kurt_chill_blank
        How's the bass holding up? It looked a bit banged up from where I was standing.
        
        -> K_H1_C2
    * [End convo]
        -> END

= K_H1_C1

#speaker: Kurt #portrait: harvey_chill_intrigued #portrait: kurt_speaking_sad
Yeah, I am. I have a little c-case that mom got me that I've been putting my guitar p-picks in. I lost it somewhere.
    * [That sucks]
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: kurt_chill_hurt
        Awe, dang. I'm sorry bro. I'm sure it's bound to show back up eventually!
        
        
        #speaker: Kurt #portrait: harvey_chill_normal-alt #portrait: kurt_speaking_sad
        I know, It's just stressful that I can't f-find it right now.
        
            -> END
    * [Have you thought about checking...]
        
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: kurt_chill_confused
        How about you ask MJ? Could she have taken it?
        
        
        #speaker: Kurt #portrait: harvey_chill_normal #portrait: kurt_speaking_normal
        Oh! Maybe she d-did. I'll check with her later.
        
            -> END

//Fix this section
= K_H1_C2

#speaker: Kurt #portrait: harvey_chill_normal-alt #portrait: kurt_speaking_normal-alt
Oh, n-no it's all good. I've just had it for a while. It w-was a Christmas gift a f-few years ago.
    * [Now I remember]
        
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: kurt_chill_happy
        Oh yeah, now I remember. You were so happy you were bouncing around the room!
        
        
        #speaker: Kurt #portrait: harvey_chill_normal-alt #portrait: kurt_speaking_happy
        Well yeah, I r-really wanted it! But, I may need a new one at some point before it t-totally dies out on me.
        
            -> END
    * [Maybe I can...]
        
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: kurt_chill_normal
        Maybe I can buy you a new one here soon to replace it then. Surely if we hold enough concerts then we can save up the money for it.
        
        
        #speaker: Kurt #portrait: harvey_chill_ambitious #portrait: kurt_speaking_normal
        That would be awesome! We just gotta do well enough then.
        
            -> END
            

