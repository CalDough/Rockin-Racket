INCLUDE ../globals.ink

-> H3

=== H3 ===
= Ace
#speaker: Ace #portrait: Ace_neutral
Oho, the villian of the evening. Here to kick me while I'm down?
    * [Really? You too?]
        #speaker: Harvey #portrait: Harvey_neutral
        Really? You too? I hoped you would be the understanding one.
        
        -> A_H3_C1
    * [End convo]
        -> END


= A_H3_C1
#speaker: Ace #portrait: Ace_neutral
I can't just let tyranny run rampant, can I? So, what do you want?
    * [I'm trying...]
        #speaker: Harvey #portrait: Harvey_neutral
        I'm trying to make amends with you guys.
        
        #speaker: Ace #portrait: Ace_neutral
        I for one suggest you stop trying then. Take that little application of yours and run along now.
        
            -> END
    * [What can I do?]
        #speaker: Harvey #portrait: Harvey_neutral
        What can I do to make it up to you?
        
        #speaker: Ace #portrait: Ace_neutral
        How about you let us be really nice and helpful to you for a few weeks and then have you randomly learn that we were using you for ulterior motives? Then you can see what the pain feels like. It seems fair to me.
        
            -> END