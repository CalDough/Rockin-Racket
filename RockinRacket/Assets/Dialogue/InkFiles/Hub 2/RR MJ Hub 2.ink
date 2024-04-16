INCLUDE ../globals.ink

-> H2

=== H2 ===
= MJ
#speaker: MJ #portrait: mj_speaking_normal #portrait: harvey_chill_normal
Need something?
    
    * [You're being too loud.]
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: mj_chill_offended
        Yeah, could you turn your speakers down a bit? It's really loud.
        
            ->M_H2_C1
    
    * [How about that gig?]
        #speaker: Harvey #portrait: harvey_speaking_ambitious #portrait: mj_chill_irritated
        Did I make the right choice or what for that venue? It was great, right?
        
            ->M_H2_C2

= M_H2_C1
#speaker: MJ #portrait: mj_speaking_smug #portrait: harvey_chill_annoyed
Is it? I couldn't tell.

    * [You couldn't?]
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: mj_chill_normal
        What do you mean you couldn't tell? It's blaring like crazy. Do you need your ears checked?
        
        #speaker: MJ #portrait: mj_speaking_normal #portrait: harvey_chill_normal
        Honestly, I might. Let's just worry about it later, I need to finish practicing right now. 
        
        #speaker: MJ #portrait: mj_speaking_normal-alt1 #portrait: harvey_chill_normal-alt
        I'll go on ahead and turn it down or put on some headphones for you though. Let the others know I'm sorry, will you?
        
        -> END
        
    * [I'm getting complaints.]
        #speaker: Harvey #portrait: harvey_speaking_annoyed #portrait: mj_chill_surprised
        Yeah, it is. It's so bad that the others are complaining about it to me. Musicians are complaining that your music is too loud. 
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: mj_chill_normal
        Not something you hear everyday. Just either turn it down or move it to the garage, will you?
        
        #speaker: MJ #portrait: mj_speaking_normal-alt1 #portrait: harvey_chill_normal-alt
        I hear you, kinda. But don't worry, I'll just go on ahead and wrap it up quickly then. 
        
        #speaker: MJ #portrait: mj_speaking_irritated #portrait: harvey_chill_normal-alt
        They should be used to this by now though, they know I always practice at this time.
        
        -> END


= M_H2_C2
#speaker: MJ #portrait: mj_speaking_very-irritated #portrait: harvey_chill_normal
I thought you said it was renovated? It still very clearly looked like a run-down barn to me.

    * [I thought it was.]
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: mj_chill_irritated
        Hey, that's what I was told. I guess it's still undergoing renovation? Or maybe they haven't actually started yet? Or... they lied to me.
        
        #speaker: MJ #portrait: mj_speaking_normal #portrait: harvey_chill_normal
        If we're gonna have a gig at an unknown location, you need to scope it out first. 
        
        #speaker: MJ #portrait: mj_speaking_normal-alt2 #portrait: harvey_chill_intrigued
        If we want to make it to the big leagues, we need to double, maybe even triple check, everything we can.
        
        -> END
        
    * [It still worked great.]
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: mj_chill_irritated
        Yeah, I know. But hey, it still worked out great, right? The atmosphere was perfect for it. And the audience didn't seem to mind, so what's the big deal?
        
        #speaker: MJ #portrait: mj_speaking_disgusted #portrait: harvey_chill_intrigued
        The big deal is that I've been pulling hay out of my fur ever since we left. It's like it's growing out of me. 
        
        #speaker: MJ #portrait: mj_speaking_irritated #portrait: harvey_chill_normal-alt
        Not to mention my backpack. There's hay in pockets that I didn't even open. Please, don't make me play there ever again unless it's in a better state than that.
        
        -> END
