INCLUDE ../globals.ink

-> H1

=== H1 ===
= MJ
#speaker: MJ #portrait: mj_speaking_normal
#speaker: Harvey #portrait: harvey_chill_normal
What do you need? I'm busy.
    * [Sorry to interrupt...]
        #speaker: MJ #portrait: mj_chill_normal
        #speaker: Harvey #portrait: harvey_speaking_normal
        Sorry to interrupt, I just wanted to see what you got going on.
        
        -> M_H1_C1
    * [How's your guitar?]
        #speaker: MJ #portrait: mj_speaking_normal
        #speaker: Harvey #portrait: harvey_speaking_normal
        How's your guitar? Is it doing well?
        
        -> M_H1_C2
    * [End convo]
        -> END

= M_H1_C1
#speaker: MJ #portrait: mj_speaking_normal
#speaker: Harvey #portrait: harvey_chill_normal
I'm practicing some songs.
    * [Practicing on a Sunday?]
        #speaker: MJ #portrait: mj_speaking_normal
        #speaker: Harvey #portrait: harvey_speaking_normal
        Practicing on a Sunday? Shouldn't you be resting?
        
        #speaker: MJ #portrait: mj_speaking_normal
        #speaker: Harvey #portrait: harvey_chill_normal
        Rest is for the weak. And I'm not weak.
        
            -> END
    * [How about taking...]
        #speaker: MJ #portrait: mj_speaking_normal
        #speaker: Harvey #portrait: harvey_speaking_normal
        How about taking a break?
        
        #speaker: MJ #portrait: mj_speaking_normal
        #speaker: Harvey #portrait: harvey_chill_normal
        I'll take a break when I'm done. Can I get back to it now?
        
            -> END

= M_H1_C2
#speaker: MJ #portrait: mj_speaking_normal
#speaker: Harvey #portrait: harvey_chill_normal
Yeah. It seems like it's holding up pretty well right now. I might have to get a new one later on, but I'm good so far.
    * [That's good]
        #speaker: MJ #portrait: mj_speaking_normal
        #speaker: Harvey #portrait: harvey_speaking_normal
        Sweet, just let me know if anything changes, okay?
        
        #speaker: MJ #portrait: mj_speaking_normal
        #speaker: Harvey #portrait: harvey_chill_normal
        I'll let you know then.
        
            -> END
    * [Maybe later on...]
        #speaker: MJ #portrait: mj_speaking_normal
        #speaker: Harvey #portrait: harvey_speaking_normal
        Maybe later on I can get you a new one. Sure it may work right now, but it does look a bit beat up.
        
        #speaker: MJ #portrait: mj_speaking_normal
        #speaker: Harvey #portrait: harvey_chill_normal
        Do whatever you think works then.
        
            -> END