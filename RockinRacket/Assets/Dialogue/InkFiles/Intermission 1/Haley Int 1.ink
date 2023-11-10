INCLUDE ../globals.ink

-> Convo

=== Convo ===
#speaker: Haley #portrait: haley_speaking_normal
#speaker: Harvey #portrait: harvey_chill_normal
Hey Harv, you getting the hang of it now?
    * [Yeah!]
    #speaker: Haley #portrait: haley_chill_normal
    #speaker: Harvey #portrait: harvey_speaking_normal
    Yeah, I actually am. It's really fun!
        -> C1

    * [I'm struggling to]
    #speaker: Haley #portrait: haley_chill_normal
    #speaker: Harvey #portrait: harvey_speaking_normal
    It's actually a bit challenging right now. Moreso than I thought it would be.
        -> C2

= C1
#speaker: Haley #portrait: haley_speaking_normal
#speaker: Harvey #portrait: harvey_chill_normal
Really!? I figured you would have had a harder time adjusting to it.
    * [No way]
    #speaker: Haley #portrait: haley_chill_normal
    #speaker: Harvey #portrait: harvey_speaking_normal
    I admit that there's a bit of a learning curve to it, but it's not a problem at all.
    
    #speaker: Haley #portrait: haley_speaking_normal
    #speaker: Harvey #portrait: harvey_chill_normal
    Well that's good. Hopefully the second half of the concert is the same.
        -> END
    
    * [Did you really?]
    #speaker: Haley #portrait: haley_chill_normal
    #speaker: Harvey #portrait: harvey_speaking_normal
    You really didn't think I'd get the hang of it this fast? So little faith in me.
    
    #speaker: Haley #portrait: haley_speaking_normal
    #speaker: Harvey #portrait: harvey_chill_normal
    I'm sorry, you're right. Its just that you don't have the best reputation with doing good work. But hey, you've proved me wrong so far, so keep at it!
        ->END


= C2
#speaker: Haley #portrait: haley_speaking_normal
#speaker: Harvey #portrait: harvey_chill_normal
Don't sweat it bro. Just take charge and stay strong. That's how I did it when we started out.

    * [Are you sure?]
    #speaker: Haley #portrait: haley_chill_normal
    #speaker: Harvey #portrait: harvey_speaking_normal
    You really think that I can handle it all?
    
    #speaker: Haley #portrait: haley_speaking_normal  
    #speaker: Harvey #portrait: harvey_chill_normal
    Of course I do. Just have faith in yourself!
        ->END

    * [What if I mess up?]
    #speaker: Haley #portrait: haley_chill_normal
    #speaker: Harvey #portrait: harvey_speaking_normal
    What about if I screw it all up? The concert would be ruined.
    
    #speaker: Haley #portrait: haley_speaking_normal
    #speaker: Harvey #portrait: harvey_chill_normal
    Don't think about that right now dude. Even if you did, which I'm sure you won't, it'll just be a valuable lesson. You got this!
        -> END


-> END