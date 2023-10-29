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
I could use one. You buying?
    * [I got you]
        #speaker: Harvey #portrait: Harvey_neutral
        I got you. Give me a bit and you'll have it.
        
        #speaker: MJ #portrait: MJ_neutral
        Thanks.
        
            -> END
    * [Maybe later]
        #speaker: Harvey #portrait: Harvey_neutral
        Maybe later. I need to rack up some more cash before I buy it.
        
        #speaker: MJ #portrait: MJ_neutral
        I won't get my hopes up too much then.
        
            -> END