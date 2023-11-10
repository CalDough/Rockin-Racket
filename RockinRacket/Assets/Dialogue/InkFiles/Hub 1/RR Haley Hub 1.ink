INCLUDE ../globals.ink

-> H1

=== H1 ===
= Haley
#speaker: Haley #portrait: haley_speaking_normal
#speaker: Harvey #portrait: harvey_chill_normal
Need something Harv?
 * [Yeah, I was wondering...] 
        #speaker: Haley #portrait: haley_chill_normal
        #speaker: Harvey #portrait: harvey_speaking_normal
        Yeah, I was wondering if I did alright during that performance? I feel like it was bad, but the crowd seemed to like it so I'm confused.
        
        -> H_H1_C1
            
 * [I wanted check in on...]
        #speaker: Haley #portrait: haley_chill_normal
        #speaker: Harvey #portrait: harvey_speaking_normal
        I wanted check in on your equipment. Everything in working order?
        
        -> H_H1_C2
 * [End convo]
        -> END

= H_H1_C1
#speaker: Haley #portrait: haley_speaking_normal
#speaker: Harvey #portrait: harvey_chill_normal
Don't worry about it. It was your first time really managing everything so of course it felt a bit rough.
    * [Thank goodness]
        #speaker: Haley #portrait: haley_chill_normal
        #speaker: Harvey #portrait: harvey_speaking_normal
        Thank goodness. I was freaking out over here because I thought I did bad.
        
            -> END
    * [When you put it that way...]
        #speaker: Haley #portrait: haley_chill_normal
        #speaker: Harvey #portrait: harvey_speaking_normal
        When you put it that way, it makes a lot of sense. I'll probably look back on this and laugh about it in the future.
        
            -> END

//Fix this section
= H_H1_C2
#speaker: Haley #portrait: haley_speaking_normal
#speaker: Harvey #portrait: harvey_chill_normal
Well considering it's mainly just my voice that I have to deal with, I think I'm all good. Maybe a new mic would be nice though? It's always good to have back-ups.
    * [I'll buy some stuff...]
        #speaker: Haley #portrait: haley_chill_normal
        #speaker: Harvey #portrait: harvey_speaking_normal
        Maybe I'll buy some stuff to help with your throat? Don't want it to harm you ability to sing after all.
        
        #speaker: Haley #portrait: haley_speaking_normal
        #speaker: Harvey #portrait: harvey_chill_normal
        If you really want to then that would be awesome! I do plenty of vocal exercises too to keep everything in tip top shape, so it's not a huge deal if you can't get anything.
        
            -> END
    * [Sounds like a plan]
        #speaker: Haley #portrait: haley_chill_normal
        #speaker: Harvey #portrait: harvey_speaking_normal
        That sounds like a plan to me. If the one you uses breaks mid song, then having another one you could switch to would be best.
        
        #speaker: Haley #portrait: haley_speaking_normal
        #speaker: Harvey #portrait: harvey_chill_normal
        I really hope that doesn't happen mid song though. That would be traumatizing. But yes, back-ups in case that happens would be great. Or maybe I could switch to them and the older one could be my back up? I'll figure it out later.
        
            -> END



-> END