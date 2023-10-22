INCLUDE ../globals.ink

-> H2

=== H2 ===
= Haley
#speaker: Haley #portrait: Haley_neutral
Hey Harv! What can I do for you?
 * [About that venue...]
        #speaker: Harvey #portrait: Harvey_neutral
        About that venue, was it good or what?
        
        -> H_H2_C1
 * [Is your mic...]
        #speaker: Harvey #portrait: Harvey_neutral
        Is your mic still in working condition?
        
        -> H_H2_C2
 * [End convo]
        -> END


= H_H2_C1
#speaker: Haley #portrait: Haley_neutral
I'll admit, it was a lot better than I thought it would be.
    * [Great to hear!]
        #speaker: Harvey #portrait: Harvey_neutral
        Great to hear! I'll keep it in mind if we every want to play there again.
        
        #speaker: Haley #portrait: Haley_neutral
        Oh for sure, it was great!
        
            -> END
    * [Thank goodness]
        #speaker: Harvey #portrait: Harvey_neutral
        Thank goodness. You were worried about it before so I was hoping you liked it.
        
        #speaker: Haley #portrait: Haley_neutral
        You gotta admit, it did sound sketchy. But it worked out in the end and the crowd loved the vibe. That's all that matters.
        
            -> END

= H_H2_C2
#speaker: Haley #portrait: Haley_neutral
I don't think I actually need one right now, but I'd be happy to get one if you are offering?
    * [Give me some time]
        #speaker: Harvey #portrait: Harvey_neutral
        Give me some time and I might be able to. Gotta have good equipment to help you perform.
        
        #speaker: Haley #portrait: Haley_neutral
        Ain't that the truth!
        
            -> END
    * [Maybe not]
        #speaker: Harvey #portrait: Harvey_neutral
        Maybe not now that I think about it. Let me check with the others though.
        
        #speaker: Haley #portrait: Haley_neutral
        It's all good! Just let me know.
        
            -> END