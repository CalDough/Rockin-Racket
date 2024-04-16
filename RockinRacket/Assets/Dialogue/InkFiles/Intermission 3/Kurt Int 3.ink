INCLUDE ../globals.ink

-> Convo


=== Convo ===

#speaker: Kurt #portrait: harvey_chill_normal-alt #portrait: kurt_speaking_normal-alt
It's crazy out there Harvey. Like REALLY c-crazy.

    * [You good?]
    
    #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: kurt_chill_confused
    How're you doing with that? It seems like you're managing, but I wanted to ask.
        ->C1
    
    
    * [Hang in there.]
    
    #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: kurt_chill_blank
    Just hang in there Kurt. We're almost done with this battle and then we'll have a break to recover.
        ->C2


= C1

#speaker: Kurt #portrait: harvey_chill_normal-alt #portrait: kurt_speaking_normal-alt
I think so? Dealing with it in the moment isn't hard, b-but then the aftermath hits me.

    * [It sucks.]
    
    #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: kurt_chill_normal
    Yeah, that part of this does suck. The adrenaline helps you out there, but then it's gone once you're not playing.
    
    
    #speaker: Kurt #portrait: harvey_chill_normal-alt #portrait: kurt_speaking_sad
    Not to mention how t-tired it makes you feel. Now I just feel like taking a nap.
    
        -> END
            

    * [Drink some water.]
    
    #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: kurt_chill_blank
    Go grab some water and drink it. That should help give you something else to focus on, but also keeps you hydrated.
    
    
    #speaker: Kurt #portrait: harvey_chill_intrigued #portrait: kurt_speaking_normal
    Good call. My mouth feels like a d-desert. Obviously without the sand though.
    
        -> END

= C2

#speaker: Kurt #portrait: harvey_chill_intrigued #portrait: kurt_speaking_confused
Don't we have more battles if we win this one though? So it won't be that much of a break.

    * [We do.]
    
    #speaker: Harvey #portrait: harvey_speaking_normal #portrait: kurt_chill_blank
    Yeah, we do have some more to go through if we keep winning. But we get to rest while the other battles are going on. 
    
    #speaker: Harvey #portrait: harvey_speaking_normal #portrait: kurt_chill_blank
    But the breaks will get shorter later on, so make good use of the ones right now.
    
    
    #speaker: Kurt #portrait: harvey_chill_normal-alt #portrait: kurt_speaking_sad
    I hope we win it all, but I know that it's not gonna be f-fun for me to deal with this each time.
    
    #speaker: Kurt #portrait: harvey_chill_normal-alt #portrait: kurt_speaking_normal
    But oh well.
    
        -> END


    * [It's a tournament.]
    
    #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: kurt_chill_blank
    Kurt, it's a tournament. Did you forget already? If we're aiming to win it all, we're still gonna need to play against several more bands.
    
    
    #speaker: Kurt #portrait: harvey_chill_normal-alt #portrait: kurt_speaking_sad
    I thought it would only be two or three matches. We might not make it back in time for me to catch the new episode of "The Three Mousekateers." Crap. 
    
        -> END

