INCLUDE ../globals.ink

-> Convo


=== Convo ===

#speaker: Kurt #portrait: harvey_chill_normal-alt #portrait: kurt_speaking_happy
Oh! Hey Harvey! I really like this barn you got for us to use tonight.

    * [Glad to hear!]
    
    #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: kurt_chill_confused
    Thank god you like it! It's not what I was expecting so I figured it would turn out bad.
        ->C1
    
    
    * [You don't hate it?]
    
    #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: kurt_chill_blank
    Wait, you don't hate it? I figured with all the junk and hay everywhere that you'd have an issue with it.
        ->C2


= C1

#speaker: Kurt #portrait: harvey_chill_normal #portrait: kurt_speaking_normal-alt
Well it did take a bit of getting used to while we were playing. I was f-fighting a sneeze for a few minutes while playing and it was torture.

    * [Yeah...]
    
    #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: kurt_chill_normal
    Yeah, the dust really doesn't help out that much with that. But hey! It doesn't seem like it messed you up too much while playing, so that's good.
    
    
    #speaker: Kurt #portrait: harvey_chill_normal-alt #portrait: kurt_speaking_normal
    That's t-true. Let's hope it doesn't bother me as much for the rest of the night.
    
        -> END
            

    * [Noted.]
    
    #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: kurt_chill_confused
    I'll keep that in mind if we wanna play here again. I could bring some fans or something to try and get the dust outside. Maybe some brooms would be nice as well.
    
    
    #speaker: Kurt #portrait: harvey_chill_normal #portrait: kurt_speaking_happy
    This place would definitely need a c-cleaning before we play here again. Not too much though or it'll ruin the ambiance it gives off.
    
        -> END

= C2

#speaker: Kurt #portrait: harvey_chill_intrigued #portrait: kurt_speaking_confused
Well it is a tad bit overwhelming at times, but it's still kinda neat. It gives the place a personality.

    * [You got a point.]
    
    #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: kurt_chill_happy
    Huh. I didn't think of it that way. I guess you're right. It's an interesting personality for sure.
    
    
    #speaker: Kurt #portrait: harvey_chill_normal-alt #portrait: kurt_speaking_happy
    See? Now you get it. Make sure to brush out the hay from your fur later though.
        -> END


    * [It still needs work.]
    
    #speaker: Harvey #portrait: harvey_speaking_normal #portrait: kurt_chill_blank
    I still think it could use a bit more work though. But never mind that for now. It works for tonight and that's all that matters.
    
    
    #speaker: Kurt #portrait: harvey_chill_normal-alt #portrait: kurt_speaking_normal-alt
    Exactly! Seems like the crowd is fine with it too, so it all worked out.
    
        -> END

