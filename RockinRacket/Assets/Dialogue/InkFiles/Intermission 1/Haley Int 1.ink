INCLUDE ../globals.ink

-> Convo

=== Convo ===
#speaker: Haley #portrait: Haley_neutral
Hey Harv, you getting the hang of it now?
    * [Yeah!]
    #speaker: Harvey #portrait: Harvey_neutral
    Yeah, I actually am. It's really fun!
        -> C1

    * [I'm struggling to]
    #speaker: Harvey #portrait: Harvey_neutral
    It's actually a bit challenging right now. Moreso than I thought it would be.
        -> C2

= C1
#speaker: Haley #portrait: Haley_neutral
Really!? I figured you would have had a harder time adjusting to it.
    * [No way]
    #speaker: Harvey #portrait: Harvey_neutral
    I admit that there's a bit of a learning curve to it, but it's not a problem at all.
    
    #speaker: Haley #portrait: Haley_neutral    
    Well that's good. Hopefully the second half of the concert is the same.
        -> END
    
    * [Did you really?]
    #speaker: Harvey #portrait: Harvey_neutral
    You really didn't think I'd get the hang of it this fast? So little faith in me.
    
    #speaker: Haley #portrait: Haley_neutral    
    I'm sorry, you're right. Its just that you don't have the best reputation with doing good work. But hey, you've proved me wrong so far, so keep at it!
        ->END


= C2
#speaker: Haley #portrait: Haley_neutral
Don't sweat it bro. Just take charge and stay strong. That's how I did it when we started out.

    * [Are you sure?]
    #speaker: Harvey #portrait: Harvey_neutral
    You really think that I can handle it all?
    
    #speaker: Haley #portrait: Haley_neutral    
    Of course I do. Just have faith in yourself!
        ->END

    * [What if I mess up?]
    #speaker: Harvey #portrait: Harvey_neutral
    What about if I screw it all up? The concert would be ruined.
    
    #speaker: Haley #portrait: Haley_neutral
    Don't think about that right now dude. Even if you did, which I'm sure you won't, it'll just be a valuable lesson. You got this!
        -> END


-> END