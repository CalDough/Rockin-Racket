INCLUDE ../globals.ink

-> H2

=== H2 ===
= Ace
#speaker: Ace #portrait: Ace_neutral
Yooo, Harvey. What up man?
    * [That drum fill sounds...]
        #speaker: Harvey #portrait: Harvey_neutral
        That drum fill sounds pretty good. Is that new?
        
        -> A_H2_C1
    * [Is everything alright with...]
        #speaker: Harvey #portrait: Harvey_neutral
        Is everything alright with your drums?
        
        -> A_H2_C2
    * [End convo]
        -> END


= A_H2_C1
#speaker: Ace #portrait: Ace_neutral
This? Heck yeah it is. I've been working on it for a few hours so far. I'm so close to getting it down.
    * [A few hours?]
        #speaker: Harvey #portrait: Harvey_neutral
        A few hours? Shouldn't you get some sleep?
        
        #speaker: Ace #portrait: Ace_neutral
        Well I've been drinking some energy drinks so I think I'll be fine.
        
            -> END
    * [What's there left to add?]
        #speaker: Harvey #portrait: Harvey_neutral
        What's there left to add to it? It already sounds really cool.
        
        #speaker: Ace #portrait: Ace_neutral
        Appreciate it man. I'm just trying to figure out where else I can add some more cymbals to.
        
            -> END

= A_H2_C2
#speaker: Ace #portrait: Ace_neutral
Not really. I've grown attached to this set. Although I do need some more sticks to use.
    * [Okay then]
        #speaker: Harvey #portrait: Harvey_neutral
        Okay then. I'll look into getting you some fancy new drumsticks.
        
        #speaker: Ace #portrait: Ace_neutral
        Heck yeah!
        
            -> END
    * [Oh. Okay]
        #speaker: Harvey #portrait: Harvey_neutral
        Oh. Okay. I was not expecting that response.
        
        #speaker: Ace #portrait: Ace_neutral
        I got some surprises in me every once in a while.
        
            -> END