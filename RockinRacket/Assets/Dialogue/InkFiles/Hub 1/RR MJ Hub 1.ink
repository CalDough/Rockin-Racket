INCLUDE ../globals.ink

-> H1

=== H1 ===
= MJ

#speaker: MJ #portrait: harvey_chill_normal #portrait: mj_speaking_normal
What do you need? I'm busy.
    * [Sorry to interrupt...]
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: mj_chill_normal
        Oh sorry. I just heard you playing and wanted to listen more.
        
        -> M_H1_C1
    * [How's your guitar?]
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: mj_chill_normal
        How's your guitar doing? Do you need anything for it?
        
        -> M_H1_C2
    * [End convo]
        -> END

= M_H1_C1

#speaker: MJ #portrait: harvey_chill_normal #portrait: mj_speaking_normal
Yeah, I'm practicing some songs.
    * [Practicing on a Sunday?]
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: mj_chill_normal
        Practicing on a Sunday tho? Don't you want to rest?
        
        
        #speaker: MJ #portrait: harvey_chill_normal #portrait: mj_speaking_normal
        Nah, rest is for the weak. And I'm not weak.
        
            -> END
    * [How about taking...]
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: mj_chill_normal
        How about taking a break?
        
        
        #speaker: MJ #portrait: harvey_chill_normal #portrait: mj_speaking_normal
        I'll take a break when I'm done. Can I get back to it now?
        
            -> END

= M_H1_C2

#speaker: MJ #portrait: harvey_chill_normal #portrait: mj_speaking_normal
It seems like it's holding up pretty well right now. But I would love a new one if you want to get me one!
    * [That's good]
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: mj_chill_normal
        Well that's good that it's still working. Let me know if that changes at all.
        
        
        #speaker: MJ #portrait: harvey_chill_normal #portrait: mj_speaking_normal
        Deal.
        
            -> END
    * [Maybe later on...]
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: mj_chill_normal
        Maybe later on I can get you a new one. Having a new one isn't gonna hurt at all.
        
        
        #speaker: MJ #portrait: harvey_chill_normal #portrait: mj_speaking_normal
        You have a point there. Having them line the walls of my room would be great.
        
            -> END


