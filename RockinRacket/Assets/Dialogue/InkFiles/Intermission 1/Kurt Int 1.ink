INCLUDE ../globals.ink

-> Convo


=== Convo ===

#speaker: Kurt #portrait: harvey_chill_normal #portrait: kurt_speaking_normal
Oh! H-hey Harvey. Um, uh, is something wrong?

    * [Not at all.]
    
    #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: kurt_chill_blank
    Not at all. Does something need to be wrong for me to talk to my little brother?
        -> C1


    * [Are you good?]
    
    #speaker: Harvey #portrait: harvey_speaking_normal #portrait: kurt_chill_hurt
    Are you okay? You look like you're about to fall apart.
        -> C2



= C1

#speaker: Kurt #portrait: harvey_chill_normal-alt #portrait: kurt_speaking_confused
I guess not. Sorry. Uh, how's your side of the c-concert going?

    * [Pretty good.]
    
    #speaker: Harvey #portrait: harvey_speaking_ambitious #portrait: kurt_chill_happy
    I think it's been pretty fluffing good so far. It's a lot of things coming at me at once, but nothing too troublesome.
    
    
    #speaker: Kurt #portrait: harvey_chill_normal-alt #portrait: kurt_speaking_normal-alt
    That's good. I was worried there'd be some d-difficulties for you, but I guess not. Hopefully it s-stays that way. Good luck!
        -> END
    
    * [Not the best.]
    
    #speaker: Harvey #portrait: harvey_speaking_normal #portrait: kurt_chill_blank
    I'm not really sure. I feel like I'm doing good but I also think there are so many things that I'm missing or am doing poorly on. Maybe it'll get better?
    
    
    #speaker: Kurt #portrait: harvey_chill_normal #portrait: kurt_speaking_sad
    Uh, yeah. It's hard starting out. Everything is new and it's t-tiring, but it's worth it in the end. J-just keep at it bro!
        -> END



= C2

#speaker: Kurt #portrait: harvey_chill_intrigued #portrait: kurt_speaking_sad
I-, uh, I'm okay. There's j-just more people than I thought there would be.

    * [Now that you mention it..]
    
    #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: kurt_chill_confused
    I noticed that too. Why is that? I thought you guys weren't that popular?
    
    
    #speaker: Kurt #portrait: harvey_chill_intrigued #portrait: kurt_speaking_confused
    We're still p-pretty new, so uh, I don't know. Maybe someone spread word about it? It's pretty nerve-wracking tho.
        -> END
    
    
    * [Are you scared of them?]
    
    #speaker: Harvey #portrait: harvey_speaking_normal #portrait: kurt_chill_normal
    Wait, are you scared of the audience? Why are you playing for them then?
    
    
    #speaker: Kurt #portrait: harvey_chill_normal #portrait: kurt_speaking_normal-alt
    I'm not scared, just uh, a b-bit anxious. I'll be fine tho. Thanks for worrying about me.
        -> END

