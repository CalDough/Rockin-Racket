INCLUDE ../globals.ink

-> H1

=== H1 ===
= Ace
#speaker: Ace #portrait: Ace_neutral
What up dude? 
    * [Not much]
        #speaker: Harvey #portrait: Harvey_neutral
        Not much, just wanted to see what you were doing.
        
        -> A_H1_C1
    * [About your drums...]
        #speaker: Harvey #portrait: Harvey_neutral
        About your drums. They still in playing condition?
        
        -> A_H1_C2
    * [End convo]
        -> END

= A_H1_C1
#speaker: Ace #portrait: Ace_neutral
Oh, I'm watching TV. Wanna join?
    * [But it's off]
        #speaker: Harvey #portrait: Harvey_neutral
        But it's off, how are you watching it?.
        
        #speaker: Ace #portrait: Ace_neutral
        I'm imagining the episodes instead. I can explain it to you if you want?
            You know what, I'll pass.
            
                -> END
    * [I might later...]
        #speaker: Harvey #portrait: Harvey_neutral
        I might later, when there's actually something on.
        
        #speaker: Ace #portrait: Ace_neutral
        Understandable. Just let me know if you do so I can make some room for you.
        
            -> END

//Fix this section
= A_H1_C2
#speaker: Ace #portrait: Ace_neutral
Heck yeah I do! Are you offering?
    * [Yeah]
        #speaker: Harvey #portrait: Harvey_neutral
        Yeah, I am. I can get one soon.
        
        #speaker: Ace #portrait: Ace_neutral
        You're the man!
        
            -> END
    * [Not now]
        #speaker: Harvey #portrait: Harvey_neutral
        Not now, but I may be able to get one later in time.
        
        #speaker: Ace #portrait: Ace_neutral
        Bummer. But I'm looking forward to it when you do.
        
            -> END