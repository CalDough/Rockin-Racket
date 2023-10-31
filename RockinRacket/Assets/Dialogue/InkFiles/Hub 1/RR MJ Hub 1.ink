INCLUDE ../globals.ink

-> H1

=== H1 ===
= MJ
#speaker: MJ #portrait: MJ_neutral
What do you need? I'm busy.
    * [Sorry to interrupt...]
        #speaker: Harvey #portrait: Harvey_neutral
        Sorry to interrupt, I just wanted to see what you got going on.
        
        -> M_H1_C1
    * [How's your guitar?]
        #speaker: Harvey #portrait: Harvey_neutral
        How's your guitar? Is it doing well?
        
        -> M_H1_C2
    * [End convo]
        -> END

= M_H1_C1
#speaker: MJ #portrait: MJ_neutral
I'm practicing some songs.
    * [Practicing on a Sunday?]
        #speaker: Harvey #portrait: Harvey_neutral
        Practicing on a Sunday? Shouldn't you be resting?
        
        #speaker: MJ #portrait: MJ_neutral
        Rest is for the weak. And I'm not weak.
        
            -> END
    * [How about taking...]
        #speaker: Harvey #portrait: Harvey_neutral
        How about taking a break?
        
        #speaker: MJ #portrait: MJ_neutral
        I'll take a break when I'm done. Can I get back to it now?
        
            -> END

= M_H1_C2
#speaker: MJ #portrait: MJ_neutral
Yeah. It seems like it's holding up pretty well right now. I might have to get a new one later on, but I'm good so far.
    * [That's good]
        #speaker: Harvey #portrait: Harvey_neutral
        Sweet, just let me know if anything changes, okay?
        
        #speaker: MJ #portrait: MJ_neutral
        I'll let you know then.
        
            -> END
    * [Maybe later on...]
        #speaker: Harvey #portrait: Harvey_neutral
        Maybe later on I can get you a new one. Sure it may work right now, but it does look a bit beat up.
        
        #speaker: MJ #portrait: MJ_neutral
        Do whatever you think works then.
        
            -> END