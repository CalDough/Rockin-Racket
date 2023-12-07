INCLUDE ../globals.ink

-> Convo


=== Convo ===

#speaker: MJ #portrait: harvey_chill_normal #portrait: mj_speaking_normal
What do you want?

    * [I'm sorry?]
    
    #speaker: Harvey #portrait: harvey_speaking_normal #portrait: mj_chill_normal
    I just wanted to say hi to you, but now I'm worried. Did I do something?
        -> C1
        
    
    * [Fix your face]
    
    #speaker: Harvey #portrait: harvey_speaking_normal #portrait: mj_chill_normal
    I was hoping that you could change your face out there to not look like you want to attack the audience?
        -> C2


= C1

#speaker: MJ #portrait: harvey_chill_normal #portrait: mj_speaking_normal
Sorry, force of habit. Usually when you come to me you need help with something mundane because you're too lazy to do it yourself.

    * [Okay, ouch]
    
    #speaker: Harvey #portrait: harvey_speaking_normal #portrait: mj_chill_normal
    Ouch! So no confidence in me then? I'll just have to prove you wrong then.
    
    
    #speaker: MJ #portrait: harvey_chill_normal #portrait: mj_speaking_normal
    Good luck with that. You're gonna have to do a lot better than what you're doing out there.
        -> END

    * [I'm trying here]
    
    #speaker: Harvey #portrait: harvey_speaking_normal #portrait: mj_chill_normal
    Okay, I get it. You don't have any confidence in me and my ability to help. But I'm trying to change that by helping you guys here.
    
    
    #speaker: MJ #portrait: harvey_chill_normal #portrait: mj_speaking_normal
    Well you haven't had much luck in the past, you know? I don't want you giving up on us or screwing up because it means a lot to us. It means a lot to me. Don't screw us over.
        -> END



= C2

#speaker: MJ #portrait: harvey_chill_normal #portrait: mj_speaking_normal
HEY! One of them was looking at me funny and I didn't appreciate it. Don't diss me for that. He got what he deserved.

    * [Wait, really?]
    
    #speaker: Harvey #portrait: harvey_speaking_normal #portrait: mj_chill_normal
    Actually? That's kinda messed up. Could you just try to ignore him then?
    
    
    #speaker: MJ #portrait: harvey_chill_normal #portrait: mj_speaking_normal
    I was trying to, but every time we made eye-contact he made a stupid face. It's difficult to not look at him now that I know he's there.
        -> END
    
    * [Make one back?]
    
    #speaker: Harvey #portrait: harvey_speaking_normal #portrait: mj_chill_normal
    What if you made a funny face back to them instead? It shows that they aren't getting a rise out of you, unlike the "I will fight you" face you were doing earlier.
    
      
    #speaker: MJ #portrait: harvey_chill_normal #portrait: mj_speaking_normal
    I'll consider it. How about that?
        -> END


