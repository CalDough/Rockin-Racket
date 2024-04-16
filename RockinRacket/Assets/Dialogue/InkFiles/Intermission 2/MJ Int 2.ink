INCLUDE ../globals.ink

-> Convo


=== Convo ===

#speaker: MJ #portrait: harvey_chill_normal #portrait: mj_speaking_normal
Are you here to tell me that we can go home now?

    * [Not at all.]
    
    #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: mj_chill_irritated
    Unfortunately, no. Do you hate this place that much?
        ->C1
    
    
    * [Already?]
    
    #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: mj_chill_normal
    You already want to leave? We're just now halfway through the concert.
        ->C2


= C1

#speaker: MJ #portrait: harvey_chill_normal #portrait: mj_speaking_irritated
I mean I don't HATE it, but it's pretty bad. I just want to get out of here as soon as possible.

    * [Bear with it.]
    
    #speaker: Harvey #portrait: harvey_speaking_normal #portrait: mj_chill_normal
    Just try and bear with it for a bit longer and then we can leave. Once the audience leaves, it should be a bit better.
    
    
    #speaker: MJ #portrait: harvey_chill_normal-alt #portrait: mj_speaking_offended
    Fine. But only because it'd be lame for me to leave halfway through right now.
    
        -> END
            
    * [What's the issue?]
    
    #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: mj_chill_irritated
    Well what's the issue with it that's bothering you so much? Maybe I can try to fix it.
    
    
    #speaker: MJ #portrait: harvey_chill_normal #portrait: mj_speaking_very-irritated
    Dude. It's basically just everything. The straw, or hay, or whatever you wanna call it, keeps getting in my eyes. And it feels a bit cramped over on my side. It's just a mess.
    
    
        -> END




= C2

#speaker: MJ #portrait: harvey_chill_normal #portrait: mj_speaking_normal-alt2
And? I didn't get much sleep last night so I'm ready to just pass out. But I'd rather do that on my bed instead of here.

    * [Need energy?]
    
    #speaker: Harvey #portrait: harvey_speaking_normal #portrait: mj_chill_offended
    I mean, if you need to stay awake then you can always try jumping around and exercising? 
    
    #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: mj_chill_offended
    That should get the blood moving enough to give you the energy to finish off the night.
    
    
    #speaker: MJ #portrait: harvey_chill_normal-alt #portrait: mj_speaking_disgusted
    Me? Exercise? No thank you. I'll just try and tough it out instead.
        -> END


    * [Sleep later.]
    
    #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: mj_chill_normal
    Just try your best to stay awake for the rest of the gig and then you can sleep on the drive home. Deal?
    
    
    #speaker: MJ #portrait: harvey_chill_normal-alt #portrait: mj_speaking_normal
    I guess I can give it a shot. Sleeping in the car still isn't the best, but it's better than on any of this stupid straw.
    
        -> END


-> END