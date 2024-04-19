INCLUDE ../globals.ink

-> Convo


=== Convo ===

#speaker: Ace #portrait: harvey_chill_normal #portrait: ace_speaking_normal
Yooo, Harv-man. What up?

    * [You're killing it!]
    
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: ace_chill_friendly
        You're absolutely killing it out there!
            ->C1
    
    
    * [How can you do this?]
    
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: ace_chill_normal
        How are you so good? I thought, you've only been playing drums for a year?
            ->C2


= C1

#speaker: Ace #portrait: harvey_chill_normal-alt #portrait: ace_speaking_normal-alt2
You're too kind, my guy. I'm just vibing with the others and playing from the heart.

    * [That's all it is?]
    
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: ace_chill_um
        Is that really all you're doing? "Playing from the heart?"
        
        
        #speaker: Ace #portrait: harvey_chill_intrigued #portrait: ace_speaking_challenging
        Well that and a bunch of practice. As MJ says, "practice makes pizza." Or something like that I guess. Pizza's pretty good so it checks out.
            -> END
            
    * [It's still cool.]
    
        #speaker: Harvey #portrait: harvey_speaking_ambitious #portrait: ace_chill_okiedokie
        Regardless of what it is, it's still pretty cool dude. You have some serious talent. Keep up the good work!
        
        
        #speaker: Ace #portrait: harvey_chill_normal-alt #portrait: ace_speaking_normal
        You too man. I see you over there handling everything else. I see you, and I appreciate you.
            -> END

= C2

#speaker: Ace #portrait: harvey_chill_normal-alt #portrait: ace_speaking_doubtful
Honestly man, sometimes I just zone out and let muscle memory from practicing take over. It's been working out so far.

    * [There's no way.]
    
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: ace_chill_normal
        No way does that actually work. How would it even work?
        
        
        #speaker: Ace #portrait: harvey_chill_intrigued #portrait: ace_speaking_normal-alt1
        I'm telling you man. I hit a state of zen and everything just flows. It's smooth and sweet, like a smoothie. 
        
        #speaker: Ace #portrait: harvey_chill_intrigued #portrait: ace_speaking_normal-alt2
        You'll get there eventually yourself man.
            -> END

    * [That can't be good.]
    
        #speaker: Harvey #portrait: harvey_speaking_annoyed #portrait: ace_chill_um
        Zoning out in the middle of playing can't be good. Do the others care not care or something?
        
        
        #speaker: Ace #portrait: harvey_chill_annoyed #portrait: ace_speaking_normal
        Nah. We talked about it one night during practice and they all seemed cool with it. 
        
        #speaker: Ace #portrait: harvey_chill_normal-alt #portrait: ace_speaking_doubtful
        Or at least until I mess up, and then they might be mad at me for it. MJ did say that she'll smack me with her guitar if I screw up tho.
            -> END


-> END