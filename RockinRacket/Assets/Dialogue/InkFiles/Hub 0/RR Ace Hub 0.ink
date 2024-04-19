INCLUDE ../globals.ink

-> H0

=== H0 ===
= Ace
#speaker: Ace #portrait: ace_speaking_challenging #portrait: harvey_chill_normal
So... Think you're ready for tomorrow?

    * [Nope!]
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: ace_chill_normal
        Not at all. But that's life so I guess I'm just gonna have to deal with it.
        
        -> A_H0_C1
    
    * [I think so!]
        #speaker: Harvey #portrait: harvey_speaking_ambitious #portrait: ace_chill_adventurous
        I think I am! As long as it doesn't get too crazy though.
        
        -> A_H0_C2


= A_H0_C1
#speaker: Ace #portrait: ace_speaking_normal #portrait: harvey_chill_normal
Ah, don't worry about it too much. It'll all work out in the end!

    * [You sure?]
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: ace_chill_um
        You sound confident in that. Why?
        
        #speaker: Ace #portrait: ace_speaking_doubtful #portrait: harvey_chill_normal
        Well nobody's expecting a whole lot from you right now anyways, so it should be fine.
        
        
        -> END
        
    * [I guess so.]
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: ace_chill_okiedokie
        I mean I guess it will. It's just going to be weird for me, that's all.
        
        #speaker: Ace #portrait: ace_speaking_normal #portrait: harvey_chill_normal-alt
        Dude, don't freak out so much about it. You did pretty well in the rehearsal so I know you'll do fine for the real thing too!
        
        
        -> END

= A_H0_C2
#speaker: Ace #portrait: ace_speaking_normal-alt2 #portrait: harvey_chill_normal-alt
See? That's the spirit! Keep that up and you'll do fine.



    * [How about you?]
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: ace_chill_normal
        How're you feeling? You ready to debut tomorrow?
        
        #speaker: ace #portrait: ace_speaking_tired #portrait: harvey_chill_normal-alt
        For sure I am. I've been mentally preparing for a week now, so I think I'm good. And if not, then oh well!
        
        -> END
        
    * [I can't wait!]
        #speaker: Harvey #portrait: harvey_speaking_ambitious #portrait: ace_chill_um
        Now I'm getting pumped for it. I don't think I'm gonna get much sleep though.
        
        #speaker: Ace #portrait: ace_speaking_normal #portrait: harvey_chill_normal-alt
        Make sure you go to bed early then. That should help you fall asleep sooner, even with the energy built up!
        
        
        -> END
