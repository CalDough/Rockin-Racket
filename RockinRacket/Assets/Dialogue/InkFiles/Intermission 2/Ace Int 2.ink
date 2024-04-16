INCLUDE ../globals.ink

-> Convo


=== Convo ===

#speaker: Ace #portrait: harvey_chill_normal #portrait: ace_speaking_tired
You should take a seat, man. These hay bales are pretty comfy once you get past the poking feeling on your tail end.

    * [No thanks.]
    
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: ace_chill_okiedokie
        Uhh, no thanks. I'd prefer not to deal with that.
            ->C1
    
    
    * [You're sitting?]
    
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: ace_chill_um
        If it's poking you, then why are you sitting on it? There are other places to sit, or even stand.
        
            ->C2


= C1

#speaker: Ace #portrait: harvey_chill_normal-alt #portrait: ace_speaking_normal
Fair enough. We really should have brought some more stuff with us to avoid this. Wasn't this place supposed to be...

#speaker: Ace #portrait: harvey_chill_normal #portrait: ace_speaking_normal-alt1
you know...

#speaker: Ace #portrait: harvey_chill_annoyed #portrait: ace_speaking_doubtful
cleaner?

    * [Yeah, it was.]
    
        #speaker: Harvey #portrait: harvey_speaking_annoyed #portrait: ace_chill_suredude
        It was supposed to be. That's what the Cricetto's told me when I asked them about using it.
    
        #speaker: Ace #portrait: harvey_chill_normal #portrait: ace_speaking_bummed
        Well they clearly lied to you about it.
        
        #speaker: Ace #portrait: harvey_chill_intrigued #portrait: ace_speaking_normal-alt2
        Just keep that in mind in case there's a next time.
    
            -> END
            
    * [Just make do.]
    
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: ace_chill_normal
        Yeah, yeah. No need to tell me. Let's just try and make do with what we can for now. Deal?

        #speaker: Ace #portrait: harvey_chill_normal-alt #portrait: ace_speaking_normal-alt2
        I'm cool with that. Just gotta keep it out of my mouth and eyes while playing, but I can manage that.
    
            -> END

= C2

#speaker: Ace #portrait: harvey_chill_normal-alt #portrait: ace_speaking_normal-alt1
Dunno. It looked soft so I went for it, but once I realized that it wasn't actually soft, I was already on it. I just didn't feel like moving.

    * [Fair enough.]
    
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: ace_chill_exasperated
        That makes sense. I guess. I probably wouldn't want to get back up after just sitting down either.
        
        #speaker: Ace #portrait: harvey_chill_normal-alt #portrait: ace_speaking_normal
        Glad you understand bro. I can manage until it's time to get back out there, so don't worry about me.
        
            -> END


    * [Time's ticking.]
    
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: ace_chill_disappointed
        Well this intermission isn't going to last forever you know? You're gonna need to move regardless here in a bit. 
        
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: ace_chill_cmondude
        Do you want to spend that time being uncomfortable?
        
        #speaker: Ace #portrait: harvey_chill_normal-alt #portrait: ace_speaking_bummed
        I know, I know. I've kinda gotten used to it now, so I'll just chill until we actually have to go back out there.
        
            -> END


-> END



