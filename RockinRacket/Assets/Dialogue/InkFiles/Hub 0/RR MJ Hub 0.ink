INCLUDE ../globals.ink

-> H0

=== H0 ===
= MJ
#speaker: MJ #portrait: mj_speaking_smug #portrait: harvey_chill_normal
Oh good. You haven't run away yet.

    * [Real funny.]
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: mj_chill_normal
        Ha ha. Real funny MJ. Glad to know you think so little of me.
        
        -> M_H0_C1
    
    * [Not yet.]
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: mj_chill_normal
        There's still time for me to change my mind.
        
        -> M_H0_C2


= M_H0_C1
#speaker: MJ #portrait: mj_speaking_normal-alt1 #portrait: harvey_chill_normal-alt
Relax, it's a joke. There's still some potential for you.

    * [Good to hear.]
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: mj_chill_happy
        Well that's good to hear. I was expecting you to trash-talk me some more.
        
        #speaker: MJ #portrait: mj_speaking_normal-alt3 #portrait: harvey_chill_intrigued
        Nah. That comes later on once you've been helping us a bit longer.
        
        -> END
        
    * [A joke?]
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: mj_chill_irritated
        That was supposed to be a joke? Sorry, I just thought jokes were supposed to be funny.
        
        #speaker: MJ #portrait: mj_speaking_normal-alt1 #portrait: harvey_chill_normal-alt
        Fair play. Keep it up and I might get you a clown nose to match your large paws.
        
        -> END

= M_H0_C2
#speaker: MJ #portrait: mj_speaking_normal #portrait: harvey_chill_normal-alt
Let's not do anything crazy just yet. Haley would kill me if I chased you off this early.

    * [Don't worry.]
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: mj_chill_happy
        Don't worry about it. I'm not much of a runner anyways.
        
        #speaker: MJ #portrait: mj_speaking_normal-alt1 #portrait: harvey_chill_normal-alt
        Good. I'd hate to lose the help.
        
        -> END
        
    * [I bet.]
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: mj_chill_irritated
        Oh I bet she would too. Or at least she'd rip out a bunch of your fur.
        
        #speaker: MJ #portrait: mj_speaking_disgusted #portrait: harvey_chill_normal-alt
        I get the chills just thinking about that. Let's hope it doesn't come to that.

        
        
        -> END
