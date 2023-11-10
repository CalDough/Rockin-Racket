INCLUDE ../globals.ink

-> H1

=== H1 ===
= Ace
#speaker: Ace #portrait: ace_speaking_normal
#speaker: Harvey #portrait: harvey_chill_normal
What up dude? 
    * [Not much]
        #speaker: Ace #portrait: ace_chill_normal
        #speaker: Harvey #portrait: harvey_speaking_normal
        Not much, just wanted to see what you were doing.
        
        -> A_H1_C1
    * [About your drums...]
        #speaker: Ace #portrait: ace_chill_normal
        #speaker: Harvey #portrait: harvey_speaking_normal
        How are your drums still producing the right sounds? It looks really beat up.
        
        -> A_H1_C2
    * [End convo]
        -> END

= A_H1_C1
#speaker: Ace #portrait: ace_speaking_normal
#speaker: Harvey #portrait: harvey_chill_normal
Oh, I'm watching TV. Wanna join?
    * [But it's off]
        #speaker: Ace #portrait: ace_chill_normal
        #speaker: Harvey #portrait: harvey_speaking_normal
        But it's off, how are you watching it?
        
        #speaker: Ace #portrait: ace_speaking_normal
        #speaker: Harvey #portrait: harvey_chill_normal
        I'm imagining the episodes instead. I can explain it to you if you want?
        
        #speaker: Ace #portrait: ace_chill_normal    
        #speaker: Harvey #portrait: harvey_speaking_normal
        You know what, I'll pass.
            
            -> END
    * [I might later...]
        #speaker: Ace #portrait: ace_chill_normal
        #speaker: Harvey #portrait: harvey_speaking_normal
        I might later, when there's actually something on.
        
        #speaker: Ace #portrait: ace_speaking_normal
        #speaker: Harvey #portrait: harvey_chill_normal
        Understandable. Just let me know if you do so I can make some room for you.
        
            -> END

//Fix this section
= A_H1_C2
#speaker: Ace #portrait: ace_speaking_normal
#speaker: Harvey #portrait: harvey_chill_normal
Honestly, no clue. I put tape on it whenever I can and that seems to have worked.
    * [You need a new one]
        #speaker: Ace #portrait: ace_chill_normal
        #speaker: Harvey #portrait: harvey_speaking_normal
        You definitely need a new one then. One that's shiny and attractive.
        
        #speaker: Ace #portrait: ace_speaking_normal
        #speaker: Harvey #portrait: harvey_chill_normal
        I would love a shiny new one if you could get it. New drums are just always the sickest things ever.
        
            -> END
    * [TAPE!?]
        #speaker: Ace #portrait: ace_chill_normal
        #speaker: Harvey #portrait: harvey_speaking_normal
        You're telling me that those drums are being held together by TAPE!? That's an accident waiting to happen.
        
        #speaker: Ace #portrait: ace_speaking_normal
        #speaker: Harvey #portrait: harvey_chill_normal
        Hey man, you may not have faith in them, but I do. My drums are a well oiled machine in my mind. Sure, I may need a new drumset eventually, but for now it works great.
        
            -> END