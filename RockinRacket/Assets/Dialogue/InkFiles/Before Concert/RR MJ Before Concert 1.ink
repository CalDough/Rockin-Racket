INCLUDE ../globals.ink

-> Before_Concert


=== Before_Concert ===

#speaker: MJ #portrait: harvey_chill_annoyed #portrait: mj_speaking_irritated
There you are, we've been waiting, like, FOREVER. Could you be any slower dude? Know what, forget it. 

#speaker: MJ #portrait: harvey_chill_normal #portrait: mj_speaking_normal
How do my clothes look? Not a wrinkle to be seen, right? You gotta be stylish for this kind of thing, first encounters and all, ya know?

    * [You look good]
        
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: mj_chill_irritated
        Yeah, totally. Your clothes look...good? Your hair could use a bit more work too, if I'm being honest.
            
            #speaker: MJ #portrait: harvey_chill_annoyed #portrait: mj_speaking_very-irritated
            So you were the wrong raccoon to ask about this, noted. I'll just go and ask Haley then. She'll agree with me, I just know it.
            
            -> END
            
    * [What about mine?]
        
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: mj_chill_normal
        What about my fit? Looks good right? I didn't have too many clean so it's all I had to work with. Still good though if I do say so myself.
            
            #speaker: MJ #portrait: harvey_chill_normal #portrait: mj_speaking_disgusted
            We really need to invest in a mirror for your room. Ain't no way you think you look good right now. 
            
            #speaker: MJ #portrait: harvey_chill_annoyed #portrait: mj_speaking_smug
            I hate to be the one to point it out, but you look kinda like roadkill right now, but you know, alive.
            
            -> END


