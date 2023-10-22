INCLUDE ../globals.ink

-> H2

=== H2 ===
= MJ
#speaker: MJ #portrait: MJ_neutral
Need something?
    * [Actually, yeah. Could you...]
        #speaker: Harvey #portrait: Harvey_neutral
        Actually, yeah. Could you lower the volume a bit?
        
        -> M_H2_C1
    * [Is your guitar...]
        #speaker: Harvey #portrait: Harvey_neutral
        Is your guitar holding up properly?
        
        -> M_H2_C2
    * [End convo]
        -> END

= M_H2_C1
#speaker: MJ #portrait: MJ_neutral
Is it bothering you?
    * [It is. I don't mind...]
        #speaker: Harvey #portrait: Harvey_neutral
        It is. I don't mind you practicing, but some of us are trying to get some sleep. So can you turn it down now?
        
        #speaker: MJ #portrait: MJ_neutral
        Fine. But next time just wait until I'm done before you go to bed.
        
            -> END
    * [Well considering you're in the garage...]
        #speaker: Harvey #portrait: Harvey_neutral
        Well considering you're in the garage, making the sounds echo and become louder, the yes. It is bothering me.
        
        #speaker: MJ #portrait: MJ_neutral
        Noted. I'll turn it down then.
        
            -> END

= M_H2_C2
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