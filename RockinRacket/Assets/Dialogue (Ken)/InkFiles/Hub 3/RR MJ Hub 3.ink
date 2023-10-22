INCLUDE ../globals.ink

-> H3

=== H3 ===
= MJ
#speaker: MJ #portrait: MJ_neutral
Look who it is. The betrayer.
    * [How many times do I...]
        #speaker: Harvey #portrait: Harvey_neutral
        How many times do I need to apologize and explain myself to you guys?
        
        -> M_H3_C1
    * [End convo]
        -> END

= M_H3_C1
#speaker: MJ #portrait: MJ_neutral
At least once more for me. How could you do something like that to us? To me?
    * [I was originally hoping that...]
        #speaker: Harvey #portrait: Harvey_neutral
        I was originally hoping that the experience would help me get into college. But ever since the gig at the barn, I loved playing with you guys and college seemed less desirable.
        
        #speaker: MJ #portrait: MJ_neutral
        You could have told us sooner about it. I'm sure we would have understood. But no, instead we had to figure it out ourselves after we grew attached to having you around helping us.
        
            -> END
    * [I wanted to tell you guys...]
        #speaker: Harvey #portrait: Harvey_neutral
        I wanted to tell you guys, but I just got so caught up in working. It made me forget all about the application until it fell out of my pocket. I swear I didn't mean to hurt you guys.
        
        #speaker: MJ #portrait: MJ_neutral
        Whether you meant to or not, it doesn't change the fact that you did. You broke our trust in you. That's something that's hard to fix, you know.
        
            -> END