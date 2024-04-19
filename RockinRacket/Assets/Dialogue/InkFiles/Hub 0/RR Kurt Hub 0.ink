INCLUDE ../globals.ink

-> H0

=== H0 ===
= Kurt
#speaker: Kurt #portrait: kurt_speaking_normal-alt #portrait: harvey_chill_normal
There you are. I was hoping to talk to you. Having f-fun yet?

    * [I guess?]
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: kurt_chill_normal
        I haven't decided yet. It's not like I'm not having fun.
        
        -> K_H0_C1
    
    * [Is that all?]
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: kurt_chill_blank
        You just wanted to ask me if I was having fun? I figured you'd have more to say.
        
        -> K_H0_C2


= K_H0_C1
#speaker: Kurt #portrait: kurt_speaking_normal #portrait: harvey_chill_normal-alt
At least that's something. I'm sure it'll g-get even more f-fun later on.

    * [How about you?]
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: kurt_chill_confused
        What about you? Are you having fun with this?
        
        #speaker: Kurt #portrait: kurt_speaking_happy #portrait: harvey_chill_normal-alt
        Of course I am! It's s-stressful, sure. But I get to p-play with my family, so that helps.
        
        -> END
        
    * [Hopefully so.]
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: kurt_chill_confused
        I hope it does. It'll be a good way to make money alongside the fame.
        
        #speaker: Kurt #portrait: kurt_speaking_confused #portrait: harvey_chill_intrigued
        Huh. I guess it will. We haven't p-put much thought into the m-money side of it.
        
        -> END

= K_H0_C2
#speaker: Kurt #portrait: kurt_speaking_happy #portrait: harvey_chill_normal
Nope! That's pretty m-much all I wanted.

#speaker: Kurt #portrait: kurt_speaking_normal #portrait: harvey_chill_intrigued
Actually. Make sure to get plenty of sleep t-tonight. And don't stay up t-too late.

    * [Sure, "mom."]
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: kurt_chill_annoyed
        You sound like mom when I was your age. She always told me that because I would oversleep and miss some of school.
        
        #speaker: Kurt #portrait: kurt_speaking_sad #portrait: harvey_chill_normal-alt
        And from what I remember, it never w-worked. So please actually try this t-time.
        
        -> END
        
    * [I plan to.]
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: kurt_chill_normal
        Don't worry. I was already planning on doing that. I didn't sleep much last night so I'm already pretty tired.
        
        #speaker: Kurt #portrait: kurt_speaking_normal #portrait: harvey_chill_normal-alt
        Well g-goodnight then. I'll see you tomorrow!
        
        
        -> END
