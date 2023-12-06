INCLUDE ../globals.ink

-> H1

=== H1 ===
= Haley

#speaker: Haley #portrait: harvey_chill_normal #portrait: haley_speaking_normal
You need something Harv?
 * [Yeah, I wanted to apologize...] 
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: haley_chill_normal
        Yeah, I wanted to apologize for almost missing the gig. That was my bad.
        
        -> H_H1_C1
            
 * [I wanted check in on...]
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: haley_chill_normal
        I wanted check in on your mic and stuff. Everything good now?
        
        -> H_H1_C2
 * [End convo]
        -> END

= H_H1_C1

#speaker: Haley #portrait: harvey_chill_normal #portrait: haley_speaking_normal
Oh don't worry about it so much. It's not like it was the end of the world or anything. Just don't let it happen again or we're all gonna be mega upset.
    * [Thank goodness]
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: haley_chill_normal
        Thank goodness. I was worried you were going to be mad at me about it.
        
            -> END
    * [I get what you mean]
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: haley_chill_normal
        Got it! I can do that, I swear. I'll try to be the best manager you ever did see!
        
            -> END

//Fix this section
= H_H1_C2
#speaker: Haley #portrait: harvey_chill_normal #portrait: haley_speaking_normal
Well considering it's mainly just my voice that I have to deal with, I think I'm all good. Maybe a new mic would be nice though? It's always good to have back-ups.
    * [I'll buy some stuff...]
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: haley_chill_normal
        Maybe I'll buy some stuff to help with your throat? It'd be kinda awkward if the vocalist lost her voice, ya know?
        
        
        #speaker: Haley #portrait: harvey_chill_normal #portrait: haley_speaking_normal
        I would FLIP the fluff out if that happened. Not to mention that I'd get an earful from MJ for the rest of my life about it. 
        
            -> END
    * [Sounds like a plan]
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: haley_chill_normal
        That sounds like a plan to me. And if not, I'm sure it could be a quick fix for your mic if something were to go wrong while playing.
        
        
        #speaker: Haley #portrait: harvey_chill_normal #portrait: haley_speaking_normal
        I really hope that doesn't happen mid song tho. That would be TRAUMATIZING!!. We'll just see what happens and hope nothing goes wrong with any of my mics. Cross your claws!
        
            -> END


