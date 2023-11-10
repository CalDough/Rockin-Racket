INCLUDE ../globals.ink

-> Convo


=== Convo ===
#speaker: MJ #portrait: mj_speaking_normal
#speaker: Harvey #portrait: harvey_chill_normal
What do you want?

    * [I'm sorry?]
    #speaker: MJ #portrait: mj_chill_normal
    #speaker: Harvey #portrait: harvey_speaking_normal
    Did I do something wrong? I just wanted to say hi to you, but now I'm worried.
        -> C1
        
    
    * [Actually yes]
    #speaker: MJ #portrait: mj_chill_normal
    #speaker: Harvey #portrait: harvey_speaking_normal
    Yes I do thank you very much. I was hoping that you could not look like you want to attack the audience?
        -> C2


= C1
#speaker: MJ #portrait: mj_speaking_normal
#speaker: Harvey #portrait: harvey_chill_normal
Sorry, force of habit. Usually when you come to me you need help with something mundane because you're too lazy to do it yourself.

    * [Okay, ouch]
    #speaker: MJ #portrait: mj_chill_normal
    #speaker: Harvey #portrait: harvey_speaking_normal
    Dang. So no confidence in me then? I'll just have to prove you wrong then, won't I?
    
    #speaker: MJ #portrait: mj_speaking_normal
    #speaker: Harvey #portrait: harvey_chill_normal
    You can try, but good luck with it. You're gonna have to do a lot better than just cleaning up trash and adjusting the volumes of our intruments.
        -> END

    * [I'm trying here]
    #speaker: MJ #portrait: mj_chill_normal
    #speaker: Harvey #portrait: harvey_speaking_normal
    Okay, I get it. You don't have any confidence in me and my ability to help. But I'm trying to change that by helping you guys here.
    
    #speaker: MJ #portrait: mj_speaking_normal
    #speaker: Harvey #portrait: harvey_chill_normal
    It's just that you've tried to change in the past and wound back up at the start. I just don't want that to happen here because it'll screw us up as well. You seem to be doing fine right now, but try and keep it that way.
        -> END



= C2
#speaker: MJ #portrait: mj_speaking_normal
#speaker: Harvey #portrait: harvey_chill_normal
One of them was looking at me funny and I didn't appreciate it.

    * [Wait, really?]
    #speaker: MJ #portrait: mj_chill_normal
    #speaker: Harvey #portrait: harvey_speaking_normal
    Was one of them actually looking at you weird? What if you just ignore them then?
    
    #speaker: MJ #portrait: mj_speaking_normal
    #speaker: Harvey #portrait: harvey_chill_normal
    I was trying to, but every time we made eye-contact they made a stupid face. I'll try to just not look at them then, but it's hard with a small crowd.
        -> END
    
    * [Make one back?]
    #speaker: MJ #portrait: mj_chill_normal
    #speaker: Harvey #portrait: harvey_speaking_normal
    What if you made a funny face back to them? It shows that they aren't getting a rise out of you, unlike the "I will fight you" face you were doing.
    
    #speaker: MJ #portrait: mj_speaking_normal   
    #speaker: Harvey #portrait: harvey_chill_normal
    I'll consider it. Thanks for the advice Harvey.
        -> END




-> END