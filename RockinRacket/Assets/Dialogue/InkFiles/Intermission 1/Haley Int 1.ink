INCLUDE ../globals.ink

-> Convo

=== Convo ===

#speaker: Haley #portrait: harvey_chill_normal-alt #portrait: haley_speaking_happy
Hey Harv, you getting the hang of it now?
    * [Yeah!]
    
        #speaker: Harvey #portrait: harvey_speaking_ambitious #portrait: haley_chill_woah
        Yeah, actually, I am. It's really fun!
            -> C1

    * [I'm struggling to.]
    
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: haley_chill_concerned
        It's actually a bit challenging right now. So many things going on and all.
            -> C2

= C1

#speaker: Haley #portrait: harvey_chill_intrigued #portrait: haley_speaking_woah-alt1
REALLY!? I figured you would have had a harder time adjusting to it. THIS IS SO EXCITING!!!

    * [No way.]
    
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: haley_chill_woah
        I admit that there's a bit of a learning curve to it, but it's not a problem at all.
        
        
        #speaker: Haley #portrait: harvey_chill_normal-alt #portrait: haley_speaking_happy
        That's great tho! We needed someone who could do it all anyway. Keep it up Harv!
            -> END
    
    * [Did you really?]
    
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: haley_chill_normal
        You really didn't think I'd get the hang of it this fast? So little faith in me. Tsk tsk tsk.
        
        
        #speaker: Haley #portrait: harvey_chill_normal-alt #portrait: haley_speaking_normal-alt2
        OH SHUT IT, you know that's not what I meant. Your reputation and reliance isn't all that good so I wasn't sure how you would do. 
        
        #speaker: Haley #portrait: harvey_chill_ambitious #portrait: haley_speaking_intrigued
        Just keep proving me wrong if you got an issue with that!
            ->END


= C2

#speaker: Haley #portrait: harvey_chill_normal-alt #portrait: haley_speaking_normal-alt2
Just take charge and stay strong. That's how I did it when we started out. You got this, I know you do.

    * [Are you sure?]
    
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: haley_chill_huh
        You really think that I can handle it all?
        
          
        #speaker: Haley #portrait: harvey_chill_normal-alt #portrait: haley_speaking_happy
        Of course!! Just have faith in yourself!
            ->END

    * [What if I mess up?]
    
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: haley_chill_normal-alt
        What about if I screw it all up? The concert would be ruined.
        
        
        #speaker: Haley #portrait: harvey_chill_intrigued #portrait: haley_speaking_normal-alt1
        You're NOT allowed to talk like that. There will be no Debbie-Downers in this family if I can help it. Just get out there and do your best!
            -> END

